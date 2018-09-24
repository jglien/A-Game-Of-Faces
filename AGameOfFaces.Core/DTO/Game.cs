using System.Collections.Generic;

namespace AGameOfFaces.Core.DTO
{
    /// <summary>
    /// Game model.
    /// </summary>
    public class Game
    {
        /// <summary>
        /// The urls for images of faces.
        /// </summary>
        public IEnumerable<string> Faces { get; set; }

        /// <summary>
        /// The selected mode.
        /// </summary>
        public string Mode { get; set; }

        /// <summary>
        /// The names.
        /// </summary>
        public IEnumerable<string> Names { get; set; }
    }
}