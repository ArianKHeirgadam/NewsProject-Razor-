using CodeHollow.FeedReader;
using NewsMVP.MOdels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsMVP.Utilities
{
    public class ExternalNewsService
    {
        private readonly List<string> _rssUrls = new List<string>
        {
            "https://www.mehrnews.com/rss",
            "https://www.isna.ir/rss"
        };

        public async Task<List<TblNews>> GetPersianNewsAsync()
        {
            var newsList = new List<TblNews>();

            foreach (var rssUrl in _rssUrls)
            {
                try
                {
                    var feed = await FeedReader.ReadAsync(rssUrl);

                    foreach (var item in feed.Items)
                    {
                        string imageUrl = ExtractImageFromContent(item.Content);
                        if (string.IsNullOrWhiteSpace(imageUrl))
                            continue;

                        var news = new TblNews
                        {
                            Title = item.Title,
                            Summry = item.Description,
                            Body = item.Content ?? item.Description ?? "",
                            ImageUrlno1 = imageUrl,
                            CategoryName = "بین الملل",
                            Date = DateOnly.FromDateTime(DateTime.Now),
                            ViewCount = 0,
                            Slider = false,
                            IsPublished = true
                        };

                        newsList.Add(news);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"⚠️ Error reading feed {rssUrl}: {ex.Message}");
                }
            }

            return newsList;
        }

        private string ExtractImageFromContent(string content)
        {
            if (string.IsNullOrWhiteSpace(content)) return null;

            int start = content.IndexOf("src=\"", StringComparison.OrdinalIgnoreCase);
            if (start == -1) return null;

            start += 5;
            int end = content.IndexOf("\"", start);
            if (end == -1) return null;

            return content.Substring(start, end - start);
        }
    }
}
