using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Podcasts.Dom.Test
{
    /// <summary>
    /// Summary description for NetFeedTests
    /// </summary>
    [TestClass]
    public class NetFeedTests
    {
        [TestMethod]
        public async Task BeastcastGet()
        {
            var feed = await PodcastFeed.LoadFeedAsync(new Uri(@"http://www.giantbomb.com/podcast-xml/beastcast"));

            Assert.AreEqual("The Giant Beastcast", feed.Title);
        }
    }
}
