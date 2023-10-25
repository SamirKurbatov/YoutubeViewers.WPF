using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using YoutubeViewers.WPF.Commands;
using YoutubeViewers.Domain.Models;
using YoutubeViewers.WPF.Stores;

namespace YoutubeViewers.WPF.ViewModels
{
    public class YouTubeViewersListingItemViewModel : ViewModelBase
    {
        public YouTubeViewer YouTubeViewer { get; private set; }

        public YouTubeViewersListingItemViewModel(YouTubeViewer youTubeViewer, YouTubeViewersStore youTubeViewersStore, ModalNavigationStore modalNavigationStore)
        {
            YouTubeViewer = youTubeViewer;

            EditCommand = new OpenEditYouTubeViewerCommand(this, youTubeViewersStore, modalNavigationStore);

            DeleteCommand = new DeleteYouTubeViewerCommand(this, youTubeViewersStore);
        }

        private bool isDeleting;
        public bool IsDeleting
        {
            get => isDeleting; 
            set
            {
                isDeleting = value;
                OnPropertyChanged(nameof(IsDeleting));
            }
        }

        private string errorMessage;

        public string ErrorMessage
        {
            get => errorMessage;
            set
            {
                errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
                OnPropertyChanged(nameof(HasErrorMessage));
            }
        }

        public bool HasErrorMessage => !string.IsNullOrEmpty(ErrorMessage);


        public string UserName => YouTubeViewer.UserName;

        public ICommand EditCommand { get; }

        public ICommand DeleteCommand { get; }

        public YouTubeViewersStore YouTubeViewersStore { get; }
        public ModalNavigationStore ModalNavigationStore { get; }

        internal void Update(YouTubeViewer youTubeViewer)
        {
            YouTubeViewer = youTubeViewer;
            OnPropertyChanged(nameof(UserName));
        }
    }
}
