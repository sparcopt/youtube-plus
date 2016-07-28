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
    public partial class ChannelView : PhoneApplicationPage
    {
        public ChannelView()
        {
            InitializeComponent();

            this.DataContext = new ChannelViewModel();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var channel = (object)NavigationService.GetNavigationData();

            await(this.DataContext as ChannelViewModel).LoadDataAsync(channel);
        }

        private void Playlist_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/View/PlaylistView.xaml?", UriKind.Relative), (sender as LongListSelector).SelectedItem); 
        }

        private void Video_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/View/VideoView.xaml?", UriKind.Relative), (sender as LongListSelector).SelectedItem);
        }
    }
}