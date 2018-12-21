using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Newtonsoft.Json;

using MyMessenger.Core;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyMessenger.Server.Entities
{
	public class AccountDialog
	{
		public int AccountDialogId { get; set; }

		[NotMapped]
		public int AccountId { get; set; }
		public virtual Account Account { get; set; }

		[NotMapped]
		public int DialogId { get; set; }
		public virtual Dialog Dialog { get; set; }
	}
}
