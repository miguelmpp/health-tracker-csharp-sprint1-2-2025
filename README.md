# HealthTracker – Registro de Atividades de Saúde em C#

Aplicação **console em C#** para registrar e acompanhar atividades de saúde, utilizando **arrays internos (paralelos)** e um **menu de texto simples e direto**.

O sistema permite registrar, listar e calcular estatísticas sobre:
- **Minutos de exercício físico**
- **Litros de água ingeridos**
- **Horas de sono**

O foco do projeto é praticar:
- Organização de código em **métodos**;
- Uso de **arrays** para armazenamento em memória;
- **Validação básica de entrada** do usuário;
- Construção de uma **interface de texto clara**.

---

## 👥 Integrantes

- Vitor Pinheiro Nascimento – RM 553693  
- Gabriel Leão – RM 552642  
- Miguel Parrado – RM 554007  
- Matheus Farias – RM 554254  

---

## 🧠 Visão Geral da Aplicação

Ao executar o programa, o usuário interage com um **menu principal** no console:

```text
===== Health Tracker =====
1) Adicionar registro
2) Listar registros
3) Exibir estatísticas
0) Sair
Escolha:
````

As opções disponíveis são:

1. **Adicionar registro**
   O usuário informa:

   * Tipo de atividade:

     * `1` – Exercício (minutos)
     * `2` – Água (litros)
     * `3` – Sono (horas)
   * Data (ex.: `08/11/2025`)
   * Valor (> 0), de acordo com o tipo escolhido

2. **Listar registros**
   Mostra todos os registros já cadastrados, em **ordem de inserção**, com:

   * Número do registro
   * Data
   * Tipo da atividade
   * Valor formatado com unidade (ex.: `30 min`, `2,00 L`, `7,50 h`)

3. **Exibir estatísticas**
   Calcula e exibe, para **cada tipo de atividade**:

   * **Soma total** dos valores
   * **Média** dos valores (quando houver pelo menos um registro daquele tipo)

4. **Sair**
   Encerra o programa.

---

## 🏗️ Estrutura Interna (Como o programa foi organizado)

### Armazenamento em Arrays

Para atender ao requisito de “armazenar em arrays internos” e manter o código o mais básico possível, foram utilizados **arrays paralelos** com capacidade fixa:

```csharp
const int CAPACIDADE = 200;

static int[] tipos = new int[CAPACIDADE];        // 1 = Exercício, 2 = Água, 3 = Sono
static DateTime[] datas = new DateTime[CAPACIDADE];
static double[] valores = new double[CAPACIDADE];
static int count = 0; // quantidade de registros preenchidos
```

* Cada índice `i` representa **um registro**.
* `tipos[i]`, `datas[i]` e `valores[i]` guardam as informações da mesma atividade.
* O campo `count` controla quantos registros estão realmente preenchidos (evitando percorrer posições vazias do array).

### Métodos Principais

O código foi dividido em **métodos** para cumprir o requisito de organização:

* `Main`
  Laço principal do programa, exibe o menu e chama as operações.

* **Interface/Menu**

  * `MostrarMenu()` – limpa a tela e exibe as opções numeradas.
  * `LerOpcao()` – lê a escolha do usuário e valida se é 0, 1, 2 ou 3.

* **Operações**

  * `AdicionarRegistro(...)` – lê tipo, data e valor, valida e grava nos arrays.
  * `ListarRegistros(...)` – exibe todos os registros cadastrados.
  * `ExibirEstatisticas(...)` – coordena o cálculo de soma e média por tipo.
  * `ExibirEstatisticasTipo(...)` – calcula soma e média para um tipo específico (1, 2 ou 3).

* **Leitura e Validação**

  * `LerTipo()` – garante que o tipo seja apenas 1, 2 ou 3.
  * `LerData()` – utiliza `DateTime.TryParse` para aceitar somente datas válidas.
  * `LerValorPositivo(...)` – só aceita valores numéricos **maiores que zero**, evitando negativos e zero.

* **Funções de apoio**

  * `NomeTipo(int t)` – retorna o nome legível do tipo (Exercício, ÁGUA, SONO).
  * `UnidadeTipo(int t)` – retorna a unidade correspondente (min, L, h).
  * `MensagemValorPorTipo(int t)` – ajusta o texto do prompt conforme o tipo.
  * `FormatarValorParaLista(...)`, `FormatarSoma(...)`, `FormatarMedia(...)` – cuidam da apresentação dos valores na tela.
  * `Pausa()` – exibe “Pressione qualquer tecla para voltar ao menu...” e aguarda o usuário.

---

## ✅ Validações Implementadas

Para atender ao requisito de **validação de entradas** e tratamento de erros simples, o programa faz:

* **Menu:**

  * Só aceita `0`, `1`, `2` ou `3`.
  * Qualquer outro valor ou texto mostra mensagem de erro e pede novamente.

* **Tipo de atividade:**

  * Só aceita `1`, `2` ou `3`.
  * Valores inválidos geram mensagem e nova tentativa.

* **Data:**

  * Usa `DateTime.TryParse` para aceitar apenas datas válidas.
  * Enquanto a data não for válida, o usuário é informado e a leitura é repetida.

* **Valor numérico:**

  * Usa `double.TryParse`.
  * Só aceita **números maiores que zero**.
  * Números negativos, zero ou textos inválidos geram mensagem de erro e uma nova tentativa.

* **Capacidade do array:**

  * Caso o número de registros chegue ao limite (`CAPACIDADE`), o sistema avisa:

    > `Capacidade esgotada. Não é possível adicionar mais registros.`

---

## 🖥️ Como Executar o Projeto

### Opção 1 – Visual Studio 2022

1. Abrir o **Visual Studio 2022**.
2. Ir em **File (Arquivo)** → **Open (Abrir)** → **Project/Solution (Projeto/Solução)**.
3. Selecionar o arquivo da solução (por exemplo, `ConsoleApp1.sln` ou o `.csproj` do projeto).
4. No **Gerenciador de Soluções (Solution Explorer)**, garantir que o projeto esteja marcado como **Startup Project**.
5. Para executar:

   * Pressionar **F5** (com debug), ou
   * Pressionar **Ctrl + F5** (sem debug).

O console será aberto com o menu principal do **HealthTracker**.

### Opção 2 – Linha de Comando (dotnet CLI)

Se o .NET SDK estiver instalado e o projeto for estilo SDK:

1. Abrir o **Prompt de Comando** ou **PowerShell** na pasta do projeto (onde está o `.csproj`).
2. Executar:

```bash
dotnet run
```

---

## 🧪 Exemplo de Uso

### 1) Adicionar 3 registros simples

* Exercício: 30 minutos em `08/11/2025`
* Água: 2 litros em `08/11/2025`
* Sono: 7,5 horas em `08/11/2025`

### 2) Listar registros

Saída esperada:

```text
=== Listar Registros ===
#   Data        Tipo       Valor
1   08/11/2025  Exercício  30 min
2   08/11/2025  ÁGUA       2,00 L
3   08/11/2025  SONO       7,50 h
```

### 3) Estatísticas

Saída esperada:

```text
=== Estatísticas ===
Exercício -> Soma: 30 min | Média: 30,00 min
ÁGUA -> Soma: 2,00 L | Média: 2,00 L
SONO -> Soma: 7,50 h | Média: 7,50 h
```

