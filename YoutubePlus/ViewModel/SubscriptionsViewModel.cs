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
    public class SubscriptionsViewModel : BindableBase
    {
        private List<Video> _subscriptionsVideos = new List<Video>();

        public List<Video> SubscriptionsVideos
        {
            get { return _subscriptionsVideos; }
            set { _subscriptionsVideos = value; RaisePropertyChanged(); }
        }

        public async Task LoadDataAsync()
        {
            var subscriptionList = await YouTubeHandler.GetActivities("snippet,contentDetails", 50, "upload");

            List<Video> subscriptionsTempList = new List<Video>();

            foreach (Activity activity in subscriptionList)
            {
                subscriptionsTempList.Add(await YouTubeHandler.GetVideo("snippet,statistics", activity.ContentDetails.Upload.VideoId));
            }

            SubscriptionsVideos = subscriptionsTempList;
        }
    }
}
