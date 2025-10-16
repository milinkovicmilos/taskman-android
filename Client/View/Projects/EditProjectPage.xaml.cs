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