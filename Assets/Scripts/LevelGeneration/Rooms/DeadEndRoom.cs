namespace LevelGeneration.Rooms
{
    public class DeadEndRoom : Room
    {
        public DeadEndRoom(int x, int z, int orientation) : base(x, z, orientation)
        {
        }

        public override bool HasDoorwayTo(int direction)
        {
            // Dead end only has a doorway to the direction it's 'coming' from.
            return direction == Orientation;
        }
    }
}