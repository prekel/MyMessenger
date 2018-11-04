using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using MyMessenger.Core;

namespace MyMessenger.Server.Entities
{
	[JsonObject(MemberSerialization.OptIn)]
	public class Dialog : IDialog
	{
		[JsonProperty] public int Id { get; set; }

		public virtual IList<Message> Messages { get; set; }

		public virtual Account FirstMember1 { get; set; }

		public virtual Account SecondMember1 { get; set; }

		public IAccount FirstMember => FirstMember1;

		public IAccount SecondMember => SecondMember1;
	}
}