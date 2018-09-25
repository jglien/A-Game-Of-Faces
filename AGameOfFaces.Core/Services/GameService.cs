using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using AGameOfFaces.Core.DTO;
using AGameOfFaces.Core.Enums;
using AGameOfFaces.Core.Repositories.Interfaces;
using AGameOfFaces.Core.Services.Interfaces;

namespace AGameOfFaces.Core.Services
{
    /// <summary>
    /// Implementation of IGameService.
    /// </summary>
    public class GameService : IGameService
    {
        #region Private Fields

        private readonly IGameRepository _gameRepository;

        #endregion

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="gameRepository"></param>
        public GameService(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository ?? throw new ArgumentNullException(nameof(gameRepository));
        }

        #region Public Methods

        public bool CheckAnswer(Guess guess)
        {
            var profiles = GetProfiles();

            var correct = profiles.Any(p => guess.Name.Equals($"{p.FirstName} {p.LastName}") && guess.Face.Equals(p.Headshot.Url));
            _gameRepository.UpdateUserGuess(correct);

            return correct;
        }

        public Game GetGameData(string requestedMode)
        {
            Enum.TryParse<Mode>(requestedMode, true, out var mode);

            switch(mode)
            {
                case Mode.Engineer:
                    return GetGameDataFilter(nameof(Mode.Engineer), ps => ps.Where(p => !string.IsNullOrEmpty(p.JobTitle) && p.JobTitle.Contains("Engineer")));
                case Mode.Matt:
                    return GetGameDataFilter(nameof(Mode.Matt), ps => ps.Where(p => Regex.Match(p.FirstName, @"Mat+(hew)??").Success));
                case Mode.Reverse:
                    return GetReverseGameData();
                case Mode.Normal:
                default:
                    return GetNormalGameData();
            }
        }
        
        public IEnumerable<UserStatistics> GetLeaderboard(int numUsers)
        {
            var userStats = _gameRepository.GetStatistics().ToList();

            userStats.Sort((x,y) => x.PercentCorrect.CompareTo(y.PercentCorrect));
            return userStats.Take(numUsers);
        }

        #endregion

        #region Public Properties

        public static IEnumerable<string> Modes { get; } = new ReadOnlyCollection<string>(new List<string>
        {
            nameof(Mode.Normal),
            nameof(Mode.Reverse),
            nameof(Mode.Matt),
            nameof(Mode.Engineer)
        });

        #endregion

        #region Private Methods

        private Game GetNormalGameData()
        {
            const int numFaces = 6;
            var random = new Random();
            var profiles = GetRandomProfiles(numFaces, random, p => p);

            var profileForName = profiles.ElementAt(random.Next(profiles.Count));
            return new Game
            {
                Faces = profiles.Select(p => p.Headshot.Url),
                Mode = nameof(Mode.Normal),
                Names = new List<string> { $"{profileForName.FirstName} {profileForName.LastName}" }
            };
        }

        private Game GetReverseGameData()
        {
            const int numNames = 6;
            var random = new Random();
            var profiles = GetRandomProfiles(numNames, random, p => p);

            var profileForFace = profiles.ElementAt(random.Next(profiles.Count));
            return new Game
            {
                Faces = new List<string> { profileForFace.Headshot.Url  },
                Mode = nameof(Mode.Reverse),
                Names = profiles.Select(p => $"{p.FirstName} {p.LastName}")
            };
        }

        private Game GetGameDataFilter(string mode, Func<IEnumerable<Profile>, IEnumerable<Profile>> filter)
        {
            const int numFaces = 6;
            var random = new Random();
            var profiles = GetRandomProfiles(numFaces, random, filter);

            var profileForName = profiles.ElementAt(random.Next(profiles.Count));
            return new Game
            {
                Faces = profiles.Select(p => p.Headshot.Url),
                Mode = mode,
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
