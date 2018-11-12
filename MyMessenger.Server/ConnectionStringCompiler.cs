using System.Runtime.InteropServices;
using System.Security;

using MyMessenger.Server.Configs;

namespace MyMessenger.Server
{
	public class ConnectionStringCompiler
	{
		private DbConfig Config { get; set; }
		private string _password;

		private string Password
		{
			get => _password ?? Config.Password;
			set => _password = value;
		}

		public ConnectionStringCompiler(DbConfig config)
		{
			Config = config;
		}

		public string Compile(string pass)
		{
			Password = pass;
			return Compile();
		}

		public string Compile()
		{
			return $"server={Config.Server};port={Config.Port};database={Config.Name};user={Config.User};password={Password};SslMode={Config.SslMode}";
		}
	}
}