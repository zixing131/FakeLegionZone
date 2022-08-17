using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace FakeLegionZone.Util
{
	// Token: 0x0200002E RID: 46
	public class JsonHelper
	{
		// Token: 0x06000137 RID: 311 RVA: 0x00007938 File Offset: 0x00005B38
		public static string ObjectToString(object jsonObject)
		{
			string result = "";
			try
			{
				using (MemoryStream memoryStream = new MemoryStream())
				{
					new DataContractJsonSerializer(jsonObject.GetType()).WriteObject(memoryStream, jsonObject);
					memoryStream.Position = 0L;
					using (StreamReader streamReader = new StreamReader(memoryStream))
					{
						result = streamReader.ReadToEnd();
					}
				}
			}
			catch (Exception ex)
			{
				LogHelper.Log("[JsonHelper] [ObjectToString] object to string 异常：" + ex.Message);
			}
			return result;
		}

		// Token: 0x06000138 RID: 312 RVA: 0x000079D4 File Offset: 0x00005BD4
		public static T StringToObject<T>(string jsonString)
		{
			T result;
			try
			{
				using (MemoryStream memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(jsonString)))
				{
					result = (T)((object)new DataContractJsonSerializer(typeof(T)).ReadObject(memoryStream));
				}
			}
			catch (Exception ex)
			{
				result = default(T);
				LogHelper.Log("[JsonHelper] [StringToObject] string to object 异常：" + ex.Message);
			}
			return result;
		}
	}
}
