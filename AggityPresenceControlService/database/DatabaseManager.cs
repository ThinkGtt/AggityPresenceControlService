using AggityPresenceControlDataModel.Database;
using SQLite.Net;
using SQLite.Net.Platform.Win32;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AggityPresenceControlService.Database
{
    public class DatabaseManager : SQLiteConnection
    {
        private object lockObject = new object();
        static SemaphoreSlim sl = new SemaphoreSlim(1);

        public DatabaseManager() : base(new SQLitePlatformWin32(), Configuration.DATABASE_FILE)
        {
            //lock (lockObject)
            {
                CreateTable<PunchData>();                
            }
        }

        public async Task<bool> AddData<T>(T data) where T : IStorable
        {
            //lock (lockObject)
            await sl.WaitAsync();
            try
            {
                return Insert(data) == 1;
            }
            finally
            {
                sl.Release();
            }
        }

        //public bool SynchronizeOfflineData(Func<PunchData, bool> sendPunchData)
        public async Task<bool> SynchronizeOfflineData(Func<PunchData, Task<bool>> sendPunchData)
        {
            //lock(lockObject)
            await sl.WaitAsync();
            try
            {
                var query = Table<PunchData>().OrderBy(p => p.Time);
                foreach(var data in query)
                {
                    if(await sendPunchData(data))
                    {
                        Delete<PunchData>(data.Id);
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            finally
            {
                sl.Release();
            }
            return true;
        }
    }
}
