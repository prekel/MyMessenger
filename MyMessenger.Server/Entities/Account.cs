namespace MyMessenger.Server.Entities
{
	public class Account : Core.Account
	{
		public byte[] PasswordHash;
		public int PasswordSalt;
	}
}