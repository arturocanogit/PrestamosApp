using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PrestamosApp.ViewModels
{
    public abstract class CustomBaseViewModel : BaseViewModel
    {
        public CustomBaseViewModel()
        {
            Title = "Prestamos App - v2.2.0";
        }
        public abstract Task LoadDataAsync();
    }
}
