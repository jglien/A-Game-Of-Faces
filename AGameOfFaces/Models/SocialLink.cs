using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AGameOfFaces.Models
{
    /// <summary>
    /// Link to social profile.
    /// </summary>
    public class SocialLink
    {
        /// <summary>
        /// Call to action.
        /// </summary>
        public string CallToAction { get; set; }

        /// <summary>
        /// Social profile link.
        /// </summary>
        public Uri Url { get; set; }

        /// <summary>
        /// Type of profile.
        /// </summary>
        public string Type { get; set; }
    }
}