using System;
using System.Collections.Generic;
using System.Text;
using BibliotecaXPTOLibs.DTOs;

namespace BibliotecaXPTOLibs.Repositories.Interfaces
{
    public interface IObrasRepository
    {
        // Pesquisa obras disponíveis para empréstimo.
        // Todos os parâmetros são opcionais — se não forem passados, a SP devolve tudo.
        //   nomeNucleo  → filtra por núcleo (ex: "Lisboa Centro")
        //   assunto     → filtra por assunto (ex: "Ficção Científica")
        List<ObraDisponivelDTO> PesquisarObrasDisponiveis(string nomeNucleo = null, string assunto = null);
    }
}
