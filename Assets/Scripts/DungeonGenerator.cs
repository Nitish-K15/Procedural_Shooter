using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    public class Cell
    {
        public bool visited = false;
        public bool[] status = new bool[4];
    }

    [System.Serializable]
    public class Rule
    {
        public GameObject room;
        public Vector2Int minPosition;
        public Vector2Int maxPosition;

        public bool obligatory;
        public int frequency;

        public int ProbabilityofSpawn(int x,int y) //0 - cannot spawn,1 - can spawn, 2 - HAS to spawn
        {
            if(x >= minPosition.x && x<= maxPosition.x && y >= minPosition.y && y <= maxPosition.y)
            {
                if (frequency > 0)
                {
                    if (obligatory)
                        return 2;
                    else
                        return 1;
                }
            }
            return 0;
        }
    }

    public Vector2 size;
    public int startPos = 0;
    public Rule[] rooms;
    public Vector2 offset;

    List<Cell> board;

    private void Start()
    {
        MazeGenerator();
    }
    void GenerateDungeon()
    {
        for(int i=0;i<size.x;i++)
        {
            for(int j=0;j<size.y;j++)
            {
                Cell currentCell = board[Mathf.FloorToInt(i + j * size.x)];
                if (currentCell.visited)
                {
                    int randomRoom = -1;
                    List<int> availableRooms = new List<int>();

                    for(int k = 0;k < rooms.Length;k++)
                    {
                        int p = rooms[k].ProbabilityofSpawn(i, j);
                        if(p == 2)
                        {
                            randomRoom = k;
                            break;
                        }
                        else if(p == 1)
                        {
                            availableRooms.Add(k);
                        }
                    }

                    if(randomRoom == -1)
                    {
                        if(availableRooms.Count > 0)
                        {
                            randomRoom = availableRooms[Random.Range(0, availableRooms.Count)];
                        }
                        else
                        {
                            randomRoom = 0;
                        }
                    }
                    var newRoom = Instantiate(rooms[randomRoom].room, new Vector3(i * offset.x, 0, -j * offset.y), Quaternion.identity, transform).GetComponent<RoomBehaviour>();
                    rooms[randomRoom].frequency--;
                    newRoom.UpdateRoom(currentCell.status);
                    newRoom.name += " " + i + "-" + j;
                }
            }
        }
    }
    void MazeGenerator()
    {
        board = new List<Cell>();
        for(int i = 0;i < size.x;i++)
        {
            for(int j = 0;j<size.y;j++)
            {
                board.Add(new Cell());
            }
        }

        int currentCell = startPos;

        Stack<int> path = new Stack<int>();

        int k = 0;
        while(k<1000)
        {
            k++;

            board[currentCell].visited = true;

            if(currentCell == board.Count -1)
            {
                break;
            }

            //Check the cell's neighbours

            List<int> neighbors = CheckNeighbours(currentCell);

            if(neighbors.Count == 0)
            {
                if(path.Count == 0)
                {
                    break;
                }
                else
                {
                    currentCell = path.Pop();
                }
            }
            else
            {
                path.Push(currentCell);
                int newCell = neighbors[Random.Range(0, neighbors.Count)];
                if(newCell>currentCell)
                {
                    //down or right
                    if(newCell-1 == currentCell)
                    {
                        board[currentCell].status[2] = true;
                        currentCell = newCell;
                        board[currentCell].status[3] = true;
                    }
                    else
                    {
                        board[currentCell].status[1] = true;
                        currentCell = newCell;
                        board[currentCell].status[0] = true;
                    }
                }
                else
                {
                    //up or left
                    if (newCell + 1 == currentCell)
                    {
                        board[currentCell].status[3] = true;
                        currentCell = newCell;
                        board[currentCell].status[2] = true;
                    }
                    else
                    {
                        board[currentCell].status[0] = true;
                        currentCell = newCell;
                        board[currentCell].status[1] = true;
                    }
                }
            }
        }
        GenerateDungeon();
    }

    List<int> CheckNeighbours(int cell)
    {
        List<int> neigbours = new List<int>();

        //Check up
        if (cell - size.x >= 0 && !board[Mathf.FloorToInt(cell-size.x)].visited)
        {
            neigbours.Add(Mathf.FloorToInt(cell - size.x));
        }

        //Check Down
        if (cell + size.x < board.Count && !board[Mathf.FloorToInt(cell + size.x)].visited)
        {
            neigbours.Add(Mathf.FloorToInt(cell + size.x));
        }

        //Check Right
        if ((cell+1)%size.x != 0 && !board[Mathf.FloorToInt(cell + 1)].visited)
        {
            neigbours.Add(Mathf.FloorToInt(cell + 1));
        }

        //Check Left
        if (cell % size.x != 0 && !board[Mathf.FloorToInt(cell - 1)].visited)
        {
            neigbours.Add(Mathf.FloorToInt(cell - 1));
        }

        return neigbours;
    }
}
