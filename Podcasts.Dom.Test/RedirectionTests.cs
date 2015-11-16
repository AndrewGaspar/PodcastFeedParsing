using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.IO;

namespace Podcasts.Dom.Test
{
    [TestClass]
    public class RedirectionTests
    {
        private async Task<PodcastFeed> OpenTestFeed(string fileName)
        {
            using (var file = File.OpenRead($@"TestFeeds\{fileName}"))
            {
                return await PodcastFeed.LoadFeedAsync(file);
            }
        }

        [TestMethod]
        public void RedirectionResolved()
        {
        }
    }
}
