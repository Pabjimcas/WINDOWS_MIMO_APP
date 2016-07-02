namespace WINDOWS_MIMO_APP_2.Services.Database
{
    using System.Threading.Tasks;
    using Windows.Storage;
    using Windows.Storage.Streams;
    public interface IStorageService
    {
        Task<StorageFile> createImage(string imageName);
        Task<StorageFolder> GetLocalFolder(string folderName);
        Task<StorageFolder> CreateLocalFolder();
        Task<bool> DeleteLocalFolder(string folderName);
        void Save(StorageFolder captureFolder, string photoName, InMemoryRandomAccessStream stream);
    }
}
