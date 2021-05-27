using System;

namespace GameEngine
{
    public class GameService
    {
        private Player _currentMovePlayer;
        public const int MaxMoves = 128;

        public GameService(Player currentMovePlayer)
        {
            _currentMovePlayer = currentMovePlayer;
        }

        // TODO: Brain exercise :) Without changing Process method signature, change
        // ProcessCommand to use pattern matching and as little ifs as possible 
        // (or none at all, the best solution!)
        public bool ProcessCommand(CommandDTO command) {
            if (command is null)
                throw new ArgumentNullException(nameof(command));
            if (command.PlayerSide != _currentMovePlayer)
                throw new InvalidMoveException("Invalid player side");
            if (command.Index > MaxMoves || command.Index < 0)
                throw new InvalidMoveException("Invalid game index");
            return Process(command.PlayerSide, (uint)command.Index, command.Move);
        }

        private bool Process(Player player, uint index, MoveType move)
        {
            Console.WriteLine($"[#{index:D3}] {player} make {move} move.");
            return true;
        }
    }
}
