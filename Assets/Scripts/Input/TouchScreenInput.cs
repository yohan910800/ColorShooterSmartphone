using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MankindGames;

/* brief discription

    gets two touches; touch0, touch1.

    direction will be determined by: 
    initial touch location , current touch location
    of touch0.

    colorcheck is done by:
    initial touch location , current touch location
    of touch1.
  
    also contains getEnterposition Method for later use in UI,
    which returns just that.

 
     */

public class TouchScreenInput : IInputModule {

    // Events
    public event Action<int> OnColorSwitch;
    public event Action<ICharacter> OnAimLock;
	public event Action<GameObject> OnTap;

    //Variables

    Vector2 direction;

    Vector2 touch0EnterPosition;
    Vector2 touch1EnterPosition;

    int loopInhibiter;

    float edgeCheckP0;
    int edgeThreshold = 100;
    float swipeTriggerDistance = 30f;
    bool edgeTouch;

    // Methods
    public void Init() {

    }

    public void Update() {
		if(Static.GamePaused) return;
        UpdateDirection();
        ColorSwitchCheck();
		TapCheck();
    }
    
    /* For later use in UI-----------------------------
     
        Might be a better idea to return 
        an array of vector2
        which contains touch(number) enter,current position
           
    */
    public Vector2 GetTouch0EnterPosition() {

        return touch0EnterPosition;
    }

    public Vector2 GetTouch1EnterPosition() {
        return touch1EnterPosition;
    }
    //-------------------------------------------------

    public Vector2 GetDirection() {
        return direction;
    }

    void UpdateDirection() {

        if (Input.touchCount > 0) {
            Touch touch0 = Input.GetTouch(0);

            switch (touch0.phase) {

                case TouchPhase.Began:
                    touch0EnterPosition = touch0.position;
                    break;

                case TouchPhase.Moved:

                    float minimumDistance = 10.0f;
                    float touchDistance = (touch0EnterPosition - touch0.position).magnitude;

                    if (touchDistance > minimumDistance) {
                        direction = (touch0.position - touch0EnterPosition).normalized;
                    }
                    break;

                case TouchPhase.Ended:
                    direction = Vector2.zero;
                    break;

            }
        }
    }

    void ColorSwitchCheck() {

        if (Input.touchCount > 1) {
            Touch touch1 = Input.GetTouch(1);

            switch (touch1.phase) {

                case TouchPhase.Began:
                    touch1EnterPosition = touch1.position;
                    loopInhibiter = 0;
                    break;

                case TouchPhase.Moved:

                    float minimumDistance = 10.0f;
                    float touchDistance = (touch1EnterPosition.x - touch1.position.x);

                    if (loopInhibiter == 0 && touchDistance < -minimumDistance && OnColorSwitch != null) {
                        Debug.Log("1");
                        loopInhibiter = 1;
                        if(OnColorSwitch!=null) OnColorSwitch(1);
                    }
                    if (loopInhibiter == 0 && touchDistance > minimumDistance && OnColorSwitch != null) {

                        loopInhibiter = 1;
                        if(OnColorSwitch!=null) OnColorSwitch(-1);
                    }

                    break;

                case TouchPhase.Ended:
                    loopInhibiter = 0;
                    break;

            }

        }

        EdgeSwipeCheck();
    }

    void EdgeSwipeCheck(){
        if(Input.touchCount>0){
            Touch touch = Input.GetTouch(0);
            switch (touch.phase) {
                case TouchPhase.Began:
                    if(touch.position.x < edgeThreshold || touch.position.x > Screen.width-edgeThreshold){
                        edgeTouch = true;
                        edgeCheckP0 = touch.position.x;
                    }
                    break;
                case TouchPhase.Moved:
                    if(edgeTouch && Mathf.Abs(edgeCheckP0-touch.position.x) >= swipeTriggerDistance){
                        edgeTouch = false;
                        int dir = edgeCheckP0 > Screen.width/2? -1 : 1;
                        if(OnColorSwitch!=null) OnColorSwitch(dir);
                    }
                    break;
                case TouchPhase.Ended:
                    edgeTouch = false;
                    break;
            }
        }
        return;
    }

    // Checks for selections and triggers respective events
	void TapCheck(){
		if (Input.touchCount > 0) {
            Touch touch0 = Input.GetTouch(0);
            if(touch0.phase != TouchPhase.Began) return;
            RaycastHit2D rayHit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(touch0.position));
            if(rayHit.collider != null){
				// Trigger OnTap
				GameObject gObj = rayHit.collider.gameObject;
				if(OnTap!=null) OnTap(gObj);
				// Trigger OnAimLock
				ICharacter character = gObj.GetComponent<ICharacter>();
				if(character != null && OnAimLock != null){
					OnAimLock(character);
				}
			}
        }
	}
}
