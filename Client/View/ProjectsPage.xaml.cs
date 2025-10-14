using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.ViewModel;

namespace Client.View;

public partial class ProjectsPage : ContentPage
{
    private readonly ProjectsViewModel _viewModel;

    public ProjectsPage(ProjectsViewModel viewModel)
    {
        InitializeComponent();

        _viewModel = viewModel;
        BindingContext = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await Task.Delay(100);
        _viewModel.LoadProjectsCommand.ExecuteAsync(null);
    }
}