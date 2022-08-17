using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Threading;
using FakeLegionZone.Common;

namespace FakeLegionZone.Util
{
	// Token: 0x02000032 RID: 50
	public class Utils
	{
		// Token: 0x06000179 RID: 377 RVA: 0x000097C8 File Offset: 0x000079C8
		public static bool Is64Process(Process process)
		{
			bool result;
			NativesApi.IsWow64Process(process.Handle, out result);
			return result;
		}

		// Token: 0x0600017A RID: 378 RVA: 0x000097E4 File Offset: 0x000079E4
		public static string GetBasePath()
		{
			return AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
		}

		// Token: 0x0600017B RID: 379 RVA: 0x000097F8 File Offset: 0x000079F8
		public static long GetTimestamp()
		{
			return (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000L) / 10000L;
		}

		// Token: 0x0600017C RID: 380 RVA: 0x0000982B File Offset: 0x00007A2B
		public static void DelayWork(Action action, int millisecond = 600)
		{
			new Action<Dispatcher, Action, int>(Utils.DoWorkAsync).BeginInvoke(Dispatcher.CurrentDispatcher, action, millisecond, null, null);
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00009848 File Offset: 0x00007A48
		private static void DoWorkAsync(Dispatcher dispatcher, Action action, int millisecond)
		{
			Thread.Sleep(millisecond);
			dispatcher.BeginInvoke(action, Array.Empty<object>());
		}
	}
}
