using System.ComponentModel.DataAnnotations.Schema;

namespace MyMessenger.Server.Entities
{
	public class AccountDialog
	{
		public AccountDialog()
		{

		}

		public AccountDialog(Account account, Dialog dialog)
		{
			Account = account;
			Dialog = dialog;
		}

		[NotMapped]
		public int AccountId { get; set; }
		public virtual Account Account { get; set; }

		[NotMapped]
		public int DialogId { get; set; }
		public virtual Dialog Dialog { get; set; }
	}
}
