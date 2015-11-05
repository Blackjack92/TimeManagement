using System;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using TimeManager.Navigation.Properties;

namespace TimeManager.Navigation.Services
{
    public static class ResourceService
    {
        public static ImageSource TodosImageSource
        {
            get
            {
                return Imaging.CreateBitmapSourceFromHBitmap(
                          Resources.Todos.GetHbitmap(),
                          IntPtr.Zero,
                          Int32Rect.Empty,
                          BitmapSizeOptions.FromEmptyOptions());
            }
        }
    }
}
