using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FakeLegionZone.Util;

namespace FakeLegionZone.Plugin
{
	// Token: 0x02000036 RID: 54
	public class PluginDlls
	{
		// Token: 0x14000015 RID: 21
		// (add) Token: 0x0600018C RID: 396 RVA: 0x00009B04 File Offset: 0x00007D04
		// (remove) Token: 0x0600018D RID: 397 RVA: 0x00009B3C File Offset: 0x00007D3C
		internal event PluginEventDelegate PluginEventDelegate;

		// Token: 0x0600018E RID: 398 RVA: 0x00009B71 File Offset: 0x00007D71
		private PluginDlls()
		{
			this.parentPluginNotifyCallback = new PluginDllNotifyCallback(this.PluginDllNotify_Callback);
			this.InitPlugins();
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600018F RID: 399 RVA: 0x00009B9C File Offset: 0x00007D9C
		public static PluginDlls Instance
		{
			get
			{
				if (PluginDlls.instance == null)
				{
					PluginDlls.instance = new PluginDlls();
				}
				return PluginDlls.instance;
			}
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00009BB4 File Offset: 0x00007DB4
		private void InitPlugins()
		{
			LogHelper.Log("[PluginDlls] [InitPlugins] 准备开始初始化所有 plugin dll 代理类。");
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			try
			{
				IEnumerable<Type> enumerable = from p in Assembly.GetExecutingAssembly().GetTypes()
											   where p.Namespace != null && p.Namespace.Equals("LZTray.Plugin.PluginProxies") && p.BaseType == typeof(PluginProxyBase)
											   select p;
				object[] args = new object[] { this.parentPluginNotifyCallback };
				foreach (Type type in enumerable)
				{
					PluginProxyBase pluginProxyBase = (PluginProxyBase)executingAssembly.CreateInstance(type.FullName, true, BindingFlags.Default, null, args, null, null);
					pluginProxyBase.PluginName = type.Name;
					if (pluginProxyBase.CanUse)
					{
						this.listPluginDll.Add(pluginProxyBase);
					}
				}
				LogHelper.Log(string.Format("[PluginDlls] [InitPlugins] 初始化所有 plugin dll 代理类结束，共有 {0} 个代理类被实例。", this.listPluginDll.Count));
			}
			catch (Exception ex)
			{
				LogHelper.Log("[PluginDlls] [InitPlugins] 初始化 plugin dll 代理类列表异常：ex = " + ex.Message);
			}
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00009CCC File Offset: 0x00007ECC
		internal void Execute(string pluginName, string jsonData)
		{
			if (pluginName == "All")
			{
				using (List<PluginProxyBase>.Enumerator enumerator = this.listPluginDll.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						PluginProxyBase pluginProxyBase = enumerator.Current;
						pluginProxyBase.Execute(jsonData);
					}
					return;
				}
			}
			IEnumerable<PluginProxyBase> enumerable = from p in this.listPluginDll
													  where p.PluginName == pluginName
													  select p;
			if (enumerable != null)
			{
				foreach (PluginProxyBase pluginProxyBase2 in enumerable)
				{
					pluginProxyBase2.Execute(jsonData);
				}
			}
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00009D8C File Offset: 0x00007F8C
		internal void UninitPlugins()
		{
			LogHelper.Log(string.Format("[PluginDlls] [UninitPlugins] 反初始化所有 plugin dll 代理类，共有 {0} 个代理类需要被反初始化。", this.listPluginDll.Count));
			foreach (PluginProxyBase pluginProxyBase in this.listPluginDll)
			{
				try
				{
					LogHelper.Log("[PluginDlls] [UninitPlugins] 反初始化" + ((pluginProxyBase != null) ? pluginProxyBase.PluginName : null) + " plugin dll 代理类。");
					pluginProxyBase.UninitPlugin();
				}
				catch (Exception ex)
				{
					LogHelper.Log(string.Concat(new string[]
					{
						"[PluginDlls] [UninitPlugins] 反初始化 ",
						(pluginProxyBase != null) ? pluginProxyBase.PluginName : null,
						" plugin dll 代理类异常：ex = ",
						ex.Message,
						"。"
					}));
				}
			}
		}

		// Token: 0x06000193 RID: 403 RVA: 0x00009E70 File Offset: 0x00008070
		private void PluginDllNotify_Callback(string pluginName, PluginNotifyCategory category, string jsonData)
		{
			PluginEventDelegate pluginEventDelegate = this.PluginEventDelegate;
			if (pluginEventDelegate == null)
			{
				return;
			}
			pluginEventDelegate(pluginName, category, jsonData);
		}

		// Token: 0x040000CE RID: 206
		private static PluginDlls instance;

		// Token: 0x040000CF RID: 207
		private List<PluginProxyBase> listPluginDll = new List<PluginProxyBase>();

		// Token: 0x040000D0 RID: 208
		private PluginDllNotifyCallback parentPluginNotifyCallback;
	}
}
