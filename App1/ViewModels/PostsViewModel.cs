using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace App1.ViewModels
{
    public class PostsViewModel:BaseViewModel
    {
        public ObservableCollection<PostViewModel> Posts
        {
            get
            {
                return posts;
            }
            set
            {
                if (value != posts)
                {
                    posts = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public ObservableCollection<PostViewModel> posts = new ObservableCollection<PostViewModel>();

        public async Task LoadPostsAsync()
        {
            Models.Post[] posts = await HttpService.Post.GetAllPosts();
            Posts = new ObservableCollection<PostViewModel>(posts.Select(r => new PostViewModel(r)));
        }
    }
}
