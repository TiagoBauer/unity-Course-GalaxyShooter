using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAI : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3f;
    [SerializeField]
    private float _health = 100;
    private float newPosition = 0f;
    public GameObject deathAnimation;
    public float spawnRate;
    private UiManager _uiManager;
    [SerializeField]
    private AudioClip _audioClip;
    // Start is called before the first frame update
    void Start()
    {
        newPosition = Random.Range(-8f, 8f);
        transform.position = new Vector3(newPosition, transform.position.y, 0);
        transform.position = new Vector3(transform.position.x, 6f, 0);

        _uiManager = GameObject.Find("Canvas").GetComponent<UiManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * (_speed * -1));
        maxHorizontal();
        maxVertical();
        checkEnd();
    }

    private void maxVertical()
    {
        if (transform.position.y < -6f)
        {
            transform.position = new Vector3(transform.position.x, 6f, 0);
            newPosition = Random.Range(-8, 8f);
            transform.position = new Vector3(newPosition, transform.position.y, 0);
        }
    }

    private void maxHorizontal()
    {
        if (transform.position.x < -11f)
        {
            transform.position = new Vector3(11f, transform.position.y, 0);
        }
        else if (transform.position.x > 11f)
        {
            transform.position = new Vector3(-11f, transform.position.y, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            player.doDamage();
            doDamage(_health);
        } else if (other.tag == "Laser")
        {
            Lazer laser = other.GetComponent<Lazer>();
            Destroy(other.gameObject);
            doDamage(laser.dps);
        } else if (other.tag == "PowerUp")
        {
            Destroy(other);
        }  
    }

    private void doDamage(float hit)
    {
        this._health = this._health - hit;
        if(this._health < 1f)
        {
            _uiManager.updateScore(100);
            Instantiate(deathAnimation, transform.position, transform.rotation);
            AudioSource.PlayClipAtPoint(_audioClip, Camera.main.transform.position, 0.3f);
            Destroy(this.gameObject);
        }
    }
    private void checkEnd()
    {
        if (_uiManager.gameOver == true)
        {
            Instantiate(deathAnimation, transform.position, transform.rotation);
            AudioSource.PlayClipAtPoint(_audioClip, Camera.main.transform.position, 0.3f);
            Destroy(gameObject);
        }
    }
}
