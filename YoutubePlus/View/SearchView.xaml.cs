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
using YoutubePlus.Domain;
using YoutubePlus.Common;

namespace YoutubePlus.View
{
    public partial class SearchView : PhoneApplicationPage
    {
        public SearchView()
        {
            InitializeComponent();
            this.DataContext = new SearchViewModel();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            TextBoxSearch.Focus();     
        }

        private async void ButtonSearch_Click(object sender, EventArgs e)
        {
            // Set focus to list in order to remove focus from TextBoxSearch
            ListSelector.Focus();
            (ApplicationBar as ApplicationBar).Mode = ApplicationBarMode.Minimized;

            if(ListPickerCategory.SelectedIndex.Equals(0))
                await(this.DataContext as SearchViewModel).LoadDataAsync(TextBoxSearch.Text, ContentCategory.Video);
            else if(ListPickerCategory.SelectedIndex.Equals(1))
                await(this.DataContext as SearchViewModel).LoadDataAsync(TextBoxSearch.Text, ContentCategory.Channel); 
            else if(ListPickerCategory.SelectedIndex.Equals(2))
                await(this.DataContext as SearchViewModel).LoadDataAsync(TextBoxSearch.Text, ContentCategory.Playlist);  
        }

        private void TextBoxSearch_GotFocus(object sender, RoutedEventArgs e)
        {
            // Show search button
            (ApplicationBar as ApplicationBar).Mode = ApplicationBarMode.Default;
        }

        private void ListSelectorSearch_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (ListPickerCategory.SelectedIndex.Equals(0))
                this.NavigationService.Navigate(new Uri("/View/VideoView.xaml?", UriKind.Relative), (sender as LongListSelector).SelectedItem);
            else if (ListPickerCategory.SelectedIndex.Equals(1))
                this.NavigationService.Navigate(new Uri("/View/ChannelView.xaml?", UriKind.Relative), (sender as LongListSelector).SelectedItem);
            else if (ListPickerCategory.SelectedIndex.Equals(2))
                this.NavigationService.Navigate(new Uri("/View/PlaylistView.xaml?", UriKind.Relative), (sender as LongListSelector).SelectedItem); 
        }
    }
}