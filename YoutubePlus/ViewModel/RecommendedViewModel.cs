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
    public class RecommendedViewModel : BindableBase
    {
        private List<Video> _recommendedVideos = new List<Video>();

        public List<Video> RecommendedVideos
        {
            get { return _recommendedVideos; }
            set { _recommendedVideos = value; RaisePropertyChanged(); }
        }

        public async Task LoadDataAsync()
        {
            var recommendedList = await YouTubeHandler.GetActivities("snippet,contentDetails", 50, "recommendation");

            List<Video> recommendedTempList = new List<Video>();

            foreach (Activity activity in recommendedList)
            {
                recommendedTempList.Add(await YouTubeHandler.GetVideo("snippet,statistics", activity.ContentDetails.Recommendation.ResourceId.VideoId));
            }

            RecommendedVideos = recommendedTempList;
        }
    }
}
