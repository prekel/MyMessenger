namespace MyMessenger.Core
{
	public interface IAccount
	{
		int Id { get; set; }
		string Nickname { get; }
	}
}