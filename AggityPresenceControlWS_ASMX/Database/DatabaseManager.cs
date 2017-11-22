using AggityPresenceControlWS_ASMX.Models;
using SQLite;

namespace AggityPresenceControlWS_ASMX.Database
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

            public DatabaseConnectionManager() : base(Configuration.DATABASE_FILE)
            {
                lock (lockObject)
                {
                    //CreateTable<PunchData>();
                    var command = CreateCommand(
                        "CREATE TABLE IF NOT EXISTS PunchData (" + 
                            "IdRow        VARCHAR  PRIMARY KEY," +
                            "Id           INTEGER," +
                            "TerminalId   VARCHAR," +
                            "CardUid      VARCHAR," +
                            "Time         DATETIME," +
                            "Exported     INTEGER," +
                            "ExportedTime DATETIME" +
                        ");"
                    );
                    command.ExecuteNonQuery();
                }
            }

            public bool InsertDataIfNew(PunchData punchData)
            {
                lock (lockObject)
                {
                    //PunchData data = Get<PunchData>(punchData.IdRow);
                    PunchData data = Find<PunchData>(punchData.IdRow);
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
