using System;
using Newtonsoft.Json;

namespace MyMessenger.Core
{
	[JsonObject]
	public interface IMessage
	{
		int Id { get; set; }
		
		string Text { get; }

		IDialog Dialog { get; }

		IAccount Author { get; }
	}
}