using System.Text.Json;

namespace SteamAPI.Logs
{
    public class CustomLogs
    {
        const string PUT = "put";
        const string PATCH = "patch";
        const string DELETE = "delete";

        public static void SaveLog(int id, string message, string title, string method, object? entityBefore = null, object entityAfter = null)
        {
            // <datetime> - Game <id> - <titulo> - <Remover|Alterar (e descrever a alteração)>
            // 01/01/2021 13:45:00 - Game 1 - Counter Strike - Alterado de X para Y
            // 01/01/2021 13:45:00 - Game 1 - Counter Strike - Removido

            //var now = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
            var now = DateTime.Now.ToString("G");

            if(method.Equals(PUT, StringComparison.InvariantCultureIgnoreCase) || method.Equals(PATCH, StringComparison.InvariantCultureIgnoreCase))
            {
                Console.WriteLine($"{now} - {message} {id} - {title} - Alterado de {JsonSerializer.Serialize(entityBefore)} para {JsonSerializer.Serialize(entityAfter)}");
            } else if(method.Equals(DELETE, StringComparison.InvariantCultureIgnoreCase))
            {
                Console.WriteLine($"{now} - {message} {id} - {title} - Removido");
            }
        }
    }
}
