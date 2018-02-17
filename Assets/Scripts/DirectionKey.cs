using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DirectionKey : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public int m_direction;
    public int m_up = 0;
    public int m_down = 1;
    public int m_left = 2;
    public int m_right = 3;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnPointerDown(PointerEventData eventData)
    {
        print(1);
        switch (m_direction)
        {
            case 0:
                PacmanMove.m_PacmanMoveState = PacmanMove.MOVE_UP;

                break;
            case 1:
                PacmanMove.m_PacmanMoveState = PacmanMove.MOVE_DOWN;
                break;
            case 2:
                PacmanMove.m_PacmanMoveState = PacmanMove.MOVE_LEFT;
                break;
            case 3:
                PacmanMove.m_PacmanMoveState = PacmanMove.MOVE_RIGHT;
                break;

        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        PacmanMove.m_PacmanMoveState = PacmanMove.MOVE_NONE;
    }
}
