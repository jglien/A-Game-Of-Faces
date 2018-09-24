using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AgameOfFaces.Core.Enums;
using AgameOfFaces.Core.Services;
using AgameOfFaces.Core.Services.Interfaces;
using AGameOfFaces.Models;

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

        #endregion

        /// <summary>
        /// Constructor.
        /// </summary>
        public GameController()
        {
            _gameService = new GameService();
        }

        /// <summary>
        /// Gets the profiles to guess from.
        /// </summary>
        /// <returns></returns>
        [Route("")]
        [HttpGet]
        public IHttpActionResult Get(string mode = "")
        {
            var requestedMode = Enum.TryParse<Mode>(mode, true, out var modeType);
            var data = _gameService.GetGameData(modeType);

            return Ok(new Game {
                Faces = data.Faces,
                Mode = requestedMode ? mode : nameof(Mode.Normal),
                Names = data.Names
            });
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
        /// Submits a guess.
        /// </summary>
        /// <returns>Whether or not the guess was correct.</returns>
        [Route("guess")]
        [HttpPost]
        public IHttpActionResult Guess(Guess guess)
        {
            var isCorrect = _gameService.CheckAnswer(guess.Name, guess.Face);

            return Ok(isCorrect);
        }
    }
}
