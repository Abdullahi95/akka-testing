using ActorModel.Messages;
using Akka.Actor;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ActorModel.Tests.MockActors
{
    public class MockDatabaseActor : ReceiveActor
    {
        public MockDatabaseActor()
        {
            this.Receive<GetInitialStatisticsMessage>(message => {

                var stats = new Dictionary<string, int>() { {"Locked Down", 1 } };
                // Line 18 in the real DatabaseActor would would contain the interaction with the database itself or a
                // method which returns the data but since this is a mock actor, we hardcoded what is returned.

                this.Sender.Tell(new InitialStatisticsMessage(new ReadOnlyDictionary<string, int>(stats)));
            });
        }

    }
}
