using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3f;
    [SerializeField]
    private int pwuId; //0 = Triple Shot, 1 = Speed, 2 = Shield 
    public float spawnRate;
    private float newPosition;
    private UiManager _uiManager;
    [SerializeField]
    private AudioClip _audioClipPickup;
    // Start is called before the first frame update
    void Start()
    {
        newPosition = Random.Range(-8f, 8f);
        transform.position = new Vector3(newPosition, transform.position.y, 0);
        transform.position = new Vector3(transform.position.x, 8f, 0);

        _uiManager = GameObject.Find("Canvas").GetComponent<UiManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        maxVertical();
        maxHorizontal();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            if(player != null)
            {
                AudioSource.PlayClipAtPoint(_audioClipPickup, Camera.main.transform.position, 0.3f);
                if(pwuId == 0) { 
                    player.enableTripleShot();
                } else if (pwuId == 1)
                {
                    player.enableSpeedBoost();
                } else if(pwuId == 2)
                {
                    player.enableShields();
                }
                _uiManager.updateScore(50);
            }
            Destroy(this.gameObject);
        }
        
    }

    private void maxVertical()
    {
        if (transform.position.y < -7)
        {
            Destroy(this.gameObject);
        }
    }

    private void maxHorizontal()
    {
        
        if (transform.position.x < -10.2f)
        {
            Destroy(this.gameObject);
        }
        else if (transform.position.x > 10.2f)
        {
            Destroy(this.gameObject);
        }
   
    }
}
