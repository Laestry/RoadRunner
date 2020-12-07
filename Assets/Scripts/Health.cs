using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [Serializable]
    private class ObamubeSource
    {
        [SerializeField]
        public AudioSource ouch, pointsYes, thankYou;
    }
    [SerializeField]
    private ObamubeSource audio;

    private float playerHealth;
    public GameObject gameController;
    public RectTransform healthBar;
    public float maxHealth = 100;
    public Text score;
    [Range(1,10)]
    public int damage = 5;
    private void Awake()
    {
        playerHealth = maxHealth;
    }

    private void OnCollisionEnter(Collision collision)    
    {
        if (collision.gameObject.CompareTag("Hazard"))
        {
            TakeDamage(damage);
            audio.ouch.Stop();
            audio.ouch.Play();
        }
        if (collision.gameObject.CompareTag("Point"))
        {
            gameController.GetComponent<GameController>().pointTaken = false;
            Destroy(collision.gameObject);
            score.text = (int.Parse(score.text) + 1).ToString();
            TakeDamage(-damage);
            audio.pointsYes.Stop();
            audio.pointsYes.Play();
        }
    }

    private void TakeDamage(int damage)
    {
        if (playerHealth - damage > 100f)
            return;
        
        playerHealth -= damage;
        float percent = playerHealth / maxHealth;
        healthBar.offsetMax = new Vector2(
            -478.7f + 478.7f * percent,
                healthBar.offsetMax.y);

        if (playerHealth == 0)
        {
            audio.thankYou.Play();
            gameController.GetComponent<GameController>().GameOver();
            score.text = "0";
            healthBar.offsetMax = new Vector2(
                0,
                healthBar.offsetMax.y);
            playerHealth = maxHealth;
        }
    }
}
