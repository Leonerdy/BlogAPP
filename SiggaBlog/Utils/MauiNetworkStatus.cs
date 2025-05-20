using SiggaBlog.Domain.Interfaces;
using System.ComponentModel;

namespace SiggaBlog.Utils
{
    public class MauiNetworkStatus : INetworkStatus, INotifyPropertyChanged
    {
        private readonly IConnectivity _connectivity;
        private bool _isOnline;

        public event PropertyChangedEventHandler? PropertyChanged;

        public MauiNetworkStatus(IConnectivity connectivity)
        {
            _connectivity = connectivity;
            _isOnline = CheckConnectivity();
            
            // Registra o evento de mudança de conectividade
            _connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
        }

        public bool IsOnline
        {
            get => _isOnline;
            private set
            {
                if (_isOnline != value)
                {
                    _isOnline = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsOnline)));
                }
            }
        }

        private void Connectivity_ConnectivityChanged(object? sender, ConnectivityChangedEventArgs e)
        {
            IsOnline = CheckConnectivity();
        }

        private bool CheckConnectivity()
        {
            var currentAccess = _connectivity.NetworkAccess;
            var profiles = _connectivity.ConnectionProfiles;

            // Verifica se tem acesso à internet e se tem algum perfil de conexão ativo
            return currentAccess == NetworkAccess.Internet && 
                   profiles.Any(p => p == ConnectionProfile.WiFi || 
                                   p == ConnectionProfile.Cellular || 
                                   p == ConnectionProfile.Ethernet);
        }
    }
} 