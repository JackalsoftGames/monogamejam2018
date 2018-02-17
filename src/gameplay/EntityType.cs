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
    public sealed class EntityType
    {
        #region Constructors
        public EntityType(byte id, string name)
        {
            ID = id;
            Name = name;
        }
        #endregion

        // Fields
        public byte ID;
        public string Name;

        // public Behavior Behavior;
        public BehaviorFlags Flags;

        public Sprite[] Sprites;
        public Color Color1;
        public Color Color2;
        public Color Color3;

        // Static methods
        public static EntityType Define(byte id, string name, int image, int c1, int c2, int c3)
        {
            EntityType result = new EntityType(id, name);
            result.SetImage(image, c1, c2, c3);
            return result;
        }

        // Methods
        #region SetImage
        public void SetImage(Sprite[] sprites, Color color1, Color color2, Color color3)
        {
            Sprites = sprites;
            Color1 = color1;
            Color2 = color2;
            Color3 = color3;
        }

        public void SetImage(int index, int c1, int c2, int c3)
        {
            Sprites = new Sprite[1] { new Sprite(Global.Textures[0], Global.Frames[index]) };
            Color1 = Global.Palettes[0][c1];
            Color2 = Global.Palettes[0][c2];
            Color3 = Global.Palettes[0][c3];
        }
        #endregion
    }
}