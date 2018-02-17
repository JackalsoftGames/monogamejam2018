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
    public enum EntityAction :
        byte
    {
        NONE = 0,

        MOVE_LEFT = 0x01,
        MOVE_RIGHT = 0x02,
        MOVE_UP = 0x04,
        MOVE_DOWN = 0x08,

        ACTION_1 = 0x10,
        ACTION_2 = 0x20,
        ACTION_3 = 0x40,
        ACTION_4 = 0x80,
    }
}