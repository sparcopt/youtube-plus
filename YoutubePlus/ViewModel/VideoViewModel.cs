using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubePlus.Common;
using Google.Apis.YouTube.v3.Data;
using MyToolkit.Multimedia;
using YoutubePlus.Domain;
using YoutubePlus.Utils;

namespace YoutubePlus.ViewModel
{
    class VideoViewModel : BindableBase
    {
        private Video _youtubeVideo;

        public Video YoutubeVideo
        {
            get { return _youtubeVideo; }
            set { _youtubeVideo = value; RaisePropertyChanged(); }
        }

        private Channel _youtubeChannel;

        public Channel YoutubeChannel
        {
            get { return _youtubeChannel; }
            set { _youtubeChannel = value; RaisePropertyChanged(); }
        }
        
        private Uri _videoUri;

        public Uri VideoUri
        {
            get { return _videoUri; }
            set { _videoUri = value; RaisePropertyChanged(); }
        }

        public async Task LoadDataAsync(object obj)
        {
            if (YoutubeChannel == null)
            {
                Video video = (Video)obj;

                var youtubeUri = await MyToolkit.Multimedia.YouTube.GetVideoUriAsync(video.Id, YouTubeQuality.Quality720P);
                VideoUri = youtubeUri.Uri;

                YoutubeVideo = video;

                YoutubeChannel = await YouTubeHandler.GetChannel("snippet,brandingSettings,statistics", video.Snippet.ChannelId);
            }
        }
    }
}
