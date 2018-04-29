using System;
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
        public GameObject Player;

        private void Start()
        {
            _levelGenerator = new LevelGenerator();
            // TODO: Generating a level takes a while; add a loading screen
            GenerateLevel();
        }

        public void GenerateLevel()
        {
            Room[,] level = _levelGenerator.GenerateLevel(EdgeLength);
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
                    GameObject builtRoom;
                    switch (room.GetType().Name)
                    {
                        case "FourWayRoom":
                            builtRoom = Instantiate(FourWayRoom, position, rotation);
                            break;
                        case "CorridorRoom":
                            builtRoom = Instantiate(CorridorRoom, position, rotation);
                            break;
                        case "CornerRoom":
                            builtRoom = Instantiate(CornerRoom, position, rotation);
                            break;
                        case "ThreeWayRoom":
                            builtRoom = Instantiate(ThreeWayRoom, position, rotation);
                            break;
                        case "DeadEndRoom":
                            builtRoom = Instantiate(DeadEndRoom, position, rotation);
                            break;
                        default:
                            throw new Exception("Trying to add a room not yet implemented. Room name was: " +
                                                room.GetType().Name);
                    }

                    // Overlapping walls cause a glitch so we disable all overlapping walls
                    // What makes this ugly is that because we're rotating the rooms, we have to check which walls are the new southern and western walls
                    // TODO: Make prettier?
                    if (room.HideSouthernWall)
                    {
                        switch (room.Orientation)
                        {
                            case 0:
                                builtRoom.transform.Find("Wall_south").gameObject.SetActive(false);
                                break;
                            case 1:
                                builtRoom.transform.Find("Wall_east").gameObject.SetActive(false);
                                break;
                            case 2:
                                builtRoom.transform.Find("Wall_north").gameObject.SetActive(false);
                                break;
                            case 3:
                                builtRoom.transform.Find("Wall_west").gameObject.SetActive(false);
                                break;
                        }
                    }

                    if (room.HideWesternWall)
                    {
                        switch (room.Orientation)
                        {
                            case 0:
                                builtRoom.transform.Find("Wall_west").gameObject.SetActive(false);
                                break;
                            case 1:
                                builtRoom.transform.Find("Wall_south").gameObject.SetActive(false);
                                break;
                            case 2:
                                builtRoom.transform.Find("Wall_east").gameObject.SetActive(false);
                                break;
                            case 3:
                                builtRoom.transform.Find("Wall_north").gameObject.SetActive(false);
                                break;
                        }
                    }

                    // Place the player in the start of dungeon
                    if (room.Beginning)
                    {
                        Vector3 playersPosition = position;
                        // Rooms are generated at y=0; Player's origin is at 1
                        playersPosition.y = 1;
                        Player.transform.position = playersPosition;
                    }
                }
            }
        }
    }
}