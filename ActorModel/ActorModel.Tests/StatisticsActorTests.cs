using ActorModel.Actors;
using Akka.TestKit.Xunit2;
using System.Collections.Generic;
using Xunit;
using ActorModel.Messages;
using System.Collections.ObjectModel;
using Akka.Actor;
using Akka.TestKit;
using Akka.TestKit.TestActors;
using ActorModel.Tests.MockActors;

namespace ActorModel.Tests
{
    public class StatisticsActorTests : TestKit
    {

        [Fact]
        public void ShouldHaveInitialPlayCountsValue()
        {
            // arrange
            StatisticsActor statisticsActor = new StatisticsActor(null);

            // act
            var expected = statisticsActor.PlayCounts;

            // assert
            Assert.Null(expected);
        }

        [Fact]
        public void ShouldSetInitialPlayCounts()
        {
            // arrange
            StatisticsActor statisticsActor = new StatisticsActor(null);

            // act
            var movies = new Dictionary<string, int>() { { "Locked Down", 3 }, { "Outside the Wire", 1 } };
            statisticsActor.HandleInitialStatisticsMessage(new InitialStatisticsMessage(new ReadOnlyDictionary<string, int>(movies)));

            // assert
            Assert.Equal(3, statisticsActor.PlayCounts["Locked Down"]);
            Assert.Equal(1, statisticsActor.PlayCounts["Outside the Wire"]);
        }

        [Fact]
        public void ShouldReceiveInitialStatisticsMessage()
        {
            // Arrange
            // We have created an actor that is running in our test actor system.
            TestActorRef<StatisticsActor> statisticsActor = ActorOfAsTestActorRef<StatisticsActor>(Props.Create(() => new StatisticsActor(ActorOf(BlackHoleActor.Props))));

            // Act
            var movies = new Dictionary<string, int>() { { "Locked Down", 3 }, { "Outside the Wire", 1 } };
            var message = new InitialStatisticsMessage(new ReadOnlyDictionary<string, int>(movies));
            statisticsActor.Tell(message);

            // Assert
            Assert.Equal(3, statisticsActor.UnderlyingActor.PlayCounts["Locked Down"]);
            Assert.Equal(1, statisticsActor.UnderlyingActor.PlayCounts["Outside the Wire"]);

        }

        [Fact]
        public void Initial_StatisticsMessage_Should_Update_PlayCounts()
        {
            // arrange
            TestActorRef<StatisticsActor> statisticsActor = ActorOfAsTestActorRef<StatisticsActor>(Props.Create(() => new StatisticsActor(ActorOf(BlackHoleActor.Props))));

            // act
            var movies = new Dictionary<string, int>() { { "Locked Down", 3 }, { "Outside the Wire", 2 } };
            
            var message = new InitialStatisticsMessage(new ReadOnlyDictionary<string, int>(movies));
            statisticsActor.Tell(message);

            statisticsActor.Tell("Outside the wire");

            // assert
            Assert.Equal(2, statisticsActor.UnderlyingActor.PlayCounts["Outside the Wire"]);
        }


        [Fact]
        public void ShouldGetInitialStatsFromData()
        {
            //arrange

            var mockDatabaseActorRef = ActorOfAsTestActorRef<MockDatabaseActor>();
            
            var statisticsActorRef = ActorOfAsTestActorRef<StatisticsActor>(Props.Create(() => new StatisticsActor(mockDatabaseActorRef)));

            // assert

            Assert.Equal(1, statisticsActorRef.UnderlyingActor.PlayCounts["Locked Down"]);

        }




    }
}
