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
    public sealed class MouseInputSource :
        InputSource<MouseState, MouseButtons>,
        IInputQueryable<MouseButtons>
    {
        #region Constructors
        public MouseInputSource(MouseState current, MouseState previous) :
            base(current, previous)
        {
        }

        public MouseInputSource() :
            this(default(MouseState), default(MouseState))
        {
        }
        #endregion

        // Methods
        #region Next
        public sealed override void Next()
        {
            Previous = Current;
            Current = Mouse.GetState();
        }
        #endregion

        #region IsUp
        public sealed override bool IsUp(MouseButtons value)
        {
            switch (value)
            {
                case (MouseButtons.LEFT_BUTTON): return (Current.LeftButton == ButtonState.Released);
                case (MouseButtons.MIDDLE_BUTTON): return (Current.MiddleButton == ButtonState.Released);
                case (MouseButtons.RIGHT_BUTTON): return (Current.RightButton == ButtonState.Released);
            }
            return false;
        }
        #endregion
        #region IsDown
        public sealed override bool IsDown(MouseButtons value)
        {
            switch (value)
            {
                case (MouseButtons.LEFT_BUTTON): return (Current.LeftButton == ButtonState.Pressed);
                case (MouseButtons.MIDDLE_BUTTON): return (Current.MiddleButton == ButtonState.Pressed);
                case (MouseButtons.RIGHT_BUTTON): return (Current.RightButton == ButtonState.Pressed);
            }
            return false;
        }
        #endregion

        #region IsPressed
        public sealed override bool IsPressed(MouseButtons value)
        {
            switch (value)
            {
                case (MouseButtons.LEFT_BUTTON): return (Current.LeftButton == ButtonState.Pressed && Previous.LeftButton == ButtonState.Released);
                case (MouseButtons.MIDDLE_BUTTON): return (Current.MiddleButton == ButtonState.Pressed && Previous.MiddleButton == ButtonState.Released);
                case (MouseButtons.RIGHT_BUTTON): return (Current.RightButton == ButtonState.Pressed && Previous.RightButton == ButtonState.Released);
            }
            return false;
        }
        #endregion
        #region IsReleased
        public sealed override bool IsReleased(MouseButtons value)
        {
            switch (value)
            {
                case (MouseButtons.LEFT_BUTTON): return (Current.LeftButton == ButtonState.Released && Previous.LeftButton == ButtonState.Pressed);
                case (MouseButtons.MIDDLE_BUTTON): return (Current.MiddleButton == ButtonState.Released && Previous.MiddleButton == ButtonState.Pressed);
                case (MouseButtons.RIGHT_BUTTON): return (Current.RightButton == ButtonState.Released && Previous.RightButton == ButtonState.Pressed);
            }
            return false;
        }
        #endregion
        #region IsHeld
        public sealed override bool IsHeld(MouseButtons value)
        {
            switch (value)
            {
                case (MouseButtons.LEFT_BUTTON): return (Current.LeftButton == ButtonState.Pressed && Previous.LeftButton == ButtonState.Pressed);
                case (MouseButtons.MIDDLE_BUTTON): return (Current.MiddleButton == ButtonState.Pressed && Previous.MiddleButton == ButtonState.Pressed);
                case (MouseButtons.RIGHT_BUTTON): return (Current.RightButton == ButtonState.Pressed && Previous.RightButton == ButtonState.Pressed);
            }
            return false;
        }
        #endregion
    }
}