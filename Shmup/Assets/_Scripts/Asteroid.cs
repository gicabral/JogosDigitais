/*
    Adaptado de:
   © OXMOND / www.oxmond.com 

*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{

    public GameObject rock;
    public GameObject asteroidMedium;
    public GameObject asteroidSmall;
    public Gameplay gameplay;
    GameManager gm;
    private float maxRotation;
    private float rotationX;
    private float rotationY;
    private float rotationZ;
    private Rigidbody2D rb;
    private float maxSpeed;
    private int _generation;
    public int asteroidSize;

    void Start()
    {
        gm = GameManager.GetInstance();

        maxRotation = 25f;
        rotationX = Random.Range(-maxRotation, maxRotation);
        rotationY = Random.Range(-maxRotation, maxRotation);
        rotationZ = Random.Range(-maxRotation, maxRotation);

        rb = rock.GetComponent<Rigidbody2D>();

        float speedX = Random.Range(200f, 800f);
        int selectorX = Random.Range(0, 2);
        float dirX = 0;
        if (selectorX == 1) { dirX = -1; }
        else { dirX = 1; }
        float finalSpeedX = speedX * dirX;
        rb.AddForce(transform.right * finalSpeedX);

        float speedY = Random.Range(200f, 800f);
        int selectorY = Random.Range(0, 2);
        float dirY = 0;
        if (selectorY == 1) { dirY = -1; }
        else { dirY = 1; }
        float finalSpeedY = speedY * dirY;
        rb.AddForce(transform.up * finalSpeedY);

    }

    public void SetGeneration(int generation)
    {
        _generation = generation;
    }

    void Update()
    {
        if (gm.gameState != GameManager.GameState.GAME) return;
        rock.transform.Rotate(new Vector3(rotationX, rotationY, 0) * Time.deltaTime);
        float dynamicMaxSpeed = 3f;
        rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -dynamicMaxSpeed, dynamicMaxSpeed), Mathf.Clamp(rb.velocity.y, -dynamicMaxSpeed, dynamicMaxSpeed));
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("bullet"))
        {
            if (asteroidSize == 3)
            {
                Instantiate(asteroidMedium, transform.position, transform.rotation); 
                Instantiate(asteroidMedium, transform.position, transform.rotation);
            }
            else if (asteroidSize == 2){
                Instantiate(asteroidSmall, transform.position, transform.rotation); 
                Instantiate(asteroidSmall, transform.position, transform.rotation);
            }
            else if(asteroidSize == 1){
                Destroy();
            }
            Destroy();  
        }
    } 

    public void Destroy()
    {
        Destroy(gameObject, 0.01f);
    }

}