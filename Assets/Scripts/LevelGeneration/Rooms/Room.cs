using System;

namespace LevelGeneration.Rooms
{
    public class Room
    {
        public readonly int X;
        public readonly int Z;
        public readonly int Orientation;

        public bool Beginning;

        // All rooms need to know their location, orientation and shape.
        // Rooms also keep track of the enemies, loot, and specialities (i.e. staircases).
        protected Room(int x, int z, int orientation)
        {
            X = x;
            Z = z;
            Orientation = orientation;
            Beginning = false;
        }

        public virtual bool HasDoorwayTo(int direction)
        {
            // Implement in children
            throw new Exception("All child rooms have to implement HasDoorwayTo(int direction)!");
        }
    }
}