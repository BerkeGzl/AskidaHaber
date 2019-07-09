using Project.DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.BLL.SingletonPattern
{
    public class DBTool2
    {
        private static LogContext _dbLogInstance;

        public static LogContext DBLogInstance
        {
            get
            {
                if (_dbLogInstance == null)
                {
                    _dbLogInstance = new LogContext();
                }
                return _dbLogInstance;
            }
        }
    }
}
