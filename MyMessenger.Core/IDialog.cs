using System.Collections;
using System.Collections.Generic;

namespace MyMessenger.Core
{
	public interface IDialog
	{
		IAccount FirstMember { get; }
		IAccount SecondMember { get; }
	}
}