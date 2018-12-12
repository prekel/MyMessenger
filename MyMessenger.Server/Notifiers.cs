using System.Collections.Generic;

namespace MyMessenger.Server
{
	public class Notifiers
	{
		private IDictionary<int, Dictionary<int, MessageNotifier>> _notifiers = new Dictionary<int, Dictionary<int, MessageNotifier>>();

		public MessageNotifier this[int dialog, int token]
		{
			get
			{
				if (!_notifiers.ContainsKey(dialog))
				{
					_notifiers[dialog] = new Dictionary<int, MessageNotifier>();
				}
				if (_notifiers[dialog].ContainsKey(token))
				{
					return _notifiers[dialog][token];
				}
				//_notifiers[dialog][token] = new Dictionary<int, Dictionary<int, MessageNotifier>>();
				_notifiers[dialog].Add(token, new MessageNotifier());
				return _notifiers[dialog][token];
			}
			//set
			//{
			//	
			//}
		}
	}
}