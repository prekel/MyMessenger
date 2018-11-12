using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using MyMessenger.Server.Entities;

namespace MyMessenger.Server
{
	public class GetMessages : ICommand
	{
		private Query Config { get; set; }

		private MessengerContext Context { get; set; }
		//private Fields Fields1 { get; set; }

		public IQueryable<Message> Result { get; private set; }

		public GetMessages(MessengerContext context, Query query)
		{
			Context = context;
			Config = query;
		}

		public void Execute()
		{
			var r = from i in Context.Messages where i.Dialog1.Id == Config.DialogId select i;
			if (Config.Fields1.HasFlag(Query.Fields.Author))
			{
				var r1 = r.Include(p => p.Author1);
				if (Config.Fields1.HasFlag(Query.Fields.AuthorDialogs))
				{
					r = r1.ThenInclude(p => p.Dialogs);
				}
			}

			if (Config.Fields1.HasFlag(Query.Fields.Dialog))
			{
				var r1 = r.Include(p => p.Dialog1);
				if (Config.Fields1.HasFlag(Query.Fields.DialogFirstMember))
				{
					r = r1.ThenInclude(p => p.FirstMember1);
				}

				if (Config.Fields1.HasFlag(Query.Fields.DialogSecondMember))
				{
					r = r1.ThenInclude(p => p.SecondMember1);
				}
			}

			Result = r;
		}

		public class Query
		{
			public int DialogId { get; set; }
			public Fields Fields1 { get; set; }

			public enum Fields
			{
				Author = 1,
				AuthorDialogs = 2,
				Dialog = 4,
				DialogFirstMember = 8,
				DialogSecondMember = 16
			}
		}

	}
		/*public class GMJsonConverter : JsonConverter<IQueryable<Message>>
		{
			private GetMessages.Query.Fields Fields;

			public GMJsonConverter(GetMessages.Query.Fields fields)
			{
				Fields = fields;
			}

			public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
			{
				var r = new JObject();
				var val = (IQueryable<Message>) value;
				foreach (var i in val)
				{
					if (Fields.HasFlag(GetMessages.Query.Fields.Author))
					{
						var n = new JObject();
						if (Fields.HasFlag(GetMessages.Query.Fields.AuthorDialogs))
						{
						}
						var y = new JObject {new JProperty("Author", i.Author1)};
						r.Add(y);
					}
				}
				
				var t = JToken.FromObject(value);

				if (t.Type != JTokenType.Object)
				{
					t.WriteTo(writer);
				}
				else
				{
					JObject o = (JObject) t;
					IList<string> propertyNames = o.Properties().Select(p => p.Name).ToList();

					o.AddFirst(new JProperty("Keys", new JArray(propertyNames)));

					o.WriteTo(writer);
				}
			}

			public override void WriteJson(JsonWriter writer, IQueryable<Message> value, JsonSerializer serializer)
			{
				foreach (var i in value)
				{
					writer.WriteValue(i.Text);
				}
			}

			public override IQueryable<Message> ReadJson(JsonReader reader, Type objectType, IQueryable<Message> existingValue, bool hasExistingValue,
				JsonSerializer serializer)
			{
				throw new NotImplementedException();
			}
		}*/
}