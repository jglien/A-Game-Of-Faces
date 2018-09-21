using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AGameOfFaces.Controllers
{
    /// <summary>
    /// Controller for game state.
    /// </summary>
    public class GameStateController : ApiController
    {
        /// <summary>
        /// Refreshes game data from server.
        /// </summary>
        /// <returns>Http response.</returns>
        [HttpGet]
        public IHttpActionResult Refresh()
        {


            return Ok();
        }

        /// <summary>
        /// Gets the leaderboard.
        /// </summary>
        /// <returns>The leaderboard</returns>
        [HttpGet]
        public IHttpActionResult Leaderboard()
        {


            return Ok();
        }
    }
}
