using System;
using System.Collections;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using static System.Console;

using Newtonsoft.Json;
using NLog;

using MyMessenger.Core;
using MyMessenger.Core.Parameters;
using MyMessenger.Core.Responses;
using MyMessenger.Server.Commands;
using MyMessenger.Server.Configs;
using MyMessenger.Server.Entities;

namespace MyMessenger.Server
{
	public abstract class AbstractServer : IDisposable
	{
		//public MessengerContext Context { get; set; }

		public IDictionary<string, IAccount> Tokens { get; } = new Dictionary<string, IAccount>();
		//private IDictionary<int, MessageNotifier> Notifiers { get; set; } = new Dictionary<int, MessageNotifier>();

		public Notifiers Notifiers { get; } = new Notifiers();

		public abstract void Dispose();
	}
}
