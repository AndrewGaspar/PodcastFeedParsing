using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.IO;
using System.Linq;

namespace Podcasts.Dom.Test
{
    [TestClass]
    public class RedirectionTests
    {
        private TestFeedResolution _uriResolution;
        private RedirectResolver _resolver;

        [TestInitialize]
        public void InitTest()
        {
            _uriResolution = new TestFeedResolution("RedirectionTests");
            _resolver = new RedirectResolver();
        }

        [TestMethod]
        public async Task RedirectionResolved()
        {
            var resolution = await _resolver.ResolveUri(
                new Uri("start_feed.xml", UriKind.Relative), _uriResolution);
            
            Assert.AreEqual(
                new Uri("end_feed.xml", UriKind.Relative), 
                resolution.ResolvedLocation);
            Assert.AreEqual("New Test Feed", resolution.Feed.Title);
        }

        [TestMethod]
        public async Task FailOnRecursion()
        {
            try
            {
                var resolution = await _resolver.ResolveUri(
                    new Uri("recursion.xml", UriKind.Relative), _uriResolution);
                Assert.Fail();
            }
            catch (RedirectResolutionMaxDepthExceededException ex)
            {
                foreach (var redirection in ex.Redirections)
                {
                    Assert.AreEqual(
                        new Uri("recursion.xml", UriKind.Relative),
                        redirection);
                }
            }
        }
    }
}
