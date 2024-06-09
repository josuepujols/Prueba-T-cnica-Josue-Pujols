using NUnit.Framework;
using RouletteGame.Core.Application.Dtos;
using RouletteGame.Core.Application.Interfaces;
using RouletteGame.Core.Application.Services;

namespace Test
{
    [TestFixture]
    public class RouletteTest
    {
        private IRouletteGameService _rouletteGameService;
        private static readonly string[] _actualColors = ["Red", "Black"];

        [SetUp]
        public void SetUp() => _rouletteGameService = new RouletteGameService();

        [Test]
        public void GetRoulette_ShouldReturnValidRouletteDto()
        {
            // Act
            var result = _rouletteGameService.GetRoulette();
            Assert.Multiple(() =>
            {

                // Assert
                Assert.That(result.Success, Is.True);
                Assert.That(result.Data, Is.Not.Null);
            });
            Assert.That(result.Data?.Number, Is.InRange(0, 36));
            Assert.That(_actualColors, Does.Contain(result.Data.Color));
        }

        [Test]
        public void BetColor_ShouldWinBet_WhenColorsMatch()
        {
            // Arrange
            var betRequest = new BetRequestDto
            {
                ColorSelected = "Red",
                ColorRoulette = "Red",
                Bet = 100,
                Amount = 1000
            };

            // Act
            var result = _rouletteGameService.BetColor(betRequest);
            Assert.Multiple(() =>
            {

                // Assert
                Assert.That(result.Success, Is.True);
                Assert.That(result.Data?.Amount, Is.EqualTo(1050));
            });
        }

        [Test]
        public void BetColor_ShouldLoseBet_WhenColorsDoNotMatch()
        {
            // Arrange
            var betRequest = new BetRequestDto
            {
                ColorSelected = "Red",
                ColorRoulette = "Black",
                Bet = 100,
                Amount = 1000
            };

            // Act
            var result = _rouletteGameService.BetColor(betRequest);

            // Assert
            Assert.IsFalse(result.Data?.IsWin);
            Assert.That(result.Data?.Amount, Is.EqualTo(900));
        }

        [Test]
        public void BetNumberAndColor_ShouldWinBet_WhenNumberAndColorMatch()
        {
            // Arrange
            var betRequest = new BetNumberAndColorRequestDto
            {
                NumberSelected = 5,
                NumberRoulette = 5,
                ColorSelected = "Red",
                ColorRoulette = "Red",
                Bet = 100,
                Amount = 1000
            };

            // Act
            var result = _rouletteGameService.BetNumberAndColor(betRequest);

            // Assert
            Assert.IsTrue(result.Success);
            Assert.That(result.Data?.Amount, Is.EqualTo(1300));
        }

        [Test]
        public void BetNumberAndColor_ShouldLoseBet_WhenNumberOrColorDoNotMatch()
        {
            // Arrange
            var betRequest = new BetNumberAndColorRequestDto
            {
                NumberSelected = 5,
                NumberRoulette = 6,
                ColorSelected = "Red",
                ColorRoulette = "Black",
                Bet = 100,
                Amount = 1000
            };

            // Act
            var result = _rouletteGameService.BetNumberAndColor(betRequest);

            // Assert
            Assert.IsFalse(result.Data?.IsWin);
            Assert.That(result.Data?.Amount, Is.EqualTo(900));
        }

        [Test]
        public void BetOddOrEven_ShouldWinBet_WhenOddEvenAndColorMatch()
        {
            // Arrange
            var betRequest = new BetOddOrEvenRequestDto
            {
                IsOdd = true,
                NumberRoulette = 5,
                ColorSelected = "Red",
                ColorRoulette = "Red",
                Bet = 100,
                Amount = 1000
            };

            // Act
            var result = _rouletteGameService.BetOddOrEven(betRequest);

            // Assert
            Assert.That(result.Success, Is.True);
            Assert.That(result.Data?.Amount, Is.EqualTo(1100));
        }

        [Test]
        public void BetOddOrEven_ShouldLoseBet_WhenOddEvenOrColorDoNotMatch()
        {
            // Arrange
            var betRequest = new BetOddOrEvenRequestDto
            {
                IsOdd = true,
                NumberRoulette = 4,
                ColorSelected = "Red",
                ColorRoulette = "Black",
                Bet = 100,
                Amount = 1000
            };

            // Act
            var result = _rouletteGameService.BetOddOrEven(betRequest);

            // Assert
            Assert.That(result.Data?.IsWin, Is.False);
            Assert.That(result.Data?.Amount, Is.EqualTo(900));
        }
    }
}