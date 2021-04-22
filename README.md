## Testes de unidade
Estudos sobre Testes de unidade

Neste repositório foi criado a aplicação fictícia BancoX, para ser criado vários cenários de testes de unidades com as regras de negócio do banco.
Os testes foram criados com as classes ContaCorrente e Investimento, utilizando algumas ferramentas de testes de unidade abaixo para facilitar o trabalho.

A classe ContaCorrenteTest, foi criada utilizando o framework Microsoft.VisualStudio.TestTools.UnitTesting as demais classes foram utilizadas o framework  XUnit conforme descrito no pacote abaixo. Neste caso, é possível perceber e verificar as principais diferenças no testes quando se utilizado um framework e outro.

**Links de documentação e ferramentas**

- 📑 Mocks, stubs and Fakes (Martin Fowler): (https://martinfowler.com/articles/mocksArentStubs.html)
- 📑 Mock Documentation: (https://documentation.help/Moq/8FE2812.htm)
- 📑 AutoFixture Documentation: (https://github.com/AutoFixture/AutoFixture)
- 📑 NBuilder Documentation: (https://github.com/nbuilder/nbuilder)
- 📑 Bogus Documentation: (https://github.com/bchavez/Bogus)
- 📑 MSTest Documentation: (https://docs.microsoft.com/en-us/dotnet/api/microsoft.visualstudio.testtools.unittesting?redirectedfrom=MSDN&view=mstest-net-1.3.2)
- 📑 Fluent Assertions Documentation: (https://fluentassertions.com/)
- :wrench: NBuilder: (https://github.com/nbuilder/nbuilder)
- :wrench: ReportGenerator: (https://github.com/danielpalme/ReportGenerator)
- :wrench: XunitCodeSnippets: (https://marketplace.visualstudio.com/items?itemName=jsakamoto.xUnitCodeSnippets)

**Pacotes utilizados**

- Moq
- NBuilder
- MSTest.TestAdapter
- MSTest.TestFramework
- Install-Package xunit -Version 2.4.1
- Install-Package xunit.runner.visualstudio -Version 2.4.1
- Install-Package Bogus
- Install-package MOQ.automock
- Install-package FluentAssertions
- Install-Package Moq.AutoMock -Version 2.0.1
- Install-Package FluentValidation -Version 9.1.3

