using AppRpgEtec.ViewModels.Armas;

namespace AppRpgEtec.Views.Armas;

public partial class ListagemArmasView : ContentPage
{
    ListagemArmaViewModel viewModel;
	public ListagemArmasView()
	{
		InitializeComponent();

        viewModel = new ListagemArmaViewModel();
        BindingContext = viewModel;
        Title = "Armas - App Rpg Etec";
    }
}