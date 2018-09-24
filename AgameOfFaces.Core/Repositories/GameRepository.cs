using System.Collections.Generic;
using System.Linq;
using System.Threading;
using AGameOfFaces.Core.DTO;
using AgameOfFaces.Core.Helpers;
using AgameOfFaces.Core.Repositories.Interfaces;

namespace AgameOfFaces.Core.Repositories
{
    /// <summary>
    /// Game repository.
    /// </summary>
    public class GameRepository : IGameRepository
    {
        /// <summary>
        /// Update guesses.
        /// </summary>
        /// <param name="correct"></param>
        public void UpdateUserGuess(bool correct)
        {
            var currentUser = Thread.CurrentPrincipal.Identity.Name;

            if (string.IsNullOrEmpty(currentUser))
            {
                return;
            }

            using (var db = new DataEntities())
            {
                var statistic = db.Statistics.FirstOrDefault(s => s.User.Equals(currentUser));
                if (statistic == null)
                {
                    var newStatistic = db.Statistics.Create();
                    newStatistic.User = currentUser;
                    newStatistic.Total_Guesses = 1;
                    newStatistic.Correct_Guesses = correct ? 1 : 0;
                    db.Statistics.Add(newStatistic);
                }
                else
                {
                    statistic.Total_Guesses++;
                    if (correct)
                    {
                        statistic.Correct_Guesses++;
                    }
                }

                db.SaveChanges();
            }
        }

        /// <summary>
        /// Gets the statistics.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<UserStatistics> GetStatistics()
        {
            using (var db = new DataEntities())
            {
                return db.Statistics.ToList().Select(s => s.ToUserStatistics());
            }
        }
    }
}
