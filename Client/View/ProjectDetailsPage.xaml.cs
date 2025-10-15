using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.ViewModel;

namespace Client.View;

public partial class ProjectDetailsPage : ContentPage
{
    public ProjectDetailsPage(ProjectDetailsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}