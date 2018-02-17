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
    public static class Global // This is fine for a jam, but really bad otherwise
    {
        #region Constructors
        static Global()
        {
            Seed = (ushort)System.DateTime.Now.Ticks;
            Random = new Random(Seed);

            Palettes = new Manager<Color[]>();            
            Textures = new Manager<Texture2D>();
            Frames = new Manager<Rectangle[]>();

            Sounds = new Manager<SoundEffect>();
            Entities = new Manager<EntityType>();
        }
        #endregion

        // Types
        #region Manager
        public sealed class Manager<T>
        {
            #region Constructors
            public Manager()
            {
                ByID = new SortedList<int, T>();
                ByName = new SortedList<string, T>();

                ToID = new Dictionary<string, int>();
                ToName = new Dictionary<int, string>();
            }
            #endregion
            #region Indexers
            public T this[int id]
            {
                get
                {
                    return (ByID.ContainsKey(id))
                        ? ByID[id]
                        : default(T);
                }
            }

            public T this[string name]
            {
                get
                {
                    return (ByName.ContainsKey(name))
                        ? ByName[name]
                        : default(T);
                }
            }
            #endregion

            // Fields
            public readonly IDictionary<int, T> ByID;
            public readonly IDictionary<string, T> ByName;

            public readonly IDictionary<string, int> ToID;
            public readonly IDictionary<int, string> ToName;

            // Methods
            #region Define
            public void Define(int id, string name, T instance)
            {
                if (String.IsNullOrWhiteSpace(name)) throw new ArgumentNullException("name");

                ByID.Add(id, instance);
                ByName.Add(name, instance);

                ToID.Add(name, id);
                ToName.Add(id, name);
            }
            #endregion
        }
        #endregion
        #region Resource
        public sealed class Resource
        {
            #region Constructors
            public Resource(int id, string name, string path)
            {
                ID = id;
                Name = name;
                Path = path;
            }
            #endregion

            // Fields
            public readonly int ID;
            public readonly string Name;
            public readonly string Path;
        }
        #endregion

        // Static fields
        public static ushort Seed;
        public static Random Random;

        public static Manager<Color[]> Palettes;
        public static Manager<Texture2D> Textures;
        public static Manager<Rectangle[]> Frames;

        public static Manager<SoundEffect> Sounds;
        public static Manager<EntityType> Entities;

        // Static methods
        #region ParseFile
        public static IList<string> ParseFile(string path)
        {
            // This data will still need to be delimited by TAB, but it will be sanitized.
            // The data may not be valid, though. And that's up to the classes that consume it.

            List<string> data = new List<string>();
            foreach (var ITEM in System.IO.File.ReadAllLines(path))
            {
                if (String.IsNullOrWhiteSpace(ITEM))
                    continue;

                string value = ITEM.Trim();
                if (value.StartsWith("#") ||
                    value.StartsWith("["))
                    continue;

                data.Add(value);
            }
            return data;
        }
        #endregion

        #region LoadPalettes
        public static void LoadPalettes(Microsoft.Xna.Framework.Game game)
        {
            foreach (var FILE in System.IO.Directory.GetFiles("res/", "*.pal", System.IO.SearchOption.AllDirectories))
            {
                byte[] bytes = System.IO.File.ReadAllBytes(FILE);
                Color[] palette = new Color[bytes.Length / 4];

                for (int i = 0; i < palette.Length; i++)
                    palette[i] = new Color(
                        bytes[i * 4 + 0],  // R
                        bytes[i * 4 + 1],  // G
                        bytes[i * 4 + 2],  // B
                        bytes[i * 4 + 3]); // A

                Palettes.Define(
                    Palettes.ByID.Count,
                    System.IO.Path.GetFileNameWithoutExtension(FILE),
                    palette);
            }
        }
        #endregion
        #region LoadTextures
        public static void LoadTextures(Microsoft.Xna.Framework.Game game)
        {
            foreach (var FILE in System.IO.Directory.GetFiles("res/", "*.png", System.IO.SearchOption.AllDirectories))
            {
                using (var STREAM = System.IO.File.OpenRead(FILE))
                {
                    Textures.Define(
                        Textures.ByID.Count,
                        System.IO.Path.GetFileNameWithoutExtension(FILE),
                        Texture2D.FromStream(game.GraphicsDevice, STREAM));
                }
            }
        }
        #endregion
        #region LoadFrames
        public static void LoadFrames(Microsoft.Xna.Framework.Game game)
        {
            foreach (var FILE in System.IO.Directory.GetFiles("res/", "*.fdat", System.IO.SearchOption.AllDirectories))
            {
                foreach (var ITEM in ParseFile(FILE))
                {
                    string[] data = ITEM.Split('\t');

                    int id = Int32.Parse(data[0]);
                    string name = data[1];
                    int count = Int32.Parse(data[2]);
                    int stride = Int32.Parse(data[3]);
                    int x = Int32.Parse(data[4]);
                    int y = Int32.Parse(data[5]);
                    int width = Int32.Parse(data[6]);
                    int height = Int32.Parse(data[7]);
                    int stepx = Int32.Parse(data[8]);
                    int stepy = Int32.Parse(data[9]);

                    Frames.Define(id, name, Sprite.CreateFrames(
                        count, stride, x, y, width, height));
                }
            }
        }
        #endregion

        public static void LoadEntityTypes(Microsoft.Xna.Framework.Game game)
        {
            List<EntityType> e = new List<EntityType>(); // container for object def
            var tx = Global.Textures[0];  // shortcut for texture
            var txc = Global.Frames;      // shortcut for frames / texture coordinates
            var pal = Global.Palettes[0]; // shortcut for palettes
            /*
            e = new EntityType(0, "empty");
            {
                e.Sprites = new Sprite[0];
                e.Color1 = Color.Transparent;
                e.Color2 = Color.Transparent;
                e.Color3 = Color.Transparent;

                Global.Entities.Define(e.ID, e.Name, e);
            }

            e = new EntityType(1, "Slime");
            {
                e.Sprites = new Sprite[5];

                e.Sprites[0] = new Sprite(tx, txc[1]);
                e.Sprites[1] = new Sprite(tx, txc[2]);
                e.Sprites[2] = new Sprite(tx, txc[3]);
                e.Sprites[3] = new Sprite(tx, txc[4]);
                e.Sprites[4] = new Sprite(tx, txc[148]);

                e.Color1 = Color.White;
                e.Color2 = Color.White;
                e.Color3 = Color.White;

                Global.Entities.Define(e.ID, e.Name, e);
            }
            */
            e.Add(EntityType.Define(2, "Bat1", 5, 37, 38, 39));
            /*e.Add(EntityType.Define(2, "Bat1", 5, 37, 38, 39));
            e.Add(EntityType.Define(2, "Bat2", 5, 37, 38, 39));
            e.Add(EntityType.Define(2, "Bat1", 5, 37, 38, 39));
            e.Add(EntityType.Define(2, "Bat1", 5, 37, 38, 39));
            e.Add(EntityType.Define(2, "Bat1", 5, 37, 38, 39));
            e.Add(EntityType.Define(2, "Bat1", 5, 37, 38, 39));
            e.Add(EntityType.Define(2, "Bat1", 5, 37, 38, 39));
            e.Add(EntityType.Define(2, "Bat1", 5, 37, 38, 39));
            */
            foreach (var ITEM in e)
                Global.Entities.Define(ITEM.ID, ITEM.Name, ITEM);
        }
    }
}