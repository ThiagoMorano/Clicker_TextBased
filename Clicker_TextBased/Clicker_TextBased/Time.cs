using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clicker_TextBased
{
    static class Time
    {
        private static long _ticksSinceLastCycle = 0;
        private static long _ticksSinceLastFrame = 0;

        public static double DeltaTime { get { return TimeSpan.FromTicks(System.DateTime.Now.Ticks - _ticksSinceLastFrame).TotalSeconds; } }
        public static double TimeSinceLastCycle { get { return TimeSpan.FromTicks(System.DateTime.Now.Ticks - _ticksSinceLastCycle).TotalSeconds; } }

        public static void Init() {
            _ticksSinceLastCycle = System.DateTime.Now.Ticks;
            _ticksSinceLastFrame = System.DateTime.Now.Ticks;
            UpdateSinceLastCycle();
            UpdateSinceLastFrame();
        }

        public static void UpdateSinceLastCycle() {
            _ticksSinceLastCycle = System.DateTime.Now.Ticks;
        }

        public static void UpdateSinceLastFrame() {
            _ticksSinceLastFrame = System.DateTime.Now.Ticks;
        }
    }
}
