using Rg.Plugins.Popup.Services;
using SmartHotel.Clients.Core.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SmartHotel.Clients.Core.ViewModels
{
    public class ConfirmationViewModel : ViewModelBase
    {
        public Action OkAction { get; set; }
        public Action NoAction { get; set; }

        public ConfirmationViewModel()
        {
            PopupText = "Confirmation";
            OkAction = Pop;
            NoAction = Pop;
            OkCommand = new Command(Ok);
            NoCommand = new Command(No);
        }
        public ConfirmationViewModel(string ConfirmText)
        {
            PopupText = ConfirmText;
            OkAction = Pop;
            NoAction = Pop;
            OkCommand = new Command(Ok);
            NoCommand = new Command(No);
        }

        public ConfirmationViewModel(string ConfirmText, Action OkAction)
        {
            PopupText = ConfirmText;
            this.OkAction = OkAction == null ? Pop : OkAction;
            NoAction = Pop;
            OkCommand = new Command(Ok);
            NoCommand = new Command(No);
        }

        public ConfirmationViewModel(string ConfirmText, Action OkAction,Action NoAction)
        {
            PopupText = ConfirmText;
            this.OkAction = OkAction == null ? Pop : OkAction;
            this.NoAction = NoAction == null ? Pop : NoAction;
            OkCommand = new Command(Ok);
            NoCommand = new Command(No);
        }

        public ICommand OkCommand { get; set; }
        public ICommand NoCommand { get; set; }
        public string PopupText { get; set; }

        public void Ok()
        {
            OkAction?.Invoke();
        }
        public void No()
        {
            NoAction?.Invoke();
        }

        public void Pop()
        {
            PopupNavigation.Instance.PopAsync();
        }
    }
}
