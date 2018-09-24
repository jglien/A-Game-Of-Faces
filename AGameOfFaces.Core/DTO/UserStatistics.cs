namespace AGameOfFaces.Core.DTO
{
    /// <summary>
    /// The user statistics DTO.
    /// </summary>
    public class UserStatistics
    {
        /// <summary>
        /// Correct guesses.
        /// </summary>
        public int CorrectGuesses { get; set; }

        /// <summary>
        /// Percent correct.
        /// </summary>
        public double PercentCorrect { get; set; }

        /// <summary>
        /// Total guesses.
        /// </summary>
        public int TotalGuesses { get; set; }

        /// <summary>
        /// The user.
        /// </summary>
        public string User { get; set; }
    }
}
