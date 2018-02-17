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
    [System.Diagnostics.DebuggerDisplay(@"\{Origin:{Origin} Scale:{Scale} Rotation:{Rotation} Translation:{Translation}\}")]
    public struct Transform :
        IEquatable<Transform>
    {
        #region Constructors
        public Transform(Vector2 origin, Vector2 scale, float rotation, Vector2 translation)
        {
            Origin = origin;
            Scale = scale;
            Rotation = rotation;
            Translation = translation;
        }

        public Transform(float originX, float originY, float scaleX, float scaleY,
            float rotation, float translationX, float translationY)
        {
            Origin = new Vector2(originX, originY);
            Scale = new Vector2(scaleX, scaleY);
            Rotation = rotation;
            Translation = new Vector2(translationX, translationY);
        }

        public Transform(Vector2 translation)
        {
            Origin = Vector2.Zero;
            Scale = Vector2.One;
            Rotation = 0.00f;
            Translation = translation;
        }

        public Transform(float x, float y)
        {
            Origin = Vector2.Zero;
            Scale = Vector2.One;
            Rotation = 0.00f;
            Translation = new Vector2(x, y);
        }
        #endregion

        // Operators
        #region ==
        public static bool operator ==(Transform value1, Transform value2)
        {
            return
                (value1.Origin == value2.Origin) &&
                (value1.Scale == value2.Scale) &&
                (value1.Rotation == value2.Rotation) &&
                (value1.Translation == value2.Translation);
        }
        #endregion
        #region !=
        public static bool operator !=(Transform value1, Transform value2)
        {
            return !(value1 == value2);
        }
        #endregion

        // Fields
        public Vector2 Origin;
        public Vector2 Scale;
        public float Rotation;
        public Vector2 Translation;

        // Static properties
        public static Transform Identity
        {
            get { return new Transform(Vector2.Zero, Vector2.One, 0.00f, Vector2.Zero); }
        }

        // Static methods
        #region Lerp
        public static Transform Lerp(Transform value1, Transform value2, float amount)
        {
            return new Transform(
                Vector2.Lerp(value1.Origin, value2.Origin, amount),
                Vector2.Lerp(value1.Scale, value2.Scale, amount),
                MathHelper.Lerp(value1.Rotation, value2.Rotation, amount),
                Vector2.Lerp(value1.Translation, value2.Translation, amount));
        }

        public static void Lerp(ref Transform value1, ref Transform value2, float amount, out Transform result)
        {
            Vector2.Lerp(ref value1.Origin, ref value2.Origin, amount, out result.Origin);
            Vector2.Lerp(ref value1.Scale, ref value2.Scale, amount, out result.Scale);
            result.Rotation = MathHelper.Lerp(value1.Rotation, value2.Rotation, amount);
            Vector2.Lerp(ref value1.Translation, ref value2.Translation, amount, out result.Translation);
        }
        #endregion

        // Methods
        #region Equals
        public bool Equals(Transform other)
        {
            return (this == other);
        }

        public override bool Equals(object other)
        {
            return (other is Transform) &&
                (this == (Transform)other);
        }
        #endregion
        #region GetHashCode
        public override int GetHashCode()
        {
            return
                Origin.GetHashCode() ^
                Scale.GetHashCode() ^
                Rotation.GetHashCode() ^
                Translation.GetHashCode();
        }
        #endregion

        #region GetMatrix
        public Matrix GetMatrix()
        {
            return
                Matrix.CreateTranslation(Origin.X, Origin.Y, 0.00f) *
                Matrix.CreateRotationZ(Rotation) *
                Matrix.CreateScale(Scale.X, Scale.Y, 0.00f) *
                Matrix.CreateTranslation(Translation.X, Translation.Y, 0.00f);
        }
        #endregion

        #region Lerp
        public Transform Lerp(Transform other, float amount)
        {
            return new Transform(
                Vector2.Lerp(this.Origin, other.Origin, amount),
                Vector2.Lerp(this.Scale, other.Scale, amount),
                MathHelper.Lerp(this.Rotation, other.Rotation, amount),
                Vector2.Lerp(this.Translation, other.Translation, amount));
        }
        #endregion
        #region TransformPoint
        public Vector2 TransformPoint(Vector2 value)
        {
            // Matrix math borrowed from FNA's source
            float val1 = (float)Math.Cos(Rotation);
            float val2 = (float)Math.Sin(Rotation);

            value = (value + Origin) * Scale;
            return Translation + new Vector2(
                value.X * val1 + value.Y * -val2,
                value.X * val2 + value.Y * val1);
        }

        public Vector2 TransformPoint(float x, float y)
        {
            return TransformPoint(new Vector2(x, y));
        }
        #endregion
    }
}