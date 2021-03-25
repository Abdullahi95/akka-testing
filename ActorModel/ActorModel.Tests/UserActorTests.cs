using ActorModel.Actors;
using ActorModel.Messages;
using Akka.Actor;
using Akka.TestKit;
using Akka.TestKit.TestActors;
using Akka.TestKit.Xunit2;
using Xunit;

namespace ActorModel.Tests
{
    public class UserActorTests : TestKit
    {
        [Fact]
        [Trait("UserActor_Tests", "Unit Test")]
        public void ShouldHaveInitialState()
        {
            // arrange
            TestActorRef<UserActor> userActor = ActorOfAsTestActorRef<UserActor>(Props.Create(() => new UserActor(ActorOf(BlackHoleActor.Props))));

            // assert
            Assert.Null(userActor.UnderlyingActor.Currentlyplaying);

        }

        [Fact]
        [Trait("UserActor_Tests", "Unit Test")]
        public void ShouldUpdateCurrentlyPlayingState()
        {
            //arrange
            var userActor = ActorOfAsTestActorRef<UserActor>(Props.Create(() => new UserActor(ActorOf(BlackHoleActor.Props))));

            // act
            userActor.Tell(new PlayMovieMessage("Locked Down"));

            // assert
            Assert.Equal("Locked Down", userActor.UnderlyingActor.Currentlyplaying);
        }
        [Fact]
        [Trait("UserActor_Tests", "Unit Test")]
        public void ShouldReceiveNowPlayingMessage()
        {
            // arrange
            IActorRef statisticsActor = ActorOfAsTestActorRef<UserActor>(Props.Create(() => new UserActor(ActorOf(BlackHoleActor.Props))));

            // act
            statisticsActor.Tell(new PlayMovieMessage("Locked Down"));

            // assert
            var received = ExpectMsg<NowPlayingMessage>();

            Assert.Equal("Locked Down", received.CurrentlyPlaying);


        }


    }
}
