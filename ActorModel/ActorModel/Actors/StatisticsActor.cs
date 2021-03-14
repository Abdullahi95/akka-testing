using ActorModel.Messages;
using Akka.Actor;
using System.Collections.Generic;

namespace ActorModel.Actors
{
    public class StatisticsActor : ReceiveActor
    {
        public Dictionary<string, int> PlayCounts { get; set; }

        public StatisticsActor()
        {
            this.Receive<InitialStatisticsMessage>(message => HandleInitialStatisticsMessage(message));
        }

        public void HandleInitialStatisticsMessage(InitialStatisticsMessage message)
        {
            this.PlayCounts = new Dictionary<string, int>(message.PlayCounts);
        }

    }
}
