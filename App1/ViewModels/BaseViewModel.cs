using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace App1.ViewModels
{
    [Flags]
    public enum ViewModelStatus : byte
    {
        NotChanged = 0,
        Updated = 1,
        Inserted = 2,
        Deleted = 4
    }
    public class BaseViewModel : INotifyPropertyChanged
    {
        public ViewModelStatus ViewModelStatus { get; set; } = ViewModelStatus.NotChanged;
        public event PropertyChangedEventHandler PropertyChanged;
        public virtual void NotifyPropertyChanged(params string[] propertyNames)
        {
            if (PropertyChanged != null)
            {
                foreach (string propertyName in propertyNames)
                {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
                }
            }
        }
        public virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        protected bool SetProperty<T>(ref T originalValue, T newValue, [CallerMemberName] string propertyName = null)
        {
            if (Equals(originalValue, newValue))
            {
                return false;
            }
            originalValue = newValue;
            NotifyPropertyChanged(propertyName);
            return true;
        }
    }
}
