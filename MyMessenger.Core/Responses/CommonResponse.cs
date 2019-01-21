namespace MyMessenger.Core.Responses
{
	public class CommonResponse : AbstractResponse
	{
		public CommonResponse(ResponseCode code)
		{
			Code = code;
		}
	}
}