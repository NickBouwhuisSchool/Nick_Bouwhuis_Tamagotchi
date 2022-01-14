﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using DiscoFramework;

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

        private Button leftButton;
        private Button rightButton;

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

            leftButton = new Button()
            {
                Active = true,
                Position = new Vector2(188, 428),
                Texture = Content.Load<Texture2D>("ButtonColor")
            };

            leftButton.OnClick += (sender, args) => LeftButtonPressed();

            rightButton = new Button()
            {
                Active = true,
                Position = new Vector2(324, 428),
                Texture = Content.Load<Texture2D>("ButtonColor")
            };

            rightButton.OnClick += (sender, args) => RightButtonPressed();

            stateManager.Load(Content);
            stateManager.Attention = 50;
            stateManager.Hunger = 80;

            stateManager.DecreaseTimer();
            stateManager.Input("");
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            stateManager.Update(gameTime);
            // TODO: Add your update logic here

            leftButton.Update(gameTime);
            rightButton.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();
            leftButton.DrawButton(_spriteBatch);
            rightButton.DrawButton(_spriteBatch);
            stateManager.Draw(gameTime, _spriteBatch);

            _spriteBatch.Draw(tamTexture, Vector2.Zero, Color.White);
            //_spriteBatch.DrawString(creditFont, "Icon made by Webalys,\n https://www.flaticon.com/authors/webalys", Vector2.Zero, Color.White);
            _spriteBatch.DrawString(creditFont, stateManager.Attention.ToString(), Vector2.Zero, Color.White);
            _spriteBatch.DrawString(creditFont, stateManager.Hunger.ToString(), new Vector2(480, 0), Color.White);


            _spriteBatch.End();


            base.Draw(gameTime);
        }
        private void LeftButtonPressed()
        {
            stateManager.ButtonInput = true;


        }
        private void RightButtonPressed()
        {
            stateManager.ButtonInput = true;


        }
        private void test()
        {
            
        }
    }
}
