using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Podcasts.Dom.Test
{
    public class TestFeedResolution : IUriResolution
    {
        private Uri _baseUri;

        public TestFeedResolution(string testFeedFolder)
        {
            var cd = new Uri($@"{Directory.GetCurrentDirectory()}\");

            _baseUri = new Uri(cd, $@"{testFeedFolder}\");
        }

        public void Dispose()
        {
            // nothing
        }

        public Task<Stream> GetStreamAsync(Uri uri)
        {
            if(uri.IsAbsoluteUri)
            {
                throw new ArgumentOutOfRangeException(nameof(uri), $"{uri} is not a relative URI");
            }

            var testFeedUri = new Uri(_baseUri, uri);

            return Task.FromResult<Stream>(File.OpenRead(testFeedUri.LocalPath));
        }
    }
}
