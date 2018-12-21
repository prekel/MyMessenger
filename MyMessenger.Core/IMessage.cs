using System;

namespace MyMessenger.Core
{
	public interface IMessage
	{
		int MessageId { get; set; }
		
		string Text { get; }

		DateTime SendDateTime { get; }

		int DialogId { get; }
		//IDialog DialogA { get; }

		int AuthorId { get; }
		//IAccount AuthorA { get; }
	}
}