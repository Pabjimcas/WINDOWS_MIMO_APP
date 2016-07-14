namespace WINDOWS_MIMO_APP_2.Services.TileService
{
    using System;
    using System.Threading.Tasks;
    using NotificationsExtensions.Tiles;
    using Windows.UI.Notifications;
    using Windows.UI.Popups;
    using Models;
    using Windows.UI.StartScreen;
    using NotificationsExtensions;
    using Windows.UI;
    using Windows.Data.Xml.Dom;
    public class TileService : ITileService
    {
        public async System.Threading.Tasks.Task CreateRecipeTile(Recipe recipe)
        {
            
            string id = Guid.NewGuid().ToString();
            SecondaryTile tile2 = new SecondaryTile(id,"OtakuCook"," ", new Uri("ms-appx:///Assets/Wide310x150Logo.scale-200.png"), TileSize.Wide310x150);

           
            tile2.VisualElements.Wide310x150Logo = new Uri("ms-appx:///Assets/Wide310x150Logo.scale-200.png"); ;
            tile2.VisualElements.ShowNameOnWide310x150Logo = true;

            tile2.VisualElements.ForegroundText = ForegroundText.Light;
            //TO DO: Not hardcode color here
            tile2.VisualElements.BackgroundColor = Color.FromArgb(50, 0, 100, 0);
          

            var result = await tile2.RequestCreateAsync();
            if (result)
            {
                UpdateSecondaryTile(id, recipe.name, recipe.photo);
            }

        
        }
        public void UpdateSecondaryTile(string id, string title, string imageURL)
        {
            if (SecondaryTile.Exists(id))
            {
                var tileXmlString = BuildTileXml(title, imageURL);

                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(tileXmlString);

                TileNotification tileNotification = new TileNotification(xmlDocument);
                TileUpdateManager.CreateTileUpdaterForSecondaryTile(id).Update(tileNotification);
            }
        }


        private string BuildTileXml(string title, string imageURL)
        {
            return string.Format(@"<tile>
                         <visual version='2'>
                             <binding template='TileSquarePeekImageAndText01' fallback='TileSquare150x150PeekImageAndText01'>
                                <image placement='peek' hint-overlay='50' src='{0}' id='1'/>
                                <text id='1'>{1}</text>
                                
                            </binding>
                            <binding template='TileWidePeekImage01' fallback='TileWide310x150PeekImage01'>
                                <image placement='peek' hint-overlay='50' src='{0}' id='1'/>
                                <text id='1'>{1}</text>
                       
                            </binding>
                        </visual>
                    </tile>", imageURL, title);
        }
       
    }
}
