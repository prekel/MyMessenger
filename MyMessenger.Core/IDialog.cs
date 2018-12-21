using System.Collections;
using System.Collections.Generic;

namespace MyMessenger.Core
{
	public interface IDialog
	{
		int DialogId { get; set; }

		IEnumerable<IAccount> MembersA { get; }

		//IAccount FirstMember { get; }
		//IAccount SecondMember { get; }
	}
}