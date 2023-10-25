using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using YoutubeViewers.WPF.Stores;
using YoutubeViewers.WPF.ViewModels;

namespace YoutubeViewers.WPF.Commands
{
    public class LoadYouTubeViewersCommand : AsyncCommandBase
    {
        private readonly YouTubeViewersViewModel youTubeViewersViewModel;
        private YouTubeViewersStore youTubeViewersStore;

        public LoadYouTubeViewersCommand(YouTubeViewersViewModel youTubeViewersViewModel, YouTubeViewersStore youTubeViewersStore)
        {
            this.youTubeViewersViewModel = youTubeViewersViewModel;
            this.youTubeViewersStore = youTubeViewersStore;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            youTubeViewersViewModel.ErrorMessage = string.Empty;
            youTubeViewersViewModel.IsLoading = true;
            try
            {
                await youTubeViewersStore.Load();
            }
            catch (Exception)
            {
                youTubeViewersViewModel.ErrorMessage = "Failed to load YouTube viewers. Please restart the application!";
            }
            finally
            {
                youTubeViewersViewModel.IsLoading = false;
            }
        }
    }
}
