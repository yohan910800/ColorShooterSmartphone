using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleNPCMovement: MonoBehaviour,
IMovement {

 int verticalHorizontalSwitch = 3; // 0 for Vertical movement 1 for Horizontal Movement
float movedistance = 0;
float waitTime = 0;
    float speed = 1.0f;
    Vector3 direction;
    Vector3 directionAimPoint;
    float aimPointOffset = 1;
    
    float directionTimer = 0;
    float waitTimer = 0;

    public bool isInitialized = false;

    ICharacter character;
    IInputModule input;
    public void Init(ICharacter character) {
        this.character = character;
        input = character.GetInputModule();
    }
    public void Start() {
        direction = new Vector3(0, 0, 0);
    }
    public void Update()
    {

       
            if (verticalHorizontalSwitch != 3)
            {
                directionAimPoint = transform.position;
                if (directionTimer < movedistance)
                {
                    directionTimer += Time.deltaTime;
                    gameObject.transform.position += new Vector3(direction.x, direction.y, 0f).normalized * speed * Time.deltaTime;
                }
                else
                {
                    waitTimer += Time.deltaTime;

                    if (waitTimer > waitTime)
                    {
                        directionTimer = 0;
                        waitTimer = 0;
                    }

                }
                UpdateDirection();
            }
  
    }
    void OnInputModuleChange() {
        input = character.GetInputModule();
    }
    void UpdateDirection() {
        if (directionTimer == 0) 
            aimPointOffset *= -1;
        if (verticalHorizontalSwitch == 0) 
            directionAimPoint.y -= aimPointOffset;
        if (verticalHorizontalSwitch == 1) 
            directionAimPoint.x -= aimPointOffset;
        
        if (waitTimer != 0) 
            direction = Vector2.zero;
        else 
            direction = transform.position - directionAimPoint;

        }
    
    public Vector3 getDirection() {
        return direction;
    }

    public void Terminate() {
        character.OnInputModuleChange -= OnInputModuleChange;
    }

    public void SetMovement(int directionSwitch, int Distance, int wait  ){

        verticalHorizontalSwitch = directionSwitch;
        movedistance = Distance;
        waitTime = wait;

       
    }
}
