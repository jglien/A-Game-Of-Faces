using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AGameOfFaces.Controllers
{
    /// <summary>
    /// Controller for game actions.
    /// </summary>
    public class GameController : ApiController
    {
        /// <summary>
        /// Gets the profiles to guess for normal mode.
        /// </summary>
        /// <returns>The profiles.</returns>
        [HttpGet]
        public IHttpActionResult NormalMode()
        {


            return Ok();
        }

        /// <summary>
        /// Gets the profiles for Mat(t)-Mode.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult MattMode()
        {


            return Ok();
        }

        /// <summary>
        /// Gets the profiles for reverse mode.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult ReverseMode()
        {


            return Ok();
        }

        /// <summary>
        /// Gets the profiles for no one mode.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult NoOneMode()
        {


            return Ok();
        }

        /// <summary>
        /// Submits the answer.
        /// </summary>
        /// <param name="answer"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult Answer([FromBody] string answer)
        {


            return Ok();
        }
    }
}
