namespace LevelGeneration.Rooms
{
	public class CorridorRoom : Room
	{
		public CorridorRoom(int x, int z, int orientation) : base(x, z, orientation)
		{
		}

		public override bool HasDoorwayTo(int direction)
		{
			// A corridor has a doorway to the direction it's 'coming' from. and to the opposite direction.
			return (direction == Orientation || direction == (Orientation + 2) % 4);
		}
	}
}