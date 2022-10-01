using UnityEngine;
using System.Collections;

public class InFogInvisible : MonoBehaviour
{
    private void FixedUpdate()
    {
        Raycast();
    }

    private void Raycast()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, transform.up, 0.1f);
        foreach (var item in hits)
        {
            if (item.collider != null)
            {
                if (item.collider.CompareTag("Fog"))
                {
                    if (item.collider.GetComponent<SpriteRenderer>().color.a < 0.3f)
                        GetComponent<SpriteRenderer>().color = Utility.enabledColor;
                    else
                        GetComponent<SpriteRenderer>().color = Utility.zeroAlpha;
                }
                if (item.collider.CompareTag("Visibility"))
                    GetComponent<SpriteRenderer>().color = Utility.enabledColor;
            }
        }
    }
}
