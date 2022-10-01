using System.Collections.Generic;
using UnityEngine;

public class Vision : MonoBehaviour
{
    public float collisionDistance = 1.0f;
    public float sideCollisionDistance = 0.2f;
    public float offset = 0.1f;
    public float sideOffset = 0.1f;
    public float viewingAngle = 0.3f;
    public bool isDispelFog;

    private Unit unit;

    private void Start()
    {
        unit = GetComponent<Unit>();
    }

    private void FixedUpdate()
    {
        Raycatings();
    }

    private void VisionHandler(RaycastHit2D _hit)
    {
        GameObject hitGO = _hit.collider.gameObject;
        if (hitGO != null && hitGO != gameObject)
        {
            if (!unit.US.Memory.objectsInMemory.Contains(hitGO) && !hitGO.CompareTag("Fog"))
                unit.US.Memory.AddObject(hitGO);
            //if (unit.SM.CurrentState == unit.SM.states.Values.(typeof(LookingForState)) && !lookingFor.isFound)
            //{
            //    if (hitGO.GetComponent<TileData>() != null)
            //    {
            //        if (hitGO.GetComponent<TileData>().isResource)
            //            if (lookingFor.CurrentLookingForResource == hitGO.GetComponentInChildren<Resource>().currrentResType)
            //            {
            //                moving.isGoing = false;
            //                lookingFor.Found(hitGO.GetComponent<TileData>());
            //            }
            //        if (hitGO.GetComponent<TileData>() == memory.destinationPoint)
            //            moving.completeCallback?.Invoke();
            //    }
            //    if (hitGO == lookingFor.CurrentLookingForGO)
            //    {
            //        moving.isGoing = false;
            //        lookingFor.Found(hitGO);
            //    }
            //}

            //if (hitGO.CompareTag("Cim") && !hitGO.GetComponent<Stats>().isDead)
            //{
            //    GetComponent<Cim>()?.Communicate(hitGO.GetComponent<Cim>());
            //    if (GetComponent<Stranger>() != null && GetComponent<Stranger>().fightingWith == null)
            //        GetComponent<Stranger>()?.AttackTarget(hitGO);
            //}
            //if (hitGO.CompareTag("Stranger") && !hitGO.GetComponent<Stats>().isDead)
            //{
            //    GetComponent<Stranger>()?.Communicate(hitGO.GetComponent<Stranger>());
            //    if (GetComponent<Cim>() != null && GetComponent<Cim>().fightingWith == null)
            //        GetComponent<Cim>()?.AttackTarget(hitGO);
            //}
        }
    }

    private void VisionFog(RaycastHit2D _hit)
    {
        GameObject hitGO = _hit.collider.gameObject;
        SpriteRenderer currentSR;

        if (hitGO.CompareTag("Fog"))
        {
            currentSR = hitGO.GetComponent<SpriteRenderer>();
            if (currentSR != null)
                currentSR.color = Utility.zeroAlpha;
        }
    }

    private void Raycatings()
    {
        Raycating((transform.up + Vector3.one * viewingAngle).normalized, sideOffset, offset, true);
        Raycating(transform.up, sideOffset, offset);
        Raycating((transform.up + (new Vector3(1.0f, -1.0f) * -viewingAngle)).normalized, sideOffset, offset, true);
        Raycating(transform.right, sideOffset, offset);
        Raycating(-transform.right, sideOffset, offset);
        //back
        Raycating((-transform.up + (new Vector3(1.0f, -1.0f) * viewingAngle)).normalized, sideOffset, offset, true, true);
        Raycating(-transform.up, sideOffset, offset, isBack: true);
        Raycating((-transform.up + (new Vector3(1.0f, 1.0f) * -viewingAngle)).normalized, sideOffset, offset, true, true);
    }

    private void OnDrawGizmosSelected()
    {
        DrawRay((transform.up + Vector3.one * viewingAngle).normalized, sideOffset, offset, true);
        DrawRay(transform.up, sideOffset, offset);
        DrawRay((transform.up + (new Vector3(1.0f, -1.0f) * -viewingAngle)), sideOffset, offset, true);
        DrawRay(transform.right, sideOffset, offset);
        DrawRay(-transform.right, sideOffset, offset);
        //back
        DrawRay((-transform.up + (new Vector3(1.0f, -1.0f) * viewingAngle)).normalized, sideOffset, offset, true, true);
        DrawRay(-transform.up, sideOffset, offset, isBack: true);
        DrawRay((-transform.up + (new Vector3(1.0f, 1.0f) * -viewingAngle)).normalized, sideOffset, offset, true, true);
    }

    private void Raycating(Vector3 _dir, float _side = 0f, float _offset = 0f, bool isSide = false, bool isBack = false)
    {
        Vector2 targetDir = _dir;
        float distance = isSide ? sideCollisionDistance : collisionDistance;
        //distance = isBack ? distance / 2.0f : distance;
        RaycastHit2D[] hitsO = Physics2D.RaycastAll(transform.position + transform.up * _offset + transform.right * _side, targetDir, distance);
        if (hitsO != null)
        {
            foreach (var item in hitsO)
            {
                if (isDispelFog) VisionFog(item);
                VisionHandler(item);
                //return; //?
            }
        }
    }

    private void DrawRay(Vector3 _dir, float _side, float _offset, bool isSide = false, bool isBack = false)
    {
        Vector2 targetDir = _dir;
        float distance = isSide ? sideCollisionDistance : collisionDistance;
        //distance = isBack ? distance / 2.0f : distance;
        Ray2D ray = new Ray2D(transform.position + transform.up * _offset + transform.right * _side, targetDir);
        RaycastHit2D[] hitsO = Physics2D.RaycastAll(transform.position + transform.up * _offset + transform.right * _side, targetDir, distance);

        if (hitsO != null)
        {
            foreach (var item in hitsO)
            {
                GameObject gameO = item.collider.gameObject;
                if (gameO != null && gameO != gameObject)
                {
                    Gizmos.color = Color.red;
                    Gizmos.DrawLine(ray.origin, ray.origin + targetDir.normalized * (distance - _offset));
                    //Gizmos.DrawSphere(item.point, 0.1f);
                    return;
                }
            }
        }

        Gizmos.color = Color.green;
        Gizmos.DrawLine(ray.origin, ray.origin + targetDir.normalized * (distance - _offset));
    }
}