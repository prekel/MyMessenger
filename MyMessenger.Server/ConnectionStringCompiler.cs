using System.Runtime.InteropServices;
using System.Security;
using MyMessenger.Server.Configs;

namespace MyMessenger.Server
{
	public class ConnectionStringCompiler
	{
		private DbConfig Config { get; set; }
		public string Password { get; set; }
	
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