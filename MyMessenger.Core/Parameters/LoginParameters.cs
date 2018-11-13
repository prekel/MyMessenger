namespace MyMessenger.Core.Parameters
{
	public class LoginParameters : AbstractParameters
	{
		public override CommandType CommandName { get; set; } = CommandType.Login;
		
		public string Nickname { get; set; }
		public string Password { get; set; }
	}
}