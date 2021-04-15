using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MankindGames;
public class CameraControl : MonoBehaviour
{

    Transform target;
    public float topLimit = 2;
    public float bottomLimit = -1.5f;
    public float leftLimit = -1.5f;
    public float rightLimit = 1.5f;

    float yOffset;
    float xOffset;
    Stats stats;
    bool initialized;

    Vector3 currentpos;

    public void SetTarget(Character character)
    {
        target = character.transform;
        transform.position = new Vector3(target.transform.position.x, target.transform.position.y, -10);
        stats = character.GetStats();
        initialized = true;
        //        Debug.Log("target set"+target.transform.position.x+""+target.transform.position.y);
    }
    public void SetTheCameraOnTargetPosInstantly()
    {
        transform.position = 
            new Vector3(target.transform.position.x, target.transform.position.y, -10);
    }
    void Update()
    {
        if (!initialized) return;
            yOffset = target.position.y - transform.position.y;
            xOffset = target.position.x - transform.position.x;
            Vector2 dir = Vector2.zero;
            if (yOffset > topLimit || yOffset < bottomLimit)
            {
                dir += new Vector2(0, yOffset);

            }
            if (xOffset > rightLimit || xOffset < leftLimit)
            {
                dir += new Vector2(xOffset, 0);
            }
            if (dir != Vector2.zero)
            {
                Move(dir);
            }

    }

    void Move(Vector2 dir)
    {
        Vector3 direction = new Vector3(dir.x, dir.y, 0).normalized;
        transform.position += direction * stats.Speed * Time.deltaTime;
        currentpos = transform.position;

    }

    public IEnumerator CameraShake(float duration, float magnitude)
    {

        float time = 0.0f;

        while (time < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;
            transform.localPosition = currentpos + new Vector3(x, y, 0);
            // transform.position = currentpos;
            time += Time.deltaTime;
            yield return null;
        }


    }
}
