using Google.Apis.YouTube.v3.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace YoutubePlus.Common.Converters
{
    /// <summary>
    /// This converter is used return the custom datatemplate based on the binded object type
    /// </summary>
    public class CategoryConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            DataTemplate template;

            if(value == null)
                return template = (DataTemplate)App.Current.Resources["VideoTemplate"];

            List<Video> videoObj = new List<Video>();
            List<Channel> channelObj = new List<Channel>();
            List<Playlist> playlistObj = new List<Playlist>();

            Type objType = value.GetType();
            Type videoType = videoObj.GetType();
            Type channelType = channelObj.GetType();
            Type playlistType = playlistObj.GetType();

            // Do with "is List<Video> .... ?"
            // Find type to cast object and compare kind (#video, #channel, #playlist)
            if (objType.Equals(videoType))
            {
                videoObj = (List<Video>)value;
                if (videoObj[0].Kind.Equals("youtube#video"))
                    return template = (DataTemplate)App.Current.Resources["VideoTemplate"];
                else
                    return "";
            }
            else if (objType.Equals(channelType))
            {
                channelObj = (List<Channel>)value;
                if (channelObj[0].Kind.Equals("youtube#channel"))
                    return template = (DataTemplate)App.Current.Resources["ChannelTemplate"];
                else
                    return "";
            }
            else if (objType.Equals(playlistType))
            {
                playlistObj = (List<Playlist>)value;
                if (playlistObj[0].Kind.Equals("youtube#playlist"))
                    return template = (DataTemplate)App.Current.Resources["PlaylistTemplate"];
                else
                    return "";
            }
            else
                return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
