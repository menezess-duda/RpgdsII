using AppRpgEtec.Models;
using AppRpgEtec.Services.Armas;
using AppRpgEtec.Services.Personagens;
using AppRpgEtec.Views.Armas;
using AppRpgEtec.Views.Personagens;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace AppRpgEtec.ViewModels.Armas
{
    public class ListagemArmaViewModel : BaseViewModel
    {
        private ArmasService aService;
        public ObservableCollection<Arma> Armas { get; set; }

        public ListagemArmaViewModel()
        {
            aService = new ArmasService();
            Armas = new ObservableCollection<Arma>();

            _ = ObterArmas();
        }

        public async Task ObterArmas()
        {
            try //Junto com o Catch evitará que erros fechem o aplicativo
            {
                Armas = await aService.GetArmasAsync();
                OnPropertyChanged(nameof(Armas)); //Informará a View que houve carregamento
            }
            catch (Exception ex)
            {
                //Captará o erro para exibir em tela
                await Application.Current.MainPage
                    .DisplayAlert("Ops", ex.Message + " Detalhes: " + ex.InnerException, "ok");
            }
        }
    }
}
