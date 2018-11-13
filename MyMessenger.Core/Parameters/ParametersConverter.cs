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
			if (value.CommandName == CommandType.Register)
			{
				serializer.Serialize(writer, value, typeof(RegisterParameters));
			}
			if (value.CommandName == CommandType.GetMessages)
			{
				serializer.Serialize(writer, value, typeof(GetMessagesParameters));
			}
			if (value.CommandName == CommandType.Login)
			{
				serializer.Serialize(writer, value, typeof(LoginParameters));
			}
		}

		public override AbstractParameters ReadJson(JsonReader reader, Type objectType, AbstractParameters existingValue, bool hasExistingValue,
			JsonSerializer serializer)
		{
			var obj = JObject.Load(reader);
			var command = (CommandType)Int32.Parse(obj["CommandName"].ToString());
			AbstractParameters ret;
			if (command == CommandType.Register)
			{
				ret = obj.ToObject<RegisterParameters>();
			}
			else if (command == CommandType.GetMessages)
			{
				ret = obj.ToObject<GetMessagesParameters>();
			}
			else //if (a.CommandName == "GetMessages")
			{
				ret = obj.ToObject<LoginParameters>();
			}
			return ret;
		}
	}
}
