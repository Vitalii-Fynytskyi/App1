﻿using App1.Enums;
using App1.Interfaces;
using App1.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace App1.ViewModels
{
    public partial class ItemDetailsViewModel :ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<string> locationTypes = new();
        [ObservableProperty]
        private ObservableCollection<string> mediums = new();
        [ObservableProperty]
        private ObservableCollection<string> itemTypes = new();
        private int _itemId;
        [ObservableProperty]
        private string itemName;
        [ObservableProperty]
        private string selectedMedium;
        [ObservableProperty]
        private string selectedItemType;
        [ObservableProperty]
        private string selectedLocation;
        [ObservableProperty]
        private bool isDirty;
        private int _selectedItemId = -1;
        protected INavigationService _navigationService;
        protected IDataService _dataService;

        public ItemDetailsViewModel(INavigationService navigationService, IDataService dataService)
        {
            _navigationService = navigationService;
            _dataService = dataService;

            PopulateLists();
        }

        public void InitializeItemDetailData(int itemId)
        {
            _selectedItemId = itemId;
            IsDirty = false;
        }

        private void PopulateLists()
        {
            ItemTypes.Clear();
            foreach (string iType in Enum.GetNames(typeof(ItemType)))
                ItemTypes.Add(iType);

            LocationTypes.Clear();
            foreach (string lType in Enum.GetNames(typeof(LocationType)))
                LocationTypes.Add(lType);

            Mediums = new ObservableCollection<string>();
        }

        private async Task SaveAsync()
        {
            MediaItem item;

            if (_itemId > 0)
            {
                item = await _dataService.GetItemAsync(_itemId);

                item.Name = ItemName;
                item.Location = (LocationType)Enum.Parse(typeof(LocationType), SelectedLocation);
                item.MediaType = (ItemType)Enum.Parse(typeof(ItemType), SelectedItemType);
                item.MediumInfo = _dataService.GetMedium(SelectedMedium);

                await _dataService.UpdateItemAsync(item);
            }
            else
            {
                item = new MediaItem
                {
                    Name = ItemName,
                    Location = (LocationType)Enum.Parse(typeof(LocationType), SelectedLocation),
                    MediaType = (ItemType)Enum.Parse(typeof(ItemType), SelectedItemType),
                    MediumInfo = _dataService.GetMedium(SelectedMedium)
                };

                await _dataService.AddItemAsync(item);
            }
        }

        public async Task SaveItemAndContinueAsync()
        {
            await SaveAsync();
            _itemId = 0;
            ItemName = string.Empty;
            SelectedMedium = null;
            SelectedLocation = null;
            SelectedItemType = null;
            IsDirty = false;
        }

        public async Task SaveItemAndReturnAsync()
        {
            await SaveAsync();
            _navigationService.GoBack();
        }

        partial void OnItemNameChanged(string value)
        {
            IsDirty = true;
        }

        partial void OnSelectedMediumChanged(string value)
        {
            IsDirty = true;
        }

        partial void OnSelectedItemTypeChanged(string value)
        {
            IsDirty = true;
            Mediums.Clear();

            if (!string.IsNullOrWhiteSpace(value))
            {
                foreach (string med in _dataService.GetMediums((ItemType)Enum.Parse(typeof(ItemType), SelectedItemType)).Select(m => m.Name))
                    Mediums.Add(med);
            }
        }

        partial void OnSelectedLocationChanged(string value)
        {
            IsDirty = true;
        }

        [RelayCommand]
        private void Cancel()
        {
            _navigationService.GoBack();
        }
    }
}
