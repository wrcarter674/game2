﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MonoGameWindowsStarter
{
    /// <summary>
    /// A delegate for spawning particles
    /// </summary>
    /// <param name="particle">the particle to spawn</param>
    public delegate void ParticleSpawner(ref Particle particle);

    /// <summary>
    /// A delegate for updating particles
    /// </summary>
    /// <param name="deltaT">The seconds elapsed between frames</param>
    /// <param name="particle">The particle to update</param>
    public delegate void ParticleUpdater(float deltaT, ref Particle particle);
    public class ParticleSystem
    {
        /// <summary>
        /// Constructs a new Particle System
        /// </summary>
        /// <param name="graphicsDevice">the graphics device</param>
        /// <param name="size">the max number of particles in the system</param>
        /// <param name="texture">The texture of the particles.</param>
        public ParticleSystem(GraphicsDevice graphicsDevice, int size, Texture2D texture)
        {
            this.particles = new Particle[size];
            this.spriteBatch = new SpriteBatch(graphicsDevice);
            this.texture = texture;

        }

        /// <summary> 
        /// Updates the particle system, spawining new particles and 
        /// moving all live particles around the screen 
        /// </summary>
        /// <param name="gameTime">A structure representing time in the game</param>
        public void Update(GameTime gameTime)
        {
            // Make sure our delegate properties are set
            if (SpawnParticle == null || UpdateParticle == null) return;

            // Part 1: Spawn new particles 
            for (int i = 0; i < SpawnPerFrame; i++)
            {
                // Create the particle
                SpawnParticle(ref particles[nextIndex]);

                // Advance the index 
                nextIndex++;
                if (nextIndex > particles.Length - 1) nextIndex = 0;
            }

            // Part 2: Update Particles
            float deltaT = (float)gameTime.ElapsedGameTime.TotalSeconds;
            for (int i = 0; i < particles.Length; i++)
            {
                // Skip any "dead" particles
                if (particles[i].Life <= 0) continue;

                // Update the individual particle
                UpdateParticle(deltaT, ref particles[i]);
            }
        }

        /// <summary>
        /// Draw the active particles in the particle system
        /// </summary>
        public void Draw()
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive);

            // Iterate through the particles
            for (int i = 0; i < particles.Length; i++)
            {
                // Skip any "dead" particles
                if (particles[i].Life <= 0) continue;

                // Draw the individual particles
                spriteBatch.Draw(texture, particles[i].Position, null, particles[i].Color, 0f, Vector2.Zero, particles[i].Scale, SpriteEffects.None, 0);
            }

            spriteBatch.End();
        }

        /// <summary>
        /// Holds a delegate to use when spawning a new particle
        /// </summary>
        public ParticleSpawner SpawnParticle { get; set; }

        /// <summary>
        /// Holds a delegate to use when updating a particle 
        /// </summary>
        /// <param name="particle"></param>
        public ParticleUpdater UpdateParticle { get; set; }

        /// <summary>
        /// the next index in the particles array to use when spawning a particle
        /// </summary>
        int nextIndex = 0;
        /// <summary>
        /// The collection of particles
        /// </summary>
        Particle[] particles;

        /// <summary>
        /// The texture this particle system uses
        /// </summary>
        Texture2D texture;

        /// <summary>
        /// The SpriteBatch this particle system uses
        /// </summary>
        SpriteBatch spriteBatch;

        /// <summary>
        /// A random number generator for this Particle System
        /// </summary>
        Random random = new Random();

        /// <summary>
        /// the emitter location for this particle system. AKA where the particles come from
        /// </summary>
        public Vector2 Emitter { get; set; }

        /// <summary>
        /// the rate of particle spawning.
        /// </summary>
        public int SpawnPerFrame { get; set; }

    }
}
