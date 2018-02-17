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
    public struct EntityStatic
    {
        #region Constructors
        public EntityStatic(byte type, Direction direction, BehaviorFlags flags)
        {
            Type = type;

            Direction = direction;
            Flags = flags;
        }

        public EntityStatic(byte type)
        {
            Type = type;

            Direction = Direction.NONE;
            Flags = BehaviorFlags.NONE;
        }

        public EntityStatic(EntityMobile actor)
        {
            Type = actor.Type;

            Direction = actor.Direction.Current;
            Flags = actor.Flags;
        }
        #endregion

        // Fields
        public byte Type;

        public Direction Direction;
        public BehaviorFlags Flags;
    }
}