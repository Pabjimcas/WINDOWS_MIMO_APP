

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
    using Models;
    using Windows.UI.Xaml.Input;
    using Windows.Storage.Pickers;
    using Windows.Storage;
    public class ImagesViewModel : ViewModelBase
    {
        private INavigationService   navService;
        private string title;
        private ObservableCollection<StorageFile> imageList;

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

        public ObservableCollection<StorageFile> ImageList {
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

                ImageList = new ObservableCollection<StorageFile>(files);
                // Application now has read/write access to the picked file(s)
                /*foreach (Windows.Storage.StorageFile file in files)
                {
                    imageList.Add(file.Name);
                }*/
            }
            else
            {
                this.navService.GoBack();
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
