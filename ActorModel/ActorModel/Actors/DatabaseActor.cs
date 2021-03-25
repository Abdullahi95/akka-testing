using ActorModel.Messages;
using Akka.Actor;
using System.Collections.ObjectModel;

namespace ActorModel.Actors
{
    public class DatabaseActor : ReceiveActor
    {
        private readonly IDatabaseGateway _databaseGateway;

        public DatabaseActor(IDatabaseGateway databaseGateway)
        {
            _databaseGateway = databaseGateway;

            this.Receive<GetInitialStatisticsMessage>(message => {

                var storedStats = _databaseGateway.GetStoredStatistics();

                this.Sender.Tell(new InitialStatisticsMessage(new ReadOnlyDictionary<string, int>(storedStats)));
            });
        }

    }
}
