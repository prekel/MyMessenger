using System.Collections.Generic;
using MyMessenger.Core;
using Terminal.Gui;

namespace MyMessenger.Client.TerminalGui
{
	public class MessagesDataSource : List<IMessage>, IListDataSource
	{
		private int MarkedItem { get; set; }

		public void Render(bool selected, int item, int col, int line, int width)
		{
			throw new System.NotImplementedException();
		}

		public bool IsMarked(int item)
		{
			return item == MarkedItem;
		}

		public void SetMark(int item, bool value)
		{
			if (value)
			{
				MarkedItem = item;
			}
			else
			{
				MarkedItem = -1;
			}
		}

		//public int Count { get; }
	}
}