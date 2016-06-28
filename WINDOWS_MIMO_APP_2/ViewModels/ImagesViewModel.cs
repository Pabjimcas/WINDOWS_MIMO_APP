

namespace WINDOWS_MIMO_APP_2.ViewModels
{
    using Services.NavigationService;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using WINDOWS_MIMO_APP_2.ViewModels.Base;
    using Windows.UI.Xaml.Navigation;
    using System.Collections.ObjectModel;
    using Windows.UI.Xaml.Input;
    using Windows.Storage.Pickers;
    using Windows.Storage;
    using Windows.UI.Xaml.Media.Imaging;
    using Windows.Storage.Streams;
    using System.Threading.Tasks;
    public class ImagesViewModel : ViewModelBase
    {
        private INavigationService   navService;
        private string title;
        private ObservableCollection<BitmapImage> imageList;
        


        public string Title
        {
            get { return title; }
            set { title = value; }
        }


        public ImagesViewModel(INavigationService navService)
        {
            this.navService = navService;
            this.title = "Imágenes de recetas realizadas";
           
        }

        public ObservableCollection<BitmapImage> ImageList {
            get
            {
                return imageList;
            }
            set
            {
                this.imageList = value;
                
                RaisePropertyChanged();
            }
        }

        public async void OpenPicker()
        {
            FileOpenPicker picker = new FileOpenPicker();
            picker.ViewMode = PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;

            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".jpeg");
            picker.FileTypeFilter.Add(".png");
            var files = await picker.PickMultipleFilesAsync();

            if (files.Count > 0)
            {
                Title = files.Count+"";

                List<BitmapImage> list = new List<BitmapImage>();

                foreach (Windows.Storage.StorageFile file in files)
                {
                    list.Add(GetImage(file));
                }
                
                ImageList = new ObservableCollection<BitmapImage>(list);
                
                
            }
            else
            {
                this.navService.GoBack();
            }

        }
        public BitmapImage GetImage(StorageFile storageFile)
        {
            var bitmapImage = new BitmapImage();
            GetImageAsync(bitmapImage, storageFile);

            // Create an image or return a bitmap that's started loading
            return bitmapImage;
        }

        private async Task GetImageAsync(BitmapImage bitmapImage, StorageFile storageFile)
        {
            using (var stream = (FileRandomAccessStream)await storageFile.OpenAsync(FileAccessMode.Read))
            {
                bitmapImage.SetSource(stream);
            }
        }
        public override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            this.navService.AppFrame = base.AppFrame;
            OpenPicker();
        }
        public override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
        }
        public override void GoBackExecute()
        {
            base.GoBackExecute();
        }
    }
}
