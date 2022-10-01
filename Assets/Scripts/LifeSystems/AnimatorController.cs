using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    // MOVED IN STATES !!!

    //private Animator animator;

    //public void SetState(Cim.State _state)
    //{
    //    if (animator == null)
    //        animator = GetComponent<Animator>();

    //    animator.SetBool("IsWalking", false);
    //    animator.SetBool("IsMining", false);
    //    animator.SetBool("IsFighting", false);

    //    if (_state == Cim.State.Going || _state == Cim.State.LookingFor || _state == Cim.State.GoToStorage || _state == Cim.State.Alarm || _state == Cim.State.GoToIdle || _state == Cim.State.GoToBuilding)
    //        animator.SetBool("IsWalking", true);
    //    else if (_state == Cim.State.Minig || _state == Cim.State.Building)
    //        animator.SetBool("IsMining", true);
    //    else if (_state == Cim.State.Fighting)
    //        animator.SetBool("IsFighting", true);
    //    else if (_state == Cim.State.Die)
    //        animator.SetBool("IsDie", true);
    //}

    //public void SetState(Stranger.State _state)
    //{
    //    if (animator == null)
    //        animator = GetComponent<Animator>();

    //    animator.SetBool("IsWalking", false);
    //    animator.SetBool("IsFighting", false);

    //    if (_state == Stranger.State.LookingFor || _state == Stranger.State.Leaving || _state == Stranger.State.Going)
    //        animator.SetBool("IsWalking", true);
    //    else if (_state == Stranger.State.Fighting)
    //        animator.SetBool("IsFighting", true);
    //    else if (_state == Stranger.State.Die)
    //        animator.SetBool("IsDie", true);
    //}
}
