## Questão 5:

Projeto desenvolvido com .NET 8, seguindo os princípios da Arquitetura Limpa e utilizando o padrão CQRS (Command Query Responsibility Segregation). O sistema tem como objetivo o gerenciamento de contas correntes e seus respectivos movimentos financeiros, oferecendo uma estrutura escalável, desacoplada e de fácil manutenção.

## Tecnologias utilizadas:

- .NET 8
- C#
- MediatR – para implementação de comandos e queries no padrão CQRS
- Swagger – para documentação e teste interativo da API REST
- Dapper – utilizado como ORM leve e performático
- SQLite – banco de dados relacional leve e embutido

## Estrutura do Projeto:

``` 
questao_5/
│
├── ConCorrente.Application/     # Regras de aplicação e CQRS (comandos, queries, handlers)
├── ConCorrente.Domain/          # Entidades e regras de negócio (núcleo do domínio)
├── ConCorrente.Infra.Data/      # Persistência de dados com Dapper e SQLite
├── ConCorrente.Infra.IoC/       # Injeção de dependência e configuração de serviços
├── ConCorrente.WebAPI/          # API REST para exposição das funcionalidades
├── prjMovimentacaoConta/        # Projeto auxiliar para movimentações (escrita)
└── prjSaldoConta/               # Projeto auxiliar para consulta de saldo (leitura)

```
## Instalação

Este projeto foi desenvolvido utilizando .NET 8.0. Para Windows, baixe o SDK pelo Visual Studio 2022 ou instale via [Microsoft website](https://dotnet.microsoft.com/pt-br/download/dotnet/8.0). No Linux, recomendo utilizar o pacote [asdf](https://asdf-vm.com/guide/getting-started.html) para um setup simples. Verifique a instalação com ```dotnet --version```.

- Clone o repositório:
```bash
# Todo o repositório:
git clone https://github.com/matheusfladislau/TesteTecnico_Ailos

# Or apenas a pasta questao_5/:
git clone --filter=blob:none --no-checkout https://github.com/matheusfladislau/TesteTecnico_Ailos
cd TesteTecnico_Ailos
git sparse-checkout init --cone
git sparse-checkout set questao_5/
git checkout main
```

- Gere as .dlls de cada projeto:
```bash
dotnet build
```

- Rode a API e os serviços:
```bash
dotnet run --project ConCorrente.WebAPI
dotnet run --project prjMovimentacaoConta
dotnet run --project prjSaldoConta
```
