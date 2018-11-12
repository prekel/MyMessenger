using System;
using Newtonsoft.Json;

namespace MyMessenger.Core.Parameters
{
	[JsonObject]
	public class Query
	{
		[JsonProperty]
		[JsonConverter(typeof(ParametersConverter))]
		public AbstractParameters Config { get; set; }
	}
}