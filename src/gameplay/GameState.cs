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
    public sealed class GameState :
        ITickable
    {
        #region Constructors
        public GameState()
        {
            Player = new PlayerStateManager(this);
            Entity = new EntityStateManager(this);
            Particles = new ParticleStateManager(this);
        }
        #endregion

        // Types
        #region Component
        public abstract class Component :
            ITickable
        {
            #region Constructors
            public Component(GameState state)
            {
                if (state == null)
                    throw new ArgumentNullException("state");
                State = state;
            }
            #endregion

            // Properties
            public GameState State { get; private set; }

            // Methods
            #region Tick
            public virtual void Tick(float time)
            {
            }
            #endregion
        }
        #endregion

        // Fields
        public PlayerStateManager Player;
        public EntityStateManager Entity;
        public ParticleStateManager Particles;

        public TickTimer GlobalAnim_30 = new TickTimer(30, true);
        public TickTimer GlobalAnim_60 = new TickTimer(60, true);
        public TickTimer GlobalAnim_120 = new TickTimer(120, true);

        // Methods
        #region Tick
        public void Tick(float time)
        {
            Player.Tick(time);
            Entity.Tick(time);
            Particles.Tick(time);

            GlobalAnim_30.Tick(time);
            GlobalAnim_60.Tick(time);
            GlobalAnim_120.Tick(time);
        }
        #endregion
    }
}