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
    public sealed class PlayerStateManager :
        GameState.Component
    {
        #region Constructors
        public PlayerStateManager(GameState state) :
            base(state)
        {
            Keyboard = new KeyboardInputSource();
            Mouse = new MouseInputSource();
            GamePad = new GamePadInputSource();

            PlayerAction = EntityAction.NONE;
            PlayerInventory = EntityInventory.NONE;
        }
        #endregion

        // Constants
        public const float GAMEPAD_THRESHOLD = 0.05f;

        // Fields
        public KeyboardInputSource Keyboard;
        public MouseInputSource Mouse;
        public GamePadInputSource GamePad;

        public EntityAction PlayerAction;
        public EntityInventory PlayerInventory;
        public EntityType PlayerSlime;

        // Properties
        public bool MoveLeft { get { return (PlayerAction & EntityAction.MOVE_LEFT) != 0; } }
        public bool MoveRight { get { return (PlayerAction & EntityAction.MOVE_RIGHT) != 0; } }
        public bool MoveUp { get { return (PlayerAction & EntityAction.MOVE_UP) != 0; } }
        public bool MoveDown { get { return (PlayerAction & EntityAction.MOVE_DOWN) != 0; } }

        public bool Action1 { get { return (PlayerAction & EntityAction.ACTION_1) != 0; } }
        public bool Action2 { get { return (PlayerAction & EntityAction.ACTION_2) != 0; } }

        // Methods
        #region Tick
        public sealed override void Tick(float time)
        {
            #region Input
            {
                Keyboard.Next();
                Mouse.Next();
                GamePad.Next();

                PlayerAction = EntityAction.NONE;

                // Left
                if (Keyboard.IsDown(Keys.Left) ||
                    Keyboard.IsDown(Keys.A) ||
                    GamePad.IsDown(Buttons.DPadLeft) ||
                    GamePad.Current.ThumbSticks.Left.X <= -GAMEPAD_THRESHOLD)
                {
                    PlayerAction |= EntityAction.MOVE_LEFT;
                }

                // Right
                if (Keyboard.IsDown(Keys.Right) ||
                    Keyboard.IsDown(Keys.D) ||
                    GamePad.IsDown(Buttons.DPadRight) ||
                    GamePad.Current.ThumbSticks.Left.X >= +GAMEPAD_THRESHOLD)
                {
                    PlayerAction |= EntityAction.MOVE_RIGHT;
                }

                // Up
                if (Keyboard.IsDown(Keys.Up) ||
                    Keyboard.IsDown(Keys.W) ||
                    GamePad.IsDown(Buttons.DPadUp) ||
                    GamePad.Current.ThumbSticks.Left.Y >= +GAMEPAD_THRESHOLD)
                {
                    PlayerAction |= EntityAction.MOVE_UP;
                }

                // Down
                if (Keyboard.IsDown(Keys.Down) ||
                    Keyboard.IsDown(Keys.S) ||
                    GamePad.IsDown(Buttons.DPadDown) ||
                    GamePad.Current.ThumbSticks.Left.Y <= -GAMEPAD_THRESHOLD)
                {
                    PlayerAction |= EntityAction.MOVE_DOWN;
                }
            }
            #endregion
        }
        #endregion

        #region AddItem
        public void AddItem(EntityInventory value)
        {
            PlayerInventory |= value;
        }
        #endregion
        #region RemoveItem
        public void RemoveItem(EntityInventory value)
        {
            PlayerInventory &= ~value;
        }
 
        public void RemoveItem()
        {
            PlayerInventory = 0;
        }
        #endregion
        #region HasItem
        public bool HasItem(EntityInventory value)
        {
            return (PlayerInventory & value) != 0;
        }
        #endregion
    }
}