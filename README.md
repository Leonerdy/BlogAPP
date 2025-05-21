# SiggaBlog

SiggaBlog é um aplicativo móvel multi-camâdas desenvolvido em .NET MAUI que permite aos usuários visualizar e criar posts consumindo os dados de um webservice e armazenando-os em um banco de dados Sqlite local, implementando uma arquitetura limpa, testes e seguindo os princípios SOLID.

## 🚀 Tecnologias

- .NET MAUI
- .NET 9 SDK
- C# 12
- Entity Framework Core
- SQLite
- MediatR
- AutoMapper
- XUnit
- Moq

## 🏗️ Arquitetura

O projeto segue a Clean Architecture e é dividido em camadas:

### 1. Domain Layer
- **Entities**: Contém as entidades do domínio (Post, Comment)
- **Interfaces**: Define contratos para repositórios e casos de uso

### 2. Application Layer
- **Use Cases**: Implementa a lógica de negócios
- **DTOs**: Objetos de transferência de dados
- **Mappers**: Configuração do AutoMapper

### 3. Infrastructure Layer
- **Repositories**: Implementações concretas dos repositórios
- **Services**: Serviços externos (JsonPlaceholder API)
- **Persistence**: Configuração do Entity Framework Core

### 4. Presentation Layer
- **Views**: Interface do usuário em XAML
- **ViewModels**: Lógica de apresentação
- **Models**: Modelos específicos da UI

## 📱 Funcionalidades

- Listagem de posts
- Detalhe do post com respectivos comentários
- Criação de novos posts
- Suporte offline
- Sincronização automática

## 🔄 Fluxo de Dados

1. **Online**:
   - Dados são buscados da API JsonPlaceholder
   - Armazenados localmente no SQLite
   - Exibidos na interface

2. **Offline**:
   - Dados são recuperados do banco local
   - Interface adaptada para modo offline
   - Sincronização quando online

## 🛠️ Padrões de Projeto
Alguns dos padrões utilizados:

- **Repository Pattern**: Abstração de acesso a dados
- **UseCase Pattern**: Ações específicas do sistema
- **Mediator Pattern**: Comunicação desacoplada
- **Dependency Injection**: Injeção de dependências
- **MVVM**: Arquitetura de apresentação

## 📦 Estrutura do Projeto

```
SiggaBlog/
├── SiggaBlog.Domain/           # Camada de domínio
├── SiggaBlog.Application/      # Camada de aplicação
├── SiggaBlog.Infrastructure/   # Camada de infraestrutura
├── SiggaBlog/                  # Camada de apresentação
└── SiggaBlog.Tests/            # Testes
```
## 🧪 Testes

O projeto inclui testes unitários e de integração:

### Testes Unitários
- Testes de casos de uso
- Testes de repositórios
- Testes de serviços

### Testes de Integração
- Testes de fluxo completo
- Testes de persistência
- Testes de API

Para executar os testes:
```bash
dotnet test
```

## 📋 Pré-requisitos

- Visual Studio 2022 17.8 ou superior
- .NET 9.0 SDK
- Android SDK (para desenvolvimento Android)
- iOS SDK (para desenvolvimento iOS, requer macOS)
- Windows SDK (para desenvolvimento Windows)

## 🔧 Instalação

1. Clone o repositório:
```bash
git clone https://github.com/seu-usuario/SiggaBlog.git
```

2. Abra a solução no Visual Studio 2022

3. Restaure os pacotes NuGet:
```bash
dotnet restore
```

4. Compile a solução:
```bash
dotnet build
```
## 👥 Autores

 -Anselmo Leonardo Teixeira da silva(https://github.com/Leonaerdy) email:Enge.leon@gmail.com


- [JsonPlaceholder](https://jsonplaceholder.typicode.com/) - API de exemplo
- [.NET MAUI](https://dotnet.microsoft.com/apps/maui) - Framework de desenvolvimento
- [Clean Architecture](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html) - Padrão de arquitetura 
