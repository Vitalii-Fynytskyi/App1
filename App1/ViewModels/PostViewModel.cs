using App1.Models;

namespace App1.ViewModels
{
    public class PostViewModel :BaseViewModel
    {
        public Post Post
        {
            get
            {
                return post;
            }
            set
            {
                post = value;
                NotifyPropertyChanged("Post", "Id", "Title", "Body", "UserId");
            }
        }
        private Post post;
        public int Id
        {
            get
            {
                return post.id;
            }
            set
            {
                if(value != post.id)
                {
                    post.id = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string Title
        {
            get
            {
                return post.title;
            }
            set
            {
                if (value != post.title)
                {
                    post.title = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string Body
        {
            get
            {
                return post.body;
            }
            set
            {
                if (value != post.body)
                {
                    post.body = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public int UserId
        {
            get
            {
                return post.userId;
            }
            set
            {
                if (value != post.userId)
                {
                    post.userId = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public PostViewModel() { }
        public PostViewModel(Post postToSet)
        {
            Post = postToSet;
        }
    }
}
