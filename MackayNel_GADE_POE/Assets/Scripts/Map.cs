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

    public enum TileType
    {
        Team1Archer,
        Team1Knight,
        Team1Wizard,
        Team2Archer,
        Team2Knight,
        Team2Wizard,
        RougeWizard,
        Gold,
        Hero,
        Obstacle,
        OpenSpace
    }

    public int dungeonSize;
    public GameObject OpenSpace;
    public GameObject Hero;
    public GameObject Obstacle;
    public GameObject Gold;
    public GameObject[] enemies;

    TileType[,] dungeon;
    int posX;
    int posZ;
    int gold = 0;

    // Start is called before the first frame update
    void Start()
    {
        InitializeDungeon();
        PlaceObstacles(20);
        PlaceEnemies(30);
        PlaceCoins(10);
        PlaceHero();

        // =========================

        Display();
    }

    // Update is called once per frame
    void Update()
    {
        //Get keyboard movements (W, A, S, D)
        if (Input.GetKeyDown(KeyCode.W))
        {
            MoveCharacter(Direction.North);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            MoveCharacter(Direction.West);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            MoveCharacter(Direction.South);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            MoveCharacter(Direction.East);
        }
    }

    public void InitializeDungeon()
    {
        dungeon = new TileType[dungeonSize, dungeonSize];

        for (int i = 0; i < dungeonSize; i++)
        {
            for (int j = 0; j < dungeonSize; j++)
            {
                dungeon[i, j] = TileType.OpenSpace;
            }
        }
        //North wall
        for (int x = 0; x < dungeonSize; x++)
        {
            dungeon[x, dungeonSize - 1] = TileType.Obstacle;
        }
        //South wall
        for (int x = 0; x < dungeonSize; x++)
        {
            dungeon[x, 0] = TileType.Obstacle;
        }
        //East wall
        for (int z = 0; z < dungeonSize; z++)
        {
            dungeon[dungeonSize - 1, z] = TileType.Obstacle;
        }
        //West wall
        for (int z = 0; z < dungeonSize; z++)
        {
            dungeon[0, z] = TileType.Obstacle;
        }
    }

    private void Display()
    {
        GameObject[] tiles = GameObject.FindGameObjectsWithTag("tile");
        foreach (GameObject g in tiles)
        {
            Destroy(g);
        }

        for (int x = 0; x < dungeonSize; x++)
        {
            for (int z = 0; z < dungeonSize; z++)
            {
                switch (dungeon[x, z])
                {
                    case TileType.OpenSpace:
                        Instantiate(OpenSpace, new Vector3(x, 1f, z), Quaternion.identity);
                        break;
                    case TileType.Obstacle:
                        Instantiate(Obstacle, new Vector3(x, 1.5f, z), Quaternion.identity);
                        break;
                    case TileType.Gold:
                        Instantiate(Gold, new Vector3(x, 1f, z), Quaternion.identity);
                        break;
                    case TileType.Hero:
                        Instantiate(Hero, new Vector3(x, 1.8f, z), Quaternion.identity);
                        break;
                    case TileType.Team1Archer:
                        Instantiate(enemies[0], new Vector3(x, 1f, z), Quaternion.identity);
                        break;
                    case TileType.Team1Knight:
                        Instantiate(enemies[1], new Vector3(x, 1f, z), Quaternion.identity);
                        break;
                    case TileType.Team1Wizard:
                        Instantiate(enemies[2], new Vector3(x, 1.8f, z), Quaternion.identity);
                        break;
                    case TileType.Team2Archer:
                        Instantiate(enemies[3], new Vector3(x, 1f, z), Quaternion.identity);
                        break;
                    case TileType.Team2Knight:
                        Instantiate(enemies[4], new Vector3(x, 1f, z), Quaternion.identity);
                        break;
                    case TileType.Team2Wizard:
                        Instantiate(enemies[5], new Vector3(x, 1.8f, z), Quaternion.identity);
                        break;
                    case TileType.RougeWizard:
                        Instantiate(enemies[6], new Vector3(x, 1f, z), Quaternion.identity);
                        break;

                }
            }
        }
    }

    private void PlaceObstacles(int numObstacles)
    {
        for (int i = 0; i < numObstacles; i++)
        {
            int x = Random.Range(1, dungeonSize - 1);
            int z = Random.Range(1, dungeonSize - 1);
            if (dungeon[x, z] == TileType.OpenSpace)
            {
                dungeon[x, z] = TileType.Obstacle;
            }
            else
            {
                i--;
            }
        }
    }

    private void PlaceCoins(int numCoins)
    {
        for (int i = 0; i < numCoins; i++)
        {
            int x = Random.Range(1, dungeonSize - 1);
            int z = Random.Range(1, dungeonSize - 1);
            if (dungeon[x, z] == TileType.OpenSpace)
            {
                dungeon[x, z] = TileType.Gold;
            }
            else
            {
                i--;
            }
        }
    }

    private void PlaceHero()
    {
        for (int i = 0; i < 1; i++)
        {
            int x = Random.Range(1, dungeonSize - 1);
            int z = Random.Range(1, dungeonSize - 1);
            if (dungeon[x, z] == TileType.OpenSpace)
            {
                dungeon[x, z] = TileType.Hero;
                posX = x;
                posZ = z;
            }
            else
            {
                i--;
            }
        }
    }

    private void PlaceEnemies(int numEnemies)
    {
        for (int i = 0; i < numEnemies; i++)
        {
            int x = Random.Range(1, dungeonSize - 1);
            int z = Random.Range(1, dungeonSize - 1);
            if (dungeon[x, z] == TileType.OpenSpace)
            {
                dungeon[x, z] = (TileType)Random.Range(0, 6);
            }
            else
            {
                i--;
            }
        }
    }
    private void MoveCharacter(Direction dir)
    {
        int newX = posX;
        int newZ = posZ;
        switch (dir)
        {
            case Direction.North: newZ = posZ + 1; break;
            case Direction.South: newZ = posZ - 1; break;
            case Direction.East: newX = posX + 1; break;
            case Direction.West: newX = posX - 1; break;
        }
        if (dungeon[newZ, newX] == TileType.OpenSpace)
        {
            dungeon[posX, posZ] = TileType.OpenSpace;
            posX = newX;
            posZ = newZ;
            dungeon[posX, posZ] = TileType.Hero;
            Display();
        }
        else if (dungeon[newX, newZ] == TileType.Gold)
        {
            gold++;
            dungeon[posX, posZ] = TileType.OpenSpace;
            posX = newX;
            posZ = newZ;
            dungeon[posX, posZ] = TileType.Hero;
            Display();
        }
        else if (dungeon[newX, newZ] == TileType.Team1Archer)
        {

        }
    }
}
