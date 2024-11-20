# Documentação da API - WattEco

## 1. Introdução

A API **WattEco** foi desenvolvida com o objetivo de contribuir para a melhoria do consumo de energia, utilizando boas práticas de arquitetura de software. A API é construída sobre uma arquitetura robusta, utilizando design patterns para garantir escalabilidade, manutenibilidade e desempenho. Inclui testes automatizados e integração com um banco de dados Oracle para persistência de dados.

---

## 2. Padrões de Design Utilizados

- **DTO (Data Transfer Object)**: Utilizado para transferência de dados entre o cliente e a API. Garante que apenas as informações necessárias sejam expostas e manipuladas.
- **Repositório**: Utilizado para interagir com o banco de dados, separando a lógica de acesso aos dados da lógica de negócios, garantindo maior organização e manutenibilidade.

---

## **3. Instruções para Rodar a API**

**Pré-requisitos:**

- .NET SDK 6.0 ou superior
- Banco de dados Oracle (configurado no arquivo `appsettings.json` com a chave `OracleConnection`)
- Visual Studio ou outro ambiente de desenvolvimento com suporte para .NET Core

**Passo a Passo:**

1. Clone o repositório:
    
    ```bash
    git clone https://github.com/StephanySiqueira/WattEco.git
    ```
    
2. Abra o projeto no Visual Studio ou no seu editor de código preferido.
3. Configure a string de conexão com o banco de dados Oracle no arquivo `appsettings.json`:
    
    ```json
    {
      "ConnectionStrings": {
        "OracleConnection": "Data Source=seubancodedados;User Id=seuusuario;Password=suasenha;"
      }
    }
    ```
    
4. Restaure as dependências do projeto:
    
    ```bash
    dotnet restore
    ```
    
5. Rode as migrações para criar o banco de dados:
    
    ```bash
    dotnet ef database update
    ```
    
6. Execute a API:
    
    ```bash
     dotnet run
    ```

---

## 4. Endpoints

### 1. **Obter Todos os Usuários**

**Descrição:** Retorna uma lista de todos os usuários cadastrados.

**Método:** `GET`

**URL:** `/api/usuarios`

#### Resposta de Sucesso:

```json
[
  {
    "id": 1,
    "nome": "João Silva",
    "email": "joao@gmail.com",
    "senha": "123456"
  },
  {
    "id": 2,
    "nome": "Maria Souza",
    "email": "maria@gmail.com",
    "senha": "abcdef"
  }
]

### 2. **Obter Usuário por ID**

**Descrição:** Retorna os detalhes de um usuário específico com base no ID fornecido.

**Método:** `GET`

**URL:** `/api/usuarios/{id}`

### Parâmetros:

| Nome | Tipo | Obrigatório | Descrição |
| --- | --- | --- | --- |
| `id` | `int` | Sim | ID do usuário. |

### Resposta de Sucesso:

```json
{
  "id": 1,
  "nome": "João Silva",
  "email": "joao@gmail.com",
  "senha": "123456"
}
