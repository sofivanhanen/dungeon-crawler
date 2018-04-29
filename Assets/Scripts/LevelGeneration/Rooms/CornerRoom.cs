namespace LevelGeneration.Rooms
{
	public class CornerRoom : Room
	{
		public CornerRoom(int x, int z, int orientation) : base(x, z, orientation)
		{
		}

		public override bool HasDoorwayTo(int direction)
		{
			// Corner has a doorway to the direction it's 'coming' from, and the next direction (+1)
			return (direction == Orientation || direction == (Orientation + 1) % 4);
		}
	}
}