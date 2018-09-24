using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AGameOfFaces.Core.DTO;
using AgameOfFaces.Core.Repositories;

namespace AgameOfFaces.Core.Helpers
{
    /// <summary>
    /// Extension methods.
    /// </summary>
    public static class ExtensionMethods
    {
        /// <summary>
        /// Converts a DB Statistic object to a UserStatistics DTO.
        /// </summary>
        /// <param name="statistic"></param>
        /// <returns></returns>
        public static UserStatistics ToUserStatistics(this Statistic statistic)
        {
            if (statistic == null)
            {
                throw new ArgumentNullException(nameof(statistic));
            }

            double percentCorrect = (statistic.Correct_Guesses / (double)statistic.Total_Guesses) * 100.00;

            return new UserStatistics
            {
                CorrectGuesses = statistic.Correct_Guesses,
                PercentCorrect = percentCorrect,
                TotalGuesses = statistic.Total_Guesses,
                User = statistic.User
            };
        }
    }
}
