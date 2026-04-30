
using AppRpgEtec.Models;
using AppRpgEtec.Services.Personagens;
using AppRpgEtec.Views.Armas;
using AppRpgEtec.Views.Personagens;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace AppRpgEtec.ViewModels.Personagens
{
    public class ListagemPersonagemViewModel : BaseViewModel
    {
        private PersonagemService pService;
        public ICommand DirecionarArmasCommand { get; set; }
        public ObservableCollection<Personagem> Personagens { get; set; }
        public ICommand NovoPersonagemCommand { get; }
        public ICommand RemoverPersonagemCommand { get; set; } 


        private Personagem personagemSelecionado;

        public Personagem PersonagemSelecionado
        {
            get { return personagemSelecionado; }
            set
            {
                if (value != null)
                {
                    personagemSelecionado = value;

                    Shell.Current
                        .GoToAsync($"cadPersonagemView?Id={personagemSelecionado.Id}");
                }
            }
        }


        public ListagemPersonagemViewModel()
        {
            string token = Preferences.Get("UsuarioToken", string.Empty);
            pService = new PersonagemService(token);
            Personagens = new ObservableCollection<Personagem>();

            _ = ObterPersonagens();
            InicializarCommands();

            NovoPersonagemCommand = new Command(async () => { await ExibirCadastroPersonagem(); });

           
            RemoverPersonagemCommand =
                new Command<Personagem>(async (Personagem p) => { await RemoverPersonagem(p); });
        }

        public void InicializarCommands()
        {
            DirecionarArmasCommand = new Command(async () => await DirecionarParaArmas());
        }

        public async Task ObterPersonagens()
        {
            try
            {
                Personagens = await pService.GetPersonagensAsync();
                OnPropertyChanged(nameof(Personagens));
            }
            catch (Exception ex)
            {

                await Application.Current.MainPage
                    .DisplayAlert("Ops", ex.Message + " Detalhes: " + ex.InnerException, "ok");
            }
        }

        public async Task DirecionarParaArmas()
        {
            Application.Current.MainPage = new ListagemArmasView();
        }

        public async Task ExibirCadastroPersonagem()
        {
            try
            {
                await Shell.Current.GoToAsync("cadPersonagemView");
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Ops", ex.Message + "Detalhes: " + ex.InnerException, "Ok");
            }
        }


        public async Task RemoverPersonagem(Personagem p)
        {
            try
            {
                if (await Application.Current.MainPage
                    .DisplayAlert("Confirmação", $"Confirma a remoção de {p.Nome}?", "Sim", "Não"))
                {
                    await pService.DeletePersonagemAsync(p.Id);

                    await Application.Current.MainPage.DisplayAlert(
                        "Mensagem",
                        "Personagem removido com sucesso!",
                        "Ok");

                    _ = ObterPersonagens();
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage
                    .DisplayAlert("Ops", ex.Message + " Detalhes: " + ex.InnerException, "Ok");
            }
        }

    }
}