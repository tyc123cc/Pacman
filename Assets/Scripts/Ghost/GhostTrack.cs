using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostTrack : Ghost
{
    public Transform m_wayPoint;
    public Transform m_target;
    public float m_speed;
    public PacmanMove m_pacman;
    private Transform m_frontPoint;//避免死循环移动，不走回头路
    private Transform m_iniWayPoint;
    private Transform m_iniTarget;
    // Use this for initialization
    void Awake()
    {
        m_iniWayPoint = m_wayPoint;
        m_iniTarget = m_target;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_target == null || WinCondiction.m_isWin == true || GameManager.m_paused == true)
        {
            return;
        }
        Vector2 p = Vector2.MoveTowards(transform.position, m_wayPoint.position, m_speed);
        transform.position = p;
        if (Vector2.Distance(transform.position, m_wayPoint.position) < 0.1f)
        {
            Transform temp = m_wayPoint;
            ChangeWayPoint();//到达路点，变更下一个路点
            m_frontPoint = temp;
            Vector2 dir = m_wayPoint.position - transform.position;
            GetComponent<Animator>().SetFloat("DirX", dir.x);
            GetComponent<Animator>().SetFloat("DirY", dir.y);
        }
    }

    private void ChangeWayPoint()
    {
        if (m_pacman.m_pacmanState == PacmanMove.Pacman_Normal || m_pacman.m_pacmanState == PacmanMove.Pacman_Hurt)//吃豆人正常状态或受伤状态下，接近吃豆人
        {
            Transform nextWay = m_wayPoint;//下一个路点，设为上一路点仅是为了避免报空值错误
            float distance = float.MaxValue;//初始距离设为最大值
            WayPoint nextWays = m_wayPoint.GetComponent<WayPoint>();
            foreach (Transform wayPoint in nextWays.m_nextPoint)//遍历该路点临近所有路点，找出与目标点最近的路点，设置为新路点
            {
                if (Vector2.Distance(wayPoint.position, m_target.position) < distance && wayPoint != m_frontPoint)//不走回头路
                {
                    distance = Vector2.Distance(wayPoint.position, m_target.position);
                    nextWay = wayPoint;
                }
            }
            m_wayPoint = nextWay;
        }
        else if (m_pacman.m_pacmanState == PacmanMove.Pacman_Invicible)//吃豆人无敌状态下，远离吃豆人
        {
            Transform nextWay = m_wayPoint;//下一个路点，设为上一路点仅是为了避免报空值错误
            float distance = 0;//初始距离设为最小值
            WayPoint nextWays = m_wayPoint.GetComponent<WayPoint>();
            foreach (Transform wayPoint in nextWays.m_nextPoint)//遍历该路点临近所有路点，找出与吃豆人最远的路点，设置为新路点
            {
                if (Vector2.Distance(wayPoint.position, m_pacman.transform.position) > distance)
                {
                    distance = Vector2.Distance(wayPoint.position, m_pacman.transform.position);
                    nextWay = wayPoint;
                }
            }
            m_wayPoint = nextWay;
        }
    }

    public void OnDisable()
    {
        m_wayPoint = m_iniWayPoint;
        m_target = m_iniTarget;
    }
}
