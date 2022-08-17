using System;
using System.Runtime.InteropServices;

namespace FakeLegionZone.Model
{
	// Token: 0x0200006B RID: 107
	public struct LZ_MSG_SELF_RUN_LZMAIN
	{
		// Token: 0x0400019F RID: 415
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 2048)]
		public string Data;
	}
}
