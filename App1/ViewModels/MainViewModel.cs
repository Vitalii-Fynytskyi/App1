using App1.Enums;
using App1.Interfaces;
using App1.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml.Input;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace App1.ViewModels
{
    public partial class MainViewModel: ObservableObject
    {
        [ObservableProperty]
        private string selectedMedium;
        [ObservableProperty]
        private ObservableCollection<MediaItem> items = new ObservableCollection<MediaItem>();
        private ObservableCollection<MediaItem> allItems;
        [ObservableProperty]
        private ObservableCollection<string> mediums;
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(DeleteCommand))]
        private MediaItem selectedMediaItem;
        private INavigationService _navigationService;
        private IDataService _dataService;
        private const string AllMediums = "All";

        public MainViewModel(INavigationService navigationService, IDataService dataService)
        {
            _navigationService = navigationService;
            _dataService = dataService;
            PopulateDataAsync();
        }

        public async Task PopulateDataAsync()
        {
            Items.Clear();

            foreach (var item in await _dataService.GetItemsAsync())
            {
                Items.Add(item);
            }

            allItems = new ObservableCollection<MediaItem>(Items);

            Mediums = new ObservableCollection<string>
            {
                AllMediums
            };

            foreach (var itemType in _dataService.GetItemTypes())
            {
                Mediums.Add(itemType.ToString());
            }

            SelectedMedium = Mediums[0];
        }

        partial void OnSelectedMediumChanged(string value)
        {
            Items.Clear();

            foreach (var item in allItems)
            {
                if (string.IsNullOrWhiteSpace(value) ||
                    value == "All" ||
                    value == item.MediaType.ToString())
                {
                    Items.Add(item);
                }
            }
        }

        [RelayCommand]
        public void AddEdit()
        {
            var selectedItemId = -1;

            if (SelectedMediaItem != null)
            {
                selectedItemId = SelectedMediaItem.Id;
            }

            _navigationService.NavigateTo("ItemDetailsPage", selectedItemId);
        }

        [RelayCommand(CanExecute = nameof(CanDeleteItem))]
        public async Task DeleteAsync()
        {

            int index = -1;
            for (int i = 0; i < Items.Count; i++)
            {
                if (Items[i] == SelectedMediaItem)
                {
                    index = i;
                    break;
                }
            }
            await _dataService.DeleteItemAsync(SelectedMediaItem);
            allItems.Remove(SelectedMediaItem);
            Items.Remove(SelectedMediaItem);
            if (index - 1 >= 0)
            {
                SelectedMediaItem = Items[index - 1];
            }
            else if (Items.Count > 0)
            {
                SelectedMediaItem = Items[0];

            }
        }
        public void ListViewDoubleTapped(object sender, DoubleTappedRoutedEventArgs args)
        {
            AddEdit();
        }
        private bool CanDeleteItem() => SelectedMediaItem != null;
    }
}