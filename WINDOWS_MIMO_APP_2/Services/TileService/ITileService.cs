namespace WINDOWS_MIMO_APP_2.Services.TileService
{
    using Models;
    using System.Threading.Tasks;

    public interface ITileService
    {
       System.Threading.Tasks.Task CreateRecipeTile (Recipe recipe);
    }
}
