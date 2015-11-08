using Podcasts.Dom;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PodcastInfo
{
    class Program
    {
        static void PrintUsage()
        {
            Console.WriteLine("PodcastInfo.exe podcast-url");
        }

        static async Task<PodcastFeed> GetFeedAsync(Uri location)
        {
            if(location.Scheme == "file")
            {
                using (var stream = File.OpenRead(location.AbsolutePath))
                {
                    return await PodcastFeed.LoadFeedAsync(stream);
                }
            }
            else if(location.Scheme == "http" || location.Scheme == "https")
            {
                return await PodcastFeed.LoadFeedAsync(location).ConfigureAwait(false);
            }

            throw new ArgumentOutOfRangeException($"{nameof(location)} must be either a web address or a file path");
        }

        static async Task MainAsync(string[] args)
        {
            if (args.Length != 1)
            {
                PrintUsage();
                return;
            }

            Uri url;
            if (!Uri.TryCreate(args[0], UriKind.Absolute, out url))
            {
                PrintUsage();
                return;
            }

            var feed = await GetFeedAsync(url);

            Console.WriteLine($"Title: {feed.Title}");
            Console.WriteLine($"Description: {feed.Description}");
            Console.WriteLine($"Categories: { string.Join(", ", feed.ITunes.Categories.Select(node => node.Text))}");

            foreach(var item in feed.Items)
            {
                Console.WriteLine();
                Console.WriteLine("--------");
                Console.WriteLine($"Title: {item.Title}");
                Console.WriteLine($"Description: {item.Description}");
                Console.WriteLine($"Published: {item.PubDate}");
                Console.WriteLine($"Guid: {item.Guid}");
                Console.WriteLine("--------");
            }
        }

        static void Main(string[] args)
        {
            MainAsync(args).Wait();
        }
    }
}
