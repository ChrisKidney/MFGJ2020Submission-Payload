using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Player : MonoBehaviour
{

    [Header("Movement")]
    public float moveSpeed = 5f; // Move speed
    public float boostSpeedMulti = 2f;
    public float boostCooldown = 5f;
    public float boostLength = 1f;
    public int boostScoreBonus = 100;
    [HideInInspector]
    public float boostTimer = 0f;
    [Header("Health")]
    public int hitPoints = 3; // Hit points, player has 3 hits before death
    public float invincibilityTime = 3f;
    public float invincibilityBlinkDuration = 5;
    [Header("Debug - Make sure to set to 0")]
    [SerializeField]
    private bool invincible = false; // Used for I-frames after collision
    [HideInInspector]
    public int score = 0;
    [HideInInspector]
    public int ammoType = 0;
    private float scoreUpdateTime = 0f;
    [Header("Attack")]
    public float fireRate = 0.25f;
    [HideInInspector]
    public float fireCooldown = 0f;
    public GameObject blueBulletPrefab;
    public GameObject yellowBulletPrefab;
    public GameObject redBulletPrefab;
    public GameObject greenBulletPrefab;
    [Header("Ship VFX")]
    public Material blueTrail;
    public Material yellowTrail;
    public Material redTrail;
    public Material greenTrail;
    public ParticleSystem playerExplosion;
    [Header("Game Layers")]
    public GameObject backgroundImage;
    public GameObject middlegroundImage;
    public GameObject levelGeometry;
    [Header("Music/UI")]
    public AudioSource backgroundMusic;
    public float FadeTime;
    public AudioClip shipExplosion;
    public AudioClip shipCollision;
    public AudioClip shipBoost;
    public AudioClip deliveringPayload;
    AudioSource audioSource;
    
   
    
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        fireCooldown = 0f;
        boostTimer = 0f;

        audioSource = GetComponent<AudioSource>();

        if (rb == null)
        {
            Debug.LogError("Player::Start can't find RigidBody2D");
        }

    }

    // Update is called once per frame
    void Update()
    {
        
        // Keep player inside camera bounds 
        // TODO fix this, make it better, maybe look into collsion for it or for tracking pos
        var dist = (transform.position - Camera.main.transform.position).z;

        var leftBorder = Camera.main.ViewportToWorldPoint(
          new Vector3(0, 0, dist)).x;

        var rightBorder = Camera.main.ViewportToWorldPoint(
          new Vector3(1, 0, dist)).x;

        var topBorder = Camera.main.ViewportToWorldPoint(
          new Vector3(0, 0, dist)).y;

        var bottomBorder = Camera.main.ViewportToWorldPoint(
          new Vector3(0, 0.98f, dist)).y;

        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, leftBorder, rightBorder),
            Mathf.Clamp(transform.position.y, topBorder, bottomBorder),
            transform.position.z);

        if (fireCooldown > 0f)
        {
            fireCooldown -= Time.deltaTime;
        }

        if(boostTimer > 0f)
        {
            boostTimer -= Time.deltaTime;
        }

        if (Input.GetButtonDown("keyFireLeft") || Input.GetButtonDown("joyFireX"))
        {
            ammoType = 0;
            this.GetComponentInChildren<LineRenderer>().material = blueTrail;
            if (fireCooldown <= 0f)
            {
                this.GetComponent<AudioSource>().Play();
                GameObject go = Instantiate(blueBulletPrefab, gameObject.transform.position, Quaternion.identity);
                Bullet bullet = go.GetComponent<Bullet>();
                bullet.targetVector = new Vector3(1, 0, 0);
                fireCooldown = fireRate;
            }
        }
        if (Input.GetButtonDown("keyFireUp") || Input.GetButtonDown("joyFireY"))
        {
            ammoType = 1;
            this.GetComponentInChildren<LineRenderer>().material = yellowTrail;
            if (fireCooldown <= 0f)
            {
                this.GetComponent<AudioSource>().Play();
                GameObject go = Instantiate(yellowBulletPrefab, gameObject.transform.position, Quaternion.identity);
                Bullet bullet = go.GetComponent<Bullet>();
                bullet.targetVector = new Vector3(1, 0, 0);
                fireCooldown = fireRate;
            }
        }
        if (Input.GetButtonDown("keyFireRight") || Input.GetButtonDown("joyFireB"))
        {
            ammoType = 2;
            this.GetComponentInChildren<LineRenderer>().material = redTrail;
            if (fireCooldown <= 0f)
            {
                this.GetComponent<AudioSource>().Play();
                GameObject go = Instantiate(redBulletPrefab, gameObject.transform.position, Quaternion.identity);
                Bullet bullet = go.GetComponent<Bullet>();
                bullet.targetVector = new Vector3(1, 0, 0);
                fireCooldown = fireRate;
            }
        }
        if (Input.GetButtonDown("keyFireDown") || Input.GetButtonDown("joyFireA"))
        {
            ammoType = 3;
            this.GetComponentInChildren<LineRenderer>().material = greenTrail;
            if (fireCooldown <= 0f)
            {
                this.GetComponent<AudioSource>().Play();
                GameObject go = Instantiate(greenBulletPrefab, gameObject.transform.position, Quaternion.identity);
                Bullet bullet = go.GetComponent<Bullet>();
                bullet.targetVector = new Vector3(1, 0, 0);
                fireCooldown = fireRate;
            }
        }
        if (Input.GetButtonDown("keyBoostSpace") || Input.GetButtonDown("joyRightBumper"))
        {
            if (boostTimer <= 0f)
            {
                audioSource.PlayOneShot(shipBoost, 0.5f);

                backgroundImage.GetComponent<ScrollingScript>().speed *= boostSpeedMulti;
                middlegroundImage.GetComponent<ScrollingScript>().speed *= boostSpeedMulti;
                levelGeometry.GetComponent<ScrollingScript>().speed *= boostSpeedMulti;

                StartCoroutine(Boost(boostLength));
                UpdateScore(boostScoreBonus);
                boostTimer = boostCooldown;
            }
        }

        UpdateScore();
    }

    IEnumerator Boost(float bL)
    {
        yield return new WaitForSeconds(boostLength);
        backgroundImage.GetComponent<ScrollingScript>().speed /= boostSpeedMulti;
        middlegroundImage.GetComponent<ScrollingScript>().speed /= boostSpeedMulti;
        levelGeometry.GetComponent<ScrollingScript>().speed /= boostSpeedMulti;
    }

    public void UpdateScore()
    {
        scoreUpdateTime += Time.deltaTime;
        if(scoreUpdateTime >= 1)
        {
            score++;
            scoreUpdateTime = 0;
            //Debug.Log(score);
        }
       
    }
    public void UpdateScore(int bonus)
    {
        score += bonus;
            
    }
        //TODO play sound effects
    public void CheckHealth()
    {
        if(hitPoints > 0)
        {

            audioSource.PlayOneShot(shipCollision, 1f);

            CameraShake camShake = Camera.main.GetComponent<CameraShake>();
            camShake.shakeDuration = 0.75f;

            SpriteRenderer playerSprite = GetComponentInChildren<SpriteRenderer>();
            LineRenderer playerLine = GetComponentInChildren<LineRenderer>();
            playerSprite.enabled = false;
            playerLine.enabled = false;

            StartCoroutine(Invincibility());
        }
        else if(hitPoints <= 0)
        {
            audioSource.PlayOneShot(shipExplosion, 1f);

            CameraShake camShake = Camera.main.GetComponent<CameraShake>();
            camShake.shakeDuration = 0.75f;

            SpriteRenderer playerSprite = GetComponentInChildren<SpriteRenderer>();
            LineRenderer playerLine = GetComponentInChildren<LineRenderer>();
            playerSprite.enabled = false;
            playerLine.enabled = false;

            Vector2 stopScrolling = new Vector2(0.0f, 0.0f);
            backgroundImage.GetComponent<ScrollingScript>().speed = stopScrolling;
            middlegroundImage.GetComponent<ScrollingScript>().speed = stopScrolling;
            levelGeometry.GetComponent<ScrollingScript>().speed = stopScrolling;

            playerExplosion.transform.position = this.transform.position;
            playerExplosion.Play();

            StartCoroutine(GameObject.FindObjectOfType<SceneFader>().Fade(SceneFader.FadeDirection.Out));
            StartCoroutine(FadeAudioOut(backgroundMusic, FadeTime));
            StartCoroutine(ScreenLoad("DeathScreen",2f));
        }
    }

    public void WinGame()
    {
        CameraShake camShake = Camera.main.GetComponent<CameraShake>();
        camShake.shakeDuration = 0.75f;

        SpriteRenderer playerSprite = GetComponentInChildren<SpriteRenderer>();
        LineRenderer playerLine = GetComponentInChildren<LineRenderer>();
        playerSprite.enabled = false;
        playerLine.enabled = false;

        audioSource.PlayOneShot(deliveringPayload, 1f);
        playerExplosion.transform.position = this.transform.position;
        playerExplosion.Play();

        Vector2 stopScrolling = new Vector2(0.0f, 0.0f);
        backgroundImage.GetComponent<ScrollingScript>().speed = stopScrolling;
        middlegroundImage.GetComponent<ScrollingScript>().speed = stopScrolling;
        levelGeometry.GetComponent<ScrollingScript>().speed = stopScrolling;

        UpdateScore(10000);
        StartCoroutine(GameObject.FindObjectOfType<SceneFader>().Fade(SceneFader.FadeDirection.Out));
        StartCoroutine(FadeAudioOut(backgroundMusic, FadeTime));
        StartCoroutine(ScreenLoad("WinScreen",3f));
    }

    // Collision function
    void OnTriggerEnter2D(Collider2D col)
    {
        if (!invincible)
        {
            if(col.gameObject.tag == "Obstacle")
            {
                // Reduce hitpoints and check health, if 0 then go to death screen, if > 0 give player iframes
                hitPoints -= 1;
                CheckHealth();
            }
            else if (col.gameObject.tag == "FireWall")
            {
                // TODO add death effect
                // Reduce hitpoints and check health, if 0 then go to death screen, if > 0 give player iframes
                hitPoints = 0;
                CheckHealth();
            }
        }
        if(col.gameObject.tag == "FinalPortTarget")
        {
            WinGame();
        }
    }
    IEnumerator FadeAudioOut(AudioSource audioSource, float FadeTime)
    {
        if(audioSource){
            float startVolume = audioSource.volume;

            while (audioSource.volume > 0)
            {
                audioSource.volume -= startVolume * Time.deltaTime / FadeTime;

                yield return null;
            }

            audioSource.Stop();
            audioSource.volume = startVolume;
        }
    }

    IEnumerator ScreenLoad(string screenToLoad,float time)
    {
        yield return new WaitForSeconds(time);
        PlayerPrefs.SetInt("Score", this.score);
        SceneManager.LoadScene(screenToLoad);
    }

    // Invicibility coroutine, blinks sprite and makes ship invicible for some time (invincibilityTime)
    IEnumerator Invincibility()
    {
        invincible = true;
        SpriteRenderer playerSprite = GetComponentInChildren<SpriteRenderer>();
        LineRenderer playerLine = GetComponentInChildren<LineRenderer>();
        Debug.Log("Invincible!");
        
        for(var i = 0; i < invincibilityBlinkDuration; i++)
        {
            playerSprite.enabled = false;
            playerLine.enabled = false;
            yield return new WaitForSeconds(.1f);
            playerSprite.enabled = true;
            playerLine.enabled = true;
            yield return new WaitForSeconds(.1f);

        }

        yield return new WaitForSeconds(invincibilityTime);
        invincible = false;
    }

    void FixedUpdate()
    {
        
        if (Input.GetAxisRaw("Vertical") != 0)
        {


            float horizontalMovement = 0;
            float verticalMovement = Input.GetAxisRaw("Vertical") * moveSpeed;
            Vector2 directionOfMovement = new Vector3(horizontalMovement, verticalMovement);
            
            // Tilt ship smoothly up and down based on direction
            if(verticalMovement > 0f)
            { 
                rb.MoveRotation(Mathf.LerpAngle(rb.rotation, 45f, 2f * Time.deltaTime));
            }else if(verticalMovement < 0f)
            {
                rb.MoveRotation(Mathf.LerpAngle(rb.rotation, -45f, 2f * Time.deltaTime));
            }
            

            rb.AddForce(directionOfMovement);
        }
    }
}

