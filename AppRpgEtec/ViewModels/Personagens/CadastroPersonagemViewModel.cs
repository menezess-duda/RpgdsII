using AppRpgEtec.Models;
using AppRpgEtec.Models.Enuns;
using AppRpgEtec.Services.Personagens;
using System;
using System.Windows.Input;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace AppRpgEtec.ViewModels.Personagens
{
    [QueryProperty("PersonagemSelecionadoId", "pId")]
    public class CadastroPersonagemViewModel : BaseViewModel
    {

        private PersonagemService pService;
        public ICommand SalvarCommand { get; }

        public CadastroPersonagemViewModel()
        {
            string token = Preferences.Get("UsuarioToken", string.Empty);
            pService = new PersonagemService(token);
            _ = ObterClasses();

            SalvarCommand = new Command(async () => { await SalvarPersonagem(); });
        }

        private int id { get; set; }
        public int Id
        {
            get => id;
            set
            {
                id = value;
                OnPropertyChanged();
            }
        }
        private string nome { get; set; }
        public string Nome
        {
            get => nome;
            set
            {
                nome = value; 
                OnPropertyChanged();
            }
        }
        private int pontosVida { get; set; }
        public int PontosVida
        {
            get => pontosVida;
            set
            {
                pontosVida = value;
                OnPropertyChanged();
            }
        }
        private int forca { get; set; }
        public int Forca
        {
            get => forca;
            set
            {
                forca = value;
                OnPropertyChanged();
            }
        }
        private int defesa { get; set; }
        public int Defesa
        {
            get => defesa;
            set
            {
                defesa = value;
                OnPropertyChanged();
            }
        }
        private int inteligencia { get; set; }
        public int Inteligencia
        {
            get => inteligencia;
            set
            {
                inteligencia = value;
                OnPropertyChanged();
            }
        }
        private int disputas { get; set; }
        public int Disputas
        {
            get => disputas;
            set
            {
                disputas = value;
                OnPropertyChanged();
            }
        }
        private int vitorias { get; set; }
        public int Vitorias
        {
            get => vitorias;
            set
            {
                vitorias = value;
                OnPropertyChanged();
            }
        }
        private int derrotas { get; set; }
        public int Derrotas
        {
            get => derrotas;
            set
            {
                derrotas = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<TipoClasse> listaTiposClasse;

        public ObservableCollection<TipoClasse> ListaTiposClasse
        {
            get { return listaTiposClasse; }
            set
            {
                if (value != null)
                {
                    listaTiposClasse = value;
                    OnPropertyChanged();
                }

            }
        }

        public async System.Threading.Tasks.Task ObterClasses()
        {
            try
            {
                ListaTiposClasse = new ObservableCollection<TipoClasse>();
                ListaTiposClasse.Add(new TipoClasse() { Id = 1, Descricao = "Cavaleiro" });
                ListaTiposClasse.Add(new TipoClasse() { Id = 2, Descricao = "Mago" });
                ListaTiposClasse.Add(new TipoClasse() { Id = 3, Descricao = "Clerigo" });
                OnPropertyChanged(nameof(ListaTiposClasse));
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Ops", ex.Message + " Detalhes: " + ex.InnerException, "Ok");
            }
        }

        private TipoClasse tipoClasseSelecionado;
        public TipoClasse TipoClasseSelecionado
        {
            get { return tipoClasseSelecionado; }
            set
            {
                if (value != null)
                {
                    tipoClasseSelecionado = value;
                    OnPropertyChanged();
                }
            }
        }

        public async System.Threading.Tasks.Task SalvarPersonagem()
        {
            try
            {
                Personagem model = new Personagem()
                {
                    Nome = this.Nome,
                    PontosVida = this.PontosVida,
                    Defesa = this.Defesa,
                    Derrotas = this.Derrotas,
                    Disputas = this.Disputas,
                    Forca = this.Forca,
                    Inteligencia = this.Inteligencia,
                    Vitorias = this.Vitorias,
                    Id = this.id,
                    Classe = (ClasseEnum)tipoClasseSelecionado.Id
                };
                if (model.Id == 0)
                    await pService.PostPersonagemAsync(model);
                else
                    await pService.PutPersonagemAsync(model);

                    await Application.Current.MainPage.DisplayAlert("Mensagem", "Dados salvos com sucesso!", "Ok");

                await Shell.Current.GoToAsync("..");
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Ops", ex.Message + " Detalhes: " + ex.InnerException.Message, "Ok");
            }
        }

        public async void CarregarPersonagem()
        {
            try
            {
                Personagem p = await pService.GetPersonagemAsync(int.Parse(personagemSelecionadoId));
                this.Nome = p.Nome;
                this.PontosVida = p.PontosVida;
                this.Defesa = p.Defesa;
                this.Derrotas = p.Derrotas;
                this.Disputas = p.Disputas;
                this.Forca = p.Forca;
                this.Inteligencia = p.Inteligencia;
                this.Vitorias = p.Vitorias;
                this.Id = p.Id;

                TipoClasseSelecionado = this.ListaTiposClasse
                    .FirstOrDefault(tClasse => tClasse.Id == (int)p.Classe); 
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Opa", ex.Message + "Detalhes:" + ex.InnerException, "Ok");
            }
        }

        private string personagemSelecionadoId;

        public string PersonagemSelecionadoId 
        {
            set
            {
                if (value != null)
                {
                    personagemSelecionadoId = Uri.UnescapeDataString(value);
                    CarregarPersonagem();
                }
            }
        }
    }
}