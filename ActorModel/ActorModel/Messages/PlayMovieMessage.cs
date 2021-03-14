using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActorModel.Messages
{
    
    public class PlayMovieMessage
    {
        public string Movie { get; private set; }

        public PlayMovieMessage(string movie)
        {
            this.Movie = movie;
        }

    }
}
