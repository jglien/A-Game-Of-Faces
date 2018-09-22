using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;
using AgameOfFaces.Core.DTO;
using AgameOfFaces.Core.Enums;
using AgameOfFaces.Core.Services.Interfaces;
using AGameOfFaces.Core.DTO;

namespace AgameOfFaces.Core.Services
{
    /// <summary>
    /// Implementation of IGameService.
    /// </summary>
    public class GameService : IGameService
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public GameService()
        {

        }

        #region Public Methods

        public bool CheckAnswer(string name, string face)
        {
            return false;
        }

        public GameData GetGameData(Mode mode)
        {
            switch(mode)
            {
                case Mode.Normal:
                default:
                    return GetNormalGameData();
            }
        }

        #endregion

        #region Public Properties

        public IEnumerable<string> Modes { get; } = new ReadOnlyCollection<string>(new List<string>
        {
            nameof(Mode.Normal)
        });

        #endregion

        #region Private Methods

        private GameData GetNormalGameData()
        {
            const int numFaces = 6;
            var random = new Random();
            var profiles = GetProfiles().ToList();

            var selectedProfiles = new List<Profile>();
            for(var i = 0; i < numFaces; i++)
            {
                var profile = profiles.ElementAt(random.Next(profiles.Count));
                selectedProfiles.Add(profile);
                
                // Remove so we don't add twice.
                profiles.Remove(profile);
            }

            var profileForName = selectedProfiles.ElementAt(random.Next(selectedProfiles.Count));
            return new GameData
            {
                Faces = selectedProfiles.Select(p => p.Headshot.Url),
                Names = new List<string> { $"{profileForName.FirstName} {profileForName.LastName}" }
            };
        }

        private IEnumerable<Profile> GetProfiles()
        {
            IEnumerable<Profile> profiles = new List<Profile>();

            using (var client = new HttpClient())
            {
                var response = client.GetAsync(@"https://www.willowtreeapps.com/api/v1.0/profiles").Result;
                if (response.IsSuccessStatusCode)
                {
                    profiles = response.Content.ReadAsAsync<IEnumerable<Profile>>().Result;
                }
            }

            return profiles;
        }

        #endregion
    }
}
