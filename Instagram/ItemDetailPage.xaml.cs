using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Instagram.Common;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Instagram.DataStructure;

namespace Instagram
{
    public sealed partial class ItemDetailPage : Page
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

        public ItemDetailPage()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += navigationHelper_LoadState;
        }

        private FeedItem feedItem;

        private void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
            feedItem = e.NavigationParameter as FeedItem;

            this.defaultViewModel["FeedItem"] = feedItem;

            if (feedItem.Location == null)
            {
                this.defaultViewModel["Location"] = new { Visibility = Windows.UI.Xaml.Visibility.Collapsed };
            }
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

        private async void Map_Tapped(object sender, TappedRoutedEventArgs e)
        {
            await new Windows.UI.Popups.MessageDialog(feedItem.Location.ToString()).ShowAsync();
        }

        private void Image_ImageOpened(object sender, RoutedEventArgs e)
        {
            progressRing.IsActive = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.navigationHelper.GoBack();
        }
    }
}
