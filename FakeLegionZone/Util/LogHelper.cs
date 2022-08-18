 
using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace FakeLegionZone.Util
{
	// Token: 0x0200002F RID: 47
	internal class LogHelper
	{
		// Token: 0x0600013A RID: 314 RVA: 0x00007A60 File Offset: 0x00005C60
		static LogHelper()
		{
			try
			{
				LogHelper.logLock = new object();
				LogHelper.isLog = RegistryHelper.Instance.GetEnableLog();
				LogHelper.pathDirectory = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Lenovo\\lz_tray\\";
				if (LogHelper.isLog && !Directory.Exists(LogHelper.pathDirectory))
				{
					Directory.CreateDirectory(LogHelper.pathDirectory);
				}
				LogHelper.logFilePath = LogHelper.pathDirectory + "lz_tray_" + DateTime.Now.ToString("yyyyMMdd") + ".log";
			}
			catch (Exception)
			{
				LogHelper.isLog = false;
			}
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00007B00 File Offset: 0x00005D00
		public static void Log(string info)
		{
			object obj = LogHelper.logLock;
			lock (obj)
			{
				if (LogHelper.isLog)
				{
					try
					{
						using (StreamWriter streamWriter = new StreamWriter(LogHelper.logFilePath, true))
						{
							streamWriter.WriteLine(string.Format("[{0:yyyy-MM-dd HH:mm:ss:fff}] \t {1}", DateTime.Now, info));
						}
						Trace.WriteLine("[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff") + "]\t" + info);
					}
					catch (Exception ex)
					{
						Trace.WriteLine("log error: [" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss: fff") + "][Log]\t" + ex.Message);
					}
				}
			}
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00007BE8 File Offset: 0x00005DE8
		public static void WriteLaunchLog(string log)
		{
			string path = Environment.GetEnvironmentVariable("TEMP") + "\\lz_launch.log";
			log = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff ") + log + "\r\n";
			File.AppendAllText(path, log, Encoding.UTF8);
		}

		// Token: 0x0400009A RID: 154
		private static readonly object logLock;

		// Token: 0x0400009B RID: 155
		private static bool isLog;

		// Token: 0x0400009C RID: 156
		private static string logFilePath;

		// Token: 0x0400009D RID: 157
		public static string pathDirectory;
	}
}
