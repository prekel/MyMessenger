using System;

using Newtonsoft.Json;

namespace MyMessenger.Client
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
}
