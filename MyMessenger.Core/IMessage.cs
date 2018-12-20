using System;

namespace MyMessenger.Core
{
	public interface IMessage
	{
		int MessageId { get; set; }
		
		string Text { get; }

		DateTime SendDateTime { get; }

		IDialog DialogA { get; }

		IAccount AuthorA { get; }
	}
}