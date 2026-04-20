using DalProLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaXPTOLibs.Repositories
{
    public class LeitoresRepository : ILeitoresRepository
    {
        public void CancelarInscricao(int leitorId)
        {
            DalPro.ExecuteSP("SP_CancelarInscricao", new Dictionary<string, object>
            {
                { "@IdLeitor", leitorId }
            });
        }
    }
}
