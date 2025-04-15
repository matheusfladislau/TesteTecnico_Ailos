## Questão 1:

Projeto desenvolvido com .NET 8 com o objetivo de cadastrar contas bancárias e realizar saques/depósitos sem infringir as regras estipuladas pela instituição bancária.

## Estrutura do Projeto:

``` 
questao_1/
└───prjInstFinanceira		
    ├───prjInstFinanceira	
    │   └───Domain	
    │       ├───Model		# Modelos de domínio
    │       └───Validation	# Entidades para validação dos modelos
    └───prjInstFinanceira.Tests	# Projeto para testes dos modelos
        └───Tests
```

## Instalação

Este projeto foi desenvolvido utilizando .NET 8.0. Para Windows, baixe o SDK pelo Visual Studio 2022 ou instale via [Microsoft website](https://dotnet.microsoft.com/pt-br/download/dotnet/8.0). No Linux, recomendo utilizar o pacote [asdf](https://asdf-vm.com/guide/getting-started.html) para um setup simples. Verifique a instalação com ```dotnet --version```.

- Clone o repositório:
```bash
# Todo o repositório:
git clone https://github.com/matheusfladislau/TesteTecnico_Ailos
cd TesteTecnico_Ailos/questao_1/

# Or apenas a pasta questao_1/:
git clone --filter=blob:none --no-checkout https://github.com/matheusfladislau/TesteTecnico_Ailos
cd TesteTecnico_Ailos
git sparse-checkout init --cone
git sparse-checkout set questao_1/
git checkout main
cd questao_1/
```

- Gere as .dlls de cada projeto:
```bash
cd prjInstFinanceira
dotnet build
```

- Rode a aplicação:
```bash
dotnet run --project prjInstFinanceira
```

- Para rodar os testes:
```bash
cd prjInstFinanceira.Tests
dotnet test
```
