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
using Google.Apis.YouTube.v3.Data;
using YoutubePlus.Common;
using YoutubePlus.Domain;

namespace YoutubePlus.View
{
    public partial class VideoView : PhoneApplicationPage
    {
        public VideoView()
        {
            InitializeComponent();

            this.DataContext = new VideoViewModel();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var video = (object)NavigationService.GetNavigationData();

            await (this.DataContext as VideoViewModel).LoadDataAsync(video);
        }

        private void Channel_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/View/ChannelView.xaml?", UriKind.Relative), ((VideoViewModel)this.DataContext).YoutubeChannel);
        }
    }
}