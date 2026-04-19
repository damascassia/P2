

using System;
using System.Collections.Generic;
using System.Text;
using BibliotecaXPTOLibs.DTOs;
using BibliotecaXPTOLibs.Helpers.Interfaces;
using BibliotecaXPTOLibs.Repositories.Interfaces;
using DalProLib;

namespace BibliotecaXPTOLibs.Repositories
{
    public class ObrasRepository : IObrasRepository
    {
        private readonly IConnectionHelper _connectionHelper;
        public ObrasRepository(IConnectionHelper connection)
        {
            _connectionHelper = connection;
            DalPro.ConnectionString = _connectionHelper.getConnectionString("DB_Biblioteca");
        }
        public List<ObraDisponivelDTO> PesquisarObrasDisponiveis(string nomeNucleo = null, string assunto = null)
        {
            var dt = DalPro.ExecuteSP("sp_ObrasDisponiveis", new Dictionary<string, object>
            {
                { "@NomeNucleo", nomeNucleo },  
                { "@Assunto",    assunto    }
            });

            var lista = new List<ObraDisponivelDTO>();
            foreach (System.Data.DataRow row in dt.Rows)
            {
                lista.Add(new ObraDisponivelDTO
                {
                    Titulo = row["Titulo"].ToString(),
                    Autor = row["Autor"].ToString()
                });
            }

            return lista;
        }
    }
}
       

