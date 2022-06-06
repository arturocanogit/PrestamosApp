using PrestamosApp.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace PrestamosApp.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}