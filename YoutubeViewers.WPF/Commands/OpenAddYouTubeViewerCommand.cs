using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using YoutubeViewers.WPF.Stores;
using YoutubeViewers.WPF.ViewModels;

namespace YoutubeViewers.WPF.Commands
{
    class OpenAddYouTubeViewerCommand : CommandBase
    {
        private readonly YouTubeViewersStore youTubeViewersStore;
        private readonly ModalNavigationStore modalNavigationStore;

        public OpenAddYouTubeViewerCommand(YouTubeViewersStore youTubeViewersStore,ModalNavigationStore modalNavigationStore)
        {
            this.youTubeViewersStore = youTubeViewersStore;
            this.modalNavigationStore = modalNavigationStore;
        }

        public override void Execute(object? parameter)
        {
            var addYouTubeViewerViewModel = new AddYouTubeViewerViewModel(youTubeViewersStore, modalNavigationStore);
            modalNavigationStore.CurrentViewModel = addYouTubeViewerViewModel;
        }
    }
}
