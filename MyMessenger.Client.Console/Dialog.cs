using System.Runtime.CompilerServices;
using MyMessenger.Core;

namespace MyMessenger.Client.Console
{
	public class Dialog : IDialog
	{
		public IAccount FirstMember { get; set; }
		public IAccount SecondMember { get; set; }
	}
}