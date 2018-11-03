using System;
using Newtonsoft.Json;

namespace MyMessenger.Core
{
	[JsonObject]
	public interface IMessage
	{
		string Text { get; }

		IDialog Dialog { get; }

		IAccount Author { get; }
	}
}