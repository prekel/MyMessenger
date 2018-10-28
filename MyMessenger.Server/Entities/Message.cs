using Microsoft.EntityFrameworkCore;

namespace MyMessenger.Server.Entities
{
	public class Message : Core.Message
	{
		public int ID { get; set; }
		public new Dialog Dialog { get; set; }
		public new Account Author;
	}
}