using ActorModel.Messages;
using Akka.Actor;
using System;
using System.Collections.Generic;

namespace ActorModel.Actors
{
    public class StatisticsActor : ReceiveActor
    {
        public Dictionary<string, int> PlayCounts { get; set; }
        public IActorRef DatabaseActor { get; private set; }

        public StatisticsActor(IActorRef databaseActor)
        {
            this.DatabaseActor = databaseActor;
            
            this.Receive<InitialStatisticsMessage>(message => HandleInitialStatisticsMessage(message));
            this.Receive<string>(message => HandleTitleMessage(message));
        }

        private void HandleTitleMessage(string message)
        {
            if (PlayCounts.ContainsKey(message))
            {
                PlayCounts[message]++;
            }
            else
            {
                PlayCounts.Add(message, 1);
            }
        }

        public void HandleInitialStatisticsMessage(InitialStatisticsMessage message)
        {
            this.PlayCounts = new Dictionary<string, int>(message.PlayCounts);
        }

        public override void AroundPreStart()
        {
            this.DatabaseActor.Tell(new GetInitialStatisticsMessage());
        }

    }
}
