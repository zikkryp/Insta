using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Authentication.Web;
using Windows.UI.Popups;
using Instagram.DataStructure;

namespace Instagram.Net
{
    public class Authorization
    {
        private static Authorization _authorization;

        public static async Task Authorize()
        {
            _authorization = new Authorization();
            await _authorization.AuthorizeAsync();
        }

        private async Task AuthorizeAsync()
        {
            try
            {
                WebAuthenticationResult result = await WebAuthenticationBroker.AuthenticateAsync(
                    WebAuthenticationOptions.None,
                    new Uri("https://api.instagram.com/oauth/authorize/?client_id=039d35ec6aef455592c2e073e70b2e70&scope=likes&redirect_uri=http://krypapp.com/&response_type=token"),
                    new Uri("http://krypapp.com/"));


                if (result.ResponseStatus == WebAuthenticationStatus.Success)
                {
                    string tokenUri = result.ResponseData;

                    string token = tokenUri.Split('=')[1].Split('&')[0];
                    int userId = int.Parse(token.Split('.')[0]);

                    var authData = new AuthData(userId, token, true);

                    await Storage.DataStorage.Create(authData);
                }
                else if (result.ResponseStatus == WebAuthenticationStatus.UserCancel)
                {
                    await AuthorizeAsync();
                }
                else if (result.ResponseStatus == WebAuthenticationStatus.ErrorHttp)
                {
                    var message = new Windows.UI.Popups.MessageDialog("Exit program?", "Http Problem");
                    message.Commands.Add(new UICommand("Yes", new UICommandInvokedHandler(this.Close)));
                    await message.ShowAsync();
                }
                else
                {
                    bool exit = false;
                    var message = new Windows.UI.Popups.MessageDialog("Exit program?", "Authorization Failed");
                    message.Commands.Add(new UICommand("Yes", new UICommandInvokedHandler(this.Close)));
                    message.Commands.Add(new UICommand("No"));
                    await message.ShowAsync();

                    if (exit == false)
                    {
                        await AuthorizeAsync();
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        private async void Retry(IUICommand command)
        {
            await AuthorizeAsync();
        }

        private void Close(IUICommand command)
        {
            Windows.UI.Xaml.Application.Current.Exit();
        }
    }
}
