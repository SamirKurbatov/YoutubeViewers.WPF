using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace YoutubeViewers.WPF.ViewModels
{
    public class YouTubeViewerDetailsFormViewModel : ViewModelBase
    {
        private string userName;

        public string UserName
        {
            get => userName;
            set
            {
                userName = value;
                OnPropertyChanged(nameof(UserName));
                OnPropertyChanged(nameof(CanSubmit));
            }
        }

        private bool isSubscribed;

        public bool IsSubscribed
        {
            get => isSubscribed;
            set
            {
                isSubscribed = value;
                OnPropertyChanged(nameof(IsSubscribed));

            }
        }

        private bool isMember;

        public bool IsMember
        {
            get => isMember;
            set
            {
                isMember = value;
                OnPropertyChanged(nameof(IsMember));
            }
        }

        private bool isSubmit;
        public bool IsSubmit
        {
            get => isSubmit;
            set
            {
                isSubmit = value;
                OnPropertyChanged(nameof(IsSubmit));
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


        public bool CanSubmit => !string.IsNullOrWhiteSpace(UserName);

        public ICommand SubmitCommand { get; }

        public ICommand CancelCommand { get; }

        public YouTubeViewerDetailsFormViewModel(ICommand submitCommand, ICommand cancelCommand = null)
        {
            SubmitCommand = submitCommand;
            CancelCommand = cancelCommand;
        }
    }
}
