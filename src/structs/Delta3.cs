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
    [System.Diagnostics.DebuggerDisplay(@"\{Previous:{Previous} Current:{Current} Next:{Next}\}")]
    public struct Delta3<TValue>
    {
        #region Constructors
        public Delta3(TValue previous, TValue current, TValue next)
        {
            Previous = previous;
            Current = current;
            Next = next;
        }

        public Delta3(TValue value)
        {
            Previous = value;
            Current = value;
            Next = value;
        }
        #endregion

        // Fields
        public TValue Previous;
        public TValue Current;
        public TValue Next;
    }
}