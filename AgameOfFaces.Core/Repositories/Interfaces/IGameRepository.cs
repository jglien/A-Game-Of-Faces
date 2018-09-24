using System.Collections.Generic;
using AGameOfFaces.Core.DTO;

namespace AgameOfFaces.Core.Repositories.Interfaces
{
    /// <summary>
    /// Game repository interface.
    /// </summary>
    public interface IGameRepository
    {
        /// <summary>
        /// Updates the user's statistics, number of guesses.
        /// </summary>
        /// <param name="correct"></param>
        void UpdateUserGuess(bool correct);

        /// <summary>
        /// Gets the statistics.
        /// </summary>
        /// <returns></returns>
        IEnumerable<UserStatistics> GetStatistics();
    }
}
