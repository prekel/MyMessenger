using System;
using System.Collections.Generic;
using System.Text;

namespace MyMessenger.Core.Parameters
{
	public class GetMessagesParameters : AbstractParameters
	{
		public int DialogId { get; set; }

		//public Fields Fields1 { get; set; }

		//[Flags]
		//public enum Fields
		//{
		//	Author = 1,
		//	AuthorDialogs = 2,
		//	Dialog = 4,
		//	DialogFirstMember = 8,
		//	DialogSecondMember = 16
		//}
	}
}
