using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using YoutubeViewers.Domain.Models;
using YoutubeViewers.WPF.Stores;
using YoutubeViewers.WPF.ViewModels;

namespace YoutubeViewers.WPF.Commands
{
    public class AddYouTubeViewerCommand : AsyncCommandBase
    {
        private readonly AddYouTubeViewerViewModel addYouTubeViewerViewModel;
        private readonly YouTubeViewersStore youTubeViewersStore;
        private readonly ModalNavigationStore modalNavigationStore;

        public AddYouTubeViewerCommand(AddYouTubeViewerViewModel addYouTubeViewerViewModel, YouTubeViewersStore youTubeViewersStore, ModalNavigationStore modalNavigationStore)
        {
            this.addYouTubeViewerViewModel = addYouTubeViewerViewModel;
            this.youTubeViewersStore = youTubeViewersStore;
            this.modalNavigationStore = modalNavigationStore;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            var formViewModel = addYouTubeViewerViewModel.YouTubeViewerDetailsFormViewModel;

            formViewModel.ErrorMessage = null;

            formViewModel.IsSubmit = true;

            YouTubeViewer youTubeViewer = new YouTubeViewer(
                Guid.NewGuid(),
                formViewModel.UserName, 
                formViewModel.IsSubscribed, 
                formViewModel.IsMember);

            try
            {
                await youTubeViewersStore.Add(youTubeViewer);

                modalNavigationStore.Close();
            }
            catch (Exception)
            {
                formViewModel.ErrorMessage = "Failed to add youtube viewer!";
            }
            finally
            {
                formViewModel.IsSubmit = false;
            }
        }
    }
}
