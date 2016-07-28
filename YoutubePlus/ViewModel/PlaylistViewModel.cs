using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubePlus.Common;
using Google.Apis.YouTube.v3.Data;
using YoutubePlus.Utils;

namespace YoutubePlus.ViewModel
{
    public class PlaylistViewModel : BindableBase
    {
        private List<Video> _playlistVideos = new List<Video>();

        public List<Video> PlaylistVideos
        {
            get { return _playlistVideos; }
            set { _playlistVideos = value; RaisePropertyChanged(); }
        }
        
        public async Task LoadDataAsync(object videoList)
        {
            if (PlaylistVideos.Count.Equals(0))
            {
                Playlist playlist = (Playlist)videoList;
                IList<PlaylistItem> playlistItems = await YouTubeHandler.GetPlaylistItems(playlist);

                List<Video> tempList = new List<Video>();

                foreach (PlaylistItem item in playlistItems)
                {
                    tempList.Add(await YouTubeHandler.GetVideo("snippet,statistics", item.Snippet.ResourceId.VideoId));
                }

                PlaylistVideos = tempList;
            }
        }
    }
}
