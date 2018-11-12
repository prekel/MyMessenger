using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MyMessenger.Server.Entities;

namespace MyMessenger.Server.Commands
{
	public class GetMessages : AbstractCommand
	{
		protected override AbstractParameters AbstractConfig { get; set; }
		private Parameters Config { get => (Parameters)AbstractConfig; set => AbstractConfig = value; }
		
		public IQueryable<Message> Result { get; private set; }

		public GetMessages(MessengerContext context, Parameters parameters) : base(context, parameters)
		{
			Context = context;
			Config = parameters;
		}


		public override void Execute()
		{
			var r = from i in Context.Messages where i.Dialog1.Id == Config.DialogId select i;
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

		public class Parameters : AbstractParameters
		{
			public int DialogId { get; set; }

			//public Fields Fields1 { get; set; }

			//[Flags]
			//public enum Fields
			//{
			//	Author = 1,
			//	AuthorDialogs = 2,
			//	Dialog = 4,
			//	DialogFirstMember = 8,
			//	DialogSecondMember = 16
			//}
		}
	}
}