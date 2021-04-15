using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Game : MonoBehaviour
{
    public static float gridHeight=14;
    public static float gridWidth=7;
    public score Score;

    // Start is called before the first frame update
    public static Transform[,] grid=new Transform[10, 20];
    void Start()
    {
        spawnNextTetromino();
    }

    
    public bool CheckIsAboveGrid(Tetromino tetromino)
    {
        for (int x = 0; x < 10; x++)
        {
            foreach (Transform mino in tetromino.transform)
            {
                Vector2 pos = mino.position;
                if(pos.y> gridHeight-0.7f)
                {
                    return true;
                }
            }
        }
        return false;
    }
    public bool IsFullRowAt(int y) //检查每行是否填满
    {
        for(int x=0;x<10;x++)
        {
            if(grid[x,y]==null)
            {
                return false;
            }
        }
        return true;
    }

    public void deleteMinoAt(int y) //删除整行
    {
        for (int x = 0; x < 10; x++)
        {
            Destroy(grid[x, y].gameObject);
            grid[x, y] = null;
        }
    }
   
    public void moveRowDown(int y) //y行下移一行
    {
        for (int x = 0; x < 10; x++)
        {
            if(grid[x, y]!=null)
            {
                grid[x, y - 1] = grid[x, y]; 
                grid[x, y].position += new Vector3(0, -0.7f, 0);
                grid[x, y] = null;
            }
        }
    }

    public void deleteRow() //检查每一行，如果满就删除,再将y行的上面每一行向下移动一行
    {
        for (int y = 0; y < 20; ++y)
        {
            if(IsFullRowAt(y))
            {
                deleteMinoAt(y);
                moveAllRowsDown(y+1);
                --y;
                Score.totalScore += 100;
            }
        }
    }
    public void moveAllRowsDown(int y)
    {
        for (int i = y; i < 20; i++)
        {
            moveRowDown(i);
        }
    }

    public void updateGrid(Tetromino tetromino)
    {
        for(int y=0;y<20;++y)
        {
            for(int x=0;x<10;++x)
            {
                if(grid[x,y]!=null)
                {
                    if(grid[x,y].parent== tetromino.transform) //grid[x,y].parent是单个方块的父类，即整个prefab的transform，grid[x,y].parent== tetromino.transform意思是正在操作当前的Tetris                                 
                    {
                        grid[x, y] = null;  //如果操作当前的Tetris到达底部或者与其他如果操作当前的Tetris到达底部或者与其他碰撞，就将当前的Tetris的四个mino放入grid，并且不会置空
                    }
                }
            }
        }
        foreach(Transform mino in tetromino.transform)  //先刷新grid再赋值，使grid保留Tetris停止运动前最后的4个mino的值
        {
            Vector2 pos = Round(mino.position / 0.69f); //使pos变成10-20的整数
            if(pos.y<20)
            {
                grid[(int)(pos.x), (int)(pos.y)] = mino;  //给每个grid单元赋mino的值
            }
        }
    }

    public Transform GetTransformFromGridPositon(Vector2 pos)
    {
        if(pos.y>20)
        {
            return null;
        }
        return grid[(int)pos.x, (int)pos.y];
    }

    public void spawnNextTetromino()
    {
        GameObject nextTetromino = (GameObject)Instantiate(Resources.Load(getRandomTetromino(),typeof(GameObject)),new Vector2(3.5f, 14.7f),Quaternion.identity);
    }

    string getRandomTetromino()
    {
        int randomTetromino = Random.Range(1,8);
        string randomTetrominoName = "prefabs/Tetromino-j";
        switch(randomTetromino)
        {
            case 1:
                randomTetrominoName = "prefabs/Tetromino-j";
                break;
            case 2:
                randomTetrominoName = "prefabs/Tetromino-l";
                break;
            case 3:
                randomTetrominoName = "prefabs/Tetromino-long";
                break;
            case 4:
                randomTetrominoName = "prefabs/Tetromino-s";
                break;
            case 5:
                randomTetrominoName = "prefabs/Tetromino-square";
                break;
            case 6:
                randomTetrominoName = "prefabs/Tetromino-t";
                break;
            case 7:
                randomTetrominoName = "prefabs/Tetromino-z";
                break;
        }
        return randomTetrominoName;
    }
    public bool checkIsInsideGrid(Vector2 pos)
    {
        return ((int)pos.x >= 0 && (int)pos.x < gridWidth && (int)pos.y > 0);
    }

    public Vector2 Round(Vector2 pos)
    {
        return new Vector2(Mathf.Round(pos.x), Mathf.Round(pos.y));  
       
    }

    public void Gameover()
    {
        SceneManager.LoadScene("Gameover");
    }
}
