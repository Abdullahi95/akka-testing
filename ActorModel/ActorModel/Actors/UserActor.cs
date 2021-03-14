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
        public string _currentlyplaying { get; private set; }

        public UserActor()
        {
            this.Receive<PlayMovieMessage>(message => HandlePlayMovieMessage(message));
        }

        private void HandlePlayMovieMessage(PlayMovieMessage message)
        {
            _currentlyplaying = message.Movie;

            Sender.Tell(new NowPlayingMessage(this._currentlyplaying));
        }
    }
}
