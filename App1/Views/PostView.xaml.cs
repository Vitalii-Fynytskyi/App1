using App1.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace App1.Views
{
    public sealed partial class PostView : UserControl
    {
        public PostViewModel ViewModel
        {
            get { return (PostViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register(
                nameof(ViewModel),
                typeof(PostViewModel),
                typeof(PostView),
                new PropertyMetadata(null));
        public PostView()
        {
            this.InitializeComponent();
        }
        public PostView(PostViewModel viewModelToSet)
        {
            this.InitializeComponent();
            ViewModel = viewModelToSet;
        }
    }
}
