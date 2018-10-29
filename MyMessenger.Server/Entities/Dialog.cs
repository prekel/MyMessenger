using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MyMessenger.Server.Entities
{
	public class Dialog : Core.Dialog
	{
		public int ID { get; set; }
		public virtual IList<Message> Messages { get; set; }

		public new virtual Account FirstMember { get; set; }
		public new virtual Account SecondMember { get; set; }
	}
}