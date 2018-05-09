using System;

namespace LevelGeneration.Rooms
{
    /// <summary>
    /// Base class for Room objects
    /// </summary>
    public class Room
    {
        // All rooms need to know their location, orientation and shape.
        // Rooms also keep track of the enemies, whether they're the beginning or end of a level,
        // and whether their southern or western wall clips with another room.
        
        // X and Z as their index in a two-dimensional array, not as Unity distance values.
        public readonly int X;
        public readonly int Z;
        // Orientation is saved as int, 0-3, corresponding to directions defined in LevelGenerator as constants.
        // 0 means default (south), 1 means to west (turned 90 degrees), and so on.
        public readonly int Orientation;
        public int Enemies;
        public bool Beginning;
        public bool End;
        public bool HideSouthernWall;
        public bool HideWesternWall;

        protected Room(int x, int z, int orientation)
        {
            X = x;
            Z = z;
            Orientation = orientation;
            Enemies = 0;
            Beginning = false;
            End = false;
            HideSouthernWall = false;
            HideWesternWall = false;
        }

        /// <summary>
        /// Checks if the room has a doorway to the specified direction. Implemented in children.
        /// </summary>
        /// <param name="direction">Direction to check. 0 is south, 1 is west, and so on.</param>
        /// <returns>True if room has a doorway to the direction, false otherwise.</returns>
        /// <exception cref="Exception">If trying to call from Room. Should call from children.</exception>
        public virtual bool HasDoorwayTo(int direction)
        {
            // Implement in children
            throw new Exception("All child rooms have to implement HasDoorwayTo(int direction)!");
        }
    }
}