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
    public class EditYouTubeViewerCommand : AsyncCommandBase
    {
        private readonly EditYouTubeViewerViewModel editYouTubeViewerViewModel;
        private readonly YouTubeViewersStore youTubeViewersStore;
        private readonly ModalNavigationStore modalNavigationStore;

        public EditYouTubeViewerCommand(EditYouTubeViewerViewModel editYouTubeViewerViewModel, YouTubeViewersStore youTubeViewersStore, ModalNavigationStore modalNavigationStore)
        {
            this.editYouTubeViewerViewModel = editYouTubeViewerViewModel;
            this.youTubeViewersStore = youTubeViewersStore;
            this.modalNavigationStore = modalNavigationStore;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            var formViewModel = editYouTubeViewerViewModel.YouTubeViewerDetailsFormViewModel;

            formViewModel.ErrorMessage = "";
            formViewModel.IsSubmit = true;

            YouTubeViewer youTubeViewer = new YouTubeViewer(
                editYouTubeViewerViewModel.YouTubeViewerId,
                formViewModel.UserName,
                formViewModel.IsSubscribed,
                formViewModel.IsMember);

            try
            {
                await youTubeViewersStore.Update(youTubeViewer);

                modalNavigationStore.Close();
            }
            catch (Exception)
            {
                formViewModel.ErrorMessage = "Failed to update youtube viewer!";
            }
            finally
            {
                formViewModel.IsSubmit = false;
            }
        }
    }
}
