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
    public sealed class Sprite
    {
        #region Constructors
        public Sprite(Texture2D texture, Rectangle[] frames, Transform transform,
            Color color, SpriteEffects flip, float depth)
        {
            Texture = texture;
            Frames = frames;

            Transform = transform;

            Color = color;
            Flip = flip;
            Depth = depth;
        }

        public Sprite(Texture2D texture, Rectangle[] frames, Vector2 position) :
            this(texture, frames, new Transform(position),
            Color.White, SpriteEffects.None, 0.00f)
        {
        }

        public Sprite(Texture2D texture, Rectangle[] frames, float x, float y) :
            this(texture, frames, new Transform(new Vector2(x, y)),
             Color.White, SpriteEffects.None, 0.00f)
        {
        }

        public Sprite(Texture2D texture, Rectangle[] frames) :
            this(texture, frames, Transform.Identity,
             Color.White, SpriteEffects.None, 0.00f)
        {
        }

        public Sprite(Texture2D texture, Vector2 position) :
            this(texture, null, new Transform(position),
            Color.White, SpriteEffects.None, 0.00f)
        {
        }

        public Sprite(Texture2D texture, float x, float y) :
            this(texture, null, new Transform(x,y),
            Color.White, SpriteEffects.None, 0.00f)
        {
        }

        public Sprite(Texture2D texture) :
            this(texture, null, Transform.Identity,
           Color.White, SpriteEffects.None, 0.00f)
        {
        }

        public Sprite() :
            this(null, null, Transform.Identity,
            Color.White, SpriteEffects.None, 0.00f)
        {
        }

        public Sprite(Sprite other)
        {
            Texture = other.Texture;
            Frames = other.Frames;

            Transform = other.Transform;
            
            Color = other.Color;
            Flip = other.Flip;
            Depth = other.Depth;
        }
        #endregion

        // Fields
        public Texture2D Texture;
        public Rectangle[] Frames;

        public Transform Transform;

        public Color Color;
        public SpriteEffects Flip;
        public float Depth;

        // Static methods
        #region CreateFrames
        public static Rectangle[] CreateFrames(
            int count, int stride,
            int x, int y, int width, int height,
            int stepX, int stepY)
        {
            if (count < 1 || stride < 1)
                return new Rectangle[0];

            Rectangle[] result = new Rectangle[count];
            for (int i = 0; i < count; i++)
            {
                result[i] = new Rectangle(
                    (i % stride) * stepX + x,
                    (i / stride) * stepY + y,
                    width,
                    height);
            }
            return result;
        }

        public static Rectangle[] CreateFrames(
            int count, int stride,
            int x, int y, int width, int height)
        {
            return CreateFrames(count, stride, x, y, width, height, width, height);
        }
        #endregion
        #region GetFrame
        public static Rectangle GetFrame(Rectangle[] frames, float percent)
        {
            if (frames == null || frames.Length == 0)
                return Rectangle.Empty;

            if (frames.Length == 1)
                return frames[0];

            if (percent <= 0.00f)
                return frames[0];
            else if (percent >= 1.00f)
                return frames[frames.Length - 1];
            else
                return frames[(int)(percent * frames.Length)];
        }

        public static Rectangle GetFrame(Rectangle[] frames, float time, float duration)
        {
            if (time <= 0.00f || duration <= 0.00f)
                return GetFrame(frames, 0.00f);
            else if (time >= duration)
                return GetFrame(frames, 1.00f);
            else
                return GetFrame(frames, time / duration);
        }
        #endregion

        // Methods
        #region GetFrame
        public Rectangle GetFrame(float percent)
        {
            return GetFrame(Frames, percent);
        }

        public Rectangle GetFrame(float time, float duration)
        {
            return GetFrame(Frames, time, duration);
        }
        #endregion
    }
}