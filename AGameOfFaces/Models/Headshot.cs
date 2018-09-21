using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AGameOfFaces.Models
{
    /// <summary>
    /// Headshot model.
    /// </summary>
    public class Headshot
    {
        /// <summary>
        /// Alt.
        /// </summary>
        public string Alt { get; set; }

        /// <summary>
        /// Picture height.
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// ID.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// MIME type.
        /// </summary>
        public string MimeType { get; set; }

        /// <summary>
        /// Type.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Resource URL.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Picture Width.
        /// </summary>
        public int Width { get; set;}
    }
}