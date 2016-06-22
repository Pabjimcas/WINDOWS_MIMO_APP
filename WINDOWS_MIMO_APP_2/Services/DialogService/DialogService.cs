namespace WINDOWS_MIMO_APP_2.Services.DialogService
{
    using System;
    using System.Threading.Tasks;
    using Windows.UI.Popups;

    public class DialogService : IDialogService
    {
        public async Task ShowMessage(string message, string title)
        {
            MessageDialog dialog = new MessageDialog(message, title);
            await dialog.ShowAsync();
        }
    }
}
