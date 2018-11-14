using System;
using MyMessenger.Core;

namespace MyMessenger.Server
{
	public class MessageNotifier
	{
		public event EventHandler<MessageNotifierEventArgs> NewMessage;

		public void MessageSent(IMessage message)
		{
			OnNewMessage(message);
		}

		protected virtual void OnNewMessage(IMessage message)
		{
			NewMessage?.Invoke(this, new MessageNotifierEventArgs(message));
		}
	}

	public class MessageNotifierEventArgs : EventArgs
	{
		public IMessage Message { get; private set; }

		public MessageNotifierEventArgs(IMessage message)
		{
			Message = message;
		}
	}
}