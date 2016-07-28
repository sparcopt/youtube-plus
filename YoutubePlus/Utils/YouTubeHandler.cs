using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubePlus.Domain;

namespace YoutubePlus.Utils
{
    /// <summary>
    /// General YouTubeHandler used to store the current YouTube Service and make calls with Google's YouTube v3 API
    /// </summary>
    public class YouTubeHandler
    {
        public static YouTubeService YoutubeService { get; set; }

        /// <summary>
        /// Returns a list of channel activity events that match the request criteria.
        /// </summary>
        /// <param name="part">Comma-separated list of activity resource properties that the API response will include.</param>
        /// <param name="maxResults">Maximum number of items that should be returned.</param>
        /// <param name="type">Name of the activity type that will be used to filter activities.</param>
        /// <returns>Task<list type="Activity"></list></returns>
        public static async Task<List<Activity>> GetActivities(string part, long maxResults, string type)
        {
            var activitiesRequest = YoutubeService.Activities.List(part);
            activitiesRequest.MaxResults = maxResults;
            activitiesRequest.Home = true;

            var activitiesResponse = await activitiesRequest.ExecuteAsync();

            List<Activity> activityList = new List<Activity>();

            foreach (var activityResult in activitiesResponse.Items)
            {
                if (activityResult.Snippet.Type.Equals(type) && type.Equals("recommendation"))
                {
                    activityList.Add(activityResult);
                }
                else if (activityResult.Snippet.Type.Equals(type) && type.Equals("upload"))
                {
                    activityList.Add(activityResult);
                }
            }
            return activityList;
        }

        /// <summary>
        /// Returns a single video that matches the request criteria.
        /// </summary>
        /// <param name="part">Comma-separated list of activity resource properties that the API response will include.</param>
        /// <param name="videoId">Id of the video that will be used to filter videos.</param>
        /// <returns>Task<Video></Video></returns>
        public static async Task<Video> GetVideo(string part, string videoId)
        {
            var searchVideoRequest = YoutubeService.Videos.List(part);
            searchVideoRequest.Id = videoId;
            var searchVideoResponse = await searchVideoRequest.ExecuteAsync();

            Video videoResult = searchVideoResponse.Items[0];

            return videoResult;
        }

        /// <summary>
        /// Returns a list of videos that match the request criteria.
        /// </summary>
        /// <param name="part">Comma-separated list of activity resource properties that the API response will include.</param>
        /// <param name="maxResults">Maximum number of items that should be returned.</param>
        /// <param name="query">Query term to search for.</param>
        /// <returns><Task<list type="Video"></list></returns>
        public static async Task<List<Video>> SearchVideos(string part, long maxResults, string query)
        {
            var searchListRequest = YouTubeHandler.YoutubeService.Search.List(part);
            searchListRequest.Q = query;
            searchListRequest.MaxResults = maxResults;

            var searchListResponse = await searchListRequest.ExecuteAsync();

            List<Video> videoList = new List<Video>();

            foreach (var searchResult in searchListResponse.Items)
            {
                if(searchResult.Id.Kind.Equals("youtube#video"))
                {
                    videoList.Add(await GetVideo("snippet,statistics", searchResult.Id.VideoId));
                }
            }
            return videoList;
        }

        /// <summary>
        /// Returns a list of channels that match the request criteria.
        /// </summary>
        /// <param name="part">Comma-separated list of activity resource properties that the API response will include.</param>
        /// <param name="maxResults">Maximum number of items that should be returned.</param>
        /// <param name="query">Query term to search for.</param>
        /// <returns><Task<list type="Channel"></list></returns>
        public static async Task<List<Channel>> SearchChannels(string part, long maxResults, string query)
        {
            var searchListRequest = YouTubeHandler.YoutubeService.Search.List(part);
            searchListRequest.Q = query;
            searchListRequest.MaxResults = maxResults;

            var searchListResponse = await searchListRequest.ExecuteAsync();

            List<Channel> channelList = new List<Channel>();

            foreach (var searchResult in searchListResponse.Items)
            {
                if (searchResult.Id.Kind.Equals("youtube#channel"))
                {
                    channelList.Add(await GetChannel("snippet,statistics,brandingSettings", searchResult.Id.ChannelId));
                }
            }
            return channelList;
        }

        /// <summary>
        /// Returns a list of subscribed channels to the current user.
        /// </summary>
        /// <param name="part">Comma-separated list of activity resource properties that the API response will include.</param>
        /// <param name="maxResults">Maximum number of items that should be returned.</param>
        /// <returns><Task><list type="Channel"></list></returns>
        public static async Task<List<Channel>> GetSubChannels(string part, long maxResults)
        {
            var subChannelsRequest = YouTubeHandler.YoutubeService.Subscriptions.List(part);
            subChannelsRequest.MaxResults = maxResults;
            subChannelsRequest.Mine = true;

            var subChannelsResponse = await subChannelsRequest.ExecuteAsync();

            List<Channel> subChannelsList = new List<Channel>();

            foreach (var searchResult in subChannelsResponse.Items)
            {
                subChannelsList.Add(await GetChannel("snippet,statistics,brandingSettings", searchResult.Snippet.ResourceId.ChannelId));
            }
            return subChannelsList;
        }

        /// <summary>
        /// Returns a list of playlists that match the request criteria.
        /// </summary>
        /// <param name="part">Comma-separated list of activity resource properties that the API response will include.</param>
        /// <param name="maxResults">Maximum number of items that should be returned.</param>
        /// <param name="query">Query term to search for.</param>
        /// <returns><Task<list type="Playlist"></list></returns>
        public static async Task<List<Playlist>> SearchPlaylists(string part, long maxResults, string query)
        {
            var searchListRequest = YouTubeHandler.YoutubeService.Search.List(part);
            searchListRequest.Q = query;
            searchListRequest.MaxResults = maxResults;
            searchListRequest.Type = "playlist";

            var searchListResponse = await searchListRequest.ExecuteAsync();

            List<Playlist> playlistList = new List<Playlist>();

            foreach (var searchResult in searchListResponse.Items)
            {
                if (searchResult.Id.Kind.Equals("youtube#playlist"))
                {
                    playlistList.Add(await GetPlaylist("snippet,contentDetails", searchResult.Id.PlaylistId));
                }
            }
            return playlistList;
        }

        /// <summary>
        /// Returns a list of playlists from a channel.
        /// </summary>
        /// <param name="channelId">Id of the channel that will be used to filter channels.</param>
        /// <param name="part">Comma-separated list of activity resource properties that the API response will include.</param>
        /// <param name="maxResults">Maximum number of items that should be returned.</param>
        /// <returns>Task<list type="Playlist"></list></returns>
        public static async Task<List<Playlist>> GetChannelPlaylists(string channelId, string part, long maxResults)
        {
            var playlistRequest = YoutubeService.Playlists.List(part);
            playlistRequest.ChannelId = channelId;
            playlistRequest.MaxResults = maxResults;

            var playlistResponse = await playlistRequest.ExecuteAsync();

            List<Playlist> playlistList = new List<Playlist>();

            foreach (Playlist playlist in playlistResponse.Items)
            {
                playlistList.Add(playlist);
            }

            return playlistList;
        }

        /// <summary>
        /// Returns a list of videos from a channel.
        /// </summary>
        /// <param name="channelId">Id of the channel that will be used to filter channels.</param>
        /// <param name="part">Comma-separated list of activity resource properties that the API response will include.</param>
        /// <param name="maxResults">Maximum number of items that should be returned.</param>
        /// <returns>Task<list type="Video"></list></returns>
        public static async Task<List<Video>> GetChannelVideos(string channelId, string part, long maxResults)
        {
            var searchListRequest = YouTubeHandler.YoutubeService.Search.List(part);
            searchListRequest.ChannelId = channelId;
            searchListRequest.MaxResults = maxResults;

            var searchListResponse = await searchListRequest.ExecuteAsync();

            List<Video> videoList = new List<Video>();

            foreach (var searchResult in searchListResponse.Items)
            {
                if (searchResult.Id.Kind.Equals("youtube#video"))
                {
                    videoList.Add(await GetVideo("snippet,statistics", searchResult.Id.VideoId));
                }
            }
            return videoList;
        }

        /// <summary>
        /// Returns a single playlist that matches the request criteria.
        /// </summary>
        /// <param name="part">Comma-separated list of activity resource properties that the API response will include.</param>
        /// <param name="playListId">Id of the playlist that will be used to filter playlists.</param>
        /// <returns>Task<Playlist></Playlist></returns>
        private static async Task<Playlist> GetPlaylist(string part, string playListId)
        {
            var playlistRequest = YoutubeService.Playlists.List(part);
            playlistRequest.Id = playListId;

            var playlistResponse = await playlistRequest.ExecuteAsync();

            return playlistResponse.Items[0];
        }

        /// <summary>
        /// Returns a single channel that matches the request criteria.
        /// </summary>
        /// <param name="part">Comma-separated list of activity resource properties that the API response will include.</param>
        /// <param name="channelId">Id of the channel that will be used to filter channels.</param>
        /// <returns>Task<Channel></Channel></returns>
        public static async Task<Channel> GetChannel(string part, string channelId)
        {
            var channelRequest = YoutubeService.Channels.List(part);
            channelRequest.Id = channelId;

            var channelResponse = await channelRequest.ExecuteAsync();

            return channelResponse.Items[0];
        }

        /// <summary>
        /// Returns a list of playlistitems from a playlist.
        /// </summary>
        /// <param name="playlist">Id of the playlist that will be used to filter playlists.</param>
        /// <returns>Task<list type="PlaylistItem"></list></returns>
        public static async Task<IList<PlaylistItem>> GetPlaylistItems(Playlist playlist)
        {
            var playlistItemsRequest = YoutubeService.PlaylistItems.List("snippet");
            playlistItemsRequest.PlaylistId = playlist.Id;
            playlistItemsRequest.MaxResults = 50;
            var playlistItemsResponse = await playlistItemsRequest.ExecuteAsync();

            return playlistItemsResponse.Items;
        }
    }
}
