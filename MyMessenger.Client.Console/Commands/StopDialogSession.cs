using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using MyMessenger.Client.Commands;
using MyMessenger.Core.Responses;

namespace MyMessenger.Client.Console.Commands
{
	public class StopDialogSession //: AbstractCommand
	{
		public static ICollection<string> CommandNames { get; } = new List<string>(new[] {"stopdialogsession", "sds"});
		
		public CancellationTokenSource CancellationTokenSource { get; } 

		public StopDialogSession(CancellationTokenSource cancellationTokenSource)
		{
			CancellationTokenSource = cancellationTokenSource;
		}

		public void Execute()
		{
			CancellationTokenSource.Cancel();
		}
	}
}