using FakeLegionZone.Model;
using System;
using System.Runtime.InteropServices; 

namespace FakeLegionZone.Common
{
	// Token: 0x02000096 RID: 150
	public class NativesApi
	{
		// Token: 0x06000434 RID: 1076 RVA: 0x0000E197 File Offset: 0x0000C397
		private NativesApi()
		{
		}

		// Token: 0x06000435 RID: 1077
		[DllImport("user32.dll", CharSet = CharSet.Unicode)]
		public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

		// Token: 0x06000436 RID: 1078
		[DllImport("user32.dll")]
		public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, ref CopyDataStruct lParam);

		// Token: 0x06000437 RID: 1079
		[DllImport("user32.dll")]
		public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, int lParam);

		// Token: 0x06000438 RID: 1080
		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern int RegisterWindowMessage(string lpString);

		// Token: 0x06000439 RID: 1081
		[DllImport("User32.dll")]
		public static extern bool ChangeWindowMessageFilterEx(IntPtr hwnd, int message, int action, ref CHANGEFILTERSTRUCT pChangeFilterStruct);

		// Token: 0x0600043A RID: 1082
		[DllImport("kernel32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool IsWow64Process([In] IntPtr process, out bool wow64Process);

		// Token: 0x0600043B RID: 1083
		[DllImport("user32.dll")]
		public static extern IntPtr GetWindowLong(IntPtr hWnd, int nIndex);

		// Token: 0x0600043C RID: 1084
		[DllImport("user32.dll", EntryPoint = "SetWindowLongPtr", SetLastError = true)]
		public static extern IntPtr IntSetWindowLongPtr(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

		// Token: 0x0600043D RID: 1085
		[DllImport("user32.dll", EntryPoint = "SetWindowLong", SetLastError = true)]
		public static extern int IntSetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

		// Token: 0x0600043E RID: 1086
		[DllImport("kernel32.dll")]
		public static extern void SetLastError(int dwErrorCode);

		// Token: 0x0400021A RID: 538
		public const int WM_CLOSE = 1132;

		// Token: 0x0400021B RID: 539
		public const int WM_COPYDATA = 74;
	}
}
