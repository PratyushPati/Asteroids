using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public player player;
    public ParticleSystem explosion;
    public float respawntime=3f;
    public int lives=3;
    public Text livesText;
    public int Score = 0;
    public Text ScoreText;
    public GameObject gm;

    public void AsteroidDestroyed(asterioid asterioid)
    {
        explosion.transform.position = asterioid.transform.position;
        explosion.Play();
        if (asterioid.size < 0.75f)
        {
            Score += 100;
        }
        if (asterioid.size < 1.25f)
        {
            Score += 50;
        }
        else 
        {
            Score += 25;
        }
        ScoreText.text = "SCORE:"+Score.ToString();
    }

    public void PlayerDied()
    {
        explosion.transform.position = player.transform.position;
        explosion.Play();
        lives--;
        livesText.text = "LIVES:" + lives.ToString();
        if (lives <= 0)
        {
            gameover();
        }
        else
        {
            Invoke(nameof(respawn), respawntime);
        }
    }
    private void respawn()
    {
        player.transform.position = Vector3.zero;
        player.gameObject.layer = LayerMask.NameToLayer("IgnoreCollision");
        player.gameObject.SetActive(true);
        Invoke(nameof(turnoncollision), 3f);
    }
    private void turnoncollision()
    {
        player.gameObject.layer = LayerMask.NameToLayer("player");
    }
    private void gameover()
    {
        gm.SetActive(true);
    }
}
