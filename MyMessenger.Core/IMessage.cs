using System;

namespace MyMessenger.Core
{
	public interface IMessage
	{
		int Id { get; set; }
		
		string Text { get; }

		DateTime SendDateTime { get; }

		IDialog Dialog { get; }

		IAccount Author { get; }
	}
}