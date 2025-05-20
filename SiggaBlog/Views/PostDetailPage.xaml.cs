using SiggaBlog.ViewModels;

namespace SiggaBlog.Views
{
    public partial class PostDetailPage : ContentPage
    {
        public PostDetailPage(PostDetailViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
} 