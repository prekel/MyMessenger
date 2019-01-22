using System;

using MyMessenger.Client.Xamarin.Forms.Models;

namespace MyMessenger.Client.Xamarin.Forms.ViewModels
{
	public class ItemDetailViewModel : BaseViewModel
	{
		public Item Item { get; set; }
		public ItemDetailViewModel(Item item = null)
		{
			Title = item?.Text;
			Item = item;
		}
	}
}
