using System;

namespace MyMessenger.Core
{
	public interface IAccount
	{
		int Id { get; set; }

		DateTime RegistrationDateTime { get; }

		string Nickname { get; }
	}
}