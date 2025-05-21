using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MediatR;
using SiggaBlog.Application.UseCases.CreatePost;
using SiggaBlog.Application.UseCases.Posts;
using SiggaBlog.Domain.Entities;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using SiggaBlog.Views;

namespace SiggaBlog.ViewModels
{
    public partial class MainPageViewModel : BaseViewModel
    {
        private readonly IGetAllPostsUseCase _getAllPostsUseCase;
        private readonly IMediator _mediator;

        [ObservableProperty]
        private ObservableCollection<Post> posts;

        [ObservableProperty]
        private bool isRefreshing;

        [ObservableProperty]
        private bool isCreatePostModalVisible;

        [ObservableProperty]
        private string newPostTitle;

        [ObservableProperty]
        private string newPostBody;

        public MainPageViewModel(
            IGetAllPostsUseCase getAllPostsUseCase,
            IMediator mediator)
        {
            _getAllPostsUseCase = getAllPostsUseCase;
            _mediator = mediator;
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
                
                var posts = await _getAllPostsUseCase.ExecuteAsync();
                
                
                Posts.Clear();
                if (posts != null)
                {
                    foreach (var post in posts)
                    {
                        Posts.Add(post);
                        
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erro carregando posts: {ex}");
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

        [RelayCommand]
        private async Task SelectPostAsync(Post post)
        {
            if (post is null) return;

            var navigationParameter = new Dictionary<string, object>
            {
                { "Post", post }
            };

            await Shell.Current.GoToAsync(nameof(PostDetailPage), navigationParameter);
        }

        [RelayCommand]
        private void ShowCreatePostModal()
        {
            IsCreatePostModalVisible = true;
            NewPostTitle = string.Empty;
            NewPostBody = string.Empty;
        }

        [RelayCommand]
        private void CancelCreatePost()
        {
            IsCreatePostModalVisible = false;
            NewPostTitle = string.Empty;
            NewPostBody = string.Empty;
        }

        [RelayCommand]
        private async Task CreatePostAsync()
        {
            if (string.IsNullOrWhiteSpace(NewPostTitle) || string.IsNullOrWhiteSpace(NewPostBody))
            {
                await Shell.Current.DisplayAlert("Atenção", "Preencha todos os campos", "OK");
                return;
            }

            try
            {
                IsBusy = true;
                var request = new CreatePostRequest(NewPostTitle, NewPostBody, 1); // UserId 1 como exemplo
                var response = await _mediator.Send(request);

                if (response != null)
                {
                    await Shell.Current.DisplayAlert("Sucesso", "Post criado com sucesso!", "OK");
                    IsCreatePostModalVisible = false;
                    await LoadPostsAsync(); // Recarrega a lista de posts
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Erro", "Não foi possível criar o post. Tente novamente.", "OK");
                System.Diagnostics.Debug.WriteLine($"Error creating post: {ex}");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
