using YoutubeViewers.WPF.Stores;

namespace YoutubeViewers.WPF.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly ModalNavigationStore modelNavigationStore;

        public ViewModelBase CurrentModalViewModel => modelNavigationStore.CurrentViewModel;

        public bool IsModalOpen => modelNavigationStore.IsOpen;

        public YouTubeViewersViewModel YouTubeViewersViewModel { get; }

        public MainViewModel(ModalNavigationStore modelNavigationStore, YouTubeViewersViewModel youTubeViewersViewModel)
        {
            this.modelNavigationStore = modelNavigationStore;
            YouTubeViewersViewModel = youTubeViewersViewModel;
            this.modelNavigationStore.CurrentViewModelChanged += OnModelCurrentViewModelChanged;
        }

        public override void Dispose()
        {
            modelNavigationStore.CurrentViewModelChanged -= OnModelCurrentViewModelChanged;

            base.Dispose();
        }

        private void OnModelCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentModalViewModel));
            OnPropertyChanged(nameof(IsModalOpen));
        }
    }
}
