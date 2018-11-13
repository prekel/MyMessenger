using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace MyMessenger.Core.Parameters
{
	[JsonObject]
	public class CreateDialogParameters : AbstractParameters
	{
		public override CommandType CommandName { get; set; } = CommandType.CreateDialog;

		[JsonProperty]
		public string Token { get; set; }

		[JsonProperty]
		public string SecondMemberNickname { get; set; }

		[JsonProperty]
		public int? SecondMemberId { get; set; }
	}
}
