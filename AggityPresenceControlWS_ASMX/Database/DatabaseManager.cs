using AggityPresenceControlWS_ASMX.Models;
using SQLite;
using System;
using System.Collections.Generic;

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

        public static void SetExportedData(string idRow)
        {
            var punchData = DataBase.Find<PunchData>(idRow);
            if (punchData != null)
            {
                punchData.Exported = true;
                punchData.ExportedTime = DateTime.Now;
                DataBase.Update(punchData);
            }
        }


        public static List<PunchData> GetNotExportedPunchData()
        {
            return DataBase.Query<PunchData>("SELECT * FROM PunchData WHERE Exported = 0 ORDER BY Time ASC, TerminalId");
        }


        class DatabaseConnectionManager : SQLiteConnection
        {
            private object lockObject = new object();

            public DatabaseConnectionManager() : base(Configuration.DATABASE_FILE)
            {
                lock (lockObject)
                {
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
