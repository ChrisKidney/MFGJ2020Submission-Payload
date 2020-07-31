using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using SpriteShatter;
using TMPro;
using UnityEngine;

public class UIScoreUpdater : MonoBehaviour   
{
    public GameObject playerObject;
    private int score;
    private TMP_Text scoreText;
    private Shatter script;

    // Start is called before the first frame update
   // void Start()
   // {
        
   // }

    public void WallHitManager(int bonusScore, GameObject go)
    {

        Player player = playerObject.GetComponent<Player>();
        player.score += bonusScore;

        script = go.GetComponentInChildren<Shatter>();
        script.shatter();

        CameraShake camShake = Camera.main.GetComponent<CameraShake>();
        camShake.shakeAmount = 0.08f;
        camShake.shakeDuration = 0.75f;
    }
    // Update is called once per frame
    void Update()
    {
        Player player = playerObject.GetComponent<Player>();

        score = player.score;

        scoreText = GetComponent<TMP_Text>();
        scoreText.text = score.ToString();

    }
}
