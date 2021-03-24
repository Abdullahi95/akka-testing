using ActorModel.Messages;
using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ActorModel.Actors
{
    public class UserActor : ReceiveActor
    {
        public string Currentlyplaying { get; private set; }
        public IActorRef ActorRef { get; private set; }

        public UserActor(IActorRef actorRef)
        {
            this.ActorRef = actorRef;

            this.Receive<PlayMovieMessage>(message => HandlePlayMovieMessage(message));
        }

        private void HandlePlayMovieMessage(PlayMovieMessage message)
        {
            Currentlyplaying = message.Movie;

            Sender.Tell(new NowPlayingMessage(this.Currentlyplaying));

            this.ActorRef.Tell(message.Movie);
        }
    }
}
