using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.ViewModel.Tasks;

namespace Client.View.Tasks;

public partial class CreateTaskPage : ContentPage
{
    public CreateTaskPage(CreateTaskViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}