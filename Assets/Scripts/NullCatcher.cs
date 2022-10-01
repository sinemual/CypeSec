using UnityEngine;
using System.Collections;

public class NullCatcher : MonoBehaviour
{
    public void AStarCatcher()
    {
        CoroutineActions.ExecuteAction(1.0f, () =>
        {
            //GetComponent<Cim>()?.SetState(Cim.State.Going, Cim.GoTo.IdlePlace);
            //GetComponent<Stranger>()?.SetState(Stranger.State.Leaving);
        });
        Debug.LogWarning("AStarCatcher!");
    }
}
