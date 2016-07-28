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
    public partial class HomeView : PhoneApplicationPage
    {
        public HomeView()
        {
            InitializeComponent();

            this.DataContext = new HomeViewModel();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            progressBar.Visibility = Visibility.Visible;
            await (this.DataContext as HomeViewModel).LoadDataAsync();
            progressBar.Visibility = Visibility.Collapsed;
        }

        private void Recommended_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/View/VideoView.xaml?", UriKind.Relative), (sender as LongListSelector).SelectedItem);
        }

        private void Subscriptions_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/View/VideoView.xaml?", UriKind.Relative), (sender as LongListSelector).SelectedItem);
        }

        private void ButtonRecommendedMore_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/View/RecommendedView.xaml?", UriKind.Relative));
        }

        private void ButtonSubscriptionsMore_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/View/SubscriptionsView.xaml?", UriKind.Relative));
        }

        private void ButtonSearch_Click(object sender, EventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/View/SearchView.xaml?", UriKind.Relative));
        }

        private void SubChannel_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/View/ChannelView.xaml?", UriKind.Relative), (sender as LongListSelector).SelectedItem);
        }
    }
}