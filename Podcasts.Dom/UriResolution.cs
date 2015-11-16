using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Podcasts.Dom
{
    public interface IUriResolution : IDisposable
    {
        Task<Stream> GetStreamAsync(Uri uri);
    }

    public class HttpUriResolution : IUriResolution
    {
        private HttpClient _client = new HttpClient();

        public void Dispose()
        {
            _client.Dispose();
        }

        public Task<Stream> GetStreamAsync(Uri location)
        {
            if (location.Scheme != "http" && location.Scheme != "https")
            {
                throw new ArgumentOutOfRangeException($"{nameof(location)} must be an http or https URI");
            }
            
            return _client.GetStreamAsync(location);
        }
    }
}
