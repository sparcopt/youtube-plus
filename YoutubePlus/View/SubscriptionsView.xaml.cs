using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using YoutubePlus.ViewModel;
using YoutubePlus.Common;

namespace YoutubePlus.View
{
    public partial class SubscriptionsView : PhoneApplicationPage
    {
        public SubscriptionsView()
        {
            InitializeComponent();

            this.DataContext = new SubscriptionsViewModel();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            progressbar.IsVisible = true;
            await (this.DataContext as SubscriptionsViewModel).LoadDataAsync();
            progressbar.IsVisible = false;
        }

        private void Subscription_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/View/VideoView.xaml?", UriKind.Relative), (sender as LongListSelector).SelectedItem);
        }
    }
}