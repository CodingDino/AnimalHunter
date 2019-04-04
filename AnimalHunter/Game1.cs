using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

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

            // Make our hunter chase the prey
            // (if it isn't already on top of our prey)
            if (hunterPosition != preyPosition)
            {

                // Vector subtraction, normalisation, scalar multiply, addition
                // Determine a direction using target vector minus current vector positions
                Vector2 direction = preyPosition - hunterPosition;
                // Determine how far we are from our target
                float distanceToTarget = direction.Length();
                // This is not length 1! (aka unit vector) - we need to normalise it
                // (this means dividing by the magnitude, but XNA does this for us)
                // This keeps the vector pointing in the same direction, but sets length to 1
                direction.Normalize();
                // We need a scalar to multiply our vector by to represent how far we are moving
                float speed = 100f; // How far we are moving in pixels per second
                                    // Determine how far the hunter should move this frame by multiplying speed by time passed this frame
                float pixelsToMove = speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

                // Check if we are overshooting
                // If we will move farther than the actual distance to our target, then we are overshooting
                if (pixelsToMove >= distanceToTarget)
                {
                    // We would overshoot, so instead, just move to the target's positoin
                    hunterPosition = preyPosition;
                }
                else
                {
                    // We are not overshooting (have some ways to go first)

                    // Generate our movement vector by scaling up our length 1 direction vector using our distance to move
                    Vector2 moveVector = direction * pixelsToMove;
                    // Add the move vector to the hunter's positoin to move the hunter based on the move vector
                    hunterPosition = hunterPosition + moveVector;
                }
            }

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
            // First determine direction based on vector subtraction
            Vector2 direction = preyPosition - hunterPosition;
            // Then determine angle using atan (arctangent aka inverse tangent)
            float hunterRotation = (float)(Math.Atan2(direction.Y, direction.X) + Math.PI * 0.5);
            // Calculate origin to be centre of the sprite
            Vector2 hunterOrigin = new Vector2(hunterTexture.Width / 2, hunterTexture.Height / 2);
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
