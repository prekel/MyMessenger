using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using MyMessenger.Core;

namespace MyMessenger.Server.Entities
{
	[JsonObject]
	public class Dialog : IDialog
	{
		[JsonProperty] public int Id { get; set; }

		[JsonIgnore] public virtual IList<Message> Messages { get; set; }

		[JsonIgnore] protected virtual Account FirstMember1 { get; set; }

		[JsonIgnore] protected virtual Account SecondMember1 { get; set; }

		[JsonIgnore] public IAccount FirstMember => FirstMember1;

		[JsonIgnore] public IAccount SecondMember => SecondMember1;
	}
}