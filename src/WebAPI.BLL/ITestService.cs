using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Model.Entities;

namespace WebAPI.BLL
{
    public interface ITestService
    {
        Task InsertAsync();
        List<sys_user> GetUsers();
        List<sys_org> GetOrgs();
    }
}
