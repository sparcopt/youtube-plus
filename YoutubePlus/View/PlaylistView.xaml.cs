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
    public partial class PlaylistView : PhoneApplicationPage
    {
        public PlaylistView()
        {
            InitializeComponent();

            this.DataContext = new PlaylistViewModel();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var video = (object)NavigationService.GetNavigationData();

            await (this.DataContext as PlaylistViewModel).LoadDataAsync(video);
        }

        private void Video_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/View/VideoView.xaml?", UriKind.Relative), (sender as LongListSelector).SelectedItem);
        }
    }
}