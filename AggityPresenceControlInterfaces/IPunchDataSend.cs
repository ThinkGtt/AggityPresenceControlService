using AggityPresenceControlDataModel;

namespace AggityPresenceControlInterfaces
{
    public interface IPunchDataSend
    {
        bool SendPunchData(PunchDataBase punchData);
    }
}
