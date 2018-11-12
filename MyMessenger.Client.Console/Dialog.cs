using System;
using System.Runtime.CompilerServices;
using MyMessenger.Core;
using Newtonsoft.Json;

namespace MyMessenger.Client.Console
{
	[JsonObject]
	public class Dialog : IDialog
	{
		[JsonProperty]
		public int Id { get; set; }

		[JsonProperty]
		[JsonConverter(typeof(Converter1<Account>))]
		public IAccount FirstMember { get; set; }

		[JsonProperty]
		[JsonConverter(typeof(Converter1<Account>))]
		public IAccount SecondMember { get; set; }

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

		public class Converter1<T> : JsonConverter<T>
		{
			public override T ReadJson(JsonReader reader, Type objectType, T existingValue, bool hasExistingValue, JsonSerializer serializer)
			{
				return (T) serializer.Deserialize(reader, typeof(T));
			}

			public override void WriteJson(JsonWriter writer, T value, JsonSerializer serializer)
			{
				serializer.Serialize(writer, value, typeof(T));
			}
		}
	}
}