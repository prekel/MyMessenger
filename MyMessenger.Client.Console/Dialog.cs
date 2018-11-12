using System;
using System.Runtime.CompilerServices;
using MyMessenger.Core;
using Newtonsoft.Json;

namespace MyMessenger.Client.Console
{
	[JsonObject]
	public class Dialog : IDialog
	{
		[JsonProperty] public int Id { get; set; }
		[JsonProperty] public IAccount FirstMember { get; set; }
		[JsonProperty] public IAccount SecondMember { get; set; }

		public class Converter : JsonConverter
		{
			public override bool CanConvert(Type objectType)
			{
				return (objectType == typeof(IDialog));
			}

			public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
				JsonSerializer serializer)
			{
				return serializer.Deserialize(reader, typeof(Dialog));
			}

			public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
			{
				serializer.Serialize(writer, value, typeof(Dialog));
			}
		}
	}
}