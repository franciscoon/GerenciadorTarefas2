# Gerenciador de Tarefas

Este projeto é uma aplicação web full stack para gerenciamento de tarefas. Ele é dividido em:

- **Backend:** ASP.NET Core (.NET 9) com Entity Framework Core e SQL Server
- **Frontend:** React com Vite

---

## Tecnologias Utilizadas

### Backend (.NET)
- ASP.NET Core (.NET 9)
- Entity Framework Core 9
- AutoMapper
- SQL Server
- Swagger (Swashbuckle)

### Frontend (React)
- React 19
- Vite
- Axios
- React Router DOM
- React Toastify
- ESLint

---

## Como rodar o projeto localmente

### Pré-requisitos

- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)
- [Node.js (v16+)](https://nodejs.org/)
- [SQL Server Local ou Azure SQL](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads)

---

### Clonando o repositório

```bash
git clone https://github.com/franciscoon/GerenciadorTarefas2.git
cd GerenciadorTarefas2
```

---

### Backend (API .NET)

1. Acesse a pasta da API:

```bash
cd GerenciadorTarefas
```

2. Restaure as dependências do projeto:

```bash
dotnet restore
```

3. Rode as migrations para criar o banco:

```bash
dotnet ef database update
```

Nota: O Entity Framework irá criar o banco de dados GerenciadorTarefasDB automaticamente, com base na configuração da sua string de conexão no arquivo appsettings.json

4. Inicie o backend:

```bash
dotnet run
```

A API estará disponível em https://localhost:44367 por padrão.

---

## 🗄Banco de Dados

- Utiliza **SQL Server Local**.
- Para criar o banco, é necessário aplicar as migrations com o comando:

```bash
dotnet ef database update
```

- As migrations estão configuradas com Entity Framework Core.
- Certifique-se de que o SQL Server esteja rodando localmente ou ajuste a `connectionString` em `appsettings.json`.

---

### Frontend (React)

1. Acesse a pasta do frontend:

```bash
cd ../gerenciadorFront
```

2. Instale as dependências:

```bash
npm install
```

3. Inicie o servidor de desenvolvimento:

```bash
npm run dev
```

- A aplicação estará disponível em [http://localhost:5173](http://localhost:5173) (por padrão).

---

## Testes
Os testes utilizam o framework xUnit. Certifique-se de que todas as dependências foram restauradas com `dotnet restore` antes de executar.
Para rodar os testes de unidade, use o comando abaixo:

1. Navegue até a pasta do projeto de testes: No terminal, vá até a pasta Gerenciador.Application.Tests

```bash
cd Gerenciador.Application.Tests
```

2. Use o comando dotnet test: 
```bash
dotnet test
```
---
