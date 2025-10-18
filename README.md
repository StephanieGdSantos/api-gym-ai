# APIGymAI

API que utiliza Inteligência Artificial Generativa (Cohere) para criar planos de treinos personalizados.

## 📋 Descrição

A APIGymAI é uma API que gera treinos personalizados com base nas características individuais do usuário, utilizando IA generativa para criar programas de exercícios adaptados às necessidades específicas de cada pessoa.

## 🚀 Funcionalidades

- Geração de treinos personalizados baseados em IA
- Suporte a diferentes níveis de condicionamento (Iniciante, Intermediário, Avançado)
- Personalização por objetivos (Hipertrofia, Emagrecimento, Definição, etc.)
- Adaptação a limitações físicas
- Variações de treino (ABC, ABCD, etc.)
- Controle de período de treino automático

## 🛠️ Tecnologias Utilizadas

- .NET 8
- C#
- Cohere API (IA Generativa)
- Swagger/OpenAPI
- Circuit Breaker Pattern
- Retry Pattern

## 📦 Endpoints

### POST /Exercicio

Cria um plano de treino personalizado.

#### Requisição

```json
{
  "idade": 25,
  "altura": 1.75,
  "peso": 70,
  "infoCorporais": {
    "percentualGordura": 20,
    "massaMuscular": 30,
    "sexo": "Masculino",
    "limitacoes": ["Joelho"]
  },
  "infoPreferencias": {
    "objetivo": "Hipertrofia",
    "partesDoCorpoEmFoco": ["Peito", "Costas"],
    "tempoDeTreinoEmMinutos": 60,
    "variacaoTreino": "ABC",
    "nivel": "Iniciante"
  }
}
```

#### Resposta (200 OK)

```json
{
  "dados": {
    "periodo": {
      "dataInicio": "2024-01-01",
      "dataFim": "2024-01-31"
    },
    "variacaoDeTreino": [
      {
        "dia": "Treino A",
        "musculosTrabalhados": ["Peito", "Tríceps"],
        "exercicios": [
          {
            "nome": "Supino Reto",
            "series": 4,
            "repeticoes": "12",
            "musculoAlvo": ["Peito", "Tríceps"],
            "observacao": "Manter escapulas retraídas"
          }
        ]
      }
    ]
  },
  "sucesso": true,
  "statusCode": 200
}
```

#### Resposta (400 Bad Request)

```json
{
  "dados": null,
  "sucesso": false,
  "statusCode": 400
}
```

## 🔧 Configurações

### Variáveis de Ambiente Necessárias

```env
COHERE_API_KEY=sua_chave_api_aqui
```

### Configurações do appsettings.json

```json
{
    "PeriodoEmDiasPorCondicionamentoFisico": {
    "Iniciante": 60,
    "Intermediario": 75,
    "Avancado": 45
  },
  "InformacoesPrompt": {
    "QuantidadeMinimaExercicios": 4,
    "QuantidadeMaximaExercicios": 12,
    "BasePrompt": "Haja como um personal trainer profissional. Considere as seguintes informações do aluno: [informações]. Monte um plano de treino de academia no formato de variação indicado no campo 'variacaoTreino' (ABC, ABCD ou ABCDE). Cada treino deve conter entre [quantidadeMinimaDeExercicios] e [quantidadeMaximaDeExercicios] exercícios, com duração aproximada de 'tempoDeTreino' por dia. Quanto mais treinos, mais isolados e específicos devem ser os grupos musculares por dia.\r\n\r\nPara cada exercício, informe:\r\n\r\n nome do exercício\r\n\r\nnúmero de séries\r\n\r\nnúmero de repetições\r\n\r\nmúsculos alvo (ex: “bíceps braquial”, “peitoral superior”, “glúteo máximo”)\r\n\r\nSua resposta deve ser um JSON válido, no formato exato abaixo. Não adicione comentários, explicações, formatação markdown, nem texto adicional. Apenas o JSON. json {\"variacaoDeTreino\": [{\"dia\": \"TREINO A\",\"musculosTrabalhados\": [\"peitoral\", \"tríceps\"],\"exercicios\": [{\"nome\": \"supino reto com barra\", \"series\": 4, \"repeticoes\": \"10-12\", \"musculoAlvo\": [\"peitoral médio\", \"tríceps medial\" ]} // demais exercícios ]}// TREINO B, C, etc.]}"
  },
  "PolicyConfigs": {
    "NumeroMaximoDeExcecoesAntesDeCair": 5,
    "IntervaloDeQuedaEmSegundos": 10,
    "IntervaloEmSegundosParaAguardarAntesDeTentarNovamente": 2,
    "QuantidadeMaximaDeRetentativas": 30
  },
  "HttpClientOptions": {
    "TempoDeVidaEmMinutos": 15
  }
}
```

## 🚀 Como Executar

1. Clone o repositório
```bash
git clone https://github.com/stephaniegdsantos/api-gym-ai.git
```

2. Navegue até a pasta do projeto
```bash
cd api-gym-ai
```

3. Restaure as dependências
```bash
dotnet restore
```

4. Configure as variáveis de ambiente

5. Execute o projeto
```bash
dotnet run
```

6. Acesse a documentação Swagger em:
```
https://localhost:5001/swagger.html
```

## 🧪 Testes

Execute os testes usando:
```bash
dotnet test
```

### Cobertura de Testes

- Testes Unitários para validações
- Testes de Integração para fluxo completo
- Testes de Resiliência para Circuit Breaker
- Mock de respostas da IA

## 🔐 Segurança

- Rate Limiting
- Validação de entrada
- Sanitização de dados
- Logs de auditoria

## 🤝 Como Contribuir

1. Faça um Fork do projeto
2. Crie uma Branch para sua feature
   ```bash
   git checkout -b feature/NovaFeature
   ```
3. Commit suas mudanças
   ```bash
   git commit -m 'Adiciona nova feature'
   ```
4. Push para a Branch
   ```bash
   git push origin feature/NovaFeature
   ```
5. Abra um Pull Request

## 📧 Contato

- Email: gomess214@gmail.com
- LinkedIn: [Stephanie Gomes](https://www.linkedin.com/in/stephanie-gomes-842a192a7/)