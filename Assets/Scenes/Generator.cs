using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public enum Direction
{
    North = 0,
    South = 1,
    East = 2,
    West = 3
}

[System.Serializable]
public struct IntVector2
{
    public int x;
    public int z;

    public IntVector2(int x, int z)
    {
        this.x = x;
        this.z = z;
    }
    
    public static IntVector2 operator + (IntVector2 a, IntVector2 b) {
        a.x += b.x;
        a.z += b.z;
        return a;
    }
    
    public static IntVector2 operator - (IntVector2 a, IntVector2 b) {
        a.x -= b.x;
        a.z -= b.z;
        return a;
    }
}

public class Generator : MonoBehaviour
{
    public Cell[,] cells;

    public Cell frontCell;

    public GameObject cellObj;

    public float cellSize;

    public IntVector2 size;

    public Dictionary<Direction, IntVector2> directionCoordinates = new Dictionary<Direction, IntVector2>
    {
        {Direction.North, new IntVector2(0, 1)},
        {Direction.South, new IntVector2(0, - 1)},
        {Direction.East, new IntVector2(1, 0)},
        {Direction.West, new IntVector2(- 1, 0)},
    };
    
    public Cell RandomCell => cells[Random.Range(0, size.x), Random.Range(0, size.z)];

    private List<Direction> _directions;
    
    private void Start()
    {
        Initialize();
        
        frontCell = RandomCell;
        
        _directions = Enum.GetValues(typeof(Direction)).Cast<Direction>().ToList();

        Traverse();
    }

    public void Initialize()
    {
        cells = new Cell[size.x,size.z];
        
        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.z; j++)
            {
                IntVector2 coordinate = new IntVector2(i, j);
                
                CreateCell(coordinate);
            }
        }
    }
    
    public void CreateCell(IntVector2 coordinate)
    {
        GameObject obj = Instantiate(cellObj, transform);

        obj.transform.localPosition = new Vector3(cellSize * coordinate.x, 0, cellSize * coordinate.z);
        
        Cell cell = obj.GetComponent<Cell>();

        cells[coordinate.x, coordinate.z] = cell;
        
        cell.coordinate = coordinate;
    }

    public void Traverse()
    {
        if (_directions.Count == 0)
        {
            if (GetTopMostUnSeenCell(out Cell unseenCell, out Cell seenAdjacentCell))
            {
                frontCell = unseenCell;

                frontCell.seen = true;
                
                //find and connect with random visited neighbor
                IntVector2 directionCoordinate = unseenCell.coordinate - seenAdjacentCell.coordinate;

                Direction directionToUnseen =
                    directionCoordinates.FirstOrDefault(x => x.Value.x == directionCoordinate.x && x.Value.z == directionCoordinate.z).Key;
                
                seenAdjacentCell.DisableWall(directionToUnseen);
                unseenCell.DisableWall(GetOpposite(directionToUnseen));
                
                _directions = Enum.GetValues(typeof(Direction)).Cast<Direction>().ToList();
                
                Traverse();
            }
            
            return;
        }
        
        Direction direction = _directions[Random.Range(0, _directions.Count)];

        _directions.Remove(direction);
        
        IntVector2 coordinate = frontCell.coordinate + directionCoordinates[direction];

        Cell adjacentCell = null;
        
        try
        {
            adjacentCell = cells[coordinate.x, coordinate.z];
        }
        
        catch (Exception e)
        {
            Traverse();
            
            return;
        }

        if (adjacentCell.seen)
        {
            Traverse();
        }

        else
        {
            adjacentCell.seen = true;

            frontCell.DisableWall(direction);
            
            frontCell = adjacentCell;
            
            frontCell.DisableWall(GetOpposite(direction));
            
            _directions = Enum.GetValues(typeof(Direction)).Cast<Direction>().ToList();
            
            Traverse();
        }
    }

    private bool FindVisitedAdjacentCell(Cell cell, out Cell seenAdjacentCell)
    {
        seenAdjacentCell = null;
        
        for (int i = 0; i < 4; i++)
        {
            IntVector2 coordinate = cell.coordinate + directionCoordinates[(Direction) i];
            
            try
            {
                Cell adjacentCell = cells[coordinate.x, coordinate.z];
                
                if (adjacentCell.seen)
                {
                    seenAdjacentCell = adjacentCell;

                    return true;
                }
            }
            
            catch (Exception e)
            {
                
            }
        }

        return false;
    }
    
    private bool GetTopMostUnSeenCell(out Cell cell, out Cell seenAdjacentCell)
    {
        cell = null;
        seenAdjacentCell = null;
        
        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.z; j++)
            {
                if (!cells[i, j].seen && FindVisitedAdjacentCell(cells[i, j], out Cell visitedAdjacentCell))
                {
                    cell = cells[i, j];
                    seenAdjacentCell = visitedAdjacentCell;

                    return true;
                }
            }
        }

        return false;
    }

    private Direction GetOpposite(Direction direction)
    {
        switch (direction)
        {
            case Direction.North:
                return Direction.South;
            case Direction.South:
                return Direction.North;
            case Direction.East:
                return Direction.West;
            case Direction.West:
                return Direction.East;
            default:
                return default;
        }
    }
}
