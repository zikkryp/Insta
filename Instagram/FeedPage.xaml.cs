using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Popups;
using System.Threading.Tasks;
using Instagram.Common;
using Instagram.Net;
using Instagram.DataModel;
using Instagram.DataStructure;
using Instagram.Storage;

namespace Instagram
{
    public sealed partial class FeedPage : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();

        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        public FeedPage()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += navigationHelper_LoadState;
            this.navigationHelper.SaveState += navigationHelper_SaveState;

            this.NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;
        }

        private void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
            if (e.PageState != null)
            {
                feed = e.PageState["Feed"] as IEnumerable<FeedItem>;
                user = e.PageState["User"] as User;

                this.defaultViewModel["User"] = user;
                this.defaultViewModel["Feed"] = feed;
            }
            else
            {
                GetDataAsync();
            }
        }

        private void navigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
            e.PageState["Feed"] = feed;
            e.PageState["User"] = user;
        }

        #region Регистрация NavigationHelper

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        private IEnumerable<FeedItem> feed;
        private User user;

        private async void GetDataAsync()
        {
            progressRing.IsActive = true;

            var authData = await DataStorage.GetAuthData();

            user = await UserInfoSource.GetUser(authData.Id);

            DisplayUserData(user);

            feed = await FeedDataSource.GetFeedAsync();

            this.defaultViewModel["Feed"] = feed;

            progressRing.IsActive = false;
        }

        private void DisplayUserData(User user)
        {
            this.defaultViewModel["User"] = new { Username = user.Username, ProfilePicture = user.ProfilePicture };
        }

        private void gridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Frame.Navigate(typeof(ItemDetailPage), e.ClickedItem);
        }

        private void gridView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private async void LoadMore_Click(object sender, RoutedEventArgs e)
        {
            progressRing.IsActive = true;

            gridView.ItemsSource = await FeedDataSource.LoadMoreAsync();

            progressRing.IsActive = false;
        }

        private void Followers_Click(object sender, RoutedEventArgs e)
        {
            GetDataAsync();
        }

        private async void UsetItem_Tap(object sender, TappedRoutedEventArgs e)
        {
            PopupMenu menu = new PopupMenu();

            menu.Commands.Add(new UICommand("View", new UICommandInvokedHandler(this.OpenUsersPage)));
            menu.Commands.Add(new UICommand("Settings", new UICommandInvokedHandler(this.OpenSettings)));
            menu.Commands.Add(new UICommand("Sign out", new UICommandInvokedHandler(this.SignOut)));

            var pointTransform = ((GridViewItem)sender).TransformToVisual(Window.Current.Content);
            var screenCoords = pointTransform.TransformPoint(new Point(200, 125));

            await menu.ShowAsync(screenCoords);
        }

        private async void OpenUsersPage(IUICommand command)
        {
            await new MessageDialog(command.Label).ShowAsync();
        }

        private async void OpenSettings(IUICommand command)
        {
            await new MessageDialog(command.Label).ShowAsync();
        }

        private async void SignOut(IUICommand command)
        {
            await new MessageDialog(command.Label).ShowAsync();
        }
    }
}
