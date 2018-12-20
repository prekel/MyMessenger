using System;

namespace MyMessenger.Core
{
	public interface IAccount
	{
		int AccountId { get; set; }

		DateTime RegistrationDateTime { get; }

		string Nickname { get; }
	}
}