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
    [System.Diagnostics.DebuggerDisplay(@"\{Value:{Value} Other:{Other}\}")]
    public struct Delta2<TValue>
    {
        #region Constructors
        public Delta2(TValue value, TValue other)
        {
            Value = value;
            Other = other;
        }

        public Delta2(TValue value)
        {
            Value = value;
            Other = value;
        }
        #endregion

        // Fields
        public TValue Value;
        public TValue Other;
    }
}