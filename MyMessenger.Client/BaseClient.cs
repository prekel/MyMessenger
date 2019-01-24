using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using MyMessenger.Client.Commands;
using MyMessenger.Core;

namespace MyMessenger.Client
{
	public class BaseClient
	{
		private IPEndPoint ServerIp { get; set; }

		private string Token { get; set; }

		public IAccount Account { get; private set; }

		//public int DialogId { get; set; }

		public async Task<bool> Connect(IPEndPoint ip, string login, string password)
		{
			ServerIp = ip;

			using (var client = new TcpClient())
			{
				await client.ConnectAsync(ServerIp.Address, ServerIp.Port);

				var stream = client.GetStream();

				var command = new Login(stream, login, password);

				try
				{
					await command.ExecuteAsync();
				}
				catch
				{
					return false;
				}

				if (command.Response.Token == null)
				{
					return false;
				}

				Token = command.Response.Token;

				Account = command.Response.Account;
			}

			return true;
			//return await Task.FromResult<string>(command.RawResponse);
		}

		public async Task SendMessage(int dialogid, string text)
		{
			using (var client = new TcpClient())
			{
				await client.ConnectAsync(ServerIp.Address, ServerIp.Port);

				var command = new SendMessage(client.GetStream(), Token, dialogid, text);

				await command.ExecuteAsync();
			}
		}

		public async Task<IMessage> GetMessageLongPool(int dialogid, TimeSpan ts)
		{
			using (var client = new TcpClient())
			{
				await client.ConnectAsync(ServerIp.Address, ServerIp.Port);

				var command = new GetMessageLongPool(client.GetStream(), Token, dialogid, ts);

				await command.ExecuteAsync();

				return command.Response.Content;
			}
		}

		public async Task<IDialog> GetDialogById(int dialogid)
		{
			using (var client = new TcpClient())
			{
				await client.ConnectAsync(ServerIp.Address, ServerIp.Port);

				var command = new GetDialogById(client.GetStream(), Token, dialogid);

				await command.ExecuteAsync();

				return command.Response.Dialog;
			}
		}

		public async Task<IAccount> GetAccountById(int accountid)
		{
			using (var client = new TcpClient())
			{
				await client.ConnectAsync(ServerIp.Address, ServerIp.Port);

				var command = new GetAccountById(client.GetStream(), Token, accountid);

				await command.ExecuteAsync();

				return command.Response.Account;
			}
		}

		public async Task CreateDialog(IList<int> membersids)
		{
			using (var client = new TcpClient())
			{
				await client.ConnectAsync(ServerIp.Address, ServerIp.Port);

				var command = new CreateDialog(client.GetStream(), Token, membersids);

				await command.ExecuteAsync();

				//return command.Response.Account;
			}
		}

		public async Task CreateDialog(IList<string> membersnicknames)
		{
			using (var client = new TcpClient())
			{
				await client.ConnectAsync(ServerIp.Address, ServerIp.Port);

				var command = new CreateDialog(client.GetStream(), Token, membersnicknames);

				await command.ExecuteAsync();

				//return command.Response.Account;
			}
		}
	}
}