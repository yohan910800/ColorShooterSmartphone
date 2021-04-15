using System;
using System.Collections.Generic;
using UnityEngine;

public interface IInputModule {
	// Events
    event Action<int> OnColorSwitch;
	event Action<ICharacter> OnAimLock;
	event Action<GameObject> OnTap;

	// Methods
    Vector2 GetDirection();
	void Init();
	void Update();
}