using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static System.Console;

namespace MyMessenger.Client.Console
{
	public class SmartConsoleReader
	{
		private StringBuilder CurrentString { get; } = new StringBuilder();

		private ConsoleKeyInfo CurrentKey { get; set; }

		private ConsoleKey Tab { get; }

		private string LastWord => CurrentString.ToString().Split().Last();

		public IEnumerable<string> CompleteList { get; set; }

		public SmartConsoleReader(IEnumerable<string> completelist, ConsoleKey tab = ConsoleKey.Tab)
		{
			Tab = tab;
			CompleteList = completelist;
		}

		private SmartAutoComplete AutoCompleter { get; set; }

		public string NextString()
		{
			while (true)
			{
				CurrentKey = ReadKey(true);
				if (CurrentKey.Key == ConsoleKey.Enter)
				{
					Write('\n');
					var ret = CurrentString.ToString();
					CurrentString.Clear();
					return ret;
				}
				else if (CurrentKey.Key == Tab)
				{
					if (AutoCompleter == null)
					{
						AutoCompleter = new SmartAutoComplete(CurrentString, LastWord, CompleteList);
					}
					AutoCompleter.CompleteOnTab();
				}
				else if (CurrentKey.Key == ConsoleKey.Backspace)
				{
					AutoCompleter = null;
					if (CurrentString.Length <= 0 || CursorLeft <= 0) continue;
					CursorLeft--;
					Write(" ");
					CursorLeft--;
					CurrentString.Remove(CurrentString.Length - 1, 1);
				}
				else
				{
					AutoCompleter = null;
					if (CurrentKey.KeyChar == '\0') continue;
					Write(CurrentKey.KeyChar);
					CurrentString.Append(CurrentKey.KeyChar);
				}
			}
		}
	}
}