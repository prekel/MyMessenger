using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static System.Console;

namespace MyMessenger.Client.Console
{
	public class SmartAutoComplete
	{
		private StringBuilder CurrentString { get; set; } = new StringBuilder();

		private ConsoleKeyInfo CurrentKey { get; set; }

		private ConsoleKey Tab { get; }

		private string LastWord => CurrentString.ToString().Split().Last();

		public IList<string> CompleteList { get; set; }

		public SmartAutoComplete(IList<string> completelist, ConsoleKey tab = ConsoleKey.Tab)
		{
			Tab = tab;
			CompleteList = completelist;
		}

		public string NextString()
		{
			while (true)
			{
				CurrentKey = ReadKey(true);
				if (CurrentKey.Key == ConsoleKey.Enter)
				{
					return CurrentString.ToString();
				}
				else if (CurrentKey.Key == Tab)
				{
					CompleteOnTab();
				}
				else if (CurrentKey.Key == ConsoleKey.Backspace)
				{
					if (CurrentString.Length <= 0 || CursorLeft <= 0) continue;
					CursorLeft--;
					Write(" ");
					CursorLeft--;
					CurrentString.Remove(CurrentString.Length - 1, 1);
				}
				else
				{
					Write(CurrentKey.KeyChar);
					CurrentString.Append(CurrentKey.KeyChar);
				}
			}
		}

		private void Append(string str)
		{
			CurrentString.Append(str);
			Write(str);
		}

		private void Wipe(int len)
		{
			while (len-- > 0)
			{
				if (CurrentString.Length <= 0 || CursorLeft <= 0) continue;
				CursorLeft--;
				Write(" ");
				CursorLeft--;
				CurrentString.Remove(CurrentString.Length - 1, 1);
			}
		}

		private bool IsCompletedStarts { get; set; }
		private string LastCompletedWord { get; set; }
		private string LastCompletedSubstring { get; set; }
		private int LastIndex { get; set; }

		private void CompleteOnTab()
		{
			var lw = LastWord;
			for (var index = LastIndex; index < CompleteList.Count; index++)
			{
				var i = CompleteList[index];
				//if (i.IndexOf(lw, StringComparison.InvariantCultureIgnoreCase) == 0)
				if (i.IndexOf(lw, StringComparison.InvariantCulture) != 0) continue;
				LastCompletedSubstring = i.Substring(lw.Length);
				Write(LastCompletedSubstring);
				CurrentString.Append(LastCompletedSubstring);
				LastIndex = index;
				break;
			}
		}
	}
}