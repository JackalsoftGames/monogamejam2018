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
using Microsoft.Xna.Framework.Media;

using Presto;
using Presto.Extensions;
using Presto.Input;
#endregion
#region ASSEMBLY
[assembly: System.Reflection.AssemblyTitle("Presto!")]
[assembly: System.Reflection.AssemblyDescription("An entry for #MonoGameJam 2018")]
[assembly: System.Reflection.AssemblyConfiguration("")]
[assembly: System.Reflection.AssemblyCompany("Jackalsoft Games")]
[assembly: System.Reflection.AssemblyProduct("Presto!")]
[assembly: System.Reflection.AssemblyCopyright("Copyright © Jackalsoft Games 2018")]
[assembly: System.Reflection.AssemblyTrademark("")]
[assembly: System.Reflection.AssemblyCulture("")]

[assembly: System.Reflection.AssemblyVersion("1.0.0.0")]
[assembly: System.Reflection.AssemblyFileVersion("1.0.0.0")]

[assembly: System.Runtime.InteropServices.ComVisible(false)]
[assembly: System.Runtime.InteropServices.Guid("e7277788-1e39-4cdc-9616-c74182191aed")]
#endregion

namespace Presto
{
    public sealed class Game :
        Microsoft.Xna.Framework.Game
    {
        #region ENTRY POINT
        public static void Main(string[] args)
        {
            using (var ITEM = new Game())
            {
                // This stuff should probably in the Game constructor, but whatever
                // We also load stuff in the Initialize method, instead of Load
                new GraphicsDeviceManager(ITEM)
                {
                    // 800x600 (256x192):
                    //   Tile  = 16x16
                    //   Map   = 16x12
                    //   Scale = 3x3

                    PreferredBackBufferWidth = 800,
                    PreferredBackBufferHeight = 600,
                };

                ITEM.IsMouseVisible = true;
                ITEM.Window.Title = "Presto!";

                ITEM.Run();
            }
        }
        #endregion

        // Static fields
        public static SpriteBatch Batch;

        // Fields
        GameState state;
        RenderTarget2D playArea;
        public static Texture2D tx; // debug

        // Methods
        #region Initialize
        protected override void Initialize()
        {
            Batch = new SpriteBatch(GraphicsDevice);
            tx = new Texture2D(GraphicsDevice, 1, 1);
            tx.SetData(new Color[] { Color.White });

            Global.LoadPalettes(this);
            Global.LoadTextures(this);
            Global.LoadFrames(this);
            Global.LoadEntityTypes(this);

            state = new GameState();
            playArea = new RenderTarget2D(GraphicsDevice, 256, 192);

            state.Entity.Slime = new EntityMobile(0, 5, 5); // This should not be init'd here
            state.Entity.Actors.Add(0, state.Entity.Slime);
            state.Player.PlayerSlime = Global.Entities[1];
        }
        #endregion
        #region Update
        protected override void Update(GameTime time)
        {
            state.Tick(1);

            if (state.Player.MoveLeft) state.Entity.MoveLeft(state.Entity.Slime, 30);
            if (state.Player.MoveRight) state.Entity.MoveRight(state.Entity.Slime, 30);
            if (state.Player.MoveUp) state.Entity.MoveUp(state.Entity.Slime, 30);
            if (state.Player.MoveDown) state.Entity.MoveDown(state.Entity.Slime, 30);
        }
        #endregion
        #region Draw
        protected override void Draw(GameTime time)
        {
            #region Play area
            {
                GraphicsDevice.SetRenderTarget(playArea);
                GraphicsDevice.Clear(new Color(16, 16, 16));
                Game.Batch.Begin();
                {
                    state.Entity.Draw(Game.Batch);
                }
                Game.Batch.End();
            }
            #endregion
            #region UI
            {
                GraphicsDevice.SetRenderTarget(null);
                GraphicsDevice.Clear(Color.Black);
                Game.Batch.Begin(0, null, SamplerState.PointClamp, null, null);
                {
                    Vector2 offset = Vector2.Zero;
                    offset.X = (Window.ClientBounds.Width - playArea.Width * 3) / 2;
                    offset.Y = 4;
                    //offset.Y = (Window.ClientBounds.Height - playArea.Height * 3) / 2;

                    Game.Batch.Draw(playArea, offset, null, Color.White,
                        0.00f, Vector2.Zero, new Vector2(3, 3),
                        SpriteEffects.None, 0.00f);
                }
                Game.Batch.End();
            }
            #endregion
        }
        #endregion
    }
}