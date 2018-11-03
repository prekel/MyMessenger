using MyMessenger.Core;

namespace MyMessenger.Client.Console
{
	public class Message : IMessage
	{
		public string Text { get; set; }
		public IDialog Dialog { get; set; }
		public IAccount Author { get; set; }
	}
}