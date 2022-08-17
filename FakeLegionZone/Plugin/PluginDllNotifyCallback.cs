using System;
using System.Runtime.InteropServices;

namespace FakeLegionZone.Plugin
{
	// Token: 0x02000034 RID: 52
	// (Invoke) Token: 0x06000185 RID: 389
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate void PluginDllNotifyCallback(string pluginName, PluginNotifyCategory category, string jsonData);
}
