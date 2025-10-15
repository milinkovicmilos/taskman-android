using Client.ViewModel;

namespace Client.View.Projects;

[QueryProperty(nameof(Id), "Id")]
public partial class ProjectDetailsPage : ContentPage
{
    private int Id { get; set; }

    private readonly ProjectDetailsViewModel _viewModel;

    public ProjectDetailsPage(ProjectDetailsViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await _viewModel.LoadProjectDetailsCommand.ExecuteAsync(Id);
    }
}