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
        ParticleSystem youWin;
        ParticleSystem youDie;
        ParticleSystem bubbles;
        ParticleSystem rain;
        Texture2D particleTexture;
     
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
           
            //Load you win particle------------------------------------------------------------
            particleTexture = Content.Load<Texture2D>("Particle");
            youWin = new ParticleSystem(this.GraphicsDevice, 1000, particleTexture);
            youWin.SpawnPerFrame = 3;
            youWin.SpawnParticle = (ref Particle particle) =>
            {
               // MouseState mouse = Mouse.GetState();
                particle.Position = new Vector2(490, 50);
                particle.Velocity = new Vector2(
                    MathHelper.Lerp(-50, 50, (float)random.NextDouble()), // X between -50 and 50
                    MathHelper.Lerp(0, 100, (float)random.NextDouble()) // Y between 0 and 100
                    );
                particle.Acceleration = 0.1f * new Vector2(0, (float)-random.NextDouble());
                particle.Color = Color.Gold;
                particle.Scale = 1f;
                particle.Life = 1.0f;
            };
            // Set the UpdateParticle method
            youWin.UpdateParticle = (float deltaT, ref Particle particle) =>
            {
                particle.Velocity += deltaT * particle.Acceleration;
                particle.Position += deltaT * particle.Velocity;
                particle.Scale -= deltaT;
                particle.Life -= deltaT;
            };

            //Load Rain Particle----------------------------------------------------------------           
            rain = new ParticleSystem(this.GraphicsDevice, 1000, particleTexture);
            rain.SpawnPerFrame = 2;
            rain.SpawnParticle = (ref Particle particle) =>
            {
                // MouseState mouse = Mouse.GetState();
                particle.Position = new Vector2(random.Next(0,900), 0);
                particle.Velocity = new Vector2(
                    MathHelper.Lerp(0, 1, (float)random.NextDouble()), // X between -50 and 50
                    MathHelper.Lerp(0, 1042, (float)random.NextDouble()) // Y between 0 and 100
                    );
                particle.Acceleration = 0.1f * new Vector2(0, (float)-random.NextDouble());
                particle.Color = Color.Aqua;
                particle.Scale = 1f;
                particle.Life = 1.0f;
            };
            // Set the UpdateParticle method
            rain.UpdateParticle = (float deltaT, ref Particle particle) =>
            {
                particle.Velocity += deltaT * particle.Acceleration;
                particle.Position += deltaT * particle.Velocity;
                particle.Scale -= deltaT;
                particle.Life -= deltaT;
            };

            //Load youdie particle------------------------------------------------------------
            particleTexture = Content.Load<Texture2D>("Particle");
            youDie = new ParticleSystem(this.GraphicsDevice, 1000, particleTexture);
            youDie.SpawnPerFrame = 3;
           
            youDie.SpawnParticle = (ref Particle particle) =>
            {
                // MouseState mouse = Mouse.GetState();
                particle.Position = new Vector2(0,0);
                particle.Velocity = new Vector2(
                    MathHelper.Lerp(-50, 50, (float)random.NextDouble()), // X between -50 and 50
                    MathHelper.Lerp(0, 100, (float)random.NextDouble()) // Y between 0 and 100
                    );
                particle.Acceleration = 0.1f * new Vector2(0, (float)-random.NextDouble());
                particle.Color = Color.Red;
                particle.Scale = 1f;
                particle.Life = 1.0f;
            };
            // Set the UpdateParticle method
            youDie.UpdateParticle = (float deltaT, ref Particle particle) =>
            {
                particle.Velocity += deltaT * particle.Acceleration;
                particle.Position += deltaT * particle.Velocity;
                particle.Scale -= deltaT;
                particle.Life -= deltaT;
            };

            //Load bubbles particle------------------------------------------------------------
            particleTexture = Content.Load<Texture2D>("Particle");
            bubbles = new ParticleSystem(this.GraphicsDevice, 1000, particleTexture);
            bubbles.SpawnPerFrame = 1;

            bubbles.SpawnParticle = (ref Particle particle) =>
            {
                // MouseState mouse = Mouse.GetState();
                particle.Position = new Vector2(player.Bounds.X+15, player.Bounds.Y+30);
                particle.Velocity = new Vector2(
                    MathHelper.Lerp(-50, 50, (float)random.NextDouble()), // X between -50 and 50
                    MathHelper.Lerp(0, 100, (float)random.NextDouble()) // Y between 0 and 100
                    );
                particle.Acceleration = 0.1f * new Vector2(0, (float)-random.NextDouble());
                particle.Color = Color.White;
                particle.Scale = 1f;
                particle.Life = 1.0f;
            };
            // Set the UpdateParticle method
            bubbles.UpdateParticle = (float deltaT, ref Particle particle) =>
            {
                particle.Velocity += deltaT * particle.Acceleration;
                particle.Position += deltaT * particle.Velocity;
                particle.Scale -= deltaT;
                particle.Life -= deltaT;
            };


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
            youWin.Update(gameTime);
            rain.Update(gameTime);
            youDie.Update(gameTime);
            bubbles.Update(gameTime);
            if (player.Bounds.Y < graphics.PreferredBackBufferHeight - 130) { deadFlag = false; }
            // TODO: Add your update logic here
            for (int i = 0; i < 4; i++)
            {
                if (player.Bounds.CollidesWith(levelOne[i].Bounds))
                {
                    splat.Play();
                    player.Reset();
                    deadFlag = true;
                    deathCount++;
                }
                else if (player.Bounds.CollidesWith(levelTwo[i].Bounds))
                {
                    splat.Play();
                    player.Reset();
                    deadFlag = true;
                    deathCount++;
                }
                else if (player.Bounds.CollidesWith(levelThree[i].Bounds))
                {
                    splat.Play();
                    player.Reset();
                    deadFlag = true;
                    deathCount++;
                }
                else if (player.Bounds.CollidesWith(levelFour[i].Bounds))
                {
                    splat.Play();
                    player.Reset();
                    deadFlag = true;
                    deathCount++;
                }
                else if (player.Bounds.CollidesWith(levelFive[i].Bounds))
                {
                    splat.Play();
                    player.Reset();
                    deadFlag = true;
                    deathCount++;
                }
                //Update cars
                levelFive[i].Update(gameTime, 3);
                levelFour[i].Update(gameTime, 3);
                levelThree[i].Update(gameTime, 3);
                levelTwo[i].Update(gameTime, 2);
                levelOne[i].Update(gameTime, 1);
            }
            if (player.Bounds.Y < 96) { finishFlag = true; } else { finishFlag = false;  }
           
            

            base.Update(gameTime);
        }
        bool finishFlag = false;
        bool deadFlag = false;
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of t;iming values.</param>
        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(SpriteSortMode.Immediate);
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Draw(background, new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight), Color.White);

            bubbles.Draw();
            player.Draw(spriteBatch);
            rain.Draw();
    
            // TODO: Add your drawing code here
            for (int i = 0; i < 4; i++)
            {
                levelFive[i].Draw(spriteBatch);
                levelFour[i].Draw(spriteBatch); 
                levelThree[i].Draw(spriteBatch);
                levelTwo[i].Draw(spriteBatch);
                levelOne[i].Draw(spriteBatch);
            }            
            if (finishFlag)
            {
                spriteBatch.DrawString(font, "You WIN!", new Vector2(490, 50), Color.White);
                youWin.Draw();
            }
            else
            {
                spriteBatch.DrawString(font, "Finish Line", new Vector2(490, 50), Color.White);
            }
            if (deadFlag)
            {
                youDie.Draw();
            }            
            spriteBatch.DrawString(font, "Death Count: "+deathCount, new Vector2(0, 0), Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
