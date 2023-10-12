using System.Data.SQLite;
using WeatherApp.Models;

namespace WeatherApp
{
    public class DBManager
    {
        private const string ConnectionString = "Data Source=database.db; Version = 3; New = True; Compress = True;";

        private readonly SQLiteConnection _sqLiteConnection;

        public DBManager()
        {
            _sqLiteConnection = new SQLiteConnection(ConnectionString);
            try
            {
                _sqLiteConnection.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public void CreateTableIfNotExist()
        {
            SQLiteCommand sqLiteCommand = _sqLiteConnection.CreateCommand();
            sqLiteCommand.CommandText = "CREATE TABLE IF NOT EXISTS WeatherForecast (" +
                                        "dt_text VARCHAR(20), " +
                                        "temp NUMERIC, " +
                                        "wind_speed NUMERIC, " +
                                        "weather_main TEXT, " +
                                        "weather_description TEXT" +
                                        ")";
            sqLiteCommand.ExecuteNonQuery();
        }

        public void InsertData(WeatherForecastShort wfs)
        {
            string insertQuery = $"INSERT INTO WeatherForecast " +
                                 $"(dt_text, temp, wind_speed, weather_main, weather_description) " +
                                 $"VALUES(@dtText, @temp, @windSpeed, @weatherMain, @weatherDescription" +
                                 $")";
            using (SQLiteCommand command = new SQLiteCommand(insertQuery, _sqLiteConnection))
            {
                command.Parameters.AddWithValue("@dtText", wfs.DataTimeText);
                command.Parameters.AddWithValue("@temp", wfs.Temperature);
                command.Parameters.AddWithValue("@windSpeed", wfs.WindSpeed);
                command.Parameters.AddWithValue("@weatherMain", wfs.WeatherMain);
                command.Parameters.AddWithValue("@weatherDescription", wfs.WeatherDescription);
                command.ExecuteNonQuery();
            }
        }

        public List<WeatherForecastShort> ReadData()
        {
            SQLiteCommand sqLiteCommand = _sqLiteConnection.CreateCommand();
            sqLiteCommand.CommandText = "SELECT * FROM WeatherForecast";
            SQLiteDataReader sqLiteDataReader = sqLiteCommand.ExecuteReader();
            List<WeatherForecastShort> data = new List<WeatherForecastShort>();
            while (sqLiteDataReader.Read())
            {
                data.Add(new WeatherForecastShort(
                    sqLiteDataReader.GetString(0),
                    sqLiteDataReader.GetDouble(1),
                    sqLiteDataReader.GetDouble(2),
                    sqLiteDataReader.GetString(3),
                    sqLiteDataReader.GetString(4)
                    ));
            }

            return data;
        }
    }
}