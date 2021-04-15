using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MankindGames;

public class PillarLastBoss : MonoBehaviour
{
    public float lifeTime = 5f;
    public float speed = 2f;
    public float absorbedSpeed;
    public float absorbedHP;
    public GameObject stopPos;
    public Transform bossPos;
    public bool isAttached;

    float dist;
    float dist2;
    float absorbtionTimer;
    EdgeCollider2D col;

    LineRenderer lr;
    Vector3 direction;
    SpriteRenderer sprite;
    Vector3 originPos;
    Vector3 velocity;


    int[] pointCount;

    protected EdgeCollider2D m_EdgeCollider2D;
    protected List<Vector2> m_Points;

    bool isOnPlayerPos;

    bool isHealing;

    //private IEnumerator coroutine;
    int i;
    GameObject getBossGameObject;
    GameManager gm;
    //Color bulletColor;

    void Start()
    {
        //bulletColor = new Color (54,132,36,0);
        //getBossGameObject = GameObject.Find("LastBoss(Clone)");
        getBossGameObject = GameObject.FindGameObjectWithTag("Boss");
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        lr = GetComponent<LineRenderer>();
        //lr.SetColors(bulletColor, bulletColor);
        bossPos = getBossGameObject.transform;

        //Destroy(gameObject, lifeTime);
        //transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().enabled = false;

        originPos = transform.position;
        direction = getBossGameObject.transform.position - originPos;
        direction = direction.normalized;
        velocity = direction * speed;
        m_Points = new List<Vector2>();

        m_EdgeCollider2D = GetComponent<EdgeCollider2D>();
    }

    void Update()
    {
        if (getBossGameObject != null)
        {

            //if (bulletColor == gm.cam.backgroundColor)
            //{
            //    gameObject.SetActive(false);
            //}
            //else
            //{
            //    gameObject.SetActive(true);

            //}

            //Log.log("HP " + getBossGameObject.GetComponent<TestEnemy>().GetStats().HP);
            if (getBossGameObject != null)
            {
                //Log.log("speed " + player.GetComponent<Player>().GetStats().Speed);


                FollowBossIfAttached();

                originPos = getBossGameObject.GetComponent<LastBossCombat>().originPos
                    - new Vector3(1f, 1f, 0.0f)/*new Vector3(0.0f, 0f, 0.0f)*/;
                lr.SetPosition(0, transform.position);
                lr.SetPosition(1, originPos);

                m_Points.Add(transform.position);
                //lr.positionCount = m_Points.Count;


                m_Points[i] = new Vector2(/*transform.position.x*/0, 0f/*transform.position.y*/);

                i++;
                if (i >= m_EdgeCollider2D.points.Length)
                {
                    i = 0;
                }

                if (!isOnPlayerPos)
                {
                    m_EdgeCollider2D.offset = transform.position * -1;
                    m_EdgeCollider2D.points = m_Points.ToArray();
                    transform.position += velocity * Time.deltaTime;


                    //dist = Vector2.Distance(transform.position.x, player.transform.position.y);
                    if (Mathf.Abs(m_EdgeCollider2D.offset.x) > Mathf.Abs(bossPos.position.x) + 2 ||
                        Mathf.Abs(m_EdgeCollider2D.offset.y) > Mathf.Abs(bossPos.position.y) + 2)
                    {
                        isOnPlayerPos = true;

                    }
                }
                else
                {
                    return;
                }
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void FollowBossIfAttached()
    {
        if (isAttached == true)
        {
            transform.position = getBossGameObject.transform.position;
            absorbtionTimer += Time.deltaTime;
            if (absorbtionTimer > 2)
            {

                //getBossGameObject.GetComponent<Player>().GetStats().SetSpeed(-0.1f);
                getBossGameObject.GetComponent<Ranged1>().GetStats().Heal(5);
                getBossGameObject.GetComponent<Ranged1>().stateUI.Refresh();
                absorbedHP += 5;
                absorbedSpeed += 0.1f;


                absorbtionTimer = 0;

            }
        }

        //if (isOnPlayerPos == true && isAttached == false)
        //{
        //    Destroy(gameObject);
        //}
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Boss")
        {
            Log.log("enter");
            //isHealing = true;
            isAttached = true;
            isOnPlayerPos = true;
            //getBossGameObject.GetComponent<TestEnemy>().GetStats().Heal(1);//dothat in the update later
        }
    }
}
