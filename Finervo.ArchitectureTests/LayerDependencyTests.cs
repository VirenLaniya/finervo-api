using NetArchTest.Rules;

namespace Finervo.ArchitectureTests;

public sealed class LayerDependencyTests
{
    [Fact]
    public void Shared_must_not_depend_on_any_application_layer()
    {
        AssertHasNoDependency(
            typeof(Finervo.Shared.AssemblyMarker).Assembly,
            "Finervo.Contracts",
            "Finervo.Core",
            "Finervo.Application",
            "Finervo.Infrastructure",
            "Finervo.API");
    }

    [Fact]
    public void Contracts_must_not_depend_on_core_or_runtime_layers()
    {
        AssertHasNoDependency(
            typeof(Finervo.Contracts.AssemblyMarker).Assembly,
            "Finervo.Core",
            "Finervo.Application",
            "Finervo.Infrastructure",
            "Finervo.API");
    }

    [Fact]
    public void Core_must_not_depend_on_contracts_or_runtime_layers()
    {
        AssertHasNoDependency(
            typeof(Finervo.Core.AssemblyMarker).Assembly,
            "Finervo.Contracts",
            "Finervo.Application",
            "Finervo.Infrastructure",
            "Finervo.API");
    }

    [Fact]
    public void Application_must_not_depend_on_infrastructure_or_api()
    {
        AssertHasNoDependency(
            typeof(Finervo.Application.AssemblyMarker).Assembly,
            "Finervo.Infrastructure",
            "Finervo.API");
    }

    [Fact]
    public void Infrastructure_must_not_depend_on_api()
    {
        AssertHasNoDependency(
            typeof(Finervo.Infrastructure.AssemblyMarker).Assembly,
            "Finervo.API");
    }

    private static void AssertHasNoDependency(
        System.Reflection.Assembly sourceAssembly,
        params string[] forbiddenDependencies)
    {
        var result = Types
            .InAssembly(sourceAssembly)
            .ShouldNot()
            .HaveDependencyOnAny(forbiddenDependencies)
            .GetResult();

        var failures = result.FailingTypeNames is null
            ? string.Empty
            : string.Join(Environment.NewLine, result.FailingTypeNames);

        Assert.True(
            result.IsSuccessful,
            $"Assembly '{sourceAssembly.GetName().Name}' has forbidden dependencies:{Environment.NewLine}{failures}");
    }
}