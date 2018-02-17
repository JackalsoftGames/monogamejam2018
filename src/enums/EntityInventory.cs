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
    public enum EntityInventory :
        byte
    {
        NONE = 0,

        KEY_RED = 0x01,
        KEY_YELLOW = 0x02,
        KEY_GREEN = 0x04,
        KEY_BLUE = 0x08,
    }
}