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
    [System.Diagnostics.DebuggerDisplay(@"\{X:{X} Y:{Y}\}")]
    public struct Position :
        IComparable<Position>,
        IEquatable<Position>
    {
        #region Constructors
        public Position(byte x, byte y)
        {
            X = x;
            Y = y;
        }

        public Position(float x, float y)
        {
            X = (byte)x;
            Y = (byte)y;
        }

        public Position(int packedValue)
        {
            X = (byte)(packedValue);
            Y = (byte)(packedValue >> 8);
        }
        #endregion

        // Static operators
        #region Operator >
        public static bool operator >(Position obj, Position other)
        {
            return obj.CompareTo(other) > 0;
        }
        #endregion
        #region Operator <
        public static bool operator <(Position obj, Position other)
        {
            return obj.CompareTo(other) < 0;
        }
        #endregion
        #region Operator >=
        public static bool operator >=(Position obj, Position other)
        {
            return obj.CompareTo(other) >= 0;
        }
        #endregion
        #region Operator <=
        public static bool operator <=(Position obj, Position other)
        {
            return obj.CompareTo(other) <= 0;
        }
        #endregion
        #region Operator ==
        public static bool operator ==(Position obj, Position other)
        {
            return
                (obj.X == other.X) &&
                (obj.Y == other.Y);
        }
        #endregion
        #region Operator !=
        public static bool operator !=(Position obj, Position other)
        {
            return !(obj == other);
        }
        #endregion

        // Fields
        public byte X;
        public byte Y;

        // Static Properties
        public static Position Zero { get { return new Position(); } }

        // Properties
        public int PackedValue { get { return (Y << 8 + X); } }

        public Position Left { get { return new Position((byte)(X - 1), Y); } }
        public Position Right { get { return new Position((byte)(X + 1), Y); } }
        public Position Up { get { return new Position(X, (byte)(Y - 1)); } }
        public Position Down { get { return new Position(X, (byte)(Y + 1)); } }

        // Methods
        #region Next
        public Position Next(Direction direction)
        {
            switch (direction.Next())
            {
                case (Direction.LEFT): return Left;
                case (Direction.RIGHT): return Right;
                case (Direction.UP): return Up;
                case (Direction.DOWN): return Down;
                default: return this;
            }
        }
        #endregion

        #region Equals
        public bool Equals(Position other)
        {
            return (this == other);
        }

        public override bool Equals(object obj)
        {
            return (obj is Position) &&
                (this == (Position)obj);
        }
        #endregion
        #region GetHashCode
        public override int GetHashCode()
        {
            return (Y << 8 + X);
        }
        #endregion

        #region CompareTo
        public int CompareTo(Position other)
        {
            if
                (this.Y > other.Y) return 1;
            else if
                (this.Y < other.Y) return -1;
            else
            {
                if
                    (this.X > other.X) return 1;
                else if
                    (this.X < other.X) return -1;
                else
                    return 0;
            }
        }
        #endregion

        #region ToString
        public override string ToString()
        {
            return String.Format("X:{0} Y:{1}", X, Y);
        }
        #endregion
        #region ToVector2
        public Vector2 ToVector2()
        {
            return new Vector2(X, Y);
        }
        #endregion
    }
}