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
    public sealed class KeyboardInputSource :
        InputSource<KeyboardState, Keys>,
        IInputQueryable<Keys>
    {
        #region Constructors
        public KeyboardInputSource(KeyboardState current, KeyboardState previous) :
            base(current, previous)
        {
        }

        public KeyboardInputSource() :
            this(default(KeyboardState), default(KeyboardState))
        {
        }
        #endregion

        // Methods
        #region Next
        public sealed override void Next()
        {
            Previous = Current;
            Current = Keyboard.GetState();
        }
        #endregion

        #region IsUp
        public sealed override bool IsUp(Keys value)
        {
            return (!Current.IsKeyUp(value));
        }
        #endregion
        #region IsDown
        public sealed override bool IsDown(Keys value)
        {
            return (Current.IsKeyDown(value));
        }
        #endregion

        #region IsPressed
        public sealed override bool IsPressed(Keys value)
        {
            return (Current.IsKeyDown(value) && Previous.IsKeyUp(value));
        }
        #endregion
        #region IsReleased
        public sealed override bool IsReleased(Keys value)
        {
            return (Current.IsKeyUp(value) && Previous.IsKeyDown(value));
        }
        #endregion
        #region IsHeld
        public sealed override bool IsHeld(Keys value)
        {
            return (Current.IsKeyDown(value) && Previous.IsKeyDown(value));
        }
        #endregion
    }
}