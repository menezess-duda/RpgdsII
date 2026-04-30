using AppRpgEtec.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace AppRpgEtec.Services.Armas
{
    public class ArmasService : Request
    {
        private readonly Request _request;
        private const string apiUrlBase = "http://luizsilva12.somee.com/RpgApi/Armas";

        public ArmasService() 
        {
            _request = new Request();
        }

        public async Task<ObservableCollection<Arma>> GetArmasAsync()
        {
            string urlComplementar = string.Format("{0}","/GetAll");
            ObservableCollection<Models.Arma> listaArmas = await
            _request.GetAsync<ObservableCollection<Models.Arma>>(apiUrlBase + urlComplementar,"");
            return listaArmas;
        }
    }
}
