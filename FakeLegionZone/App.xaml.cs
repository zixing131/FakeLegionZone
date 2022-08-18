using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using FakeLegionZone.Common;
using FakeLegionZone.Model;
using FakeLegionZone.Plugin;
using FakeLegionZone.Util;
using Hardcodet.Wpf.TaskbarNotification;
using Microsoft.Win32;

namespace FakeLegionZone
{
	// Token: 0x02000028 RID: 40
	public partial class App : Application, IComponentConnector
	{
		public static App self;
		// Token: 0x060000B9 RID: 185 RVA: 0x00003C84 File Offset: 0x00001E84
		protected override void OnStartup(StartupEventArgs e)
		{
			self = this; 

			LogHelper.Log("[App] [OnStartup] LZTray 启动。");
			var isrealenable = RegistryHelper.Instance.GetIsPerformMonitorReal();
			RegistryHelper.Instance.SetIsPerformMonitor(isrealenable);

			using (RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Lenovo\\LegionZone\\", RegistryKeyPermissionCheck.ReadWriteSubTree, RegistryRights.FullControl))
			{
				if (registryKey != null)
				{
					string text = string.Empty;
					object value = registryKey.GetValue("LaunchDetail");
					if (value != null)
					{
						text = value.ToString();
					}
					text = text + "TRAY_StartUP=" + Environment.TickCount.ToString() + ";";
					registryKey.SetValue("LaunchDetail", text);
				}
			}
			base.DispatcherUnhandledException += this.App_DispatcherUnhandledException;
			AppDomain.CurrentDomain.UnhandledException += this.CurrentDomain_UnhandledException;
			Directory.SetCurrentDirectory(Utils.GetBasePath());
			this.HandleArgs(e.Args);
			if (this.IsRunning())
			{
				LogHelper.Log("[App] [OnStartup] LZTray 已有实例在运行，退出。");
				if (CommandLine.CommandLineArgs.Contains("--lzmain"))
				{
					this.SendMessageToSelf();
				}
				this.isStopLenovoOneService = false;
				Application.Current.Shutdown();
				return;
			}
			LogHelper.Log("[App] [OnStartup] 没有正在运行的实例，正常启动。");
			this.listStartedGame = new List<GameInfo>();
			this.InitTray();
			LogHelper.Log("[App] [OnStartup] 不延时初始化 Message 和 PluginDll。");
			this.InitMessage();
			LogHelper.Log("[App] [OnStartup] InitMessage End");
			using (RegistryKey registryKey2 = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Lenovo\\LegionZone\\", RegistryKeyPermissionCheck.ReadWriteSubTree, RegistryRights.FullControl))
			{
				if (registryKey2 != null)
				{
					string text2 = string.Empty;
					object value2 = registryKey2.GetValue("LaunchDetail");
					if (value2 != null)
					{
						text2 = value2.ToString();
					}
					text2 = text2 + "TRAY_InitTrayPlugin_Begin=" + Environment.TickCount.ToString() + ";";
					registryKey2.SetValue("LaunchDetail", text2);
				}
			}
			this.InitPluginDll();
			LogHelper.Log("[App] [OnStartup] InitPluginDll End");
			using (RegistryKey registryKey3 = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Lenovo\\LegionZone\\", RegistryKeyPermissionCheck.ReadWriteSubTree, RegistryRights.FullControl))
			{
				if (registryKey3 != null)
				{
					string text3 = string.Empty;
					object value3 = registryKey3.GetValue("LaunchDetail");
					if (value3 != null)
					{
						text3 = value3.ToString();
					}
					text3 = text3 + "TRAY_InitDlls_Begin=" + Environment.TickCount.ToString() + ";";
					registryKey3.SetValue("LaunchDetail", text3);
				}
			}
			this.InitPluginDlls();
			LogHelper.Log("[App] [OnStartup] InitPluginDlls End");
			using (RegistryKey registryKey4 = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Lenovo\\LegionZone\\", RegistryKeyPermissionCheck.ReadWriteSubTree, RegistryRights.FullControl))
			{
				if (registryKey4 != null)
				{
					string text4 = string.Empty;
					object value4 = registryKey4.GetValue("LaunchDetail");
					if (value4 != null)
					{
						text4 = value4.ToString();
					}
					text4 = text4 + "TRAY_InitAutoReport_Begin=" + Environment.TickCount.ToString() + ";";
					registryKey4.SetValue("LaunchDetail", text4);
				}
			}
			this.InitAutoReport();
			LogHelper.Log("[App] [OnStartup] InitAutoReport End");
			Optimize.Instance.RecoveryDataForExceptionExit();
			this.DeleteOldVersion();
			this.InitUpdate();
			if (CommandLine.CommandLineArgs.Contains("--lzmain"))
			{
				LogHelper.Log("[App] [OnStartup] 查找到命令行参数，启动 LZMain 主程序。");
				this.OpenLegionZone(LegionZoneFromType.unknow);
				LogHelper.Log("[App] [OnStartup] 启动 LZMain 主程序 end");
			}
			base.OnStartup(e);
			LudpHelper.AutoReport();
			this.CheckRegistry();
			if (LenovoOne.IsSupported())
			{
				LenovoOne.Instance.Init();
			}
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00003FE8 File Offset: 0x000021E8
		protected override void OnExit(ExitEventArgs e)
		{
			if (this.notifyIcon != null)
			{
				this.notifyIcon = null;
			}
			if (this.updateTimer != null)
			{
				this.updateTimer.Stop();
			}
			if (this.autoReportTimer != null)
			{
				this.updateTimer.Stop();
			}
			if (LenovoOne.IsSupported() && this.isStopLenovoOneService)
			{
				LenovoOne.Instance.Stop();
			}
			if (App.mutex != null)
			{
				try
				{
					App.mutex.Close();
				}
				catch (Exception ex)
				{
					LogHelper.Log("[App] [OnExit] 关闭 mutex 时异常：ex = " + ex.Message + "。");
				}
			}
			LogHelper.Log("[App] [OnExit] LZTray 退出。");
			base.OnExit(e);
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00004094 File Offset: 0x00002294
		private bool IsRunning()
		{
			bool flag = false;
			try
			{
				App.mutex = new Mutex(true, "FakeLegionZone{09B964CF-5E60-4388-A4CA-79CFB6432ADD}", out flag);
			}
			catch (Exception ex)
			{
				LogHelper.Log("[App] [IsRunning] 检查 LZTray 单例时异常：ex = " + ex.Message + "。");
			}
			if (flag)
			{
				if ((from p in Process.GetProcesses()
					 where p.ProcessName == "LZTray"
					 select p).Count<Process>() > 1)
				{
					flag = false;
				}
			}
			return !flag;
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00004120 File Offset: 0x00002320
		private void HandleArgs(string[] args)
		{
			CommandLine.CommandLineArgs = args.ToList<string>();
		}

		// Token: 0x060000BD RID: 189 RVA: 0x0000412D File Offset: 0x0000232D
		private void CheckRegistry()
		{

			RegistryHelper.Instance.CheckInstallDir();
			RegistryHelper.Instance.CheckNameKey();
			RegistryHelper.Instance.CheckIsGamingHelperKey();
			RegistryHelper.Instance.DeleteOOBEKey();
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00004150 File Offset: 0x00002350
		public void SendMessageToSelf()
		{
			IntPtr intPtr = NativesApi.FindWindow(null, "FakeLegionZone");
			if (intPtr != IntPtr.Zero)
			{
				LogHelper.Log("[App] [OnStartup] 需要向已存在的实例发送消息，退出。");
				string data = "empty";
				if (CommandLine.CommandLineArgs.Count > 0)
				{
					data = JsonHelper.ObjectToString(CommandLine.CommandLineArgs);
				}
				LZ_MSG_SELF_RUN_LZMAIN structure = new LZ_MSG_SELF_RUN_LZMAIN
				{
					Data = data
				};
				int num = Marshal.SizeOf<LZ_MSG_SELF_RUN_LZMAIN>(structure);
				IntPtr intPtr2 = Marshal.AllocHGlobal(num);
				Marshal.StructureToPtr<LZ_MSG_SELF_RUN_LZMAIN>(structure, intPtr2, true);
				CopyDataStruct copyDataStruct;
				copyDataStruct.dwData = 99;
				copyDataStruct.lpData = intPtr2;
				copyDataStruct.cbData = num;
				NativesApi.SendMessage(intPtr, 74, IntPtr.Zero, ref copyDataStruct);
			}
		}

		// Token: 0x060000BF RID: 191 RVA: 0x000041F0 File Offset: 0x000023F0
		private void OpenLegionZone(LegionZoneFromType from)
		{
			try
			{
				string text = Path.Combine(Utils.GetBasePath(), "LegionZone.exe");
				LogHelper.Log("[App] [OnStartup] VerifySignature Legionzone Begin");
				if (!VerifySignature.Verify(text))
				{
					LogHelper.Log("[App] [OpenLegionZone] 验证签名失败：file_path = " + text);
				}
				else
				{
					LogHelper.Log("[App] [OnStartup] VerifySignature Legionzone End");
					if (!string.IsNullOrEmpty(text) && File.Exists(text))
					{
						string arguments = " --none";
						if (from != LegionZoneFromType.tray)
						{
							if (from == LegionZoneFromType.unknow)
							{
								StringBuilder stringBuilder = new StringBuilder();
								foreach (string arg in CommandLine.CommandLineArgs)
								{
									stringBuilder.AppendFormat(" {0}", arg);
								}
								arguments = stringBuilder.ToString();
							}
						}
						else
						{
							arguments = string.Format(" --from={0}", LegionZoneFromType.tray);
						}
						Process.Start(text, arguments);
					}
					else
					{
						LogHelper.Log("[App] [OpenLZMain] 未找到 LZMain 主程序：" + text);
					}
				}
			}
			catch (Exception ex)
			{
				LogHelper.Log("[App] [OpenLZMain] 启动 LZMain 主程序异常：ex = " + ex.Message);
			}
		} 
		// Token: 0x060000C2 RID: 194 RVA: 0x000043E4 File Offset: 0x000025E4
		private void InitPluginDll()
		{
			PluginDll.Instance.GameStartedEvent += this.PluginDll_GameStartedEvent;
			PluginDll.Instance.GameEndedEvent += this.PluginDll_GameEndedEvent;
			PluginDll.Instance.GameCleanMemoryCompletedEvent += this.PluginDll_CleanMemoryCompletedEvent;
			PluginDll.Instance.GameHardwareInfoChangedEvent += this.Instance_GameHardwareInfoChangedEvent;
			PluginDll.Instance.GameModeChangedEvent += this.Instance_GameModeChangedEvent;
			PluginDll.Instance.GameBatteryModeChangedEvent += this.Instance_GameBatteryModeChangedEvent;
			PluginDll.Instance.GameExternalDeviceChangedEvent += this.Instance_GameExternalDeviceChangedEvent;
			PluginDll.Instance.GameACPDBatteryModeChangedEvent += this.Instance_GameACPDBatteryModeChangedEvent;
			PluginDll.Instance.RegisterNotifyCallback();
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x000044AC File Offset: 0x000026AC
		private void PluginDll_GameStartedEvent(object sender, GameInfo gameInfo)
		{
			LogHelper.Log(string.Format("[App] [PluginDll_GameStartedEvent] 收到游戏启动事件：process_id = {0} \t name = {1} \t is64 = {2} \t message_handler_count = {3} \t main_window_title = {4}", new object[]
			{
				gameInfo.ProcessId,
				gameInfo.ProcessName,
				gameInfo.Is64,
				gameInfo.MessageHandlerCollection.Count,
				gameInfo.MainWindowTitle
			}));
			this.HandleGameStartEvent(gameInfo);
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00004518 File Offset: 0x00002718
		private async void PluginDll_GameEndedEvent(object sender, GameInfo gameInfo)
		{
			LogHelper.Log(string.Format("[App] [PluginDll_GameEndedEvent] 收到游戏结束事件：process_id = {0} \t name = {1} \t is64 = {2}", gameInfo.ProcessId, gameInfo.ProcessName, gameInfo.Is64));
			await this.HandleGameEndEvent(gameInfo);
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00004558 File Offset: 0x00002758
		private void PluginDll_CleanMemoryCompletedEvent(object sender, GameCleanMemoryCompletedReceivedData clearMemory)
		{
			LogHelper.Log(string.Format("[App] [PluginDll_CleanMemoryCompletedEvent] 收到清理内存结束事件：clearMemorySize = {0} \t dram = {1}", clearMemory.clearMemorySize, clearMemory.Dram));
			Message message = this.message;
			if (message == null)
			{
				return;
			}
			message.SendCleanResultMessage(clearMemory.clearMemorySize, clearMemory.Dram);
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x000045A6 File Offset: 0x000027A6
		private void Instance_GameBatteryModeChangedEvent(object sender, GameBatteryModeChangedReceivedData batteryMode)
		{
			LogHelper.Log(string.Format("[App] [Instance_GameBatteryModeChangedEvent] 收到电源模式改变事件：power_mode = {0} \t battery_remaining = {1}", batteryMode.PowerMode, batteryMode.BatteryRemaining));
			this.HandleGameBatteryModeEvent(batteryMode);
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x000045D4 File Offset: 0x000027D4
		private void Instance_GameModeChangedEvent(object sender, GameMode newMode, ChangeModeSenderType senderType)
		{
			LogHelper.Log(string.Format("[App] [Instance_GameModeChangedEvent] 收到游戏模式改变事件：new_game_mode = {0} \t sender_type = {1}", newMode, senderType));
			this.HandleGameModeChangedEvent(newMode, senderType);
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x000045F9 File Offset: 0x000027F9
		private void Instance_GameHardwareInfoChangedEvent(object sender, GameHardwareInfoChangedReceivedData hardwareInfo)
		{
			this.HandleGameHardwareInfoChangedEvent(hardwareInfo);
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00004602 File Offset: 0x00002802
		private void Instance_GameExternalDeviceChangedEvent(object sender, GameExternalDeviceChangedReceivedData externalDeviceInfo)
		{
			LogHelper.Log(string.Format("[App] [Instance_GameExternalDeviceChangedEvent] 收到外设改变事件：action = {0}", externalDeviceInfo.action));
			this.HandleGameExternalDeviceChangedEvent(externalDeviceInfo);
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00004625 File Offset: 0x00002825
		private void Instance_GameACPDBatteryModeChangedEvent(object sender, GameACPDModeChangedReceivedData acpdMode)
		{
			LogHelper.Log(string.Format("[App] [Instance_GameACPDBatteryModeChangedEvent] 收到 AC PD 电源改变事件：mode = {0}, status = {1}", acpdMode.PowerMode, acpdMode.Status));
			//this.ShowACPDBalloon(acpdMode.PowerMode, acpdMode.Status);
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00004660 File Offset: 0x00002860
		private string GetDetailsJsonStr()
		{
			DetailsInfo detailsInfo = new DetailsInfo();
			detailsInfo.support_model = (GlobalCurrentStatus.IsMLSupport ? "2" : "1");
			detailsInfo.perf_major = (RegistryHelper.Instance.GetIsPerformOCMode() ? "1" : "0");
			detailsInfo.game_boost = (RegistryHelper.Instance.GetIsGamingBoost() ? "1" : "0");
			if (RegistryHelper.Instance.GetIsPerformMonitor())
			{
				string text = RegistryHelper.Instance.GetPerformMonitorDBData().Replace("\\", "");
				if (!"".Equals(text))
				{
					if (text.Contains("9"))
					{
						text = text.Replace("9", "3");
					}
					else if (text.Contains("20"))
					{
						text = text.Replace("20", "2");
					}
					else if (text.Contains("48"))
					{
						text = text.Replace("48", "1");
					}
				}
				detailsInfo.perf_monitor = text;
			}
			else
			{
				detailsInfo.perf_monitor = "0";
			}
			if (RegistryHelper.Instance.GetIsMisTouch())
			{
				string text2 = RegistryHelper.Instance.GetAvoidTouchDBData().Replace("\\", "");
				try
				{
					if (text2.StartsWith("{\"Value\":"))
					{
						text2 = text2.Substring(9, text2.Length - 10);
					}
				}
				catch (Exception ex)
				{
					LogHelper.Log("[APP] [GetDetailsJsonStr] 获取打点，details字段信息异常：" + ex.Message);
				}
				detailsInfo.antimistouch = text2;
			}
			else
			{
				detailsInfo.antimistouch = "0";
			}
			return JsonHelper.ObjectToString(detailsInfo);
		}

		// Token: 0x060000CC RID: 204 RVA: 0x000047FC File Offset: 0x000029FC
		private void HandleGameStartEvent(GameInfo gameInfo)
		{
			string detailsJsonStr = this.GetDetailsJsonStr();
			LogHelper.Log("[APP] [HandleGameStartEvent] GetDetailsJsonStr : " + detailsJsonStr);
			Dictionary<string, string> dicParams = new Dictionary<string, string>
			{
				{ "gameName", gameInfo.GameName },
				{ "processName", gameInfo.ProcessName },
				{ "details", detailsJsonStr }
			};
			LudpHelper.TrackEvent("LZ_gameAssistant", "LZ_gameStarting", "", dicParams);
			this.listStartedGame.Add(gameInfo);
			LogHelper.Log(string.Format("[App] [HandleGameStartEvent] 已添加到列表中，列表中现有 {0} 个游戏：process_id = {1}, support_injecte = {2}", this.listStartedGame.Count, gameInfo.ProcessId, gameInfo.SupportInjected));
			if (GlobalCurrentStatus.InGaming)
			{
				try
				{
					if (this.tokenSource != null)
					{
						this.tokenSource.Cancel();
						this.tokenSource = null;
					}
					goto IL_15B;
				}
				catch (Exception ex)
				{
					LogHelper.Log(string.Format("[App] [HandleGameStartEvent] tokenSource 异常：process_id = {0}, ex = {1}", gameInfo.ProcessId, ex.Message));
					goto IL_15B;
				}
			}
			GlobalCurrentStatus.InGaming = true;
			//if (gameInfo.ShowAssistant && RegistryHelper.Instance.GetIsGamingBoost())
			//{
			//	base.Dispatcher.Invoke(delegate ()
			//	{
			//		this.CheckNotifyInit();
			//		GameAccelerateBalloon gameAccelerateBalloon = new GameAccelerateBalloon();
			//		gameAccelerateBalloon.KillProcessReleaseMemoryCompletedEvent += this.GameAccelerateBalloon_KillProcessReleaseMemoryCompletedEvent;
			//		gameAccelerateBalloon.ListStartedGame = this.listStartedGame;
			//		gameAccelerateBalloon.CurrentGameInfo = gameInfo;
			//		this.notifyIcon.ShowCustomBalloon(gameAccelerateBalloon, PopupAnimation.Slide, null);
			//	});
			//}
			//else
			//{
			//	Optimize.Instance.AccelerateBackground();
			//}
		IL_15B:
			if (gameInfo.SupportInjected)
			{
				LogHelper.Log(string.Format("[App] [HandleGameStartEvent] 准备开始注入：process_id = {0}", gameInfo.ProcessId));
				this.Inject(gameInfo);
			}
		}

		// Token: 0x060000CD RID: 205 RVA: 0x000049AC File Offset: 0x00002BAC
		private async Task HandleGameEndEvent(GameInfo gameInfo)
		{
			if (this.listStartedGame.Count > 0)
			{
				GameInfo gameInfo2 = this.listStartedGame.FirstOrDefault((GameInfo p) => p.ProcessId == gameInfo.ProcessId);
				if (gameInfo2 != null)
				{
					this.listStartedGame.Remove(gameInfo2);
					LogHelper.Log(string.Format("[App] [HandleGameEndEvent] 从列表中移除结束的游戏：process_id = {0} \t name = {1} \t is64 = {2}", gameInfo2.ProcessId, gameInfo2.ProcessName, gameInfo2.Is64));
				}
				LogHelper.Log(string.Format("[App] [HandleGameEndEvent] 列表中还有 {0} 个游戏", this.listStartedGame.Count));
				if (this.listStartedGame.Count <= 0)
				{
					PluginDll.Instance.StopInfobarData();
					Task<bool> taskAwaiter = this.ReadyRecovery();
					if (!taskAwaiter.IsCompleted)
					{
                        await taskAwaiter;
						Task<bool> taskAwaiter2 = default(Task<bool>);
						taskAwaiter = taskAwaiter2;
						taskAwaiter2 = default(Task<bool>);
					}
					if (taskAwaiter!=null && taskAwaiter.Result)
					{
						GlobalCurrentStatus.InGaming = false;
						LogHelper.Log("[App] [HandleGameEndEvent] 列表中所有游戏都已退出，并且在5秒内没有再启动新的游戏，开始恢复设置。");
						if (this.notifyIcon != null)
						{
							this.notifyIcon.CloseBalloon();
						}
						this.tokenSource = null;
						Optimize.Instance.StartRecovery(null);
					}
					else
					{
						LogHelper.Log("[App] [HandleGameEndEvent] 列表中所有游戏都已退出，但在5秒内又启动了新的游戏，停止恢复设置。");
					}
				}
			}
		}

		// Token: 0x060000CE RID: 206 RVA: 0x000049F8 File Offset: 0x00002BF8
		private void HandleGameBatteryModeEvent(GameBatteryModeChangedReceivedData batteryMode)
		{
			try
			{
				GlobalCurrentStatus.CurrentPowerMode = batteryMode.PowerMode;
				GlobalCurrentStatus.CurrentPowerBatteryRemaining = batteryMode.BatteryRemaining;
			}
			catch (Exception ex)
			{
				LogHelper.Log("[App] [HandleGameBatteryModeEvent] 处理接收到电源模式改变数据时异常：ex = " + ex.Message);
			}
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00004A48 File Offset: 0x00002C48
		private void HandleGameModeChangedEvent(GameMode newMode, ChangeModeSenderType senderType)
		{
			try
			{
				if (Enum.IsDefined(typeof(GameMode), newMode))
				{
					GlobalCurrentStatus.CurrentGameMode = newMode;
					if (senderType == ChangeModeSenderType.Tray)
					{
						GlobalCurrentStatus.InGameModeChanging = true;
						Optimize.Instance.SaveRecoveryDataFile();
					}
					else
					{
						GlobalCurrentStatus.InGameModeChanging = false;
					}
					Message message = this.message;
					if (message != null)
					{
						message.SendChangeModeMessage();
					}
				}
				else
				{
					LogHelper.Log(string.Format("[App] [HandleGameModeChangedEvent] 收到的模式更改通知中的新模式不在枚举值中：newMode = {0}", newMode));
				}
			}
			catch (Exception ex)
			{
				LogHelper.Log("[App] [HandleGameModeChangedEvent] 处理接收到模式改变数据时异常：ex = " + ex.Message);
			}
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00004AE0 File Offset: 0x00002CE0
		private void HandleGameHardwareInfoChangedEvent(GameHardwareInfoChangedReceivedData hardwareInfo)
		{
			try
			{
				int loadPercent = hardwareInfo.CPUInfo.LoadPercent;
				int cpuFreq = Convert.ToInt32(hardwareInfo.CPUInfo.CurFreq * 100.0);
				Convert.ToInt32(hardwareInfo.CPUInfo.FreqMax);
				int cpuTemperature = Convert.ToInt32(hardwareInfo.CPUInfo.Temperature);
				int gpuPrecent = -1;
				int gpuFreq = -1;
				int gpuFreqMax = -1;
				int gpuTemperature = -1;
				int dram = hardwareInfo.Dram;
				int gpu_fan_rpm = 0;
				int gpu_fan_rpm_max = 0;
				int gpu_power = 0;
				int gpu_mem_usage = 0;
				if (hardwareInfo.GPUInfo != null && hardwareInfo.GPUInfo.Count > 0)
				{
					gpuPrecent = hardwareInfo.GPUInfo[0].LoadPercent;
					gpuFreq = Convert.ToInt32(hardwareInfo.GPUInfo[0].CurFreq * 100.0);
					gpuFreqMax = Convert.ToInt32(hardwareInfo.GPUInfo[0].FreqMax);
					gpuTemperature = Convert.ToInt32(hardwareInfo.GPUInfo[0].Temperature);
					gpu_fan_rpm = hardwareInfo.GPUInfo[0].Fan_Speed;
					gpu_fan_rpm_max = hardwareInfo.GPUInfo[0].Fan_Speed_Max;
					gpu_power = hardwareInfo.GPUInfo[0].Power;
					gpu_mem_usage = hardwareInfo.GPUInfo[0].Mem_Usage;
				}
				LZ_MSG_DEVICE_INFO deviceInfo = new LZ_MSG_DEVICE_INFO
				{
					CpuPercent = loadPercent,
					CpuFreq = cpuFreq,
					CpuTemperature = cpuTemperature,
					cpu_fan_rpm = hardwareInfo.CPUInfo.Fan_Speed,
					cpu_fan_rpm_max = hardwareInfo.CPUInfo.Fan_Speed_Max,
					cpu_power = hardwareInfo.CPUInfo.Power,
					CpuFreqMax = hardwareInfo.CPUInfo.FreqMax,
					GpuPrecent = gpuPrecent,
					GpuFreq = gpuFreq,
					GpuFreqMax = gpuFreqMax,
					GpuTemperature = gpuTemperature,
					Power = GlobalCurrentStatus.CurrentPowerBatteryRemaining,
					PowerMode = (int)GlobalCurrentStatus.CurrentPowerMode,
					gpu_fan_rpm = gpu_fan_rpm,
					gpu_fan_rpm_max = gpu_fan_rpm_max,
					gpu_power = gpu_power,
					gpu_mem_usage = gpu_mem_usage,
					Dram = dram,
					dram_freq = hardwareInfo.Dram_Freq
				};
				Message message = this.message;
				if (message != null)
				{
					message.SendHardwareInfoMessage(deviceInfo);
				}
			}
			catch (Exception ex)
			{
				LogHelper.Log("[App] [HandleGameHardwareInfoChangedEvent] 处理 cpu 和 gpu 实时数据时异常：ex = " + ex.Message);
			}
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00004D50 File Offset: 0x00002F50
		private void HandleGameExternalDeviceChangedEvent(GameExternalDeviceChangedReceivedData externalDeviceInfo)
		{
			new Thread(new ParameterizedThreadStart(this.HandleGameExternalDeviceChangedEventThread)).Start(externalDeviceInfo);
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00004D6C File Offset: 0x00002F6C
		private async Task<bool> ReadyRecovery()
		{
			this.tokenSource = new CancellationTokenSource();
			CancellationToken ct = this.tokenSource.Token;
			bool result = true;
			Task task = Task.Run(delegate ()
			{
				ct.ThrowIfCancellationRequested();
				int num = 5000;
				for (int i = 0; i <= num; i += 500)
				{
					if (ct.IsCancellationRequested)
					{
						ct.ThrowIfCancellationRequested();
					}
					Thread.Sleep(500);
					LogHelper.Log(string.Format("[App] [ReadyRecovery] 列表中最后一个游戏已退出，开始计时：{0}...", i));
				}
			}, this.tokenSource.Token);
			try
			{
				await task;
			}
			catch (OperationCanceledException ex)
			{
				result = false;
				LogHelper.Log("[App] [ReadyRecovery] 在准备恢复设置时，有新游戏启动，停止计时：oce = " + ex.Message);
			}
			catch (Exception ex2)
			{
				LogHelper.Log("[App] [ReadyRecovery] 在准备恢复设置时发生异常：" + ex2.Message + "。");
			}
			finally
			{
				if (this.tokenSource != null)
				{
					this.tokenSource.Dispose();
				}
			}
			return result;
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00004DB0 File Offset: 0x00002FB0
		private void InitMessage()
		{
			try
			{
				this.message = new Message(this.InitWindowForMessage());
				this.message.GameInsideHookStartEvent += this.Message_GameInsideHookStartEvent;
				this.message.GameInsideInfobarShowedEvent += this.Message_GameInsideInfobarShowedEvent;
				this.message.GameInsideInfobarHidedEvent += this.Message_GameInsideInfobarHidedEvent;
				this.message.GameInsideChangeGameModeEvent += this.Message_GameInsideChangeGameModeEvent;
				this.message.GameInsideCleanMemoryEvent += this.Message_GameInsideCleanMemoryEvent;
				this.message.GameInsideImageSnapedEvent += this.Message_GameInsideImageSnapedEvent;
				this.message.GameInsideReportEvent += this.Message_GameInsideReportEvent;
				this.message.FromSelfMessageEvent += this.Message_FromSelfMessageEvent;
				this.message.ExitMessageEvent += this.Message_ExitMessageEvent;
			}
			catch (Exception ex)
			{
				LogHelper.Log("[App] [InitMessage] 初始化 Message 时异常：ex = " + ex.Message);
			}
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00004EC8 File Offset: 0x000030C8
		private Window InitWindowForMessage()
		{
			Window window = new Window();
			window.Height = 100.0;
			window.Width = 100.0;
			window.AllowsTransparency = true;
			window.Background = new SolidColorBrush(Colors.Transparent);
			window.ShowInTaskbar = false;
			window.WindowStyle = WindowStyle.None;
			window.Visibility = Visibility.Hidden;
			window.Title = "LZTray-{4B82EA50-B09D-4531-A0BE-71A0DFE3ACA2}";
			window.Show();
			return window;
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00004F38 File Offset: 0x00003138
		private void Message_ExitMessageEvent()
		{
			LogHelper.Log("[App] [Message_ExitMessageEvent] 收到退出程序消息事件");
			Optimize.Instance.StartRecovery(null);
			Message message = this.message;
			if (message != null)
			{
				message.SendExitMessageToLZMain();
			}
			LogHelper.Log("[App] [Message_ExitMessageEvent] 通知 LZMain 退出。");
			Message message2 = this.message;
			if (message2 != null)
			{
				message2.SendExitMessage();
			}
			LogHelper.Log("[App] [Message_ExitMessageEvent] 通知游戏内工具栏退出。");
			Application.Current.Shutdown();
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00004F9C File Offset: 0x0000319C
		private void Message_FromSelfMessageEvent(MessageEventArgs<LZ_MSG_SELF_RUN_LZMAIN> messageEventArgs)
		{
			LogHelper.Log("[App] [Message_FromSelfMessageEvent] 收到从自身其它实例进程消息事件：data = " + messageEventArgs.EventData.Data);
			if (string.IsNullOrEmpty(messageEventArgs.EventData.Data))
			{
				return;
			}
			try
			{
				string[] array = JsonHelper.StringToObject<string[]>(messageEventArgs.EventData.Data);
				if (array != null && array.Length != 0)
				{
					this.HandleArgs(array);
					if (CommandLine.CommandLineArgs.Contains("--lzmain"))
					{
						LogHelper.Log("[App] [Message_FromSelfMessageEvent] 查找到命令行参数，启动 LZMain 主程序。");
						using (RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Lenovo\\LegionZone\\", RegistryKeyPermissionCheck.ReadWriteSubTree, RegistryRights.FullControl))
						{
							if (registryKey != null)
							{
								registryKey.SetValue("LaunchWithTray", "1");
								string text = string.Empty;
								object value = registryKey.GetValue("LaunchDetail");
								if (value != null)
								{
									text = value.ToString();
								}
								text = text + "TRAY_StartUP_HaveTray_RunLZ=" + Environment.TickCount.ToString() + ";";
								registryKey.SetValue("LaunchDetail", text);
							}
						}
						this.OpenLegionZone(LegionZoneFromType.unknow);
					}
				}
				else
				{
					LogHelper.Log("[App] [Message_FromSelfMessageEvent] 转换 Json 失败：json_data = " + messageEventArgs.EventData.Data);
				}
			}
			catch (Exception ex)
			{
				LogHelper.Log("[App] [Message_FromSelfMessageEvent] 处理从自身其它实例进程消息时异常：json_data = " + messageEventArgs.EventData.Data + ", ex = " + ex.Message);
			}
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00005104 File Offset: 0x00003304
		private void Message_GameInsideHookStartEvent(MessageEventArgs<LZ_MSG_HOOK_START> messageEventArgs)
		{
			LogHelper.Log(string.Format("[App] [Message_GameInsideHookStartEvent] 收到游戏内注入成功消息事件：process_id = {0} \t message_handler = {1} \t gpu_device_id = {2} \t gpu_vendor_id = {3} \t gpu_desc = {4}", new object[]
			{
				messageEventArgs.EventData.ProcessId,
				messageEventArgs.Handler,
				messageEventArgs.EventData.GpuDeviceId,
				messageEventArgs.EventData.GpuVendorId,
				messageEventArgs.EventData.GpuDesc
			}));

			var checkprocess = this.listStartedGame.Find(p => (uint)(p.ProcessId) == messageEventArgs.EventData.ProcessId);
			if (checkprocess ==null)
            {
				this.listStartedGame.Add(new GameInfo()
				{
					ProcessId = (int)(messageEventArgs.EventData.ProcessId),
					ProcessName = "",
					IsInjected = true,
					SupportInjected = true,
					GpuDeviceId = messageEventArgs.EventData.GpuDeviceId,
					GpuVendorId = messageEventArgs.EventData.GpuVendorId,
					GpuDesc = messageEventArgs.EventData.GpuDesc.ToString(),
					Is64 = Utils.Is64Process(Process.GetProcessById((int)messageEventArgs.EventData.ProcessId)),
					ShowAssistant = false,
					ChangeToPerformance = false,
					NetworkOptimization = false
				});
			}

			Func<GameInfo, bool>  _9__1 = null;
			ThreadPool.QueueUserWorkItem(delegate (object state)
			{
				List<GameInfo> list = this.listStartedGame;
				GameInfo gameInfo;
				if (list == null)
				{
					gameInfo = null;
				}
				else
				{
					Func<GameInfo, bool> predicate;
					if ((predicate = _9__1) == null)
					{
						predicate = (_9__1 = (GameInfo p) => (long)p.ProcessId == (long)((ulong)messageEventArgs.EventData.ProcessId));
					}
					gameInfo = list.FirstOrDefault(predicate);
				}
				GameInfo gameInfo2 = gameInfo;
				if (gameInfo2 != null)
				{
					gameInfo2.IsInjected = true;
					gameInfo2.MessageHandlerCollection.Add(messageEventArgs.Handler);
					gameInfo2.GpuDeviceId = messageEventArgs.EventData.GpuDeviceId;
					gameInfo2.GpuVendorId = messageEventArgs.EventData.GpuVendorId;
					gameInfo2.GpuDesc = messageEventArgs.EventData.GpuDesc.ToString();
					Message message = this.message;
					if (message != null)
					{
						message.SendChangeModeMessage();
					}
					Dictionary<string, string> dicParams = new Dictionary<string, string>
					{
						{ "gameName", gameInfo2.GameName },
						{ "processName", gameInfo2.ProcessName }
					};
					LudpHelper.TrackEvent("LZ_gameAssistant", "LZ_gameInjection", "", dicParams);
					return;
				}
				LogHelper.Log(string.Format("[App] [Message_GameInsideHookStartEvent] 收到游戏内注入成功消息事件后，未在 listOverlayGame 列表中查找到对应的 ProcessId：process_id = {0}", messageEventArgs.EventData.ProcessId));
			});
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x000051BC File Offset: 0x000033BC
		private void Message_GameInsideInfobarShowedEvent(MessageEventArgs messageEventArgs)
		{
			LogHelper.Log(string.Format("[App] [Message_GameInsideInfobarShowedEvent] 收到游戏内显示性能监控栏消息事件：handler = {0}", messageEventArgs.Handler));
			ThreadPool.QueueUserWorkItem(delegate (object state)
			{
				PluginDll.Instance.StartInfobarData();
			});
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00005208 File Offset: 0x00003408
		private void Message_GameInsideInfobarHidedEvent(MessageEventArgs messageEventArgs)
		{
			LogHelper.Log(string.Format("[App] [Message_GameInsideInfobarHidedEvent] 收到游戏内隐藏性能监控栏消息事件：handler = {0}", messageEventArgs.Handler));
			ThreadPool.QueueUserWorkItem(delegate (object state)
			{
				PluginDll.Instance.StopInfobarData();
			});
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00005254 File Offset: 0x00003454
		private void Message_GameInsideChangeGameModeEvent(MessageEventArgs<LZ_MSG_CHANGE_MODE> messageEventArgs)
		{
			LogHelper.Log(string.Format("[App] [Message_GameInsideChangeGameModeEvent] 收到游戏内修改游戏模式消息事件：message_handler = {0} \t target_game_mode = {1}", messageEventArgs.Handler, messageEventArgs.EventData.TargetGameMode));
			ThreadPool.QueueUserWorkItem(delegate (object state)
			{
				PluginDll.Instance.ChangeMode(ChangeModeSenderType.Toolkit, (GameMode)messageEventArgs.EventData.TargetGameMode);
			});
		}

		// Token: 0x060000DB RID: 219 RVA: 0x000052B4 File Offset: 0x000034B4
		private void Message_GameInsideCleanMemoryEvent(MessageEventArgs messageEventArgs)
		{
			LogHelper.Log(string.Format("[App] [Message_GameInsideCleanMemoryEvent] 收到游戏内清理内存消息事件：handler = {0}", messageEventArgs.Handler));
			ThreadPool.QueueUserWorkItem(delegate (object state)
			{
				PluginDll.Instance.CleanMemory();
			});
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00005300 File Offset: 0x00003500
		private void Message_GameInsideImageSnapedEvent(MessageEventArgs<LZ_MSG_IMAGE_SNAPED> messageEventArgs)
		{
			LogHelper.Log(string.Format("[App] [Message_GameInsideImageSnapedEvent] 收到游戏内截图成功消息事件：message_handler = {0} \t image_path = {1}", messageEventArgs.Handler, messageEventArgs.EventData.ImagePath));
			try
			{
				string @string = Encoding.UTF8.GetString(messageEventArgs.EventData.ImagePath, 0, messageEventArgs.EventData.DataLength);
				ThreadPool.QueueUserWorkItem(new WaitCallback(this.SaveSnapImageThread), @string);
			}
			catch (Exception ex)
			{
				LogHelper.Log(string.Format("[App] [Message_GameInsideImageSnapedEvent] 转换保存图片路径时异常：image_path = {0} \t data_length = {1} \t ex = {2} \t ", messageEventArgs.EventData.ImagePath, messageEventArgs.EventData.DataLength, ex.Message));
			}
		}

		// Token: 0x060000DD RID: 221 RVA: 0x000053AC File Offset: 0x000035AC
		private void Message_GameInsideReportEvent(MessageEventArgs<LZ_MSG_REPORT> messageEventArgs)
		{
			LogHelper.Log(string.Format("[App] [Message_GameInsideReportEvent] 收到从游戏内数据打点消息事件：data = {0}", messageEventArgs.EventData.Data));
			ThreadPool.QueueUserWorkItem(delegate (object state)
			{
				try
				{
					string @string = Encoding.UTF8.GetString(messageEventArgs.EventData.Data, 0, messageEventArgs.EventData.DataLength);
					LogHelper.Log("[App] [Message_GameInsideReportEvent] 解析数据完成：data = " + @string);
					GameReportReceivedData gameReportReceivedData = JsonHelper.StringToObject<GameReportReceivedData>(@string);
					if (gameReportReceivedData != null && gameReportReceivedData.value != null)
					{
						Dictionary<string, string> dictionary = new Dictionary<string, string>();
						foreach (KeyValue keyValue in gameReportReceivedData.value)
						{
							dictionary.Add(GameReportReceivedData.KeyNameClickType, keyValue.clickType);
						}
						LudpHelper.TrackEvent(gameReportReceivedData.category, gameReportReceivedData.action, gameReportReceivedData.label, dictionary);
					}
				}
				catch (Exception ex)
				{
					LogHelper.Log("[App] [Message_GameInsideReportEvent] 处理游戏内数据打点消息异常：ex = " + ex.Message);
				}
			});
		}

		// Token: 0x060000DE RID: 222 RVA: 0x000053F8 File Offset: 0x000035F8
		private void SaveSnapImageThread(object o)
		{
			string text = o as string;
			if (!string.IsNullOrEmpty(text) && File.Exists(text))
			{
				string saveSnapImagePath = RegistryHelper.Instance.GetSaveSnapImagePath();
				try
				{
					if (!Directory.Exists(saveSnapImagePath))
					{
						Directory.CreateDirectory(saveSnapImagePath);
						LogHelper.Log("[App] [SaveSnapImage] 保存截图的文件夹不存在，创建文件夹：dir_path = " + saveSnapImagePath);
					}
				}
				catch (Exception ex)
				{
					LogHelper.Log("[App] [SaveSnapImage] 创建保存截图的文件夹异常：dir_path = " + saveSnapImagePath + " \t ex = " + ex.Message);
					Message message = this.message;
					if (message != null)
					{
						message.SendSaveImageResultMessage(0);
					}
				}
				try
				{
					string destFileName = Path.Combine(saveSnapImagePath, DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".jpg");
					File.Copy(text, destFileName, true);
					LogHelper.Log("[App] [SaveSnapImage] 保存截图成功：save_path = " + saveSnapImagePath + " \t image_temp_path = " + text);
					Message message2 = this.message;
					if (message2 != null)
					{
						message2.SendSaveImageResultMessage(1);
					}
					return;
				}
				catch (Exception ex2)
				{
					LogHelper.Log(string.Concat(new string[] { "[App] [SaveSnapImage] 保存截图异常：save_path = ", saveSnapImagePath, " \t image_temp_path = ", text, " \t ex = ", ex2.Message }));
					Message message3 = this.message;
					if (message3 != null)
					{
						message3.SendSaveImageResultMessage(0);
					}
					return;
				}
			}
			LogHelper.Log("[App] [SaveSnapImage] 保存截图时，从游戏内接收到的临时文件路径不存在：image_temp_path = " + text);
			Message message4 = this.message;
			if (message4 == null)
			{
				return;
			}
			message4.SendSaveImageResultMessage(0);
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00005564 File Offset: 0x00003764
		private void InitTray()
		{
			LogHelper.Log("[App] [OnStartup] InitTray");
			base.Dispatcher.BeginInvoke(new Action(delegate ()
			{
				this.CheckNotifyInit();
				LogHelper.Log("[App] [OnStartup] InitTray end ");
			}), Array.Empty<object>());
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x0000558D File Offset: 0x0000378D
		private void CheckNotifyInit()
		{
			if (this.notifyIcon == null)
			{
				this.notifyIcon = (TaskbarIcon)base.FindResource("MyNotifyIcon");
			}
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x000055B0 File Offset: 0x000037B0
		private void MyNotifyIcon_TrayLeftMouseDown(object sender, RoutedEventArgs e)
		{
			LogHelper.Log("[App] [MyNotifyIcon_TrayLeftMouseDown] 用户点击了托盘，启动 LZMain 主程序。");
			FakeLegionZone.MainWindow.self.Show();
			FakeLegionZone.MainWindow.self.Topmost = true;
			FakeLegionZone.MainWindow.self.Topmost = false;
			FakeLegionZone.MainWindow.self.WindowState = WindowState.Normal;

			//using (RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Lenovo\\LegionZone\\", RegistryKeyPermissionCheck.ReadWriteSubTree, RegistryRights.FullControl))
			//{
			//	if (registryKey != null)
			//	{
			//		registryKey.SetValue("LaunchTime", Environment.TickCount.ToString());
			//		registryKey.SetValue("LaunchWithTray", "1");
			//		registryKey.SetValue("LaunchDetail", "");
			//		string value = "TRAY_CLICK_RUNLZ=" + Environment.TickCount.ToString() + ";";
			//		registryKey.SetValue("LaunchDetail", value);
			//	}
			//}
			//this.OpenLegionZone(LegionZoneFromType.tray);
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00005664 File Offset: 0x00003864
		private void Show_LZMain_MenuItem_Click(object sender, RoutedEventArgs e)
		{
			LogHelper.Log("[App] [Show_LZMain_MenuItem_Click] 用户点击了菜单，启动 LZMain 主程序。");
			FakeLegionZone.MainWindow.self.Show();
			FakeLegionZone.MainWindow.self.Topmost=true;
			FakeLegionZone.MainWindow.self.Topmost = false;
			FakeLegionZone.MainWindow.self.WindowState = WindowState.Normal;
			
			//using (RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Lenovo\\LegionZone\\", RegistryKeyPermissionCheck.ReadWriteSubTree, RegistryRights.FullControl))
			//{
			//	if (registryKey != null)
			//	{
			//		registryKey.SetValue("LaunchTime", Environment.TickCount.ToString());
			//		registryKey.SetValue("LaunchWithTray", "1");s
			//		registryKey.SetValue("LaunchDetail", "");
			//		string value = "TRAY_MENU_RUNLZ=" + Environment.TickCount.ToString() + ";";
			//		registryKey.SetValue("LaunchDetail", value);
			//	}
			//}
			//this.OpenLegionZone(LegionZoneFromType.tray);
		}

		private void HideBar()
        {
			var enable = RegistryHelper.Instance.GetIsPerformMonitor();
			RegistryHelper.Instance.SetIsPerformMonitorReal(enable);
			RegistryHelper.Instance.SetIsPerformMonitor(false);
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00005718 File Offset: 0x00003918
		private void Exit_MenuItem_Click(object sender, RoutedEventArgs e)
		{
			bool? flag = true;// new WindowExitConfirm().ShowDialog();
			if (flag != null && flag.Value)
			{
				HideBar();
				unhookThread(); 

				Optimize.Instance.StartRecovery(null);
				Message message = this.message;
				if (message != null)
				{
					message.SendExitMessageToLZMain();
				}
				LogHelper.Log("[App] [Exit_MenuItem_Click] 通知 LZMain 退出。");
				Message message2 = this.message;
				if (message2 != null)
				{
					message2.SendExitMessage();
				}
				LogHelper.Log("[App] [Exit_MenuItem_Click] 通知游戏内工具栏退出。");
				PluginDlls.Instance.UninitPlugins();
				LogHelper.Log("[App] [Exit_MenuItem_Click] 通知通知所有 plugin dll 退出。");
				PluginDll.Instance.UnInitDll();
				
				Application.Current.Shutdown();
			}
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x000057AC File Offset: 0x000039AC
		private void Inject(GameInfo gameInfo)
		{
			if (!RegistryHelper.Instance.GetIsPerformMonitor())
			{
				LogHelper.Log("[App] [Inject] 注入总开关（注册表中的性能监控开关）关闭，不注入。");
				return;
			}
			string text = Path.Combine(Utils.GetBasePath(), gameInfo.Is64 ? "LZToolkit64.exe" : "LZToolkit32.exe");
			string text2 = Path.Combine(Utils.GetBasePath(), gameInfo.Is64 ? "LZToolkit64.dll" : "LZToolkit32.dll");
			if (!VerifySignature.Verify(text) || !VerifySignature.Verify(text2))
			{
				LogHelper.Log("[App] [Inject] 验证签名失败，终止注入：exe_file_path = " + text + " \t dll_file_path = " + text2);
				return;
			}
			LogHelper.Log(string.Format("[App] [HandleGameStartEvent] 准备开始使用线程延时注入：process_id = {0}", gameInfo.ProcessId));
			new Thread(new ParameterizedThreadStart(this.InjectThread)).Start(gameInfo);
		}

		private void unhookThread()
		{
			return;
			//foreach (GameInfo gameInfo in listStartedGame)
			 //         {
			//	string text = Path.Combine(Utils.GetBasePath(), gameInfo.Is64 ? "LZToolkit64.exe" : "LZToolkit32.exe");
			//	if (!File.Exists(text))
			//	{
			//		LogHelper.Log(string.Format("[App] [Inject] 注入程序文件未找到：path = {0}, process_id = {1}", text, gameInfo.ProcessId));
			//		return;
			//	}
			//	string text2 = Path.Combine(Utils.GetBasePath(), gameInfo.Is64 ? "LZToolkit64.dll" : "LZToolkit32.dll");
			//	if (!File.Exists(text2))
			//	{
			//		LogHelper.Log(string.Format("[App] [Inject] 注入 dll 文件未找到：path = {0}, process_id = {1}", text2, gameInfo.ProcessId));
			//		return;
			//	}

			//	string text3 = string.Format(string.Format("{0} \"{1}\" unhook", gameInfo.ProcessId, text2), Array.Empty<object>());

			//	Process.Start(text, text3);
			//} 
		}
		
		// Token: 0x060000E5 RID: 229 RVA: 0x00005868 File Offset: 0x00003A68
		private void InjectThread(object oGameInfo)
		{
			GameInfo gameInfo = oGameInfo as GameInfo;
			if (gameInfo == null)
			{
				return;
			}
			Func<GameInfo, bool> _9__0 = null;
			for (int i = 1; i <= 1; i++)
			{
				LogHelper.Log(string.Format("[App] [InjectThread] 开始注入前计时：process_id = {0}, count = {1}", gameInfo.ProcessId, i));
				Thread.Sleep(500);
				if (gameInfo.IsInjected)
				{
					LogHelper.Log(string.Format("[App] [InjectThread] 游戏已注入成功，停止重试：process_id = {0}", gameInfo.ProcessId));
					return;
				}
				if (!this.listStartedGame.Contains(gameInfo))
				{
					LogHelper.Log(string.Format("[App] [InjectThread] 计时结束，但游戏已结束并从列表中移除，中止调用注入：process_id = {0}", gameInfo.ProcessId));
					return;
				}
				LogHelper.Log(string.Format("[App] [Inject] 计时结束，开始调用注入程序：process_id = {0} \t name = {1} \t is64 = {2}", gameInfo.ProcessId, gameInfo.ProcessName, gameInfo.Is64));
				string text = Path.Combine(Utils.GetBasePath(), gameInfo.Is64 ? "LZToolkit64.exe" : "LZToolkit32.exe");
				if (!File.Exists(text))
				{
					LogHelper.Log(string.Format("[App] [Inject] 注入程序文件未找到：path = {0}, process_id = {1}", text, gameInfo.ProcessId));
					return;
				}
				string text2 = Path.Combine(Utils.GetBasePath(), gameInfo.Is64 ? "LZToolkit64.dll" : "LZToolkit32.dll");
				if (!File.Exists(text2))
				{
					LogHelper.Log(string.Format("[App] [Inject] 注入 dll 文件未找到：path = {0}, process_id = {1}", text2, gameInfo.ProcessId));
					return;
				}
				string text3 = string.Format(string.Format("{0} \"{1}\"", gameInfo.ProcessId, text2), Array.Empty<object>());
				try
				{
					IEnumerable<GameInfo> source = this.listStartedGame;
					Func<GameInfo, bool> predicate;
					if ((predicate = _9__0) == null)
					{
						predicate = (_9__0 = (GameInfo p) => p.ProcessId == gameInfo.ProcessId);
					}
					if (source.FirstOrDefault(predicate) == null)
					{
						LogHelper.Log(string.Format("[App] [Inject] 游戏已不在列表中，不调用注入程序：process_id = {0}", gameInfo.ProcessId));
						break;
					}
					if (gameInfo.IsInjected)
					{
						LogHelper.Log(string.Format("[App] [InjectThread] 游戏已注入成功，停止重试：process_id = {0}", gameInfo.ProcessId));
						break;
					}
					Dictionary<string, string> dicParams = new Dictionary<string, string>
					{
						{ "gameName", gameInfo.GameName },
						{ "processName", gameInfo.ProcessName }
					};
					LudpHelper.TrackEvent("LZ_gameAssistant", "LZ_gameInjectionStart", "", dicParams);
					Process.Start(text, text3);
					LogHelper.Log(string.Format("[App] [Inject] 调用注入程序结束：process_id = {0} \t exe_path = {1} \t dll_path = {2}", gameInfo.ProcessId, text, text2));
				}
				catch (Exception ex)
				{
					LogHelper.Log(string.Format("[App] [Inject] 调用注入程序异常：process_id = {0} \t exe_path = {1} \t dll_path = {2} \t arguments = {3} \t ex_message = {4}", new object[] { gameInfo.ProcessId, text, text2, text3, ex.Message }));
				}
				LogHelper.Log(string.Format("[App] [InjectThread] 第 {0} 次注入重试结束：process_id = {1}", i, gameInfo.ProcessId));
			}
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00005B9C File Offset: 0x00003D9C
		private void InitUpdate()
		{
			this.updateTimer = new DispatcherTimer();
			int num = 30;
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Lenovo\\LegionZone\\debug\\", RegistryKeyPermissionCheck.ReadSubTree, RegistryRights.ExecuteKey))
			{
				if (registryKey != null)
				{
					object value = registryKey.GetValue("TrayUpdateInterval", 30);
					try
					{
						num = Convert.ToInt32(value);
					}
					catch
					{
						num = 30;
					}
				}
			}
			this.updateTimer.Interval = TimeSpan.FromMinutes((double)num);
			this.updateTimer.Tick += this.UpdateTimer_Tick;
			this.updateTimer.Start();
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00005C50 File Offset: 0x00003E50
		private async void UpdateTimer_Tick(object sender, EventArgs e)
		{
			this.CheckNotifyInit();
			string format = "[App] [UpdateTimer_Tick] 当前托盘的显示状态：tray_visibility = {0}。";
			TaskbarIcon taskbarIcon = this.notifyIcon;
			LogHelper.Log(string.Format(format, (taskbarIcon != null) ? new Visibility?(taskbarIcon.Visibility) : null));
			if (this.notifyIcon != null && this.notifyIcon.Visibility != Visibility.Visible)
			{
				this.notifyIcon.Visibility = Visibility.Visible;
				string format2 = "[App] [UpdateTimer_Tick] 未知原因使托盘处于隐藏状态，重新设置成显示状态：tray_visibility = {0}。";
				TaskbarIcon taskbarIcon2 = this.notifyIcon;
				LogHelper.Log(string.Format(format2, (taskbarIcon2 != null) ? new Visibility?(taskbarIcon2.Visibility) : null));
			}
			await this.UpdateCheck();
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00005C88 File Offset: 0x00003E88
		private async Task UpdateCheck()
		{
			LogHelper.Log("[App] [UpdateCheck] 开始定时检查更新。");
			await this.CallAndWaitExitUpdateCheck();
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00005CCC File Offset: 0x00003ECC
		private async Task CallAndWaitExitUpdateCheck()
		{
			await Task.Run(delegate ()
			{
				try
				{
					string text = Path.Combine(RegistryHelper.Instance.InstallDir, "LZUpdate.exe");
					if (!VerifySignature.Verify(text))
					{
						LogHelper.Log("[App] [CallAndWaitExitUpdateCheck] 验证签名失败，终止调用：update_exe_path = " + text);
						return;
					}
					if (!string.IsNullOrEmpty(text) && File.Exists(text))
					{
						LogHelper.Log("[App] [CallAndWaitExitUpdateCheck] 开始调用 LZUpdate 程序：update_exe_path = " + text);
						if (Process.Start(text).WaitForExit(180000))
						{
							this.CheckUpdateVersion();
						}
					}
					else
					{
						LogHelper.Log("[App] [CallAndWaitExitUpdateCheck] 未找到 LZUpdate 程序：update_exe_path = " + text);
					}
				}
				catch (Exception ex)
				{
					LogHelper.Log("[App] [CallAndWaitExitUpdateCheck] 启动 LZUpdate 程序异常：ex = " + ex.Message);
				}
				LogHelper.Log("[App] [CallAndWaitExitUpdateCheck] 定时检查更新结束。");
			});
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00005D10 File Offset: 0x00003F10
		private void CheckUpdateVersion()
		{
			string version = RegistryHelper.Instance.GetVersion();
			string newVersionNumber = RegistryHelper.Instance.GetNewVersionNumber();
			LogHelper.Log("[App] [CheckUpdateVersion] 新旧版本号：current_version = " + version + " \t new_version = " + newVersionNumber);
			if (string.IsNullOrEmpty(version) || string.IsNullOrEmpty(newVersionNumber))
			{
				LogHelper.Log("[App] [CheckUpdateVersion] 无新版本更新：current_version = " + version + " \t new_version = " + newVersionNumber);
				return;
			}
			Version value = new Version(version);
			Version version2 = new Version(newVersionNumber);
			if (version2.CompareTo(value) <= 0 || RegistryHelper.Instance.GetNotified())
			{
				LogHelper.Log(string.Format("[App] [CheckUpdateVersion] 无新版本或者新版本已经提示过用户，不再提示：current_version = {0} \t new_version = {1} \t notified = {2}", version, newVersionNumber, RegistryHelper.Instance.GetNotified()));
				return;
			}
			UpdateInfo updateInfo = new UpdateInfo
			{
				NewVersion = version2.ToString()
			};
			string newVersionUpdateInfo = RegistryHelper.Instance.GetNewVersionUpdateInfo();
			LogHelper.Log("[App] [CheckUpdateVersion] 有新版本需要更新，新版本提示信息：update_info = " + newVersionUpdateInfo);
			if (!string.IsNullOrEmpty(newVersionUpdateInfo))
			{
				string[] array = newVersionUpdateInfo.Split(new string[] { "<br/>" }, StringSplitOptions.RemoveEmptyEntries);
				for (int i = 0; i < array.Length; i++)
				{
					updateInfo.infos.Add(new UpdateInfoItem
					{
						Id = (i + 1).ToString() + ".",
						Info = array[i]
					});
				}
			}
			if (this.listStartedGame.Count <= 0)
			{
				LogHelper.Log("[App] [CheckUpdateVersion] 没有游戏在运行，显示弹框。");
				base.Dispatcher.Invoke(delegate ()
				{
					this.CheckNotifyInit();
					//UpdateBalloon balloon = new UpdateBalloon(updateInfo);
					//this.notifyIcon.ShowCustomBalloon(balloon, PopupAnimation.Slide, new int?(10000));
				});
				RegistryHelper.Instance.SetNotifyed(true);
				return;
			}
			LogHelper.Log(string.Format("[App] [CheckUpdateVersion] 有游戏在运行，不显示弹框：game_count = {0}", this.listStartedGame.Count));
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00005ECC File Offset: 0x000040CC
		private void DeleteOldVersion()
		{
			string oldVersion = RegistryHelper.Instance.GetOldVersion();
			if (string.IsNullOrEmpty(oldVersion))
			{
				return;
			}
			try
			{
				LogHelper.Log("[App] [DeleteOldVersion] 发现旧版本残留，准备删除：old_version = " + oldVersion);
				Version value = new Version(oldVersion);
				string text = Utils.GetBasePath().Remove(Utils.GetBasePath().LastIndexOf("\\"));
				int num = text.LastIndexOf("\\") + 1;
				if (new Version(Utils.GetBasePath().Substring(num, text.Length - num)).CompareTo(value) > 0)
				{
					Directory.Delete(Path.Combine(RegistryHelper.Instance.InstallDir, oldVersion), true);
					RegistryHelper.Instance.DeleteOldVersion();
				}
				LogHelper.Log("[App] [DeleteOldVersion] 删除旧版本文件夹成功：old_version = " + oldVersion);
			}
			catch (Exception ex)
			{
				LogHelper.Log("[App] [DeleteOldVersion] 删除旧版本残留文件失败：ex = " + ex.Message);
			}
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00005FAC File Offset: 0x000041AC
		private void InitExternalDeviceList()
		{
			LogHelper.Log("[App] [InitExternalDeviceList] 加载本地外设设备数据。");
			this.listExternalDevice = new ExternalDeviceInfoList();
			this.listExternalDevice.AddItemsFromExternalDeviceData(ExternalDeviceDataOperation.Read());
			this.listExternalDevice.Initialized = true;
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00005FE0 File Offset: 0x000041E0
		private void HandleGameExternalDeviceChangedEventThread(object o)
		{
			GameExternalDeviceChangedReceivedData gameExternalDeviceChangedReceivedData = o as GameExternalDeviceChangedReceivedData;
			if (gameExternalDeviceChangedReceivedData == null)
			{
				LogHelper.Log(string.Format("[App] [HandleGameExternalDeviceChangedEventThread] 转换处理接收到外设改变数据时失败：o = {0}", o));
				return;
			}
			if (gameExternalDeviceChangedReceivedData == null || gameExternalDeviceChangedReceivedData.EnumLZPeripheralDev == null || gameExternalDeviceChangedReceivedData.EnumLZPeripheralDev.Count <= 0)
			{
				LogHelper.Log("[App] [HandleGameExternalDeviceChangedEventThread] 从 Plugin 接收到的数据不正确，或者接收到的数据列表为 0。");
				return;
			}
			if (this.listExternalDevice == null)
			{
				this.InitExternalDeviceList();
			}
			ExternalDeviceInfo deviceInfo2 = null;
			foreach (Device device in gameExternalDeviceChangedReceivedData.EnumLZPeripheralDev)
			{
				LogHelper.Log(string.Format("[App] [HandleGameExternalDeviceChangedEventThread] 接收到的外设数据：pid = {0} \t vid = {1} \t status = {2}", device.pid, device.vid, device.status));
				ExternalDeviceInfo deviceInfo = ExternalDeviceInfo.Create(device, gameExternalDeviceChangedReceivedData.action);
				if (deviceInfo != null && deviceInfo.Status == ExternalDeviceStatus.In)
				{
					deviceInfo2 = deviceInfo;
					if (this.listExternalDevice.FirstOrDefault((ExternalDeviceInfo p) => p.PID.ToUpper() == deviceInfo.PID.ToUpper() && p.VID.ToUpper() == deviceInfo.VID.ToUpper()) == null)
					{
						LogHelper.Log(string.Format("[App] [HandleGameExternalDeviceChangedEventThread] 没有从列表中查匹配到外设，说明没有提示过，添加到列表中：pid = {0} \t vid = {1} \t status = {2}", device.pid, device.vid, device.status));
						this.listExternalDevice.Add(deviceInfo);
					}
					else
					{
						LogHelper.Log(string.Format("[App] [HandleGameExternalDeviceChangedEventThread] 从列表中查匹配到外设，说明已经提示过：pid = {0} \t vid = {1} \t status = {2}", device.pid, device.vid, device.status));
					}
				}
				else
				{
					LogHelper.Log(string.Format("[App] [HandleGameExternalDeviceChangedEventThread] 未从已知外设设备列表中匹配到，或者是设备正在拔出：pid = {0} \t vid = {1} \t status = {2}", device.pid, device.vid, device.status));
				}
			}
			if (RegistryHelper.Instance.GetLACIsInstalled())
			{
				this.ExternalDeviceNotify();
				return;
			}
			LogHelper.Log("[App] [HandleGameExternalDeviceChangedEventThread] 未安装 LAC，准备弹出安装提示。");
			this.InstallLAC(deviceInfo2);
		}

		// Token: 0x060000EE RID: 238 RVA: 0x000061BC File Offset: 0x000043BC
		private void InstallLAC(ExternalDeviceInfo deviceInfo)
		{
			if (deviceInfo == null || deviceInfo.Status != ExternalDeviceStatus.In)
			{
				LogHelper.Log("[App] [InstallLAC] 未找到插入的设备，所以不弹出。");
				return;
			}
			if (RegistryHelper.Instance.GetLACIsNeverNotify())
			{
				LogHelper.Log("[App] [InstallLAC] 设置了不再提醒安装 LAC，所以不弹出。");
				return;
			}
			foreach (ExternalDeviceInfo externalDeviceInfo in from p in this.listExternalDevice
															  where !p.IsNotified && p.Status == ExternalDeviceStatus.In
															  select p)
			{
				externalDeviceInfo.IsNotified = true;
			}
			base.Dispatcher.Invoke(delegate ()
			{
				this.CheckNotifyInit();
				//LACInstallBalloon balloon = new LACInstallBalloon(deviceInfo, this.listExternalDevice, LACInstallStatus.Notify);
				//this.notifyIcon.ShowCustomBalloon(balloon, PopupAnimation.Slide, null);
			});
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00006290 File Offset: 0x00004490
		private void ExternalDeviceNotify()
		{
			try
			{
				IEnumerable<ExternalDeviceInfo> enumerable = from p in this.listExternalDevice
															 where !p.IsNotified && p.Status == ExternalDeviceStatus.In
															 select p;
				if (enumerable.Count<ExternalDeviceInfo>() <= 0)
				{
					LogHelper.Log("[App] [ExternalDeviceNotify] 没有新的、未提示过的外设设备，不提示！");
				}
				else
				{
					using (IEnumerator<ExternalDeviceInfo> enumerator = enumerable.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							ExternalDeviceInfo item = enumerator.Current;
							item.IsNotified = true;
							base.Dispatcher.Invoke(delegate ()
							{
								this.CheckNotifyInit();
								LogHelper.Log(string.Format("\t [App] [ExternalDeviceNotify] 弹出新外设提示：pid = {0} \t vid = {1} \t status = {2}", item.PID, item.VID, item.Status));
								//LACInstallBalloon balloon = new LACInstallBalloon(item, this.listExternalDevice, LACInstallStatus.Installed);
								//this.notifyIcon.ShowCustomBalloon(balloon, PopupAnimation.Slide, null);
							});
							Thread.Sleep(1500);
						}
					}
					LogHelper.Log("\t [App] [SaveExternalDeviceData] 存储外设设备数据。");
					this.listExternalDevice.SaveExternalDeviceData();
				}
			}
			catch (Exception ex)
			{
				LogHelper.Log("[App] [ExternalDeviceNotify] 弹出外设提示异常：ex = " + ex.Message);
			}
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00006390 File Offset: 0x00004590
		private void InitAutoReport()
		{
			if (LudpHelper.IsTrack)
			{
				this.autoReportTimer = new DispatcherTimer();
				this.autoReportTimer.Interval = TimeSpan.FromMinutes(30.0);
				this.autoReportTimer.Tick += this.AutoReportTimer_Tick;
				this.autoReportTimer.Start();
			}
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x000063EA File Offset: 0x000045EA
		private void AutoReportTimer_Tick(object sender, EventArgs e)
		{
			ThreadPool.QueueUserWorkItem(delegate (object state)
			{
				LudpHelper.AutoReport();
			});
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00006414 File Offset: 0x00004614
		private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			Exception ex = e.ExceptionObject as Exception;
			LogHelper.Log("[App] [CurrentDomain_UnhandledException] 非 UI 线程异常：ex = " + ((ex != null) ? ex.Message : null));
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00006448 File Offset: 0x00004648
		private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
		{
			e.Handled = true;
			string str = "[App] [CurrentDomain_UnhandledException] 非 UI 线程异常：ex = ";
			Exception exception = e.Exception;
			LogHelper.Log(str + ((exception != null) ? exception.Message : null));
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00006472 File Offset: 0x00004672
		private void InitPluginDlls()
		{
			PluginDlls.Instance.PluginEventDelegate += this.Instance_PluginEventDelegate;
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x0000648C File Offset: 0x0000468C
		private void Instance_PluginEventDelegate(string pluginName, PluginNotifyCategory category, string jsonData)
		{
			base.Dispatcher.Invoke(delegate ()
			{
				PluginNotifyCategory category2 = category;
				if (category2 == PluginNotifyCategory.Popup)
				{
					this.HandlePluginPopupData(pluginName, jsonData);
					return;
				}
				if (category2 != PluginNotifyCategory.SpeedBall)
				{
					return;
				}
				this.HnadleSpeedBall(jsonData);
			});
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x000064D4 File Offset: 0x000046D4
		private void HandlePluginPopupData(string pluginName, string jsonData)
		{
			JsonPopupReceivedData jsonPopupReceivedData = JsonHelper.StringToObject<JsonPopupReceivedData>(jsonData);
			if (jsonPopupReceivedData != null)
			{
				PopupData popupData = new PopupData();
				popupData.Content = jsonPopupReceivedData.content;
				popupData.Title = jsonPopupReceivedData.title;
				popupData.Duration = jsonPopupReceivedData.duration;
				if (jsonPopupReceivedData.logo_id == PopupLogoType.LogionZone)
				{
					popupData.Logo = new BitmapImage(new Uri("pack://application:,,,/images/legion_logo_40.png"));
				}
				popupData.StyleId = jsonPopupReceivedData.style_id;
				popupData.Action = jsonPopupReceivedData.action;
				popupData.ButtonText = jsonPopupReceivedData.button_text;
				popupData.PluginName = pluginName;
				this.ShowPopup(popupData);
			}
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00006568 File Offset: 0x00004768
		private void ShowPopup(PopupData data)
		{
			if (data != null && data.StyleId == PopupStyleType.Unknow)
			{
				return;
			}
			//PopupStyleType styleId = data.StyleId;
			//if (styleId != PopupStyleType.Style1)
			//{
			//	if (styleId != PopupStyleType.Style2)
			//	{
			//		return;
			//	}
			//	Style2Balloon balloon = new Style2Balloon(data);
			//	LogHelper.Log("[App Plugin] [ShowPopup] 显示样式2 弹窗");
			//	if (data.Duration == 0)
			//	{
			//		this.notifyIcon.ShowCustomBalloon(balloon, PopupAnimation.Slide, null);
			//		return;
			//	}
			//	this.notifyIcon.ShowCustomBalloon(balloon, PopupAnimation.Slide, new int?(data.Duration));
			//	return;
			//}
			//else
			//{
			//	Style1Balloon balloon2 = new Style1Balloon(data);
			//	LogHelper.Log("[App Plugin] [ShowPopup] 显示样式1 弹窗");
			//	if (data.Duration == 0)
			//	{
			//		this.notifyIcon.ShowCustomBalloon(balloon2, PopupAnimation.Slide, null);
			//		return;
			//	}
			//	this.notifyIcon.ShowCustomBalloon(balloon2, PopupAnimation.Slide, new int?(data.Duration));
			//	return;
			//}
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00006624 File Offset: 0x00004824
		private void HnadleSpeedBall(string jsonData)
		{
			JsonSpeedBallReceivedData jsonSpeedBallReceivedData = JsonHelper.StringToObject<JsonSpeedBallReceivedData>(jsonData);
			if (jsonSpeedBallReceivedData != null)
			{
				this.ShowSpeedBall(new SpeedBallData
				{
					Title = jsonSpeedBallReceivedData.title,
					Delay = jsonSpeedBallReceivedData.delay,
					Drop = jsonSpeedBallReceivedData.drop,
					Progress = jsonSpeedBallReceivedData.progress,
					Status = jsonSpeedBallReceivedData.status,
					Flowsum = jsonSpeedBallReceivedData.flowsum
				});
			}
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00006690 File Offset: 0x00004890
		private void ShowSpeedBall(SpeedBallData data)
		{
			if (data != null && data.Status == SpeedBallStauts.Open)
			{
				LogHelper.Log("[App Plugin] [ShowSpeedBall] 显示加速球");
				//new WindowSpeedBall(data).Show();
			}
		}

		// Token: 0x060000FA RID: 250 RVA: 0x000066B3 File Offset: 0x000048B3
		private void ExecutePluginDllsMethod(string pluginName, string jsonData)
		{
			PluginDlls.Instance.Execute(pluginName, jsonData);
		}

		//// Token: 0x060000FB RID: 251 RVA: 0x000066C4 File Offset: 0x000048C4
		//[DebuggerNonUserCode]
		//[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		//public void InitializeComponent()
		//{
		//	if (this._contentLoaded)
		//	{
		//		return;
		//	}
		//	this._contentLoaded = true;
		//	Uri resourceLocator = new Uri("/LZTray;component/app.xaml", UriKind.Relative);
		//	Application.LoadComponent(this, resourceLocator);
		//}

		// Token: 0x060000FC RID: 252 RVA: 0x000066F4 File Offset: 0x000048F4
		//[DebuggerNonUserCode]
		//[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		//[EditorBrowsable(EditorBrowsableState.Never)]
		// void IComponentConnector.Connect(int connectionId, object target)
		//{
		//	switch (connectionId)
		//	{
		//		case 1:
		//			((TaskbarIcon)target).TrayLeftMouseDown += this.MyNotifyIcon_TrayLeftMouseDown;
		//			return;
		//		case 2:
		//			((MenuItem)target).Click += this.Show_LZMain_MenuItem_Click;
		//			return;
		//		case 3:
		//			((MenuItem)target).Click += this.Exit_MenuItem_Click;
		//			return;
		//		default:
		//			this._contentLoaded = true;
		//			return;
		//	}
		//}

		// Token: 0x060000FD RID: 253 RVA: 0x00006766 File Offset: 0x00004966
		//[STAThread]
		//[DebuggerNonUserCode]
		//[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		//public static void Main()
		//{
		//	//App app = new App();
		//	//app.InitializeComponent();
		//	//app.Run();
		//}

		// Token: 0x0400007E RID: 126
		public const string LegionZoneFileName = "LegionZone.exe";

		// Token: 0x0400007F RID: 127
		private DispatcherTimer updateTimer;

		// Token: 0x04000080 RID: 128
		private DispatcherTimer autoReportTimer;

		// Token: 0x04000081 RID: 129
		public Message message;

		// Token: 0x04000082 RID: 130
		private TaskbarIcon notifyIcon;

		// Token: 0x04000083 RID: 131
		private List<GameInfo> listStartedGame;

		// Token: 0x04000084 RID: 132
		private ExternalDeviceInfoList listExternalDevice;

		// Token: 0x04000085 RID: 133
		private CancellationTokenSource tokenSource;

		// Token: 0x04000086 RID: 134
		private static Mutex mutex;

		// Token: 0x04000087 RID: 135
		private bool isStopLenovoOneService = true;

		// Token: 0x04000088 RID: 136
		//private bool _contentLoaded;
	}
}
