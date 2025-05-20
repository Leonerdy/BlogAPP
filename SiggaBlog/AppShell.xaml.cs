using SiggaBlog.Views;

namespace SiggaBlog
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            Routing.RegisterRoute(
                nameof(PostDetailPage),
                typeof(PostDetailPage));

            InitializeComponent();
        }
    }
}
