using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{


    private float _horizontaKey;
    private float _verticalKey;
    private float _canFire;
    private float _initialSpeed;

    [SerializeField]
    private int _health = 3;
    [SerializeField]
    private GameObject _deathAnimation;
    [SerializeField]
    private GameObject _lazerPrefab;
    [SerializeField]
    private GameObject _shield;
    public float speed = 5f;
    public bool wrap;
    public float fireRate = 0.25f;
    public bool pwuTripleShoot = false;
    public bool pwuSpeedBoost = false;
    public bool pwuShield = false;
    private UiManager _uiManager;
    [SerializeField]
    private AudioSource _audioSource;
    [SerializeField]
    private AudioClip _audioClipExplosion;
    [SerializeField]
    private GameObject[] _engine;
    private int damageEngine;
    private int lastDamage;
    // Start is called before the first frame update
    void Start()
    {
        _initialSpeed = speed;

        _uiManager = GameObject.Find("Canvas").GetComponent<UiManager>();

        if(_uiManager != null)
        {
            _uiManager.updateLife(_health);
        }

        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        movement();
        maxHorizontal();
        maxVertical();
        primaryFire();
    }

    private void movement()
    {
        _verticalKey = Input.GetAxis("Vertical");
        _horizontaKey = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * Time.deltaTime * speed * _horizontaKey);
        transform.Translate(Vector3.up * Time.deltaTime * speed * _verticalKey);
    }

    private void maxVertical()
    {
        if (transform.position.y > 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y < -4)
        {
            transform.position = new Vector3(transform.position.x, -4, 0);
        }
    }

    private void maxHorizontal()
    {
        if (wrap)
        {
            if (transform.position.x < -9.8f)
            {
                transform.position = new Vector3(9.8f, transform.position.y, 0);
            }
            else if (transform.position.x > 9.8f)
            {
                transform.position = new Vector3(-9.8f, transform.position.y, 0);
            }
        }
        else
        {
            if (transform.position.x < -8.2f)
            {
                transform.position = new Vector3(-8.2f, transform.position.y, 0);
            }
            else if (transform.position.x > 8.2f)
            {
                transform.position = new Vector3(8.2f, transform.position.y, 0);
            }
        }
    }

    private void primaryFire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if(Time.time > _canFire)
            {
                _audioSource.Play();
                if (pwuTripleShoot)
                {
                    Instantiate(_lazerPrefab, transform.position + new Vector3(0, 0.72f, 0), Quaternion.identity);
                    Instantiate(_lazerPrefab, transform.position + new Vector3(-0.56f, -0.22f, 0), Quaternion.identity);
                    Instantiate(_lazerPrefab, transform.position + new Vector3(0.56f, -0.22f, 0), Quaternion.identity);
                }
                else if (!pwuTripleShoot)
                {
                    Instantiate(_lazerPrefab, transform.position + new Vector3(0, 0.72f, 0), Quaternion.identity);
                }
                _canFire = Time.time + fireRate;
            }
            
        }
    }
    public void enableTripleShot()
    {
        pwuTripleShoot = true;
        StartCoroutine(tripleShotPwuRoutine());
    }

    public IEnumerator tripleShotPwuRoutine()
    {
        yield return new WaitForSeconds(5f);
        pwuTripleShoot = false;
    }

    public void enableSpeedBoost()
    {
        pwuSpeedBoost = true;
        StartCoroutine(speedBoosRoutine());
    }

    public IEnumerator speedBoosRoutine()
    {
        speed = (speed * 2);
        yield return new WaitForSeconds(5f);
        pwuSpeedBoost = false;
        speed = _initialSpeed;
    }

    public void doDamage()
    {
        if (!pwuShield) {
            _health--;
            _uiManager.updateLife(_health);
            randonDamage();
            if (_health < 1) { 
                Instantiate(_deathAnimation, transform.position, Quaternion.identity);
                _uiManager.gameOver = true;
                _uiManager.title.gameObject.SetActive(true);
                _uiManager.pressSpace.gameObject.SetActive(true);
                _uiManager.gameObject.SetActive(true);
                AudioSource.PlayClipAtPoint(_audioClipExplosion, Camera.main.transform.position, 0.3f);
                Destroy(this.gameObject);
            }
        } else {
            pwuShield = false;
            _shield.SetActive(false); 
        }
    }

    public void enableShields()
    {
        pwuShield = true;
        _shield.SetActive(true);
    }

    private void randonDamage()
    {
        damageEngine = Random.Range(0, 2);
        if(!_engine[damageEngine].activeSelf)
        {
            _engine[damageEngine].SetActive(true);
            lastDamage = damageEngine;
        } else
        {
            while (damageEngine == lastDamage) { 
                damageEngine = Random.Range(0, 2);
            }
            _engine[damageEngine].SetActive(true);
        }
    }
}
