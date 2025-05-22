namespace Consimple.Infrastructure.Persistence.Initializers.Interfaces;

public interface IDbInitializer
{
    Task InitializeAsync();
}