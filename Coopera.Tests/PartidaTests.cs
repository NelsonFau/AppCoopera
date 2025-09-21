using Coopera.Controllers;

namespace Coopera.Tests;

public class PartidaTests
{
    [Fact]
    public void CalcularMetasTotales_ConSemillaFijo_DebeSerElMismo()
    {
        PartidaController controller = new PartidaController(null); // al usar sql no pdesmo instanciar el contexto 
        bool esV1 = true;
        int totalMax = 100;
        int semilla = 123;
        //Al metodo ser privado, utilizamos 'reflection'
        var (madera, piedra, comida) = controller
            .GetType() //btiene el tipo del objeto (PartidaController).
            .GetMethod("CalcularMetasTotales", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)  //busca un método privado de instancia.
            .Invoke(controller, new object[] { esV1, semilla, totalMax }) as ValueTuple<int, int, int>? ?? (0, 0, 0);   //llama al método pasando los parámetros.

        Assert.Equal((37, 34, 29), (madera, piedra, comida)); // resultado fijo con la semilla en valor de instancia 123
    }


    [Theory]
    [InlineData(true, 100)]
    [InlineData(true, 90)]
    [InlineData(true, 80)]
    [InlineData(true, 70)]
    public void CalcularMetasTotales_CalcularQueElTotalMaxSeIgualASumatoriasDeRecursos(bool esV1, int totalMax)
    {
        PartidaController controller = new PartidaController(null);

        var (madera, piedra, comida) = controller
            .GetType()
            .GetMethod("CalcularMetasTotales", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .Invoke(controller, new object[] { esV1, null, totalMax }) as ValueTuple<int, int, int>? ?? (0, 0, 0);

        Assert.Equal(totalMax, madera + piedra + comida);
    }
}

