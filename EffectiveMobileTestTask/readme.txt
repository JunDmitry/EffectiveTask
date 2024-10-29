=================================Настройка и запуск приложения=========================
*************************************************Описание*********************************************************************
Это консольное приложения для фильтрации заказов доставки по району и времени доставки. 
Тестовые данный берутся из файла "testData.json" в папке data. Количество записей: 1_000_000.
В результате работы программы сохраняются логи по фильтрации данных и результат фильтрации данных.
******************************************************Исходные данные******************************************************
Заказ представлен в валидном JSON формате
Диапазон весов: (0 - 15)
Диапазон дат в тестовых данных: [2022 - 2023]-[1 - 12]-[1 - 31] [0 - 23]:[0 - 59]:[0 - 59]
Список районов(data\districts.json): 
- Amur Oblast
- Arkhangelsk Oblast
- Chelyabinsk Oblast
- Ivanovo Oblast
- Kaluga Oblast
- Kostroma Oblast
- Kurgan Oblast
- Leningrad Oblast
- Moscow City
- Moscow Oblast
- Murmansk Oblast
- Nizhny Novgorod Oblast
- Novgorod Oblast
- Orenburg Oblast
- Oryol Oblast
- Penza Oblast
- Ryazan Oblast
- Sakhalin Oblast
- Saratov Oblast
- Sevastopol City
- Smolensk Oblast
- Tambov Oblast
- Tver Oblast
- Tyumen Oblast
- Ulyanovsk Oblast
- Vladimir Oblast
- Volgograd Oblast
- Vologda Oblast
- Voronezh Oblast
- Yaroslavl Oblast
****************************************************Параметры запуска*************************************************
4 параметра командной строки:
- district: string
- firstDeliveryTime: yyyy-MM-dd HH:mm:ss
- logPath: path to logs file
- outputPath: path to ouput file
Запуск без аргументов реализует конфигурацию по умолчанию
****************************************************Запуск приложения*************************************************
dotnet build
example: dotnet run "Penza Oblast" "2022-10-23 16:29:18" "testLog.json" "testOutput.json"
- district = Penza Oblast
- firstDeliveryTime = 2022-10-23 16:29:18
- logPath = testLog.json
- outputPath = testOutput.json

Логирование   
Все действия связанные с фильтрацией данных и их результатом записываются в файл логов указанный при запуске приложения
