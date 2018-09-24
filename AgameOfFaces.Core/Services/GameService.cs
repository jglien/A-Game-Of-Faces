using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Text.RegularExpressions;
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
            var profiles = GetProfiles();

            return profiles.Any(p => name.Equals($"{p.FirstName} {p.LastName}") && face.Equals(p.Headshot.Url));
        }

        public GameData GetGameData(Mode mode)
        {
            switch(mode)
            {
                case Mode.Matt:
                    return GetGameDataFilter(ps => ps.Where(p => Regex.Match(p.FirstName, @"Mat+(hew)??").Success));
                case Mode.Reverse:
                    return GetReverseGameData();
                case Mode.Normal:
                default:
                    return GetNormalGameData();
            }
        }

        #endregion

        #region Public Properties

        public IEnumerable<string> Modes { get; } = new ReadOnlyCollection<string>(new List<string>
        {
            nameof(Mode.Normal),
            nameof(Mode.Reverse),
            nameof(Mode.Matt)
        });

        #endregion

        #region Private Methods

        private GameData GetNormalGameData()
        {
            const int numFaces = 6;
            var random = new Random();
            var profiles = GetRandomProfiles(numFaces, random, p => p);

            var profileForName = profiles.ElementAt(random.Next(profiles.Count));
            return new GameData
            {
                Faces = profiles.Select(p => p.Headshot.Url),
                Names = new List<string> { $"{profileForName.FirstName} {profileForName.LastName}" }
            };
        }

        private GameData GetReverseGameData()
        {
            const int numNames = 6;
            var random = new Random();
            var profiles = GetRandomProfiles(numNames, random, p => p);

            var profileForFace = profiles.ElementAt(random.Next(profiles.Count));
            return new GameData
            {
                Faces = new List<string> { profileForFace.Headshot.Url  },
                Names = profiles.Select(p => $"{p.FirstName} {p.LastName}")
            };
        }

        private GameData GetGameDataFilter(Func<IEnumerable<Profile>, IEnumerable<Profile>> filter)
        {
            const int numFaces = 6;
            var random = new Random();
            var profiles = GetRandomProfiles(numFaces, random, filter);

            var profileForName = profiles.ElementAt(random.Next(profiles.Count));
            return new GameData
            {
                Faces = profiles.Select(p => p.Headshot.Url),
                Names = new List<string> { $"{profileForName.FirstName} {profileForName.LastName}" }
            };
        }

        private IList<Profile> GetRandomProfiles(int numProfiles, Random random, Func<IEnumerable<Profile>, IEnumerable<Profile>> filter)
        {
            var profiles = filter(GetProfiles()).ToList();

            var selectedProfiles = new List<Profile>();
            for (var i = 0; i < numProfiles; i++)
            {
                var profile = profiles.ElementAt(random.Next(profiles.Count));
                selectedProfiles.Add(profile);

                // Remove so we don't add twice.
                profiles.Remove(profile);
            }

            return selectedProfiles;
        }

        private IEnumerable<Profile> GetProfiles()
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync(@"https://www.willowtreeapps.com/api/v1.0/profiles").Result;
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsAsync<IEnumerable<Profile>>().Result;
                }
            }

            return Enumerable.Empty<Profile>();
        }

        #endregion
    }
}
