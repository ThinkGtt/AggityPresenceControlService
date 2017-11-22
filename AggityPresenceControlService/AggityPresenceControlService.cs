using System.ServiceProcess;

namespace AggityPresenceControlService
{
    partial class AggityPresenceControlService : ServiceBase
    {
        public AggityPresenceControlService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            // TODO: agregar código aquí para iniciar el servicio.
            Program.Start(args);
        }

        protected override void OnStop()
        {
            // TODO: agregar código aquí para realizar cualquier anulación necesaria para detener el servicio.
            Program.Stop();
        }
    }
}
