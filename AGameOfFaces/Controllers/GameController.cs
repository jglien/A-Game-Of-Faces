using System.Web.Http;
using AgameOfFaces.Core.Repositories;
using AgameOfFaces.Core.Repositories.Interfaces;
using AgameOfFaces.Core.Services;
using AgameOfFaces.Core.Services.Interfaces;
using AGameOfFaces.Core.DTO;

namespace AGameOfFaces.Controllers
{
    /// <summary>
    /// The game API controller.
    /// </summary>
    [RoutePrefix("api/game")]
    public class GameController : ApiController
    {
        #region Private Fields

        private readonly IGameService _gameService;

        private readonly IGameRepository _gameRepository;

        #endregion

        /// <summary>
        /// Constructor.
        /// </summary>
        public GameController()
        {
            _gameRepository = new GameRepository();
            _gameService = new GameService(_gameRepository);
        }

        /// <summary>
        /// Gets the profiles to guess from.
        /// </summary>
        /// <returns></returns>
        [Route("")]
        [HttpGet]
        public IHttpActionResult Get(string mode = "")
        {
            var game = _gameService.GetGameData(mode);

            return Ok(game);
        }

        /// <summary>
        /// Get the list of modes.
        /// </summary>
        /// <returns>The modes</returns>
        [Route("modes")]
        [HttpGet]
        public IHttpActionResult Modes()
        {
            return Ok(_gameService.Modes);
        }

        /// <summary>
        /// Gets the leaderboard.
        /// </summary>
        /// <param name="numUsers"></param>
        /// <returns></returns>
        /// <remarks>
        /// Defaults to 10 users.
        /// </remarks>
        [Route("leaderboard")]
        [HttpGet]
        public IHttpActionResult GetLeaderboard(int numUsers = 10)
        {
            return Ok(_gameService.GetLeaderboard(numUsers));
        }

        /// <summary>
        /// Submits a guess.
        /// </summary>
        /// <returns>Whether or not the guess was correct.</returns>
        [Route("guess")]
        [HttpPost]
        public IHttpActionResult Guess(Guess guess)
        {
            var isCorrect = _gameService.CheckAnswer(guess);

            return Ok(isCorrect);
        }
    }
}
