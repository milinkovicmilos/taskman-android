using Client.ViewModel;

namespace Client.View.Projects;

public partial class ProjectDetailsPage : ContentPage
{
    public ProjectDetailsPage(ProjectDetailsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}