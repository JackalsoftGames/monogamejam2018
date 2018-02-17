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

namespace Presto.Extensions
{
    public static class CopymanExtensions
    {
        #region Direction.Next
        public static Direction Next(this Direction obj)
        {
            switch (obj)
            {
                // Forward
                case (Direction.FORWARD | Direction.LEFT): return Direction.LEFT;
                case (Direction.FORWARD | Direction.RIGHT): return Direction.RIGHT;
                case (Direction.FORWARD | Direction.UP): return Direction.UP;
                case (Direction.FORWARD | Direction.DOWN): return Direction.DOWN;

                // Backward
                case (Direction.BACKWARD | Direction.LEFT): return Direction.RIGHT;
                case (Direction.BACKWARD | Direction.RIGHT): return Direction.LEFT;
                case (Direction.BACKWARD | Direction.UP): return Direction.DOWN;
                case (Direction.BACKWARD | Direction.DOWN): return Direction.UP;

                // Clockwise
                case (Direction.CLOCKWISE | Direction.LEFT): return Direction.UP;
                case (Direction.CLOCKWISE | Direction.RIGHT): return Direction.DOWN;
                case (Direction.CLOCKWISE | Direction.UP): return Direction.RIGHT;
                case (Direction.CLOCKWISE | Direction.DOWN): return Direction.LEFT;

                // Counterclockwise
                case (Direction.COUNTERCLOCKWISE | Direction.LEFT): return Direction.DOWN;
                case (Direction.COUNTERCLOCKWISE | Direction.RIGHT): return Direction.UP;
                case (Direction.COUNTERCLOCKWISE | Direction.UP): return Direction.LEFT;
                case (Direction.COUNTERCLOCKWISE | Direction.DOWN): return Direction.RIGHT;

                // Default
                default:
                    return obj;
            }
        }

        public static Direction Next(this Direction obj, Direction direction)
        {
            switch (obj | direction)
            {
                // Forward
                case (Direction.FORWARD | Direction.LEFT): return Direction.LEFT;
                case (Direction.FORWARD | Direction.RIGHT): return Direction.RIGHT;
                case (Direction.FORWARD | Direction.UP): return Direction.UP;
                case (Direction.FORWARD | Direction.DOWN): return Direction.DOWN;

                // Backward
                case (Direction.BACKWARD | Direction.LEFT): return Direction.RIGHT;
                case (Direction.BACKWARD | Direction.RIGHT): return Direction.LEFT;
                case (Direction.BACKWARD | Direction.UP): return Direction.DOWN;
                case (Direction.BACKWARD | Direction.DOWN): return Direction.UP;

                // Clockwise
                case (Direction.CLOCKWISE | Direction.LEFT): return Direction.UP;
                case (Direction.CLOCKWISE | Direction.RIGHT): return Direction.DOWN;
                case (Direction.CLOCKWISE | Direction.UP): return Direction.RIGHT;
                case (Direction.CLOCKWISE | Direction.DOWN): return Direction.LEFT;

                // Counterclockwise
                case (Direction.COUNTERCLOCKWISE | Direction.LEFT): return Direction.DOWN;
                case (Direction.COUNTERCLOCKWISE | Direction.RIGHT): return Direction.UP;
                case (Direction.COUNTERCLOCKWISE | Direction.UP): return Direction.LEFT;
                case (Direction.COUNTERCLOCKWISE | Direction.DOWN): return Direction.RIGHT;

                // Default
                default:
                    return obj;
            }
        }
        #endregion
    }
}