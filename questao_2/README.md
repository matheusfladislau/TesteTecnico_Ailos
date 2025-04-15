## Questão 2:

Projeto desenvolvido com .NET 8 com o objetivo de calcular a quantidade de gols marcados por um time em um ano.

## Estrutura do Projeto:

``` 
questao_2/
│
└── prjGolsAno
	├── Models/		# Modelos de domínio
        └── Services/		# Serviço para utilização da API
```
## Instalação

Este projeto foi desenvolvido utilizando .NET 8.0. Para Windows, baixe o SDK pelo Visual Studio 2022 ou instale via [Microsoft website](https://dotnet.microsoft.com/pt-br/download/dotnet/8.0). No Linux, recomendo utilizar o pacote [asdf](https://asdf-vm.com/guide/getting-started.html) para um setup simples. Verifique a instalação com ```dotnet --version```.

- Clone o repositório:
```bash
# Todo o repositório:
git clone https://github.com/matheusfladislau/TesteTecnico_Ailos
cd TesteTecnico_Ailos/questao_2/

# Or apenas a pasta questao_2/:
git clone --filter=blob:none --no-checkout https://github.com/matheusfladislau/TesteTecnico_Ailos
cd TesteTecnico_Ailos
git sparse-checkout init --cone
git sparse-checkout set questao_2/
git checkout main
cd questao_2/
```

- Gere as .dlls de cada projeto:
```bash
cd prjGolsAno
dotnet build
```

- Rode a aplicação:
```bash
dotnet run --project prjGolsAno
```
