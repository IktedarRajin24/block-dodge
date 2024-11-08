using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI healthText;
    
    Rigidbody2D rb;

    public float moveSpeed = 5f;
    private int health = 5;
    private int score = 0;
    private bool isGameEnded = false;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        if (isGameEnded) return;
        if (health == 0)
        {
            EndGame();

        }
        if (health >= 0)
        {
            scoreText.text = score.ToString();
            healthText.text = health.ToString();
        }
        if (Input.GetMouseButton(0))
        {

            Vector2 touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if(touchPosition.x < 0)
            {
                MovePlayer(moveSpeed);
            }else if(touchPosition.x > 0)
            {
                MovePlayer(-moveSpeed);
            }
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    void MovePlayer(float moveSpeed)
    {
        //rb.AddForce(Vector2.left * moveSpeed);
        transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Bomb" || collision.collider.tag == "Block")
        {
            Destroy(collision.gameObject);
            health--;
        }
        if(collision.collider.tag == "Fruits")
        {
            Destroy(collision.gameObject);
            score++;
        }
    }

    private void EndGame()
    {
        isGameEnded = true;
        Invoke("ReloadScene", 2f);

    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
