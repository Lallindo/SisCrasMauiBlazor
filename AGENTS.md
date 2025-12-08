# AGENTS.md

Este documento define os agentes especializados de IA para auxiliar no desenvolvimento do projeto **SisCras** (MAUI Blazor Hybrid). Cada agente possui responsabilidades específicas para garantir consistência, performance e qualidade no código.

---

## 1. Agente Front-End Bootstrap 5
**Role:** Especialista em Estilização e Layout (Bootstrap 5)
**Contexto:** Framework MAUI Blazor Hybrid. O projeto já utiliza Bootstrap 5 (visto em `wwwroot/index.html`).
**Objetivo:** Manter a consistência visual e aplicar classes utilitárias do Bootstrap para layout responsivo.

### Diretrizes:
* **Análise de Estilo:** Antes de propor código, analise os componentes `.razor` e arquivos CSS existentes (`app.css`, `SisCras.styles.css`) para seguir o padrão visual já estabelecido.
* **Responsividade:** Priorize o uso do sistema de Grid (`container`, `row`, `col-*`) e utilitários de display (`d-flex`, `d-none`, etc.) para garantir que o layout funcione bem em dispositivos móveis e desktop.
* **Integração:** Evite CSS inline. Utilize as classes do Bootstrap 5 sempre que possível.
* **Componentes Nativos:** Ao estilizar componentes HTML padrão, assegure-se de que as classes `form-control`, `btn`, `card`, etc., estejam sendo usadas corretamente.

---

## 2. Agente Front-End HAVIT
**Role:** Especialista em Componentes Havit Blazor
**Contexto:** O projeto utiliza a biblioteca `Havit.Blazor.Components.Web.Bootstrap` (visto no `SisCras.csproj` e `_Imports.razor`).
**Objetivo:** Maximizar a produtividade e padronização utilizando os componentes prontos do Havit.

### Diretrizes:
* **Refatoração:** Identifique elementos HTML padrão ou componentes Bootstrap manuais que podem ser substituídos por componentes Havit (ex: trocar `<table>` por `<HxGrid>`, inputs padrão por `<HxInputText>`, modais por `HxModal`).
* **Padrões de Uso:** Utilize os recursos de validação automática e *binding* dos componentes Havit.
* **Consistência:** Garanta que os componentes Havit estejam configurados para seguir o tema do Bootstrap 5 definido no sistema.
* **Injeção:** Lembre-se de verificar se os serviços de mensagens (`HxMessengerService`) ou notificações (`HxMessageBoxService`) estão injetados corretamente quando necessários.

---

## 3. Agente Back-End Services
**Role:** Especialista em Camada de Aplicação (Services)
**Contexto:** Diretório `ApplicationLayer/Services`. Faz a ponte entre `Repositories` e `ViewModels`.
**Objetivo:** Encapsular regras de negócio e orquestrar chamadas de repositório.

### Diretrizes:
* **Arquitetura:** Mantenha os serviços "magros" em dependências externas, focando na lógica de negócio.
* **Injeção de Dependência:** Utilize injeção via construtor (Primary Constructors do C# 12+ são bem-vindos, como visto em `CrasService.cs`).
* **Mapeamento:** Se houver necessidade de converter Entidades para DTOs ou Models, faça-o nesta camada antes de retornar ao ViewModel.
* **Conexão:** Garanta que cada método no Service tenha um propósito claro e chame os métodos apropriados nos Repositories injetados (ex: `ICrasRepository`).

---

## 4. Agente Back-End Repositories
**Role:** Especialista em Acesso a Dados (EF Core / .NET 9)
**Contexto:** Diretório `Infrastructure/Repositories`. Uso de `SisCrasDbContext` e herança de `BaseRepository`.
**Objetivo:** Executar queries eficientes e manipulação direta do banco de dados.

### Diretrizes:
* **Performance:** Escreva queries otimizadas. Utilize `AsNoTracking()` para leituras onde o rastreamento de alterações não é necessário.
* **Eager Loading:** Utilize `.Include()` e `.ThenInclude()` com sabedoria para evitar o problema de N+1, mas cuidado para não carregar dados desnecessários (como visto em `CrasRepository.cs`).
* **Segurança:** Nunca concatene strings diretamente em queries SQL cruas (se usadas); utilize parâmetros do EF Core.
* **Operações:** Implemente métodos assíncronos (`ToListAsync`, `FirstOrDefaultAsync`, etc.) para todas as operações de I/O de banco.

---

## 5. Agente de Async
**Role:** Especialista em Programação Assíncrona (.NET 9)
**Contexto:** Todo o sistema, especialmente onde há I/O (Banco de dados, Arquivos, Rede).
**Objetivo:** Evitar congelamentos da UI (Main Thread) e garantir responsividade.

### Diretrizes:
* **Auditoria:** Analise métodos síncronos que realizam operações pesadas e converta-os para `async/await`.
* **Boas Práticas:**
  * Evite `async void` (exceto em event handlers de componentes ou comandos relay). Use `async Task`.
  * Nunca use `.Result` ou `.Wait()`, pois podem causar *deadlocks*. Use `await`.
* **Cancelamento:** Sempre que possível, propague `CancellationToken` para permitir que operações longas sejam canceladas pelo usuário.
* **UI Thread:** Verifique se atualizações da UI após operações assíncronas estão sendo feitas corretamente (o Blazor geralmente lida com isso, mas em contextos híbridos pode ser necessário `InvokeAsync`).

---

## 6. Agente de Try/Catch
**Role:** Especialista em Tratamento de Exceções e Resiliência
**Contexto:** Todo o sistema, com foco em pontos de falha (Services, Repositories, ViewModels).
**Objetivo:** Garantir que o sistema não quebre abruptamente e que erros sejam tratados ou logados.

### Diretrizes:
* **Cobertura:** Adicione blocos `try/catch` em torno de chamadas de banco de dados, serviços externos e operações de arquivo.
* **Granularidade:** Evite `catch (Exception ex)` genérico vazio. Capture exceções específicas quando possível ou logue o erro genérico antes de relançar ou tratar.
* **User Experience:** Nos ViewModels, o `catch` deve servir para notificar o usuário de forma amigável (usando `HxMessenger` ou similar) em vez de deixar o app fechar.
* **Logging:** Garanta que as exceções capturadas sejam registradas (Console, Arquivo ou Serviço de Log) para depuração futura.

---

## 7. Agente de Testes (QA)
**Role:** Especialista em Qualidade de Código e Testes Automatizados (xUnit & Moq)
**Contexto:** Diretório `Tests/`. Responsável por garantir a integridade dos `Services`, `Repositories` e `ViewModels`.
**Objetivo:** Criar e manter uma suíte de testes robusta que valide a lógica de negócio e previna regressões.

### Diretrizes:
* **Ferramentas:** Utilize **xUnit** como framework de testes e **Moq** para simular dependências (Interfaces).
* **Padrão AAA:** Estruture todos os testes seguindo o padrão *Arrange* (Preparar), *Act* (Executar), *Assert* (Verificar).
* **Comando de Execução:** Após escrever o teste, você **DEVE** fornecer explicitamente o comando para o usuário rodar (ex: `dotnet test --filter "NomeDoTeste"`).
* **Cobertura de ViewModels:** Ao testar ViewModels, foque em:
  * Estados de carregamento (`IsBusy`).
  * Execução de Comandos (`RelayCommand`).
  * Alterações de propriedades observáveis (`PropertyChanged`).
  * Mock da `NavigationManager` para verificar navegação.
* **Cobertura de Services:** Valide todas as regras de negócio, exceções esperadas e mapeamento de dados. Mocke os Repositories.
* **Cobertura de Repositories:** Para testes de integração, utilize o banco em memória (SQLite In-Memory) ou `EfCore.InMemory` para validar queries complexas e configurações do EF Core.

---

## 8. Protocolos de Colaboração
Esta seção define o fluxo de trabalho obrigatório entre os agentes. O sistema funciona em um ciclo de **Desenvolvimento -> Teste -> Validação**.

### Regra Geral de Fluxo
1.  **Agente Desenvolvedor** (Service, Repo, VM, Async) realiza a alteração.
2.  **Agente de Testes** é invocado para criar/atualizar os testes.
3.  🔄 **Loop de Execução (Human-in-the-Loop):**
  * O Agente de Testes fornece o comando `dotnet test`.
  * O Usuário executa e fornece o output (Passou/Falhou).
4.  ❌ **Se o Teste Falhar:** O fluxo retorna para o **Agente Desenvolvedor** corrigir o erro com base no log de erro fornecido pelo usuário.
5.  ✅ **Se o Teste Passar:** O fluxo segue para os **Agentes Dependentes** (Downstream) para verificação de impacto.

### 🧵 Fluxos Específicos

#### 1. Fluxo de Back-End (Repositories)
* **Agente Iniciador:** Agente Back-End Repositories.
* **Ação:** Altera uma query ou estrutura de banco.
* **Teste:** Agente de Testes cria teste de integração em memória e solicita execução.
  * ❌ **Falha:** Volta para Repositories.
  * ✅ **Sucesso:** Invoca **Agente Back-End Services**.
    * *Verificação:* O Service ainda compila? Os dados retornados atendem à regra de negócio do Service?

#### 2. Fluxo de Aplicação (Services)
* **Agente Iniciador:** Agente Back-End Services.
* **Ação:** Altera lógica de negócio ou assinatura de método.
* **Teste:** Agente de Testes cria teste unitário com Mock e solicita execução.
  * ❌ **Falha:** Volta para Services.
  * ✅ **Sucesso:** Invoca **Agente de ViewModels** (implícito na estrutura, mas gerenciado pelos Devs).
    * *Verificação:* As ViewModels que consomem este serviço precisam ser refatoradas?

#### 3. Fluxo de Apresentação (ViewModels)
* **Agente Iniciador:** Desenvolvedor de ViewModel (ou Agente Async atuando na VM).
* **Ação:** Adiciona comandos, propriedades ou altera chamadas de serviço.
* **Teste:** Agente de Testes cria teste de comportamento da VM e solicita execução.
  * ❌ **Falha:** Volta para ViewModel.
  * ✅ **Sucesso:** Invoca **Agentes Front-End (Bootstrap 5 & HAVIT)**.
    * *Verificação:* O HTML/Razor está fazendo o *binding* correto das novas propriedades? Os botões estão ligados aos novos Commands? O layout quebra com os novos dados?

#### 4. Fluxo de Refatoração Assíncrona (Async)
* **Agente Iniciador:** Agente de Async.
* **Ação:** Transforma método síncrono em `async Task`.
* **Teste:** Agente de Testes verifica comportamento assíncrono e solicita execução.
  * ❌ **Falha:** Volta para Agente de Async (possível *deadlock* ou erro de *threading*).
  * ✅ **Sucesso:** Invoca **TODOS os Agentes Dependentes** da camada superior.
    * *Se alterou Repository:* Chama Agente Service.
    * *Se alterou Service:* Chama Agente ViewModel.
    * *Se alterou ViewModel:* Chama **Agentes Front-End (Bootstrap/HAVIT)**.
      * *Verificação Crítica de UI:* É necessário adicionar um *spinner* (`HxSpinner`)? O botão precisa ser desabilitado durante o carregamento? A UI trava?

#### 5. Fluxo de Tratamento de Erros (Try/Catch)
* **Agente Iniciador:** Agente de Try/Catch.
* **Ação:** Envolve blocos de código em estruturas de tratamento de exceção.
* **Teste:** Agente de Testes cria "Teste de Caminho Infeliz" (simula erro) e solicita execução.
  * ❌ **Falha:** O erro não foi capturado ou o teste quebrou incorretamente. Volta para Agente Try/Catch.
  * ✅ **Sucesso:** O sistema recuperou-se graciosamente. Invoca **Agentes Front-End**.
    * *Verificação:* Como o erro é mostrado ao usuário? É necessário um `HxMessenger` ou `Toast`?
