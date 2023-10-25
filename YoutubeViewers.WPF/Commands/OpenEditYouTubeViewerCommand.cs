using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using YoutubeViewers.Domain.Models;
using YoutubeViewers.WPF.Stores;
using YoutubeViewers.WPF.ViewModels;

namespace YoutubeViewers.WPF.Commands
{
    public class OpenEditYouTubeViewerCommand : CommandBase
    {
        private readonly YouTubeViewersListingItemViewModel youTubeViewerListingItemViewModel;
        private readonly YouTubeViewersStore youTubeViewersStore;
        private readonly ModalNavigationStore modalNavigationStore;

        public OpenEditYouTubeViewerCommand(
            YouTubeViewersListingItemViewModel youTubeViewerListingItemViewModel, 
            YouTubeViewersStore youTubeViewersStore, 
            ModalNavigationStore modalNavigationStore)
        {
            this.youTubeViewerListingItemViewModel = youTubeViewerListingItemViewModel;
            this.youTubeViewersStore = youTubeViewersStore;
            this.modalNavigationStore = modalNavigationStore;
        }

        public override void Execute(object? parameter)
        {
            YouTubeViewer youTubeViewer = youTubeViewerListingItemViewModel.YouTubeViewer;

            EditYouTubeViewerViewModel editYouTubeViewerViewModel = new EditYouTubeViewerViewModel(youTubeViewer, youTubeViewersStore,modalNavigationStore);
            modalNavigationStore.CurrentViewModel = editYouTubeViewerViewModel;
        }
    }
}
