using System;
using MyMessenger.Core;
using System.Collections.Generic;
using System.Threading;

namespace MyMessenger.Server
{
	public class MessageNotifier : IDisposable
	{
		public event EventHandler<MessageNotifierEventArgs> NewMessage;

		public void MessageSent(IMessage message)
		{
			OnNewMessage(message);
			CancellationTokenSource.Cancel();
			CancellationTokenSource = new CancellationTokenSource();
		}

		protected virtual void OnNewMessage(IMessage message)
		{
			NewMessage?.Invoke(this, new MessageNotifierEventArgs(message));
		}

		private CancellationTokenSource CancellationTokenSource { get; set; } = new CancellationTokenSource();

		public CancellationToken CancellationToken => CancellationTokenSource.Token;

		public MessageNotifier()
		{
		}

		#region IDisposable Support
		private bool disposedValue = false;

		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					// TODO: dispose managed state (managed objects).
				}

				// TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
				// TODO: set large fields to null.
				NewMessage += MessageNotifier_NewMessage;

				disposedValue = true;
			}
		}

		private void MessageNotifier_NewMessage(object sender, MessageNotifierEventArgs e)
		{
			throw new NotImplementedException();
		}

		public void Dispose()
		{
			Dispose(true);
		}
		#endregion
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