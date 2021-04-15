using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageText : MonoBehaviour
{
    public float lifeTime = 0.7f;
    TextMeshPro text;
    Vector3 direction;
    float speed = 4;
    // Start is called before the first frame update

    public void Init(int damage, Color color){
        text = GetComponent<TextMeshPro>();
        text.color = color;
        text.SetText(damage.ToString());
        direction = Random.insideUnitCircle;
    }

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction*speed*Time.deltaTime;
        speed = speed <= 0 ? 0 : speed-Time.deltaTime; 
    }
}
