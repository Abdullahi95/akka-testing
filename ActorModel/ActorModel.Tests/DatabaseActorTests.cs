using Akka.TestKit.Xunit2;
using System.Collections.Generic;
using Xunit;
using Moq;
using Akka.Actor;
using ActorModel.Actors;
using ActorModel.Messages;

namespace ActorModel.Tests
{
    public class DatabaseActorTests : TestKit
    {
        [Fact]
        public void ShouldReadStatsFromDatabase()
        {   
            // creating a dictionary that will hold the mock stats data the database will return
            var statsData = new Dictionary<string, int> { { "A", 1 }, { "B" , 2 } };

            var mockDb = new Mock<IDatabaseGateway>();
            mockDb.Setup(x => x.GetStoredStatistics()).Returns(statsData);

            IActorRef databaseActor = ActorOf(Props.Create(() => new DatabaseActor(mockDb.Object)));

            databaseActor.Tell(new GetInitialStatisticsMessage());

            var doc = this.ExpectMsg<InitialStatisticsMessage>();

            Assert.Collection(
                doc.PlayCounts,
                item => Assert.Equal(1, item.Value),
                item => Assert.Equal(2, item.Value)
                );
        }

    }
}
