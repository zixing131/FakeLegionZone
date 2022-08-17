using System;
using System.Runtime.InteropServices;

namespace FakeLegionZone.Plugin
{
	// Token: 0x02000024 RID: 36
	// (Invoke) Token: 0x0600007C RID: 124
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate void PluginNotifyCallback(string pszCategory, string pszInputJson);
}
