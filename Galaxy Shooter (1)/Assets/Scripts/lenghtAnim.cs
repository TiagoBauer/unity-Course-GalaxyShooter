using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lenghtAnim : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(deathAnim());
    }
    
    private IEnumerator deathAnim()
    {
        yield return new WaitForSeconds(2f);
        Destroy(this.gameObject);
    }
}
