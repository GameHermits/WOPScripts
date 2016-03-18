using UnityEngine;
using System.Collections;

public class OnHitAnimation_TrainingTargeetCollider : MonoBehaviour {

    public Animation anim_GameObjectAnimationRef;
    
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Projectile")
            anim_GameObjectAnimationRef.Play("Training Targets Animation");
    }
}
