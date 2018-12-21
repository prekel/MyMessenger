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
			if (value.CommandName == CommandType.SendMessage)
			{
				serializer.Serialize(writer, value, typeof(SendMessageParameters));
			}
			if (value.CommandName == CommandType.CreateDialog)
			{
				serializer.Serialize(writer, value, typeof(CreateDialogParameters));
			}
			if (value.CommandName == CommandType.DialogSession)
			{
				serializer.Serialize(writer, value, typeof(DialogSessionParameters));
			}
			if (value.CommandName == CommandType.GetMessageLongPool)
			{
				serializer.Serialize(writer, value, typeof(GetMessageLongPoolParameters));
			}
			if (value.CommandName == CommandType.GetAccountById)
			{
				serializer.Serialize(writer, value, typeof(GetAccountByIdParameters));
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
			else if (command == CommandType.Login)
			{
				ret = obj.ToObject<LoginParameters>();
			}
			else if (command == CommandType.SendMessage)
			{
				ret = obj.ToObject<SendMessageParameters>();
			}
			else if (command == CommandType.CreateDialog)
			{
				ret = obj.ToObject<CreateDialogParameters>();
			}
			else if (command == CommandType.DialogSession)
			{
				ret = obj.ToObject<DialogSessionParameters>();
			}
			else if (command == CommandType.GetMessageLongPool)
			{
				ret = obj.ToObject<GetMessageLongPoolParameters>();
			}
			else // if (command == CommandType.GetMessages)
			{
				ret = obj.ToObject<GetAccountByIdParameters>();
			}
			return ret;
		}
	}
}
