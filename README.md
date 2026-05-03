# GestaoTarefas API

> API REST desenvolvida em C# como parte do Trabalho de Conclusão de Curso (TCC) do curso de Sistemas de Informação.

---

## 📋 Sobre o Projeto

Muitos pais enfrentam dificuldades em incentivar seus filhos a cumprirem tarefas domésticas e escolares de forma consistente. A falta de engajamento pode impactar no desenvolvimento de responsabilidade, organização e disciplina.

Além disso, a educação financeira infantil ainda é pouco trabalhada na prática, principalmente no acompanhamento da mesada e na análise de gastos.

O **GestaoTarefas** é uma API REST integrada a um aplicativo mobile gamificado, desenvolvida para incentivar crianças ao cumprimento de tarefas domésticas e escolares, além de promover educação financeira por meio da análise de gastos da mesada.

---

## 🎯 Objetivos

- Desenvolver uma API REST utilizando ASP.NET Core
- Implementar autenticação de usuários via JWT (perfil Pai e Filho)
- Criar banco de dados relacional com SQL Server
- Implementar sistema de cadastro de tarefas com pontuação
- Permitir envio de foto como comprovação da tarefa
- Implementar sistema de validação pelo responsável
- Criar sistema de pontuação e recompensas
- Desenvolver módulo de upload e leitura de planilhas financeiras (CSV/Excel)
- Gerar análises e relatórios simples sobre gastos da mesada

---

## 🏗️ Arquitetura

O projeto segue a arquitetura em camadas (Clean Architecture):

```
GestaoTarefas/
├── GestaoTarefas.API          # Controllers, Middlewares, Program.cs
├── GestaoTarefas.Application  # Services, Interfaces, DTOs, Mapping
├── GestaoTarefas.Domain       # Entities, Enums, Interfaces de repositório
└── GestaoTarefas.Infra        # DbContext, Repositories, Migrations, Mappings EF
```

---

## 🛠️ Tecnologias

| Camada | Tecnologia |
|--------|-----------|
| Back-end | C# / ASP.NET Core 8 |
| ORM | Entity Framework Core |
| Banco de Dados | SQL Server |
| Autenticação | JWT Bearer |
| Criptografia | BCrypt.Net |
| Documentação | Swagger / OpenAPI |
| Front-end Mobile | React Native (repositório separado) |

---

## 📦 Pacotes NuGet

- `Microsoft.EntityFrameworkCore.SqlServer`
- `Microsoft.AspNetCore.Authentication.JwtBearer`
- `BCrypt.Net-Next`
- `Swashbuckle.AspNetCore`

---

## 🗄️ Modelo de Dados

```
usuario               → dados base do usuário (Pai ou Filho)
pai                   → extensão para perfil Pai
filho                 → extensão para perfil Filho (DataNascimento)
pais_filhos           → vínculo entre Pai e Filho (N:N)
tarefa                → tarefas criadas pelo Pai para o Filho
comprovacao_tarefa    → foto enviada pelo Filho como comprovação
pontuacao             → pontos ganhos pelo Filho ao completar tarefas
resgate_pontuacao     → registro de pontos debitados ao resgatar recompensas
recompensa            → recompensas cadastradas pelo Pai
recompensa_resgatada  → histórico de resgates do Filho
mesada                → controle de mesada do Filho
registro_financeiro   → gastos registrados da mesada
categoria_financeira  → categorias dos gastos
```

---

## 🔐 Segurança

- Autenticação via **JWT Bearer Token**
- Senhas criptografadas com **BCrypt**
- Rotas protegidas com `[Authorize]`
- Controle de acesso por perfil:
  - `[Authorize(Roles = "Pai")]` — criar tarefas, validar comprovações, cadastrar filhos
  - `[Authorize(Roles = "Filho")]` — enviar comprovações, ver pontos, resgatar recompensas

---

## 🚀 Como Executar

### Pré-requisitos

- .NET 8 SDK
- SQL Server
- Visual Studio 2022 ou VS Code

### Configuração

1. Clone o repositório:
```bash
git clone https://github.com/seu-usuario/GestaoTarefas.git
```

2. Configure a string de conexão no `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=SEU_SERVIDOR;Database=GestaoTarefasDB;Trusted_Connection=True;"
  },
  "Jwt": {
    "Key": "sua-chave-secreta-minimo-32-caracteres",
    "Issuer": "GestaoTarefasAPI",
    "Audience": "GestaoTarefasApp",
    "ExpiracaoHoras": 8
  }
}
```

3. Execute as migrations:
```bash
dotnet ef database update --context AppDbContexto --project GestaoTarefas.Infra --startup-project GestaoTarefas.API
```

4. Execute o projeto:
```bash
dotnet run --project GestaoTarefas.API
```

5. Acesse o Swagger:
```
http://localhost:7018/swagger
```

---

## 📡 Endpoints

### Auth
| Método | Rota | Descrição | Perfil |
|--------|------|-----------|--------|
| POST | `/api/v1/Auth/Login` | Login e geração de token | Público |

### Usuário
| Método | Rota | Descrição | Perfil |
|--------|------|-----------|--------|
| POST | `/api/v1/Usuario/AdicionarUsuario` | Cadastrar Pai | Público |
| POST | `/api/v1/Usuario/AdicionarFilho` | Cadastrar Filho | Pai |
| GET | `/api/v1/Usuario/ObterTodos` | Listar usuários | Autenticado |
| GET | `/api/v1/Usuario/ObterPorId/{id}` | Buscar usuário | Autenticado |
| PUT | `/api/v1/Usuario/AtualizarUsuario/{id}` | Atualizar usuário | Pai |
| DELETE | `/api/v1/Usuario/RemoverUsuario/{id}` | Remover usuário | Pai |

### Tarefa
| Método | Rota | Descrição | Perfil |
|--------|------|-----------|--------|
| POST | `/api/v1/Tarefa/CriarTarefa` | Criar tarefa | Pai |
| GET | `/api/v1/Tarefa/ObterTodas` | Listar tarefas | Autenticado |
| GET | `/api/v1/Tarefa/{tarefaId}` | Buscar tarefa | Autenticado |
| GET | `/api/v1/Tarefa/filho/{filhoId}` | Tarefas do Filho | Filho |
| PUT | `/api/v1/Tarefa/{tarefaId}` | Atualizar tarefa | Pai |
| DELETE | `/api/v1/Tarefa/{tarefaId}` | Remover tarefa | Pai |

### Comprovação
| Método | Rota | Descrição | Perfil |
|--------|------|-----------|--------|
| POST | `/api/v1/Comprovacao/Enviar` | Enviar foto | Filho |
| PATCH | `/api/v1/Comprovacao/Validar/{id}` | Aprovar/Reprovar | Pai |
| GET | `/api/v1/Comprovacao/tarefa/{tarefaId}` | Comprovações da tarefa | Autenticado |
| GET | `/api/v1/Comprovacao/{id}` | Buscar comprovação | Autenticado |

### Pontuação
| Método | Rota | Descrição | Perfil |
|--------|------|-----------|--------|
| GET | `/api/v1/Pontuacao/ObterPorFilho/{filhoId}` | Histórico de pontos | Filho |
| GET | `/api/v1/Pontuacao/ObterTotal/{filhoId}` | Saldo de pontos | Filho |
| POST | `/api/v1/Pontuacao/Adicionar` | Adicionar pontos | Pai |

### Recompensa
| Método | Rota | Descrição | Perfil |
|--------|------|-----------|--------|
| POST | `/api/v1/Recompensa/Criar` | Criar recompensa | Pai |
| GET | `/api/v1/Recompensa/ObterPorFilho/{filhoId}` | Recompensas do Filho | Autenticado |
| GET | `/api/v1/Recompensa/ObterPorId/{id}` | Buscar recompensa | Autenticado |
| PUT | `/api/v1/Recompensa/Atualizar/{id}` | Atualizar recompensa | Pai |
| DELETE | `/api/v1/Recompensa/Remover/{id}` | Remover recompensa | Pai |
| POST | `/api/v1/Recompensa/Resgatar/{filhoId}/{recompensaId}` | Resgatar recompensa | Filho |
| GET | `/api/v1/Recompensa/ObterResgatadas/{filhoId}` | Histórico de resgates | Autenticado |

---

## 🎮 Fluxo de Gamificação

```
1. Pai cadastra tarefa com pontuação definida
2. Filho visualiza a tarefa
3. Filho conclui e envia foto como comprovação
4. Pai aprova ou reprova a comprovação
5. Se aprovada → pontos são creditados ao Filho
6. Filho acumula pontos e pode resgatar recompensas
7. Ao resgatar → pontos são debitados via resgate_pontuacao
```

---

## 👤 Autor

Ian Rodrigues Bitencourt — Curso de Sistemas de Informação
