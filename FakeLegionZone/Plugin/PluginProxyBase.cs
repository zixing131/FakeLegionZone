using System;
using System.IO;
using System.Runtime.InteropServices;
using FakeLegionZone.Util;

namespace FakeLegionZone.Plugin
{
	// Token: 0x02000038 RID: 56
	public abstract class PluginProxyBase
	{
		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000194 RID: 404 RVA: 0x00009E85 File Offset: 0x00008085
		// (set) Token: 0x06000195 RID: 405 RVA: 0x00009E8D File Offset: 0x0000808D
		internal string PluginName { get; set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000196 RID: 406 RVA: 0x00009E96 File Offset: 0x00008096
		// (set) Token: 0x06000197 RID: 407 RVA: 0x00009E9E File Offset: 0x0000809E
		public bool CanUse { get; set; }

		// Token: 0x06000198 RID: 408 RVA: 0x00009EA8 File Offset: 0x000080A8
		public PluginProxyBase(PluginDllNotifyCallback parentPluginNotifyCallback, string dllFilePath)
		{
			LogHelper.Log("[PluginProxyBase] [PluginProxyBase] 准备开始初始化代理类：plugin_dll = " + dllFilePath + "。");
			if (this.CheckFileExist(dllFilePath) && this.CheckSignature(dllFilePath))
			{
				this.InitPlugin();
				if (this.IsRegisterCallback)
				{
					this.parentPluginNotifyCallback = parentPluginNotifyCallback;
					this.subPluginNotifyCallback = new PluginProxyBase.SubPluginDllNotifyCallback(this.SubPluginNotify_Callback);
					this.RegisterDelegate();
				}
			}
			else
			{
				this.CanUse = false;
				LogHelper.Log("[PluginProxyBase] [PluginProxyBase] 初始化代理类失败，plugin dll 文件不存在或者文件签名不正确：plugin_dll = " + dllFilePath + "。");
			}
			LogHelper.Log("[PluginProxyBase] [PluginProxyBase] 初始化代理类结束：plugin_dll = " + dllFilePath + "。");
		}

		// Token: 0x06000199 RID: 409
		protected abstract void InitPlugin();

		// Token: 0x0600019A RID: 410
		protected abstract void RegisterDelegate();

		// Token: 0x0600019B RID: 411
		internal abstract void Execute(string jsonData);

		// Token: 0x0600019C RID: 412
		internal abstract void UninitPlugin();

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600019D RID: 413
		internal abstract bool IsRegisterCallback { get; }

		// Token: 0x0600019E RID: 414
		protected abstract void SubPluginNotify_Callback(PluginNotifyCategory category, string jsonData);

		// Token: 0x0600019F RID: 415 RVA: 0x00009F43 File Offset: 0x00008143
		private bool CheckFileExist(string path)
		{
			return File.Exists(path);
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x00009F50 File Offset: 0x00008150
		private bool CheckSignature(string path)
		{
			return VerifySignature.Verify(path);
		}

		// Token: 0x040000D7 RID: 215
		protected PluginDllNotifyCallback parentPluginNotifyCallback;

		// Token: 0x040000DA RID: 218
		protected PluginProxyBase.SubPluginDllNotifyCallback subPluginNotifyCallback;

		// Token: 0x020000CF RID: 207
		// (Invoke) Token: 0x06000511 RID: 1297
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		protected delegate void SubPluginDllNotifyCallback(PluginNotifyCategory category, string jsonData);
	}
}
