using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading; 
using FakeLegionZone.Model;
using FakeLegionZone.Plugin;
using FakeLegionZone.Util;
using Microsoft.Win32;

namespace FakeLegionZone
{
	// Token: 0x02000025 RID: 37
	public class PluginDll
	{
		// Token: 0x0600007F RID: 127
		[DllImport("LZTrayPlugin.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern bool SetNotifyFunc(PluginNotifyCallback pluginNotifyCallback);

		// Token: 0x06000080 RID: 128
		[DllImport("LZTrayPlugin.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern bool UnInit();

		// Token: 0x06000081 RID: 129
		[DllImport("LZTrayPlugin.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern bool ClearMemory(string listId);

		// Token: 0x06000082 RID: 130
		[DllImport("LZTrayPlugin.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern bool SetSmartFanMode(int ioptType, int mode);

		// Token: 0x06000083 RID: 131
		[DllImport("LZTrayPlugin.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern void SetSendHwMsg(bool isSend);

		// Token: 0x06000084 RID: 132
		[DllImport("LZTrayPlugin.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern void SetSendHwMsgForDeviceConnect(bool isSend);

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000085 RID: 133 RVA: 0x00002C7C File Offset: 0x00000E7C
		// (remove) Token: 0x06000086 RID: 134 RVA: 0x00002CB4 File Offset: 0x00000EB4
		public event GameStartedDelegate GameStartedEvent;

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000087 RID: 135 RVA: 0x00002CEC File Offset: 0x00000EEC
		// (remove) Token: 0x06000088 RID: 136 RVA: 0x00002D24 File Offset: 0x00000F24
		public event GameEndedDelegate GameEndedEvent;

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06000089 RID: 137 RVA: 0x00002D5C File Offset: 0x00000F5C
		// (remove) Token: 0x0600008A RID: 138 RVA: 0x00002D94 File Offset: 0x00000F94
		public event GameCleanMemoryCompletedDelegate GameCleanMemoryCompletedEvent;

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x0600008B RID: 139 RVA: 0x00002DCC File Offset: 0x00000FCC
		// (remove) Token: 0x0600008C RID: 140 RVA: 0x00002E04 File Offset: 0x00001004
		public event GameHardwareInfoChangedDelegate GameHardwareInfoChangedEvent;

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x0600008D RID: 141 RVA: 0x00002E3C File Offset: 0x0000103C
		// (remove) Token: 0x0600008E RID: 142 RVA: 0x00002E74 File Offset: 0x00001074
		public event GameModeChangedDelegate GameModeChangedEvent;

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x0600008F RID: 143 RVA: 0x00002EAC File Offset: 0x000010AC
		// (remove) Token: 0x06000090 RID: 144 RVA: 0x00002EE4 File Offset: 0x000010E4
		public event GameBatteryModeChangedDelegate GameBatteryModeChangedEvent;

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x06000091 RID: 145 RVA: 0x00002F1C File Offset: 0x0000111C
		// (remove) Token: 0x06000092 RID: 146 RVA: 0x00002F54 File Offset: 0x00001154
		public event GameExternalDeviceChangedDelegate GameExternalDeviceChangedEvent;

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x06000093 RID: 147 RVA: 0x00002F8C File Offset: 0x0000118C
		// (remove) Token: 0x06000094 RID: 148 RVA: 0x00002FC4 File Offset: 0x000011C4
		public event GameACPDBatteryModeChangedDelegate GameACPDBatteryModeChangedEvent;

		// Token: 0x06000095 RID: 149 RVA: 0x00002FFC File Offset: 0x000011FC
		private PluginDll()
		{
			try
			{
				this.dllFileFullPath = Path.Combine(Utils.GetBasePath(), "LZTrayPlugin.dll");
				this.PluginDllIsExistsSignatureValid();
			}
			catch (Exception ex)
			{
				LogHelper.Log("[PluginDll] [PluginDll] 构造 PluginDll 异常：" + ex.Message);
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000096 RID: 150 RVA: 0x00003060 File Offset: 0x00001260
		public static PluginDll Instance
		{
			get
			{
				if (PluginDll.instance == null)
				{
					PluginDll.instance = new PluginDll();
				}
				return PluginDll.instance;
			}
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00003078 File Offset: 0x00001278
		private void PluginDllIsExistsSignatureValid()
		{
			if (!File.Exists(this.dllFileFullPath))
			{
				LogHelper.Log("[PluginDll] [RegisterNotifyCallback] 未找到 PluginDll.dll 文件：dll_file_name = " + this.dllFileFullPath);
				this.dllIsExistsAndSignatureValid = false;
				return;
			}
			if (VerifySignature.Verify(this.dllFileFullPath))
			{
				this.dllIsExistsAndSignatureValid = true;
				return;
			}
			this.dllIsExistsAndSignatureValid = false;
		}

		// Token: 0x06000098 RID: 152 RVA: 0x000030CC File Offset: 0x000012CC
		public void RegisterNotifyCallback()
		{
			if (this.dllIsExistsAndSignatureValid)
			{
				try
				{
					this.pluginNotifyCallback = new PluginNotifyCallback(this.PluginNotify_Callback);
					LogHelper.Log("[PluginDll] [RegisterNotifyCallback] 注册 LZTrayPlugin::SetNotifyFunc 开始！");
					if (PluginDll.SetNotifyFunc(this.pluginNotifyCallback))
					{
						LogHelper.Log("[PluginDll] [RegisterNotifyCallback] 注册 LZTrayPlugin.dll 回调成功！");
					}
					else
					{
						LogHelper.Log("[PluginDll] [RegisterNotifyCallback] 注册 LZTrayPlugin.dll 回调失败！");
					}
				}
				catch (Exception ex)
				{
					LogHelper.Log("[PluginDll] [RegisterNotifyCallback] 注册 LZTrayPlugin.dll 回调异常：" + ex.Message);
				}
			}
		}

		// Token: 0x06000099 RID: 153 RVA: 0x0000314C File Offset: 0x0000134C
		public void UnInitDll()
		{
			PluginDll.UnInit();
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00003154 File Offset: 0x00001354
		public void CleanMemory()
		{
			if (this.dllIsExistsAndSignatureValid)
			{
				try
				{
					PluginDll.ClearMemory("");
				}
				catch (Exception ex)
				{
					LogHelper.Log("[PluginDll] [CleanMemory] 调用 Dll 清理内存接口异常：" + ex.Message);
				}
			}
		}

		// Token: 0x0600009B RID: 155 RVA: 0x000031A0 File Offset: 0x000013A0
		public void StartInfobarData()
		{
			if (this.dllIsExistsAndSignatureValid)
			{
				try
				{
					PluginDll.SetSendHwMsg(true);
				}
				catch (Exception ex)
				{
					LogHelper.Log("[PluginDll] [StartInfobarData] 调用开始发送 cpu 和 gpu 实时数据接口异常：" + ex.Message);
				}
			}
		}

		// Token: 0x0600009C RID: 156 RVA: 0x000031E8 File Offset: 0x000013E8
		public void StopInfobarData()
		{
			if (this.dllIsExistsAndSignatureValid)
			{
				try
				{
					PluginDll.SetSendHwMsg(false);
				}
				catch (Exception ex)
				{
					LogHelper.Log("[PluginDll] [StopInfobarData] 调用停止发送 cpu 和 gpu 实时数据接口异常：" + ex.Message);
				}
			}
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00003230 File Offset: 0x00001430
		public void StartLenovoOneData()
		{
			if (this.dllIsExistsAndSignatureValid)
			{
				try
				{
					PluginDll.SetSendHwMsgForDeviceConnect(true);
				}
				catch (Exception ex)
				{
					LogHelper.Log("[PluginDll] [StartLenovoOneData] 调用开始发送 cpu 和 gpu 实时数据接口异常：" + ex.Message);
				}
				LogHelper.Log("[PluginDll] [StartLenovoOneData] 调用开始发送 cpu 和 gpu 实时数据");
				using (RegistryKey registryKey = Registry.LocalMachine.CreateSubKey("SOFTWARE\\Lenovo\\LegionZone\\", RegistryKeyPermissionCheck.ReadWriteSubTree))
				{
					if (registryKey != null)
					{
						registryKey.SetValue("IsLoeWorking", 1);
					}
				}
			}
		}

		// Token: 0x0600009E RID: 158 RVA: 0x000032BC File Offset: 0x000014BC
		public void StopLenovoOneData()
		{
			if (this.dllIsExistsAndSignatureValid)
			{
				try
				{
					PluginDll.SetSendHwMsgForDeviceConnect(false);
				}
				catch (Exception ex)
				{
					LogHelper.Log("[PluginDll] [StopLenovoOneData] 调用停止发送 cpu 和 gpu 实时数据接口异常：" + ex.Message);
				}
				LogHelper.Log("[PluginDll] [StopLenovoOneData] 停止发送 cpu 和 gpu 实时数据");
				using (RegistryKey registryKey = Registry.LocalMachine.CreateSubKey("SOFTWARE\\Lenovo\\LegionZone\\", RegistryKeyPermissionCheck.ReadWriteSubTree))
				{
					if (registryKey != null)
					{
						registryKey.SetValue("IsLoeWorking", 0);
					}
				}
			}
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00003348 File Offset: 0x00001548
		public void ChangeMode(ChangeModeSenderType senderType, GameMode newMode)
		{
			if (this.dllIsExistsAndSignatureValid)
			{
				try
				{
					PluginDll.SetSmartFanMode((int)senderType, (int)newMode);
				}
				catch (Exception ex)
				{
					LogHelper.Log("[PluginDll] [ChangeMode] 调用修改游戏接口异常：" + ex.Message);
				}
			}
		}
		internal static uint ComputeStringHash(string s)
		{
			uint num = 0;
			if (s != null)
			{
				num = 2166136261U;
				for (int i = 0; i < s.Length; i++)
				{
					num = ((uint)s[i] ^ num) * 16777619U;
				}
			}
			return num;
		}

		private void PluginNotify_Callback(string pszCategory, string pszInputJson)
		{
			uint num = ComputeStringHash(pszCategory);
			if (num <= 947186039U)
			{
				if (num <= 469231146U)
				{
					if (num != 157105097U)
					{
						if (num != 223954650U)
						{
							if (num != 469231146U)
							{
								return;
							}
							if (!(pszCategory == "LZ_EVEVT_GAME_BATTERY_INFO"))
							{
								return;
							}
							LogHelper.Log("[PluginDll] [PluginNotify_Callback] pszCategory = " + pszCategory + ", pszInputJson = " + pszInputJson);
							this.HandleBatteryModeChangedNotifyData(pszInputJson);
							return;
						}
						else
						{
							if (!(pszCategory == "LZ_EVEVT_GAME_ENTER"))
							{
								return;
							}
							LogHelper.Log("[PluginDll] [PluginNotify_Callback] pszCategory = " + pszCategory + ", pszInputJson = " + pszInputJson);
							this.HandleGameStartedNotifyData(pszInputJson);
							return;
						}
					}
					else
					{
						if (!(pszCategory == "LZ_EVEVT_EXTERNAL_DEVICE"))
						{
							return;
						}
						LogHelper.Log("[PluginDll] [PluginNotify_Callback] pszCategory = " + pszCategory + ", pszInputJson = " + pszInputJson);
						this.HandleExternalDeviceChangedNotifyData(pszInputJson);
						return;
					}
				}
				else if (num != 802758044U)
				{
					if (num != 939758226U)
					{
						if (num != 947186039U)
						{
							return;
						}
						if (!(pszCategory == "LZ_EVEVT_GAME_SUPPORT_SMART_FAN"))
						{
							return;
						}
						LogHelper.Log("[PluginDll] [PluginNotify_Callback] pszCategory = " + pszCategory + ", pszInputJson = " + pszInputJson);
						this.HandleGameSupportSmartFanNotifyData(pszInputJson);
						return;
					}
					else
					{
						if (!(pszCategory == "LZ_EVEVT_GAME_HARDWARE_INFO_DEVICECONNECT"))
						{
							return;
						}
						if (CommandLine.CommandLineArgs.Contains("--enable-hardware-info-changed-log"))
						{
							LogHelper.Log("[PluginDll] [PluginNotify_Callback] pszCategory = " + pszCategory + ", pszInputJson = " + pszInputJson);
						}
						this.HandleHardwareInfoChangedForLenovoOneNotifyData(pszInputJson);
						return;
					}
				}
				else
				{
					if (!(pszCategory == "LZ_TELL_INFO"))
					{
						return;
					}
					LogHelper.Log("[PluginDll] [PluginNotify_Callback] pszCategory = " + pszCategory + ", pszInputJson = " + pszInputJson);
					this.HandleTellInfoNotifyData(pszInputJson);
					return;
				}
			}
			else if (num <= 2189968556U)
			{
				if (num != 1255731955U)
				{
					if (num != 1785731553U)
					{
						if (num != 2189968556U)
						{
							return;
						}
						if (!(pszCategory == "LZ_EVEVT_GAME_EXIT"))
						{
							return;
						}
						LogHelper.Log("[PluginDll] [PluginNotify_Callback] pszCategory = " + pszCategory + ", pszInputJson = " + pszInputJson);
						this.HandleGameEndedNotifyData(pszInputJson);
						return;
					}
					else
					{
						if (!(pszCategory == "LZ_EVEVT_GAME_SUPPORT_FAN_MODE"))
						{
							return;
						}
						LogHelper.Log("[PluginDll] [PluginNotify_Callback] pszCategory = " + pszCategory + ", pszInputJson = " + pszInputJson);
						this.HandleSupportedGameModeNotifyData(pszInputJson);
						return;
					}
				}
				else
				{
					if (!(pszCategory == "LZ_EVEVT_GAME_SMART_FAN_MODE"))
					{
						return;
					}
					LogHelper.Log("[PluginDll] [PluginNotify_Callback] pszCategory = " + pszCategory + ", pszInputJson = " + pszInputJson);
					this.HandleModeChangedNotifyData(pszInputJson);
					return;
				}
			}
			else if (num <= 2766238641U)
			{
				if (num != 2570223547U)
				{
					if (num != 2766238641U)
					{
						return;
					}
					if (!(pszCategory == "LZ_EVEVT_GAME_HARDWARE_INFO"))
					{
						return;
					}
					if (CommandLine.CommandLineArgs.Contains("--enable-hardware-info-changed-log"))
					{
						LogHelper.Log("[PluginDll] [PluginNotify_Callback] pszCategory = " + pszCategory + ", pszInputJson = " + pszInputJson);
					}
					this.HandleHardwareInfoChangedNotifyData(pszInputJson);
					return;
				}
				else
				{
					if (!(pszCategory == "LZ_FN_REPORT"))
					{
						return;
					}
					LogHelper.Log("[PluginDll] [PluginNotify_Callback] pszCategory = " + pszCategory + ", pszInputJson = " + pszInputJson);
					this.HandleLudpNotifyData(pszInputJson);
					return;
				}
			}
			else if (num != 2921127240U)
			{
				if (num != 3675980264U)
				{
					return;
				}
				if (!(pszCategory == "LZ_EVEVT_CLEAN_MEMORY"))
				{
					return;
				}
				LogHelper.Log("[PluginDll] [PluginNotify_Callback] pszCategory = " + pszCategory + ", pszInputJson = " + pszInputJson);
				this.HandleCleanMemoryCompletedNotifyData(pszInputJson);
				return;
			}
			else
			{
				if (!(pszCategory == "LZ_EVEVT_PDAC_STATUS"))
				{
					return;
				}
				LogHelper.Log("[PluginDll] [PluginNotify_Callback] pszCategory = " + pszCategory + ", pszInputJson = " + pszInputJson);
				this.HandleACPDBatteryModeChangedNotifyData(pszInputJson);
				return;
			}
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x000036D0 File Offset: 0x000018D0
		private void HandleGameStartedNotifyData(string data)
		{
			new Thread(delegate ()
			{
				GameStartedAndEndedReceivedData gameStartedAndEndedReceivedData = JsonHelper.StringToObject<GameStartedAndEndedReceivedData>(data);
				if (gameStartedAndEndedReceivedData == null)
				{
					return;
				}
				GameInfo gameInfo = new GameInfo
				{
					ProcessId = gameStartedAndEndedReceivedData.ProcessId,
					ProcessName = gameStartedAndEndedReceivedData.ProcessName,
					GameName = gameStartedAndEndedReceivedData.GameName,
					Is64 = (gameStartedAndEndedReceivedData.IsProcess64 == 1),
					SupportInjected = (gameStartedAndEndedReceivedData.SupportInjected == 1),
					ShowAssistant = (gameStartedAndEndedReceivedData.ShowAssistant == 1),
					ChangeToPerformance = (gameStartedAndEndedReceivedData.ChangeToPerformance == 1)
				};
				GameStartedDelegate gameStartedEvent = this.GameStartedEvent;
				if (gameStartedEvent == null)
				{
					return;
				}
				gameStartedEvent(this, gameInfo);
			}).Start();
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x000036FA File Offset: 0x000018FA
		private void HandleGameEndedNotifyData(string data)
		{
			new Thread(delegate ()
			{
				GameStartedAndEndedReceivedData gameStartedAndEndedReceivedData = JsonHelper.StringToObject<GameStartedAndEndedReceivedData>(data);
				if (gameStartedAndEndedReceivedData == null)
				{
					return;
				}
				GameInfo gameInfo = new GameInfo
				{
					ProcessId = gameStartedAndEndedReceivedData.ProcessId,
					ProcessName = gameStartedAndEndedReceivedData.ProcessName,
					GameName = gameStartedAndEndedReceivedData.GameName,
					Is64 = (gameStartedAndEndedReceivedData.IsProcess64 == 1),
					SupportInjected = (gameStartedAndEndedReceivedData.SupportInjected == 1),
					ShowAssistant = (gameStartedAndEndedReceivedData.ShowAssistant == 1)
				};
				GameEndedDelegate gameEndedEvent = this.GameEndedEvent;
				if (gameEndedEvent == null)
				{
					return;
				}
				gameEndedEvent(this, gameInfo);
			}).Start();
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00003724 File Offset: 0x00001924
		private void HandleCleanMemoryCompletedNotifyData(string data)
		{
			new Thread(delegate ()
			{
				GameCleanMemoryCompletedReceivedData gameCleanMemoryCompletedReceivedData = JsonHelper.StringToObject<GameCleanMemoryCompletedReceivedData>(data);
				if (gameCleanMemoryCompletedReceivedData == null)
				{
					return;
				}
				GameCleanMemoryCompletedDelegate gameCleanMemoryCompletedEvent = this.GameCleanMemoryCompletedEvent;
				if (gameCleanMemoryCompletedEvent == null)
				{
					return;
				}
				gameCleanMemoryCompletedEvent(this, gameCleanMemoryCompletedReceivedData);
			}).Start();
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x0000374E File Offset: 0x0000194E
		private void HandleModeChangedNotifyData(string data)
		{
			new Thread(delegate ()
			{
				GameModeChangedReceivedData gameModeChangedReceivedData = JsonHelper.StringToObject<GameModeChangedReceivedData>(data);
				if (gameModeChangedReceivedData == null)
				{
					return;
				}
				//if (LenovoOne.IsSupported())
				//{
				//	LenovoOne.Instance.OnGameModeChanged(gameModeChangedReceivedData.SmartFanMode);
				//}
				GameModeChangedDelegate gameModeChangedEvent = this.GameModeChangedEvent;
				if (gameModeChangedEvent == null)
				{
					return;
				}
				gameModeChangedEvent(this, gameModeChangedReceivedData.SmartFanMode, gameModeChangedReceivedData.Executor);
			}).Start();
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00003778 File Offset: 0x00001978
		private void HandleHardwareInfoChangedNotifyData(string data)
		{
			new Thread(delegate ()
			{
				GameHardwareInfoChangedReceivedData gameHardwareInfoChangedReceivedData = JsonHelper.StringToObject<GameHardwareInfoChangedReceivedData>(data);
				if (gameHardwareInfoChangedReceivedData == null)
				{
					return;
				}
				GameHardwareInfoChangedDelegate gameHardwareInfoChangedEvent = this.GameHardwareInfoChangedEvent;
				if (gameHardwareInfoChangedEvent == null)
				{
					return;
				}
				gameHardwareInfoChangedEvent(this, gameHardwareInfoChangedReceivedData);
			}).Start();
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x000037A2 File Offset: 0x000019A2
		private void HandleBatteryModeChangedNotifyData(string data)
		{
			new Thread(delegate ()
			{
				GameBatteryModeChangedReceivedData gameBatteryModeChangedReceivedData = JsonHelper.StringToObject<GameBatteryModeChangedReceivedData>(data);
				if (gameBatteryModeChangedReceivedData == null)
				{
					return;
				}
				GameBatteryModeChangedDelegate gameBatteryModeChangedEvent = this.GameBatteryModeChangedEvent;
				if (gameBatteryModeChangedEvent == null)
				{
					return;
				}
				gameBatteryModeChangedEvent(this, gameBatteryModeChangedReceivedData);
			}).Start();
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x000037CC File Offset: 0x000019CC
		private void HandleACPDBatteryModeChangedNotifyData(string data)
		{
			new Thread(delegate ()
			{
				GameACPDModeChangedReceivedData gameACPDModeChangedReceivedData = JsonHelper.StringToObject<GameACPDModeChangedReceivedData>(data);
				if (gameACPDModeChangedReceivedData == null)
				{
					return;
				}
				GameACPDBatteryModeChangedDelegate gameACPDBatteryModeChangedEvent = this.GameACPDBatteryModeChangedEvent;
				if (gameACPDBatteryModeChangedEvent == null)
				{
					return;
				}
				gameACPDBatteryModeChangedEvent(this, gameACPDModeChangedReceivedData);
			}).Start();
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x000037F6 File Offset: 0x000019F6
		private void HandleExternalDeviceChangedNotifyData(string data)
		{
			new Thread(delegate ()
			{
				GameExternalDeviceChangedReceivedData gameExternalDeviceChangedReceivedData = JsonHelper.StringToObject<GameExternalDeviceChangedReceivedData>(data);
				if (gameExternalDeviceChangedReceivedData == null)
				{
					return;
				}
				GameExternalDeviceChangedDelegate gameExternalDeviceChangedEvent = this.GameExternalDeviceChangedEvent;
				if (gameExternalDeviceChangedEvent == null)
				{
					return;
				}
				gameExternalDeviceChangedEvent(this, gameExternalDeviceChangedReceivedData);
			}).Start();
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00003820 File Offset: 0x00001A20
		private void HandleSupportedGameModeNotifyData(string data)
		{
			new Thread(delegate ()
			{
				GameSupportedGameModeReceivedData gameSupportedGameModeReceivedData = JsonHelper.StringToObject<GameSupportedGameModeReceivedData>(data);
				if (gameSupportedGameModeReceivedData == null || gameSupportedGameModeReceivedData.SupportThermalMode == null)
				{
					return;
				}
				LogHelper.Log("[PluginDll] [HandleSupportedGameModeNotifyData] 收到本机支持的游戏模式回调：modes = " + data);
				foreach (int num in gameSupportedGameModeReceivedData.SupportThermalMode)
				{
					if (Enum.IsDefined(typeof(GameMode), num))
					{
						GlobalCurrentStatus.SupportedGameMode.Add((GameMode)num);
					}
				}
			}).Start();
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00003844 File Offset: 0x00001A44
		private void HandleGameSupportSmartFanNotifyData(string data)
		{
			GameSupportSmartFanReceivedData gameSupportSmartFanReceivedData = JsonHelper.StringToObject<GameSupportSmartFanReceivedData>(data);
			if (((gameSupportSmartFanReceivedData != null && gameSupportSmartFanReceivedData.IsSupportSmartFan == SmartFanMode.ThermalMode_4) || (gameSupportSmartFanReceivedData != null && gameSupportSmartFanReceivedData.IsSupportSmartFan == SmartFanMode.ThermalMode_5)) && gameSupportSmartFanReceivedData != null && gameSupportSmartFanReceivedData.MType == MType.NB)
			{
				GlobalCurrentStatus.IsSupportSmartFan = true;
			}
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00003884 File Offset: 0x00001A84
		private void HandleHardwareInfoChangedForLenovoOneNotifyData(string data)
		{
			if (!LenovoOne.IsSupported())
			{
				return;
			}
			new Thread(delegate ()
			{
				LenovoOne.Instance.SendHardwareToLenovoOneDevice(data);
			}).Start();
		}

		// Token: 0x060000AC RID: 172 RVA: 0x000038BC File Offset: 0x00001ABC
		private void HandleLudpNotifyData(string data)
		{
			try
			{
				LudpReceivedData ludpReceivedData = JsonHelper.StringToObject<LudpReceivedData>(data);
				if (ludpReceivedData != null && !string.IsNullOrEmpty(ludpReceivedData.category) && !string.IsNullOrEmpty(ludpReceivedData.action))
				{
					if (ludpReceivedData.label == null)
					{
						ludpReceivedData.label = "";
					}
					Dictionary<string, string> dictionary = null;
					if (ludpReceivedData.param != null && ludpReceivedData.param.Count > 0)
					{
						dictionary = new Dictionary<string, string>();
						foreach (LudpParam ludpParam in ludpReceivedData.param)
						{
							dictionary.Add(ludpParam.key, ludpParam.value);
						}
					}
					LudpHelper.TrackEvent(ludpReceivedData.category, ludpReceivedData.action, ludpReceivedData.label, dictionary);
				}
			}
			catch (Exception ex)
			{
				LogHelper.Log("[PluginDll] [HandleLudpNotifyData] LZTrayPlugin 数据打点异常：data = " + data + ", ex = " + ex.Message);
			}
		}

		// Token: 0x060000AD RID: 173 RVA: 0x000039BC File Offset: 0x00001BBC
		private void HandleTellInfoNotifyData(string data)
		{
			GameTellInfoReceivedData gameTellInfoReceivedData = JsonHelper.StringToObject<GameTellInfoReceivedData>(data);
			if (gameTellInfoReceivedData != null)
			{
				GlobalCurrentStatus.IsMLSupport = gameTellInfoReceivedData.MLSupport == 1;
			}
		}

		// Token: 0x0400005F RID: 95
		private string dllFileFullPath = "";

		// Token: 0x04000060 RID: 96
		private const string dllFileName = "LZTrayPlugin.dll";

		// Token: 0x04000061 RID: 97
		private bool dllIsExistsAndSignatureValid;

		// Token: 0x04000062 RID: 98
		private const string LZ_EVEVT_GAME_ENTER = "LZ_EVEVT_GAME_ENTER";

		// Token: 0x04000063 RID: 99
		private const string LZ_EVEVT_GAME_EXIT = "LZ_EVEVT_GAME_EXIT";

		// Token: 0x04000064 RID: 100
		private const string LZ_EVEVT_CLEAN_MEMORY = "LZ_EVEVT_CLEAN_MEMORY";

		// Token: 0x04000065 RID: 101
		private const string LZ_EVEVT_GAME_SMART_FAN_MODE = "LZ_EVEVT_GAME_SMART_FAN_MODE";

		// Token: 0x04000066 RID: 102
		private const string LZ_EVEVT_GAME_HARDWARE_INFO = "LZ_EVEVT_GAME_HARDWARE_INFO";

		// Token: 0x04000067 RID: 103
		private const string LZ_EVEVT_GAME_BATTERY_INFO = "LZ_EVEVT_GAME_BATTERY_INFO";

		// Token: 0x04000068 RID: 104
		private const string LZ_EVEVT_EXTERNAL_DEVICE = "LZ_EVEVT_EXTERNAL_DEVICE";

		// Token: 0x04000069 RID: 105
		private const string LZ_EVEVT_GAME_SUPPORT_FAN_MODE = "LZ_EVEVT_GAME_SUPPORT_FAN_MODE";

		// Token: 0x0400006A RID: 106
		private const string LZ_EVEVT_GAME_SUPPORT_SMART_FAN = "LZ_EVEVT_GAME_SUPPORT_SMART_FAN";

		// Token: 0x0400006B RID: 107
		private const string LZ_EVEVT_GAME_HARDWARE_INFO_DEVICECONNECT = "LZ_EVEVT_GAME_HARDWARE_INFO_DEVICECONNECT";

		// Token: 0x0400006C RID: 108
		private const string LZ_EVEVT_PDAC_STATUS = "LZ_EVEVT_PDAC_STATUS";

		// Token: 0x0400006D RID: 109
		private const string LZ_FN_REPORT = "LZ_FN_REPORT";

		// Token: 0x0400006E RID: 110
		private const string LZ_TELL_INFO = "LZ_TELL_INFO";

		// Token: 0x0400006F RID: 111
		private PluginNotifyCallback pluginNotifyCallback;

		// Token: 0x04000070 RID: 112
		private static PluginDll instance;
	}
}
