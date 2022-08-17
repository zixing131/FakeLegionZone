using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeLegionZone.Util
{
    internal class LenovoOne
    {

        public static LenovoOne Instance  = new LenovoOne();
        internal static bool IsSupported()
        {
            return false;
        }

        public void SendHardwareToLenovoOneDevice(string data)
        {

        }
        public void Stop()
        {

        }

        internal void Init()
        {

        }
    }
}
