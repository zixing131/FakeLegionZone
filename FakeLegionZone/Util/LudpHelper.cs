using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeLegionZone.Util
{
    internal class LudpHelper
    {
        public static bool IsTrack { get; internal set; } = false;

        internal static void TrackEvent(string category, string action, string label, Dictionary<string, string> dictionary)
        {
            return;
        }

        internal static void AutoReport()
        {

        }
    }
}
