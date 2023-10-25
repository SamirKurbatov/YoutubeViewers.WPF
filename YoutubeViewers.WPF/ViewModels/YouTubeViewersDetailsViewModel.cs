using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeViewers.Domain.Models;
using YoutubeViewers.WPF.Stores;

namespace YoutubeViewers.WPF.ViewModels
{
    public class YouTubeViewersDetailsViewModel : ViewModelBase
    {
        private readonly SelectedYouTubeViewerStore selectedYouTubeViewerStore;

        public YouTubeViewer SelectedYouTubeViewer => selectedYouTubeViewerStore.SelectedYouTubeViewer;

        public YouTubeViewersDetailsViewModel(SelectedYouTubeViewerStore selectedYouTubeViewerStore)
        {
            this.selectedYouTubeViewerStore = selectedYouTubeViewerStore;

            selectedYouTubeViewerStore.SelectedYouTubeViewerChanged += OnSelectedYouTubeViewerStreChanged;
        }

        private void OnSelectedYouTubeViewerStreChanged()
        {
            OnPropertyChanged(nameof(HasSelectedYouTubeViewer));
            OnPropertyChanged(nameof(UserName));
            OnPropertyChanged(nameof(IsSubscribedDisplay));
            OnPropertyChanged(nameof(IsMemberDisplay));
        }

        public override void Dispose()
        {
            selectedYouTubeViewerStore.SelectedYouTubeViewerChanged -= OnSelectedYouTubeViewerStreChanged;

            base.Dispose();
        }

        public bool HasSelectedYouTubeViewer => SelectedYouTubeViewer != null;

        public string UserName => SelectedYouTubeViewer?.UserName ?? "Unknown";

        public string IsSubscribedDisplay => (SelectedYouTubeViewer?.IsSubsribed ?? false) ? "Yes" : "No";

        public string IsMemberDisplay => (SelectedYouTubeViewer?.IsMember ?? false) ? "Yes" : "No";
    }
}
