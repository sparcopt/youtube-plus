using Google.Apis.YouTube.v3.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubePlus.Common;
using YoutubePlus.Domain;
using YoutubePlus.Utils;

namespace YoutubePlus.ViewModel
{
    public class SearchViewModel : BindableBase
    {
        private object _searchResults;

        public object SearchResults
        {
            get { return _searchResults; }
            set { _searchResults = value; RaisePropertyChanged(); }
        }

        public async Task LoadDataAsync(string query, ContentCategory category) 
        {
            switch (category)
            {
                case ContentCategory.Video:
                    SearchResults = await YouTubeHandler.SearchVideos("snippet", 20, query);
                    break;

                case ContentCategory.Channel:
                    SearchResults = await YouTubeHandler.SearchChannels("snippet", 50, query);
                    break;

                case ContentCategory.Playlist:
                    SearchResults = await YouTubeHandler.SearchPlaylists("snippet", 20, query);
                    break;
            }
        }
    }
}
