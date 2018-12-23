using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyMessenger.Client.Console
{
	public class SmartAutoComplete
	{
		private StringBuilder CurrentString { get; }

		private bool IsCompletedStarts { get; set; }

		private string LastCompletedWord { get; set; }
		private string LastCompletedSubstring { get; set; }
		//private int LastIndex { get; set; }

		private IEnumerable<string> CompleteList { get; }
		private string LastWord { get; }

		private IEnumerator<string> CompleteListEnumerator { get; }

		public SmartAutoComplete(StringBuilder currentString, string last, IEnumerable<string> completeList)
		{
			CurrentString = currentString;
			LastWord = last;
			CompleteList = completeList
				.Where(p => p.StartsWith(LastWord, StringComparison.InvariantCulture))
				.Select(p => p)
				.Append(LastWord)
				.ToList();
			CompleteListEnumerator = CompleteList.GetEnumerator();
		}

		~SmartAutoComplete()
		{
			CompleteListEnumerator.Dispose();
		}

		public void CompleteOnTab()
		{
			if (IsCompletedStarts)
			{
				ConsoleWipe.Wipe(CurrentString, LastCompletedSubstring.Length);
			}
			IsCompletedStarts = true;

			if (!CompleteList.Any())
			{
				return;
			}

			if (CompleteListEnumerator.MoveNext() == false)
			{
				CompleteListEnumerator.Reset();
				CompleteListEnumerator.MoveNext();
			}

			LastCompletedWord = CompleteListEnumerator.Current;

			LastCompletedSubstring = LastCompletedWord.Substring(LastWord.Length);

			ConsoleWipe.Append(CurrentString, LastCompletedSubstring);
		}
	}
}