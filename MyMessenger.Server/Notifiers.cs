using System.Collections.Generic;

namespace MyMessenger.Server
{
	public class Notifiers
	{
		private IDictionary<string, MessageNotifier> _notifiers = new Dictionary<string, MessageNotifier>();
		
		public MessageNotifier this[string token]
		{
			get
			{
				if (_notifiers.ContainsKey(token))
				{
					return _notifiers[token];
				}
				_notifiers[token] = new MessageNotifier();
				return _notifiers[token];
			}
			//set
			//{
			//	
			//}
		}
	}
}