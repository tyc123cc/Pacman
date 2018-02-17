using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIjoysticks : MonoBehaviour
{

    //虚拟方向按钮初始位置  
    public Vector3 initPosition;
    //虚拟方向按钮可移动的半径  
    public float r;
    //border对象  
    public Transform border;

    public bool m_isDrag = false;
    void Start()
    {
        //获取border对象的transform组件  
        border = GameObject.Find("border").transform;
        initPosition = GetComponentInParent<RectTransform>().position;
        r = Vector3.Distance(transform.position, border.position);
        StartCoroutine(Move());
    }


    IEnumerator Move()
    {

            while (m_isDrag)
            {
                if (transform.localPosition.x > 0)
                {
                    float angle = Mathf.Atan(transform.localPosition.y / transform.localPosition.x);
                    if (angle > Mathf.PI / 4)
                    {
                        PacmanMove.m_PacmanMoveState = PacmanMove.MOVE_UP;
                        yield return new WaitForFixedUpdate();
                        if (PacmanMove.PACMAN_CANMOVE == false)
                        {
                            PacmanMove.m_PacmanMoveState = PacmanMove.MOVE_RIGHT;
                            yield return new WaitForFixedUpdate();
                        }
                    }
                    else if (angle < -Mathf.PI / 4)
                    {
                        PacmanMove.m_PacmanMoveState = PacmanMove.MOVE_DOWN;
                        yield return new WaitForFixedUpdate();
                        if (PacmanMove.PACMAN_CANMOVE == false)
                        {
                            PacmanMove.m_PacmanMoveState = PacmanMove.MOVE_RIGHT;
                            yield return new WaitForFixedUpdate();
                        }
                    }

                    else
                    {
                        PacmanMove.m_PacmanMoveState = PacmanMove.MOVE_RIGHT;
                        yield return new WaitForFixedUpdate();
                        if (PacmanMove.PACMAN_CANMOVE == false)
                        {
                            if (angle > 0)
                            {
                                PacmanMove.m_PacmanMoveState = PacmanMove.MOVE_UP;
                                yield return new WaitForFixedUpdate();
                            }
                            else
                            {
                                PacmanMove.m_PacmanMoveState = PacmanMove.MOVE_DOWN;
                                yield return new WaitForFixedUpdate();
                            }
                        }

                    }
                }
                else if (transform.localPosition.x <= 0)
                {
                    float angle = Mathf.Atan(transform.localPosition.y / -transform.localPosition.x);
                    //print("angle:" + Mathf.Atan(transform.localPosition.y / -transform.localPosition.x));
                    //print("y:" + transform.localPosition.y);
                    //print("x:" + transform.localPosition.x);
                    if (angle > Mathf.PI / 4)
                    {
                        PacmanMove.m_PacmanMoveState = PacmanMove.MOVE_UP;
                        yield return new WaitForFixedUpdate();
                        if (PacmanMove.PACMAN_CANMOVE == false)
                        {
                            PacmanMove.m_PacmanMoveState = PacmanMove.MOVE_LEFT;
                            yield return new WaitForFixedUpdate();
                        }
                    }
                    else if (angle < -Mathf.PI / 4)
                    {
                        PacmanMove.m_PacmanMoveState = PacmanMove.MOVE_DOWN;
                        yield return new WaitForFixedUpdate();
                        if (PacmanMove.PACMAN_CANMOVE == false)
                        {
                            PacmanMove.m_PacmanMoveState = PacmanMove.MOVE_LEFT;
                            yield return new WaitForFixedUpdate();
                        }
                    }
                    else
                    {
                        PacmanMove.m_PacmanMoveState = PacmanMove.MOVE_LEFT;
                        yield return new WaitForFixedUpdate();
                        if (PacmanMove.PACMAN_CANMOVE == false)
                        {
                            if (angle > 0)
                            {
                                PacmanMove.m_PacmanMoveState = PacmanMove.MOVE_UP;
                                yield return new WaitForFixedUpdate();
                            }
                            else
                            {
                                PacmanMove.m_PacmanMoveState = PacmanMove.MOVE_DOWN;
                                yield return new WaitForFixedUpdate();
                            }
                        }
                    }
                }
            }
            yield return new WaitForFixedUpdate();
            StartCoroutine(Move());

        
    }
    //鼠标拖拽  
    public void OnDragIng()
    {
        //如果鼠标到虚拟键盘原点的位置 < 半径r  
        if (Vector3.Distance(Input.mousePosition, initPosition) < r)
        {
            //虚拟键跟随鼠标  
            transform.position = Input.mousePosition;
        }
        else
        {
            //计算出鼠标和原点之间的向量  
            Vector3 dir = Input.mousePosition - initPosition;
            //这里dir.normalized是向量归一化的意思，实在不理解你可以理解成这就是一个方向，就是原点到鼠标的方向，乘以半径你可以理解成在原点到鼠标的方向上加上半径的距离  
            transform.position = initPosition + dir.normalized * r;

        }
        m_isDrag = true;
    }
    //鼠标松开  
    public void OnDragEnd()
    {
        //松开鼠标虚拟摇杆回到原点  
        transform.position = initPosition;
        m_isDrag = false;
        PacmanMove.m_PacmanMoveState = PacmanMove.MOVE_NONE;
    }
}