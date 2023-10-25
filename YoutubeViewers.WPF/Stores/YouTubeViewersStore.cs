using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using YoutubeViewers.Domain.Models;
using YouTubeViewers.Domain.Commands;
using YouTubeViewers.Domain.Queries;

namespace YoutubeViewers.WPF.Stores
{
    public class YouTubeViewersStore
    {
        private readonly IGetAllYouTubeViewersQuery getYouTubeViewerQuery;
        private readonly ICreateYouTubeViewerCommand createYouTubeViewerCommand;
        private readonly IUpdateYouTubeViewerCommand updateYouTubeViewerCommand;
        private readonly IDeleteYouTubeViewerCommand deleteYouTubeViewerCommand;
        private List<YouTubeViewer> youTubeViewers;

        public IEnumerable<YouTubeViewer> YouTubeViewers => youTubeViewers; 

        public YouTubeViewersStore(
            IGetAllYouTubeViewersQuery getYouTubeViewerCommand, 
            ICreateYouTubeViewerCommand createYouTubeViewerCommand, 
            IUpdateYouTubeViewerCommand updateYouTubeViewerCommand, 
            IDeleteYouTubeViewerCommand deleteYouTubeViewerCommand)
        {
            this.getYouTubeViewerQuery = getYouTubeViewerCommand;
            this.createYouTubeViewerCommand = createYouTubeViewerCommand;
            this.updateYouTubeViewerCommand = updateYouTubeViewerCommand;
            this.deleteYouTubeViewerCommand = deleteYouTubeViewerCommand;

            youTubeViewers = new List<YouTubeViewer>();
        }

        public event Action YouTubeViewersLoaded;
        public event Action<Guid> YouTubeViewerDeleted;
        public event Action<YouTubeViewer> YouTubeViewerAdded;
        public event Action<YouTubeViewer> YouTubeViewerUpdated;

        public async Task Load()
        {
            IEnumerable<YouTubeViewer> youtubeViewers = await getYouTubeViewerQuery?.Execute();

            this.youTubeViewers.Clear();
            this.youTubeViewers.AddRange(youtubeViewers);

            YouTubeViewersLoaded?.Invoke();
        }

        public async Task Delete(Guid id)
        {
            await deleteYouTubeViewerCommand.Execute(id);

            youTubeViewers.RemoveAll(y => y.Id == id);

            YouTubeViewerDeleted?.Invoke(id);
        }

        public async Task Add(YouTubeViewer youTubeViewer)
        {
            await createYouTubeViewerCommand.Execute(youTubeViewer);

            youTubeViewers.Add(youTubeViewer);

            YouTubeViewerAdded?.Invoke(youTubeViewer);
        }

        public async Task Update(YouTubeViewer youTubeViewer)
        {
            await updateYouTubeViewerCommand.Execute(youTubeViewer);

            var currentIndex = youTubeViewers.FindIndex(y => y.Id == youTubeViewer.Id);
            if (currentIndex != -1)
            {
                this.youTubeViewers[currentIndex] = youTubeViewer;
            }
            else
            {
                this.youTubeViewers.Add(youTubeViewer);
            }

            YouTubeViewerUpdated?.Invoke(youTubeViewer);
        }
    }
}
