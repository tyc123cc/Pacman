using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour {
    public static GameObject m_Restart;//重新开始界面
   public virtual void  OnTriggerEnter2D(Collider2D collider)
   {
       if (collider.tag == "Player")
       {
           int pacmanState =  collider.transform.GetComponent<PacmanMove>().m_pacmanState;
           if (pacmanState == PacmanMove.Pacman_Normal)
           {
               //m_Restart.SetActive(true);
               //Destroy(collider.gameObject);
               PacmanMove.m_life--;
               if (PacmanMove.m_life <= 0)//吃豆人死亡
               {
                   GameManager.Instant.SaveHighScore();
                   WinCondiction.Instant.Death();
                   m_Restart.SetActive(true);
                   Destroy(collider.gameObject);
               }
               else
               {
                   collider.GetComponent<PacmanMove>().ChangeState(PacmanMove.Pacman_Hurt);
               }
           }
           else if (pacmanState == PacmanMove.Pacman_Invicible)
           {
               GameManager.Instant.AddScore(100);
               GameManager.Instant.GhostRevenge(gameObject);
               gameObject.SetActive(false);
           }
           if (pacmanState == PacmanMove.Pacman_Hurt)
           {
               //什么都不做
           }
           
       }

   }

    
    public Transform EscapeToNearWayPoint()
   {
       GameObject[] wayPoints =  GameObject.FindGameObjectsWithTag("WayPoint");
       Transform nearWay = transform;
       float distance = float.MaxValue;
       foreach (var item in wayPoints)
       {
           if (Vector2.Distance(item.transform.position,transform.position) < distance)
           {
               distance = Vector2.Distance(item.transform.position, transform.position);
               nearWay = item.transform;
           }
       }
       return nearWay;
   }

    public Transform ChangeFarWayPoint(Transform pacman,Transform nowPosition)
    {
        Transform nextWay = transform;//下一个路点，设为上一路点仅是为了避免报空值错误
        float distance = 0;//初始距离设为最大值
        WayPoint nextWays = nowPosition.GetComponent<WayPoint>();
        foreach (Transform wayPoint in nextWays.m_nextPoint)//遍历该路点临近所有路点，找出与吃豆人最近的路点，设置为新路点
        {
            if (Vector2.Distance(wayPoint.position, pacman.position) > distance)
            {
                distance = Vector2.Distance(wayPoint.position, pacman.position);
                nextWay = wayPoint;
            }
        }
        return nextWay;
    }
}
