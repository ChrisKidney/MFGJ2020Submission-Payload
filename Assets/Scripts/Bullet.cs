using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int speed = 50;
    public Vector3 targetVector;
    public float lifetime = 10f;
    public float damage = 1;
    private UIScoreUpdater scoreManager;
    private GameObject explosion;
    private AudioSource wallExplosion;
    private AudioSource bulletExplosion;
    
   

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D rb = gameObject.GetComponentInChildren<Rigidbody2D>();
        rb.AddForce(targetVector.normalized * speed);
        scoreManager = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<UIScoreUpdater>();
        explosion = GameObject.FindGameObjectWithTag("Explosion");
        wallExplosion = GameObject.FindGameObjectWithTag("WallExplosion").GetComponent<AudioSource>();
        bulletExplosion = GameObject.FindGameObjectWithTag("BulletExplosion").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        lifetime -= Time.deltaTime;
        if (lifetime <= 0f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Obstacle")
        {
            Vector3 impact = this.transform.position;
            bulletExplosion.Play();
            GameObject explosionPlay = Instantiate(explosion, impact, Quaternion.identity);
            explosionPlay.GetComponent<ParticleSystem>().Play();
            Destroy(explosionPlay, 0.5f);
            Destroy(gameObject);
        }
        else if(col.tag == "FireWall")
        {
            Vector3 impact = this.transform.position;
            bulletExplosion.Play();
            GameObject explosionPlay = Instantiate(explosion, impact, Quaternion.identity);
            explosionPlay.GetComponent<ParticleSystem>().Play();
            Destroy(explosionPlay, 0.5f);
            Destroy(gameObject);
        }
        else if(col.tag == "BlueTarget" && this.tag == "BlueSquare")
        {
            GameObject go = col.transform.parent.gameObject;
            wallExplosion.Play();
            scoreManager.WallHitManager(1000,go);
            Destroy(col.gameObject);
            Destroy(go.GetComponent<Collider2D>());
            Destroy(gameObject);

        }
        else if (col.tag == "YellowTarget" && this.tag == "YellowTriangle")
        {
            GameObject go = col.transform.parent.gameObject;
            wallExplosion.Play();
            scoreManager.WallHitManager(2500, go);
            Destroy(col.gameObject);
            Destroy(go.GetComponent<Collider2D>());
            Destroy(gameObject);

        }
        else if (col.tag == "RedTarget" && this.tag == "RedCircle")
        {
            GameObject go = col.transform.parent.gameObject;
            wallExplosion.Play();
            scoreManager.WallHitManager(3000, go);
            Destroy(col.gameObject);
            Destroy(go.GetComponent<Collider2D>());
            Destroy(gameObject);

        }
        else if (col.tag == "GreenTarget" && this.tag == "GreenOctagon")
        {
            GameObject go = col.transform.parent.gameObject;
            wallExplosion.Play();
            scoreManager.WallHitManager(3250, go);
            Destroy(col.gameObject);
            Destroy(go.GetComponent<BoxCollider2D>());
            Destroy(go.GetComponent<PolygonCollider2D>());
            Destroy(gameObject);

        }
    }
}
