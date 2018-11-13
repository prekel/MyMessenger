using System;
using System.Linq;

using Microsoft.EntityFrameworkCore;

using MyMessenger.Core.Parameters;
using MyMessenger.Server.Entities;

namespace MyMessenger.Server.Commands
{
	public class GetMessages : AbstractCommand
	{
		private GetMessagesParameters Config1 { get => (GetMessagesParameters)Config; set => Config = value; }
		
		public IQueryable<Message> Result { get; private set; }
		
		public GetMessages(MessengerContext context, AbstractParameters config) : base(context, config)
		{
		}

		public override void Execute()
		{
			var r = from i in Context.Messages where i.Dialog1.Id == Config1.DialogId select i;

			//if (AbstractConfig.Fields1.HasFlag(Parameters.Fields.Author))
			//{
			//	var r1 = r.Include(p => p.Author1);
			//	if (AbstractConfig.Fields1.HasFlag(Parameters.Fields.AuthorDialogs))
			//	{
			//		r = r1.ThenInclude(p => p.Dialogs);
			//	}
			//}

			//if (AbstractConfig.Fields1.HasFlag(Parameters.Fields.Dialog))
			//{
			//	var r1 = r.Include(p => p.Dialog1);
			//	if (AbstractConfig.Fields1.HasFlag(Parameters.Fields.DialogFirstMember))
			//	{
			//		r = r1.ThenInclude(p => p.FirstMember1);
			//	}

			//	if (AbstractConfig.Fields1.HasFlag(Parameters.Fields.DialogSecondMember))
			//	{
			//		r = r1.ThenInclude(p => p.SecondMember1);
			//	}
			//}

			Result = r;
		}
	}
}