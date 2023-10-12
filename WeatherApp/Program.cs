using WeatherApp.Models;

namespace WeatherApp
{
    public class Program
    {
        private static ApiHandler? _apiHandler;
        private static DBManager? _dbManager;
        
        public static async Task Main(string[] args)
        {
            _apiHandler = new ApiHandler();
            _dbManager = new DBManager();
            _dbManager.CreateTableIfNotExist();
            
            await HandleInput();
        }

        private static async Task HandleInput()
        {
            while (true)
            {
                string input = Console.ReadLine();

                switch (input)
                {
                    case "get":
                        GetData();
                        break;
                    case "print":
                        PrintData();
                        break;
                    case "exit":
                        return;
                    default:
                        break;
                }
            }
        }

        private static void GetData()
        {
            WeatherForecast wf = _apiHandler.GetData().Result;
            _dbManager.InsertData(new WeatherForecastShort(
                wf.list[0].dt_txt, 
                wf.list[0].main.temp, 
                wf.list[0].wind.speed, 
                wf.list[0].weather[0].main, 
                wf.list[0].weather[0].description
                ));
        }

        private static void PrintData()
        {
            List<WeatherForecastShort> data = _dbManager.ReadData();
            foreach (var dataItem in data)
            {
                Console.WriteLine(dataItem);
            }
        }
    }
}