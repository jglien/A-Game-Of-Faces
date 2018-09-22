using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AGameOfFaces.Models
{
    /// <summary>
    /// Model for "beggining" a game.
    /// </summary>
    public class Begin
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