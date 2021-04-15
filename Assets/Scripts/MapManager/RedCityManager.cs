//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class RedCityManager: MapManager {
//    /* RedCity Manager discription
//    ● Spawn NPC of first street immediately at the start of the scene
//    ● initialize/ spawn npc on the spawnpoint
//    ●
//    ●
//    */

//    /* Note
//        MapManager variable
//        int phase



//    what we need

//    */

//    /* when adding dialouge?


//    */

//    GameObject NPCSpawnPointContainer_Area1;
//    GameObject NPCSpawnPointContainer_Area2;
//    GameObject NPCSpawnPointContainer_Area3;
//    GameObject NPCBasePrefab;
//    GameObject[] NPCArea1;
//    GameObject[] NPCArea2;
//    GameObject[] NPCArea3;
   
//   Vector3 Area2TriggerPoint;
//    Vector3 Area3TriggerPoint;
//    public Vector3[] NPCSpawnPointArea1;
//    public Vector3[] NPCSpawnPointArea2;
//    public Vector3[] NPCSpawnPointArea3;
//    Transform[] SpawnTrigger;

//    float cameraLength;

//    Sprite[] FaceSprite = new Sprite[7];
//    protected override void Start() {
//        base.Start();
//        LoadResource();
//        FindGameObjectsAndComponents();
//        setArray();
//        phase = 0;
//        justOnce[phase] = true;
//        //player.GetHit(50);
//        //player.AddCredits(100);
//    }
//    /*protected override */void Update() {
//        TriggerArea1();
        
            
//        if (TriggerCheck(Area2TriggerPoint)){
//            TriggerArea2();
////            Debug.Log("Area2Triggered");
//        }
//        if(TriggerCheck(Area3TriggerPoint)){
//            TriggerArea3();
//            //   Debug.Log("Area3Triggered");
//        }
        
//    }


//    // --------------------------------------
//    // set variables/arrays..etc
//    // --------------------------------------
//    void setArray() {
//        // ----------------------------------------------------------------
//        // Set SpawnPoint_Area1 Array
//        // ----------------------------------------------------------------
//        int count = 0;
//        foreach(Transform child in NPCSpawnPointContainer_Area1.transform) {
//            count++;
//        }
//        NPCSpawnPointArea1 = new Vector3[count];
//        NPCArea1 = new GameObject[count];

//        count = 0;
//        foreach(Transform child in NPCSpawnPointContainer_Area1.transform) {
//            NPCSpawnPointArea1[count] = child.gameObject.transform.position;
//            // Debug.Log("NPCSpawnPoint["+count+"]="+NPCSpawnPointArea1[count]);
//            // Log----------
//            count++;
//        }

//        // ----------------------------------------------------------------
//        // Set SpawnPoint_Area2 Array
//        // ----------------------------------------------------------------

//        count = 0;
//        foreach(Transform child in NPCSpawnPointContainer_Area2.transform) {
//            count++;
//        }
//        NPCSpawnPointArea2 = new Vector3[count];
//        NPCArea2 = new GameObject[count];

//        count = 0;
//        foreach(Transform child in NPCSpawnPointContainer_Area2.transform) {
//            NPCSpawnPointArea2[count] = child.gameObject.transform.position;
//            // Debug.Log("NPCSpawnPoint["+count+"]="+NPCSpawnPointArea2[count]);
//            // Log----------
//            count++;
//        }

//        // ----------------------------------------------------------------
//        // Set SpawnPoint_Area3 Array
//        // ----------------------------------------------------------------
//        count = 0;
//        foreach(Transform child in NPCSpawnPointContainer_Area3.transform) {
//            count++;
//        }
//        NPCSpawnPointArea3 = new Vector3[count];
//        NPCArea3 = new GameObject[count];

//        count = 0;
//        foreach(Transform child in NPCSpawnPointContainer_Area3.transform) {
//            NPCSpawnPointArea3[count] = child.gameObject.transform.position;
//            // Debug.Log("NPCSpawnPoint["+count+"]="+NPCSpawnPointArea3[count]);
//            // Log----------
//            count++;
//        }
//        justOnce = new bool[4];

//        for (int i = 0; i < 4;i++){
//            justOnce[i] = false;
//        }
//    }

//    void LoadResource() {
//        FaceSprite[0] = Resources.Load<Sprite>("Sprites/Characters/Face/NPC_face1");
//        FaceSprite[1] = Resources.Load<Sprite>("Sprites/Characters/Face/NPC_face2");
//        FaceSprite[2] = Resources.Load<Sprite>("Sprites/Characters/Face/NPC_face3");
//        FaceSprite[3] = Resources.Load<Sprite>("Sprites/Characters/Face/NPC_face4");
//        FaceSprite[4] = Resources.Load<Sprite>("Sprites/Characters/Face/NPC_face5");
//        FaceSprite[5] = Resources.Load<Sprite>("Sprites/Characters/Face/NPC_face6");
//        FaceSprite[6] = Resources.Load<Sprite>("Sprites/Characters/Face/NPC_face7");
//        NPCBasePrefab = Resources.Load(
//            "Prefabs/Characters/NPC/BASE_NPC",
//            typeof(GameObject)
//        )as GameObject;
//    }
//    void FindGameObjectsAndComponents()
//    {
//        NPCSpawnPointContainer_Area1 = GameObject.Find("NPCSpawnPointContainer_Area1");
//        NPCSpawnPointContainer_Area2 = GameObject.Find("NPCSPawnPointContainer_Area2");
//        NPCSpawnPointContainer_Area3 = GameObject.Find("NPCSPawnPointContainer_Area3");
//        Area2TriggerPoint = GameObject.Find("Area2Trigger").transform.position;
//        Area3TriggerPoint = GameObject.Find("Area3Trigger").transform.position;
//        cameraLength = Camera.main.orthographicSize;

//    }
//    void SetNPCMovement(GameObject gO, int direction, int distance, int waitTime) {
//        SimpleNPCMovement simpleNPCMovement;
//        simpleNPCMovement = gO.GetComponent<SimpleNPCMovement>();
//        simpleNPCMovement.SetMovement(direction, distance, waitTime);

//    }
//    void SetNPCFace(GameObject gO, int spriteIndex) {
//        gO
//            .GetComponent<AnimationManager>()
//            .faceSprite = FaceSprite[spriteIndex];
//        gO
//            .GetComponent<AnimationManager>()
//            .faceTiltedSprite = FaceSprite[spriteIndex];
//    }

//    bool TriggerCheck(Vector3 triggerPos){
        
//        float distance = Mathf.Abs(player.transform.position.y - triggerPos.y);
//         if(distance<cameraLength) return true;
//        else return false;
//    }
//    // ---------------------------------------------------------------------------------------------------------
//    // Area1 related
//    // ---------------------------------------------------------------------------------------------------------

//    void TriggerArea1() {
//        if (justOnce[0]) {
//            SpawnArea1();
//            Area1NPCConfig();

//            justOnce[phase] = false;
//          //Debug.Log("phase" + phase + "justonce" + justOnce[phase + 1]);
//             phase++;
//            justOnce[phase] = true;
         
//        }else return;

//    }
//    void SpawnArea1() {
//        for (int i = 0; i < NPCSpawnPointArea1.Length; i++) {
//            NPCArea1[i] = Instantiate(
//                NPCBasePrefab,
//                NPCSpawnPointArea1[i],
//                Quaternion.identity
//            );
//            NPCArea1[i].transform.parent = GameObject
//                .Find("NPC_Area1")
//                .transform;
//            NPCArea1[i].name = "Area 1・NPC " + (
//                i + 1
//            );
//            NPCArea1[i]
//                .GetComponent<NPC>()
//                .Init();
//        }
//    }

//    void Area1NPCConfig() { // Edit NPC Face, Movement from here. I want to make this more simple tho.
//        //Index----- 3 young men
//        SetNPCFace(NPCArea1[0], 2);

//        // Index----- 

//    }

//    // ---------------------------------------------------------------------------------------------------------
//    // Area2 related
//    // ---------------------------------------------------------------------------------------------------------

//    void TriggerArea2() {
//        if (justOnce[1]) {
//            SpawnArea2();
//            Area2NPCConfig();

//            justOnce[phase] = false;
//          //Debug.Log("phase" + phase + "justonce" + justOnce[phase + 1]);
//           phase++;
//            justOnce[phase] = true;
           
//        }else return;

//    }

//    void SpawnArea2() {
//        for (int i = 0; i < NPCSpawnPointArea2.Length; i++) {
//            NPCArea2[i] = Instantiate(
//                NPCBasePrefab,
//                NPCSpawnPointArea2[i],
//                Quaternion.identity
//            );
//            NPCArea2[i].transform.parent = GameObject
//                .Find("NPC_Area2")
//                .transform;
//            NPCArea2[i].name = "Area 2・NPC " + (
//                i + 1
//            );
//            NPCArea2[i]
//                .GetComponent<NPC>()
//                .Init();
//        }
//    }

//    void Area2NPCConfig() { // Edit NPC Face, Movement from here. I want to make this more simple tho.
//        //Index----- 3 young men
//        SetNPCFace(NPCArea2[0], 2);
//    }

//    // ---------------------------------------------------------------------------------------------------------
//    // Area3 related
//    // ---------------------------------------------------------------------------------------------------------

//    void TriggerArea3() {
//        if (justOnce[2]) {
//            SpawnArea3();
//            Area3NPCConfig();

//            justOnce[phase] = false;
//          //Debug.Log("phase" + phase + "justonce" + justOnce[phase + 1]);
//            justOnce[phase + 1] = true;
//            phase++;
//        }else return;
        
//    }

//    void SpawnArea3() {
//        for (int i = 0; i < NPCSpawnPointArea3.Length; i++) {
//            NPCArea3[i] = Instantiate(
//                NPCBasePrefab,
//                NPCSpawnPointArea3[i],
//                Quaternion.identity
//            );
//            NPCArea3[i].transform.parent = GameObject
//                .Find("NPC_Area3")
//                .transform;
//            NPCArea3[i].name = "Area 3・NPC " + (
//                i + 1
//            );
//            NPCArea3[i]
//                .GetComponent<NPC>()
//                .Init();
//        }
//    }

//    void Area3NPCConfig() { // Edit NPC Face, Movement from here. I want to make this more simple tho.
//        //Index----- 3 young men
//        SetNPCFace(NPCArea3[0], 2);
//    }
//}
