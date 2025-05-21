# SiggaBlog

SiggaBlog é um aplicativo móvel desenvolvido em .NET MAUI que permite aos usuários visualizar e criar posts, implementando uma arquitetura limpa e seguindo os princípios SOLID.

## 🚀 Tecnologias

- .NET MAUI
- C# 12
- Entity Framework Core
- SQLite
- MediatR
- AutoMapper
- XUnit
- Moq

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

## 🏗️ Arquitetura

O projeto segue a Clean Architecture e é dividido em camadas:

### 1. Domain Layer
- **Entities**: Contém as entidades do domínio (Post, Comment)
- **Interfaces**: Define contratos para repositórios e casos de uso
- **Value Objects**: Implementa objetos de valor imutáveis

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

## 📱 Funcionalidades

- Listagem de posts
- Criação de novos posts
- Visualização de detalhes do post
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

- **Repository Pattern**: Abstração de acesso a dados
- **CQRS**: Separação de comandos e consultas
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

## 🤝 Contribuição

1. Faça o fork do projeto
2. Crie uma branch para sua feature (`git checkout -b feature/AmazingFeature`)
3. Commit suas mudanças (`git commit -m 'Add some AmazingFeature'`)
4. Push para a branch (`git push origin feature/AmazingFeature`)
5. Abra um Pull Request

## 📝 Licença

Este projeto está sob a licença MIT. Veja o arquivo [LICENSE](LICENSE) para mais detalhes.

## 👥 Autores

- Seu Nome - [seu-usuario](https://github.com/seu-usuario)

## 🙏 Agradecimentos

- [JsonPlaceholder](https://jsonplaceholder.typicode.com/) - API de exemplo
- [.NET MAUI](https://dotnet.microsoft.com/apps/maui) - Framework de desenvolvimento
- [Clean Architecture](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html) - Padrão de arquitetura 