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

        public Task<List<FuelStationModel>> GetAllFuelStationAsync()
        {
            return Connection.Table<FuelStationModel>().ToListAsync();
        }

        public Task<FuelStationModel> GetFuelStationAsync(int id)
        {
            return Connection.Table<FuelStationModel>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveFuelStationAsync(FuelStationModel item)
        {
            if (item.ID != 0)
            {
                return Connection.UpdateAsync(item);
            }
            else
            {
                return Connection.InsertAsync(item);
            }
        }

        public Task<int> DeleteFuelStationAsync(FuelStationModel item)
        {
            return Connection.DeleteAsync(item);
        }
    }
}
