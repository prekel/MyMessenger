﻿using System.Collections;
using System.Collections.Generic;

namespace MyMessenger.Core
{
	public interface IDialog
	{
		int Id { get; set; }

		IList<IAccount> Members { get; }

		IAccount FirstMember { get; }
		IAccount SecondMember { get; }
	}
}