using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgameOfFaces.Core.DTO;
using AgameOfFaces.Core.Enums;
using AgameOfFaces.Core.Services.Interfaces;

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
                    break;
            }

            return new GameData();
        }

        #endregion

        #region Public Properties

        public IEnumerable<string> Modes { get; } = new ReadOnlyCollection<string>(new List<string>
        {
            nameof(Mode.Normal)
        });

        #endregion
    }
}
