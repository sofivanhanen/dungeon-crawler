﻿using System;
using LevelGeneration;
using LevelGeneration.Rooms;
using UnityEngine;

namespace GameObjectControllers
{
    public class GameMasterController : MonoBehaviour
    {
        private LevelGenerator _levelGenerator;

        // Variables set in inspector
        public int EdgeLength;
        public GameObject DeadEndRoom;
        public GameObject CorridorRoom;
        public GameObject CornerRoom;
        public GameObject ThreeWayRoom;
        public GameObject FourWayRoom;

        private void Start()
        {
            _levelGenerator = new LevelGenerator(EdgeLength);
            // TODO: Generating a level takes a while; add a loading screen
            GenerateLevel();
        }

        public void GenerateLevel()
        {
            Room[,] level = _levelGenerator.GenerateLevel();
            for (int x = 0; x < EdgeLength; x++)
            {
                for (int z = 0; z < EdgeLength; z++)
                {
                    Room room = level[x, z];
                    if (room == null) continue;
                    Vector3 position = new Vector3();
                    position.x = ((0 - EdgeLength / 2) + room.X) * 10;
                    position.z = ((0 - EdgeLength / 2) + room.Z) * 10;
                    Quaternion rotation = Quaternion.Euler(0, 90 * room.Orientation, 0);
                    // TODO: Fix the stuttering that happens with overlapping walls
                    if (room.GetType().Name == "FourWayRoom")
                    {
                        Instantiate(FourWayRoom, position, rotation);
                    }
                    else if (room.GetType().Name == "CorridorRoom")
                    {
                        Instantiate(CorridorRoom, position, rotation);
                    }
                    else if (room.GetType().Name == "CornerRoom")
                    {
                        Instantiate(CornerRoom, position, rotation);
                    }
                    else if (room.GetType().Name == "ThreeWayRoom")
                    {
                        Instantiate(ThreeWayRoom, position, rotation);
                    }
                    else if (room.GetType().Name == "DeadEndRoom")
                    {
                        Instantiate(DeadEndRoom, position, rotation);
                    }
                    else
                    {
                        throw new Exception("Trying to add a room not yet implemented. Room name was: " +
                                            room.GetType().Name);
                    }
                }
            }
        }
    }
}