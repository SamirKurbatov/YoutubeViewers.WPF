using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoutubeViewers.WPF.Commands
{
    public abstract class AsyncCommandBase : CommandBase
    {
        private bool isExecuting;
        public bool IsExecuting
        {
            get => isExecuting; 
            set
            {
                isExecuting = value;
                OnCanExecutedChanged();
            }
        }

        public override async void Execute(object? parameter)
        {
            IsExecuting = true;
            try
            {
                await ExecuteAsync(parameter);
            }
            catch (Exception)
            {

            }
            finally
            {
                IsExecuting = false;
            }
        }

        public override bool CanExecute(object? parameter)
        {
            return !IsExecuting && base.CanExecute(parameter);
        }

        public abstract Task ExecuteAsync(object parameter);
    }
}
