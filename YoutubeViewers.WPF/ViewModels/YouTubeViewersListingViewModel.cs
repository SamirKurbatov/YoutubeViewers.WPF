using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using YoutubeViewers.WPF.Commands;
using YoutubeViewers.Domain.Models;
using YoutubeViewers.WPF.Stores;

namespace YoutubeViewers.WPF.ViewModels
{
    public class YouTubeViewersListingViewModel : ViewModelBase
    {
        private readonly ObservableCollection<YouTubeViewersListingItemViewModel> youTubeViewersListingItemViewModels;
        private readonly YouTubeViewersStore youTubeViewersStore;
        private readonly SelectedYouTubeViewerStore selectedYouTubeViewerStore;
        private readonly ModalNavigationStore modalNavigationStore;

        public IEnumerable<YouTubeViewersListingItemViewModel> YouTubeViewersListingItemViewModels
            => youTubeViewersListingItemViewModels;

        private YouTubeViewersListingItemViewModel selectedYouTubeViewersListingItemViewModel;

        public YouTubeViewersListingItemViewModel SelectedYouTubeViewersListingItemViewModel
        {
            get => selectedYouTubeViewersListingItemViewModel;
            set
            {
                selectedYouTubeViewersListingItemViewModel = value;
                OnPropertyChanged();
                selectedYouTubeViewerStore.SelectedYouTubeViewer = selectedYouTubeViewersListingItemViewModel?.YouTubeViewer;
            }
        }



        public YouTubeViewersListingViewModel(YouTubeViewersStore youTubeViewersStore,
            SelectedYouTubeViewerStore selectedYouTubeViewerStore,
            ModalNavigationStore modalNavigationStore)
        {
            this.youTubeViewersStore = youTubeViewersStore;
            this.selectedYouTubeViewerStore = selectedYouTubeViewerStore;
            this.modalNavigationStore = modalNavigationStore;
            youTubeViewersListingItemViewModels = new ObservableCollection<YouTubeViewersListingItemViewModel>();

            youTubeViewersStore.YouTubeViewerDeleted += OnYouTubeViewersDeleted;
            youTubeViewersStore.YouTubeViewersLoaded += OnYouTubeViewersLoaded;
            youTubeViewersStore.YouTubeViewerAdded += OnYouTubeViewersAdded;
            youTubeViewersStore.YouTubeViewerUpdated += OnYouTubeViewersUpdated;
        }

        private void OnYouTubeViewersDeleted(Guid id)
        {
            var itemViewModel = youTubeViewersListingItemViewModels.FirstOrDefault(y => y.YouTubeViewer?.Id == id);

            if (itemViewModel != null)
            {
                youTubeViewersListingItemViewModels.Remove(itemViewModel);
            }
        }

        private void OnYouTubeViewersLoaded()
        {
            youTubeViewersListingItemViewModels.Clear();

            foreach (YouTubeViewer viewer in youTubeViewersStore.YouTubeViewers)
            {
                AddYouTubeViewer(viewer);
            }
        }

        private void OnYouTubeViewersUpdated(YouTubeViewer youTubeViewer)
        {
            var youTubeViewerViewModel = youTubeViewersListingItemViewModels.FirstOrDefault(y => y.YouTubeViewer.Id == youTubeViewer.Id);

            if (youTubeViewerViewModel is not null)
            {
                youTubeViewerViewModel.Update(youTubeViewer);
            }
        }

        private void OnYouTubeViewersAdded(YouTubeViewer youTubeViewer)
        {
            AddYouTubeViewer(youTubeViewer);
        }

        private void AddYouTubeViewer(YouTubeViewer youTubeViewer)
        {
            YouTubeViewersListingItemViewModel itemViewModel =
                new YouTubeViewersListingItemViewModel(youTubeViewer, this.youTubeViewersStore, modalNavigationStore);
            youTubeViewersListingItemViewModels.Add(itemViewModel);
        }

        public override void Dispose()
        {
            youTubeViewersStore.YouTubeViewerDeleted -= OnYouTubeViewersDeleted;
            youTubeViewersStore.YouTubeViewersLoaded -= OnYouTubeViewersLoaded;
            youTubeViewersStore.YouTubeViewerAdded -= OnYouTubeViewersAdded;
            youTubeViewersStore.YouTubeViewerUpdated -= OnYouTubeViewersUpdated;
            base.Dispose();
        }
    }
}