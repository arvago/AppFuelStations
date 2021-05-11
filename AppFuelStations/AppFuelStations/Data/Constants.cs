using System;
using System.IO;

namespace AppFuelStations.Data
{
    public class Constants
    {
        //CONSTANTE PARA ABRIR NUESTRO ARCHIVO SQLite en modo lectura, escritura, crear y cache compartido.
        public const SQLite.SQLiteOpenFlags Flags = SQLite.SQLiteOpenFlags.ReadWrite |
                                                    SQLite.SQLiteOpenFlags.Create |
                                                    SQLite.SQLiteOpenFlags.SharedCache;

        public static string DatabasePath
        {
            get
            {
                //FORMULA LA RUTA DONDE GENERAREMOS Y ACCEDEREMOS AL ARCHIVO DE SQLite PARA ESTA APP
                string basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                return Path.Combine(basePath, "AppFuelStation.db3");
            }
        }
    }
}
