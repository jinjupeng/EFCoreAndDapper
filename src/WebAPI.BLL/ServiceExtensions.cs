using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace WebAPI.BLL
{
    public static class ServiceExtensions
    {
        /// <summary>
        /// 获取程序集名称
        /// </summary>
        /// <returns></returns>
        public static string GetAssemblyName()
        {
            return Assembly.GetExecutingAssembly().GetName().Name;
        }
    }
}
