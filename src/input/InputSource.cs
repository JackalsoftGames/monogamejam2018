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

namespace Presto.Input
{
    public abstract class InputSource<TState, TButton> :
        IInputQueryable<TButton>
    {
        #region Constructors
        public InputSource(TState current, TState previous)
        {
            Current = current;
            Previous = previous;
        }

        public InputSource(TState value) :
            this(value, value)
        {
        }

        public InputSource() :
            this(default(TState), default(TState))
        {
        }
        #endregion

        // Fields
        public TState Current;
        public TState Previous;

        // Methods
        #region Next
        public virtual void Next()
        {
        }
        #endregion

        public abstract bool IsUp(TButton value);
        public abstract bool IsDown(TButton value);

        public abstract bool IsPressed(TButton value);
        public abstract bool IsReleased(TButton value);
        public abstract bool IsHeld(TButton value);
    }
}