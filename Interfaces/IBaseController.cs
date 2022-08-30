using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace SteamAPI.Interfaces
{
    public interface IBaseController<T>
    {
        Task<IActionResult> Get();

        Task<IActionResult> GetByKey(int key);

        Task<IActionResult> Post(T entity);

        Task<IActionResult> Put(int key, T entity);

        Task<IActionResult> Patch(int key, T entity);

        Task<IActionResult> Delete(int key);
    }
}
