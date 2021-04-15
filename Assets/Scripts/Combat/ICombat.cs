using UnityEngine;

public interface ICombat {
    // Methods
    void Init(ICharacter character);
    void Update();
    void Terminate();
    Vector3 GetAimDirection();
}
