using System;
using System.Threading.Tasks;

namespace AppFuelStations.Extensions
{
    public static class TaskExtensions
    {
        //EXTENSION PARA UNA Task PARA PODERLO INVOCAR SIN NECESIDAD DE LA PALABRA RESERVADA AWAIT
        public static async void SafeFireAndForget(this Task task, bool returnToCallingContext, Action<Exception> onException = null)
        {
            try
            {
                await task.ConfigureAwait(returnToCallingContext);
            }
            catch (Exception ex) when (onException != null)
            {

                onException(ex);
            }
        }
    }
}
