# DTI
Simulação de cálculos de Juros Simples e Composto.
Programa escrito em .NET Core v2.1 do tipo Console Application.
Construção de API em .NET Core v2.1 do tipo .Net Core Web API. 

# Executando a Apliacação

Para executar a aplicação, clone este repositório (https://github.com/Vkto/DTI). 

Abra o terminal na pasta DTI  e execute o comando Dotnet build para compilar o programa. 

Execute o comando dotnet run na pasta API para que o console utilize dela. 

Após executado a API, execute o comando dotnet run --project .\DTI\DTI.csproj para iniciar a aplicação, ou execute dotnet publish -c Release -r win-x64 --self-contained false para gerar um executável na pasta \src\DTI\bin\Release\netcoreapp2.1\win-x64\publish.

Execute Dotnet test para executar os testes.

# Observações

O sistema utiliza da Fachada para acesso a API, que executa suas regras e somatórios pela classe de Negócio.

O Programa utiliza também de classes como:
- Dados para controle de informações. <br>
- Apresentação para exibição de informações para o usuário. <br>
- Enumerador para personificação de botões e ações. <br>
- Fachada para acesso a Controller. <br>

Ao executar o console, será disponibilizado duas opções para uso: 

- Simular Juros<br>
- Consultar Histórico<br>

A simulação de juros se basea na transferência de 3 informações para a execução do cálculo, o montante, a taxa de juros que será aplicado em cada parcela e o prazo (em anos) que será somado. 

A Consulta do histórico de simulações permite que o usuário veja as simulações anteriores executadas por ele, a fim de ter os resultados gravados para fins posteriores. 

# Instruções

A aplicação console utiliza da API para requisições Http, sendo assim, este controle é dado pela URL LocalHost, que está configurada para acessar a porta https://localhost:5001/. 

Caso esta esteja ocupada, poderá ser modificada no arquivo JurosFachada linha 23. 

# ALERTA

Para a simulação de juros, é crucial que a API esteja rodando. Para isso, sete-a como prioridade na ordem de execução (como é o caso do VisualStudio) ou execute-a antes da utilização do console. 
