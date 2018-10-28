using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MyMessenger.Server
{
	public class Dialog
	{
		public int ID { get; set; }
		public IList<Message> Messages { get; set; }
	}
}