using System;
using System.Diagnostics;
using System.IO;
using System.Management;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace FakeLegionZone.Util
{
	// Token: 0x02000033 RID: 51
	public class VerifySignature
	{
		// Token: 0x06000180 RID: 384 RVA: 0x00009874 File Offset: 0x00007A74
		public static bool Verify(string fileName)
		{ 
            //取消签名验证
            
            return true;
           
			if (File.Exists(Utils.GetBasePath() + "NO_SIGNA.CFG"))
			{
				return true;
			}
			object obj = VerifySignature.lockVerify;
			bool result;
			lock (obj)
			{
				try
				{
					if (CommandLine.CommandLineArgs.Contains("--disable-verify-signature-test"))
					{
						LogHelper.Log("[VerifySignature] [Verify] 命令行参数中有跳过验证签名的开关，所以不验证签名，直接返回 true：file_name = " + fileName);
						result = true;
					}
					else
					{
						LogHelper.Log("[VerifySignature] [Verify] 验证文件签名：file_name = " + fileName);
						if (string.IsNullOrEmpty(fileName) || !File.Exists(fileName))
						{
							LogHelper.Log("[VerifySignature] [Verify] 要验证的文件不存在：file_name = " + fileName);
							result = false;
						}
						else
						{
							string nameInfo = new X509Certificate2(fileName).GetNameInfo(X509NameType.SimpleName, false);
							if (string.IsNullOrEmpty(nameInfo) || nameInfo.ToUpper().IndexOf("LENOVO") < 0)
							{
								LogHelper.Log("[VerifySignature] [Verify] 签名无效：file_name = " + fileName + " \t file_signature = " + nameInfo);
								result = false;
							}
							else
							{
								LogHelper.Log("[VerifySignature] [Verify] 签名有效：file_name = " + fileName + " \t file_signature = " + nameInfo);
								result = true;
							}
						}
					}
				}
				catch (CryptographicException ex)
				{
					LogHelper.Log("[VerifySignature] [Verify] 无签名：file_name = " + fileName + " \t ce = " + ex.Message);
					result = false;
				}
				catch (Exception ex2)
				{
					LogHelper.Log("[VerifySignature] [Verify] 验证签名异常：file_name = " + fileName + " \t ex = " + ex2.Message);
					result = false;
				}
			}
			return result;
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00009A00 File Offset: 0x00007C00
		public static bool VerifyParentProcessSignature()
		{
			string parentPath = VerifySignature.GetParentPath();
			if (string.IsNullOrEmpty(parentPath) || !File.Exists(parentPath))
			{
				LogHelper.Log("[VerifySignature] [VerifyParentProcessSignature] 父进程文件不存在：file_name = " + parentPath);
				return false;
			}
			LogHelper.Log("[VerifySignature] [VerifyParentProcessSignature] 开始验证父进程签名：file_name = " + parentPath);
			return VerifySignature.Verify(parentPath);
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00009A4C File Offset: 0x00007C4C
		private static string GetParentPath()
		{
			string result;
			try
			{
				int id = Process.GetCurrentProcess().Id;
				string queryString = string.Format("SELECT ParentProcessId FROM Win32_Process WHERE ProcessId = {0}", id);
				ManagementObjectCollection.ManagementObjectEnumerator enumerator = new ManagementObjectSearcher("root\\CIMV2", queryString).Get().GetEnumerator();
				enumerator.MoveNext();
				string fileName = Process.GetProcessById((int)((uint)enumerator.Current["ParentProcessId"])).MainModule.FileName;
				LogHelper.Log("[VerifySignature] [GetParentPath] 获取当前里程的父进程成功：parent_file_name = " + fileName);
				result = fileName;
			}
			catch (Exception ex)
			{
				LogHelper.Log("[VerifySignature] [GetParentPath] 获取当前里程的父进程异常：ex = " + ex.Message);
				result = "";
			}
			return result;
		}

		// Token: 0x040000CC RID: 204
		private static readonly object lockVerify = new object();

		// Token: 0x040000CD RID: 205
		private const string SignatureName = "LENOVO";
	}
}
