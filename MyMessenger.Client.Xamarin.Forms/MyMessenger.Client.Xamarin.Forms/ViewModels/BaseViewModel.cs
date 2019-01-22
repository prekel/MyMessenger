﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using Xamarin.Forms;

using MyMessenger.Client.Xamarin.Forms.Models;
using MyMessenger.Client.Xamarin.Forms.Services;

namespace MyMessenger.Client.Xamarin.Forms.ViewModels
{
	public class BaseViewModel : INotifyPropertyChanged
	{
		public IDataStore<Item> DataStore => DependencyService.Get<IDataStore<Item>>() ?? new MockDataStore();
		//public Services.Client Client => DependencyService.Get<Services.Client>() ?? new Services.Client();

		private bool _isBusy;
		public bool IsBusy
		{
			get => _isBusy;
			set => SetProperty(ref _isBusy, value);
		}

		private string _title = string.Empty;
		public string Title
		{
			get => _title;
			set => SetProperty(ref _title, value);
		}

		protected bool SetProperty<T>(ref T backingStore, T value,
			[CallerMemberName]string propertyName = "",
			Action onChanged = null)
		{
			if (EqualityComparer<T>.Default.Equals(backingStore, value))
				return false;

			backingStore = value;
			onChanged?.Invoke();
			OnPropertyChanged(propertyName);
			return true;
		}

		#region INotifyPropertyChanged
		public event PropertyChangedEventHandler PropertyChanged;
		protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
		{
			var changed = PropertyChanged;

			changed?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
		#endregion
	}
}
