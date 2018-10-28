using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyMessenger.Server.Entities
{
	public class Account : Core.Account
	{
		public int ID { get; set; }

		[MaxLength(32)]
		public byte[] PasswordHash { get; set; }
		public int PasswordSalt { get; set; }

		public virtual IList<Dialog> Dialogs { get; set; }
		public virtual IList<Message> Messages { get; set; }
	}
}