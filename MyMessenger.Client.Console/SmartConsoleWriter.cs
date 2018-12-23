using static System.Console;

namespace MyMessenger.Client.Console
{
	public class SmartConsoleWriter
	{
		private SmartConsoleReader Reader { get; }

		public SmartConsoleWriter(SmartConsoleReader reader)
		{
			Reader = reader;
		}

		public void WriteLine(string s)
		{
			Reader.WipeCurrent();
			System.Console.WriteLine(s);
			Reader.WriteCurrent();
		}
	}
}