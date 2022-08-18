using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;
using System.Security.AccessControl;
using System.Security.Principal;
using Microsoft.Win32;

namespace FakeLegionZone.Util
{
	// Token: 0x02000030 RID: 48
	public class RegistryHelper
	{
		// Token: 0x0600013E RID: 318 RVA: 0x00007C3B File Offset: 0x00005E3B
		private RegistryHelper()
		{
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00007C44 File Offset: 0x00005E44
		~RegistryHelper()
		{
			if (this.watcher != null)
			{
				this.watcher.EventArrived -= this.Watcher_EventArrived;
				this.watcher.Stop();
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000140 RID: 320 RVA: 0x00007C94 File Offset: 0x00005E94
		public static RegistryHelper Instance
		{
			get
			{
				if (RegistryHelper.instance == null)
				{
					RegistryHelper.instance = new RegistryHelper();
				}
				return RegistryHelper.instance;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000141 RID: 321 RVA: 0x00007CAC File Offset: 0x00005EAC
		public string InstallDir
		{
			get
			{
				if (string.IsNullOrEmpty(this.installDir))
				{
					this.installDir = this.GetInstallDir();
				}
				return this.installDir;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000142 RID: 322 RVA: 0x00007CCD File Offset: 0x00005ECD
		public string SupplyID
		{
			get
			{
				if (string.IsNullOrEmpty(this.supplyID))
				{
					this.supplyID = this.GetSupplyID();
				}
				return this.supplyID;
			}
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00007CF0 File Offset: 0x00005EF0
		private string GetInstallDir()
		{
			string result;
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Lenovo\\LegionZone\\", RegistryKeyPermissionCheck.ReadSubTree, RegistryRights.ExecuteKey))
				{
					if (registryKey != null)
					{
						result = (string)registryKey.GetValue("InstallDir", string.Empty);
					}
					else
					{
						result = string.Empty;
					}
				}
			}
			catch (Exception ex)
			{
				LogHelper.Log("[RegistryHelper] [GetInstallDir] 从注册表读取安装路径异常：" + ex.Message);
				result = string.Empty;
			}
			return result;
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00007D7C File Offset: 0x00005F7C
		public string GetVersion()
		{
			string result;
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Lenovo\\LegionZone\\", RegistryKeyPermissionCheck.ReadSubTree, RegistryRights.ExecuteKey))
				{
					if (registryKey != null)
					{
						result = (string)registryKey.GetValue("Version", string.Empty);
					}
					else
					{
						result = string.Empty;
					}
				}
			}
			catch (Exception ex)
			{
				LogHelper.Log("[RegistryHelper] [GetVersion] 从注册表读取版本号异常：" + ex.Message);
				result = string.Empty;
			}
			return result;
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00007E08 File Offset: 0x00006008
		private string GetSupplyID()
		{
			string result;
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Lenovo\\LegionZone\\", RegistryKeyPermissionCheck.ReadSubTree, RegistryRights.ExecuteKey))
				{
					if (registryKey != null)
					{
						result = (string)registryKey.GetValue("SupplyID", string.Empty);
					}
					else
					{
						result = string.Empty;
					}
				}
			}
			catch (Exception ex)
			{
				LogHelper.Log("[RegistryHelper] [GetSupplyID] 从注册表读取渠道号异常：" + ex.Message);
				result = string.Empty;
			}
			return result;
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00007E94 File Offset: 0x00006094
		public bool GetEnableLog()
		{ 
			bool result;
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Lenovo\\LegionZone\\", RegistryKeyPermissionCheck.ReadSubTree, RegistryRights.ExecuteKey))
				{
					if (registryKey != null)
					{
						result = (int)registryKey.GetValue("EnableLog", 0) != 0;
					}
					else
					{
						result = false;
					}
				}
			}
			catch (Exception)
			{
				result = false;
			}
			return result;
		}
		
		/// <summary>
		/// 设置是否启用日志
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public bool SetEnableLog(bool data)
		{ 
			bool result;
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Lenovo\\LegionZone\\", RegistryKeyPermissionCheck.ReadWriteSubTree, RegistryRights.WriteKey))
				{
					if (registryKey != null)
					{
						registryKey.SetValue("EnableLog", data ? 1 : 0)  ;
						result = true;
					}
					else
					{
						var registryKey2 = Registry.LocalMachine.CreateSubKey("SOFTWARE\\Lenovo\\LegionZone\\");
						registryKey2.Close();
						return SetEnableLog(data);
					}
				}
			}
			catch (Exception)
			{
				result = false;
			}
			return result;
		}


		// Token: 0x06000147 RID: 327 RVA: 0x00007F0C File Offset: 0x0000610C
		public void DeleteOOBEKey()
		{
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Lenovo\\LegionZone\\", RegistryKeyPermissionCheck.ReadWriteSubTree))
				{
					if (registryKey != null)
					{
						if (registryKey.GetValueNames().Contains("LZOOBE"))
						{
							LogHelper.Log("[RegistryHelper] [DeleteOOBEKey] 注册表中存在 LZOOBE 键值，需要删除");
							registryKey.DeleteValue("LZOOBE");
						}
					}
					else
					{
						LogHelper.Log("[RegistryHelper] [DeleteOOBEKey] key = null");
					}
				}
			}
			catch (Exception ex)
			{
				LogHelper.Log("[RegistryHelper] [DeleteOOBEKey] 删除 LZOOBE 键值异常：" + ex.Message);
			}
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00007FA4 File Offset: 0x000061A4
		public bool GetIsLZTrayAutoRun()
		{
			bool result;
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Lenovo\\LegionZone\\config\\", RegistryKeyPermissionCheck.ReadSubTree, RegistryRights.ExecuteKey))
				{
					if (registryKey != null)
					{
						result = (int)registryKey.GetValue("IsLZTrayAutoRun", 0) != 0;
					}
					else
					{
						result = false;
					}
				}
			}
			catch (Exception ex)
			{
				LogHelper.Log("[RegistryHelper] [GetIsLZTrayAutoRun] 从注册表读取是否开机自动运行托盘异常：" + ex.Message);
				result = false;
			}
			return result;
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00008030 File Offset: 0x00006230
		public bool GetIsGamingBoost()
		{
			bool result;
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Lenovo\\LegionZone\\config\\", RegistryKeyPermissionCheck.ReadSubTree, RegistryRights.ExecuteKey))
				{
					if (registryKey != null)
					{
						result = (int)registryKey.GetValue("IsGamingBoost", 0) != 0;
					}
					else
					{
						result = false;
					}
				}
			}
			catch (Exception ex)
			{
				LogHelper.Log("[RegistryHelper] [GetIsGamingBoost] 从注册表读取是否自动游戏加速异常：" + ex.Message);
				result = false;
			}
			return result;
		}

		// Token: 0x0600014A RID: 330 RVA: 0x000080BC File Offset: 0x000062BC
		public bool GetIsPerformOCMode()
		{
			bool result;
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Lenovo\\LegionZone\\config\\", RegistryKeyPermissionCheck.ReadSubTree, RegistryRights.ExecuteKey))
				{
					if (registryKey != null)
					{
						result = (int)registryKey.GetValue("IsPerformOCMode", 0) != 0;
					}
					else
					{
						result = false;
					}
				}
			}
			catch (Exception ex)
			{
				LogHelper.Log("[RegistryHelper] [GetIsPerformOCMode] 从注册表读取是否自动切换到野兽模式异常：" + ex.Message);
				result = false;
			}
			return result;
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00008148 File Offset: 0x00006348
		public List<string> GetGameSnapshotHotkey()
		{
			List<string> list = new List<string>();
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Lenovo\\LegionZone\\config\\", RegistryKeyPermissionCheck.ReadSubTree, RegistryRights.ExecuteKey))
				{
					if (registryKey != null)
					{
						string text = (string)registryKey.GetValue("GameSnapshotHotkey", 0);
						if (!string.IsNullOrEmpty(text))
						{
							foreach (string item in text.Split(new char[] { '+' }))
							{
								list.Add(item);
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				LogHelper.Log("[RegistryHelper] [GetGameSnapshotHotkey] 从注册表读取游戏内截图热键异常：" + ex.Message);
			}
			return list;
		}

		// Token: 0x0600014C RID: 332 RVA: 0x0000820C File Offset: 0x0000640C
		public string GetGameSnapshotHotkeyString()
		{
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Lenovo\\LegionZone\\config\\", RegistryKeyPermissionCheck.ReadSubTree, RegistryRights.ExecuteKey))
				{
					if (registryKey != null)
					{
						return (string)registryKey.GetValue("GameSnapshotHotkey", 0);
					}
				}
			}
			catch (Exception ex)
			{
				LogHelper.Log("[RegistryHelper] [GetGameSnapshotHotkey] 从注册表读取游戏内截图热键异常：" + ex.Message);
			}
			return "";
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00008294 File Offset: 0x00006494
		public List<string> GetLZToolkitHotkey()
		{
			List<string> list = new List<string>();
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Lenovo\\LegionZone\\config\\", RegistryKeyPermissionCheck.ReadSubTree, RegistryRights.ExecuteKey))
				{
					if (registryKey != null)
					{
						string text = (string)registryKey.GetValue("LZToolkitHotkey", 0);
						if (!string.IsNullOrEmpty(text))
						{
							foreach (string item in text.Split(new char[] { '+' }))
							{
								list.Add(item);
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				LogHelper.Log("[RegistryHelper] [GetLZToolkitHotkey] 从注册表读取启动游戏内工具箱热键异常：" + ex.Message);
			}
			return list;
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00008358 File Offset: 0x00006558
		public string GetLZToolkitHotkeyString()
		{
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Lenovo\\LegionZone\\config\\", RegistryKeyPermissionCheck.ReadSubTree, RegistryRights.ExecuteKey))
				{
					if (registryKey != null)
					{
						return (string)registryKey.GetValue("LZToolkitHotkey", "");
					}
				}
			}
			catch (Exception ex)
			{
				LogHelper.Log("[RegistryHelper] [GetLZToolkitHotkey] 从注册表读取启动游戏内工具箱热键异常：" + ex.Message);
			}
			return "";
		}

		// Token: 0x0600014F RID: 335 RVA: 0x000083E0 File Offset: 0x000065E0
		public string GetSaveSnapImagePath()
		{
			string result;
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Lenovo\\LegionZone\\config\\", RegistryKeyPermissionCheck.ReadSubTree, RegistryRights.ExecuteKey))
				{
					if (registryKey != null)
					{
						string text = (string)registryKey.GetValue("GameSnapshotPath", "not exist");
						if (text == "not exist" || text == "")
						{
							result = this.SetSaveSnapImagePath();
						}
						else
						{
							result = text;
						}
					}
					else
					{
						result = "";
					}
				}
			}
			catch (Exception ex)
			{
				LogHelper.Log("[RegistryHelper] [GetSaveSnapImagePath] 从注册表读取是否自动切换到野兽模式异常：" + ex.Message);
				result = "";
			}
			return result;
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00008490 File Offset: 0x00006690
		private string SetSaveSnapImagePath()
		{
			string defaultSaveImagePath = this.GetDefaultSaveImagePath();
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Lenovo\\LegionZone\\config\\", RegistryKeyPermissionCheck.ReadWriteSubTree))
				{
					if (registryKey != null)
					{
						registryKey.SetValue("GameSnapshotPath", defaultSaveImagePath, RegistryValueKind.String);
					}
				}
			}
			catch (Exception ex)
			{
				LogHelper.Log("[RegistryHelper] [SetSaveSnapImagePath] 向注册表中添加默认保存截图路径异常：" + ex.Message);
			}
			return defaultSaveImagePath;
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00008508 File Offset: 0x00006708
		private string GetDefaultSaveImagePath()
		{
			string path = "Legion Zone\\Picture";
			string path2 = "C:\\";
			try
			{
				DriveInfo[] drives = DriveInfo.GetDrives();
				long num = 0L;
				foreach (DriveInfo driveInfo in drives)
				{
					if (driveInfo.IsReady && driveInfo.DriveType == DriveType.Fixed && driveInfo.TotalSize > num)
					{
						path2 = driveInfo.Name;
						num = driveInfo.TotalFreeSpace;
					}
				}
			}
			catch (Exception ex)
			{
				LogHelper.Log("[RegistryHelper] [GetDefaultSaveImagePath] 查询当前电脑空间最大盘符异常：" + ex.Message);
			}
			return Path.Combine(path2, path);
		}

		// Token: 0x06000152 RID: 338 RVA: 0x000085A4 File Offset: 0x000067A4
		public bool GetIsQuitUEPlan()
		{
			bool result;
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Lenovo\\LegionZone\\config\\", RegistryKeyPermissionCheck.ReadSubTree, RegistryRights.ExecuteKey))
				{
					if (registryKey != null)
					{
						int num = (int)registryKey.GetValue("IsQuitUEPlan", -1);
						if (num == -1)
						{
							this.SetIsQuitUEPlanDefaultValue();
							result = false;
						}
						else
						{
							result = num != 0;
						}
					}
					else
					{
						result = false;
					}
				}
			}
			catch (Exception ex)
			{
				LogHelper.Log("[RegistryHelper] [GetIsQuitUEPlan] 从注册表读取否退出用户体验计划异常：" + ex.Message);
				result = false;
			}
			return result;
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00008640 File Offset: 0x00006840
		public bool GetIsGamingHelper()
		{
			return this.CheckIsGamingHelperKey();
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00008648 File Offset: 0x00006848
		public bool GetIsPerformMonitor()
		{
			bool flag = false;
			bool result;
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Lenovo\\LegionZone\\config\\", RegistryKeyPermissionCheck.ReadSubTree, RegistryRights.ExecuteKey))
				{
					if (registryKey != null)
					{
						int num = (int)registryKey.GetValue("IsPerformMonitor", -1);
						if (num == -1)
						{
							result = flag;
						}
						else
						{
							result = num != 0;
						}
					}
					else
					{
						result = flag;
					}
				}
			}
			catch (Exception ex)
			{
				LogHelper.Log("[RegistryHelper] [GetIsPerformMonitor] 从注册表读取游戏注入总开关（性能监控开关）异常：" + ex.Message);
				result = flag;
			}
			return result;
		}

		public bool GetIsPerformMonitorReal()
		{
			bool flag = false;
			bool result;
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Lenovo\\LegionZone\\config\\", RegistryKeyPermissionCheck.ReadSubTree, RegistryRights.ExecuteKey))
				{
					if (registryKey != null)
					{
						int num = (int)registryKey.GetValue("IsPerformMonitorReal", -1);
						if (num == -1)
						{
							result = flag;
						}
						else
						{
							result = num != 0;
						}
					}
					else
					{
						result = flag;
					}
				}
			}
			catch (Exception ex)
			{
				LogHelper.Log("[RegistryHelper] [GetIsPerformMonitorReal] 从注册表读取游戏注入总开关（性能监控开关）异常：" + ex.Message);
				result = flag;
			}
			return result;
		}

		public bool SetIsPerformMonitor(bool data)
		{ 
			bool result = false ;
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Lenovo\\LegionZone\\config\\", RegistryKeyPermissionCheck.ReadWriteSubTree, RegistryRights.WriteKey))
				{
					if (registryKey != null)
					{
						registryKey.SetValue("IsPerformMonitor", data ? 1 :0);
						result = true;
					}
                    else
                    {
						var registryKey2 = Registry.LocalMachine.CreateSubKey("SOFTWARE\\Lenovo\\LegionZone\\config\\");
						registryKey2.Close();
						return SetIsPerformMonitor(data);
					}
				}
			}
			catch (Exception ex)
			{
				LogHelper.Log("[RegistryHelper] [GetIsPerformMonitor] 从注册表设置游戏注入总开关（性能监控开关）异常：" + ex.Message);
			}
			return result;
		}

		public bool SetIsPerformMonitorReal(bool data)
		{
			bool result = false;
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Lenovo\\LegionZone\\config\\", RegistryKeyPermissionCheck.ReadWriteSubTree, RegistryRights.WriteKey))
				{
					if (registryKey != null)
					{
						registryKey.SetValue("IsPerformMonitorReal", data ? 1 : 0);
						result = true;
					}
					else
					{
						var registryKey2 = Registry.LocalMachine.CreateSubKey("SOFTWARE\\Lenovo\\LegionZone\\config\\");
						registryKey2.Close();
						return SetIsPerformMonitor(data);
					}
				}
			}
			catch (Exception ex)
			{
				LogHelper.Log("[RegistryHelper] [SetIsPerformMonitorReal] 从注册表设置游戏注入总开关（性能监控开关）异常：" + ex.Message);
			}
			return result;
		}


		// Token: 0x06000155 RID: 341 RVA: 0x000086E0 File Offset: 0x000068E0
		public string GetPerformMonitorDBData()
		{
			string result;
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Lenovo\\LegionZone\\config\\", RegistryKeyPermissionCheck.ReadSubTree, RegistryRights.ExecuteKey))
				{
					if (registryKey != null)
					{
						result = (string)registryKey.GetValue("MonitorSettings", "");
					}
					else
					{
						result = "";
					}
				}
			}
			catch (Exception ex)
			{
				LogHelper.Log("[RegistryHelper] [GetPerformMonitorDBData] 从注册表读取性能监控数据库配置异常：" + ex.Message);
				result = "";
			}
			return result;
		}

		public bool SetPerformMonitorDBData(string data)
		{
			bool result;
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Lenovo\\LegionZone\\config\\", RegistryKeyPermissionCheck.ReadWriteSubTree, RegistryRights.WriteKey))
				{
					if (registryKey != null)
					{
					     registryKey.SetValue("MonitorSettings", data);
						result = true;
					}
					else
					{
						var registryKey2 = Registry.LocalMachine.CreateSubKey("SOFTWARE\\Lenovo\\LegionZone\\config\\");
						registryKey2.Close();
						return SetPerformMonitorDBData(data);
					}
				}
			}
			catch (Exception ex)
			{
				LogHelper.Log("[RegistryHelper] [GetPerformMonitorDBData] 从注册表写入性能监控数据库配置异常：" + ex.Message);
				result = false;
			}
			return result;
		}


		// Token: 0x06000156 RID: 342 RVA: 0x0000876C File Offset: 0x0000696C
		public string GetAvoidTouchDBData()
		{
			string result;
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Lenovo\\LegionZone\\config\\", RegistryKeyPermissionCheck.ReadSubTree, RegistryRights.ExecuteKey))
				{
					if (registryKey != null)
					{
						result = (string)registryKey.GetValue("AvoidTouchDBChange", "");
					}
					else
					{
						result = "";
					}
				}
			}
			catch (Exception ex)
			{
				LogHelper.Log("[RegistryHelper] [GetPerformMonitorDBData] 从注册表防误触数据库配异常：" + ex.Message);
				result = "";
			}
			return result;
		}

		// Token: 0x06000157 RID: 343 RVA: 0x000087F8 File Offset: 0x000069F8
		public bool GetIsMisTouch()
		{
			bool flag = false;
			bool result;
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Lenovo\\LegionZone\\config\\", RegistryKeyPermissionCheck.ReadSubTree, RegistryRights.ExecuteKey))
				{
					if (registryKey != null)
					{
						int num = (int)registryKey.GetValue("IsAvoidTouch", -1);
						if (num == -1)
						{
							result = flag;
						}
						else
						{
							result = num != 0;
						}
					}
					else
					{
						result = flag;
					}
				}
			}
			catch (Exception ex)
			{
				LogHelper.Log("[RegistryHelper] [GetIsPerformMonitor] 从注册表读取游戏注入总开关（性能监控开关）异常：" + ex.Message);
				result = flag;
			}
			return result;
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00008890 File Offset: 0x00006A90
		private void SetIsQuitUEPlanDefaultValue()
		{
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Lenovo\\LegionZone\\config\\", RegistryKeyPermissionCheck.ReadWriteSubTree))
				{
					if (registryKey != null)
					{
						registryKey.SetValue("IsQuitUEPlan", 0, RegistryValueKind.DWord);
					}
				}
			}
			catch (Exception ex)
			{
				LogHelper.Log("[RegistryHelper] [SetIsQuitUEPlanDefaultValue] 向注册表中设置否退出用户体验计划默认值异常：" + ex.Message);
			}
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00008908 File Offset: 0x00006B08
		public bool CheckIsGamingHelperKey()
		{
			bool result;
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Lenovo\\LegionZone\\config\\", RegistryKeyPermissionCheck.ReadWriteSubTree))
				{
					if (registryKey != null)
					{
						if (registryKey.GetValueNames().FirstOrDefault((string p) => p == "IsGamingHelper") == null)
						{
							LogHelper.Log("[RegistryHelper] [CheckIsGamingHelperKey] 注册表中不存在 IsGamingHelper 键值，需要写入");
							registryKey.SetValue("IsGamingHelper", 0, RegistryValueKind.DWord);
						}
						result = (int)registryKey.GetValue("IsGamingHelper", 0) != 0;
					}
					else
					{
						LogHelper.Log("[RegistryHelper] [CheckIsGamingHelperKey] key = null");
						result = false;
					}
				}
			}
			catch (Exception ex)
			{
				LogHelper.Log("[RegistryHelper] [CheckIsGamingHelperKey] 向注册表中写入 IsGamingHelper 键值异常：" + ex.Message);
				result = false;
			}
			return result;
		}

		// Token: 0x0600015A RID: 346 RVA: 0x000089E0 File Offset: 0x00006BE0
		public bool GetIsLOEnable()
		{
			return this.CheckIsLOEnableKey();
		}

		// Token: 0x0600015B RID: 347 RVA: 0x000089E8 File Offset: 0x00006BE8
		public bool CheckIsLOEnableKey()
		{
			bool result;
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Lenovo\\LegionZone\\config\\", RegistryKeyPermissionCheck.ReadWriteSubTree))
				{
					if (registryKey != null)
					{
						if (!registryKey.GetValueNames().Contains("IsLOEnable"))
						{
							LogHelper.Log("[RegistryHelper] [CheckIsLOEnableKey] 注册表中不存在 IsLOEnable 键值，需要写入");
							registryKey.SetValue("IsLOEnable", 0, RegistryValueKind.DWord);
						}
						result = (int)registryKey.GetValue("IsLOEnable", 0) != 0;
					}
					else
					{
						LogHelper.Log("[RegistryHelper] [CheckIsLOEnableKey] 找不到 SOFTWARE\\Lenovo\\LegionZone\\config\\ 路径");
						result = false;
					}
				}
			}
			catch (Exception ex)
			{
				LogHelper.Log("[RegistryHelper] [CheckIsLOEnableKey] 向注册表中写入 IsLOEnable 键值异常：" + ex.Message);
				result = false;
			}
			return result;
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00008AA8 File Offset: 0x00006CA8
		public string GetRecoveryData()
		{
			string result;
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Lenovo\\LegionZone\\config\\", RegistryKeyPermissionCheck.ReadSubTree, RegistryRights.ExecuteKey))
				{
					if (registryKey != null)
					{
						result = (string)registryKey.GetValue("RecoveryData", "");
					}
					else
					{
						result = "";
					}
				}
			}
			catch (Exception ex)
			{
				LogHelper.Log("[RegistryHelper] [GetExternalDeviceData] 从注册表读取优化备份数据异常：" + ex.Message);
				result = "";
			}
			return result;
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00008B34 File Offset: 0x00006D34
		public void SetRecoveryData(string data)
		{
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Lenovo\\LegionZone\\config\\", RegistryKeyPermissionCheck.ReadWriteSubTree))
				{
					if (registryKey != null)
					{
						registryKey.SetValue("RecoveryData", data, RegistryValueKind.String);
					}
				}
			}
			catch (Exception ex)
			{
				LogHelper.Log("[RegistryHelper] [SetExternalDeviceData] 向注册表中更新优化备份数据异常：" + ex.Message);
			}
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00008BA4 File Offset: 0x00006DA4
		public string GetExternalDeviceData()
		{
			string result;
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Lenovo\\LegionZone\\config\\", RegistryKeyPermissionCheck.ReadSubTree, RegistryRights.ExecuteKey))
				{
					if (registryKey != null)
					{
						result = (string)registryKey.GetValue("ExternalDevice", "");
					}
					else
					{
						result = "";
					}
				}
			}
			catch (Exception ex)
			{
				LogHelper.Log("[RegistryHelper] [GetExternalDeviceData] 从注册表读取外设设置数据异常：" + ex.Message);
				result = "";
			}
			return result;
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00008C30 File Offset: 0x00006E30
		public void SetExternalDeviceData(string data)
		{
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Lenovo\\LegionZone\\config\\", RegistryKeyPermissionCheck.ReadWriteSubTree))
				{
					if (registryKey != null)
					{
						registryKey.SetValue("ExternalDevice", data, RegistryValueKind.String);
					}
				}
			}
			catch (Exception ex)
			{
				LogHelper.Log("[RegistryHelper] [SetExternalDeviceData] 向注册表中更新外设设备数据异常：" + ex.Message);
			}
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00008CA0 File Offset: 0x00006EA0
		public string GetNewVersionNumber()
		{
			string result;
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Lenovo\\LegionZone\\UpdateInfor\\", RegistryKeyPermissionCheck.ReadSubTree, RegistryRights.ExecuteKey))
				{
					if (registryKey != null)
					{
						result = (string)registryKey.GetValue("UpdateVersion", "");
					}
					else
					{
						result = "";
					}
				}
			}
			catch (Exception ex)
			{
				LogHelper.Log("[RegistryHelper] [GetNewVersionNumber] 从注册表读取新版本号异常：" + ex.Message);
				result = "";
			}
			return result;
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00008D2C File Offset: 0x00006F2C
		public string GetNewVersionUpdateInfo()
		{
			string result;
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Lenovo\\LegionZone\\UpdateInfor\\", RegistryKeyPermissionCheck.ReadSubTree, RegistryRights.ExecuteKey))
				{
					if (registryKey != null)
					{
						result = (string)registryKey.GetValue("UpdateInfo", "");
					}
					else
					{
						result = "";
					}
				}
			}
			catch (Exception ex)
			{
				LogHelper.Log("[RegistryHelper] [GetNewVersionUpdateInfo] 从注册表读取新版本的更新信息异常：" + ex.Message);
				result = "";
			}
			return result;
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00008DB8 File Offset: 0x00006FB8
		public bool GetNotified()
		{
			bool result;
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Lenovo\\LegionZone\\UpdateInfor\\", RegistryKeyPermissionCheck.ReadSubTree, RegistryRights.ExecuteKey))
				{
					if (registryKey != null)
					{
						result = (int)registryKey.GetValue("Notified", 0) != 0;
					}
					else
					{
						result = false;
					}
				}
			}
			catch (Exception ex)
			{
				LogHelper.Log("[RegistryHelper] [GetNotified] 从注册表读取是否已提示过异常：" + ex.Message);
				result = false;
			}
			return result;
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00008E44 File Offset: 0x00007044
		public void SetNotifyed(bool flag)
		{
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Lenovo\\LegionZone\\UpdateInfor\\", RegistryKeyPermissionCheck.ReadWriteSubTree))
				{
					if (registryKey != null)
					{
						registryKey.SetValue("Notified", flag ? 1 : 0, RegistryValueKind.DWord);
					}
				}
			}
			catch (Exception ex)
			{
				LogHelper.Log("[RegistryHelper] [SetNotifyed] 向注册表中写入是否已提示过异常：" + ex.Message);
			}
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00008EC0 File Offset: 0x000070C0
		public string GetOldVersion()
		{
			string result;
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Lenovo\\LegionZone\\", RegistryKeyPermissionCheck.ReadSubTree, RegistryRights.ExecuteKey))
				{
					if (registryKey != null)
					{
						result = (string)registryKey.GetValue("OldVersion", "");
					}
					else
					{
						result = string.Empty;
					}
				}
			}
			catch (Exception ex)
			{
				LogHelper.Log("[RegistryHelper] [GetOldVersion] 从注册表读取旧版本号异常：" + ex.Message);
				result = string.Empty;
			}
			return result;
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00008F4C File Offset: 0x0000714C
		public void DeleteOldVersion()
		{
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Lenovo\\LegionZone\\", true))
				{
					if (registryKey != null)
					{
						registryKey.DeleteValue("OldVersion");
					}
				}
			}
			catch (Exception ex)
			{
				LogHelper.Log("[RegistryHelper] [DeleteOldVersion] 从注册表中删除旧版本号异常：" + ex.Message);
			}
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00008FBC File Offset: 0x000071BC
		public string GetLACPath()
		{
			string result;
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Lenovo\\Legion Accessory Central\\", RegistryKeyPermissionCheck.ReadSubTree, RegistryRights.ExecuteKey))
				{
					if (registryKey != null)
					{
						result = (string)registryKey.GetValue("Path", "");
					}
					else
					{
						result = "";
					}
				}
			}
			catch (Exception ex)
			{
				LogHelper.Log("[RegistryHelper] [GetLACPath] 从注册表读取 LAC 安装路径异常：" + ex.Message);
				result = "";
			}
			return result;
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00009048 File Offset: 0x00007248
		public string GetLACVersion()
		{
			string result;
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Lenovo\\Legion Accessory Central\\", RegistryKeyPermissionCheck.ReadSubTree, RegistryRights.ExecuteKey))
				{
					if (registryKey != null)
					{
						result = (string)registryKey.GetValue("Version", "");
					}
					else
					{
						result = "";
					}
				}
			}
			catch (Exception ex)
			{
				LogHelper.Log("[RegistryHelper] [GetLACPath] 从注册表读取 LAC 版本号异常：" + ex.Message);
				result = "";
			}
			return result;
		}

		// Token: 0x06000168 RID: 360 RVA: 0x000090D4 File Offset: 0x000072D4
		public bool GetLACIsInstalled()
		{
			bool result;
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\{36136AB2-8565-4A03-90DD-197DD5AEA090}_is1", RegistryKeyPermissionCheck.ReadSubTree, RegistryRights.ExecuteKey))
				{
					if (registryKey == null)
					{
						LogHelper.Log("[RegistryHelper] [GetLACIsInstalled] LAC 未安装：在注册表中未找到 SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\{36136AB2-8565-4A03-90DD-197DD5AEA090}_is1 路径。");
						result = false;
					}
					else
					{
						string[] valueNames = registryKey.GetValueNames();
						if (valueNames == null || !valueNames.Contains("DisplayIcon"))
						{
							LogHelper.Log("[RegistryHelper] [GetLACIsInstalled] LAC 未安装：在注册表中未找到 DisplayIcon 键。");
							result = false;
						}
						else
						{
							LogHelper.Log("[RegistryHelper] [GetLACIsInstalled] LAC 已安装。");
							result = true;
						}
					}
				}
			}
			catch (Exception ex)
			{
				LogHelper.Log("[RegistryHelper] [GetLACIsInstalled] 从注册表获取 LAC 是否已安装异常：" + ex.Message);
				result = false;
			}
			return result;
		}

		// Token: 0x06000169 RID: 361 RVA: 0x0000917C File Offset: 0x0000737C
		public bool GetLACIsNeverNotify()
		{
			bool result;
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Lenovo\\LegionZone\\config\\", RegistryKeyPermissionCheck.ReadWriteSubTree))
				{
					if (registryKey == null)
					{
						LogHelper.Log("[RegistryHelper] [GetLACIsNeverNotify] key = null");
						result = false;
					}
					else
					{
						if (!registryKey.GetValueNames().Contains("LACNeverNotify"))
						{
							LogHelper.Log("[RegistryHelper] [GetLACIsNeverNotify] 注册表中不存在 LACNeverNotify 键值，需要写入，默认值是：0");
							registryKey.SetValue("LACNeverNotify", 0, RegistryValueKind.DWord);
						}
						int num = (int)registryKey.GetValue("LACNeverNotify", 0);
						LogHelper.Log(string.Format("[RegistryHelper] [GetLACIsNeverNotify] LACNeverNotifyRegKey = {0}", num));
						result = num != 0;
					}
				}
			}
			catch (Exception ex)
			{
				LogHelper.Log("[RegistryHelper] [GetLACIsNeverNotify] 向注册表中写入 LACNeverNotify 键值异常：" + ex.Message);
				result = false;
			}
			return result;
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00009254 File Offset: 0x00007454
		public void SetLACNeverNotify()
		{
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Lenovo\\LegionZone\\config\\", RegistryKeyPermissionCheck.ReadWriteSubTree))
				{
					if (registryKey == null)
					{
						LogHelper.Log("[RegistryHelper] [SetLACNeverNotify] key = null");
					}
					else
					{
						registryKey.SetValue("LACNeverNotify", 1, RegistryValueKind.DWord);
						LogHelper.Log("[RegistryHelper] [SetLACNeverNotify] 设置 LACNeverNotify 为 1，成功。");
					}
				}
			}
			catch (Exception ex)
			{
				LogHelper.Log("[RegistryHelper] [SetLACNeverNotify] 向注册表中写入 LACNeverNotify 键值异常：" + ex.Message);
			}
		}

		// Token: 0x0600016B RID: 363 RVA: 0x000092E0 File Offset: 0x000074E0
		public void SetLACInstallExternalDeviceFlag()
		{
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Lenovo\\LegionZone\\config\\", RegistryKeyPermissionCheck.ReadWriteSubTree))
				{
					if (registryKey == null)
					{
						LogHelper.Log("[RegistryHelper] [SetLACInstallExternalDeviceFlag] key = null");
					}
					else
					{
						registryKey.SetValue("InstallExternalDevice", 3, RegistryValueKind.DWord);
						LogHelper.Log("[RegistryHelper] [SetLACInstallExternalDeviceFlag] 设置 InstallExternalDevice 为 3，成功。");
					}
				}
			}
			catch (Exception ex)
			{
				LogHelper.Log("[RegistryHelper] [SetLACInstallExternalDeviceFlag] 向注册表中写入 InstallExternalDevice 键值异常：" + ex.Message);
			}
		}

		// Token: 0x0600016C RID: 364 RVA: 0x0000936C File Offset: 0x0000756C
		public void SetLODeviceName(string deviceName)
		{
			try
			{
				if (deviceName == null)
				{
					deviceName = "";
				}
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Lenovo\\LegionZone\\", RegistryKeyPermissionCheck.ReadWriteSubTree))
				{
					if (registryKey != null)
					{
						string[] subKeyNames = registryKey.GetSubKeyNames();
						if (subKeyNames == null || !subKeyNames.Contains("loinfo"))
						{
							LogHelper.Log("[RegistryHelper] [SetLODeviceName] 未找到 loinfo 键，需要创建。");
							registryKey.CreateSubKey("loinfo");
						}
					}
				}
				using (RegistryKey registryKey2 = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Lenovo\\LegionZone\\loinfo\\", RegistryKeyPermissionCheck.ReadWriteSubTree))
				{
					if (registryKey2 != null)
					{
						registryKey2.SetValue("devicename", deviceName, RegistryValueKind.String);
					}
				}
			}
			catch (Exception ex)
			{
				LogHelper.Log("[RegistryHelper] [SetLODeviceName] 向注册表中写入 LenovoOne 设备连接状态异常：status = " + deviceName + ", ex = " + ex.Message);
			}
		}

		// Token: 0x14000014 RID: 20
		// (add) Token: 0x0600016D RID: 365 RVA: 0x00009448 File Offset: 0x00007648
		// (remove) Token: 0x0600016E RID: 366 RVA: 0x00009480 File Offset: 0x00007680
		public event EventArrivedEventHandler IsLoEnableChanged;

		// Token: 0x0600016F RID: 367 RVA: 0x000094B8 File Offset: 0x000076B8
		public void StartIsLoEnableMonitor()
		{
			WindowsIdentity.GetCurrent();
			string queryOrEventClassName;
			if (Environment.Is64BitOperatingSystem)
			{
				queryOrEventClassName = "SELECT * FROM RegistryValueChangeEvent WHERE Hive='HKEY_LOCAL_MACHINE' AND KeyPath='SOFTWARE\\\\WOW6432Node\\\\Lenovo\\\\LegionZone\\\\config' AND ValueName='IsLOEnable'";
			}
			else
			{
				queryOrEventClassName = "SELECT * FROM RegistryValueChangeEvent WHERE Hive='HKEY_LOCAL_MACHINE' AND KeyPath='SOFTWARE\\\\Lenovo\\\\LegionZone\\\\config' AND ValueName='IsLOEnable'";
			}
			WqlEventQuery query = new WqlEventQuery(queryOrEventClassName);
			this.watcher = new ManagementEventWatcher(query);
			this.watcher.EventArrived += this.Watcher_EventArrived;
			this.watcher.Start();
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00009515 File Offset: 0x00007715
		private void Watcher_EventArrived(object sender, EventArrivedEventArgs e)
		{
			EventArrivedEventHandler isLoEnableChanged = this.IsLoEnableChanged;
			if (isLoEnableChanged == null)
			{
				return;
			}
			isLoEnableChanged(sender, e);
		}
		public void CheckInstallDir()
		{
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Lenovo\\LegionZone\\", RegistryKeyPermissionCheck.ReadWriteSubTree))
				{
					bool flag = false;
					if (registryKey != null)
					{
						string text = (string)registryKey.GetValue("InstallDir", "");
						if (string.IsNullOrEmpty(text))
						{
							RegistryKey registryKey2 = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Lenovo\\LegionZone\\", RegistryKeyPermissionCheck.ReadWriteSubTree, RegistryRights.WriteKey);
							registryKey2.SetValue("InstallDir", Utils.GetBasePath());
							registryKey2.SetValue("InstallVersion", "1.0.0");
							registryKey2.SetValue("Version", "1.0.0"); 
							registryKey2.Close();
						}
					}
					else {
						Registry.LocalMachine.CreateSubKey("SOFTWARE\\Lenovo\\LegionZone\\");
						CheckInstallDir();
					}

				}
			}
			catch (Exception ex)
			{
				LogHelper.Log("[RegistryHelper] [CheckNameKey] 向注册表中设置 name 值异常：" + ex.Message);
			}
		}
		
		// Token: 0x06000171 RID: 369 RVA: 0x0000952C File Offset: 0x0000772C
		public void CheckNameKey()
		{
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Lenovo\\LegionZone\\", RegistryKeyPermissionCheck.ReadWriteSubTree))
				{
					bool flag = false;
					if (registryKey != null)
					{
						string text = (string)registryKey.GetValue("name", "");
						if (text == "Legion Zone")
						{
							flag = true;
						}
						else
						{
							LogHelper.Log("[RegistryHelper] [CheckNameKey] 注册表中 name 值不正确，需要重新设置：name = " + text);
						}
						if (!flag)
						{
							registryKey.SetValue("name", "Legion Zone", RegistryValueKind.String);
							LogHelper.Log("[RegistryHelper] [CheckNameKey] 向注册表中设置 name 值成功！");
						}
					}
				}
			}
			catch (Exception ex)
			{
				LogHelper.Log("[RegistryHelper] [CheckNameKey] 向注册表中设置 name 值异常：" + ex.Message);
			}
		}

		// Token: 0x0400009E RID: 158
		public const string RootRegPath = "SOFTWARE\\Lenovo\\LegionZone\\";

		// Token: 0x0400009F RID: 159
		public const string DebugRegPath = "SOFTWARE\\Lenovo\\LegionZone\\debug\\";

		// Token: 0x040000A0 RID: 160
		public const string IsLOEWorking = "IsLoeWorking";

		// Token: 0x040000A1 RID: 161
		private const string InstallDirRegKey = "InstallDir";

		// Token: 0x040000A2 RID: 162
		private const string MainExeRegKey = "Exe";

		// Token: 0x040000A3 RID: 163
		private const string SupplyIDRegKey = "SupplyID";

		// Token: 0x040000A4 RID: 164
		private const string VersionRegKey = "Version";

		// Token: 0x040000A5 RID: 165
		private const string OldVersionRegKey = "OldVersion";

		// Token: 0x040000A6 RID: 166
		private const string EnableLogRegKey = "EnableLog";

		// Token: 0x040000A7 RID: 167
		private const string NameRegKey = "name";

		// Token: 0x040000A8 RID: 168
		private const string LZOOBERegKey = "LZOOBE";

		// Token: 0x040000A9 RID: 169
		private const string ConfigRegPath = "SOFTWARE\\Lenovo\\LegionZone\\config\\";

		// Token: 0x040000AA RID: 170
		private const string IsLZTrayAutoRunRegKey = "IsLZTrayAutoRun";

		// Token: 0x040000AB RID: 171
		private const string IsGamingBoostRegKey = "IsGamingBoost";

		// Token: 0x040000AC RID: 172
		private const string IsPerformOCModeRegKey = "IsPerformOCMode";

		// Token: 0x040000AD RID: 173
		private const string GameSnapshotHotkeyRegKey = "GameSnapshotHotkey";

		// Token: 0x040000AE RID: 174
		private const string LZToolkitHotkeyRegKey = "LZToolkitHotkey";

		// Token: 0x040000AF RID: 175
		private const string GameSnapshotPathRegKey = "GameSnapshotPath";

		// Token: 0x040000B0 RID: 176
		private const string RecoveryDataRegKey = "RecoveryData";

		// Token: 0x040000B1 RID: 177
		private const string ExternalDeviceRegKey = "ExternalDevice";

		// Token: 0x040000B2 RID: 178
		private const string IsQuitUEPlanRegKey = "IsQuitUEPlan";

		// Token: 0x040000B3 RID: 179
		private const string IsGamingHelperRegKey = "IsGamingHelper";

		// Token: 0x040000B4 RID: 180
		private const string IsPerformMonitorRegKey = "IsPerformMonitor";

		// Token: 0x040000B5 RID: 181
		private const string PerformMonitorDBRegKey = "MonitorSettings";

		// Token: 0x040000B6 RID: 182
		private const string AvoidTouchDBRegKey = "AvoidTouchDBChange";

		// Token: 0x040000B7 RID: 183
		private const string IsMisTouchRegKey = "IsAvoidTouch";

		// Token: 0x040000B8 RID: 184
		private const string LACNeverNotifyRegKey = "LACNeverNotify";

		// Token: 0x040000B9 RID: 185
		private const string LACInstallExternalDeviceRegKey = "InstallExternalDevice";

		// Token: 0x040000BA RID: 186
		private const string IsLOEnableRegKey = "IsLOEnable";

		// Token: 0x040000BB RID: 187
		private const string UpdateRegPath = "SOFTWARE\\Lenovo\\LegionZone\\UpdateInfor\\";

		// Token: 0x040000BC RID: 188
		private const string UpdateVersionRegKey = "UpdateVersion";

		// Token: 0x040000BD RID: 189
		private const string UpdateInfoRegKey = "UpdateInfo";

		// Token: 0x040000BE RID: 190
		private const string NotifiedRegKey = "Notified";

		// Token: 0x040000BF RID: 191
		private const string LegionAccessoryCentralRegPath = "SOFTWARE\\Lenovo\\Legion Accessory Central\\";

		// Token: 0x040000C0 RID: 192
		private const string LACPathRegKey = "Path";

		// Token: 0x040000C1 RID: 193
		private const string LACVersionRegKey = "Version";

		// Token: 0x040000C2 RID: 194
		private const string LACUninstallRegPath = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\{36136AB2-8565-4A03-90DD-197DD5AEA090}_is1";

		// Token: 0x040000C3 RID: 195
		private const string LACUninstallDisplayIconRegKey = "DisplayIcon";

		// Token: 0x040000C4 RID: 196
		private const string LoinfoRegPath = "loinfo";

		// Token: 0x040000C5 RID: 197
		private const string LORegPath = "SOFTWARE\\Lenovo\\LegionZone\\loinfo\\";

		// Token: 0x040000C6 RID: 198
		private const string LODeviceNameRegKey = "devicename";

		// Token: 0x040000C7 RID: 199
		private static RegistryHelper instance;

		// Token: 0x040000C8 RID: 200
		private string installDir;

		// Token: 0x040000C9 RID: 201
		private string supplyID;

		// Token: 0x040000CB RID: 203
		private ManagementEventWatcher watcher;
	}
}
