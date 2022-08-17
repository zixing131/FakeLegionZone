using System;
using System.Collections.Generic;
using System.Diagnostics;
using FakeLegionZone.Util;

namespace FakeLegionZone.Model
{
	// Token: 0x02000060 RID: 96
	public class GameInfo
	{
		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060002E4 RID: 740 RVA: 0x0000BEEF File Offset: 0x0000A0EF
		// (set) Token: 0x060002E5 RID: 741 RVA: 0x0000BEF8 File Offset: 0x0000A0F8
		public int ProcessId
		{
			get
			{
				return this.processId;
			}
			set
			{
				this.processId = value;
				try
				{
					Process processById = this.GetProcessById(this.processId);
					if (processById != null)
					{
						this.mainWindowTitle = processById.MainWindowTitle;
					}
				}
				catch (Exception ex)
				{
					LogHelper.Log("[GameInfo] [ProcessId] 获取进程 MainWindowTitle 异常：ex = " + ex.Message + "。");
				}
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060002E6 RID: 742 RVA: 0x0000BF58 File Offset: 0x0000A158
		public string MainWindowTitle
		{
			get
			{
				return this.mainWindowTitle;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060002E7 RID: 743 RVA: 0x0000BF60 File Offset: 0x0000A160
		// (set) Token: 0x060002E8 RID: 744 RVA: 0x0000BF68 File Offset: 0x0000A168
		public string GpuDesc { get; set; }

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060002E9 RID: 745 RVA: 0x0000BF71 File Offset: 0x0000A171
		// (set) Token: 0x060002EA RID: 746 RVA: 0x0000BF79 File Offset: 0x0000A179
		public uint GpuVendorId { get; set; }

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060002EB RID: 747 RVA: 0x0000BF82 File Offset: 0x0000A182
		// (set) Token: 0x060002EC RID: 748 RVA: 0x0000BF8A File Offset: 0x0000A18A
		public uint GpuDeviceId { get; set; }

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060002ED RID: 749 RVA: 0x0000BF93 File Offset: 0x0000A193
		// (set) Token: 0x060002EE RID: 750 RVA: 0x0000BF9B File Offset: 0x0000A19B
		public bool Is64 { get; set; }

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060002EF RID: 751 RVA: 0x0000BFA4 File Offset: 0x0000A1A4
		// (set) Token: 0x060002F0 RID: 752 RVA: 0x0000BFAC File Offset: 0x0000A1AC
		public string ProcessName { get; set; }

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060002F1 RID: 753 RVA: 0x0000BFB5 File Offset: 0x0000A1B5
		// (set) Token: 0x060002F2 RID: 754 RVA: 0x0000BFBD File Offset: 0x0000A1BD
		public string GameName { get; set; }

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060002F3 RID: 755 RVA: 0x0000BFC6 File Offset: 0x0000A1C6
		// (set) Token: 0x060002F4 RID: 756 RVA: 0x0000BFCE File Offset: 0x0000A1CE
		public bool NetworkOptimization { get; set; }

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060002F5 RID: 757 RVA: 0x0000BFD7 File Offset: 0x0000A1D7
		// (set) Token: 0x060002F6 RID: 758 RVA: 0x0000BFDF File Offset: 0x0000A1DF
		public bool ShowAssistant { get; set; }

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060002F7 RID: 759 RVA: 0x0000BFE8 File Offset: 0x0000A1E8
		// (set) Token: 0x060002F8 RID: 760 RVA: 0x0000BFF0 File Offset: 0x0000A1F0
		public bool SupportInjected { get; set; }

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060002F9 RID: 761 RVA: 0x0000BFF9 File Offset: 0x0000A1F9
		// (set) Token: 0x060002FA RID: 762 RVA: 0x0000C001 File Offset: 0x0000A201
		public bool ChangeToPerformance { get; set; }

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060002FB RID: 763 RVA: 0x0000C00A File Offset: 0x0000A20A
		// (set) Token: 0x060002FC RID: 764 RVA: 0x0000C012 File Offset: 0x0000A212
		public bool IsInjected { get; set; }

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060002FD RID: 765 RVA: 0x0000C01B File Offset: 0x0000A21B
		public List<IntPtr> MessageHandlerCollection
		{
			get
			{
				return this.messageHandlerCollection;
			}
		}

		// Token: 0x060002FE RID: 766 RVA: 0x0000C023 File Offset: 0x0000A223
		public Process GetProcessById(int processId)
		{
			return Process.GetProcessById(processId);
		}

		// Token: 0x060002FF RID: 767 RVA: 0x0000C02B File Offset: 0x0000A22B
		public void Clean()
		{
			this.processId = -1;
			this.mainWindowTitle = null;
		}

		// Token: 0x0400016C RID: 364
		private int processId;

		// Token: 0x0400016D RID: 365
		private string mainWindowTitle;

		// Token: 0x04000179 RID: 377
		private List<IntPtr> messageHandlerCollection = new List<IntPtr>();
	}
}
