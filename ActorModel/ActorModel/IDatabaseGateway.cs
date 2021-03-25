using System.Collections.Generic;

namespace ActorModel
{
    public interface IDatabaseGateway
    {
        IDictionary<string, int> GetStoredStatistics();

        // In a real implementation, this would go to a database, for example an 
        // SQL Server and retrieve a dictionary of all of the movie titles with their
        // with their play counts

    }
}
