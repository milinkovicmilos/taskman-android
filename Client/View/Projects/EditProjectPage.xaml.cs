using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.ViewModel;

namespace Client.View.Projects;

public partial class EditProjectPage : ContentPage
{
    public EditProjectPage(EditProjectViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}