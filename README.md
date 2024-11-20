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
 ```

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
 ```
### Resposta de Erro:

- **404 Not Found**:
    
    ```json
    {
      "message": "Usuário não encontrado."
    }
    ```

### 3. **Criar Usuário**

**Descrição:** Cria um novo usuário na base de dados.

**Método:** `POST`

**URL:** `/api/usuarios`

### Body (JSON):

```json
{
  "nome": "Ana Clara",
  "email": "ana@gmail.com",
  "senha": "123456"
}
 ```

### Resposta de Sucesso:

- **201 Created**:
    
    ```json
    {
      "message": "Usuário criado com sucesso.",
      "usuario": {
        "id": 3,
        "nome": "Ana Clara",
        "email": "ana@gmail.com",
        "senha": "123456"
      }
    }
    ```

### 4. **Atualizar Usuário**

**Descrição:** Atualiza as informações de um usuário existente.

**Método:** `PUT`

**URL:** `/api/usuarios/{id}`

### Parâmetros:

| Nome | Tipo | Obrigatório | Descrição |
| --- | --- | --- | --- |
| `id` | `int` | Sim | ID do usuário. |

### Body (JSON):

```json
{
  "id": 1,
  "nome": "João Silva Atualizado",
  "email": "joao_atualizado@gmail.com",
  "senha": "novaSenha123"
}
 ```
### Resposta de Sucesso:

- **204 No Content**: Indica que a operação foi concluída com sucesso sem corpo de resposta.

### Resposta de Erro:

- **400 Bad Request**:
    
    ```json
    {
      "message": "ID do usuário não corresponde."
    }
    ```
    
- **404 Not Found**:
    
    ```json
    {
      "message": "Usuário não encontrado."
    }
    ```

### 5. **Deletar Usuário**

**Descrição:** Remove um usuário da base de dados.

**Método:** `DELETE`

**URL:** `/api/usuarios/{id}`

### Parâmetros:

| Nome | Tipo | Obrigatório | Descrição |
| --- | --- | --- | --- |
| `id` | `int` | Sim | ID do usuário. |

### Resposta de Sucesso:

- **200 OK**:
    
    ```json
    {
      "message": "Usuário deletado com sucesso."
    }
    ```

### Resposta de Erro:

- **404 Not Found**:
    
    ```json
    {
      "message": "Usuário não encontrado."
    }
    ```
---
## 5. Swagger

Para facilitar a navegação e os testes nos endpoints, o Swagger foi integrado à API.

Acesse a documentação interativa em: [http://localhost:5000/swagger](http://localhost:5000/swagger).

---

## 6. **Relacionamentos de Entidades**

### **Usuário, Missão e Recompensa:**

### **Usuário e Missão:**

- **Missão:** Cada missão é registrada por um usuário e pode ter um valor em pontos, que o usuário define ao criar a missão. Isso significa que o **Usuário** é o responsável por atribuir um valor de pontos para cada missão.
- **Relacionamento:** O campo `UsuarioId` na **Missão** associa a missão ao usuário que a criou. O valor em pontos da missão será um atributo dessa missão.

### **Usuário e Recompensa:**

- **Recompensa:** A recompensa tem um valor de pontos necessário para ser "usada" ou resgatada. O **Usuário** é responsável por definir a quantidade de pontos necessária para resgatar a recompensa.
- **Relacionamento:** O campo `UsuarioId` na **Recompensa** associará a recompensa ao usuário que a registrou. A recompensa estará atrelada a uma quantidade mínima de pontos para ser resgatada.

### **Modelo de Dados:**

1. **Usuário**: O usuário pode registrar múltiplas missões e recompensas.
    
    ```json
    {
      "id": 1,
      "nome": "João Silva",
      "email": "joao@gmail.com",
      "senha": "123456"
    }
    ```
    
2. **Missão**: O usuário define o valor da missão em pontos.
    
    ```json
    {
      "id": 1,
      "descricao": "Missão de Energia Solar",
      "pontos": 50,   
      "usuarioId": 1   
    }
    ```
    
3. **Recompensa**: O usuário define a quantidade de pontos necessária para resgatar a recompensa.
    
    ```json
    {
      "id": 1,
      "descricao": "Desconto de 20% em energia",
      "pontosNecessarios": 100,  
      "usuarioId": 1   
    }
    ```

---

## **7. Testes Implementados**
O projeto inclui testes automatizados para garantir o funcionamento adequado das funcionalidades.

**Testes de Unidade:**

- Verificação da persistência de dados de `Usuario` no banco de dados.
- Validação da recuperação de `Usuario` através do ID.
- Testes de lógicas no serviço de usuário usando mocks para o repositório.

**Testes de Integração:**

- Validação das operações de CRUD no banco de dados em memória.
- Comunicação entre o serviço de usuário e o repositório de dados.

Os testes foram implementados com **xUnit**, garantindo execução contínua e integração com ferramentas de CI/CD.

---

## **8. Clean Code**

Para garantir legibilidade e facilidade de manutenção, o projeto segue os princípios de Clean Code:

- **Nomes Significativos:**
    
    As classes, métodos e variáveis são nomeados de forma clara e descritiva, como `GetAllUsuarios`, `CreateUsuario` e `IUsuarioRepository`.
    
- **Métodos Pequenos e Focados:**
    
    Cada método tem uma única responsabilidade, facilitando a leitura e a reutilização do código.
    
- **Tratamento de Erros Centralizado:**
    
    O tratamento de exceções é centralizado, o que garante que erros sejam gerenciados de forma consistente e eficaz em todo o sistema.

**Princípios SOLID**

O projeto adota os princípios SOLID para garantir que o sistema seja escalável, fácil de manter e de estender:

- **S - Single Responsibility Principle (SRP):**
    
    Cada classe tem uma única responsabilidade, o que facilita a manutenção e evolução do código.
    
- **O - Open/Closed Principle (OCP):**
    
    O sistema está aberto para extensão, mas fechado para modificação, permitindo que novas funcionalidades sejam adicionadas sem alterar o código existente.
    
- **L - Liskov Substitution Principle (LSP):**
    
    As subclasses podem ser usadas no lugar de suas classes base sem afetar a funcionalidade do sistema, garantindo flexibilidade.
    
- **I - Interface Segregation Principle (ISP):**
    
    Interfaces são específicas e focadas, evitando a implementação de métodos desnecessários e melhorando a coesão.
    
- **D - Dependency Inversion Principle (DIP):**
    
    O sistema depende de abstrações (interfaces), e não de implementações concretas, tornando o código mais flexível e fácil de testar.

---

## **9. Funcionalidades de IA Generativa - Análise de Consumo de Energia**

A API **WattEco** inclui um serviço de análise de sentimento baseado em aprendizado de máquina, desenvolvido com o ML.NET, que permite analisar a polaridade do feedback gerado a partir do consumo de energia, ajudando os usuários a entenderem a natureza do seu uso e como otimizar o consumo.

### Pré-requisito

- **Configuração do Ambiente**: Certifique-se de que o ambiente de desenvolvimento está configurado corretamente.

### Estrutura do Código

- **EnergyConsumptionData**: Representa os dados de entrada para a análise, contendo o texto do feedback gerado sobre o consumo de energia.
- **SentimentPrediction**: Representa o resultado da previsão, contendo o sentimento (positivo ou negativo) e a probabilidade associada.
- **SentimentAnalysisService**: Serviço responsável pela execução do modelo de aprendizado de máquina para a previsão de sentimentos.
- **EnergiaController**: Controla as requisições da API para análise de consumo de energia e geração de feedback.

### Endpoint de Análise de Consumo de Energia

- **Método**: POST
- **URL**: `/api/energia/analyze`
- **Corpo da Requisição**:
    
    ```json
    [100, 250, 150]  
    ```
  - Lista de consumos de energia em kWh
### Resposta da API

A resposta da API incluirá a média do consumo, o feedback gerado, o sentimento previsto e a probabilidade associada. A estrutura da resposta é a seguinte:

```json
{
  "MediaConsumo": 166.67,
  "Feedback": "Seu consumo de energia está na média. Considere práticas para otimizar o uso de energia.",
  "Sentimento": "Negativo",
  "Probabilidade": 0.75
}
 ```

### Exemplos de Uso

Abaixo estão exemplos de entradas de consumo e suas respectivas saídas de análise de sentimento pela API:

### Consumo Alto

- **Entrada**: `[300, 400, 350]`
- **Saída**:
    
    ```json
    {
      "MediaConsumo": 350,
      "Feedback": "Seu consumo de energia está alto. Tente aplicar práticas para reduzir o uso e economizar energia.",
      "Sentimento": "Negativo",
      "Probabilidade": 0.92
    }
    ```

### Consumo Ótimo

- **Entrada**: `[80, 90, 70]`
- **Saída**:
    
    ```json
    {
      "MediaConsumo": 80,
      "Feedback": "Seu consumo de energia está ótimo. Continue com as boas práticas!",
      "Sentimento": "Positivo",
      "Probabilidade": 0.98
    }
    ```

### Consumo Médio

- **Entrada**: `[150, 180, 170]`
- **Saída**:
    
    ```json
    {
      "MediaConsumo": 166.67,
      "Feedback": "Seu consumo de energia está na média. Considere práticas para otimizar o uso de energia.",
      "Sentimento": "Negativo",
      "Probabilidade": 0.75
    }
    ```

---

## **10. Membros do Grupo**

- **Stephany Siqueira** RM: 98258
- **Camila Dos Santos Cunha** RM: 551785
- **Guilherme Castro** RM: 99624
- **Thiemi Hiratani Favaro** RM: 551478
- **Ana Clara Rocha de Oliveira** RM: 550110
