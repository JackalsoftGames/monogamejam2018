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
    [Flags]
    public enum Direction :
        byte
    {
        NONE = 0,

        LEFT = 0x01,
        RIGHT = 0x02,
        UP = 0x04,
        DOWN = 0x08,

        FORWARD = 0x10,
        BACKWARD = 0x20,
        CLOCKWISE = 0x40,
        COUNTERCLOCKWISE = 0x80,
    }
}