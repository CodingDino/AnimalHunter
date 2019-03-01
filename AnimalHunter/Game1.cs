using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AnimalHunter
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private Texture2D preyTexture;
        private Texture2D hunterTexture;
        private Vector2 preyPosition = new Vector2(900, 500);
        private Vector2 hunterPosition = new Vector2(200, 200);

        SpriteFont gameFont;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
            graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
            graphics.ApplyChanges();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            preyTexture = Content.Load<Texture2D>("Animals/duck");
            hunterTexture = Content.Load<Texture2D>("Animals/dog");

            gameFont = Content.Load<SpriteFont>("arial");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            spriteBatch.Begin();


            // Draw Prey Sprite
            // - using vector positoining
            float preyRotation = 0;
            Vector2 preyOrigin = Vector2.Zero;
            Rectangle preyRect = new Rectangle((int)preyPosition.X, (int)preyPosition.Y, preyTexture.Width, preyTexture.Height);
            spriteBatch.Draw(preyTexture,
                preyRect,
                null,
                Color.White,
                preyRotation,
                preyOrigin,
                SpriteEffects.None,
                0);

            // Draw Hunter Sprite
            // - using vector positoining
            float hunterRotation = 0;
            Vector2 hunterOrigin = Vector2.Zero;
            Rectangle hunterRect = new Rectangle((int)hunterPosition.X, (int)hunterPosition.Y, hunterTexture.Width, hunterTexture.Height);
            spriteBatch.Draw(hunterTexture,
                hunterRect,
                null,
                Color.White,
                hunterRotation,
                hunterOrigin,
                SpriteEffects.None,
                0);


            // Draw labels
            // Use vector addition to add an offset to each position
            Vector2 offset = new Vector2(50, -50);
            spriteBatch.DrawString(gameFont, "Prey", preyPosition + offset, Color.White);
            spriteBatch.DrawString(gameFont, "Hunter", hunterPosition + offset, Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
