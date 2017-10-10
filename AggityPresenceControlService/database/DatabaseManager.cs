using SQLite.Net;
using SQLite.Net.Platform.Win32;
using System;

namespace AggityPresenceControlService.Database
{
    public class DatabaseManager : SQLiteConnection
    {
        private object lockObject = new object();

        public DatabaseManager() : base(new SQLitePlatformWin32(), Configuration.DATABASE_FILE)
        {
            lock (lockObject)
            {
                CreateTable<PunchData>();                
            }
        }

        public bool AddData<T>(T data) where T : IStorable
        {
            lock (lockObject)
            {
                return Insert(data) == 1;
            }
        }

        public bool SynchronizeOfflineData(Func<PunchData, bool> sendPunchData)
        {
            lock(lockObject)
            {
                var query = Table<PunchData>().OrderBy(p => p.Time);
                foreach(var data in query)
                {
                    if (sendPunchData(data))
                    {
                        Delete<PunchData>(data.Id);
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return true;
        }

    }
}
