namespace ConsimpleWeb.Extensions;

using Consimple.Infrastructure.Persistence.Initializers.Interfaces;

public static class ApplicationBuilderExtensions
{
    public async static Task UseDatabaseInitializerAsync(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var initializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
        await initializer.InitializeAsync();
    }
}