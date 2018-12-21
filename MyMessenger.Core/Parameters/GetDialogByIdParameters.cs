using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace MyMessenger.Core.Parameters
{
	[JsonObject]
	public class GetDialogByIdParameters : AbstractParameters
	{
		public override CommandType CommandName { get; set; } = CommandType.GetDialogById;
		
		[JsonProperty]
		public string Token { get; set; }
		
		[JsonProperty]
		public int DialogId { get; set; }
	}
}
