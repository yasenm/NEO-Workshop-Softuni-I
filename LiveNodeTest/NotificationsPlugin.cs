using Neo.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveNodeTest
{
    public class NotificationsPlugin : Plugin
    {
        public NotificationsPlugin()
        {
            Plugin.System.ActorSystem.ActorOf(NoticationBroadcaster.Props(Plugin.System.Blockchain));
        }
    }
}
