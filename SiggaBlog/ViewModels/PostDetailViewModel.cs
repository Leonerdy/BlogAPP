using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SiggaBlog.Application.UseCases.Comments;
using SiggaBlog.Domain.Entities;
using System.Collections.ObjectModel;

namespace SiggaBlog.ViewModels
{
    public partial class PostDetailViewModel : BaseViewModel, IQueryAttributable
    {
        private readonly IGetCommentsByPostIdUseCase _getCommentsUseCase;

        [ObservableProperty]
        private Post post;

        [ObservableProperty]
        private ObservableCollection<Comment> comments;

        [ObservableProperty]
        private bool isRefreshing;

        public PostDetailViewModel(IGetCommentsByPostIdUseCase getCommentsUseCase)
        {
            _getCommentsUseCase = getCommentsUseCase;
            Comments = new ObservableCollection<Comment>();
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.TryGetValue("Post", out var postObj) && postObj is Post selectedPost)
            {
                Post = selectedPost;
                Title = Post.Title;
                Task.Run(LoadCommentsAsync);
            }
        }

        private async Task LoadCommentsAsync()
        {
            if (IsBusy) return;

            try
            {
                IsBusy = true;
                var comments = await _getCommentsUseCase.ExecuteAsync(Post.Id);
                
                Comments.Clear();
                foreach (var comment in comments)
                {
                    Comments.Add(comment);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading comments: {ex}");
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
            await LoadCommentsAsync();
        }
    }
} 