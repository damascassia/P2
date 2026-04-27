# Biblioteca Pazu API

API REST desenvolvida em ASP.NET Core para gestão de biblioteca, com persistência em SQL Server e segurança via JWT.

---

### Descrição
Sistema modular para controlo de utilizadores, obras e empréstimos, utilizando uma arquitetura em camadas para garantir escalabilidade e organização.

### Tecnologias
* Backend: C# / ASP.NET Core Web API
* Base de Dados: SQL Server / ADO.NET
* Segurança: Autenticação e Autorização JWT
* Documentação: Swagger (OpenAPI)

### Arquitetura
* WebAPI: Controllers e Endpoints
* Services: Lógica de negócio e Interfaces
* DAL: Persistência e acesso a dados (DalProLib)
* Models: Estrutura de dados e DTOs

### Segurança
Para aceder aos endpoints protegidos via Swagger:
1. Gere o token no endpoint de login.
2. Clique em Authorize.
3. Insira: Bearer {seu_token}.

### Instalação
1. Execute o script BibliotecaXPTODB.sql.
2. Configure a DefaultConnection no appsettings.json.
3. Execute: dotnet run.
4. Aceda a: https://localhost:{porta}/swagger.

### Autores
Cássia Damas Silva | Pedro Matias | Luiza Bandeira
Projeto Académico (UPskill)