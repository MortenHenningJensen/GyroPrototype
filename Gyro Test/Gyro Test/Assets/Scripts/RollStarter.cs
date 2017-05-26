using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollStarter : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            StartCoroutine(ToDestroy());
        }
    }

    IEnumerator ToDestroy()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
    }
}
