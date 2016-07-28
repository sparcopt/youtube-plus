using Google.Apis.Services;
using Google.Apis.YouTube.v3;
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
    public class HomeViewModel : BindableBase
    {
        private List<Video> _recommendedVideos = new List<Video>();

        public List<Video> RecommendedVideos
        {
            get { return _recommendedVideos; }
            set { _recommendedVideos = value; RaisePropertyChanged(); }
        }

        private List<Video> _subscriptionsVideos = new List<Video>();

        public List<Video> SubscriptionVideos
        {
            get { return _subscriptionsVideos; }
            set { _subscriptionsVideos = value; RaisePropertyChanged(); }
        }

        private List<Channel> _subChannels;

        public List<Channel> SubChannels
        {
            get { return _subChannels; }
            set { _subChannels = value; RaisePropertyChanged(); }
        }
               
        public async Task LoadDataAsync()
        {
            // Populate lists if they are empty
            if (RecommendedVideos.Count.Equals(0) || SubscriptionVideos.Count.Equals(0))
            {
                // Login
                var credential = await OAuthManager.GetUserCredentialAsync();

                YouTubeHandler.YoutubeService = new YouTubeService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = "YoutubePlus"
                });

                var recommendedList = await YouTubeHandler.GetActivities("snippet,contentDetails", 50, "recommendation");
                var subscriptionList = await YouTubeHandler.GetActivities("snippet,contentDetails", 50, "upload");

                List<Video> recommendedTempList = new List<Video>();

                foreach(Activity activity in recommendedList)
                {
                    recommendedTempList.Add(await YouTubeHandler.GetVideo("snippet,statistics", activity.ContentDetails.Recommendation.ResourceId.VideoId));
                }

                List<Video> subscriptionsTempList = new List<Video>();

                foreach (Activity activity in subscriptionList)
                {
                    subscriptionsTempList.Add(await YouTubeHandler.GetVideo("snippet,statistics", activity.ContentDetails.Upload.VideoId));
                }

                // If list > 6, display the first 6 items
                if (recommendedTempList.Count > 6)
                    RecommendedVideos = recommendedTempList.GetRange(0, 6);
                else
                    RecommendedVideos = recommendedTempList.GetRange(0, recommendedTempList.Count);

                if (subscriptionsTempList.Count > 6)
                    SubscriptionVideos = subscriptionsTempList.GetRange(0, 6);
                else
                    SubscriptionVideos = subscriptionsTempList.GetRange(0, subscriptionsTempList.Count);

                SubChannels = await YouTubeHandler.GetSubChannels("snippet", 50);
            }
        }
    }
}
