using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public List<Sprite> EnemyStates = new List<Sprite>();
    public float velocity;
    public int damage;
    protected int lives;
    public int points;

    private bool isCloseBy;

    private Bounds defaultBounds;


    private int defLives;
    private int defDamage;
    private float defVelocity;

    AudioSource audSource;

    SpriteRenderer sr;

    private void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        defaultBounds = sr.sprite.bounds;
        audSource = GetComponent<AudioSource>();
    }


    // Use this for initialization
    void Start()
    {
        defLives = EnemyStates.Count;
        defDamage = damage;
        defVelocity = velocity;
        ResetEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }


    public void ResetEnemy()
    {
        gameObject.SetActive(true);
        audSource.Stop();
        AudioManager.instance.PlayEnemySound(audSource);
        lives = defLives;
        damage = defDamage;
        velocity = defVelocity;
        isCloseBy = false;

        updateSprite();
    }


    void Move()
    {
        float step = velocity * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, Camera.main.transform.position, step);

        float dist = Vector3.Distance(transform.position, Gamemanager.instance.player.transform.position);
        if (dist <= 0.1)
        {
            //enemy reached player
            this.gameObject.SetActive(false);
            Gamemanager.instance.player.Hit(damage);
        }
        else if(dist <= 1 && !isCloseBy)
        {
            Debug.Log("Enemy is close by");
            isCloseBy = true;
            AudioManager.instance.PlayEnemyCloseBySound(audSource);
        }
    }

    public int Hit(int dmg)
    {
        if (lives <= 0) return 0;
        this.lives -= dmg;



        if (lives <= 0)
        {
            //this enemy is dead
            this.gameObject.SetActive(false);
            return points;
        }
        else
        {
            updateSprite();
            return 0;
        }

    }

    void updateSprite()
    {
        Sprite newSprite = EnemyStates[EnemyStates.Count - lives];
        sr.sprite = newSprite;
        resize();
    }

    void resize()
    {
        //stel originele bounds zijn 1 x 1
        // nieuwe bounds zijn 3x4
        // 3 * (1/3) 

        float xDif = defaultBounds.size.x / sr.sprite.bounds.size.x;
        float yDif = defaultBounds.size.y / sr.sprite.bounds.size.y;

        sr.transform.localScale = new Vector3(xDif, yDif, sr.transform.localScale.z);
    }

}

