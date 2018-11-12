using System;
using MyMessenger.Core;
using Newtonsoft.Json;

namespace MyMessenger.Client.Console
{
	[JsonObject]
	public class Message : IMessage
	{
		[JsonProperty]
		public int Id { get; set; }
		[JsonProperty]
		public string Text { get; set; }
		//[JsonProperty(ItemConverterType = typeof(IDialogConvert))]
		[JsonProperty]
		public IDialog Dialog { get; set; }
		//[JsonProperty(ItemConverterType = typeof(IAccountConvert))]
		[JsonProperty]
		public IAccount Author { get; set; }
		
		public class Converter : JsonConverter
		{
			public override bool CanConvert(Type objectType)
			{
				return (objectType == typeof(IMessage));
			}

			public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
				JsonSerializer serializer)
			{
				return serializer.Deserialize(reader, typeof(Message));
			}

			public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
			{
				serializer.Serialize(writer, value, typeof(Message));
			}
		}
	}
}