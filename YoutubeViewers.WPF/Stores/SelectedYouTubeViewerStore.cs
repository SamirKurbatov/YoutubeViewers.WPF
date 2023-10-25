using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeViewers.Domain.Models;

namespace YoutubeViewers.WPF.Stores
{
    public class SelectedYouTubeViewerStore
    {
		private YouTubeViewer selectedYouTubeViewer;
        private readonly YouTubeViewersStore youTubeViewersStore;

        public YouTubeViewer SelectedYouTubeViewer
		{
			get => selectedYouTubeViewer;
			set
			{
				selectedYouTubeViewer = value;
				SelectedYouTubeViewerChanged?.Invoke();
			}
		}

        public SelectedYouTubeViewerStore(YouTubeViewersStore youTubeViewersStore)
        {
            this.youTubeViewersStore = youTubeViewersStore;

            this.youTubeViewersStore.YouTubeViewerUpdated += OnYouTubeViewerUpdated;
            this.youTubeViewersStore.YouTubeViewerAdded += OnYouTubeViewerAdded;
        }

        private void OnYouTubeViewerAdded(YouTubeViewer youTubeViewer)
        {
            SelectedYouTubeViewer = youTubeViewer;
        }

        private void OnYouTubeViewerUpdated(YouTubeViewer youTubeViewer)
        {
            if (youTubeViewer.Id == SelectedYouTubeViewer?.Id)
            {
                SelectedYouTubeViewer = youTubeViewer;
            }
        }

        public event Action SelectedYouTubeViewerChanged;
	}
}
