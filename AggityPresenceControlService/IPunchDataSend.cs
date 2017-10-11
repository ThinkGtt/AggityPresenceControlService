using AggityPresenceControlService.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AggityPresenceControlService
{
    public interface IPunchDataSend
    {
        bool SendPunchData(PunchData punchData);
    }
}
