using System.Runtime.CompilerServices;

namespace EffectiveMobileTestTask.Logging;

public static partial class LoggerExtensions
{
    public static void Log(this ILogger logger, LogLevel logLevel,
        [InterpolatedStringHandlerArgument("logger", "logLevel")] ref StructuredLoggingInterpolatedStringHandler handler)
    {
        if (handler.IsEnabled)
        {
            var (template, arguments) = handler.GetTemplateAndArguments();
            logger.Log(logLevel, template, arguments);
        }
    }
}