using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnManager : MonoBehaviour
{

    [SerializeField]
    private GameObject _enemyShip;

    [SerializeField]
    private GameObject[] _powerUps;
    private int item;
    private int count = 0;
    private int control = 0;
    private UiManager _uiManager;
    // Start is called before the first frame update
    void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UiManager>();
        StartCoroutine(enemySpawnRoutine());
        StartCoroutine(pwuSpawnRoutine()); 
    }

    private IEnumerator enemySpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(_enemyShip.GetComponent<enemyAI>().spawnRate);
            if (!_uiManager.gameOver){
                Instantiate(_enemyShip, new Vector3(8, 0, 0), Quaternion.identity);
            }
        }
    }

    private IEnumerator pwuSpawnRoutine()
    {
        while (true) { 
            item = Random.Range(0, 3);
            yield return new WaitForSeconds(_powerUps[item].GetComponent<PowerUp>().spawnRate);
            if (!_uiManager.gameOver)
            {
                Instantiate(_powerUps[item], new Vector3(8, 8, 0), Quaternion.identity);
            }
         }
    }
}
