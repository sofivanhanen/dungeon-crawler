namespace LevelGeneration.Rooms
{
	public class ThreeWayRoom : Room
	{
		public ThreeWayRoom(int x, int z, int orientation) : base(x, z, orientation)
		{
		}

		public override bool HasDoorwayTo(int direction)
		{
			// A 3 way room has a doorway to the specified direction, and +1 and -1 (which is same as +3).
			return direction == Orientation || direction == (Orientation + 1) % 4 || direction == (Orientation + 3) % 4;
		}
	}
}