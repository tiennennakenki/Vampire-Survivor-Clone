using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeaponBehaviour : SaiMonoBehaviour
{
    [SerializeField] protected Vector3 direction;
    [SerializeField] protected float destroyAfterSeconds = 3;

    protected override void Start()
    {
        base.Start();
        Destroy(gameObject, this.destroyAfterSeconds);
    }

    public virtual void DirectionChecker(Vector3 dir)
    {
        this.direction = dir;

        float dirX = this.direction.x;
        float dirY = this.direction.y;

        Vector3 scale = transform.localScale;
        Vector3 rotation = transform.rotation.eulerAngles;

        if(dirX < 0 && dirY == 0) //left
        {
            scale.x = scale.x * -1;
            scale.y = scale.y * -1;
        }
        else if(dirX == 0 && dirY < 0) //down
        {
            scale.y = scale.y * -1; 
        }
        else if (dirX == 0 && dirY > 0) //up
        {
            scale.x = scale.x * -1;
        }
        else if (dirX > 0 && dirY > 0) //right up
        {
            rotation.z = 0f;
        }
        else if (dirX > 0 && dirY < 0) //right down
        {
            rotation.z = -90f;
        }
        else if (dirX < 0 && dirY > 0) //left up
        {
            scale.x = scale.x * -1;
            scale.y = scale.y * -1;
            rotation.z = -90f;
        }
        else if (dirX < 0 && dirY < 0) //left down
        {
            scale.x = scale.x * -1;
            scale.y = scale.y * -1;
            rotation.z = 0f;
        }

        transform.localScale = scale;
        transform.rotation = Quaternion.Euler(rotation);
    }
}
