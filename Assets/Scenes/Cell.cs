using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    [System.Serializable]
    public struct Wall
    {
        public Direction direction;
        
        public Transform transform;
    }

    [HideInInspector] public IntVector2 coordinate;

    public bool seen;
    
    public List<Wall> walls;

    public void DisableWall(Direction direction)
    {
        walls.Find(w => w.direction == direction).transform.gameObject.SetActive(false);
    }
}
