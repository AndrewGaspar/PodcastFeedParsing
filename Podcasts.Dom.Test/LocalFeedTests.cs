using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.IO;
using System.Linq;

namespace Podcasts.Dom.Test
{
    [TestClass]
    public class LocalFeedTests
    {
        private async Task<PodcastFeed> OpenTestFeed(string fileName)
        {
            using (var file = File.OpenRead($@"TestFeeds\{fileName}"))
            {
                return await PodcastFeed.LoadFeedAsync(file);
            }
        }

        [TestMethod]
        public async Task BeastcastFile()
        {
            var beastcast = await OpenTestFeed("beastcast.xml");

            Assert.AreEqual("The Giant Beastcast", beastcast.Title);
            Assert.AreEqual("Giant Bomb", beastcast.ITunes.Owner.Name);
            Assert.AreEqual("beastcast@giantbomb.com", beastcast.ITunes.Owner.Email);
            
            var episode11 = beastcast.Items.First(item => item.Guid.Value == "1600-1309");

            Assert.AreEqual("The Giant Beastcast: Episode 11", episode11.Title);
            Assert.AreEqual(
                new DateTime(year: 2015, month: 8, day: 7, hour: 11, minute: 0, second: 0, kind: DateTimeKind.Utc),
                episode11.PubDate);

            Assert.AreEqual(26, beastcast.Items.Count());
            Assert.AreEqual(26, beastcast.Items.Count());
        }
    }
}
