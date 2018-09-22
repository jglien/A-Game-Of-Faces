using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgameOfFaces.Core.DTO
{
    /// <summary>
    /// Data required to play a game.
    /// </summary>
    public class GameData
    {
        /// <summary>
        /// The urls for images of faces.
        /// </summary>
        public IEnumerable<string> Faces { get; set; }

        /// <summary>
        /// The names.
        /// </summary>
        public IEnumerable<string> Names { get; set; }
    }
}
