﻿using System;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using TimeManager.ManageTodos.Properties;

namespace TimeManager.ManageTodos.Services
{
    public static class ResourceService
    {
        public static ImageSource AddImageSource
        {
            get
            {
                return Imaging.CreateBitmapSourceFromHBitmap(
                          Resources.Add.GetHbitmap(),
                          IntPtr.Zero,
                          Int32Rect.Empty,
                          BitmapSizeOptions.FromEmptyOptions());
            }
        }

        public static ImageSource RemoveImageSource
        {
            get
            {
                return Imaging.CreateBitmapSourceFromHBitmap(
                          Resources.Delete.GetHbitmap(),
                          IntPtr.Zero,
                          Int32Rect.Empty,
                          BitmapSizeOptions.FromEmptyOptions());
            }
        }

        public static ImageSource SaveImageSource
        {
            get
            {
                return Imaging.CreateBitmapSourceFromHBitmap(
                          Resources.Save.GetHbitmap(),
                          IntPtr.Zero,
                          Int32Rect.Empty,
                          BitmapSizeOptions.FromEmptyOptions());
            }
        }

        public static ImageSource OpenImageSource
        {
            get
            {
                return Imaging.CreateBitmapSourceFromHBitmap(
                          Resources.Open.GetHbitmap(),
                          IntPtr.Zero,
                          Int32Rect.Empty,
                          BitmapSizeOptions.FromEmptyOptions());
            }
        }
    }
}
