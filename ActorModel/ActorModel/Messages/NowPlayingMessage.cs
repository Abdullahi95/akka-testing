using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActorModel.Messages
{
    public class NowPlayingMessage
    {
        public string CurrentlyPlaying { get; private set; }

        public NowPlayingMessage(string currentlyPlaying)
        {
            this.CurrentlyPlaying = currentlyPlaying;
        }
    }
}
