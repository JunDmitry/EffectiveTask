using Serilog;
using Serilog.Formatting.Json;

namespace EffectiveMobileTestTask.Logging;

public static class ConfigurationLogger
{
    private static readonly Dictionary<Type, object> _loggers = new();

    private static readonly string _defaultLogPath = @"I:\Code_Source\EffectiveMobileTestTask\EffectiveMobileTestTask\data\";

    public static ILogger<T> CreateFileLogger<T>(string path)
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .WriteTo.File(new JsonFormatter(), path)
            .CreateLogger();

        using ILoggerFactory loggerFactory = LoggerFactory.Create(builder => builder.AddSerilog());
        ILogger<T> logger = loggerFactory.CreateLogger<T>();
        _loggers[typeof(T)] = logger;

        return logger;
    }

    public static ILogger<T> GetLogger<T>()
    {
        Type type = typeof(T);
        if (!_loggers.ContainsKey(type))
            CreateFileLogger<T>(_defaultLogPath + type.Name);
        return (ILogger<T>)_loggers[type];
    }
}