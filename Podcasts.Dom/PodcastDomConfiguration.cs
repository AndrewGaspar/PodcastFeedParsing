using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Podcasts.Dom
{
    public class PodcastDomConfiguration : IDisposable
    {
        private static PodcastDomConfiguration _current;
        public static PodcastDomConfiguration Current
        {
            get
            {
                return _current ?? (_current = new PodcastDomConfiguration());
            }
            set
            {
                if(value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                if (_current != null)
                {
                    _current.Dispose();
                }

                _current = value;
            }
        }
        
        public IUriResolution UriResolution { get; set; } = new HttpUriResolution();

        public void Dispose()
        {
            if(UriResolution != null)
            {
                UriResolution.Dispose();
            }
        }
    }
}
