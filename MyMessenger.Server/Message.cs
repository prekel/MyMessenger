using Microsoft.EntityFrameworkCore;

namespace MyMessenger.Server
{
	public class Message : Core.Message
	{
		public int ID { get; set; }
		public Dialog Dialog { get; set; }
	}
}