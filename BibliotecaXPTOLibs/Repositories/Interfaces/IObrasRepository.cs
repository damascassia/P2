using System;
using System.Collections.Generic;
using System.Text;
using BibliotecaXPTOLibs.DTOs;

namespace BibliotecaXPTOLibs.Repositories.Interfaces
{
    public interface IObrasRepository
    {
        List<ObraDisponivelDTO> PesquisarObrasDisponiveis(string nomeNucleo = null, string assunto = null);

        (bool Sucesso, string Mensagem, List<ObraDisponivelDTO> Dados) PesquisarObrasDisponiveis(string nomeNucleo, string assunto);
    }
}
