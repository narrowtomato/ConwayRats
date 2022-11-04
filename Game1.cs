using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ConwayRats
{
    public class Game1 : Game
    {
        Texture2D ratTexture;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Board game_board = new Board(10);

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.IsFullScreen = false;
            _graphics.PreferredBackBufferWidth = 320;
            _graphics.PreferredBackBufferHeight = 320;
            _graphics.ApplyChanges();

            game_board.state[1, 3] = true;
            game_board.state[1, 4] = true;
            game_board.state[1, 5] = true;
            game_board.state[4, 1] = true;
            game_board.state[0, 4] = true;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            ratTexture = Content.Load<Texture2D>("rat");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            game_board.Cycle(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (game_board.state[i, j])
                    {
                        _spriteBatch.Draw(ratTexture, new Vector2(32 * i, 32 * j), Color.White);
                    }
                }
            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}