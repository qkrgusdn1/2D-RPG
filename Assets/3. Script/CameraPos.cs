using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPos : MonoBehaviour
{
    GameObject playerObj;
    Vector3 originalPos;
    private void Update()
    {
        if(playerObj == null)
        {
            playerObj = GameObject.FindGameObjectWithTag("Player");
        }
        else
        {
            originalPos = transform.position;
            transform.position = new Vector3(playerObj.transform.position.x, playerObj.transform.position.y + 3, -10);
        }
    }

    public IEnumerator Shake(float duration, float magnitude)
    {
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            originalPos = new Vector3(playerObj.transform.position.x, playerObj.transform.position.y + 3, -10);
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.position = new Vector3(originalPos.x + x, originalPos.y + y, originalPos.z);

            elapsed += Time.deltaTime;

            yield return null;
        }
        transform.position = new Vector3(playerObj.transform.position.x, playerObj.transform.position.y + 3, -10);
    }
}
