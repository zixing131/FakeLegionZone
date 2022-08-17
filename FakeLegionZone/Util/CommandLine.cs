using System;
using System.Collections.Generic;

namespace FakeLegionZone.Util
{
	// Token: 0x02000002 RID: 2
	public class CommandLine
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		// (set) Token: 0x06000002 RID: 2 RVA: 0x00002057 File Offset: 0x00000257
		public static List<string> CommandLineArgs
		{
			get
			{
				return CommandLine.commandLineArgs;
			}
			set
			{
				CommandLine.commandLineArgs = value;
			}
		}

		// Token: 0x04000001 RID: 1
		public const string CommandLineLzMainRunSwitch = "--lzmain";

		// Token: 0x04000002 RID: 2
		public const string CommandLineDisableVerifySignature = "--disable-verify-signature-test";

		// Token: 0x04000003 RID: 3
		public const string CommandLineEnableHardwareInfoChangedLog = "--enable-hardware-info-changed-log";

		// Token: 0x04000004 RID: 4
		private static List<string> commandLineArgs = new List<string>() { 
			"--enable-hardware-info-changed-log"
		};
	}
}
