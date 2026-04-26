using BibliotecaXPTOLibs.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaXPTOLibs.Repositories.Interfaces
{
    public interface IMetricsRepository
    {
        public List<TopBooksDTO> GetTopBooks(DateTime dataInicio, DateTime dataFim);
        public List<BottomBooksDTO> GetBottomBooks(DateTime dataInicio, DateTime dataFim);
        public List<Emprestimo_NucleoDTO> GetBottomEmprestimos(DateTime dataInicio, DateTime dataFim);

        public List<Emprestimo_NucleoDTO> GetTopEmprestimos(DateTime dataInicio, DateTime dataFim);
        public List<TopLeitoresDTO> GetTopLeitores(DateTime dataInicio, DateTime dataFim);


    }
}
