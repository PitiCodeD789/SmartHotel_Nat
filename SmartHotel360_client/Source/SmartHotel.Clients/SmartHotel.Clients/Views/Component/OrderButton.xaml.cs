using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartHotel.Clients.Core.Views.Component
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrderButton : ContentView
    {
        public OrderButton()
        {
            InitializeComponent();
        }

        #region CommandButton
        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create(
                propertyName: "Command",
                returnType: typeof(ICommand),
                declaringType: typeof(OrderButton));
        private void MButton_Clicked(object sender, EventArgs e)
        {
            Command?.Execute(null);
        }

        #endregion

        #region TextButton
        public string TextButton    
        {
            get { return (string)GetValue(TextButtonProperty); }
            set { SetValue(TextButtonProperty, value); }
        }

        public static readonly BindableProperty TextButtonProperty =
            BindableProperty.Create(
                propertyName: "TextEntry",
                returnType: typeof(string),
                declaringType: typeof(OrderButton),
                defaultBindingMode: BindingMode.TwoWay);
        #endregion

        #region BackgroundButton
        private Color backgroundButton;

        public Color BackgroundButton
        {
            get { return backgroundButton; }
            set
            {
                backgroundButton = value;
                mButton.BackgroundColor = backgroundButton;
            }
        }
        #endregion

        #region TextColor
        private Color textColor;

        public Color TextColor
        {
            get { return textColor; }
            set
            {
                textColor = value;
                mButton.TextColor = textColor;
            }
        }
        #endregion

    }
}