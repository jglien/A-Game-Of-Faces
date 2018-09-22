using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AGameOfFaces.Models;

namespace AGameOfFaces.Controllers
{
    /// <summary>
    /// The game API controller.
    /// </summary>
    [RoutePrefix("api/game")]
    public class GameController : ApiController
    {
        /// <summary>
        /// Gets the profiles to guess from.
        /// </summary>
        /// <returns></returns>
        [Route("begin")]
        [HttpGet]
        public IHttpActionResult Begin(string mode = "")
        {


            return Ok();
        }

        /// <summary>
        /// Get the list of modes.
        /// </summary>
        /// <returns>The modes</returns>
        [Route("modes")]
        [HttpGet]
        public IHttpActionResult Modes()
        {


            return Ok();
        }

        /// <summary>
        /// Submits a guess.
        /// </summary>
        /// <returns>Whether or not the guess was correct.</returns>
        [Route("guess")]
        [HttpPost]
        public IHttpActionResult Guess(Guess guess)
        {


            return Ok();
        }
    }
}
