using System.Text.RegularExpressions;

namespace TechTree.PersantionLayer.Helpers
{
    public static class YoutubeHelper
    {
        public static string GetYoutubeEmbedSrc(string youtubeUrl)
        {

            // Regular expression to extract YouTube video ID
            Regex regex = new Regex(@"(?:youtube\.com\/(?:[^\/\n\s]+\/\S+\/|(?:v|e(?:mbed)?)\/|\S*?[?&]v=)|youtu\.be\/)([a-zA-Z0-9_-]{11})");

            Match match = regex.Match(youtubeUrl);
            if (match.Success)
            {
                string videoId = match.Groups[1].Value;
                return $"https://www.youtube.com/embed/{videoId}";
            }
            else
            {
                // Return an error message or handle invalid URLs as you wish
                return "Invalid YouTube URL";
            }

        }
    }
}
