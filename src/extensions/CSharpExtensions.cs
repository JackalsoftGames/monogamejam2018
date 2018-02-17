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
    public static class CSharpExtensions
    {
        #region Array.GetIndex
        public static int GetIndex<T>(this T[] obj, float percent)
        {
            if (obj.Length <= 1)
                return 0;

            if (percent <= 0.00f)
                return 0;
            else if (percent >= 1.00f)
                return obj.Length - 1;
            else
                return (int)(obj.Length * percent);
        }
        #endregion

        // Random
        #region Random.Chance
        public static bool Chance(this Random obj, int maxValue)
        {
            return (obj.Next(0, maxValue) == 0);
        }
        #endregion
        #region Random.Pick
        public static T Pick<T>(this Random obj, T[] value)
        {
            if (value == null || value.Length == 0)
                return default(T);
            return value[obj.Next(0, value.Length)];
        }
        #endregion

        #region Random.NextFloat
        public static float NextFloat(this Random obj)
        {
            return (float)obj.NextDouble();
        }
        #endregion
        #region Random.NextVector2f
        public static Vector2 NextVector2(this Random obj)
        {
            return new Vector2(
                (float)obj.NextDouble(),
                (float)obj.NextDouble());
        }
        #endregion
        #region Random.NextVector3f
        public static Vector3 NextVector3f(this Random obj)
        {
            return new Vector3(
                (float)obj.NextDouble(),
                (float)obj.NextDouble(),
                (float)obj.NextDouble());
        }
        #endregion

        #region Random.NextArc
        public static Vector2 NextArc(this Random obj, double radians)
        {
            radians *= obj.NextDouble();
            return new Vector2(
                (float)Math.Cos(radians),
                (float)Math.Sin(radians));
        }
        #endregion
        #region Random.NextQuadrant
        public static Vector2 NextQuadrant(this Random obj)
        {
            double x, y;
            do
            {
                x = obj.NextDouble();
                y = obj.NextDouble();
            }
            while (x * x + y * y >= 1.00);
            return new Vector2((float)x, (float)y);
        }
        #endregion
        #region Random.NextCircle
        public static Vector2 NextCircle(this Random obj)
        {
            Vector2 result = obj.NextQuadrant();
            if (obj.Next(0, 2) == 0) result.X *= -1;
            if (obj.Next(0, 2) == 0) result.Y *= -1;
            return result;
        }
        #endregion
    }
}