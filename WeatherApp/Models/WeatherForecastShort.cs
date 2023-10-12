namespace WeatherApp.Models;

public class WeatherForecastShort
{
    public string? DataTimeText { get; private set; }
    public double? Temperature { get; private set; }
    public double? WindSpeed { get; private set; }
    public string? WeatherMain { get; private set; }
    public string? WeatherDescription { get; private set; }

    public WeatherForecastShort(string? dataTimeText, double? temperature, 
        double? windSpeed, string? weatherMain, string? weatherDescription)
    {
        DataTimeText = dataTimeText;
        Temperature = temperature;
        WindSpeed = windSpeed;
        WeatherMain = weatherMain;
        WeatherDescription = weatherDescription;
    }
    
    public override string ToString()
    {
        return $"DataTime: {DataTimeText}; Temperature: {Temperature}; Wind speed: {WindSpeed}; Weather main: {WeatherMain}; Weather description: {WeatherDescription};";
    }
}