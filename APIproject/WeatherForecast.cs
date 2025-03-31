namespace LU2_project;

public class WeatherForecast
{
    /*public DateOnly Date { get; set; }*/
    public string UserEmail { get; set; }

    public string PassWord { get; set; }
    public Guid UserGuid { get; internal set; }

    //public int TemperatureC { get; set; }

    //public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

    /*public string? Summary { get; set; }*/
}
