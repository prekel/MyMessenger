using System;
using MyMessenger.Core;
using Newtonsoft.Json;

namespace MyMessenger.Client.Console
{
	[JsonObject]
	public class Account : IAccount
	{
		[JsonProperty] public int Id { get; set; }

		[JsonProperty] public string Nickname { get; set; }

		public class Converter : JsonConverter
		{
			public override bool CanConvert(Type objectType)
			{
				return (objectType == typeof(IAccount));
			}

			public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
				JsonSerializer serializer)
			{
				return serializer.Deserialize(reader, typeof(Account));
			}

			public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
			{
				serializer.Serialize(writer, value, typeof(Account));
			}
		}
	}
}