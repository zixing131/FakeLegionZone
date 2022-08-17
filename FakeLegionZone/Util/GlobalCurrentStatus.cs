using FakeLegionZone.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FakeLegionZone.Util
{
	// Token: 0x0200000C RID: 12
	public class GlobalCurrentStatus
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000E RID: 14 RVA: 0x00002536 File Offset: 0x00000736
		// (set) Token: 0x0600000F RID: 15 RVA: 0x0000253D File Offset: 0x0000073D
		public static GameMode OriginalGameMode
		{
			get
			{
				return GlobalCurrentStatus.originalGameMode;
			}
			set
			{
				if (GlobalCurrentStatus.originalGameMode != value)
				{
					GlobalCurrentStatus.originalGameMode = value;
					GlobalCurrentStatus.OnPropertyChanged("OriginalGameMode");
				}
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000010 RID: 16 RVA: 0x00002557 File Offset: 0x00000757
		// (set) Token: 0x06000011 RID: 17 RVA: 0x0000255E File Offset: 0x0000075E
		public static GameMode CurrentGameMode
		{
			get
			{
				return GlobalCurrentStatus.currentGameMode;
			}
			set
			{
				if (GlobalCurrentStatus.currentGameMode != value)
				{
					GlobalCurrentStatus.currentGameMode = value;
					GlobalCurrentStatus.OnPropertyChanged("CurrentGameMode");
				}
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000012 RID: 18 RVA: 0x00002578 File Offset: 0x00000778
		public static List<GameMode> SupportedGameMode
		{
			get
			{
				return GlobalCurrentStatus.supportedGameMode;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000013 RID: 19 RVA: 0x0000257F File Offset: 0x0000077F
		// (set) Token: 0x06000014 RID: 20 RVA: 0x00002586 File Offset: 0x00000786
		public static PowerMode CurrentPowerMode
		{
			get
			{
				return GlobalCurrentStatus.currentPowerMode;
			}
			set
			{
				if (GlobalCurrentStatus.currentPowerMode != value)
				{
					GlobalCurrentStatus.currentPowerMode = value;
					GlobalCurrentStatus.OnPropertyChanged("CurrentPowerMode");
				}
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000015 RID: 21 RVA: 0x000025A0 File Offset: 0x000007A0
		// (set) Token: 0x06000016 RID: 22 RVA: 0x000025A7 File Offset: 0x000007A7
		public static bool IsSupportSmartFan
		{
			get
			{
				return GlobalCurrentStatus.isSupportSmartFan;
			}
			set
			{
				if (GlobalCurrentStatus.isSupportSmartFan != value)
				{
					GlobalCurrentStatus.isSupportSmartFan = value;
					GlobalCurrentStatus.OnPropertyChanged("IsSupportSmartFan");
				}
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000017 RID: 23 RVA: 0x000025C1 File Offset: 0x000007C1
		// (set) Token: 0x06000018 RID: 24 RVA: 0x000025C8 File Offset: 0x000007C8
		public static bool IsMLSupport
		{
			get
			{
				return GlobalCurrentStatus.isMLSupport;
			}
			set
			{
				if (GlobalCurrentStatus.isMLSupport != value)
				{
					GlobalCurrentStatus.isMLSupport = value;
					GlobalCurrentStatus.OnPropertyChanged("IsMLSupport");
				}
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000019 RID: 25 RVA: 0x000025E2 File Offset: 0x000007E2
		// (set) Token: 0x0600001A RID: 26 RVA: 0x000025E9 File Offset: 0x000007E9
		public static int CurrentPowerBatteryRemaining
		{
			get
			{
				return GlobalCurrentStatus.currentPowerRemaining;
			}
			set
			{
				if (GlobalCurrentStatus.currentPowerRemaining != value)
				{
					GlobalCurrentStatus.currentPowerRemaining = value;
					GlobalCurrentStatus.OnPropertyChanged("CurrentPowerBatteryRemaining");
				}
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600001B RID: 27 RVA: 0x00002603 File Offset: 0x00000803
		// (set) Token: 0x0600001C RID: 28 RVA: 0x0000260A File Offset: 0x0000080A
		public static bool InGaming
		{
			get
			{
				return GlobalCurrentStatus.inGaming;
			}
			set
			{
				if (GlobalCurrentStatus.inGaming != value)
				{
					GlobalCurrentStatus.inGaming = value;
					GlobalCurrentStatus.OnPropertyChanged("InGaming");
				}
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600001D RID: 29 RVA: 0x00002624 File Offset: 0x00000824
		// (set) Token: 0x0600001E RID: 30 RVA: 0x0000262B File Offset: 0x0000082B
		public static bool InGameAccelerating
		{
			get
			{
				return GlobalCurrentStatus.inGameAccelerating;
			}
			set
			{
				if (GlobalCurrentStatus.inGameAccelerating != value)
				{
					GlobalCurrentStatus.inGameAccelerating = value;
					GlobalCurrentStatus.OnPropertyChanged("InGameAccelerating");
				}
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600001F RID: 31 RVA: 0x00002645 File Offset: 0x00000845
		// (set) Token: 0x06000020 RID: 32 RVA: 0x0000264C File Offset: 0x0000084C
		public static bool InGameModeChanging
		{
			get
			{
				return GlobalCurrentStatus.inGameModeChanging;
			}
			set
			{
				if (GlobalCurrentStatus.inGameModeChanging != value)
				{
					GlobalCurrentStatus.inGameModeChanging = value;
					GlobalCurrentStatus.OnPropertyChanged("InGameModeChanging");
				}
			}
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000021 RID: 33 RVA: 0x00002668 File Offset: 0x00000868
		// (remove) Token: 0x06000022 RID: 34 RVA: 0x0000269C File Offset: 0x0000089C
		public static event EventHandler<PropertyChangedEventArgs> StaticPropertyChanged;

		// Token: 0x06000023 RID: 35 RVA: 0x000026CF File Offset: 0x000008CF
		private static void OnPropertyChanged([CallerMemberName] string propertyName = "")
		{
			EventHandler<PropertyChangedEventArgs> staticPropertyChanged = GlobalCurrentStatus.StaticPropertyChanged;
			if (staticPropertyChanged == null)
			{
				return;
			}
			staticPropertyChanged(null, new PropertyChangedEventArgs(propertyName));
		}

		// Token: 0x0400002D RID: 45
		private static GameMode originalGameMode;

		// Token: 0x0400002E RID: 46
		private static GameMode currentGameMode;

		// Token: 0x0400002F RID: 47
		private static List<GameMode> supportedGameMode = new List<GameMode>();

		// Token: 0x04000030 RID: 48
		private static PowerMode currentPowerMode;

		// Token: 0x04000031 RID: 49
		private static bool isSupportSmartFan;

		// Token: 0x04000032 RID: 50
		private static bool isMLSupport;

		// Token: 0x04000033 RID: 51
		public static int currentPowerRemaining = -1;

		// Token: 0x04000034 RID: 52
		private static bool inGaming = false;

		// Token: 0x04000035 RID: 53
		private static bool inGameAccelerating = false;

		// Token: 0x04000036 RID: 54
		private static bool inGameModeChanging = false;
	}
}
