using BibliotecaXPTOLibs.DTOs;
using BibliotecaXPTOLibs.Repositories.Interfaces;
using DalProLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaXPTOLibs.Repositories
{
    public class MetricsRepository : IMetricsRepository
    {
        public List <TopBooksDTO> GetTopBooks()
        {
            string sql = "EXEC sp_DezObrasMaisrequisitadas @DataInicioIntervalo, @DataFimIntervalo";
            var parametros = new Dictionary<string, object>
            {
                { "@DataInicioIntervalo", DateTime.Now.AddDays(-30) },
                { "@DataFimIntervalo", DateTime.Now }
            };

            return DalPro.Query<TopBooksDTO>(sql, parametros);

        }
    }
}
