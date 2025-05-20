using MAUI = Microsoft.Maui.Controls;

namespace SiggaBlog
{
    public partial class App : MAUI.Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}