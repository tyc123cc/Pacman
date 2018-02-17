using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirPos : MonoBehaviour {
    public Transform m_dirPos;
    private Animator m_ani;
    public float m_distance;
	// Use this for initialization
	void Start () {
        m_ani = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        //读取当前动画状态
        AnimatorStateInfo stateInfo = m_ani.GetCurrentAnimatorStateInfo(0);
        //如果吃豆人面向右侧
        if (stateInfo.fullPathHash == Animator.StringToHash("Base Layer.pacman_right") && !m_ani.IsInTransition(0))
        {
            m_dirPos.position = new Vector2(transform.position.x + m_distance, transform.position.y);//方向点向右移动
        }
        //如果吃豆人面向左侧
        if (stateInfo.fullPathHash == Animator.StringToHash("Base Layer.pacman_left") && !m_ani.IsInTransition(0))
        {
            m_dirPos.position = new Vector2(transform.position.x - m_distance, transform.position.y);//方向点向左移动
        }
        //如果吃豆人面向上侧
        if (stateInfo.fullPathHash == Animator.StringToHash("Base Layer.pacman_up") && !m_ani.IsInTransition(0))
        {
            m_dirPos.position = new Vector2(transform.position.x, transform.position.y + m_distance);//方向点向上移动
        }
        //如果吃豆人面向下侧
        if (stateInfo.fullPathHash == Animator.StringToHash("Base Layer.pacman_down") && !m_ani.IsInTransition(0))
        {
            m_dirPos.position = new Vector2(transform.position.x, transform.position.y - m_distance);//方向点向下移动
        }
	}
}
