using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tetromino : MonoBehaviour
{
    private float fall = 0;
    public float fallSpeed = 1f;

    public bool allowRotation = true;
    public bool limitRotation=false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void CheckUserInput()
    {
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += new Vector3(0.7f, 0, 0);

            if (CheckIsValidPositon())
            {
                FindObjectOfType<Game>().updateGrid(this);
            }
            else 
            {
                transform.position += new Vector3(-0.7f, 0, 0);

            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(-0.7f, 0, 0);
            if (CheckIsValidPositon())
            {
                FindObjectOfType<Game>().updateGrid(this);
            }
            else
            {
                transform.position += new Vector3(0.7f, 0, 0);

            }
        }
        else if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            if(allowRotation)
            {
                if(limitRotation)
                {
                    if(transform.rotation.eulerAngles.z>=90)
                    {
                        transform.Rotate(0, 0, -90);
                    }else
                    {
                        transform.Rotate(0, 0, 90);
                    }
                }else
                {
                    transform.Rotate(0, 0, 90);
                   
                }
                if (CheckIsValidPositon())
                {
                    FindObjectOfType<Game>().updateGrid(this);
                }
                else
                {
                    if(limitRotation)
                    {
                        if (transform.rotation.eulerAngles.z >= 90)
                        {
                            transform.Rotate(0, 0, -90);
                        }
                        else
                        {
                            transform.Rotate(0, 0, 90);
                        }
                    }else
                    {
                        transform.Rotate(0, 0, -90);
                    }
                }

            }
            
        }
        else if(Input.GetKeyDown(KeyCode.DownArrow)||Time.time-fall>=fallSpeed)  //每秒下降一格
        {
            transform.position += new Vector3(0, -0.7f, 0);
            fall = Time.time;

            if (CheckIsValidPositon())
            {
                FindObjectOfType<Game>().updateGrid(this);
            }
            else
            {
                transform.position += new Vector3(0, 0.7f, 0);
                FindObjectOfType<Game>().deleteRow();//检测是否有填满的行，有则删除
                if(FindObjectOfType<Game>().CheckIsAboveGrid(this))
                {
                    FindObjectOfType<Game>().Gameover();
                }
                enabled = false;
                FindObjectOfType<Game>().spawnNextTetromino();
            }
        }
    }
    // Update is called once per frame

    private bool CheckIsValidPositon()
    {
        foreach(Transform mino in transform) //找每一个子方块transform
        {
            Vector2 pos = FindObjectOfType<Game>().Round(mino.position);
            Vector2 pos1 = FindObjectOfType<Game>().Round(mino.position/0.69f);  //将单位变为整数与二维数组下标匹配
            if (FindObjectOfType<Game>().checkIsInsideGrid(pos)==false)
            {
                return false;
            }
            if(FindObjectOfType<Game>().GetTransformFromGridPositon(pos1) !=null&& FindObjectOfType<Game>().GetTransformFromGridPositon(pos1).parent !=transform) //有一个mino的位置与已经赋值的grid重合并且已经赋值的grid的父物体不是正在操作的物体，则往回退一格并且不能旋转
            {
                return false;
            }
        }

        return true;
    }

    void Update()
    {
        CheckUserInput();
    }
}
