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
    public class ChannelViewModel : BindableBase
    {
        private Channel _youtubeChannel;

        public Channel YoutubeChannel
        {
            get { return _youtubeChannel; }
            set { _youtubeChannel = value; RaisePropertyChanged(); }
        }

        private List<Playlist> _channelPlaylists = new List<Playlist>();

        public List<Playlist> ChannelPlaylists
        {
            get { return _channelPlaylists; }
            set { _channelPlaylists = value; RaisePropertyChanged(); }
        }

        private List<Video> _channelVideos = new List<Video>();

        public List<Video> ChannelVideos
        {
            get { return _channelVideos; }
            set { _channelVideos = value; RaisePropertyChanged(); }
        }
        
        public async Task LoadDataAsync(object channel)
        {
            if (YoutubeChannel == null)
            {
                YoutubeChannel = (Channel)channel;

                ChannelPlaylists = await YouTubeHandler.GetChannelPlaylists(YoutubeChannel.Id, "snippet,contentDetails", 50);

                ChannelVideos = await YouTubeHandler.GetChannelVideos(YoutubeChannel.Id, "snippet", 50);
            }
        }
    }
}
