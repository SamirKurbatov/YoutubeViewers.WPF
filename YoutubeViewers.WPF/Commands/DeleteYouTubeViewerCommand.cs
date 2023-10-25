using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeViewers.Domain.Models;
using YoutubeViewers.WPF.Stores;
using YoutubeViewers.WPF.ViewModels;

namespace YoutubeViewers.WPF.Commands
{
    public class DeleteYouTubeViewerCommand : AsyncCommandBase
    {
        private readonly YouTubeViewersListingItemViewModel youTubeViewerListingItemViewModel;
        private readonly YouTubeViewersStore youTubeViewersStore;

        public DeleteYouTubeViewerCommand(
            YouTubeViewersListingItemViewModel youTubeViewerListingItemViewModel,
            YouTubeViewersStore youTubeViewersStore)
        {
            this.youTubeViewerListingItemViewModel = youTubeViewerListingItemViewModel;
            this.youTubeViewersStore = youTubeViewersStore;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            YouTubeViewer youTubeViewer = youTubeViewerListingItemViewModel.YouTubeViewer;

            youTubeViewerListingItemViewModel.ErrorMessage = "";
            youTubeViewerListingItemViewModel.IsDeleting = true;

            try
            {
                await youTubeViewersStore.Delete(youTubeViewer.Id);
            }
            catch (Exception)
            {
                youTubeViewerListingItemViewModel.ErrorMessage = "Failed to delete youtube viewer. Please try again later ";
            }
            finally
            {
                youTubeViewerListingItemViewModel.IsDeleting = false;
            }
        }
    }
}
