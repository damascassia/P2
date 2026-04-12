

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
        }
       
    }
}
