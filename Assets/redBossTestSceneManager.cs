using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class redBossTestSceneManager : MonoBehaviour
{
    // Start is called before the first frame update

   public GameManager gm;
   public GameObject prefab;
    void Start()
    {
         
        Vector3 pos = new Vector3(0,5,0);
        ICharacter enemy = Instantiate(prefab, pos, Quaternion.identity).GetComponent<ICharacter>();
        enemy.Init();
        gm.AddEnemy(enemy);
      
       
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
