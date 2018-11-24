using System.Collections.Generic;

namespace MyMessenger.Server
{
	public class Notifiers
	{
		private IDictionary<int, MessageNotifier> _notifiers = new Dictionary<int, MessageNotifier>();
		
		public MessageNotifier this[int dialog]
		{
			get
			{
				if (_notifiers.ContainsKey(dialog))
				{
					return _notifiers[dialog];
				}
				_notifiers[dialog] = new MessageNotifier();
				return _notifiers[dialog];
			}
			//set
			//{
			//	
			//}
		}
	}
}