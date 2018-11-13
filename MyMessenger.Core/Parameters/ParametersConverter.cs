using System;
using System.Collections.Generic;
using System.Text;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MyMessenger.Core.Parameters
{
	public class ParametersConverter : JsonConverter<AbstractParameters>
	{
		public override void WriteJson(JsonWriter writer, AbstractParameters value, JsonSerializer serializer)
		{
			if (value.CommandName == "Register")
			{
				serializer.Serialize(writer, value, typeof(RegisterParameters));
			}
			if (value.CommandName == "GetMessages")
			{
				serializer.Serialize(writer, value, typeof(GetMessagesParameters));
			}
		}

		public override AbstractParameters ReadJson(JsonReader reader, Type objectType, AbstractParameters existingValue, bool hasExistingValue,
			JsonSerializer serializer)
		{
			//var a = (AbstractParameters)serializer.Deserialize(reader, typeof(AbstractParameters));
			var obj = JObject.Load(reader);
			var command = obj["CommandName"].ToString();
			AbstractParameters ret;
			if (command == "Register")
			{
				ret = obj.ToObject<RegisterParameters>();
				//ret = (RegisterParameters)serializer.Deserialize(reader, typeof(RegisterParameters));
			}
			else //if (a.CommandName == "GetMessages")
			{
				ret = obj.ToObject<GetMessagesParameters>();
				//ret = (GetMessagesParameters)serializer.Deserialize(reader, typeof(GetMessagesParameters));
			}
			return ret;
		}
	}
}
