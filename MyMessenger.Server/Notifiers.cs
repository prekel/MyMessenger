using System.Collections.Generic;

namespace MyMessenger.Server
{
	public class Notifiers
	{
		private IDictionary<int, Dictionary<string, MessageNotifier>> _notifiers = new Dictionary<int, Dictionary<string, MessageNotifier>>();

		public MessageNotifier this[int dialog, string token]
		{
			get
			{
				if (!_notifiers.ContainsKey(dialog))
				{
					_notifiers[dialog] = new Dictionary<string, MessageNotifier>();
				}
				if (_notifiers[dialog].ContainsKey(token))
				{
					return _notifiers[dialog][token];
				}
				//_notifiers[dialog][token] = new Dictionary<int, Dictionary<int, MessageNotifier>>();
				_notifiers[dialog].Add(token, new MessageNotifier());
				return _notifiers[dialog][token];
			}
			set
			{
				if (_notifiers.ContainsKey(dialog) && _notifiers[dialog].ContainsKey(token))
				{
					_notifiers[dialog][token] = value;
				}
			}
		}
	}
}