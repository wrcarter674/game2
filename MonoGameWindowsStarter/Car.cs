using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;


namespace MonoGameWindowsStarter
{
    class Car
    {
        /// <summary>
        /// The game the ball belongs to
        /// </summary>
        Game1 game;

        /// <summary>
        /// The ball's texture
        /// </summary>
        Texture2D texture;

        /// <summary>
        /// The ball's bounds
        /// </summary>
        public BoundingRectangle Bounds;
        /// <summary>
        /// The ball's velocity vector
        /// </summary>
        public int Velocity;

        /// <summary>
        /// Creates a new ball
        /// </summary>
        /// <param name="game">The game the ball belongs to</param>
        public Car(Game1 game)
        {
            this.game = game;
        }

        /// <summary>
        /// Initializes the ball, placing it in the center 
        /// of the screen and giving it a random velocity
        /// vector of length 1.
        /// </summary>
        public void Initialize(int x, int y, int velocity)
        {
            Random ran = new Random();
            Bounds.Width = 60;
            Bounds.Height = 40; 
           
            Bounds.X = x;
            
            Bounds.Y = y;
            // position the ball in the center of the screen
            Velocity = velocity;
           // Bounds.X += offset * Velocity;
        }

        /// <summary>
        /// Loads the ball's texture
        /// </summary>
        /// <param name="content">The ContentManager to use</param>
        public void LoadContent(ContentManager content, int num)
        {
           
            switch (num)
            {
              
                case 1:
                    texture = content.Load<Texture2D>("blackCar2");
                    break;
           
                case 2:
                    texture = content.Load<Texture2D>("racecar2");
                    break;
            }            
        }


        /// <summary>
        /// Updates the ball's position and bounces it off walls
        /// </summary>
        /// <param name="gameTime">The current GameTime</param>
        public void Update(GameTime gameTime, int speeedLevel)
        {
            var viewport = game.GraphicsDevice.Viewport;
            if (Bounds.X < 0)
            {
                Bounds.X = viewport.Width;
            }
            if (Bounds.X > viewport.Width)
            {
                Bounds.X = 0;
            }
            switch (speeedLevel)
            {
                case 1:
                    Bounds.X += .4f * (float)gameTime.ElapsedGameTime.TotalMilliseconds * Velocity;
                    break;
                case 2:
                    Bounds.X += .6f * (float)gameTime.ElapsedGameTime.TotalMilliseconds * Velocity;
                    break;
                case 3:
                    Bounds.X += .9f * (float)gameTime.ElapsedGameTime.TotalMilliseconds * Velocity;
                    break;
            }
        }

        /// <summary>
        /// Draws the ball
        /// </summary>
        /// <param name="spriteBatch">
        /// The SpriteBatch to use to draw the ball.  
        /// This method should be invoked between 
        /// SpriteBatch.Begin() and SpriteBatch.End() calls.
        /// </param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Bounds, Color.White);
        }

      

    }
}
