

// La plantilla de elemento Página en blanco está documentada en http://go.microsoft.com/fwlink/?LinkId=234238

namespace WINDOWS_MIMO_APP_2.Views
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Runtime.InteropServices.WindowsRuntime;
    using Windows.Devices.Sensors;
    using Windows.Foundation;
    using Windows.Foundation.Collections;
    using Windows.Graphics.Imaging;
    using Windows.Media.Capture;
    using Windows.Media.MediaProperties;
    using Windows.Storage;
    using Windows.Storage.FileProperties;
    using Windows.Storage.Streams;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Controls.Primitives;
    using Windows.UI.Xaml.Data;
    using Windows.UI.Xaml.Input;
    using Windows.UI.Xaml.Media;
    using Windows.UI.Xaml.Media.Imaging;
    using Windows.UI.Xaml.Navigation;
    using Services.Database;
    using WINDOWS_MIMO_APP2.Views.Base;
    using System.Threading.Tasks;
    using ViewModels;
    using Services.NavigationService;/// <summary>
                                     /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
                                     /// </summary>
    public sealed partial class CaptureImagePage : ViewBase
    {
        private bool _isInitialized;
        private StorageFolder _captureFolder = null;

        public CaptureImagePage()
        {
            this.InitializeComponent();
            this.InitCamera();
            this.storageService = new Services.Database.StorageService();
            this.navigationService = new NavigationService();
        }

        Windows.Media.Capture.MediaCapture captureManager;
        private StorageService storageService;
        private NavigationService navigationService;

        private async void InitCamera()
        {
            try
            {
                captureManager = new MediaCapture();
                await captureManager.InitializeAsync();
                _isInitialized = true;
            }
            catch (UnauthorizedAccessException)
            {
                Debug.WriteLine("The app was denied access to the camera");
            }

            if (_isInitialized)
            {
                capturePreview.Source = captureManager;
                await captureManager.StartPreviewAsync();
                _captureFolder = await storageService.CreateLocalFolder();
            }
        }

        private static PhotoOrientation ConvertOrientationToPhotoOrientation(SimpleOrientation orientation)
        {
            switch (orientation)
            {
                case SimpleOrientation.Rotated90DegreesCounterclockwise:
                    return PhotoOrientation.Rotate90;
                case SimpleOrientation.Rotated180DegreesCounterclockwise:
                    return PhotoOrientation.Rotate180;
                case SimpleOrientation.Rotated270DegreesCounterclockwise:
                    return PhotoOrientation.Rotate270;
                case SimpleOrientation.NotRotated:
                default:
                    return PhotoOrientation.Normal;
            }
        }

        async private void CapturePhoto_Click(object sender, RoutedEventArgs e)
        {
            ImageEncodingProperties imgFormat = ImageEncodingProperties.CreateJpeg();

            var stream = new InMemoryRandomAccessStream();

            await captureManager.CapturePhotoToStreamAsync(imgFormat, stream);

            var name = RecipeTitle.Text;
            storageService.Save(_captureFolder,name+".jpg",stream);
            

            await captureManager.StopPreviewAsync();
            if (stream != null)
            {
                Frame.GoBack();
            }
            
        }

    




        }
}
