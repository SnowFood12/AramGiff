using Newtonsoft.Json;
using System.Text.Json;

namespace Aram.Infrastructure
{
	public static class SessionExtensions
	{
		public static void SetJson(this ISession session, string key, object value)
		{
			session.SetString(key, System.Text.Json.JsonSerializer.Serialize(value));
		}
		public static T? GetJson<T>(this ISession session, string key)
		{
			var sessionData = session.GetString(key); 
			return sessionData == null 
				? default(T) : System.Text.Json.JsonSerializer.Deserialize<T>(sessionData);
		}
	}
}