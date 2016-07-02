﻿namespace WINDOWS_MIMO_APP_2.Services.TileService
{
    using System;
    using System.Threading.Tasks;
    using NotificationsExtensions.Tiles;
    using Windows.UI.Notifications;
    using Windows.UI.Popups;
    using Models;
    using Windows.UI.StartScreen;
    public class TileService : ITileService
    {
        public async System.Threading.Tasks.Task CreateRecipeTile(Recipe recipe)
        {
            var tileContent = new TileContent()
            {
                Visual = new TileVisual()
                {
                    TileSmall = CreateSmallTile(recipe),
                    TileMedium = CreateMediumTile(recipe),
                    TileLarge = CreateLargeTile(recipe),
                    TileWide = CreateWideTile(recipe)
                }
            };
            string id = Guid.NewGuid().ToString();
            SecondaryTile tile2 = new SecondaryTile(id, "OtakuCook", "tileargs", new Uri("ms-appx:///Assets/Wide310x150Logo.scale-200.png"), TileSize.Wide310x150);
            //SecondaryTile tile = new SecondaryTile(id);
            //tile.VisualElements.Wide310x150Logo = new Uri("ms-appx:///Assets/Wide310x150Logo.scale-200.png");
            //SecondaryTile tile = new SecondaryTile(id,"My awesome tile!","tileargs",new Uri("ms-appx:///Assets/Wide310x150Logo.scale-200.png"),TileSize.Wide310x150);
            
            var result = await tile2.RequestCreateAsync();
            if (result)
            {
                var updater = Windows.UI.Notifications.TileUpdateManager.CreateTileUpdaterForApplication(id);
                updater.Update(new TileNotification(tileContent.GetXml()));
            }

        
        }

        private static TileBinding CreateWideTile(Recipe recipe)
        {
            return new TileBinding()
            {
                Content = new TileBindingContentAdaptive()
                {
                    BackgroundImage = new TileBackgroundImage()
                    {
                        Source = recipe.photo
                    }
                }
            };
        }

        private static TileBinding CreateLargeTile(Recipe recipe)
        {
            return new TileBinding()
            {
                Content = new TileBindingContentAdaptive()
                {
                    Children =
                            {
                                new TileImage()
                                {
                                    Source = new TileImageSource(recipe.photo),
                                    Align = TileImageAlign.Center,
                                    RemoveMargin = true
                                },
                                new TileText()
                                {
                                    Text = recipe.name,
                                    Align = TileTextAlign.Center,
                                    MaxLines = 1,
                                    Style = TileTextStyle.Header
                                }
                            }
                }
            };
        }

        private static TileBinding CreateMediumTile(Recipe recipe)
        {
            return new TileBinding()
            {
                Content = new TileBindingContentAdaptive()
                {
                    BackgroundImage = new TileBackgroundImage()
                    {
                        Source = recipe.photo
                    }
                }
            };
        }

        private static TileBinding CreateSmallTile(Recipe recipe)
        {
            return new TileBinding()
            {
                Content = new TileBindingContentAdaptive()
                {
                    BackgroundImage = new TileBackgroundImage()
                    {
                        Source = recipe.photo
                    }
                }
            };
        }
    }
}
