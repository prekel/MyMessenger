using System;

namespace MyMessenger.Core
{
	public interface IMessage
	{
		int MessageId { get; set; }
		
		string Text { get; }

		DateTimeOffset SendDateTime { get; }

		int DialogId { get; }

		int AuthorId { get; }
	}
}