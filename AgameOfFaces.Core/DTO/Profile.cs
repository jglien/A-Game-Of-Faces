using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AGameOfFaces.Core.DTO
{
    /// <summary>
    /// Profile DTO.
    /// </summary>
    public class Profile
    {
        /// <summary>
        /// First name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Headshot.
        /// </summary>
        public Headshot Headshot { get; set; }

        /// <summary>
        /// ID.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Job title.
        /// </summary>
        public string JobTitle { get; set; }

        /// <summary>
        /// Last name.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// SLUG.
        /// </summary>
        public string Slug { get; set; }

        /// <summary>
        /// Social links.
        /// </summary>
        public IEnumerable<SocialLink> SocialLinks { get; set; }

        /// <summary>
        /// Type of profile.
        /// </summary>
        public string Type { get; set; }
    }
}