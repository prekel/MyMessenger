using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace MyMessenger.Core.Parameters
{
	[JsonObject]
	public class GetMessagesParameters : AbstractParameters
	{
		public override CommandType CommandName { get; set; } = CommandType.GetMessages;

		[JsonProperty]
		public int DialogId { get; set; }
		
		[JsonProperty]
		public string Token { get; set; }

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
