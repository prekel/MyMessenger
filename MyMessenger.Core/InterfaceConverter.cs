using System;
using Newtonsoft.Json;

namespace MyMessenger.Core
{
	public class InterfaceConverter<T> : JsonConverter<T>
	{
		public override T ReadJson(JsonReader reader, Type objectType, T existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			return (T)serializer.Deserialize(reader, typeof(T));
		}

		public override void WriteJson(JsonWriter writer, T value, JsonSerializer serializer)
		{
			serializer.Serialize(writer, value, typeof(T));
		}
	}

	public class InterfaceConverter<I, T> : JsonConverter
	{
		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(I);
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			return (T)serializer.Deserialize(reader, typeof(T));
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			serializer.Serialize(writer, value, typeof(T));
		}
	}
}
