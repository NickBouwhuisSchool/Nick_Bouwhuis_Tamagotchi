using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Nick_Bouwhuis_Tamagotchi
{
    //middle size of tamagotchi, 256x 248
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D tamTexture;
        private SpriteFont creditFont;
        private CharacterStateManager stateManager = new CharacterStateManager();

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.PreferredBackBufferHeight = 512;
            _graphics.PreferredBackBufferWidth = 512;

        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            stateManager.Initialize();
            






            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);


            tamTexture = Content.Load<Texture2D>("tamagotchi");
            creditFont = Content.Load<SpriteFont>("File");

            stateManager.Load(Content);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            stateManager.Update(gameTime);
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();

            stateManager.Draw(gameTime, _spriteBatch);

            _spriteBatch.Draw(tamTexture, Vector2.Zero, Color.White);
            _spriteBatch.DrawString(creditFont, "Icon made by Webalys,\n https://www.flaticon.com/authors/webalys", Vector2.Zero, Color.White);



            _spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
