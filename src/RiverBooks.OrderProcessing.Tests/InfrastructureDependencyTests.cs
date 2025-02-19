using ArchUnitNET.Domain;
using ArchUnitNET.Fluent;
using ArchUnitNET.Fluent.Syntax.Elements.Types;
using ArchUnitNET.Loader;
using ArchUnitNET.xUnit;

namespace RiverBooks.OrderProcessing.Tests;

public class InfrastructureDependencyTests(ITestOutputHelper outputHelper)
{
  private static readonly Architecture Architecture = new ArchLoader()
    .LoadAssemblies(typeof(AssemblyInfo).Assembly)
    .Build();

  [Fact]
  public void DomainTypesShouldNotReferenceInfrastructure()
  {
    GivenTypesConjunctionWithDescription domainTypes = ArchRuleDefinition.Types().That()
      .ResideInNamespace("RiverBooks.OrderProcessing.Domain.*", useRegularExpressions: true)
      .As("OrderProcessing Domain Types");

    GivenTypesConjunctionWithDescription infrastructureTypes = ArchRuleDefinition.Types().That()
      .ResideInNamespace("RiverBooks.OrderProcessing.Infrastructure.*", useRegularExpressions: true)
      .As("OrderProcessing Infrastructure Types");

    TypesShouldConjunction rule = domainTypes.Should().NotDependOnAny(infrastructureTypes);

    PrintTypes(domainTypes, infrastructureTypes);

    rule.Check(Architecture);
  }

  /// <summary>
  /// Used for debugging purposes
  /// </summary>
  private void PrintTypes(GivenTypesConjunctionWithDescription domainTypes,
    GivenTypesConjunctionWithDescription infrastructureTypes)
  {
    foreach (var domainClass in domainTypes.GetObjects(Architecture))
    {
      outputHelper.WriteLine($"Domain Type: {domainClass.FullName}");
      foreach (var dependency in domainClass.Dependencies)
      {
        IType targetType = dependency.Target;
        if (infrastructureTypes.GetObjects(Architecture).Any(infraClass => Equals(infraClass, targetType)))
        {
          outputHelper.WriteLine($"  Depends on Infrastructure: {targetType.FullName}");
        }
      }
    }

    foreach (var iType in infrastructureTypes.GetObjects(Architecture))
    {
      outputHelper.WriteLine($"Infrastructure Type: {iType.FullName}");
    }
  }
}
