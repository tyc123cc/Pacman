using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacmanMove : MonoBehaviour
{
    public float speed = 0.4f;
    Vector2 dest = Vector2.zero;
    public int m_pacmanState = 0;
    public static int Pacman_Normal = 0;
    public static int Pacman_Invicible = 1;
    public static int Pacman_Hurt = 2;
    public static int m_life = 3;
    public static int m_maxLife = 3;
    public const float m_invicibleTime = 5;
    public const float m_invicibleFlashTime = 2.5f;
    private float m_invicibleTimer = 0;
    private float m_invicibleFlashTimer = 0;

    public static int m_PacmanMoveState = 0;
    public static int MOVE_NONE = 0;
    public static int MOVE_UP = 1;
    public static int MOVE_DOWN = 2;
    public static int MOVE_LEFT = 3;
    public static int MOVE_RIGHT = 4;

    public static bool PACMAN_CANMOVE = true;
    // Use this for initialization
    void Start()
    {
        dest = transform.position;
        m_pacmanState = Pacman_Normal;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_pacmanState == Pacman_Invicible)
        {
            m_invicibleTimer -= Time.deltaTime;
            if (m_invicibleTimer <= m_invicibleFlashTime)//小于闪烁时间时，每隔0.5s闪烁
            {
                m_invicibleFlashTimer += Time.deltaTime;
                if (m_invicibleFlashTimer >= 0.5f)
                {
                    if (GetComponent<SpriteRenderer>().color == Color.red)
                    {
                        GetComponent<SpriteRenderer>().color = Color.white;
                    }
                    else
                    {
                        GetComponent<SpriteRenderer>().color = Color.red;
                    }
                    m_invicibleFlashTimer = 0;
                }
            }
            if (m_invicibleTimer <= 0)
            {
                m_invicibleTimer = 0;
                ChangeState(Pacman_Normal);
            }
        }
        if (m_PacmanMoveState == MOVE_UP)
        {
            PACMAN_CANMOVE = valid((Vector2.up + Vector2.left * 0.3f) * 2) && valid((Vector2.up + Vector2.right * 0.3f) * 2);
        }
        else if (m_PacmanMoveState == MOVE_RIGHT)
        {
            PACMAN_CANMOVE = valid((Vector2.right + Vector2.up * 0.3f) * 2) && valid((Vector2.right + Vector2.down * 0.3f) * 2);
        }
        else if (m_PacmanMoveState == MOVE_DOWN)
        {
            PACMAN_CANMOVE = valid((Vector2.down + Vector2.left * 0.3f) * 2) && valid((Vector2.down + Vector2.right * 0.3f) * 2);
        }
        else if (m_PacmanMoveState == MOVE_LEFT)
        {
            PACMAN_CANMOVE = valid((Vector2.left + Vector2.up * 0.3f) * 2) && valid((Vector2.left + Vector2.down * 0.3f) * 2);
        }
        print("CanMove:" + PACMAN_CANMOVE);
    }

    void FixedUpdate()
    {
        if (GameManager.m_paused == true || WinCondiction.m_isWin == true)
        {
            return;
        }
        // Move closer to Destination
        Vector2 p = Vector2.MoveTowards(transform.position, dest, speed);
        transform.position = p;
        // Check for Input if not moving
        if ((Vector2)transform.position == dest)
        {
            print("Move:"+m_PacmanMoveState);
            if (m_PacmanMoveState == MOVE_UP && valid((Vector2.up + Vector2.left * 0.3f) * 2) && valid((Vector2.up + Vector2.right * 0.3f) * 2))
                dest = (Vector2)transform.position + Vector2.up;
            if (m_PacmanMoveState == MOVE_RIGHT && valid((Vector2.right + Vector2.up * 0.3f) * 2) && valid((Vector2.right + Vector2.down * 0.3f) * 2))
                dest = (Vector2)transform.position + Vector2.right;
            if (m_PacmanMoveState == MOVE_DOWN && valid((Vector2.down + Vector2.left * 0.3f) * 2) && valid((Vector2.down + Vector2.right * 0.3f) * 2))
                dest = (Vector2)transform.position - Vector2.up;
            if (m_PacmanMoveState == MOVE_LEFT && valid((Vector2.left + Vector2.up * 0.3f) * 2) && valid((Vector2.left + Vector2.down * 0.3f) * 2))
                dest = (Vector2)transform.position - Vector2.right;

            if (Input.GetKey(KeyCode.UpArrow) && valid((Vector2.up + Vector2.left * 0.3f) * 2) && valid((Vector2.up + Vector2.right * 0.3f) * 2))
                dest = (Vector2)transform.position + Vector2.up;
            if (Input.GetKey(KeyCode.RightArrow) && valid((Vector2.right + Vector2.up * 0.3f) * 2) && valid((Vector2.right + Vector2.down * 0.3f) * 2))
                dest = (Vector2)transform.position + Vector2.right;
            if (Input.GetKey(KeyCode.DownArrow) && valid((Vector2.down + Vector2.left * 0.3f) * 2) && valid((Vector2.down + Vector2.right * 0.3f) * 2))
                dest = (Vector2)transform.position - Vector2.up;
            if (Input.GetKey(KeyCode.LeftArrow) && valid((Vector2.left + Vector2.up * 0.3f) * 2) && valid((Vector2.left + Vector2.down * 0.3f) * 2))
                dest = (Vector2)transform.position - Vector2.right;
        }
        // Animation Parameters
        Vector2 dir = dest - (Vector2)transform.position;
        GetComponent<Animator>().SetFloat("DirX", dir.x);
        GetComponent<Animator>().SetFloat("DirY", dir.y);
    }
    bool valid(Vector2 dir)
    {
        // Cast Line from 'next to Pac-Man' to 'Pac-Man'
        Vector2 pos = transform.position;
        RaycastHit2D hit = Physics2D.Linecast(pos + dir, pos);

        if (hit.collider == null)
        {
            return true;
        }
        if (hit.collider.gameObject.layer == LayerMask.NameToLayer("wall") || hit.collider.gameObject.layer == LayerMask.NameToLayer("Door"))
        {
            return false;
        }

        return true;
    }

    public void ChangeState(int state)
    {
        m_pacmanState = state;
        if (state == Pacman_Normal)
        {
            GetComponent<SpriteRenderer>().color = Color.white;

        }
        else if (state == Pacman_Invicible)
        {
            m_invicibleTimer += m_invicibleTime;
            GetComponent<SpriteRenderer>().color = Color.red;
        }
        else if (state == Pacman_Hurt)
        {
            StartCoroutine(Hurt());

        }
    }

    IEnumerator Invicible()
    {
        yield return new WaitForSeconds(5);
        GetComponent<SpriteRenderer>().color = Color.white;
        yield return new WaitForSeconds(0.5f);
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.5f);
        GetComponent<SpriteRenderer>().color = Color.white;
        yield return new WaitForSeconds(0.5f);
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.5f);
        ChangeState(Pacman_Normal);

    }

    IEnumerator Hurt()
    {
        Color color = GetComponent<SpriteRenderer>().color;
        color.a = 0;
        GetComponent<SpriteRenderer>().color = color;
        yield return new WaitForSeconds(0.2f);
        color.a = 1;
        GetComponent<SpriteRenderer>().color = color;
        yield return new WaitForSeconds(0.2f);
        color.a = 0;
        GetComponent<SpriteRenderer>().color = color;
        yield return new WaitForSeconds(0.2f);
        color.a = 1;
        GetComponent<SpriteRenderer>().color = color;
        yield return new WaitForSeconds(0.2f);
        color.a = 0;
        GetComponent<SpriteRenderer>().color = color;
        yield return new WaitForSeconds(0.2f);
        color.a = 1;
        GetComponent<SpriteRenderer>().color = color;
        ChangeState(Pacman_Normal);

    }
}
