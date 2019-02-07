using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

using MyMessenger.Core;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace MyMessenger.Server.Entities
{
	[JsonObject(MemberSerialization.OptIn)]
	public class Dialog : IDialog
	{
		[JsonProperty]
		public int DialogId { get; set; }

		[JsonProperty]
		public IEnumerable<int> MembersIds => Members.Select(p => p.Account.AccountId);

		//[JsonProperty]
		//public IEnumerable<IAccount> MembersA => Members.Select(p => p.Account);

		public virtual IList<Message> Messages { get; set; }

		public virtual List<AccountDialog> Members { get; set; }

		public Dialog()
		{

		}

		public Dialog(int id)
		{
			DialogId = id;
		}
	}
}