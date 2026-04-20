

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BibliotecaXPTOLibs.DTOs;
using BibliotecaXPTOLibs.Helpers.Interfaces;
using BibliotecaXPTOLibs.Repositories.Interfaces;
using DalProLib;
using Microsoft.Data.SqlClient;

namespace BibliotecaXPTOLibs.Repositories
{
    public class ObrasRepository : IObrasRepository
    {
        private readonly IConnectionHelper _connectionHelper;

        private readonly IAssuntoRepository _assuntoRepository;
        public ObrasRepository(IConnectionHelper connection, IAssuntoRepository assuntoRepository   )
        {
            _connectionHelper = connection;
            _assuntoRepository = assuntoRepository;
        }

        public ObraDTO? GetById(int id, string tagRepo)
        {
            string sql = "SELECT * FROM Obras WHERE Id = @Id"; 

            var param = new Dictionary<string, object>
            {
                {"Id", id}
            };

            string connection = _connectionHelper.getConnectionString(tagRepo);

            try
            {
                if (string.IsNullOrEmpty(connection))
                    throw new Exception($"Erro de Configuração: A tag '{tagRepo}' não possui uma ConnectionString válida.");

                DalPro.ConnectionString = connection;

                return DalPro.Query<ObraDTO>(sql, param).FirstOrDefault();
            }
            catch (SqlException ex)
            {
                throw new Exception($"Erro de banco de dados: {ex}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro inesperado: {ex}");
            }
        }

        public List<HistObrasDTO> GetHistorico(RequestHistObrasDTO dto, string tagRepo)
        {
            string sql = "EXEC sp_HistObrasRequisitadas @DocumentoLeitor, @TipoDocumento, @AgruparNucleo, @AgruparDatas, @InicioIntervalo, @FimIntervalo";

            var param = new Dictionary<string, object>
            {
                {"@DocumentoLeitor", dto.DocumentoLeitor},
                {"@TipoDocumento", dto.TipoDocumento},
                {"@AgruparNucleo", dto.AgruparNucleo},
                {"@AgruparDatas", dto.AgruparDatas},
                {"@InicioIntervalo", dto.InicioIntervalo},
                {"@FimIntervalo", dto.FimIntervalo}
            };

            string connection = _connectionHelper.getConnectionString(tagRepo);

            try
            {
                if (string.IsNullOrEmpty(connection))
                    throw new Exception($"Erro de Configuração: A tag '{tagRepo}' não possui uma ConnectionString válida.");

                DalPro.ConnectionString = connection;

                return DalPro.Query<HistObrasDTO>(sql, param);
            }
            catch (SqlException ex)
            {
                throw new Exception($"Erro de banco de dados: {ex}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro inesperado: {ex}");
            }
        }

        

        public int Insert(CreateObraDTO dto, string tagRepo)
        {
            string connection = _connectionHelper.getConnectionString(tagRepo);
            if (string.IsNullOrEmpty(connection))
                throw new Exception($"Erro de conexão com base de dados");

            DalPro.ConnectionString = connection;

            string sql = "EXEC sp_AdicionarObra @Autor, @Titulo, @AssuntoId";

            var param = new Dictionary<string, object>
             {
                 {"@Autor", dto.Autor},
                 {"@Titulo", dto.Titulo},
                 {"@AssuntoId", dto.Assunto_Id}
             };


            try
            {
                if (string.IsNullOrEmpty(connection))
                    throw new Exception($"Erro de Configuração: A tag '{tagRepo}' não possui uma ConnectionString válida.");

                DalPro.ConnectionString = connection;

                int id = Convert.ToInt32(DalPro.ExecuteScalar(sql, param));

                return id;
            }
            catch (SqlException ex)
            {
                throw new Exception($"Erro de banco de dados: {ex}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro inesperado: {ex}");
            }

        }

        public bool Update(int id, CreateObraDTO dto, string tagRepo)
        {
            var obra = GetById(id, tagRepo);

            if (obra is null) return false;

            var assunto = _assuntoRepository.GetById(dto.Assunto_Id, tagRepo);

            if (assunto is null) return false;

            string connection = _connectionHelper.getConnectionString(tagRepo);

            string sql = @"UPDATE Obras SET
            Autor = @Autor, Titulo = @Titulo, Assunto = @Assunto
            WHERE Id = @Id";

            var param = new Dictionary<string, object>
            {
                {"@autor", dto.Autor},
                {"@Assunto", dto.Assunto_Id},
                {"@Titulo", dto.Titulo},
                {"@Id", id }
            };

            try
            {
                if (string.IsNullOrEmpty(connection))
                    throw new Exception($"Erro de Configuração: A tag '{tagRepo}' não possui uma ConnectionString válida.");

                DalPro.ConnectionString = connection;

                int rows = DalPro.Execute(sql, param);

                return rows > 0;
            }
            catch (SqlException ex)
            {
                throw new Exception($"Erro de banco de dados: {ex}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro inesperado: {ex}");
            }
        }

        public int UpdateCount(int id, string tagRepo)
        {
            string sql = "SELECT COUNT(Id) FROM Exemplares WHERE Id_Obra = @Id_Obra";

            var param = new Dictionary<string, object>
            {
                {"Id_Obra", id}
            };

            string connection = _connectionHelper.getConnectionString(tagRepo);

            try
            {
                if (string.IsNullOrEmpty(connection))
                    throw new Exception($"Erro de Configuração: A tag '{tagRepo}' não possui uma ConnectionString válida.");

                DalPro.ConnectionString = connection;

                return Convert.ToInt32(DalPro.ExecuteScalar(sql, param));
            }
            catch (SqlException ex)
            {
                throw new Exception($"Erro de banco de dados: {ex}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro inesperado: {ex}");
            }
        }


        public bool Delete(int id, string tagRepo)
        {
            var obra = GetById(id, tagRepo);

            if(obra is null) return false;  

            string connection = _connectionHelper.getConnectionString(tagRepo);

            string sql = "EXEC sp_DeleteObra @Id";

            var param = new Dictionary<string, object>
            {
                {"@Id", id}
            };

            try
            {
                if (string.IsNullOrEmpty(connection))
                    throw new Exception($"Erro de Configuração: A tag '{tagRepo}' não possui uma ConnectionString válida.");

                DalPro.ConnectionString = connection;

                DalPro.Execute(sql, param);

                int rows = DalPro.Execute(sql, param);

                return rows > 0;
            }
            catch (SqlException ex)
            {
                throw new Exception($"Erro de banco de dados: {ex}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro inesperado: {ex}");
            }
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
       

