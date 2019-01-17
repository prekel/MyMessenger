using System;
using System.Collections.Generic;

namespace MyMessenger.Core
{
	public interface IAccount
	{
		int AccountId { get; set; }

		DateTimeOffset RegistrationDateTime { get; }

		DateTimeOffset LoginDateTime { get; }

		string Nickname { get; }

		IEnumerable<int> DialogsIds { get; }

		//IEnumerable<IDialog> DialogsA { get; }

		//TimeZoneInfo TimeZone { get; }
	}
}