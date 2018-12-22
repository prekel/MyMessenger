using System.Text;

namespace MyMessenger.Client.Console
{
	public static class ConsoleWipe
	{
		public static void Append(StringBuilder current, string str)
		{
			current.Append(str);
			System.Console.Write(str);
		}

		public static void Wipe(StringBuilder current, int len)
		{
			while (len-- > 0)
			{
				if (current.Length <= 0 || System.Console.CursorLeft <= 0) continue;
				System.Console.CursorLeft--;
				System.Console.Write(" ");
				System.Console.CursorLeft--;
				current.Remove(current.Length - 1, 1);
			}
		}
	}
}