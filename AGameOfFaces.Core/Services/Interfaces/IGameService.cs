using System.Collections.Generic;
using AGameOfFaces.Core.DTO;

namespace AGameOfFaces.Core.Services.Interfaces
{
    /// <summary>
    /// The game service interface.
    /// </summary>
    public interface IGameService
    {
        /// <summary>
        /// Get the data to play a game.
        /// </summary>
        /// <param name="requestedMode">The game mode.</param>
        /// <returns></returns>
        Game GetGameData(string requestedMode);

        /// <summary>
        /// Checks if the answer is correct by comparing the name to the face.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="face"></param>
        /// <returns></returns>
        bool CheckAnswer(Guess guess);

        /// <summary>
        /// Gets the leaderboard.
        /// </summary>
        /// <param name="numUsers"></param>
        /// <returns></returns>
        IEnumerable<UserStatistics> GetLeaderboard(int numUsers);

        /// <summary>
        /// The game modes.
        /// </summary>
        IEnumerable<string> Modes { get; }
    }
}
