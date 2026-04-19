using System;
using System.Collections.Generic;
using System.Text;
using BibliotecaXPTOLibs.DTOs;

namespace BibliotecaXPTOLibs.Repositories.Interfaces
{
    public interface IObrasRepository
    {
        List<ObraDisponivelDTO> PesquisarObrasDisponiveis(string nomeNucleo = null, string assunto = null);
    }
}
