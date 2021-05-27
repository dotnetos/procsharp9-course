using System;
using Xunit;

namespace GameEngine.Tests
{
    public class BasicTests
    {
        [Fact]
        public void GivenNullCommand_WhenProcess_ThenThrows()
        {
            var service = new GameService(Player.A);

            CommandDTO command = null;

            var exception = Record.Exception(() => service.ProcessCommand(command));

            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public void GivenInvalidIndex_WhenProcess_ThenThrows()
        {
            var service = new GameService(Player.A);

            var command = new CommandDTO(-1, Player.A, MoveType.DrawCard);

            var exception = Record.Exception(() => service.ProcessCommand(command));

            Assert.NotNull(exception);
            Assert.IsType<InvalidMoveException>(exception);
            Assert.Equal("Invalid game index", exception.Message);
        }

        [Fact]
        public void GivenTooBigIndex_WhenProcess_ThenThrows()
        {
            var service = new GameService(Player.A);

            var command = new CommandDTO(GameService.MaxMoves + 1, Player.A, MoveType.DrawCard);

            var exception = Record.Exception(() => service.ProcessCommand(command));

            Assert.NotNull(exception);
            Assert.IsType<InvalidMoveException>(exception);
            Assert.Equal("Invalid game index", exception.Message);
        }

        [Fact]
        public void GivenInvalidSideA_WhenProcess_ThenThrows()
        {
            var service = new GameService(Player.B);

            var command = new CommandDTO(0, Player.A, MoveType.DrawCard);

            var exception = Record.Exception(() => service.ProcessCommand(command));

            Assert.NotNull(exception);
            Assert.IsType<InvalidMoveException>(exception);
            Assert.Equal("Invalid player side", exception.Message);
        }

        [Fact]
        public void GivenInvalidSideB_WhenProcess_ThenThrows()
        {
            var service = new GameService(Player.A);

            var command = new CommandDTO(0, Player.B, MoveType.DrawCard);

            var exception = Record.Exception(() => service.ProcessCommand(command));

            Assert.NotNull(exception);
            Assert.IsType<InvalidMoveException>(exception);
            Assert.Equal("Invalid player side", exception.Message);
        }
    }
}
