using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Newtonsoft.Json;

using MyMessenger.Core;

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

		[JsonProperty]
		public DateTimeOffset RegistrationDateTime { get; set; }

		[JsonProperty]
		public DateTimeOffset LoginDateTime { get; set; }

		//[JsonProperty]
		//public TimeZoneInfo TimeZone { get; set; }

		[MaxLength(32)]
		public byte[] PasswordHash { get; set; }
		public int PasswordSalt { get; set; }
		
		public virtual IList<AccountDialog> Dialogs { get; set; }

		public virtual IList<Message> Messages { get; set; }

		public Account()
		{

		}

		public Account(int id)
		{
			AccountId = id;
		}
	}
}  