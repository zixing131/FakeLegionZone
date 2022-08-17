using FakeLegionZone.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace FakeLegionZone.Model
{
	public class GameCleanMemoryCompletedReceivedData
	{
		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000370 RID: 880 RVA: 0x0000C51A File Offset: 0x0000A71A
		// (set) Token: 0x06000371 RID: 881 RVA: 0x0000C522 File Offset: 0x0000A722
		public int clearMemorySize { get; set; }

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000372 RID: 882 RVA: 0x0000C52B File Offset: 0x0000A72B
		// (set) Token: 0x06000373 RID: 883 RVA: 0x0000C533 File Offset: 0x0000A733
		public int Dram { get; set; }
	}
	public class CpuInfo
	{
		// Token: 0x170000AA RID: 170
		// (get) Token: 0x0600033A RID: 826 RVA: 0x0000C351 File Offset: 0x0000A551
		// (set) Token: 0x0600033B RID: 827 RVA: 0x0000C359 File Offset: 0x0000A559
		public double CurFreq { get; set; }

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x0600033C RID: 828 RVA: 0x0000C362 File Offset: 0x0000A562
		// (set) Token: 0x0600033D RID: 829 RVA: 0x0000C36A File Offset: 0x0000A56A
		public int LoadPercent { get; set; }

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x0600033E RID: 830 RVA: 0x0000C373 File Offset: 0x0000A573
		// (set) Token: 0x0600033F RID: 831 RVA: 0x0000C37B File Offset: 0x0000A57B
		public double Temperature { get; set; }

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000340 RID: 832 RVA: 0x0000C384 File Offset: 0x0000A584
		// (set) Token: 0x06000341 RID: 833 RVA: 0x0000C38C File Offset: 0x0000A58C
		public int Fan_Speed { get; set; }

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000342 RID: 834 RVA: 0x0000C395 File Offset: 0x0000A595
		// (set) Token: 0x06000343 RID: 835 RVA: 0x0000C39D File Offset: 0x0000A59D
		public int Fan_Speed_Max { get; set; }

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000344 RID: 836 RVA: 0x0000C3A6 File Offset: 0x0000A5A6
		// (set) Token: 0x06000345 RID: 837 RVA: 0x0000C3AE File Offset: 0x0000A5AE
		public int Power { get; set; }

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000346 RID: 838 RVA: 0x0000C3B7 File Offset: 0x0000A5B7
		// (set) Token: 0x06000347 RID: 839 RVA: 0x0000C3BF File Offset: 0x0000A5BF
		public int FreqMax { get; set; }
	}


	// Token: 0x02000075 RID: 117
	public class GpuInfo
	{
		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000349 RID: 841 RVA: 0x0000C3D0 File Offset: 0x0000A5D0
		// (set) Token: 0x0600034A RID: 842 RVA: 0x0000C3D8 File Offset: 0x0000A5D8
		public int DeviceID { get; set; }

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x0600034B RID: 843 RVA: 0x0000C3E1 File Offset: 0x0000A5E1
		// (set) Token: 0x0600034C RID: 844 RVA: 0x0000C3E9 File Offset: 0x0000A5E9
		public double CurFreq { get; set; }

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x0600034D RID: 845 RVA: 0x0000C3F2 File Offset: 0x0000A5F2
		// (set) Token: 0x0600034E RID: 846 RVA: 0x0000C3FA File Offset: 0x0000A5FA
		public int LoadPercent { get; set; }

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x0600034F RID: 847 RVA: 0x0000C403 File Offset: 0x0000A603
		// (set) Token: 0x06000350 RID: 848 RVA: 0x0000C40B File Offset: 0x0000A60B
		public double Temperature { get; set; }

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000351 RID: 849 RVA: 0x0000C414 File Offset: 0x0000A614
		// (set) Token: 0x06000352 RID: 850 RVA: 0x0000C41C File Offset: 0x0000A61C
		public int Mem_Usage { get; set; }

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000353 RID: 851 RVA: 0x0000C425 File Offset: 0x0000A625
		// (set) Token: 0x06000354 RID: 852 RVA: 0x0000C42D File Offset: 0x0000A62D
		public int Fan_Speed { get; set; }

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000355 RID: 853 RVA: 0x0000C436 File Offset: 0x0000A636
		// (set) Token: 0x06000356 RID: 854 RVA: 0x0000C43E File Offset: 0x0000A63E
		public int Fan_Speed_Max { get; set; }

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000357 RID: 855 RVA: 0x0000C447 File Offset: 0x0000A647
		// (set) Token: 0x06000358 RID: 856 RVA: 0x0000C44F File Offset: 0x0000A64F
		public int Power { get; set; }

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000359 RID: 857 RVA: 0x0000C458 File Offset: 0x0000A658
		// (set) Token: 0x0600035A RID: 858 RVA: 0x0000C460 File Offset: 0x0000A660
		public int FreqMax { get; set; }
	}

	public class GameHardwareInfoChangedReceivedData
	{
		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000331 RID: 817 RVA: 0x0000C305 File Offset: 0x0000A505
		// (set) Token: 0x06000332 RID: 818 RVA: 0x0000C30D File Offset: 0x0000A50D
		public CpuInfo CPUInfo { get; set; }

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000333 RID: 819 RVA: 0x0000C316 File Offset: 0x0000A516
		// (set) Token: 0x06000334 RID: 820 RVA: 0x0000C31E File Offset: 0x0000A51E
		public List<GpuInfo> GPUInfo { get; set; }

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000335 RID: 821 RVA: 0x0000C327 File Offset: 0x0000A527
		// (set) Token: 0x06000336 RID: 822 RVA: 0x0000C32F File Offset: 0x0000A52F
		public int Dram { get; set; }

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000337 RID: 823 RVA: 0x0000C338 File Offset: 0x0000A538
		// (set) Token: 0x06000338 RID: 824 RVA: 0x0000C340 File Offset: 0x0000A540
		public int Dram_Freq { get; set; }
	}
	public enum ChangeModeSenderType
	{
		// Token: 0x04000028 RID: 40
		PluginDll,
		// Token: 0x04000029 RID: 41
		Tray,
		// Token: 0x0400002A RID: 42
		TrayRecovery,
		// Token: 0x0400002B RID: 43
		Toolkit,
		// Token: 0x0400002C RID: 44
		LenovoOneDevice
	}
	// Token: 0x0200000D RID: 13
	public enum GameMode
	{
		// Token: 0x04000039 RID: 57
		Unknow,
		// Token: 0x0400003A RID: 58
		Quiet,
		// Token: 0x0400003B RID: 59
		Balance,
		// Token: 0x0400003C RID: 60
		Beast,
		// Token: 0x0400003D RID: 61
		Customize = 255
	}
	public enum PowerMode
	{
		// Token: 0x0400005A RID: 90
		Unknow,
		// Token: 0x0400005B RID: 91
		AC,
		// Token: 0x0400005C RID: 92
		DC
	}
	public class GameBatteryModeChangedReceivedData
	{
		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x0600032C RID: 812 RVA: 0x0000C2DB File Offset: 0x0000A4DB
		// (set) Token: 0x0600032D RID: 813 RVA: 0x0000C2E3 File Offset: 0x0000A4E3
		public int BatteryRemaining { get; set; }

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x0600032E RID: 814 RVA: 0x0000C2EC File Offset: 0x0000A4EC
		// (set) Token: 0x0600032F RID: 815 RVA: 0x0000C2F4 File Offset: 0x0000A4F4
		public PowerMode PowerMode { get; set; }
	}
	public enum ExternalDeviceAction
	{
		// Token: 0x0400000A RID: 10
		Init,
		// Token: 0x0400000B RID: 11
		RealTime,
		// Token: 0x0400000C RID: 12
		Local,
		// Token: 0x0400000D RID: 13
		Unknow
	}
	public enum ExternalDeviceStatus
	{
		// Token: 0x04000006 RID: 6
		Out,
		// Token: 0x04000007 RID: 7
		In,
		// Token: 0x04000008 RID: 8
		Unknow
	}

	// Token: 0x0200005C RID: 92
	public class Device
	{
		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060002AD RID: 685 RVA: 0x0000BB10 File Offset: 0x00009D10
		// (set) Token: 0x060002AE RID: 686 RVA: 0x0000BB18 File Offset: 0x00009D18
		public string pid { get; set; }

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060002AF RID: 687 RVA: 0x0000BB21 File Offset: 0x00009D21
		// (set) Token: 0x060002B0 RID: 688 RVA: 0x0000BB29 File Offset: 0x00009D29
		public string vid { get; set; }

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060002B1 RID: 689 RVA: 0x0000BB32 File Offset: 0x00009D32
		// (set) Token: 0x060002B2 RID: 690 RVA: 0x0000BB3A File Offset: 0x00009D3A
		public ExternalDeviceStatus status { get; set; }
	}
	public class GameExternalDeviceChangedReceivedData
	{
		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060002A8 RID: 680 RVA: 0x0000BAE6 File Offset: 0x00009CE6
		// (set) Token: 0x060002A9 RID: 681 RVA: 0x0000BAEE File Offset: 0x00009CEE
		public ExternalDeviceAction action { get; set; }

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060002AA RID: 682 RVA: 0x0000BAF7 File Offset: 0x00009CF7
		// (set) Token: 0x060002AB RID: 683 RVA: 0x0000BAFF File Offset: 0x00009CFF
		public List<Device> EnumLZPeripheralDev { get; set; }
	}
	public class GameACPDModeChangedReceivedData
	{
		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000231 RID: 561 RVA: 0x0000B0CF File Offset: 0x000092CF
		// (set) Token: 0x06000232 RID: 562 RVA: 0x0000B0D7 File Offset: 0x000092D7
		public int PowerMode { get; set; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000233 RID: 563 RVA: 0x0000B0E0 File Offset: 0x000092E0
		// (set) Token: 0x06000234 RID: 564 RVA: 0x0000B0E8 File Offset: 0x000092E8
		public int Status { get; set; }
	}

	public class GameStartedAndEndedReceivedData
	{
		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000361 RID: 865 RVA: 0x0000C49B File Offset: 0x0000A69B
		// (set) Token: 0x06000362 RID: 866 RVA: 0x0000C4A3 File Offset: 0x0000A6A3
		public string ProcessName { get; set; }

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000363 RID: 867 RVA: 0x0000C4AC File Offset: 0x0000A6AC
		// (set) Token: 0x06000364 RID: 868 RVA: 0x0000C4B4 File Offset: 0x0000A6B4
		public string GameName { get; set; }

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000365 RID: 869 RVA: 0x0000C4BD File Offset: 0x0000A6BD
		// (set) Token: 0x06000366 RID: 870 RVA: 0x0000C4C5 File Offset: 0x0000A6C5
		public int ProcessId { get; set; }

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000367 RID: 871 RVA: 0x0000C4CE File Offset: 0x0000A6CE
		// (set) Token: 0x06000368 RID: 872 RVA: 0x0000C4D6 File Offset: 0x0000A6D6
		public int IsProcess64 { get; set; }

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000369 RID: 873 RVA: 0x0000C4DF File Offset: 0x0000A6DF
		// (set) Token: 0x0600036A RID: 874 RVA: 0x0000C4E7 File Offset: 0x0000A6E7
		public int ShowAssistant { get; set; }

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x0600036B RID: 875 RVA: 0x0000C4F0 File Offset: 0x0000A6F0
		// (set) Token: 0x0600036C RID: 876 RVA: 0x0000C4F8 File Offset: 0x0000A6F8
		public int SupportInjected { get; set; }

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x0600036D RID: 877 RVA: 0x0000C501 File Offset: 0x0000A701
		// (set) Token: 0x0600036E RID: 878 RVA: 0x0000C509 File Offset: 0x0000A709
		public int ChangeToPerformance { get; set; }
	}
	public class GameModeChangedReceivedData
	{
		// Token: 0x170000BA RID: 186
		// (get) Token: 0x0600035C RID: 860 RVA: 0x0000C471 File Offset: 0x0000A671
		// (set) Token: 0x0600035D RID: 861 RVA: 0x0000C479 File Offset: 0x0000A679
		public GameMode SmartFanMode { get; set; }

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x0600035E RID: 862 RVA: 0x0000C482 File Offset: 0x0000A682
		// (set) Token: 0x0600035F RID: 863 RVA: 0x0000C48A File Offset: 0x0000A68A
		public ChangeModeSenderType Executor { get; set; }
	}
	public class GameSupportedGameModeReceivedData
	{
		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060002A5 RID: 677 RVA: 0x0000BACD File Offset: 0x00009CCD
		// (set) Token: 0x060002A6 RID: 678 RVA: 0x0000BAD5 File Offset: 0x00009CD5
		public List<int> SupportThermalMode { get; set; }
	}
	// Token: 0x02000008 RID: 8
	public enum MType
	{
		// Token: 0x0400001E RID: 30
		NB,
		// Token: 0x0400001F RID: 31
		DT
	}

	public class GameSupportSmartFanReceivedData
	{
		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000239 RID: 569 RVA: 0x0000B112 File Offset: 0x00009312
		// (set) Token: 0x0600023A RID: 570 RVA: 0x0000B11A File Offset: 0x0000931A
		public SmartFanMode IsSupportSmartFan { get; set; }

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x0600023B RID: 571 RVA: 0x0000B123 File Offset: 0x00009323
		// (set) Token: 0x0600023C RID: 572 RVA: 0x0000B12B File Offset: 0x0000932B
		public MType MType { get; set; }
	}

	// Token: 0x0200000E RID: 14
	public enum SmartFanMode
	{
		// Token: 0x0400003F RID: 63
		ThermalMode_1,
		// Token: 0x04000040 RID: 64
		ThermalMode_2,
		// Token: 0x04000041 RID: 65
		ThermalMode_3,
		// Token: 0x04000042 RID: 66
		ThermalMode_4 = 4,
		// Token: 0x04000043 RID: 67
		ThermalMode_5
	}
	public class LudpParam
	{
		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000247 RID: 583 RVA: 0x0000B188 File Offset: 0x00009388
		// (set) Token: 0x06000248 RID: 584 RVA: 0x0000B190 File Offset: 0x00009390
		public string key { get; set; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000249 RID: 585 RVA: 0x0000B199 File Offset: 0x00009399
		// (set) Token: 0x0600024A RID: 586 RVA: 0x0000B1A1 File Offset: 0x000093A1
		public string value { get; set; }
	}

	public class LudpReceivedData
	{
		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600023E RID: 574 RVA: 0x0000B13C File Offset: 0x0000933C
		// (set) Token: 0x0600023F RID: 575 RVA: 0x0000B144 File Offset: 0x00009344
		public string category { get; set; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000240 RID: 576 RVA: 0x0000B14D File Offset: 0x0000934D
		// (set) Token: 0x06000241 RID: 577 RVA: 0x0000B155 File Offset: 0x00009355
		public string action { get; set; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000242 RID: 578 RVA: 0x0000B15E File Offset: 0x0000935E
		// (set) Token: 0x06000243 RID: 579 RVA: 0x0000B166 File Offset: 0x00009366
		public string label { get; set; }

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000244 RID: 580 RVA: 0x0000B16F File Offset: 0x0000936F
		// (set) Token: 0x06000245 RID: 581 RVA: 0x0000B177 File Offset: 0x00009377
		public List<LudpParam> param { get; set; }
	}
	
	// Token: 0x0200004E RID: 78
	public class GameTellInfoReceivedData
	{
		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000236 RID: 566 RVA: 0x0000B0F9 File Offset: 0x000092F9
		// (set) Token: 0x06000237 RID: 567 RVA: 0x0000B101 File Offset: 0x00009301
		public int MLSupport { get; set; }
	}
	public class MessageEventArgs : EventArgs
	{
		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060002B5 RID: 693 RVA: 0x0000BB53 File Offset: 0x00009D53
		// (set) Token: 0x060002B6 RID: 694 RVA: 0x0000BB5B File Offset: 0x00009D5B
		public IntPtr Handler { get; set; }
	}
	// Token: 0x0200005E RID: 94
	public class MessageEventArgs<T> : MessageEventArgs
	{
		// Token: 0x060002B7 RID: 695 RVA: 0x0000BB64 File Offset: 0x00009D64
		public MessageEventArgs(T eventData)
		{
			this.EventData = eventData;
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060002B8 RID: 696 RVA: 0x0000BB73 File Offset: 0x00009D73
		// (set) Token: 0x060002B9 RID: 697 RVA: 0x0000BB7B File Offset: 0x00009D7B
		public T EventData { get; set; }
	}
	public struct LZ_MSG_HOOK_START
	{
		// Token: 0x0400017D RID: 381
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
		public string GpuDesc;

		// Token: 0x0400017E RID: 382
		public uint GpuVendorId;

		// Token: 0x0400017F RID: 383
		public uint GpuDeviceId;

		// Token: 0x04000180 RID: 384
		public uint ProcessId;
	}

	public class DetailsInfo
	{
		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600025A RID: 602 RVA: 0x0000B2E1 File Offset: 0x000094E1
		// (set) Token: 0x0600025B RID: 603 RVA: 0x0000B2E9 File Offset: 0x000094E9
		public string support_model { get; set; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600025C RID: 604 RVA: 0x0000B2F2 File Offset: 0x000094F2
		// (set) Token: 0x0600025D RID: 605 RVA: 0x0000B2FA File Offset: 0x000094FA
		public string perf_major { get; set; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600025E RID: 606 RVA: 0x0000B303 File Offset: 0x00009503
		// (set) Token: 0x0600025F RID: 607 RVA: 0x0000B30B File Offset: 0x0000950B
		public string game_boost { get; set; }

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000260 RID: 608 RVA: 0x0000B314 File Offset: 0x00009514
		// (set) Token: 0x06000261 RID: 609 RVA: 0x0000B31C File Offset: 0x0000951C
		public string perf_monitor { get; set; }

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000262 RID: 610 RVA: 0x0000B325 File Offset: 0x00009525
		// (set) Token: 0x06000263 RID: 611 RVA: 0x0000B32D File Offset: 0x0000952D
		public string antimistouch { get; set; }
	}
	public struct LZ_MSG_CHANGE_MODE
	{
		// Token: 0x04000194 RID: 404
		public int TargetGameMode;
	}


	public struct LZ_MSG_IMAGE_SNAPED
	{
		// Token: 0x04000198 RID: 408
		public int DataLength;

		// Token: 0x04000199 RID: 409
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 2048)]
		public byte[] ImagePath;
	}

	// Token: 0x0200006A RID: 106
	public struct LZ_MSG_REPORT
	{
		// Token: 0x0400019D RID: 413
		public int DataLength;

		// Token: 0x0400019E RID: 414
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4096)]
		public byte[] Data;
	}

	// Token: 0x02000066 RID: 102
	public struct LZ_MSG_CLEAN_RESULT
	{
		// Token: 0x04000196 RID: 406
		public int CleanedMemory;

		// Token: 0x04000197 RID: 407
		public int Dram;
	}
	public struct LZ_MSG_SAVEIMAGE_RESULT
	{
		// Token: 0x0400019A RID: 410
		public int Result;
	}
	public struct LZ_MSG_GAME_MODE
	{
		// Token: 0x04000195 RID: 405
		public int CurGameMode;
	}

	public struct LZ_MSG_DEVICE_INFO
	{
		// Token: 0x04000181 RID: 385
		public int CpuPercent;

		// Token: 0x04000182 RID: 386
		public int CpuFreq;

		// Token: 0x04000183 RID: 387
		public int CpuFreqMax;

		// Token: 0x04000184 RID: 388
		public int CpuTemperature;

		// Token: 0x04000185 RID: 389
		public int cpu_fan_rpm;

		// Token: 0x04000186 RID: 390
		public int cpu_fan_rpm_max;

		// Token: 0x04000187 RID: 391
		public int cpu_power;

		// Token: 0x04000188 RID: 392
		public int GpuPrecent;

		// Token: 0x04000189 RID: 393
		public int GpuFreq;

		// Token: 0x0400018A RID: 394
		public int GpuFreqMax;

		// Token: 0x0400018B RID: 395
		public int GpuTemperature;

		// Token: 0x0400018C RID: 396
		public int gpu_fan_rpm;

		// Token: 0x0400018D RID: 397
		public int gpu_fan_rpm_max;

		// Token: 0x0400018E RID: 398
		public int gpu_power;

		// Token: 0x0400018F RID: 399
		public int gpu_mem_usage;

		// Token: 0x04000190 RID: 400
		public int Power;

		// Token: 0x04000191 RID: 401
		public int PowerMode;

		// Token: 0x04000192 RID: 402
		public int Dram;

		// Token: 0x04000193 RID: 403
		public int dram_freq;
	}

	public struct LZ_MSG_GAMEMODES_RESULT
	{
		// Token: 0x0400019B RID: 411
		public int mode_size;

		// Token: 0x0400019C RID: 412
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
		public int[] modes;
	}
	internal enum MessageType
	{
		// Token: 0x04000046 RID: 70
		LZ_MESSAGE_BEGIN,
		// Token: 0x04000047 RID: 71
		LZ_MESSAGE_HOOK_START,
		// Token: 0x04000048 RID: 72
		LZ_MESSAGE_INFOBAR_SHOWED,
		// Token: 0x04000049 RID: 73
		LZ_MESSAGE_INFOBAR_HIDED,
		// Token: 0x0400004A RID: 74
		LZ_MESSAGE_DEVICE_INFO,
		// Token: 0x0400004B RID: 75
		LZ_MESSAGE_NOTIFY_GAME_MODE,
		// Token: 0x0400004C RID: 76
		LZ_MESSAGE_CHANGE_GAME_MODE,
		// Token: 0x0400004D RID: 77
		LZ_MESSAGE_CLEAN_MEMORY,
		// Token: 0x0400004E RID: 78
		LZ_MESSAGE_MEMORY_CLEANED,
		// Token: 0x0400004F RID: 79
		LZ_MESSAGE_IMAGE_SNAPED,
		// Token: 0x04000050 RID: 80
		LZ_MESSAGE_TRAY_EXITED,
		// Token: 0x04000051 RID: 81
		LZ_MESSAGE_SAVEIMAGE_RESULT,
		// Token: 0x04000052 RID: 82
		LZ_MESSAGE_GET_GAMEMODES,
		// Token: 0x04000053 RID: 83
		LZ_MESSAGE_GET_GAMEMODES_RESULT,
		// Token: 0x04000054 RID: 84
		LZ_MESSAGE_REPORT,
		// Token: 0x04000055 RID: 85
		LZ_LENOVOONE_MESSAGE = 50,
		// Token: 0x04000056 RID: 86
		LZ_MESSAGE_SELF_RUN_LZMAIN = 99,
		// Token: 0x04000057 RID: 87
		LZ_MESSAGE_END,
		// Token: 0x04000058 RID: 88
		LZ_MESSAGE_COUNT = 100
	}

	public class GameReportReceivedData
	{
		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000298 RID: 664 RVA: 0x0000BA5C File Offset: 0x00009C5C
		// (set) Token: 0x06000299 RID: 665 RVA: 0x0000BA64 File Offset: 0x00009C64
		public string action { get; set; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600029A RID: 666 RVA: 0x0000BA6D File Offset: 0x00009C6D
		// (set) Token: 0x0600029B RID: 667 RVA: 0x0000BA75 File Offset: 0x00009C75
		public string category { get; set; }

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600029C RID: 668 RVA: 0x0000BA7E File Offset: 0x00009C7E
		// (set) Token: 0x0600029D RID: 669 RVA: 0x0000BA86 File Offset: 0x00009C86
		public string label { get; set; }

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x0600029E RID: 670 RVA: 0x0000BA8F File Offset: 0x00009C8F
		// (set) Token: 0x0600029F RID: 671 RVA: 0x0000BA97 File Offset: 0x00009C97
		public List<KeyValue> value { get; set; }

		// Token: 0x04000149 RID: 329
		public static string KeyNameClickType = "clickType";
	}
	// Token: 0x02000059 RID: 89
	public class KeyValue
	{
		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060002A2 RID: 674 RVA: 0x0000BAB4 File Offset: 0x00009CB4
		// (set) Token: 0x060002A3 RID: 675 RVA: 0x0000BABC File Offset: 0x00009CBC
		public string clickType { get; set; }
	}
	public class UpdateInfo
	{
		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000322 RID: 802 RVA: 0x0000C26E File Offset: 0x0000A46E
		// (set) Token: 0x06000323 RID: 803 RVA: 0x0000C276 File Offset: 0x0000A476
		public string NewVersion
		{
			get
			{
				return this.newVersion;
			}
			set
			{
				if (this.newVersion != value)
				{
					this.newVersion = value;
				}
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000324 RID: 804 RVA: 0x0000C28D File Offset: 0x0000A48D
		// (set) Token: 0x06000325 RID: 805 RVA: 0x0000C295 File Offset: 0x0000A495
		public List<UpdateInfoItem> Infos
		{
			get
			{
				return this.infos;
			}
			set
			{
				this.infos = value;
			}
		}

		// Token: 0x040001AF RID: 431
		private string newVersion;

		// Token: 0x040001B0 RID: 432
		public List<UpdateInfoItem> infos = new List<UpdateInfoItem>();
	}

	// Token: 0x02000071 RID: 113
	public class UpdateInfoItem
	{
		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000327 RID: 807 RVA: 0x0000C2B1 File Offset: 0x0000A4B1
		// (set) Token: 0x06000328 RID: 808 RVA: 0x0000C2B9 File Offset: 0x0000A4B9
		public string Id { get; set; }

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000329 RID: 809 RVA: 0x0000C2C2 File Offset: 0x0000A4C2
		// (set) Token: 0x0600032A RID: 810 RVA: 0x0000C2CA File Offset: 0x0000A4CA
		public string Info { get; set; }
	}

	public class ExternalDeviceData
	{
		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000295 RID: 661 RVA: 0x0000BA43 File Offset: 0x00009C43
		// (set) Token: 0x06000296 RID: 662 RVA: 0x0000BA4B File Offset: 0x00009C4B
		public List<Device> DeviceInfos { get; set; }
	}
	public class ExternalDeviceInfoList : IList<ExternalDeviceInfo>, ICollection<ExternalDeviceInfo>, IEnumerable<ExternalDeviceInfo>
	{
		// Token: 0x0600027D RID: 637 RVA: 0x0000B512 File Offset: 0x00009712
		public ExternalDeviceInfoList()
		{
			this.list = new List<ExternalDeviceInfo>();
		}

		// Token: 0x1700005E RID: 94
		public ExternalDeviceInfo this[int index]
		{
			get
			{
				return this.list[index];
			}
			set
			{
				if (index < this.Count)
				{
					this.list[index] = value;
				}
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000280 RID: 640 RVA: 0x0000B54B File Offset: 0x0000974B
		public int Count
		{
			get
			{
				return this.list.Count;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000281 RID: 641 RVA: 0x0000B558 File Offset: 0x00009758
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000282 RID: 642 RVA: 0x0000B55B File Offset: 0x0000975B
		// (set) Token: 0x06000283 RID: 643 RVA: 0x0000B563 File Offset: 0x00009763
		public bool Initialized
		{
			get
			{
				return this.initialized;
			}
			set
			{
				this.initialized = value;
			}
		}

		// Token: 0x06000284 RID: 644 RVA: 0x0000B56C File Offset: 0x0000976C
		public void Add(ExternalDeviceInfo item)
		{
			this.list.Add(item);
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0000B57A File Offset: 0x0000977A
		public void Clear()
		{
			this.Clear();
		}

		// Token: 0x06000286 RID: 646 RVA: 0x0000B584 File Offset: 0x00009784
		public bool Contains(ExternalDeviceInfo item)
		{
			return item != null && this.list.FirstOrDefault((ExternalDeviceInfo p) => p.PID == item.PID && p.VID == item.VID) != null;
		}

		// Token: 0x06000287 RID: 647 RVA: 0x0000B5C4 File Offset: 0x000097C4
		public void CopyTo(ExternalDeviceInfo[] array, int arrayIndex)
		{
			this.list.CopyTo(array, arrayIndex);
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0000B5D3 File Offset: 0x000097D3
		public IEnumerator<ExternalDeviceInfo> GetEnumerator()
		{
			return this.list.GetEnumerator();
		}

		// Token: 0x06000289 RID: 649 RVA: 0x0000B5E5 File Offset: 0x000097E5
		public int IndexOf(ExternalDeviceInfo item)
		{
			return this.list.IndexOf(item);
		}

		// Token: 0x0600028A RID: 650 RVA: 0x0000B5F3 File Offset: 0x000097F3
		public void Insert(int index, ExternalDeviceInfo item)
		{
			this.list.Insert(index, item);
		}

		// Token: 0x0600028B RID: 651 RVA: 0x0000B602 File Offset: 0x00009802
		public bool Remove(ExternalDeviceInfo item)
		{
			return this.list.Remove(item);
		}

		// Token: 0x0600028C RID: 652 RVA: 0x0000B610 File Offset: 0x00009810
		public void RemoveAt(int index)
		{
			this.list.RemoveAt(index);
		}

		// Token: 0x0600028D RID: 653 RVA: 0x0000B61E File Offset: 0x0000981E
		//IEnumerator<ExternalDeviceInfo> IEnumerator<ExternalDeviceInfo>.GetEnumerator()
		//{
		//	return this.list.GetEnumerator();
		//}

		// Token: 0x0600028E RID: 654 RVA: 0x0000B630 File Offset: 0x00009830
		public void AddItemsFromExternalDeviceData(ExternalDeviceData data)
		{
			if (data != null && data.DeviceInfos != null)
			{
				foreach (Device device in data.DeviceInfos)
				{
					ExternalDeviceInfo externalDeviceInfo = new ExternalDeviceInfo
					{
						PID = device.pid,
						VID = device.vid,
						Action = ExternalDeviceAction.Local,
						IsNotified = true,
						Status = ExternalDeviceStatus.Unknow
					};
					externalDeviceInfo.SetName();
					this.list.Add(externalDeviceInfo);
				}
			}
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0000B6D0 File Offset: 0x000098D0
		public ExternalDeviceData ConvertToExternalDeviceData()
		{
			if (this.Count <= 0)
			{
				return null;
			}
			ExternalDeviceData externalDeviceData = new ExternalDeviceData();
			externalDeviceData.DeviceInfos = new List<Device>();
			foreach (ExternalDeviceInfo externalDeviceInfo in this.list)
			{
				externalDeviceData.DeviceInfos.Add(new Device
				{
					pid = externalDeviceInfo.PID,
					vid = externalDeviceInfo.VID,
					status = ExternalDeviceStatus.Unknow
				});
			}
			return externalDeviceData;
		}

		// Token: 0x06000290 RID: 656 RVA: 0x0000B768 File Offset: 0x00009968
		public void SaveExternalDeviceData()
		{
			ExternalDeviceData externalDeviceData = this.ConvertToExternalDeviceData();
			if (externalDeviceData != null)
			{
				ExternalDeviceDataOperation.Save(externalDeviceData);
			}
		}

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        // Token: 0x04000145 RID: 325
        private bool initialized;

		// Token: 0x04000146 RID: 326
		private List<ExternalDeviceInfo> list;
	}
	public enum ExternalDeviceType
	{
		// Token: 0x0400000F RID: 15
		Mouse,
		// Token: 0x04000010 RID: 16
		Keyboard,
		// Token: 0x04000011 RID: 17
		Unknow
	}
	public class ExternalDeviceInfo
	{
		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000265 RID: 613 RVA: 0x0000B33E File Offset: 0x0000953E
		// (set) Token: 0x06000266 RID: 614 RVA: 0x0000B346 File Offset: 0x00009546
		public string DisplayName { get; set; }

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000267 RID: 615 RVA: 0x0000B34F File Offset: 0x0000954F
		// (set) Token: 0x06000268 RID: 616 RVA: 0x0000B357 File Offset: 0x00009557
		public string Name { get; set; }

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000269 RID: 617 RVA: 0x0000B360 File Offset: 0x00009560
		// (set) Token: 0x0600026A RID: 618 RVA: 0x0000B368 File Offset: 0x00009568
		public string Model { get; set; }

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600026B RID: 619 RVA: 0x0000B371 File Offset: 0x00009571
		// (set) Token: 0x0600026C RID: 620 RVA: 0x0000B379 File Offset: 0x00009579
		public ExternalDeviceType Type { get; set; }

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x0600026D RID: 621 RVA: 0x0000B382 File Offset: 0x00009582
		// (set) Token: 0x0600026E RID: 622 RVA: 0x0000B38A File Offset: 0x0000958A
		public string TypeName { get; set; }

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x0600026F RID: 623 RVA: 0x0000B393 File Offset: 0x00009593
		// (set) Token: 0x06000270 RID: 624 RVA: 0x0000B39B File Offset: 0x0000959B
		public string PID { get; set; }

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000271 RID: 625 RVA: 0x0000B3A4 File Offset: 0x000095A4
		// (set) Token: 0x06000272 RID: 626 RVA: 0x0000B3AC File Offset: 0x000095AC
		public string VID { get; set; }

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000273 RID: 627 RVA: 0x0000B3B5 File Offset: 0x000095B5
		// (set) Token: 0x06000274 RID: 628 RVA: 0x0000B3BD File Offset: 0x000095BD
		public ExternalDeviceStatus Status { get; set; }

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000275 RID: 629 RVA: 0x0000B3C6 File Offset: 0x000095C6
		// (set) Token: 0x06000276 RID: 630 RVA: 0x0000B3CE File Offset: 0x000095CE
		public bool IsNotified { get; set; }

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000277 RID: 631 RVA: 0x0000B3D7 File Offset: 0x000095D7
		// (set) Token: 0x06000278 RID: 632 RVA: 0x0000B3DF File Offset: 0x000095DF
		public ExternalDeviceAction Action { get; set; }

		// Token: 0x06000279 RID: 633 RVA: 0x0000B3E8 File Offset: 0x000095E8
		public static ExternalDeviceInfo Create(Device device, ExternalDeviceAction action)
		{
			if (device == null)
			{
				return null;
			}
			ExternalDeviceInfo externalDeviceInfo = new ExternalDeviceInfo
			{
				DisplayName = "未知设备",
				Name = "未知设备",
				Model = "未知设备",
				Type = ExternalDeviceType.Unknow,
				PID = device.pid,
				VID = device.vid,
				Status = device.status,
				Action = action,
				IsNotified = false
			};
			if (externalDeviceInfo.SetName())
			{
				return externalDeviceInfo;
			}
			return null;
		}

		// Token: 0x0600027A RID: 634 RVA: 0x0000B468 File Offset: 0x00009668
		public bool SetName()
		{
			ExternalDeviceInfo externalDeviceInfo = PresetExternalDeviceSet.PresetExternalDevice.FirstOrDefault((ExternalDeviceInfo p) => p.PID.ToUpper() == this.PID.ToUpper() && p.VID.ToUpper() == this.VID.ToUpper());
			if (externalDeviceInfo != null)
			{
				this.DisplayName = externalDeviceInfo.DisplayName;
				this.Name = externalDeviceInfo.Name;
				this.Model = externalDeviceInfo.Model;
				this.Type = externalDeviceInfo.Type;
				this.TypeName = externalDeviceInfo.TypeName;
				return true;
			}
			return false;
		}
	}

	public class ExternalDeviceDataOperation
	{
		// Token: 0x06000027 RID: 39 RVA: 0x00002720 File Offset: 0x00000920
		public static bool Save(ExternalDeviceData data)
		{
			bool flag;
			try
			{
				string externalDeviceData = JsonHelper.ObjectToString(data);
				object obj = ExternalDeviceDataOperation.logLock;
				lock (obj)
				{
					RegistryHelper.Instance.SetExternalDeviceData(externalDeviceData);
				}
				flag = true;
			}
			catch (Exception ex)
			{
				LogHelper.Log("[ExternalDeviceDataFile] [Save] 保存数据异常：ex = " + ex.Message);
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002798 File Offset: 0x00000998
		public static ExternalDeviceData Read()
		{
			ExternalDeviceData result;
			try
			{
				ExternalDeviceData externalDeviceData = null;
				object obj = ExternalDeviceDataOperation.logLock;
				lock (obj)
				{
					string externalDeviceData2 = RegistryHelper.Instance.GetExternalDeviceData();
					if (!string.IsNullOrEmpty(externalDeviceData2))
					{
						externalDeviceData = JsonHelper.StringToObject<ExternalDeviceData>(externalDeviceData2);
					}
				}
				result = externalDeviceData;
			}
			catch (Exception ex)
			{
				LogHelper.Log("[ExternalDeviceDataFile] [Read] 读取数据异常：ex = " + ex.Message);
				result = null;
			}
			return result;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002820 File Offset: 0x00000A20
		public static bool Delete()
		{
			bool flag;
			try
			{
				object obj = ExternalDeviceDataOperation.logLock;
				lock (obj)
				{
					RegistryHelper.Instance.SetExternalDeviceData("");
				}
				flag = true;
			}
			catch (Exception ex)
			{
				LogHelper.Log("[ExternalDeviceDataFile] [Delete] 删除数据异常：ex = " + ex.Message);
				flag = false;
			}
			return flag;
		}

		// Token: 0x04000044 RID: 68
		private static readonly object logLock = new object();
	}


	public class PresetExternalDeviceSet
	{
		// Token: 0x06000291 RID: 657 RVA: 0x0000B786 File Offset: 0x00009986
		static PresetExternalDeviceSet()
		{
			PresetExternalDeviceSet.Init();
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000292 RID: 658 RVA: 0x0000B797 File Offset: 0x00009997
		public static List<ExternalDeviceInfo> PresetExternalDevice
		{
			get
			{
				return PresetExternalDeviceSet.presetExternalDevice;
			}
		}

		// Token: 0x06000293 RID: 659 RVA: 0x0000B7A0 File Offset: 0x000099A0
		private static void Init()
		{
			PresetExternalDeviceSet.presetExternalDevice.Add(new ExternalDeviceInfo
			{
				DisplayName = "拯救者M500鼠标",
				Name = "鼠标M500",
				Model = "M500",
				PID = "60d4",
				VID = "17ef",
				TypeName = "鼠标",
				Type = ExternalDeviceType.Mouse,
				Action = ExternalDeviceAction.Unknow,
				IsNotified = true,
				Status = ExternalDeviceStatus.Unknow
			});
			PresetExternalDeviceSet.presetExternalDevice.Add(new ExternalDeviceInfo
			{
				DisplayName = "拯救者K500键盘",
				Name = "键盘K500",
				Model = "K500",
				PID = "60d5",
				VID = "17ef",
				TypeName = "键盘",
				Type = ExternalDeviceType.Keyboard,
				Action = ExternalDeviceAction.Unknow,
				IsNotified = true,
				Status = ExternalDeviceStatus.Unknow
			});
			PresetExternalDeviceSet.presetExternalDevice.Add(new ExternalDeviceInfo
			{
				DisplayName = "拯救者M300鼠标",
				Name = "鼠标M300",
				Model = "M300",
				PID = "60e4",
				VID = "17ef",
				TypeName = "鼠标",
				Type = ExternalDeviceType.Mouse,
				Action = ExternalDeviceAction.Unknow,
				IsNotified = true,
				Status = ExternalDeviceStatus.Unknow
			});
			PresetExternalDeviceSet.presetExternalDevice.Add(new ExternalDeviceInfo
			{
				DisplayName = "拯救者K300键盘",
				Name = "键盘K300",
				Model = "K300",
				PID = "60f0",
				VID = "17ef",
				TypeName = "键盘",
				Type = ExternalDeviceType.Keyboard,
				Action = ExternalDeviceAction.Unknow,
				IsNotified = true,
				Status = ExternalDeviceStatus.Unknow
			});
			PresetExternalDeviceSet.presetExternalDevice.Add(new ExternalDeviceInfo
			{
				DisplayName = "拯救者M600鼠标",
				Name = "鼠标M600",
				Model = "M600(Wired)",
				PID = "60e5",
				VID = "17ef",
				TypeName = "鼠标",
				Type = ExternalDeviceType.Mouse,
				Action = ExternalDeviceAction.Unknow,
				IsNotified = true,
				Status = ExternalDeviceStatus.Unknow
			});
			PresetExternalDeviceSet.presetExternalDevice.Add(new ExternalDeviceInfo
			{
				DisplayName = "拯救者M600鼠标",
				Name = "鼠标M600",
				Model = "M600(Wireless)",
				PID = "60e6",
				VID = "17ef",
				TypeName = "鼠标",
				Type = ExternalDeviceType.Mouse,
				Action = ExternalDeviceAction.Unknow,
				IsNotified = true,
				Status = ExternalDeviceStatus.Unknow
			});
		}

		// Token: 0x04000147 RID: 327
		private static List<ExternalDeviceInfo> presetExternalDevice = new List<ExternalDeviceInfo>();
	}
	public class JsonPopupReceivedData
	{
		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000222 RID: 546 RVA: 0x0000B050 File Offset: 0x00009250
		// (set) Token: 0x06000223 RID: 547 RVA: 0x0000B058 File Offset: 0x00009258
		public string content { get; set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000224 RID: 548 RVA: 0x0000B061 File Offset: 0x00009261
		// (set) Token: 0x06000225 RID: 549 RVA: 0x0000B069 File Offset: 0x00009269
		public int duration { get; set; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000226 RID: 550 RVA: 0x0000B072 File Offset: 0x00009272
		// (set) Token: 0x06000227 RID: 551 RVA: 0x0000B07A File Offset: 0x0000927A
		public PopupLogoType logo_id { get; set; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000228 RID: 552 RVA: 0x0000B083 File Offset: 0x00009283
		// (set) Token: 0x06000229 RID: 553 RVA: 0x0000B08B File Offset: 0x0000928B
		public string title { get; set; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x0600022A RID: 554 RVA: 0x0000B094 File Offset: 0x00009294
		// (set) Token: 0x0600022B RID: 555 RVA: 0x0000B09C File Offset: 0x0000929C
		public PopupStyleType style_id { get; set; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x0600022C RID: 556 RVA: 0x0000B0A5 File Offset: 0x000092A5
		// (set) Token: 0x0600022D RID: 557 RVA: 0x0000B0AD File Offset: 0x000092AD
		public string action { get; set; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x0600022E RID: 558 RVA: 0x0000B0B6 File Offset: 0x000092B6
		// (set) Token: 0x0600022F RID: 559 RVA: 0x0000B0BE File Offset: 0x000092BE
		public string button_text { get; set; }
	}
	public enum PopupLogoType
	{
		// Token: 0x040000E0 RID: 224
		None,
		// Token: 0x040000E1 RID: 225
		LogionZone
	}   
	
	// Token: 0x02000039 RID: 57
	public enum PopupStyleType
	{
		// Token: 0x040000DC RID: 220
		Unknow,
		// Token: 0x040000DD RID: 221
		Style1,
		// Token: 0x040000DE RID: 222
		Style2
	}



	// Token: 0x02000049 RID: 73
	public class PopupData : INotifyPropertyChanged
	{
		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060001F8 RID: 504 RVA: 0x0000ADDE File Offset: 0x00008FDE
		// (set) Token: 0x060001F9 RID: 505 RVA: 0x0000ADE6 File Offset: 0x00008FE6
		public int Duration
		{
			get
			{
				return this.duration;
			}
			set
			{
				if (value != this.duration)
				{
					this.duration = value;
					this.OnPropertyChanged("Duration");
				}
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060001FA RID: 506 RVA: 0x0000AE03 File Offset: 0x00009003
		// (set) Token: 0x060001FB RID: 507 RVA: 0x0000AE0B File Offset: 0x0000900B
		public PopupStyleType StyleId
		{
			get
			{
				return this.styleId;
			}
			set
			{
				if (value != this.styleId)
				{
					this.styleId = value;
					this.OnPropertyChanged("StyleId");
				}
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060001FC RID: 508 RVA: 0x0000AE28 File Offset: 0x00009028
		// (set) Token: 0x060001FD RID: 509 RVA: 0x0000AE30 File Offset: 0x00009030
		public string Title
		{
			get
			{
				return this.title;
			}
			set
			{
				if (value != this.title)
				{
					this.title = value;
					this.OnPropertyChanged("Title");
				}
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060001FE RID: 510 RVA: 0x0000AE52 File Offset: 0x00009052
		// (set) Token: 0x060001FF RID: 511 RVA: 0x0000AE5A File Offset: 0x0000905A
		public string Content
		{
			get
			{
				return this.content;
			}
			set
			{
				if (value != this.content)
				{
					this.content = value;
					this.OnPropertyChanged("Content");
				}
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000200 RID: 512 RVA: 0x0000AE7C File Offset: 0x0000907C
		// (set) Token: 0x06000201 RID: 513 RVA: 0x0000AE84 File Offset: 0x00009084
		public ImageSource Logo
		{
			get
			{
				return this.logo;
			}
			set
			{
				if (value != this.logo)
				{
					this.logo = value;
					this.OnPropertyChanged("Logo");
				}
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000202 RID: 514 RVA: 0x0000AEA1 File Offset: 0x000090A1
		// (set) Token: 0x06000203 RID: 515 RVA: 0x0000AEA9 File Offset: 0x000090A9
		public string Action
		{
			get
			{
				return this.action;
			}
			set
			{
				if (value != this.action)
				{
					this.action = value;
					this.OnPropertyChanged("Action");
				}
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000204 RID: 516 RVA: 0x0000AECB File Offset: 0x000090CB
		// (set) Token: 0x06000205 RID: 517 RVA: 0x0000AED3 File Offset: 0x000090D3
		public string ButtonText
		{
			get
			{
				return this.buttonText;
			}
			set
			{
				if (value != this.buttonText)
				{
					this.buttonText = value;
					this.OnPropertyChanged("ButtonText");
				}
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000206 RID: 518 RVA: 0x0000AEF5 File Offset: 0x000090F5
		// (set) Token: 0x06000207 RID: 519 RVA: 0x0000AEFD File Offset: 0x000090FD
		public string PluginName { get; set; }

		// Token: 0x14000018 RID: 24
		// (add) Token: 0x06000208 RID: 520 RVA: 0x0000AF08 File Offset: 0x00009108
		// (remove) Token: 0x06000209 RID: 521 RVA: 0x0000AF40 File Offset: 0x00009140
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x0600020A RID: 522 RVA: 0x0000AF75 File Offset: 0x00009175
		private void OnPropertyChanged([CallerMemberName] string propertyName = "")
		{
			PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
			if (propertyChanged == null)
			{
				return;
			}
			propertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}

		// Token: 0x0400010B RID: 267
		private int duration;

		// Token: 0x0400010C RID: 268
		private PopupStyleType styleId;

		// Token: 0x0400010D RID: 269
		private string title;

		// Token: 0x0400010E RID: 270
		private string content;

		// Token: 0x0400010F RID: 271
		private ImageSource logo;

		// Token: 0x04000110 RID: 272
		private string action;

		// Token: 0x04000111 RID: 273
		private string buttonText;
	}
	public enum SpeedBallStauts
	{
		// Token: 0x040000E3 RID: 227
		Unknow,
		// Token: 0x040000E4 RID: 228
		Open,
		// Token: 0x040000E5 RID: 229
		Speeding,
		// Token: 0x040000E6 RID: 230
		Completed,
		// Token: 0x040000E7 RID: 231
		Close
	}
	public class JsonSpeedBallReceivedData
	{
		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000215 RID: 533 RVA: 0x0000AFE2 File Offset: 0x000091E2
		// (set) Token: 0x06000216 RID: 534 RVA: 0x0000AFEA File Offset: 0x000091EA
		public SpeedBallStauts status { get; set; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000217 RID: 535 RVA: 0x0000AFF3 File Offset: 0x000091F3
		// (set) Token: 0x06000218 RID: 536 RVA: 0x0000AFFB File Offset: 0x000091FB
		public int progress { get; set; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000219 RID: 537 RVA: 0x0000B004 File Offset: 0x00009204
		// (set) Token: 0x0600021A RID: 538 RVA: 0x0000B00C File Offset: 0x0000920C
		public string title { get; set; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600021B RID: 539 RVA: 0x0000B015 File Offset: 0x00009215
		// (set) Token: 0x0600021C RID: 540 RVA: 0x0000B01D File Offset: 0x0000921D
		public string delay { get; set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600021D RID: 541 RVA: 0x0000B026 File Offset: 0x00009226
		// (set) Token: 0x0600021E RID: 542 RVA: 0x0000B02E File Offset: 0x0000922E
		public string drop { get; set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600021F RID: 543 RVA: 0x0000B037 File Offset: 0x00009237
		// (set) Token: 0x06000220 RID: 544 RVA: 0x0000B03F File Offset: 0x0000923F
		public string flowsum { get; set; }
	}


	public class SpeedBallData : INotifyPropertyChanged
	{
		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x0000AC5D File Offset: 0x00008E5D
		// (set) Token: 0x060001E9 RID: 489 RVA: 0x0000AC65 File Offset: 0x00008E65
		public SpeedBallStauts Status
		{
			get
			{
				return this.status;
			}
			set
			{
				if (value != this.status)
				{
					this.status = value;
					this.OnPropertyChanged("Status");
				}
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060001EA RID: 490 RVA: 0x0000AC82 File Offset: 0x00008E82
		// (set) Token: 0x060001EB RID: 491 RVA: 0x0000AC8A File Offset: 0x00008E8A
		public int Progress
		{
			get
			{
				return this.progress;
			}
			set
			{
				if (value != this.progress)
				{
					this.progress = value;
					this.OnPropertyChanged("Progress");
				}
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060001EC RID: 492 RVA: 0x0000ACA7 File Offset: 0x00008EA7
		// (set) Token: 0x060001ED RID: 493 RVA: 0x0000ACAF File Offset: 0x00008EAF
		public string Title
		{
			get
			{
				return this.title;
			}
			set
			{
				if (value != this.title)
				{
					this.title = value;
					this.OnPropertyChanged("Title");
				}
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060001EE RID: 494 RVA: 0x0000ACD1 File Offset: 0x00008ED1
		// (set) Token: 0x060001EF RID: 495 RVA: 0x0000ACD9 File Offset: 0x00008ED9
		public string Delay
		{
			get
			{
				return this.delay;
			}
			set
			{
				if (value != this.delay)
				{
					this.delay = value;
					this.OnPropertyChanged("Delay");
				}
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060001F0 RID: 496 RVA: 0x0000ACFB File Offset: 0x00008EFB
		// (set) Token: 0x060001F1 RID: 497 RVA: 0x0000AD03 File Offset: 0x00008F03
		public string Drop
		{
			get
			{
				return this.drop;
			}
			set
			{
				if (value != this.drop)
				{
					this.drop = value;
					this.OnPropertyChanged("Drop");
				}
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060001F2 RID: 498 RVA: 0x0000AD25 File Offset: 0x00008F25
		// (set) Token: 0x060001F3 RID: 499 RVA: 0x0000AD2D File Offset: 0x00008F2D
		public string Flowsum
		{
			get
			{
				return this.flowsum;
			}
			set
			{
				if (value != this.flowsum)
				{
					this.flowsum = value;
					this.OnPropertyChanged("Flowsum");
				}
			}
		}

		// Token: 0x14000017 RID: 23
		// (add) Token: 0x060001F4 RID: 500 RVA: 0x0000AD50 File Offset: 0x00008F50
		// (remove) Token: 0x060001F5 RID: 501 RVA: 0x0000AD88 File Offset: 0x00008F88
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x060001F6 RID: 502 RVA: 0x0000ADBD File Offset: 0x00008FBD
		private void OnPropertyChanged([CallerMemberName] string propertyName = "")
		{
			PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
			if (propertyChanged == null)
			{
				return;
			}
			propertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}

		// Token: 0x04000104 RID: 260
		private SpeedBallStauts status;

		// Token: 0x04000105 RID: 261
		private int progress;

		// Token: 0x04000106 RID: 262
		private string title;

		// Token: 0x04000107 RID: 263
		private string delay;

		// Token: 0x04000108 RID: 264
		private string drop;

		// Token: 0x04000109 RID: 265
		private string flowsum;
	}



}
