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
- **Entities**: Contém as entidades do domínio (Post, Comment) que representam os objetos de negócio
- **Interfaces**: Define contratos para repositórios e casos de uso, estabelecendo as regras de comunicação entre camadas
- **Funcionalidade**: Responsável por definir as regras de negócio e entidades do sistema, sendo independente de frameworks e tecnologias externas

### 2. Application Layer
- **Use Cases**: Implementa a lógica de negócios através de casos de uso específicos (GetAllPosts, CreatePost)
- **DTOs**: Objetos de transferência de dados que isolam a camada de domínio da apresentação
- **Mappers**: Configuração do AutoMapper para conversão entre entidades e DTOs
- **Funcionalidade**: Orquestra as operações do sistema, aplicando regras de negócio e coordenando o fluxo de dados

### 3. Infrastructure Layer
- **Repositories**: Implementações concretas dos repositórios que gerenciam o acesso a dados
- **Services**: Serviços externos (JsonPlaceholder API) para integração com APIs
- **Persistence**: Configuração do Entity Framework Core e SQLite para persistência local
- **Funcionalidade**: Fornece implementações concretas para interfaces definidas nas camadas superiores, lidando com detalhes técnicos

### 4. Presentation Layer
- **Views**: Interface do usuário em XAML com layouts responsivos
- **ViewModels**: Lógica de apresentação que gerencia o estado da UI e comandos
- **Models**: Modelos específicos da UI para binding de dados
- **Funcionalidade**: Responsável pela interface com o usuário, implementando o padrão MVVM para separação de responsabilidades

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
├── SiggaBlog.Domain/                    # Camada de domínio
│   ├── Entities/
│   │   └── Post.cs                      # Entidade principal do domínio
│   └── Interfaces/
│       └── IPostRepository.cs           # Contrato do repositório
│
├── SiggaBlog.Application/               # Camada de aplicação
│   ├── UseCases/
│   │   └── Posts/
│   │       └── GetAllPostsUseCase.cs    # Caso de uso para listar posts
│   └── DTOs/
│       └── PostDTO.cs                   # Objeto de transferência
│
├── SiggaBlog.Infrastructure/            # Camada de infraestrutura
│   ├── Repositories/
│   │   └── PostRepository.cs            # Implementação do repositório
│   └── Services/
│       └── JsonPlaceholderService.cs    # Serviço de API
│
├── SiggaBlog/                           # Camada de apresentação
│   ├── Views/
│   │   └── MainPage.xaml               # Tela principal
│   └── ViewModels/
│       └── MainPageViewModel.cs        # ViewModel principal
│
└── SiggaBlog.Tests/                     # Testes
    ├── Application/
    │   └── UseCases/
    │       └── GetAllPostsUseCaseTests.cs  # Testes unitários
    └── Integration/
        └── PostRepositoryTests.cs          # Testes de integração
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
