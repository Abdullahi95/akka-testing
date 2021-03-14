using ActorModel.Actors;
using Akka.TestKit.Xunit2;
using System.Collections.Generic;
using Xunit;
using ActorModel.Messages;
using System.Collections.ObjectModel;

namespace ActorModel.Tests
{
    public class StatisticsActorTests : TestKit
    {

        [Fact]
        public void ShouldHaveInitialPlayCountsValue()
        {
            // arrange
            StatisticsActor statisticsActor = new StatisticsActor();

            // act
            var expected = statisticsActor.PlayCounts;

            // assert
            Assert.Null(expected);
        }

        [Fact]
        public void ShouldSetInitialPlayCounts()
        {
            // arrange
            StatisticsActor statisticsActor = new StatisticsActor();

            // act
            var movies = new Dictionary<string, int>() { { "Locked Down", 3 }, { "Outside the Wire", 1 } };
            statisticsActor.HandleInitialStatisticsMessage(new InitialStatisticsMessage(new ReadOnlyDictionary<string, int>(movies)));

            // assert
            Assert.Equal(3, statisticsActor.PlayCounts["Locked Down"]);
            Assert.Equal(1, statisticsActor.PlayCounts["Outside the Wire"]);
        }

    }
}
