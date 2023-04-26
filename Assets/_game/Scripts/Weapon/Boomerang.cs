using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : Weapon, IRotateWeapon
{
    public override void Fly(Vector3 target, float flySpeed)
    {
        base.Fly(target, flySpeed);
        StartCoroutine(Rotate());
        
    }

    public override IEnumerator FlyStraight(Vector3 target, float flySpeed)
    {
        Vector3 returnPos = this.owner.transform.position; 
        while (Vector3.Distance(this.transform.position, target) > 0.1f && this.gameObject.activeSelf && !this.isStuckAtObstacle)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, target, flySpeed * Time.deltaTime);
            yield return null;
        } 
        while (Vector3.Distance(this.transform.position, returnPos) > 0.1f && this.gameObject.activeSelf && !this.isStuckAtObstacle)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, returnPos, flySpeed * Time.deltaTime);
            yield return null;
        }
        if (!isStuckAtObstacle)
        {
            this.weaponPool.ReturnToPool(this.gameObject);
        }
        yield return null;
    }

    public IEnumerator Rotate()
    {
        float rotateSpeed = this.weaponData.rotateSpeed;
        while (!isStuckAtObstacle)
        {
            this.transform.Rotate(0, 0, -rotateSpeed * Time.deltaTime);
            yield return null;
        }
    }

}
