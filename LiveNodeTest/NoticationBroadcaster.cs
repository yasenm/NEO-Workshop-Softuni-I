using Akka.Actor;
using Neo.Ledger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveNodeTest
{
    public class NoticationBroadcaster : UntypedActor
    {
        public static event EventHandler<Blockchain.ApplicationExecuted> ApplicationExecuted;

        public NoticationBroadcaster(IActorRef blockchain)
        {
            blockchain.Tell(new Blockchain.Register());
        }

        protected override void OnReceive(object message)
        {
            if (message is Blockchain.ApplicationExecuted e)
            {
                var test = e.ExecutionResults;
            }
        }

        public static Props Props(IActorRef blockchain)
        {
            return Akka.Actor.Props.Create(() => new NoticationBroadcaster(blockchain));
        }
    }
}
