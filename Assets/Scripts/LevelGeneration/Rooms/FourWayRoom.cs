namespace LevelGeneration.Rooms
{
    public class FourWayRoom : Room
    {
        public FourWayRoom(int x, int z, int orientation) : base(x, z, orientation)
        {
        }

        public override bool HasDoorwayTo(int direcion)
        {
            // A 4 way room has a doorway in every direction
            return true;
        }
    }
}