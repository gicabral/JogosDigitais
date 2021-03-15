using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : SteerableBehaviour, IShooter, IDamageable
{
    Animator animator;
    GameManager gm;

    // private int lifes;
    public AudioClip shootSFX;
    public AudioSource rocketCollision;
    public InputField highScoreInput;

    public GameObject Engine;
    private ParticleSystem ps;
    public GameObject bullet;
    public Transform weapon;
    public float bullletForce;

    public float shotDelay = 1.0f;
    public float angleStep = 10.0f;
    private float _lastShootTimeStamp = 0.0f;
    private Vector3 direcao;

    public GameObject explosion;
    public float screenTop = 12;
    public float screenBottom = -12;
    public float screenLeft = -16;
    public float screenRight = 16;

    private void Start()
    {
        ps = Engine.GetComponent<ParticleSystem>();
        ps.Stop();
        animator = GetComponent<Animator>();
        animator.enabled = false;
        // lifes = 10;
        gm = GameManager.GetInstance();
    }

    public void Shoot()
    {
        if(Time.time - _lastShootTimeStamp < shotDelay) return;
        AudioManager.PlaySFX(shootSFX);
        _lastShootTimeStamp = Time.time;
        GameObject newBullet = Instantiate(bullet, weapon.position, transform.rotation);
        newBullet.GetComponent<Rigidbody2D> ().AddRelativeForce(Vector2.up * bullletForce);
    }

    public void TakeDamage()
    {
        gm.vidas--;  
        if(gm.vidas <= 0 && gm.gameState == GameManager.GameState.GAME)
        {
            GameObject newExplosion = Instantiate(explosion, transform.position, transform.rotation);
            Destroy(newExplosion, 3f);
            if(gm.CheckForHighScore(gm.pontos)){
                gm.ChangeState(GameManager.GameState.NEWHIGHSCORE);
            }else{
                gm.ChangeState(GameManager.GameState.ENDGAME);
            }
        } 
    }

    public void HighScoreInput()
    {
        string newInput = highScoreInput.text;
        Debug.Log(newInput);
        gm.ChangeState(GameManager.GameState.ENDGAME);
        PlayerPrefs.SetString("highscoreName", newInput);
        PlayerPrefs.SetInt("highScore", gm.highscore);
    }

    public void Die()
    {
        Destroy(gameObject);
    }
    

    void FixedUpdate()
    {
        if (gm.gameState != GameManager.GameState.GAME) return;
        float yInput = Input.GetAxis("Vertical");
        float xInput = Input.GetAxis("Horizontal");
        Thrust(xInput, yInput);

        if(Input.GetAxisRaw("Jump") != 0){
            Shoot();
        }
    } 
    public override void Thrust(float x, float y){
        //transform.Rotate(Vector3.forward);
        // Debug.Log(x);
        // Debug.Log(y);
        // rb.AddRelativeForce(transform.up * y);
        // rb.AddTorque(x * rb.inertia, ForceMode2D.Impulse);
        rb.MoveRotation(rb.rotation - x * angleStep);
        Vector2 vec = transform.up;
        rb.MovePosition(rb.position + vec*(y * td.thrustIntensity.y) * Time.fixedDeltaTime);

        // rb.AddRelativeForce(Vector3.right * y * driveForce);
        // rb.AddTorque(angleStep * x);
    } 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Inimigos") || collision.CompareTag("InimigoPersegue") || collision.CompareTag("InimigoTiro") || collision.CompareTag("InimigoRochaG") || collision.CompareTag("InimigoRochaM") || collision.CompareTag("InimigoRochaP"))
        {
            rocketCollision.Play();
            Destroy(collision.gameObject);
            TakeDamage();
        }
    } 
    

    private void Update()
    {
        if (gm.gameState != GameManager.GameState.GAME) return;
        if (Input.GetKeyDown("up") || Input.GetKeyDown("w"))
        {
            ps.Play();
            animator.enabled = true;
        }
        if (Input.GetKeyDown("down") || Input.GetKeyDown("s"))
        {
            animator.enabled = true;
        }
        if (Input.GetKeyUp("up") || Input.GetKeyUp("w"))
        {
            ps.Stop();
            animator.enabled = false;
        }
        if (Input.GetKeyUp("down") || Input.GetKeyUp("s"))
        {
            animator.enabled = false;
        }
        if(Input.GetKeyDown(KeyCode.Escape) && gm.gameState == GameManager.GameState.GAME) {
            gm.ChangeState(GameManager.GameState.PAUSE);
        }

        Vector2 newPos = transform.position;
        if(transform.position.y > screenTop){
            newPos.y = screenTop;
        }
        if(transform.position.y < screenBottom){
            newPos.y = screenTop;
        }
        if(transform.position.x > screenRight){
            newPos.x = screenLeft;
        }
        if(transform.position.x < screenLeft){
            newPos.x = screenRight;
        }
        transform.position = newPos;
    }


    
}
