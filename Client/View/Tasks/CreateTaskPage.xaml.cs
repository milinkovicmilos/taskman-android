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