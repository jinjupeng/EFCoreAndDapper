using ApiServer.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.DAL.UnitOfWork;
using WebAPI.Extensions.Attributes;
using WebAPI.IBLL;

namespace WebAPI.BLL
{
    public class TestService : ITestService
    {
        private readonly IBaseService<sys_user> _baseSysUserService;
        private readonly IBaseService<sys_org> _baseSysOrgService;
        private readonly IUnitOfWork _unitOfWork;

        public TestService(IBaseService<sys_user> baseSysUserService, IBaseService<sys_org> baseSysOrgService,
            IUnitOfWork unitOfWork)
        {
            _baseSysUserService = baseSysUserService;
            _baseSysOrgService = baseSysOrgService;
            _unitOfWork = unitOfWork;
        }

        [Transaction]
        public void InsertAsync()
        {
            //http://codethug.com/2021/03/17/Caching-with-Attributes-in-DotNet-Core5/
            //https://blog.csdn.net/XinShun/article/details/99551993 异步方法和同步方法需分开处理
            var insertSql = @$"INSERT INTO sys_org ( org_pid, org_pids, org_name, is_leaf, level, status) VALUES
                       (@org_pid, @org_pids, @org_name, @is_leaf, @level, @status); ";

            var param = new { org_pid = 1, org_pids = "[0]", org_name = "orgname", is_leaf = true, level = 1, status = true };
            var count =  _unitOfWork.ExecuteAsync(insertSql, param, _unitOfWork.CurrentTransaction);

            var sysUser = new sys_user
            {
                username = "efcoreanddapper",
                password = "12345678",
                org_id = 1,
                enabled = true,
                phone = "hfiguierhgdsahgkahrikshjfwjefuerijclmcjsoiwamsllksjfowjeijljkljlsj" // 超过最大字符，抛异常
            };
            _baseSysUserService.Add(sysUser);
        }

        public List<sys_user> GetUsers()
        {
            var result = _baseSysUserService.GetModels(_ => true).ToList();
            return result;
        }

        public List<sys_org> GetOrgs()
        {
            var result = _baseSysOrgService.GetModels(_ => true).ToList();
            return result;
        }
    }
}
