using System;
using LevelGeneration.Rooms;
using Random = System.Random;

namespace LevelGeneration
{
    public class LevelGenerator
    {
        public static int South = 0;
        public static int West = 1;
        public static int North = 2;
        public static int East = 3;

        public static int Dead = 10;
        public static int Corridor = 11;
        public static int Corner = 12;
        public static int ThreeWay = 13;
        public static int FourWay = 14;

        private int _edgeSize;
        private Room[,] _level;
        private Random random;

        public LevelGenerator(int edgeSize)
        {
            if (edgeSize < 3)
            {
                throw new Exception("Cannot generate level smaller than 3x3");
            }

            _edgeSize = edgeSize;
            random = new Random();
        }

        public Room[,] GenerateLevel()
        {
            // Setup
            _level = new Room[_edgeSize, _edgeSize];

            // Choose starting place
            int x = random.Next(1, _edgeSize - 1);
            int z = random.Next(1, _edgeSize - 1);
            _level[x, z] = new FourWayRoom(x, z, 0);
            _level[x, z].Beginning = true; // TODO: 'Beginning' doesn't yet do anything

            // Starting the iterative generation
            ContinueFrom(_level[x, z]);

            // Generation finished, return room
            return _level;
            // TODO: Generate monsters and stairways
        }

        private void ContinueFrom(Room room)
        {
            // TODO: Randomize the direction we start in
            int x = room.X;
            int z = room.Z;
            // TODO: Copy-paste here too
            if (room.HasDoorwayTo(South) && _level[x, z - 1] == null)
            {
                Generate(x, z - 1, North);
            }

            if (room.HasDoorwayTo(West) && _level[x - 1, z] == null)
            {
                Generate(x - 1, z, East);
            }

            if (room.HasDoorwayTo(North) && _level[x, z + 1] == null)
            {
                Generate(x, z + 1, South);
            }

            if (room.HasDoorwayTo(East) && _level[x + 1, z] == null)
            {
                Generate(x + 1, z, West);
            }
        }

        private void Generate(int x, int z, int comingFrom)
        {
            int shape;
            int orientation;
            Room room;
            while (true)
            {
                // Only dead end rooms for now
                shape = random.Next(Dead, Dead + 1);
                // Testing with every orientation
                orientation = random.Next(0, 4);
                for (int i = 0; i < 4; i++)
                {
                    room = GenerateRoom(x, z, orientation % 4, shape);
                    if (IsValid(room, comingFrom))
                    {
                        // Found a nicely fitting room. Moving on.
                        goto finish;
                    }

                    orientation++;
                }
            }

            finish:
            {
                _level[x, z] = room;
                ContinueFrom(room);
            }
        }

        private Room GenerateRoom(int x, int z, int orientation, int shape)
        {
            // TODO Add cases for all rooms
            if (shape == Dead)
            {
                return new DeadEndRoom(x, z, orientation);
            }
            else if (shape == FourWay)
            {
                return new FourWayRoom(x, z, orientation);
            }

            throw new Exception("Trying to add a room not yet implemented");
        }

        private bool IsValid(Room room, int comingFrom)
        {
            // TODO: Fix this horrible copy-paste
            // Checking from south
            if (comingFrom == South)
            {
                if (!room.HasDoorwayTo(South)) return false;
            }
            else
            {
                if (room.HasDoorwayTo(South))
                {
                    try
                    {
                        if (_level[room.X, room.Z - 1] != null)
                        {
                            // Room has doorway to here, but the path is already taken
                            return false;
                        }
                    }
                    catch (Exception e)
                    {
                        // Reached end of legal area
                        return false;
                    }
                }
            }

            // Checking from west
            if (comingFrom == West)
            {
                if (!room.HasDoorwayTo(West)) return false;
            }
            else
            {
                if (room.HasDoorwayTo(West))
                {
                    try
                    {
                        if (_level[room.X - 1, room.Z] != null)
                        {
                            return false;
                        }
                    }
                    catch (Exception e)
                    {
                        return false;
                    }
                }
            }

            // Checking from north
            if (comingFrom == North)
            {
                if (!room.HasDoorwayTo(North)) return false;
            }
            else
            {
                if (room.HasDoorwayTo(North))
                {
                    try
                    {
                        if (_level[room.X, room.Z + 1] != null)
                        {
                            return false;
                        }
                    }
                    catch (Exception e)
                    {
                        return false;
                    }
                }
            }

            // Checking from east
            if (comingFrom == East)
            {
                if (!room.HasDoorwayTo(East)) return false;
            }
            else
            {
                if (room.HasDoorwayTo(East))
                {
                    try
                    {
                        if (_level[room.X + 1, room.Z] != null)
                        {
                            return false;
                        }
                    }
                    catch (Exception e)
                    {
                        return false;
                    }
                }
            }

            // Wow, we got this far. In that case the room is valid.
            return true;
        }

        public void ModifySize(int newEdgeSize)
        {
            _edgeSize = newEdgeSize;
        }
    }
}