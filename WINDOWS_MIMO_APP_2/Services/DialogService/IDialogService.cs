namespace WINDOWS_MIMO_APP_2.Services.DialogService
{
    using System.Threading.Tasks;

    public interface IDialogService
    {
        Task ShowMessage(string message, string title);
    }
}
