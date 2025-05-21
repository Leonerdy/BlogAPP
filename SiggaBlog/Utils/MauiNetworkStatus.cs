using SiggaBlog.Domain.Interfaces;
namespace SiggaBlog.Utils
{
    public class MauiNetworkStatus : INetworkStatus
    {
        public bool HasInternetConnection()
        {
            return Connectivity.Current.NetworkAccess == NetworkAccess.Internet;
        }
    }
}