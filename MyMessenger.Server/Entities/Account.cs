using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Newtonsoft.Json;

using MyMessenger.Core;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace MyMessenger.Server.Entities
{
	[JsonObject(MemberSerialization.OptIn)]
	public class Account : IAccount
	{
		[JsonProperty]
		public int AccountId { get; set; }

		[JsonProperty]
		public string Nickname { get; set; }

		[JsonProperty]
		public IEnumerable<int> DialogsIds => Dialogs.Select(p => p.Dialog.DialogId);

		//[JsonProperty]
		//public IEnumerable<IDialog> DialogsA => Dialogs.Select(p => p.Dialog);

		[JsonProperty]
		public DateTime RegistrationDateTime { get; set; }

		[JsonProperty]
		public DateTime LoginDateTime { get; set; }

		[MaxLength(32)]
		public byte[] PasswordHash { get; set; }
		public int PasswordSalt { get; set; }
		
		public virtual IList<AccountDialog> Dialogs { get; set; }

		public virtual IList<Message> Messages { get; set; }
	}
}  