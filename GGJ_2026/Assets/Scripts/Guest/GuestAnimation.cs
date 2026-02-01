using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuestAnimation : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GuestController gc;
    [SerializeField] private Animator anim;

    public void UpdateFacingDir(Vector2Int newFacingDir) 
    {
        if (newFacingDir == Vector2Int.zero) return;

        anim.SetInteger("FacingHorizontaly", newFacingDir.x);
        anim.SetInteger("FacingVerticaly", newFacingDir.y);
    }
}
