using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public enum Direction
    {
        North,
        East,
        South,
        West
    }

    public enum BlockType
    {
        Team1Archer,
        Team1Knight,
        Team1Wizard,
        Team2Archer,
        Team2Knight,
        Team2Wizard,
        RougeWizard,
        Team1Factory,
        Team1Resource,
        Team2Factory,
        Team2Resource,
        Wall,
        OpenSpace
    }

    public int MapSize;
    public GameObject OpenSpace;
    public GameObject Border;
    public GameObject[] Buildings;
    public GameObject[] Units;

    BlockType[,] map;
    int posX;
    int posZ;

    // Start is called before the first frame update
    void Start()
    {
        InitializeDungeon();
        PlaceBuildings(4);
        PlaceUnits(50);
        Display();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void InitializeDungeon()
    {
        map = new BlockType[MapSize, MapSize];

        for (int i = 0; i < MapSize; i++)
        {
            for (int j = 0; j < MapSize; j++)
            {
                map[i, j] = BlockType.OpenSpace;
            }
        }
        //Creates Boundries for the map
        for (int x = 0; x < MapSize; x++)
        {
            map[x, MapSize - 1] = BlockType.Wall;
        }
        for (int x = 0; x < MapSize; x++)
        {
            map[x, 0] = BlockType.Wall;
        }
        for (int z = 0; z < MapSize; z++)
        {
            map[MapSize - 1, z] = BlockType.Wall ;
        }
        for (int z = 0; z < MapSize; z++)
        {
            map[0, z] = BlockType.Wall;
        }
    }
    //Unit and Map Display
    private void Display()
    {
        GameObject[] tiles = GameObject.FindGameObjectsWithTag("tile");
        foreach (GameObject g in tiles)
        {
            Destroy(g);
        }

        for (int x = 0; x < MapSize; x++)
        {
            for (int z = 0; z < MapSize; z++)
            {
                switch (map[x, z])
                {
                    case BlockType.OpenSpace:
                        Instantiate(OpenSpace, new Vector3(x, 1f, z), Quaternion.identity);
                        break;
                    case BlockType.Wall:
                        Instantiate(Border, new Vector3(x, 1f, z), Quaternion.identity);
                        break;
                    case BlockType.Team1Factory:
                        Instantiate(Buildings[0], new Vector3(x, 1.5f, z), Quaternion.identity);
                        break;
                    case BlockType.Team1Resource:
                        Instantiate(Buildings[1], new Vector3(x, 1.5f, z), Quaternion.identity);
                        break;
                    case BlockType.Team2Factory:
                        Instantiate(Buildings[2], new Vector3(x, 1.5f, z), Quaternion.identity);
                        break;
                    case BlockType.Team2Resource:
                        Instantiate(Buildings[3], new Vector3(x, 1.5f, z), Quaternion.identity);
                        break;
                    case BlockType.Team1Archer:
                        Instantiate(Units[0], new Vector3(x, 1f, z), Quaternion.identity);
                        break;
                    case BlockType.Team1Knight:
                        Instantiate(Units[1], new Vector3(x, 1f, z), Quaternion.identity);
                        break;
                    case BlockType.Team1Wizard:
                        Instantiate(Units[2], new Vector3(x, 1f, z), Quaternion.identity);
                        break;
                    case BlockType.Team2Archer:
                        Instantiate(Units[3], new Vector3(x, 1f, z), Quaternion.identity);
                        break;
                    case BlockType.Team2Knight:
                        Instantiate(Units[4], new Vector3(x, 1f, z), Quaternion.identity);
                        break;
                    case BlockType.Team2Wizard:
                        Instantiate(Units[5], new Vector3(x, 1f, z), Quaternion.identity);
                        break;
                    case BlockType.RougeWizard:
                        Instantiate(Units[6], new Vector3(x, 1f, z), Quaternion.identity);
                        break;

                }
            }
        }
    }
    //Spawns Buildings
    private void PlaceBuildings(int numBuildings)
    {
        for (int i = 0; i < numBuildings; i++)
        {
            int x = Random.Range(1, MapSize - 1);
            int z = Random.Range(1, MapSize - 1);
            if (map[x, z] == BlockType.OpenSpace)
            {
                map[x, z] = (BlockType)Random.Range(0,4);
            }
            else
            {
                i--;
            }
        }
    }
    //Spawns Units
    private void PlaceUnits(int numUnits)
    {
        for (int i = 0; i < numUnits; i++)
        {
            int x = Random.Range(1, MapSize - 1);
            int z = Random.Range(1, MapSize - 1);
            if (map[x, z] == BlockType.OpenSpace)
            {
                map[x, z] = (BlockType)Random.Range(0, 7);
            }
            else
            {
                i--;
            }
        }
    }
}
