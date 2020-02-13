﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGameWindowsStarter
{
    /// <summary>
    /// An enum representing the states the player can be in
    /// </summary>
    /// 
    enum State
    {
        South = 0,
        East = 3, 
        West = 1,
        North = 2,
        Idle = 4,
    }

    /// <summary>
    /// A class representing a player
    /// </summary>
    public class Player
    {
        /// <summary>
        /// How quickly the animation should advance frames (1/8 second as milliseconds)
        /// </summary>
        const int ANIMATION_FRAME_RATE = 124;
        public BoundingRectangle Bounds;
        /// <summary>
        /// How quickly the player should move
        /// </summary>
        const float PLAYER_SPEED = 400;

        /// <summary>
        /// The width of the animation frames
        /// </summary>
        const int FRAME_WIDTH = 40;

        /// <summary>
        /// The hieght of the animation frames
        /// </summary>
        const int FRAME_HEIGHT = 40;

        // Private variables
        Game1 game;
        Texture2D texture;
        State state;
        TimeSpan timer;
        int frame;
        Vector2 position;
      //  SpriteFont font;

        /// <summary>
        /// Creates a new player object
        /// </summary>
        /// <param name="game"></param>
        public Player(Game1 game)
        {
            this.game = game;
            timer = new TimeSpan(0);
            position = new Vector2(1042/2, 768-FRAME_HEIGHT);
            state = State.Idle;
            Bounds.Width = 40;
            Bounds.Height = 40;
            Bounds.X = position.X;
            Bounds.Y = position.Y;
        }

        /// <summary>
        /// Loads the sprite's content
        /// </summary>
        public void LoadContent()
        {
            texture = game.Content.Load<Texture2D>("Frog_Sprites");
       //     font = game.Content.Load<SpriteFont>("defaultFont");
        }

        /// <summary>
        /// Update the sprite, moving and animating it
        /// </summary>
        /// <param name="gameTime">The GameTime object</param>
        public void Update(GameTime gameTime)
        {
            KeyboardState keyboard = Keyboard.GetState();
            float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            //check edges
            if(position.X <= 0)
            {
                position.X = 0;
                Bounds.X = position.X;
            }
            if(position.Y >= 768-Bounds.Height)
            {
                position.Y = 768 - Bounds.Height;
                Bounds.Y = position.Y;
            }
            if(position.X >= 1042 - Bounds.Width)
            {
                position.X = 1042 - Bounds.Width;
                Bounds.X = 1042 - Bounds.Width;
            }
            if(position.Y <= 0)
            {
                position.Y = 0;
                Bounds.Y = position.Y;
            }
            // Update the player state based on input
            if (keyboard.IsKeyDown(Keys.Up))
            {
                state = State.North;
                position.Y -= delta * PLAYER_SPEED;
                Bounds.Y = position.Y;
            }
            else if (keyboard.IsKeyDown(Keys.Left))
            {
                state = State.West;
                position.X -= delta * PLAYER_SPEED;
                Bounds.X = position.X;
            }
            else if (keyboard.IsKeyDown(Keys.Right))
            {
                state = State.East;
                position.X += delta * PLAYER_SPEED;
                Bounds.X = position.X;
            }
            else if (keyboard.IsKeyDown(Keys.Down))
            {
                state = State.South;
                position.Y += delta * PLAYER_SPEED;
                Bounds.Y = position.Y;
            }
            else  state = State.Idle;

            // Update the player animation timer when the player is moving
            if(state != State.Idle) timer += gameTime.ElapsedGameTime;
            
            // Determine the frame should increase.  Using a while 
            // loop will accomodate the possiblity the animation should 
            // advance more than one frame.
            while(timer.TotalMilliseconds > ANIMATION_FRAME_RATE) {
                // increase by one frame
                frame++;
                // reduce the timer by one frame duration
                timer -= new TimeSpan(0, 0, 0, 0, ANIMATION_FRAME_RATE);
            }

            // Keep the frame within bounds (there are four frames)
            frame %= 3;
        }

        /// <summary>
        /// Renders the sprite on-screen
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            // determine the source rectagle of the sprite's current frame
            var source = new Rectangle(
                (int)state % 4 *FRAME_WIDTH, // X value 
                frame * FRAME_HEIGHT, // Y value
                FRAME_WIDTH, // Width 
                FRAME_HEIGHT // Height
                );

            // render the sprite
            spriteBatch.Draw(texture, position, source, Color.White);

            // render the sprite's coordinates in the upper-right-hand corner of the screen
        //    spriteBatch.DrawString(font, $"X:{position.X} Y:{position.Y}", Vector2.Zero, Color.White);
        }

        public void Reset()
        {
            state = State.Idle;
            position.X = 1042 / 2;
            position.Y = 768 - FRAME_HEIGHT;
            Bounds.X = position.X;
            Bounds.Y = position.Y;
        }

    }
}
