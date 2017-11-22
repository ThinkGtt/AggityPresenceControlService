using AggityPresenceControlWS.Models;
using SQLite.Net;
using SQLite.Net.Platform.Win32;

namespace AggityPresenceControlWS.Database
{
    public static class DatabaseManager
    {
        static DatabaseConnectionManager DataBase { get; set; }

        static DatabaseManager()
        {
             DataBase = new DatabaseConnectionManager();
        }

        public static bool InsertDataIfNew(PunchData punchData)
        {
            return DataBase.InsertDataIfNew(punchData);
        }

        class DatabaseConnectionManager : SQLiteConnection
        {
            private object lockObject = new object();

            public DatabaseConnectionManager() : base(new SQLitePlatformWin32(), Configuration.DATABASE_FILE)
            {
                lock (lockObject)
                {
                    CreateTable<PunchData>();
                }
            }

            public bool InsertDataIfNew(PunchData punchData)
            {
                lock (lockObject)
                {
                    PunchData data = Get<PunchData>(punchData.RowId);
                    if (data != null)
                    {
                        return true;
                    }
                    return Insert(punchData) == 1;
                }
            }
        }
    }
}
