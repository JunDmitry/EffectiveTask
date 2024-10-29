using EffectiveMobileTestTask;
using EffectiveMobileTestTask.Filtering;
using EffectiveMobileTestTask.Logging;
using Serilog;
using System.Configuration;
using System.Text.Json;

internal class Program
{
    private static readonly string _testDataPath = @"I:\Code_Source\EffectiveMobileTestTask\EffectiveMobileTestTask\data\testData.json";

    private static string[] _districts = [];

    private static ArgumentData _argumentData = ArgumentData.GetDefault();

    /*
     * Прошу прощения, за некрасивый и неэффективный код, так как нехватило времени
     * из-за отсутствия свободного времени.
     * Среди 7 дней было свободно только 2 дня и это все что успел.
     * Возможно в некоторый местах перемудрил, а где-то написал вообще не очень. Есть много лишнего.
     * Однозначно много ошибок, да и логирование с конфигурациями совсем не знаю, тут уж как есть.
     * Написал, как успел изучить. Фильтрацию можно было реализовать очень многими способами
     * и реализовал через паттерн, но для такой маленькой фильтрации это перебор.
     * Если не пройду, то будет не плохо, если будет чоть какой-нибудь фидбек.
     * Даже если и не будет, то все равно спасибо за потраченное драгоценное время.
     */

    private static void Main(string[] args)
    {
        bool isSuccess = args.Length != 0;
        if (args.Length != 0 && !ArgumentData.TryParse(args, out _argumentData))
            isSuccess = false;
        else if (!isSuccess && !ArgumentData.TryParse(ConfigurationManager.AppSettings, out _argumentData))
        {
            Console.WriteLine("Invalid input arguments and configuration");
            return;
        }

        Result<Order[]> result = ParseData<Order[]>(_testDataPath);
        if (!result.IsSuccess || result.Value == null)
        {
            Console.WriteLine(result.Exception?.Message ?? "Invalid data source file");
            return;
        }
        if (!TryParseDistricts())
            return;

        ILogger<Program> logger = default!;
        if (!TryAction(() => logger = ConfigurationLogger.CreateFileLogger<Program>(_argumentData.LogPath),
            $"Error on reading or access to log file: {_argumentData.LogPath}"))
            return;

        IRule<Order> filter = OrderRules
            .Unary(v => v != null)
            .AndBetweenDeliveryTime(_argumentData.FirstDeliveryTime, AddMinutes(_argumentData.FirstDeliveryTime, 30))
            .AndWithDistrictName(_districts[Random.Shared.Next(0, _districts.Length)]);
        FilterAndWriteToFile(filter, result.Value, logger);

        Log.CloseAndFlush();
    }

    private static Result<T> ParseData<T>()
    {
        return ParseData<T>(_testDataPath);
    }

    private static Result<T> ParseData<T>(string path)
    {
        T? data = default;
        Exception? exception = null;
        try
        {
            using FileStream rawData = File.OpenRead(path);
            data = JsonSerializer.Deserialize<T>(rawData);
        }
        catch (JsonException ex)
        {
            exception = new ApplicationException($"Invalid Json data for type {typeof(T)}", ex);
        }
        catch (Exception ex)
        {
            exception = new ApplicationException($"Error reading or access the file: {path}", ex);
        }

        return new Result<T>
        {
            Value = data,
            Exception = exception,
            IsSuccess = exception == null
        };
    }

    private static void FilterAndWriteToFile<T>(IRule<T> filter, IEnumerable<T> collection, ILogger<Program> logger)
    {
        StreamWriter outputFile = default!;
        if (!TryAction(() => outputFile = new StreamWriter(_argumentData.OutputPath, true),
            $"Error on reading or access to output file: {_argumentData.OutputPath}"))
            return;
        foreach (T item in collection)
        {
            if (filter.Should(item))
            {
                logger.LogInformation($"Element is Match condition: {item}");
                if (!TryAction(() => outputFile.WriteLine(JsonSerializer.Serialize(item)),
                    $"Error on writing to output file: {_argumentData.OutputPath}, Value: {item}"))
                    break;
            }
            else
                logger.LogInformation($"This Element doesn't Match: {item}");
        }
        outputFile.Flush();
        outputFile.Close();
    }

    private static bool TryParseDistricts()
    {
        return TryAction(() =>
        {
            using FileStream stream = File.OpenRead(@"I:\Code_Source\EffectiveMobileTestTask\EffectiveMobileTestTask\data\districts.json");
            _districts = JsonSerializer.Deserialize<string[]>(stream) ?? [];
        }, "Error on reading or access to file with districts");
    }

    private static string AddMinutes(string dataTime, int count)
    {
        string[] data = dataTime.Split(new char[] { '-', ' ', '\t', '\n', ':' }, StringSplitOptions.RemoveEmptyEntries);
        int year = int.Parse(data[0]);
        int month = int.Parse(data[1]);
        int day = int.Parse(data[2]);
        int hour = int.Parse(data[3]);
        int minute = int.Parse(data[4]);
        int second = int.Parse(data[5]);

        return new DateTime(year, month, day, hour, minute, second)
            .AddMinutes(count)
            .ToString("yyyy-MM-dd HH:mm:ss");
    }

    private static bool TryAction(Action action, string message)
    {
        try
        {
            action();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error on action: {message}\n" + ex.Message);
            return false;
        }

        return true;
    }
}