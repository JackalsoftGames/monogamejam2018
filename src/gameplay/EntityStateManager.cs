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
    public sealed class EntityStateManager :
        GameState.Component
    {
        // 16x12 = 4:3, 192 tiles
        #region Constructors
        public EntityStateManager(GameState state) :
            base(state)
        {
            Actors = new SortedList<int, EntityMobile>();
            Tiles = new EntityStatic[16 * 12];
            Occupied = new bool[16 * 12];

            Bounds = new Rectangle(0, 0, 16, 12);
        }
        #endregion

        // Fields
        public EntityMobile Slime;

        public SortedList<int, EntityMobile> Actors;
        public EntityStatic[] Tiles;
        public bool[] Occupied;

        public Rectangle Bounds;

        // Methods
        #region Tick
        public sealed override void Tick(float time)
        {
            foreach (var ITEM in Actors.Values)
            {
                ITEM.Tick(time);

                if (ITEM.Timer.IsElapsed)
                {
                    ITEM.Position.Previous = ITEM.Position.Current;
                    ITEM.Position.Current = ITEM.Position.Next;
                }
            }
        }
        #endregion
        #region Draw
        public void Draw(SpriteBatch batch)
        {
            float p = State.GlobalAnim_30.GetPercent();

            // Tiles
            for (int y = Bounds.Top; y < Bounds.Bottom; y++)
                for (int x = Bounds.Left; x < Bounds.Right; x++)
                {
                    byte index = (byte)(y * Bounds.Width + x);
                    foreach (var SPRITE in GetSpritesFromEntityType(Tiles[index].Type))
                        batch.DrawSprite(SPRITE, new Vector2(x, y) * 16, p);

                    if (IsOccupied(index))
                        batch.Draw(Game.tx, new Rectangle(x * 16, y * 16, 2, 2), Color.Red);
                    else
                        batch.Draw(Game.tx, new Rectangle(x * 16, y * 16, 2, 2), Color.Green);
                }

            // Objects
            foreach (var ACTOR in Actors.Values)
            {
                foreach (var SPRITE in GetSpritesFromEntityType(ACTOR.Type))
                    batch.DrawSprite(SPRITE, ACTOR.Position.Current.ToVector2() * 16, p);
            }

            // Player
            // This works by giving the actual player object a sprite index of 0 (no draw)
            // Then, we store a ref to the player object instance and use its state
            // and special case it, below. Wow this is not scaleable.
            {
                Sprite[] sprites = GetSpritesFromEntityType(1);
                Vector2 position = Slime.Position.Current.ToVector2();

                if (sprites == null || sprites.Length < 5)
                    return; // hacky

                // Because we're overwriting the sprite values here, this makes affects all
                // slime instances, so we can't ever have more than one player object or it will break
                sprites[0].Color = State.Player.PlayerSlime.Color1 * 0.80f;
                sprites[1].Color = Color.Black * 0.35f;
                sprites[2].Color = State.Player.PlayerSlime.Color2 * 0.80f;
                sprites[3].Color = Color.Lerp(State.Player.PlayerSlime.Color3, Color.Black, 0.50f) * 0.40f;
                sprites[4].Color = Color.White;

                foreach (var SPRITE in sprites)
                    Game.Batch.DrawSprite(SPRITE, position, p);
            }
        }
        #endregion

        #region GetSprites
        public Sprite[] GetSpritesFromEntityType(byte type)
        {
            EntityType eType = Global.Entities[type];
            if (eType == null)
                return new Sprite[0];
            return eType.Sprites;
        }
        #endregion

        // Methods (tile)
        #region TileExists
        public bool TileExists(byte position)
        {
            return (position < Tiles.Length);
        }
        #endregion

        #region Occupy
        public void Occupy(byte position)
        {
            if (TileExists(position))
                Occupied[position] = true;
        }
        #endregion
        #region Unoccupy
        public void Unoccupy(byte position)
        {
            if (TileExists(position))
                Occupied[position] = false;
        }
        #endregion
        #region IsOccupied
        public bool IsOccupied(byte position)
        {
            if(TileExists(position))
                return Occupied[position];
            return false;
        }
        #endregion

        // Methods (actor)
        #region Move
        public void Move(EntityMobile actor, Position position, Direction direction, ushort duration)
        {
            if (actor.IsMoving)
                return;

            actor.Position.Next = position;
            actor.Direction.Next = direction;
            actor.Timer.Start(duration, false);
        }
        #endregion
        #region MoveCancel
        public void MoveCancel(EntityMobile actor)
        {
            if (!actor.IsMoving)
                return;

            actor.Position.Next = actor.Position.Current;
            actor.Direction.Next = actor.Direction.Current;
            actor.Timer.Reset();
        }
        #endregion
        #region MoveDelay
        public void MoveDelay(EntityMobile actor, ushort duration)
        {
            if (actor.IsMoving)
                return;

            actor.Timer.Start(duration, false);
        }
        #endregion
        #region MoveFacing
        public void MoveFacing(EntityMobile actor, ushort duration, Direction direction)
        {
            if (actor.IsMoving)
                return;

            actor.Position.Next = actor.Position.Current;
            actor.Direction.Next = direction;
            actor.Timer.Start(duration, false);
        }
        #endregion

        #region MoveLeft
        public void MoveLeft(EntityMobile actor, ushort duration)
        {
            Move(actor, actor.Position.Current.Left, Direction.LEFT, duration);
        }
        #endregion
        #region MoveRight
        public void MoveRight(EntityMobile actor, ushort duration)
        {
            Move(actor, actor.Position.Current.Right, Direction.LEFT, duration);
        }
        #endregion
        #region MoveUp
        public void MoveUp(EntityMobile actor, ushort duration)
        {
            Move(actor, actor.Position.Current.Up, Direction.LEFT, duration);
        }
        #endregion
        #region MoveDown
        public void MoveDown(EntityMobile actor, ushort duration)
        {
            Move(actor, actor.Position.Current.Down, Direction.LEFT, duration);
        }
        #endregion
    }
}