using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using MyMessenger.Core;

namespace MyMessenger.Server.Entities
{
	[JsonObject(MemberSerialization.OptIn)]
	public class Dialog : IDialog
	{
		[JsonProperty] 
		public int Id { get; set; }

		//[JsonProperty]
		public virtual IList<Message> Messages { get; set; }

		//[JsonProperty]
		public virtual Account FirstMember1 { get; set; }

		//[JsonProperty]
		public virtual Account SecondMember1 { get; set; }

		[JsonProperty]
		public IAccount FirstMember => FirstMember1;

		[JsonProperty]
		public IAccount SecondMember => SecondMember1;
	}
}