using Avalonia.Media.Imaging;
using AvaloniaTester.Helpers;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Threading.Tasks;

namespace AvaloniaTester.ViewModels
{
    internal class ImagePageViewModel : ViewModelBase
    {        
        public string ImageSourceString => "/Assets/beans.jpg";

        public Bitmap ImageSourceBitmapLocal => ImageHelper.LoadFromResource("/Assets/blackcoffee.jpg");

        public Task<Bitmap?> ImageSourceBitmapWeb => ImageHelper.LoadFromWeb("https://unsplash.com/photos/a-brown-and-white-dog-standing-next-to-a-bush-SuRy-HZqNWI");
    }
}
