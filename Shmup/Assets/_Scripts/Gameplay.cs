/*
    Adaptado de:
   © OXMOND / www.oxmond.com 

*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameplay : MonoBehaviour
{

    public GameObject asteroid;
    GameManager gm;
    private int _startLevelAsteroidsNum;
    private int levelAsteroidNum;
    private int asteroidLife;
    public float shotDelay = 2.0f;
    private float _lastShootTimeStamp = 0.0f;


    private void Start()
    {
        gm = GameManager.GetInstance();
        asteroid.SetActive(false);
    }

    private void Update()
    {
        if (gm.gameState != GameManager.GameState.GAME) return;
        
        if (asteroidLife <= 0)
        {
            asteroidLife = 6;
            CreateAsteroids(1);
        }
        if(Time.time - _lastShootTimeStamp >= shotDelay){
            CreateAsteroids(10);
            _lastShootTimeStamp = Time.time;
        }
    }

    private void CreateAsteroids(float asteroidsNum)
    {
        for (int i = 1; i <= asteroidsNum; i++)
        {
            GameObject AsteroidClone = Instantiate(asteroid, new Vector2(Random.Range(-10, 10), 6f), transform.rotation);
            AsteroidClone.GetComponent<Asteroid>().SetGeneration(1);
            AsteroidClone.SetActive(true);
        }
    }

    public void asterodDestroyed()
    {
        asteroidLife--;
    }

    public int startLevelAsteroidsNum
    {
        get { return _startLevelAsteroidsNum; }
    }

}