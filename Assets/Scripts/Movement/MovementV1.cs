using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementV1 : MonoBehaviour, IMovement {
    
    // Private variables
    ICharacter character;
    IInputModule input;
    GameObject gObj;
    Stats stats;

    public void Init(ICharacter character){
        this.character = character;
        input = character.GetInputModule();
        gObj = character.GetGameObject();
        stats = character.GetStats();
        // Subscribing to the input change event so we can react to the change
        character.OnInputModuleChange += OnInputModuleChange;
    }

    public void Update(){
        Vector2 direction = input.GetDirection();
        gObj.transform.position += new Vector3(direction.x,direction.y, 0f).normalized * stats.Speed * Time.deltaTime;
    }

    // If there is a change of input we update our instance
    void OnInputModuleChange(){
        input = character.GetInputModule();
    }

    public void Terminate(){
        character.OnInputModuleChange -= OnInputModuleChange;
    }
}