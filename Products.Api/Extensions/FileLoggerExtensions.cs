using Products.Providers;

namespace Products.Api.Extensions;

public static class FileLoggerExtensions
{
    public static ILoggingBuilder AddFile(this ILoggingBuilder factory, string filePath)
    {
        factory.AddProvider(new FileLoggerProvider(filePath));

        return factory;
    }
}