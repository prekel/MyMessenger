using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

using MyMessenger.Core;
using System.ComponentModel.DataAnnotations.Schema;

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
		[NotMapped]
		public virtual Account FirstMember1 { get; set; }

		//[JsonProperty]
		[NotMapped]
		public virtual Account SecondMember1 { get; set; }

		public virtual List<Account> Members1 { get; set; }

		[JsonProperty]
		public IAccount FirstMember => FirstMember1;

		[JsonProperty]
		public IAccount SecondMember => SecondMember1;
		
		[JsonProperty]
		public IList<IAccount> Members => (IList<IAccount>)Members;
	}
}