using SQLite;
//Se crea el modelo que tiene el objeto, que con MySqLite se utiliza directamente como una tabla de una BD
namespace AppFuelStations.Models
{
    [Table("FuelStation")]
    public class FuelStationModel
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Brand { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }
        public double GreenPrice { get; set; }
        public double RedPrice { get; set; }
        public double DieselPrice { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
