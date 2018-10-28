using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MyMessenger.Server.Entities
{
	public class Dialog : Core.Dialog
	{
		public int ID { get; set; }
		public IList<Message> Messages { get; set; }

		public new Account FirstMember { get; set; }
		public new Account SecondMember { get; set; }
	}
}