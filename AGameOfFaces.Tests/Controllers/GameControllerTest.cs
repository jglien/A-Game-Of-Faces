using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Results;
using AGameOfFaces.Controllers;
using AGameOfFaces.Core.DTO;
using AGameOfFaces.Core.Enums;
using AGameOfFaces.Core.Repositories.Interfaces;
using AGameOfFaces.Core.Services;
using AGameOfFaces.Core.Services.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace AGameOfFaces.Tests.Controllers
{
    [TestClass]
    public class GameControllerTest
    {
        private Mock<IGameRepository> _gameRepository;
        private IGameService _gameService;
        private GameController _gameController;

        [TestInitialize]
        public void TestInit()
        {
            _gameRepository = new Mock<IGameRepository>();
            _gameService = new GameService(_gameRepository.Object);
            _gameController = new GameController(_gameRepository.Object, _gameService);
        }

        [TestMethod]
        public void GetGameModes_Success()
        {
            // Arrange
            var numEnums = Enum.GetNames(typeof(Mode)).Length;

            // Act
            var result = _gameController.Modes();
            var contentResult = result as OkNegotiatedContentResult<IEnumerable<string>>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(numEnums, contentResult.Content.Count());
            foreach(var mode in contentResult.Content)
            {
                var isValidMode = Enum.TryParse<Mode>(mode, true, out var tmp);
                Assert.IsTrue(isValidMode);
            }
        }

        [TestMethod]
        public void GetGame_Success()
        {
            // Arrange
            const int numFacesOrNames = 6;

            // Act
            var normalData = _gameController.Get() as OkNegotiatedContentResult<Game>;
            var reverseData = _gameController.Get(nameof(Mode.Reverse)) as OkNegotiatedContentResult<Game>;
            var mattData = _gameController.Get(nameof(Mode.Matt)) as OkNegotiatedContentResult<Game>;
            var engineerData = _gameController.Get(nameof(Mode.Engineer)) as OkNegotiatedContentResult<Game>;

            // Assert
            // Normal
            Assert.IsNotNull(normalData);
            Assert.AreEqual(nameof(Mode.Normal), normalData.Content.Mode);
            Assert.AreEqual(numFacesOrNames, normalData.Content.Faces.Count());
            Assert.AreEqual(1, normalData.Content.Names.Count());

            // Reverse
            Assert.IsNotNull(reverseData);
            Assert.AreEqual(nameof(Mode.Reverse), reverseData.Content.Mode);
            Assert.AreEqual(1, reverseData.Content.Faces.Count());
            Assert.AreEqual(numFacesOrNames, reverseData.Content.Names.Count());
            
            // Matt
            Assert.IsNotNull(mattData);
            Assert.AreEqual(nameof(Mode.Matt), mattData.Content.Mode);
            Assert.AreEqual(numFacesOrNames, mattData.Content.Faces.Count());
            Assert.AreEqual(1, mattData.Content.Names.Count());
            Assert.IsTrue(mattData.Content.Names.FirstOrDefault().StartsWith("Mat"));

            // Engineer
            Assert.IsNotNull(engineerData);
            Assert.AreEqual(nameof(Mode.Engineer), engineerData.Content.Mode);
            Assert.AreEqual(numFacesOrNames, engineerData.Content.Faces.Count());
            Assert.AreEqual(1, engineerData.Content.Names.Count());
        }

        [TestMethod]
        public void Guess_Success()
        {
            // Arrange
            // Don't do anything.
            _gameRepository.Setup(r => r.UpdateUserGuess(It.IsAny<bool>()));

            var correctGuess = new Guess
            {
                Face = @"//images.ctfassets.net/3cttzl4i3k1h/4IqIPfZlFeK4SMC2GCSE4q/b29bf93afec3cab2a55f6e6a8c98a351/headshot_sean_harvey.jpg",
                Name = "Sean Harvey"
            };
            var incorrectGuess = new Guess
            {
                Face = @"//someimage",
                Name = "Joe Nobody"
            };

            // Act
            var correctResponse = _gameController.Guess(correctGuess) as OkNegotiatedContentResult<bool>;
            var incorrectResponse = _gameController.Guess(incorrectGuess) as OkNegotiatedContentResult<bool>;

            // Assert
            Assert.IsNotNull(correctResponse);
            Assert.IsNotNull(incorrectResponse);
            Assert.IsTrue(correctResponse.Content);
            Assert.IsFalse(incorrectResponse.Content);
            _gameRepository.Verify(s => s.UpdateUserGuess(true), Times.Once);
            _gameRepository.Verify(s => s.UpdateUserGuess(false), Times.Once);
        }

        [TestMethod]
        public void GetLeaderboard_Success()
        {
            // Arrange
            var leaderBoard = new List<UserStatistics>
            {
                new UserStatistics
                {
                    CorrectGuesses = 1,
                    PercentCorrect = 50.0,
                    TotalGuesses = 2,
                    User = "user0"
                },
                new UserStatistics
                {
                    CorrectGuesses = 14,
                    PercentCorrect = 70.0,
                    TotalGuesses = 20,
                    User = "user1"
                },
                new UserStatistics
                {
                    CorrectGuesses = 2,
                    PercentCorrect = 5.0,
                    TotalGuesses = 40,
                    User = "user2"
                }
            };
            var numUsers = leaderBoard.Count;
            _gameRepository.Setup(s => s.GetStatistics()).Returns(leaderBoard);

            // Act
            var fullBoardResponse = _gameController.GetLeaderboard() as OkNegotiatedContentResult<IEnumerable<UserStatistics>>;
            var oneLessBoardResponse = _gameController.GetLeaderboard(numUsers - 1) as OkNegotiatedContentResult<IEnumerable<UserStatistics>>;

            // Assert
            Assert.IsNotNull(fullBoardResponse);
            Assert.IsNotNull(oneLessBoardResponse);
            Assert.IsNotNull(fullBoardResponse.Content);
            Assert.IsNotNull(oneLessBoardResponse.Content);
            Assert.AreEqual(numUsers, fullBoardResponse.Content.Count());
            Assert.AreEqual(numUsers - 1, oneLessBoardResponse.Content.Count());
            _gameRepository.Verify(s => s.GetStatistics(), Times.Exactly(2));
        }
    }
}
