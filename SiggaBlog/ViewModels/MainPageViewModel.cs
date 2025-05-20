using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SiggaBlog.Application.UseCases.Posts;
using SiggaBlog.Domain.Entities;
using System.Collections.ObjectModel;

namespace SiggaBlog.ViewModels
{
    public partial class MainPageViewModel : BaseViewModel
    {
        private readonly GetAllPostsUseCase _getAllPostsUseCase;

        [ObservableProperty]
        private ObservableCollection<Post> posts;

        [ObservableProperty]
        private bool isRefreshing;

        public MainPageViewModel(GetAllPostsUseCase getAllPostsUseCase)
        {
            _getAllPostsUseCase = getAllPostsUseCase;
            Posts = new ObservableCollection<Post>();
            Title = "Posts";
        }

        [RelayCommand]
        public async Task LoadPostsAsync()
        {
            if (IsBusy) return;

            try
            {
                IsBusy = true;
                System.Diagnostics.Debug.WriteLine("Loading posts...");
                var posts = await _getAllPostsUseCase.ExecuteAsync();
                System.Diagnostics.Debug.WriteLine($"Posts loaded: {posts?.Count() ?? 0}");
                
                Posts.Clear();
                if (posts != null)
                {
                    foreach (var post in posts)
                    {
                        Posts.Add(post);
                        System.Diagnostics.Debug.WriteLine($"Added post: {post.Title}");
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading posts: {ex}");
                await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
                IsRefreshing = false;
            }
        }

        [RelayCommand]
        private async Task RefreshAsync()
        {
            IsRefreshing = true;
            await LoadPostsAsync();
        }
    }
}
