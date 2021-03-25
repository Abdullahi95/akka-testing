using ActorModel.Actors;
using ActorModel.Messages;
using Akka.Actor;
using Akka.TestKit;
using Akka.TestKit.TestActors;
using Akka.TestKit.Xunit2;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ActorModel.Tests
{
    public class IntegrationTests : TestKit
    {
        [Fact]
        public void UserShouldUpdatePlayCounts()
        {
            TestActorRef<StatisticsActor> statisticsActor = ActorOfAsTestActorRef(() => new StatisticsActor(ActorOf(BlackHoleActor.Props)));

            var movies = new Dictionary<string, int>() { { "Locked Down", 23 }, { "Outside the Wire", 2 } };

            var message = new InitialStatisticsMessage(new ReadOnlyDictionary<string, int>(movies));
            
            statisticsActor.Tell(message);

            var userActor = ActorOfAsTestActorRef<UserActor>(Props.Create(() => new UserActor(statisticsActor)));

            userActor.Tell(new PlayMovieMessage("Locked Down"));

            Assert.Equal(24, statisticsActor.UnderlyingActor.PlayCounts["Locked Down"]);

        }

        // Actors of type IActorRef and TestActorRef, run on different threads, which in case gives us trouble testing. 

    }
}
