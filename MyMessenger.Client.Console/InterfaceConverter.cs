using System;
using System.Collections.Generic;
using System.Text;

using Newtonsoft.Json;

namespace MyMessenger.Client.Console
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
