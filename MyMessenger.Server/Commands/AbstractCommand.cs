using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MyMessenger.Core.Parameters;
using MyMessenger.Core;
using MyMessenger.Core.Responses;
using NLog;

namespace MyMessenger.Server.Commands
{
	public abstract class AbstractCommand : ICommand
	{
		private static Logger Log { get; } = LogManager.GetCurrentClassLogger();

		protected AbstractParameters Config { get; set; }
		protected MessengerContext Context { get; set; }

		protected IDictionary<string, IAccount> Tokens { get; set; }
		
		public AbstractResponse Response { get; set; }

		public static CommandType CommandName { get; protected set; }

		public ResponseCode Code
		{
			get => Response.Code;
			set => Response.Code = value;
		}

		protected AbstractCommand(MessengerContext context, AbstractParameters config)
		{
			Context = context;
			Config = config;
		}

		protected AbstractCommand(MessengerContext context, IDictionary<string, IAccount> tokens, AbstractParameters config) : this(context, config)
		{
			Tokens = tokens;
		}

		public void Execute()
		{
			Log.Trace($"Поток:  {Thread.CurrentThread.ManagedThreadId} Выполняется запрос {CommandName}");
			ExecuteImpl();
			Log.Trace($"Поток:  {Thread.CurrentThread.ManagedThreadId} Возвращено {Code}");
		}

		public async Task ExecuteAsync()
		{
			Log.Trace($"Поток:  {Thread.CurrentThread.ManagedThreadId} Выполняется запрос {CommandName}");
			await ExecuteImplAsync();
			Log.Trace($"Поток:  {Thread.CurrentThread.ManagedThreadId} Возвращено {Code}");
		}

		protected abstract void ExecuteImpl();

		protected abstract Task ExecuteImplAsync();
	}
}
