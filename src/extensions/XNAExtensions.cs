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

namespace Presto.Extensions
{
    public static class XNAExtensions
    {
        #region SpriteBatch.DrawSprite
        public static void DrawSprite(this SpriteBatch obj, Sprite sprite, Vector2 position, float percent)
        {
            obj.Draw(sprite.Texture, position + sprite.Transform.Translation, sprite.GetFrame(percent), sprite.Color,
                sprite.Transform.Rotation, sprite.Transform.Origin, sprite.Transform.Scale,
                sprite.Flip, sprite.Depth);
        }

        public static void DrawSprite(this SpriteBatch obj, Sprite sprite, float x, float y, float percent)
        {
            obj.Draw(sprite.Texture, new Vector2(x, y) + sprite.Transform.Translation, sprite.GetFrame(percent), sprite.Color,
                sprite.Transform.Rotation, sprite.Transform.Origin, sprite.Transform.Scale,
                sprite.Flip, sprite.Depth);
        }

        public static void DrawSprite(this SpriteBatch obj, Sprite sprite, float percent)
        {
            obj.Draw(sprite.Texture, sprite.Transform.Translation, sprite.GetFrame(percent), sprite.Color,
                sprite.Transform.Rotation, sprite.Transform.Origin, sprite.Transform.Scale,
                sprite.Flip, sprite.Depth);
        }
        #endregion
    }
}