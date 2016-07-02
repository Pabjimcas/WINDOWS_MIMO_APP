

namespace WINDOWS_MIMO_APP_2.Services.Database
{
    using SQLite.Net;
    using SQLite.Net.Platform.WinRT;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Windows.Graphics.Imaging;
    using Windows.Storage;
    using Windows.Storage.Streams;
    using WINDOWS_MIMO_APP_2.Models;
    public class StorageService : IStorageService
    {

        public async Task<StorageFile> createImage(string imageName)
        {
            StorageFolder localFolder = await ApplicationData.Current.LocalFolder.CreateFolderAsync("Images", CreationCollisionOption.OpenIfExists);

            StorageFile file = await localFolder.CreateFileAsync(imageName, CreationCollisionOption.GenerateUniqueName);
            return file;
        }


        public async Task<StorageFolder> GetLocalFolder(string folderName)
        {
            StorageFolder folder = Windows.Storage.ApplicationData.Current.LocalFolder;
            try
            {
                return await folder.GetFolderAsync(folderName);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<StorageFolder> CreateLocalFolder()
        {
            var picturesLibrary = await StorageLibrary.GetLibraryAsync(KnownLibraryId.Pictures);
            StorageFolder folder = picturesLibrary.SaveFolder ?? ApplicationData.Current.LocalFolder;
            return folder;
        }

        public async Task<bool> DeleteLocalFolder(string folderName)
        {
            StorageFolder folder = Windows.Storage.ApplicationData.Current.LocalFolder;
            StorageFolder deleteFolder = await GetLocalFolder(folderName);

            if (deleteFolder != null)
            {
                await deleteFolder.DeleteAsync();
                return true;
            }

            return false;
        }

        public async void Save(StorageFolder captureFolder,string photoName, InMemoryRandomAccessStream stream)
        {
            try
            {
                var fileoutput = await captureFolder.CreateFileAsync(photoName, CreationCollisionOption.GenerateUniqueName);

                await SavePhotoAsync(stream, fileoutput);

                Debug.WriteLine("Photo saved!");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception when taking a photo: " + ex.ToString());
            }
        }

        private async System.Threading.Tasks.Task SavePhotoAsync(IRandomAccessStream stream, StorageFile file)
        {
            using (var inputStream = stream)
            {
                var decoder = await BitmapDecoder.CreateAsync(inputStream);

                using (var outputStream = await file.OpenAsync(FileAccessMode.ReadWrite))
                {
                    var encoder = await BitmapEncoder.CreateForTranscodingAsync(outputStream, decoder);
                    await encoder.FlushAsync();
                }
            }
        }
    }
}
