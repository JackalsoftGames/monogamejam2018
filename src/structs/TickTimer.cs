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
    [System.Diagnostics.DebuggerDisplay(@"\{Time:{Time} Duration:{Duration} Enabled:{Enabled} Repeat:{Repeat}\}")]
    public struct TickTimer :
        ITickable
    {
        #region Constructors
        public TickTimer(ushort duration, bool repeat)
        {
            Time = 0;
            Duration = duration;

            Enabled = true;
            Repeat = repeat;
        }
        #endregion

        // Fields
        public ushort Time;
        public ushort Duration;

        public bool Enabled;
        public bool Repeat;

        // Properties
        public bool IsElapsed
        {
            get
            {
                if (Repeat)
                    return (Time == 0);
                return (Time >= Duration);
            }
        }

        // Methods
        #region Tick
        public void Tick(float time)
        {
            if (!Enabled)
                return;

            if (Duration == 0)
            {
                if (Time > 0)
                    Time = 0;
                return;
            }

            time += Time;
            if (time < Duration)
                Time = (ushort)time;
            else
            {
                if (Repeat)
                    Time = 0;
                else
                    Reset();
            }
        }
        #endregion
        #region GetPercent
        public float GetPercent()
        {
            if (Time == 0 || Duration == 0)
                return 0.00f;
            else if (Time >= Duration)
                return 1.00f;
            return Time / (float)Duration;
        }
        #endregion

        #region Start
        public void Start(ushort duration, bool repeat)
        {
            Time = 0;
            Duration = duration;

            Enabled = true;
            Repeat = repeat;
        }

        public void Start()
        {
            Enabled = true;
        }
        #endregion
        #region Stop
        public void Stop()
        {
            Enabled = false;
        }
        #endregion
        #region Pause
        public void Pause()
        {
            Enabled = !Enabled;
        }
        #endregion
        #region Reset
        public void Reset()
        {
            Time = 0;
            Duration = 0;

            Enabled = false;
            Repeat = false;
        }
        #endregion
        #region Restart
        public void Restart()
        {
            Time = 0;
            Enabled = true;
        }
        #endregion
    }
}