using UnityEngine;

namespace Assets.Infrastructure.CoreTools.Instanced.JsonFormatter
{
	public class JsonFormatter : IJsonFormatter
	{
		public T FromString<T>(string data)
		{
			return JsonUtility.FromJson<T>(data);
		}

		public string ToJsonString<T>(T data)
		{
			return JsonUtility.ToJson(data);
		}
	}
}