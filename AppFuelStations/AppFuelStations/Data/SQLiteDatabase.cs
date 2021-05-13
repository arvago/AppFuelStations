using AppFuelStations.Extensions;
using AppFuelStations.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppFuelStations.Data
{
    public class SQLiteDatabase
    {
        //Lazy NOS AYUDA PARA QUE AL CREAR-CONECTAR A NUESTRA BASE DE DATOS DE SQLite NO SE BLOQUEE LA APP
        static readonly Lazy<SQLiteAsyncConnection> LazyInitializer = new Lazy<SQLiteAsyncConnection>(() =>
        {
            return new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        });

        static SQLiteAsyncConnection Connection => LazyInitializer.Value;

        static bool IsInitialized = false;

        async Task InitializeAsync()
        {
            if (!IsInitialized)
            {
                if (!Connection.TableMappings.Any(m => m.MappedType.Name == typeof(FuelStationModel).Name))
                {
                    //CREAMOS UNA TABLA CON MODELO FUELSTATIONMODEL
                    await Connection.CreateTablesAsync(CreateFlags.None, typeof(FuelStationModel)).ConfigureAwait(false);
                    IsInitialized = true;
                }
            }
        }

        public SQLiteDatabase()
        {
            InitializeAsync().SafeFireAndForget(false);
        }

        public void OnInitializeError()
        {
            throw new NotImplementedException();
        }

        //METODO PARA OBTENER TODAS LAS GASOLINERAS GUARDADAS EN SQLITE
        public Task<List<FuelStationModel>> GetAllFuelStationAsync()
        {
            return Connection.Table<FuelStationModel>().ToListAsync();
        }

        //METODO PARA OBTENER UNA GASOLINERA GUARDADA EN SQLITE POR ID
        public Task<FuelStationModel> GetFuelStationAsync(int id)
        {
            return Connection.Table<FuelStationModel>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        //METODO PARA GUARDAR UNA GASOLINERA EN SQLITE
        public Task<int> SaveFuelStationAsync(FuelStationModel item)
        {
            if (item.ID != 0)//SI NO EXISTE EL ID CREA UNA NUEVA GASOLINERA
            {
                return Connection.UpdateAsync(item);
            }
            else//SI LA GASOLINERA YA EXISTE LA ACTUALIZA
            {
                return Connection.InsertAsync(item);
            }
        }

        //METODO PARA ELIMINAR UNA GASOLINERA EN SQLITE
        public Task<int> DeleteFuelStationAsync(FuelStationModel item)
        {
            return Connection.DeleteAsync(item);
        }
    }
}
