using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : Weapon, IBeautify
{
    public override void Fly(Vector3 target, float flySpeed)
    {
        base.Fly(target, flySpeed);
        Beautify(target);
    }

    public void Beautify(Vector3 target)
    {
        Vector3 dir = target - this.transform.position;
        dir.y = 0;
        float attackAngle = Vector3.Angle(dir, new Vector3(1, 0, 0));
        if (dir.z > 0)
        {
            attackAngle = -attackAngle;
        }
        this.transform.rotation = Quaternion.Euler(new Vector3(-90, 0, attackAngle + 90));
    }
}
