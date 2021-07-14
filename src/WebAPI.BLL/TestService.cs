using ApiServer.Model.Entities;
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
        public async Task InsertAsync()
        {
            var insertSql = @$"INSERT INTO sys_org ( org_pid, is_leaf, level, status) VALUES
                       (@org_pid, @is_leaf, @level, @status); ";

            var param = new { org_pid = 1, is_leaf = true, level = 1, status = true };
            var count = await _unitOfWork.ExecuteAsync(insertSql, param, _unitOfWork.CurrentTransaction);
            //if(count == 1)
            //{
            //    throw new Exception("测试");
            //}
            var sysUser = new sys_user
            {
                username = "efcoreanddapper",
                password = "123456",
                org_id = 1,
                enabled = true
            };
            await _baseSysUserService.AddAsync(sysUser);
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
