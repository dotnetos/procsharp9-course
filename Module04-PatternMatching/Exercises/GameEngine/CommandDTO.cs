namespace GameEngine
{
    public record CommandDTO(int Index, Player PlayerSide, MoveType Move);

    public enum Player
    {
        A,
        B
    }

    public enum MoveType
    {
        PlayCard,
        DrawCard,
        Skip
    }
}