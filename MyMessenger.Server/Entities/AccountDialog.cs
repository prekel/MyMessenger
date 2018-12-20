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
		public int AccountId { get; set; }
		public Account Account { get; set; }

		public int DialogId { get; set; }
		public Dialog Dialog { get; set; }
	}
}
