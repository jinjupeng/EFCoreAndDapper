using ApiServer.Model.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPI.BLL
{
    public interface ITestService
    {
        void InsertAsync();
        List<sys_user> GetUsers();
        List<sys_org> GetOrgs();
    }
}
