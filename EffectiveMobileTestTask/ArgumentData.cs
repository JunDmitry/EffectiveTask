using System.Collections.Specialized;

namespace EffectiveMobileTestTask;

public class ArgumentData
{
    public string District { get; private set; } = "*";

    public string FirstDeliveryTime { get; private set; } = "1900-01-01 00:00:00";

    public string LogPath { get; private set; } = @"I:\Code_Source\EffectiveMobileTestTask\EffectiveMobileTestTask\data\logs.json";

    public string OutputPath { get; private set; } = @"I:\Code_Source\EffectiveMobileTestTask\EffectiveMobileTestTask\data\output.json";

    public static bool TryParse(NameValueCollection arguments, out ArgumentData result)
    {
        result = GetDefault();
        if (!arguments.HasKeys())
            return false;
        return TryParse(arguments.AllKeys
            .Select(key => arguments[key])
            .Where(val => val != null)
            .Cast<string>().ToArray(), out result);
    }

    public static bool TryParse(string[] args, out ArgumentData result)
    {
        result = new ArgumentData();
        if (args.Length < 4)
        {
            Console.WriteLine("Required parameters(name: format): {district: string} {firstDeliveryTime: yyyy-MM-dd HH:mm:ss} {logPath: path} {outputPath: path}");
            return false;
        }
        result.District = args[0];
        result.FirstDeliveryTime = args[1];

        if (!Order.DateTimeRegex.IsMatch(result.FirstDeliveryTime))
        {
            Console.WriteLine($"Invalid {nameof(result.FirstDeliveryTime)}: {result.FirstDeliveryTime}. Format: yyyy-MM-dd HH:mm:ss");
            return false;
        }

        result.LogPath = args[2];
        result.OutputPath = args[3];
        return true;
    }

    public static ArgumentData GetDefault()
    {
        return new ArgumentData();
    }
}