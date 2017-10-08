using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public int lives;
    public int damage;
    public bool useBullets = true;
    public int score { get; private set; }

    public List<Sprite> lifeSprites = new List<Sprite>();
    private AudioSource audioSource;

    private Animation anim;

    private const int maxBullets = 10;
    private int bullets;

    private bool reloading { get { return anim.IsPlaying("reload"); } }

    //gizmos variables

    private void Awake()
    {
        bullets = maxBullets;
        audioSource = GetComponent<AudioSource>();
        anim = GetComponentInChildren<Animation>();
        lives = lifeSprites.Count;
    }

    // Use this for initialization
    void Start()
    {
        Gamemanager.instance.setPlayer(this);
        AnimationManager.instance.PlayDraw(anim);
        UIManager.instance.updateLives();
        UIManager.instance.updateScore();
        UIManager.instance.UpdateBullets(bullets);

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Hit(int dmg)
    {
        if (lives <= 0) return;

        AudioManager.instance.PlayHitSound(audioSource);
        lives -= dmg;

        if (lives <= 0)
        {
            GameOver();
        }

        UIManager.instance.updateLives();
    }

    private void GameOver()
    {
        //player dead
        Debug.Log("Game Over!");
        UIManager.instance.GameOver();
        InputManager.instance.PauzeGame();
    }

    public void Shoot()
    {
        if (reloading || bullets <= 0) return;


        AudioManager.instance.PlayGunSound(audioSource);
        AnimationManager.instance.PlayFireGun(anim);

        Ray ray = Camera.main.ScreenPointToRay(Gamemanager.instance.crossHair.transform.position);
        Debug.DrawRay(ray.origin, ray.direction, Color.red);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
        if (hit.collider != null)
        {
            Enemy enem = hit.collider.GetComponentInParent<Enemy>();
            if (enem != null)
            {
                score += enem.Hit(damage);
                UIManager.instance.updateScore();
            }
        }

        bullets--;

        if (bullets <= 0) Reload();
        else UIManager.instance.UpdateBullets(bullets);

    }

    public void Reload()
    {
        if (reloading || bullets >= maxBullets) return;

        AnimationManager.instance.PlayReload(anim);
        bullets = maxBullets;
        UIManager.instance.UpdateBullets(bullets);
    }

    public Sprite getLivesSprite()
    {
        if (lives <= 0) return lifeSprites[lifeSprites.Count];
        return lifeSprites[lifeSprites.Count - lives];

    }

    private void OnDrawGizmos()
    {
        //Ray ray = Camera.main.ScreenPointToRay(Gamemanager.instance.crossHair.transform.position);
        //Gizmos.color = Color.red;
        //Gizmos.DrawLine(ray.origin, ray.GetPoint(100));
    }
}


