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
    public sealed class GamePadInputSource :
        InputSource<GamePadState, Buttons>,
        IInputQueryable<Buttons>
    {
        #region Constructors
        public GamePadInputSource(GamePadState current, GamePadState previous, PlayerIndex player, GamePadDeadZone deadZone) :
            base(current, previous)
        {
            Player = player;
            DeadZone = deadZone;
        }

        public GamePadInputSource(GamePadState current, GamePadState previous, PlayerIndex player) :
            this(current, previous, player, GamePadDeadZone.IndependentAxes)
        {
        }

        public GamePadInputSource(GamePadState current, GamePadState previous) :
            this(current, previous, PlayerIndex.One, GamePadDeadZone.IndependentAxes)
        {
        }

        public GamePadInputSource(PlayerIndex player, GamePadDeadZone deadZone) :
            this(default(GamePadState), default(GamePadState), player, deadZone)
        {
        }

        public GamePadInputSource(PlayerIndex player) :
            this(default(GamePadState), default(GamePadState), player, GamePadDeadZone.IndependentAxes)
        {
        }

        public GamePadInputSource() :
            this(default(GamePadState), default(GamePadState), PlayerIndex.One, GamePadDeadZone.IndependentAxes)
        {
        }
        #endregion

        // Fields
        public PlayerIndex Player;
        public GamePadDeadZone DeadZone;

        // Methods
        #region Next
        public sealed override void Next()
        {
            Previous = Current;
            Current = GamePad.GetState(Player, DeadZone);
        }
        #endregion

        #region IsUp
        public sealed override bool IsUp(Buttons value)
        {
            return (Current.IsButtonUp(value));
        }
        #endregion
        #region IsDown
        public sealed override bool IsDown(Buttons value)
        {
            return (Current.IsButtonDown(value));
        }
        #endregion

        #region IsPressed
        public sealed override bool IsPressed(Buttons value)
        {
            return (Current.IsButtonDown(value) && Previous.IsButtonUp(value));
        }
        #endregion
        #region IsReleased
        public sealed override bool IsReleased(Buttons value)
        {
            return (Current.IsButtonUp(value) && Previous.IsButtonDown(value));
        }
        #endregion
        #region IsHeld
        public sealed override bool IsHeld(Buttons value)
        {
            return (Current.IsButtonDown(value) && Previous.IsButtonDown(value));
        }
        #endregion
    }
}