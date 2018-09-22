using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgameOfFaces.Core.DTO;
using AgameOfFaces.Core.Enums;

namespace AgameOfFaces.Core.Services.Interfaces
{
    /// <summary>
    /// The game service interface.
    /// </summary>
    public interface IGameService
    {
        /// <summary>
        /// Get the data to play a game.
        /// </summary>
        /// <param name="mode">The game mode.</param>
        /// <returns></returns>
        GameData GetGameData(Mode mode);

        /// <summary>
        /// Checks if the answer is correct by comparing the name to the face.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="face"></param>
        /// <returns></returns>
        bool CheckAnswer(string name, string face);

        /// <summary>
        /// The game modes.
        /// </summary>
        IEnumerable<string> Modes { get; }
    }
}
