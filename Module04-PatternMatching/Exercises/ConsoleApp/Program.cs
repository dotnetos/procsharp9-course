using GameEngine;
using System;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var engine = new GameService(Player.A);
            int index = 0;
            while (true)
            {
                try
                {
                    Console.Write("> ");
                    string input = Console.ReadLine();
                    if (Enum.TryParse<MoveType>(input, out var move))
                    {
                        var command = new CommandDTO(index++, Player.A, move);
                        engine.ProcessCommand(command);
                    }
                    else
                        Console.WriteLine("?!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }
    }
}
