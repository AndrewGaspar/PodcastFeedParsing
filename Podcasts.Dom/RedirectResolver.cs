using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Podcasts.Dom
{
    public class RedirectionResolution
    {
        public Uri ResolvedLocation { get; private set; }
        public PodcastFeed Feed { get; private set; }

        internal RedirectionResolution(Uri location, PodcastFeed feed)
        {
            ResolvedLocation = location;
            Feed = feed;
        }
    }

    public class RedirectResolutionMaxDepthExceededException : Exception
    {
        public IList<Uri> Redirections { get; private set; }
        public int MaximumDepth { get; private set; }

        public RedirectResolutionMaxDepthExceededException(int maximumDepth, IList<Uri> redirections)
            : base($"Reached maximum depth of {maximumDepth} while attepting to resolve a podcast feed.")
        {
            MaximumDepth = maximumDepth;
            Redirections = redirections;
        }
    }

    public class RedirectResolver
    {
        public int MaximumDepth { get; private set; } = 5;

        public RedirectResolver() { /* defaults */ }

        public RedirectResolver(int maximumDepth)
        {
            MaximumDepth = maximumDepth;
        }

        private async Task<RedirectionResolution> EvaluatePodcastFeedAsync(
            Uri podcastUri, IUriResolution uriResolver)
        {
            var previouslyTriedUris = new List<Uri>();

            for(var i = 0; i < MaximumDepth; i++)
            {
                PodcastFeed feed;
                using (var stream = await uriResolver.GetStreamAsync(podcastUri))
                {
                    feed = await PodcastFeed.LoadFeedAsync(podcastUri).ConfigureAwait(false);
                }

                if (feed.ITunes.NewFeedUrl == null)
                {
                    return new RedirectionResolution(
                        location: podcastUri,
                        feed: feed);
                }
                
                previouslyTriedUris.Add(podcastUri);

                podcastUri = feed.ITunes.NewFeedUrl;
            }

            throw new RedirectResolutionMaxDepthExceededException(
                maximumDepth: MaximumDepth, 
                redirections: previouslyTriedUris);
        }

        public Task<RedirectionResolution> ResolveUri(Uri location, IUriResolution uriResolver)
        {
            return EvaluatePodcastFeedAsync(location, uriResolver);
        }

        public Task<RedirectionResolution> ResolveUri(Uri location)
        {
            return ResolveUri(location, PodcastDomConfiguration.Current.UriResolution);
        }
    }
}
