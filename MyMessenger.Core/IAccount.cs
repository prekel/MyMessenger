using System;
using System.Collections.Generic;

namespace MyMessenger.Core
{
	public interface IAccount
	{
		int AccountId { get; set; }

		DateTime RegistrationDateTime { get; }

		DateTime LoginDateTime { get; }

		string Nickname { get; }

		IEnumerable<IDialog> DialogsA { get; }
	}
}