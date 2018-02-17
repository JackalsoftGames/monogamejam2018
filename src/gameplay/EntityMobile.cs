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

using Presto;
using Presto.Extensions;
using Presto.Input;
#endregion

namespace Presto
{
    public sealed class EntityMobile :
        ITickable
    {
        #region Constructors
        public EntityMobile(byte type, byte x, byte y)
        {
            Type = type;

            Position = new Delta3<Position>(new Position());
            Direction = new Delta3<Direction>(0);

            Flags = BehaviorFlags.NONE;
            Timer = new TickTimer();
        }

        public EntityMobile(EntityStatic tile, byte x, byte y) :
            this(tile.Type, x, y)
        {
            Direction = new Delta3<Presto.Direction>(tile.Direction);
            Flags = tile.Flags;
        }

        public EntityMobile(EntityMobile other)
        {
            this.Type = other.Type;

            this.Position = other.Position;
            this.Direction = other.Direction;

            this.Flags = other.Flags;
            this.Timer = other.Timer;
        }
        #endregion

        // Fields
        public byte Type;

        public Delta3<Position> Position;
        public Delta3<Direction> Direction;

        public BehaviorFlags Flags;
        public TickTimer Timer;

        // Properties
        public bool IsMoving { get { return Timer.Duration > 0; } }

        // Methods
        #region Tick
        public void Tick(float time)
        {
            Timer.Tick(time);
        }
        #endregion
    }
}