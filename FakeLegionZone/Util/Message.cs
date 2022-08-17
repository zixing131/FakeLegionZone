 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Interop;
using FakeLegionZone.Common;
using FakeLegionZone.Model;
using FakeLegionZone.Plugin;

namespace FakeLegionZone.Util
{
	// Token: 0x0200002A RID: 42
	public class Message
	{
		// Token: 0x1400000B RID: 11
		// (add) Token: 0x06000104 RID: 260 RVA: 0x000068A4 File Offset: 0x00004AA4
		// (remove) Token: 0x06000105 RID: 261 RVA: 0x000068DC File Offset: 0x00004ADC
		public event GameInsideHookStartDelegate GameInsideHookStartEvent;

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x06000106 RID: 262 RVA: 0x00006914 File Offset: 0x00004B14
		// (remove) Token: 0x06000107 RID: 263 RVA: 0x0000694C File Offset: 0x00004B4C
		public event GameInsideInfobarShowedDelegate GameInsideInfobarShowedEvent;

		// Token: 0x1400000D RID: 13
		// (add) Token: 0x06000108 RID: 264 RVA: 0x00006984 File Offset: 0x00004B84
		// (remove) Token: 0x06000109 RID: 265 RVA: 0x000069BC File Offset: 0x00004BBC
		public event GameInsideInfobarHidedDelegate GameInsideInfobarHidedEvent;

		// Token: 0x1400000E RID: 14
		// (add) Token: 0x0600010A RID: 266 RVA: 0x000069F4 File Offset: 0x00004BF4
		// (remove) Token: 0x0600010B RID: 267 RVA: 0x00006A2C File Offset: 0x00004C2C
		public event GameInsideChangeGameModeDelegate GameInsideChangeGameModeEvent;

		// Token: 0x1400000F RID: 15
		// (add) Token: 0x0600010C RID: 268 RVA: 0x00006A64 File Offset: 0x00004C64
		// (remove) Token: 0x0600010D RID: 269 RVA: 0x00006A9C File Offset: 0x00004C9C
		public event GameInsideCleanMemoryDelegate GameInsideCleanMemoryEvent;

		// Token: 0x14000010 RID: 16
		// (add) Token: 0x0600010E RID: 270 RVA: 0x00006AD4 File Offset: 0x00004CD4
		// (remove) Token: 0x0600010F RID: 271 RVA: 0x00006B0C File Offset: 0x00004D0C
		public event GameInsideImageSnapedDelegate GameInsideImageSnapedEvent;

		// Token: 0x14000011 RID: 17
		// (add) Token: 0x06000110 RID: 272 RVA: 0x00006B44 File Offset: 0x00004D44
		// (remove) Token: 0x06000111 RID: 273 RVA: 0x00006B7C File Offset: 0x00004D7C
		public event GameInsideReportDelegate GameInsideReportEvent;

		// Token: 0x14000012 RID: 18
		// (add) Token: 0x06000112 RID: 274 RVA: 0x00006BB4 File Offset: 0x00004DB4
		// (remove) Token: 0x06000113 RID: 275 RVA: 0x00006BEC File Offset: 0x00004DEC
		public event FromSelfMessageDelegate FromSelfMessageEvent;

		// Token: 0x14000013 RID: 19
		// (add) Token: 0x06000114 RID: 276 RVA: 0x00006C24 File Offset: 0x00004E24
		// (remove) Token: 0x06000115 RID: 277 RVA: 0x00006C5C File Offset: 0x00004E5C
		public event ExitMessageDelegate ExitMessageEvent;

		// Token: 0x06000116 RID: 278 RVA: 0x00006C94 File Offset: 0x00004E94
		public Message(Window window)
		{
			WindowInteropHelper windowInteropHelper = new WindowInteropHelper(window);
			this.wpfHwnd = windowInteropHelper.Handle;
			HwndSource.FromHwnd(this.wpfHwnd).AddHook(new HwndSourceHook(this.WndProc));
			CHANGEFILTERSTRUCT structure = default(CHANGEFILTERSTRUCT);
			structure.size = (uint)Marshal.SizeOf<CHANGEFILTERSTRUCT>(structure);
			structure.info = 0U;
			NativesApi.ChangeWindowMessageFilterEx(this.wpfHwnd, 74, 1, ref structure);
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00006D10 File Offset: 0x00004F10
		private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
		{
			if (msg == 74)
			{
				try
				{
					this.HandleMessage(wParam, lParam);
					goto IL_3C;
				}
				catch (Exception ex)
				{
					LogHelper.Log(string.Format("[Message] [WndProc] 处理从游戏接收到的消息时失败：handle = {0} \t ex = {1}。", wParam, ex.Message));
					goto IL_3C;
				}
			}
			if (msg == 1040)
			{
				this.HandleExitMessage();
			}
		IL_3C:
			return IntPtr.Zero;
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00006D70 File Offset: 0x00004F70
		public void SendCleanResultMessage(int size, int dram)
		{
			LZ_MSG_CLEAN_RESULT structure = new LZ_MSG_CLEAN_RESULT
			{
				CleanedMemory = size,
				Dram = dram
			};
			int num = Marshal.SizeOf<LZ_MSG_CLEAN_RESULT>(structure);
			IntPtr intPtr = Marshal.AllocHGlobal(num);
			Marshal.StructureToPtr<LZ_MSG_CLEAN_RESULT>(structure, intPtr, true);
			CopyDataStruct cds;
			cds.dwData = 8;
			cds.lpData = intPtr;
			cds.cbData = num;
			LogHelper.Log(string.Format("[Message] [SendCleanResultMessage] 发送清理内存结果消息到游戏内：size = {0}。", size));
			this.SendMessageToGameInside(cds);
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00006DE0 File Offset: 0x00004FE0
		public void SendSaveImageResultMessage(int result)
		{
			LZ_MSG_SAVEIMAGE_RESULT structure = new LZ_MSG_SAVEIMAGE_RESULT
			{
				Result = result
			};
			int num = Marshal.SizeOf<LZ_MSG_SAVEIMAGE_RESULT>(structure);
			IntPtr intPtr = Marshal.AllocHGlobal(num);
			Marshal.StructureToPtr<LZ_MSG_SAVEIMAGE_RESULT>(structure, intPtr, true);
			CopyDataStruct cds;
			cds.dwData = 11;
			cds.lpData = intPtr;
			cds.cbData = num;
			LogHelper.Log(string.Format("[Message] [SendSaveImageResultMessage] 发送保存截图结果消息到游戏内：result = {0}。", result));
			this.SendMessageToGameInside(cds);
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00006E48 File Offset: 0x00005048
		public void SendChangeModeMessage()
		{
			LZ_MSG_GAME_MODE structure = new LZ_MSG_GAME_MODE
			{
				CurGameMode = (int)GlobalCurrentStatus.CurrentGameMode
			};
			int num = Marshal.SizeOf<LZ_MSG_GAME_MODE>(structure);
			IntPtr intPtr = Marshal.AllocHGlobal(num);
			Marshal.StructureToPtr<LZ_MSG_GAME_MODE>(structure, intPtr, true);
			CopyDataStruct cds;
			cds.dwData = 5;
			cds.lpData = intPtr;
			cds.cbData = num;
			LogHelper.Log(string.Format("[Message] [SendChangeModeMessage] 发送游戏模式消息到游戏内：game_mode = {0}。", GlobalCurrentStatus.CurrentGameMode));
			this.SendMessageToGameInside(cds);
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00006EB8 File Offset: 0x000050B8
		public void SendHardwareInfoMessage(LZ_MSG_DEVICE_INFO deviceInfo)
		{
			int num = Marshal.SizeOf<LZ_MSG_DEVICE_INFO>(deviceInfo);
			IntPtr intPtr = Marshal.AllocHGlobal(num);
			Marshal.StructureToPtr<LZ_MSG_DEVICE_INFO>(deviceInfo, intPtr, true);
			CopyDataStruct cds;
			cds.dwData = 4;
			cds.lpData = intPtr;
			cds.cbData = num;
			if (CommandLine.CommandLineArgs.Contains("--enable-hardware-info-changed-log"))
			{
				LogHelper.Log(string.Format("[Message] [SendHardwareInfoMessage] 发送设备信息消息到游戏内：cpu_percent = {0}, cpu_freq = {1}, cpu_temperature = {2}, gpu_percent = {3}, gpu_freq = {4}, gpu_temperature = {5}, power = {6}, power_mode = {7}。", new object[] { deviceInfo.CpuPercent, deviceInfo.CpuFreq, deviceInfo.CpuTemperature, deviceInfo.GpuPrecent, deviceInfo.GpuFreq, deviceInfo.GpuTemperature, deviceInfo.Power, deviceInfo.PowerMode }));
			}
			this.SendMessageToGameInside(cds);
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00006F94 File Offset: 0x00005194
		private void SendSupportedGameModeMessage()
		{
			int[] array = new int[5];
			int num = ((GlobalCurrentStatus.SupportedGameMode.Count < array.Length) ? GlobalCurrentStatus.SupportedGameMode.Count : array.Length);
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < num; i++)
			{
				array[i] = (int)GlobalCurrentStatus.SupportedGameMode[i];
				stringBuilder.AppendFormat("{0}: {1}; ", array[i], GlobalCurrentStatus.SupportedGameMode[i]);
			}
			LZ_MSG_GAMEMODES_RESULT structure = new LZ_MSG_GAMEMODES_RESULT
			{
				mode_size = num,
				modes = array
			};
			int num2 = Marshal.SizeOf<LZ_MSG_GAMEMODES_RESULT>(structure);
			IntPtr intPtr = Marshal.AllocHGlobal(num2);
			Marshal.StructureToPtr<LZ_MSG_GAMEMODES_RESULT>(structure, intPtr, true);
			CopyDataStruct cds;
			cds.dwData = 13;
			cds.lpData = intPtr;
			cds.cbData = num2;
			LogHelper.Log(string.Format("[Message] [SendSupportedGameModeMessage] 发送本机支持的游戏模式消息到游戏内：mode_size = {0}, modes = {1}。", num, stringBuilder));
			this.SendMessageToGameInside(cds);
		}

		// Token: 0x0600011D RID: 285 RVA: 0x0000707C File Offset: 0x0000527C
		public void SendExitMessage()
		{
			CopyDataStruct cds;
			cds.dwData = 10;
			cds.lpData = IntPtr.Zero;
			cds.cbData = 0;
			LogHelper.Log("[Message] [SendExitMessage] 发送托盘程序退出消息到游戏内。");
			this.SendMessageToGameInside(cds);
		}

		// Token: 0x0600011E RID: 286 RVA: 0x000070B8 File Offset: 0x000052B8
		public void SendExitMessageToLZMain()
		{
			IntPtr intPtr = NativesApi.FindWindow("LEGionZone_MicroCoreClass", "LegionZone");
			if (intPtr != IntPtr.Zero)
			{
				this.SendMessage(intPtr, 1132);
			}
		}

		// Token: 0x0600011F RID: 287 RVA: 0x000070F0 File Offset: 0x000052F0
		private void SendMessageToGameInside(CopyDataStruct cds)
		{
			foreach (Message.GameInsideMessage gameInsideMessage in this.listGameHandler)
			{
				this.SendMessage(gameInsideMessage.Handler, cds);
			}
		}

		// Token: 0x06000120 RID: 288 RVA: 0x0000714C File Offset: 0x0000534C
		private void SendMessage(IntPtr handler, CopyDataStruct cds)
		{
			try
			{
				if (handler != IntPtr.Zero)
				{
					NativesApi.SendMessage(handler, 74, this.wpfHwnd, ref cds);
				}
				else
				{
					LogHelper.Log("[Message] [SendMessage] 无法发送消息到游戏内，未找到句柄。");
				}
			}
			catch (Exception ex)
			{
				LogHelper.Log("[Message] [SendMessage] 向游戏内发送消息异常：ex = " + ex.Message + "。");
			}
		}

		// Token: 0x06000121 RID: 289 RVA: 0x000071B4 File Offset: 0x000053B4
		public void SendMessage(IntPtr handler, int msg)
		{
			try
			{
				if (handler != IntPtr.Zero)
				{
					NativesApi.SendMessage(handler, msg, this.wpfHwnd, 0);
				}
				else
				{
					LogHelper.Log("[Message] [SendMessage] 无法发送消息到游戏内，未找到句柄。");
				}
			}
			catch (Exception ex)
			{
				LogHelper.Log("[Message] [SendMessage] 向游戏内发送消息异常：ex = " + ex.Message + "。");
			}
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00007218 File Offset: 0x00005418
		private void HandleMessage(IntPtr wParam, IntPtr lParam)
		{
			CopyDataStruct copyDataStruct;
			try
			{
				copyDataStruct = (CopyDataStruct)Marshal.PtrToStructure(lParam, typeof(CopyDataStruct));
			}
			catch (Exception ex)
			{
				LogHelper.Log(string.Format("[Message] [HandleMessage] 处理从游戏接收到的消息时，解析 CopyDataStruct 异常：hwnd = {0} \t ex = {1}。", wParam, ex.Message));
				return;
			}
			MessageType dwData = (MessageType)copyDataStruct.dwData;
			switch (dwData)
			{
				case MessageType.LZ_MESSAGE_HOOK_START:
					if (this.CheckMessageSize(Marshal.SizeOf(typeof(LZ_MSG_HOOK_START)), copyDataStruct.cbData))
					{
						this.HandleHookStartMessage(wParam, copyDataStruct.lpData);
						return;
					}
					LogHelper.Log(string.Format("[Message] [HandleMessage] LZ_MSG_HOOK_START 接收到的数据结构 size 错误：hwnd = {0}, size = {1}, wrong_size = {2}。", wParam, Marshal.SizeOf(typeof(LZ_MSG_HOOK_START)), copyDataStruct.cbData));
					return;
				case MessageType.LZ_MESSAGE_INFOBAR_SHOWED:
					this.HandleInfobarShowedMessage(wParam, copyDataStruct.lpData);
					return;
				case MessageType.LZ_MESSAGE_INFOBAR_HIDED:
					this.HandleInfobarHidedMessage(wParam, copyDataStruct.lpData);
					return;
				case MessageType.LZ_MESSAGE_DEVICE_INFO:
				case MessageType.LZ_MESSAGE_NOTIFY_GAME_MODE:
				case MessageType.LZ_MESSAGE_MEMORY_CLEANED:
				case MessageType.LZ_MESSAGE_TRAY_EXITED:
				case MessageType.LZ_MESSAGE_SAVEIMAGE_RESULT:
				case MessageType.LZ_MESSAGE_GET_GAMEMODES_RESULT:
					break;
				case MessageType.LZ_MESSAGE_CHANGE_GAME_MODE:
					if (this.CheckMessageSize(Marshal.SizeOf(typeof(LZ_MSG_CHANGE_MODE)), copyDataStruct.cbData))
					{
						this.HandleChangeGameModeMessage(wParam, copyDataStruct.lpData);
						return;
					}
					LogHelper.Log(string.Format("[Message] [HandleMessage] LZ_MSG_CHANGE_MODE 接收到的数据结构 size 错误：hwnd = {0}, size = {1}, wrong_size = {2}。", wParam, Marshal.SizeOf(typeof(LZ_MSG_CHANGE_MODE)), copyDataStruct.cbData));
					return;
				case MessageType.LZ_MESSAGE_CLEAN_MEMORY:
					this.HandleCleanMemoryMessage(wParam, copyDataStruct.lpData);
					return;
				case MessageType.LZ_MESSAGE_IMAGE_SNAPED:
					if (this.CheckMessageSize(Marshal.SizeOf(typeof(LZ_MSG_IMAGE_SNAPED)), copyDataStruct.cbData))
					{
						this.HandleImageSnapedMessage(wParam, copyDataStruct.lpData);
						return;
					}
					LogHelper.Log(string.Format("[Message] [HandleMessage] LZ_MSG_IMAGE_SNAPED 接收到的数据结构 size 错误：hwnd = {0}, size = {1}, wrong_size = {2}。", wParam, Marshal.SizeOf(typeof(LZ_MSG_IMAGE_SNAPED)), copyDataStruct.cbData));
					return;
				case MessageType.LZ_MESSAGE_GET_GAMEMODES:
					this.HandleGetGameModesMessage(wParam, copyDataStruct.lpData);
					return;
				case MessageType.LZ_MESSAGE_REPORT:
					if (this.CheckMessageSize(Marshal.SizeOf(typeof(LZ_MSG_REPORT)), copyDataStruct.cbData))
					{
						this.HandleReportMessage(wParam, copyDataStruct.lpData);
						return;
					}
					LogHelper.Log(string.Format("[Message] [HandleMessage] LZ_MSG_REPORT 接收到的数据结构 size 错误：hwnd = {0}, size = {1}, wrong_size = {2}。", wParam, Marshal.SizeOf(typeof(LZ_MSG_REPORT)), copyDataStruct.cbData));
					return;
				default:
					if (dwData != MessageType.LZ_MESSAGE_SELF_RUN_LZMAIN)
					{
						return;
					}
					this.HandleFromSelfMessage(wParam, copyDataStruct.lpData);
					break;
			}
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00007484 File Offset: 0x00005684
		private void HandleHookStartMessage(IntPtr wParam, IntPtr lpData)
		{
			if (lpData == IntPtr.Zero)
			{
				LogHelper.Log(string.Format("[Message] [HandleHookStartMessage] 处理从游戏接收到注入成功的消息时，lpData = IntPtr.Zero 为 ：hwnd = {0}。", wParam));
				return;
			}
			LZ_MSG_HOOK_START lz_MSG_HOOK_START;
			try
			{
				lz_MSG_HOOK_START = (LZ_MSG_HOOK_START)Marshal.PtrToStructure(lpData, typeof(LZ_MSG_HOOK_START));
			}
			catch (Exception ex)
			{
				LogHelper.Log(string.Format("[Message] [HandleHookStartMessage] 处理从游戏接收到的消息时，解析 LZ_MSG_HOOK_START 异常：hwnd = {0} \t ex = {1}。", wParam, ex.Message));
				return;
			}
			Message.GameInsideMessage gameInsideMessage = new Message.GameInsideMessage
			{
				ProcessId = (int)lz_MSG_HOOK_START.ProcessId,
				Handler = wParam
			};
			if (this.listGameHandler.FirstOrDefault((Message.GameInsideMessage p) => p.Handler == gameInsideMessage.Handler) == null)
			{
				this.listGameHandler.Add(gameInsideMessage);
			}
			GameInsideHookStartDelegate gameInsideHookStartEvent = this.GameInsideHookStartEvent;
			if (gameInsideHookStartEvent == null)
			{
				return;
			}
			gameInsideHookStartEvent(new MessageEventArgs<LZ_MSG_HOOK_START>(lz_MSG_HOOK_START)
			{
				Handler = wParam
			});
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00007568 File Offset: 0x00005768
		private void HandleInfobarShowedMessage(IntPtr wParam, IntPtr lpData)
		{
			GameInsideInfobarShowedDelegate gameInsideInfobarShowedEvent = this.GameInsideInfobarShowedEvent;
			if (gameInsideInfobarShowedEvent == null)
			{
				return;
			}
			gameInsideInfobarShowedEvent(new MessageEventArgs
			{
				Handler = wParam
			});
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00007586 File Offset: 0x00005786
		private void HandleInfobarHidedMessage(IntPtr wParam, IntPtr lpData)
		{
			GameInsideInfobarHidedDelegate gameInsideInfobarHidedEvent = this.GameInsideInfobarHidedEvent;
			if (gameInsideInfobarHidedEvent == null)
			{
				return;
			}
			gameInsideInfobarHidedEvent(new MessageEventArgs
			{
				Handler = wParam
			});
		}

		// Token: 0x06000126 RID: 294 RVA: 0x000075A4 File Offset: 0x000057A4
		private void HandleChangeGameModeMessage(IntPtr wParam, IntPtr lpData)
		{
			if (lpData == IntPtr.Zero)
			{
				LogHelper.Log(string.Format("[Message] [HandleChangeGameModeMessage] 处理从游戏接收到修改游戏模式的消息时，lpData = IntPtr.Zero 为 ：hwnd = {0}。", wParam));
				return;
			}
			LZ_MSG_CHANGE_MODE eventData;
			try
			{
				eventData = (LZ_MSG_CHANGE_MODE)Marshal.PtrToStructure(lpData, typeof(LZ_MSG_CHANGE_MODE));
			}
			catch (Exception ex)
			{
				LogHelper.Log(string.Format("[Message] [HandleChangeGameModeMessage] 处理从游戏接收到的消息时，解析 LZ_MSG_CHANGE_MODE 异常：hwnd = {0} \t ex = {1}。", wParam, ex.Message));
				return;
			}
			GameInsideChangeGameModeDelegate gameInsideChangeGameModeEvent = this.GameInsideChangeGameModeEvent;
			if (gameInsideChangeGameModeEvent == null)
			{
				return;
			}
			gameInsideChangeGameModeEvent(new MessageEventArgs<LZ_MSG_CHANGE_MODE>(eventData)
			{
				Handler = wParam
			});
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00007638 File Offset: 0x00005838
		private void HandleCleanMemoryMessage(IntPtr wParam, IntPtr lpData)
		{
			GameInsideCleanMemoryDelegate gameInsideCleanMemoryEvent = this.GameInsideCleanMemoryEvent;
			if (gameInsideCleanMemoryEvent == null)
			{
				return;
			}
			gameInsideCleanMemoryEvent(new MessageEventArgs
			{
				Handler = wParam
			});
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00007658 File Offset: 0x00005858
		private void HandleImageSnapedMessage(IntPtr wParam, IntPtr lpData)
		{
			if (lpData == IntPtr.Zero)
			{
				LogHelper.Log(string.Format("[Message] [HandleImageSnapedMessage] 处理从游戏接收到游戏截图成功的消息时，lpData = IntPtr.Zero 为 ：hwnd = {0}。", wParam));
				return;
			}
			LZ_MSG_IMAGE_SNAPED eventData;
			try
			{
				eventData = (LZ_MSG_IMAGE_SNAPED)Marshal.PtrToStructure(lpData, typeof(LZ_MSG_IMAGE_SNAPED));
			}
			catch (Exception ex)
			{
				LogHelper.Log(string.Format("[Message] [HandleImageSnapedMessage] 处理从游戏接收到的消息时，解析 LZ_MSG_IMAGE_SNAPED 异常：hwnd = {0} \t ex = {1}。", wParam, ex.Message));
				return;
			}
			GameInsideImageSnapedDelegate gameInsideImageSnapedEvent = this.GameInsideImageSnapedEvent;
			if (gameInsideImageSnapedEvent == null)
			{
				return;
			}
			gameInsideImageSnapedEvent(new MessageEventArgs<LZ_MSG_IMAGE_SNAPED>(eventData)
			{
				Handler = wParam
			});
		}

		// Token: 0x06000129 RID: 297 RVA: 0x000076EC File Offset: 0x000058EC
		private void HandleGetGameModesMessage(IntPtr wParam, IntPtr lpData)
		{
			LogHelper.Log(string.Format("[Message] [HandleGetGameModesMessage] 收到游戏内获取本机支持的游戏模式消息事件：handler = {0}", wParam));
			ThreadPool.QueueUserWorkItem(delegate (object state)
			{
				this.SendSupportedGameModeMessage();
			});
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00007718 File Offset: 0x00005918
		private void HandleReportMessage(IntPtr wParam, IntPtr lpData)
		{
			if (lpData == IntPtr.Zero)
			{
				LogHelper.Log(string.Format("[Message] [HandleReportMessage] 处理从游戏接收到数据打点的消息时，lpData = IntPtr.Zero 为 ：hwnd = {0}。", wParam));
				return;
			}
			LZ_MSG_REPORT eventData;
			try
			{
				eventData = (LZ_MSG_REPORT)Marshal.PtrToStructure(lpData, typeof(LZ_MSG_REPORT));
			}
			catch (Exception ex)
			{
				LogHelper.Log(string.Format("[Message] [HandleReportMessage] 处理从游戏接收到数据打点的消息时，解析 LZ_MSG_REPORT 异常：hwnd = {0} \t ex = {1}。", wParam, ex.Message));
				return;
			}
			GameInsideReportDelegate gameInsideReportEvent = this.GameInsideReportEvent;
			if (gameInsideReportEvent == null)
			{
				return;
			}
			gameInsideReportEvent(new MessageEventArgs<LZ_MSG_REPORT>(eventData)
			{
				Handler = wParam
			});
		}

		// Token: 0x0600012B RID: 299 RVA: 0x000077AC File Offset: 0x000059AC
		private void HandleFromSelfMessage(IntPtr wParam, IntPtr lpData)
		{
			if (lpData == IntPtr.Zero)
			{
				LogHelper.Log(string.Format("[Message] [HandleFromSelfMessage] 处理从自身多实例进程发送来的消息时，lpData = IntPtr.Zero 为 ：hwnd = {0}。", wParam));
				return;
			}
			LZ_MSG_SELF_RUN_LZMAIN eventData;
			try
			{
				eventData = (LZ_MSG_SELF_RUN_LZMAIN)Marshal.PtrToStructure(lpData, typeof(LZ_MSG_SELF_RUN_LZMAIN));
			}
			catch (Exception ex)
			{
				LogHelper.Log(string.Format("[Message] [HandleFromSelfMessage] 处理从自身多实例进程发送来的消息时，解析 LZ_MSG_SELF_RUN_LZMAIN 异常：hwnd = {0} \t ex = {1}。", wParam, ex.Message));
				return;
			}
			FromSelfMessageDelegate fromSelfMessageEvent = this.FromSelfMessageEvent;
			if (fromSelfMessageEvent == null)
			{
				return;
			}
			fromSelfMessageEvent(new MessageEventArgs<LZ_MSG_SELF_RUN_LZMAIN>(eventData)
			{
				Handler = wParam
			});
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00007840 File Offset: 0x00005A40
		private void HandleExitMessage()
		{
			LogHelper.Log("[Message] [HandlerExitMessage] 收到程序退出消息事件");
			ExitMessageDelegate exitMessageEvent = this.ExitMessageEvent;
			if (exitMessageEvent == null)
			{
				return;
			}
			exitMessageEvent();
		}

		// Token: 0x0600012D RID: 301 RVA: 0x0000785C File Offset: 0x00005A5C
		private bool CheckMessageSize(int originalSize, int receivedSize)
		{
			return originalSize == receivedSize;
		}

		// Token: 0x0400008A RID: 138
		private const int WM_COPYDATA = 74;

		// Token: 0x0400008B RID: 139
		private const int EXIT_MSG = 1040;

		// Token: 0x04000095 RID: 149
		private List<Message.GameInsideMessage> listGameHandler = new List<Message.GameInsideMessage>();

		// Token: 0x04000096 RID: 150
		private readonly IntPtr wpfHwnd;

		// Token: 0x020000C5 RID: 197
		public class GameInsideMessage
		{
			// Token: 0x170000FA RID: 250
			// (get) Token: 0x060004FE RID: 1278 RVA: 0x00011B1F File Offset: 0x0000FD1F
			// (set) Token: 0x060004FF RID: 1279 RVA: 0x00011B27 File Offset: 0x0000FD27
			public int ProcessId { get; set; }

			// Token: 0x170000FB RID: 251
			// (get) Token: 0x06000500 RID: 1280 RVA: 0x00011B30 File Offset: 0x0000FD30
			// (set) Token: 0x06000501 RID: 1281 RVA: 0x00011B38 File Offset: 0x0000FD38
			public IntPtr Handler { get; set; }
		}
	}
}
