using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaXPTOLibs.Repositories
{
    public interface ILeitoresRepository
    {
        void CancelarInscricao(int leitorId, string tagRepo);
    }
}
