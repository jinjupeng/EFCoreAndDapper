using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebAPI.BLL;
using WebAPI.DAL;
using WebAPI.DAL.UnitOfWork;
using WebAPI.Extensions.AOP;
using WebAPI.IBLL;
using WebAPI.IDAL;
using WebAPI.Model.Contexts;

namespace WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<ITestService, TestService>();
            // 泛型注入
            services.AddScoped(typeof(IBaseService<>), typeof(BaseService<>));
            services.AddScoped(typeof(IBaseDal<>), typeof(BaseDal<>));
            //https://www.cnblogs.com/sheng-jie/p/7416302.html
            services.AddScoped<IUnitOfWork, UnitOfWork<ContextMySql>>();
            services.AddScoped(typeof(TransactionInterceptor));

            // 数据库上下文注入
            services.AddDbContext<ContextMySql>(option => option.UseMySql(Configuration["Setting:DefaultConnection"]));
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
