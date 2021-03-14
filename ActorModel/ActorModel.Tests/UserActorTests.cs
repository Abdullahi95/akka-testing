using ActorModel.Actors;
using ActorModel.Messages;
using Akka.TestKit;
using Akka.TestKit.Xunit2;
using Xunit;

namespace ActorModel.Tests
{
    public class UserActorTests : TestKit
    {
        [Fact]
        public void ShouldHaveInitialState()
        {
            // arrange
            TestActorRef<UserActor> userActor = ActorOfAsTestActorRef<UserActor>();

            // assert
            Assert.Null(userActor.UnderlyingActor._currentlyplaying);

        }

        [Fact]
        public void ShouldUpdateCurrentlyPlayingState()
        {
            //arrange
            TestActorRef<UserActor> userActor = ActorOfAsTestActorRef<UserActor>();

            // act
            userActor.Tell(new PlayMovieMessage("Locked Down"));

            // assert
            Assert.Equal("Locked Down", userActor.UnderlyingActor._currentlyplaying);
        }
    }
}
