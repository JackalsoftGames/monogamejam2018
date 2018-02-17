#region
/*
 * Author: Jackalsoft Games
 * Date: February 2018
 * Project: Presto! (#MonoGameJam2018 entry)
 * 
 * This code is provided as-is under the MIT license,
 * with no warranty express or implied.
 */

using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

using Presto;
using Presto.Extensions;
using Presto.Input;
#endregion

namespace Presto
{
    public sealed class ParticleStateManager :
        GameState.Component
    {
        #region Constructors
        public ParticleStateManager(GameState state) :
            base(state)
        {
            Emitters = new Dictionary<int, Emitter>();
        }
        #endregion

        // Types
        #region Emitter
        public sealed class Emitter :
            ITickable
        {
            #region Constructors
            public Emitter(int maximum)
            {
                Particles = new Particle[maximum];
                Count = 0;
            }

            public Emitter() :
                this(256)
            {
            }
            #endregion

            // Fields
            public Particle[] Particles;
            public int Count;

            public Sprite Sprite;
            public Transform Transform;
            public Color Color;

            public float MinReleaseVelocity;
            public float MaxReleaseVelocity;

            public float MinReleaseAngle;
            public float MaxReleaseAngle;

            public ushort MinReleaseDuration;
            public ushort MaxReleaseDuration;

            // Methods
            #region Tick
            public void Tick(float time)
            {
                for (int i = 0; i < Count; )
                {
                    if (Particles[i].Time > 0)
                    {
                        Particles[i].Position += Particles[i].Velocity;
                        Particles[i].Time--;
                    }

                    if (Particles[i].Time == 0)
                    {
                        if (i < --Count)
                            Particles[i] = Particles[Count];
                    }
                    else
                        i++;
                }
            }
            #endregion
            #region Clear
            public void Clear()
            {
                Count = 0;
                for (int i = 0; i < Particles.Length; i++)
                    Particles[i] = new Particle();
            }
            #endregion

            #region CreateParticle
            public void CreateParticle(Random random, int count)
            {
                for (int i = 0; i < count; i++)
                {
                    if (Count >= Particles.Length - 1)
                        return;

                    float velocity = MinReleaseVelocity + random.NextFloat() * (MaxReleaseVelocity - MinReleaseVelocity);
                    float angle = MinReleaseAngle + random.NextFloat() * (MaxReleaseAngle - MinReleaseAngle);

                    Particle p = new Particle(0, 0, (ushort)random.Next(MinReleaseDuration, MaxReleaseDuration));
                    p.Velocity.X = (float)Math.Cos(angle) * velocity;
                    p.Velocity.Y = (float)Math.Sin(angle) * velocity;

                    Particles[Count++] = p;
                }
            }
            #endregion
        }
        #endregion
        #region Particle
        public struct Particle
        {
            #region Constructors
            public Particle(Vector2 position, ushort duration)
            {
                Type = 0;
                Flags = 0;

                Position = new Vector4(position.X, position.Y, 0, 0);
                Velocity = Vector4.Zero;

                Time = duration;
                Duration = duration;
            }

            public Particle(float x, float y, ushort duration)
            {
                Type = 0;
                Flags = 0;

                Position = new Vector4(x, y, 0, 0);
                Velocity = Vector4.Zero;

                Time = duration;
                Duration = duration;
            }
            #endregion

            // Fields
            public Vector4 Position; // W and Z are user-defined, eg rotation or scale
            public Vector4 Velocity;

            public ushort Type; // Additional state flags for rendering, as per user
            public ushort Flags;

            public ushort Time;
            public ushort Duration;

            // Methods
            #region GetPercent
            public float GetPercent()
            {
                if (Time <= 0 || Duration <= 0)
                    return 0.00f;
                else if (Time >= Duration)
                    return 1.00f;
                return Time / (float)Duration;
            }
            #endregion
        }
        #endregion

        // Fields
        public Dictionary<int, Emitter> Emitters;

        // Methods
        #region Tick
        public sealed override void Tick(float time)
        {
            foreach (var ITEM in Emitters.Values)
                ITEM.Tick(time);
        }
        #endregion
    }
}