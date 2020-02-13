using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;
using System;

namespace MonoGameWindowsStarter
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private SpriteFont font;
        private Texture2D background;
        private int deathCount;
        Player player;
        SoundEffect splat;
     
        Car[] levelFive = new Car[4];
        Car[] levelFour = new Car[4];
        Car[] levelThree = new Car[4];
        Car[] levelTwo = new Car[4];
        Car[] levelOne = new Car[4];

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            for (int i = 0; i < 4; i++)
            {
                levelFive[i] = new Car(this);
                levelFour[i] = new Car(this);
                levelThree[i] = new Car(this);
                levelTwo[i] = new Car(this);
                levelOne[i] = new Car(this);
            }
            
            player = new Player(this);
            
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = 1042;
            graphics.PreferredBackBufferHeight = 768;
            deathCount = 0;
            graphics.ApplyChanges();
            // TODO: Add your initialization logic here
            Random random = new Random();

            for(int i=0; i<4; i++)
            {
                levelFive[i].Initialize(1042 +(random.Next(0, 1042)*-1), 96, -1);
                levelFour[i].Initialize(0 + (random.Next(0, 1042)), 208, 1);
                levelThree[i].Initialize(1042 + (random.Next(0, 1042) * -1), 384, -1);
                levelTwo[i].Initialize(0 + (random.Next(0, 1042)), 480, 1);
                levelOne[i].Initialize(1042 + (random.Next(0, 1042) * -1), 576, -1);
            }
            
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
            splat = Content.Load<SoundEffect>("Splat");
            background = Content.Load<Texture2D>("Background");
            font = Content.Load<SpriteFont>("DefaultFont");
            Random random = new Random();
       
            for (int i=0;i < 4; i++)
            {
                levelFive[i].LoadContent(Content, random.Next(1, 3));
                levelFour[i].LoadContent(Content, random.Next(1, 3));
                levelThree[i].LoadContent(Content, random.Next(1, 3));
                levelTwo[i].LoadContent(Content, random.Next(1, 3));
                levelOne[i].LoadContent(Content, random.Next(1, 3));
            }
            player.LoadContent();
            // TODO: use this.Content to load your game content here
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
            player.Update(gameTime);
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
          
            // TODO: Add your update logic here
            for (int i = 0; i < 4; i++)
            {
                if (player.Bounds.CollidesWith(levelOne[i].Bounds))
                {
                    splat.Play();
                    player.Reset();
                    deathCount++;
                }
                else if (player.Bounds.CollidesWith(levelTwo[i].Bounds))
                {
                    splat.Play();
                    player.Reset();
                    deathCount++;
                }
                else if (player.Bounds.CollidesWith(levelThree[i].Bounds))
                {
                    splat.Play();
                    player.Reset();
                    deathCount++;
                }
                else if (player.Bounds.CollidesWith(levelFour[i].Bounds))
                {
                    splat.Play();
                    player.Reset();
                    deathCount++;
                }
                else if (player.Bounds.CollidesWith(levelFive[i].Bounds))
                {
                    splat.Play();
                    player.Reset();
                    deathCount++;
                }
                levelFive[i].Update(gameTime, 3);
                levelFour[i].Update(gameTime, 3);
                levelThree[i].Update(gameTime, 3);
                levelTwo[i].Update(gameTime, 2);
                levelOne[i].Update(gameTime, 1);
                

            }
            
             
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of t;iming values.</param>
        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Draw(background, new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight), Color.White);
            player.Draw(spriteBatch);
            levelFive[0].Draw(spriteBatch);
            levelFive[1].Draw(spriteBatch);
            levelFive[2].Draw(spriteBatch);
            levelFive[3].Draw(spriteBatch);
    
            // TODO: Add your drawing code here
            for (int i = 0; i < 4; i++)
            {
                
                levelFour[i].Draw(spriteBatch); ;
                levelThree[i].Draw(spriteBatch);
                levelTwo[i].Draw(spriteBatch);
                levelOne[i].Draw(spriteBatch);
            }
            spriteBatch.DrawString(font, "Finish Line", new Vector2(490, 50), Color.White);
            spriteBatch.DrawString(font, "Death Count: "+deathCount, new Vector2(0, 0), Color.Red);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
