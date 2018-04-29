using System;
using LevelGeneration.Rooms;
using Random = System.Random;

namespace LevelGeneration
{
    public class LevelGenerator
    {
        private const int South = 0;
        private const int West = 1;
        private const int North = 2;
        private const int East = 3;

        private const int Dead = 10;
        private const int Corridor = 11;
        private const int Corner = 12;
        private const int ThreeWay = 13;
        private const int FourWay = 14;

        private int _edgeSize;
        private Room[,] _level;
        private readonly Random _random;

        public LevelGenerator()
        {
            _random = new Random();
        }

        public Room[,] GenerateLevel(int edgeSize)
        {
            // Setup
            if (edgeSize < 3)
            {
                throw new Exception("Cannot generate level smaller than 3x3");
            }

            _edgeSize = edgeSize;
            _level = new Room[_edgeSize, _edgeSize];
            
            // Choose starting place
            int x = _random.Next(1, _edgeSize - 1);
            int z = _random.Next(1, _edgeSize - 1);
            _level[x, z] = new FourWayRoom(x, z, 0) {Beginning = true};

            // Starting the recursive generation
            ContinueFrom(_level[x, z]);

            // Generation finished, return room
            return _level;
            // TODO: Generate monsters and stairways
        }

        private void ContinueFrom(Room room)
        {
            int x = room.X;
            int z = room.Z;
            int direction = _random.Next(0, 4);
            for (int i = 0; i < 4; i++)
            {
                if (room.HasDoorwayTo(direction % 4) && GetRoomInDirection(room, direction % 4) == null)
                {
                    // Generating a room
                    switch (direction % 4)
                    {
                        case South:
                            Generate(x, z - 1, North);
                            break;
                        case West:
                            Generate(x - 1, z, East);
                            break;
                        case North:
                            Generate(x, z + 1, South);
                            break;
                        case East:
                            Generate(x + 1, z, West);
                            break;
                    }
                }
                
                direction++;
            }
        }

        private void Generate(int x, int z, int comingFrom)
        {
            int shape;
            int orientation;
            Room room;
            while (true)
            {
                shape = _random.Next(Dead, FourWay + 1);
                // Testing with every orientation
                orientation = _random.Next(0, 4);
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

        private static Room GenerateRoom(int x, int z, int orientation, int shape)
        {
            switch (shape)
            {
                case Dead:
                    return new DeadEndRoom(x, z, orientation);
                case Corridor:
                    return new CorridorRoom(x, z, orientation);
                case Corner:
                    return new CornerRoom(x, z, orientation);
                case ThreeWay:
                    return new ThreeWayRoom(x, z, orientation);
                case FourWay:
                    return new FourWayRoom(x, z, orientation);
                default:
                    throw new Exception("Trying to add a room not yet implemented");
            }
        }

        private bool IsValid(Room room, int comingFrom)
        {
            // Checking every direction
            for (int direction = 0; direction < 4; direction++)
            {
                if (comingFrom == direction)
                {
                    if (!room.HasDoorwayTo(direction)) return false;
                }
                else
                {
                    if (room.HasDoorwayTo(direction))
                    {
                        try
                        {
                            if (GetRoomInDirection(room, direction) != null)
                            {
                                // Room has doorway to here, but the path is already taken
                                // If the blocking door also has a nicely fitting doorway (to the opposite direction so they meet), this is a match
                                if (!GetRoomInDirection(room, direction).HasDoorwayTo((direction + 2) % 4)) return false;
                            }
                        }
                        catch (Exception)
                        {
                            // Reached end of legal area, though there should be space here
                            return false;
                        }
                    }
                    else
                    {
                        // Room does not have a doorway to specific direction: Now we have to check that the neighbouring room is not trying to access this room
                        try
                        {
                            if (GetRoomInDirection(room, direction) != null)
                            {
                                // There is a room here
                                // If the other room has a doorway to where we're generating, this is not a match
                                if (GetRoomInDirection(room, direction).HasDoorwayTo((direction + 2) % 4)) return false;
                            }
                        }
                        catch (Exception)
                        {
                            // Reached end of legal area
                        }
                    }
                }
            }

            // Wow, we got this far. In that case the room is valid.
            return true;
        }

        private Room GetRoomInDirection(Room room, int direction)
        {
            switch (direction)
            {
                case South:
                    return _level[room.X, room.Z - 1];
                case North:
                    return _level[room.X, room.Z + 1];
                case West:
                    return _level[room.X - 1, room.Z];
                case East:
                    return _level[room.X + 1, room.Z];
                default:
                    // Direction not correct?
                    throw new Exception("Couldn't find room in direction" + direction);
            }
        }
    }
}