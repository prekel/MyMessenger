using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;

namespace MyMessenger.Client.Console
{
	public class SmartAutoComplete
	{
		private StringBuilder CurrentString { get; }

		private bool IsCompletedStarts { get; set; }

		private string LastCompletedWord { get; set; }
		private string LastCompletedSubstring { get; set; }
		//private int LastIndex { get; set; }

		private IList<string> CompleteList { get; }
		private string LastWord { get; }

		private IEnumerator<string> CompleteListEnumerator { get; }

		public SmartAutoComplete(StringBuilder currentString, string last, IList<string> completeList)
		{
			CurrentString = currentString;
			LastWord = last;
			CompleteList = completeList;
			CompleteListEnumerator = completeList.GetEnumerator();
		}

		~SmartAutoComplete()
		{
			CompleteListEnumerator.Dispose();
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

		public void CompleteOnTab()
		{
			if (IsCompletedStarts)
			{
				Wipe(LastCompletedSubstring.Length);
			}
				IsCompletedStarts = true;
			while (true)
			{
				if (CompleteListEnumerator.MoveNext() == false)
				{
					CompleteListEnumerator.Reset();
					CompleteListEnumerator.MoveNext();
				}
				var i = CompleteListEnumerator.Current;
				if (i.IndexOf(LastWord, StringComparison.InvariantCulture) == 0)
				{
					LastCompletedWord = i;
					break;
				}
			}

			LastCompletedSubstring = LastCompletedWord.Substring(LastWord.Length);

			Append(LastCompletedSubstring);
		}
	}
}