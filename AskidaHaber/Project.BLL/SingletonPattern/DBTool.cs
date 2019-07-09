using Project.DAL.Context;

namespace Project.BLL.SingletonPattern
{
    public class DBTool
    {
        private DBTool() { } 

        private static MyContext _dbInstance;

        private static object _LockSync = new object();

        public static MyContext DBInstance
        {
            get
            {
                if (_dbInstance == null)
                {
                    lock (_LockSync)
                    {
                        if (_dbInstance == null)
                        {
                            _dbInstance = new MyContext();
                        }
                    }
                }
                return _dbInstance;
            }
        }
    }
}
