using BibliotecaXPTOLibs.DTOs;
using BibliotecaXPTOLibs.Helpers.Interfaces;
using BibliotecaXPTOLibs.Repositories.Interfaces;
using DalProLib;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaXPTOLibs.Repositories
{
    public class MetricsRepository : IMetricsRepository
    {
        public readonly string _connectionString;
        public MetricsRepository(IConnectionHelper connectionHelper, IConfiguration configuration)
        {
            var tagGlobal = configuration["Settings:ActiveTag"] ?? "BibliotecaXPTO";
            _connectionString = connectionHelper.getConnectionString(tagGlobal);
        }
        public List <TopBooksDTO> GetTopBooks(DateTime dataInicio, DateTime dataFim)
        {
            DalPro.ConnectionString = _connectionString;
            string sql = "EXEC sp_DezObrasMaisrequisitadas @DataInicioIntervalo, @DataFimIntervalo";
            var parametros = new Dictionary<string, object>
            {
                { "@DataInicioIntervalo", dataInicio},
                { "@DataFimIntervalo", dataFim }
            };

            return DalPro.Query<TopBooksDTO>(sql, parametros);

        }
        public List<BottomBooksDTO> GetBottomBooks(DateTime dataInicio, DateTime dataFim)
        {
            DalPro.ConnectionString = _connectionString;
            string sql = "EXEC sp_DezObrasMenosrequisitadas @DataInicioIntervalo, @DataFimIntervalo";
            var parametros = new Dictionary<string, object>
            {
                { "@DataInicioIntervalo", dataInicio},
                { "@DataFimIntervalo", dataFim }
            };

            return DalPro.Query<BottomBooksDTO>(sql, parametros);

        }
        public List<Emprestimo_NucleoDTO> GetBottomEmprestimos(DateTime dataInicio, DateTime dataFim)
        {
            DalPro.ConnectionString = _connectionString;
            string sql = "EXEC sp_NucleoMenosEmprestimos @DataInicioIntervalo, @DataFimIntervalo";
            var parametros = new Dictionary<string, object>
            {
                { "@DataInicioIntervalo", dataInicio},
                { "@DataFimIntervalo", dataFim }
            };

            return DalPro.Query<Emprestimo_NucleoDTO>(sql, parametros);

        }
        public List<Emprestimo_NucleoDTO> GetTopEmprestimos(DateTime dataInicio, DateTime dataFim)
        {
            DalPro.ConnectionString = _connectionString;
            string sql = "EXEC sp_NucleoMaisEmprestimos @DataInicioIntervalo, @DataFimIntervalo";
            var parametros = new Dictionary<string, object>
            {
                { "@DataInicioIntervalo", dataInicio},
                { "@DataFimIntervalo", dataFim }
            };

            return DalPro.Query<Emprestimo_NucleoDTO>(sql, parametros);
        }
        public List<TopLeitoresDTO> GetTopLeitores(DateTime dataInicio, DateTime dataFim)
        {
            DalPro.ConnectionString = _connectionString;
            string sql = "EXEC sp_TopLeitores @DataInicioIntervalo, @DataFimIntervalo";
            var parametros = new Dictionary<string, object>
            {
                { "@DataInicioIntervalo", dataInicio},
                { "@DataFimIntervalo", dataFim }
            };

            return DalPro.Query<TopLeitoresDTO>(sql, parametros);
        }
    }
}
