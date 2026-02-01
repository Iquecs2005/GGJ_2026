using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private PlayerController pc;

    [SerializeField] private Animator anim;

    public void UpdateAnimation() 
    {
        Vector2 faceDir = pc.facingDirection;

        anim.SetInteger("FacingHorizontaly", (int)faceDir.x);
        anim.SetInteger("FacingVerticaly", (int)faceDir.y);
    }
}
