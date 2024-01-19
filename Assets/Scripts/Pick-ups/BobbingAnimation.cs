using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobbingAnimation : SaiMonoBehaviour
{
    [SerializeField] protected float frequency = 3f; //Speed of movement
    [SerializeField] protected float magnitude = 0.1f; //Range of movement
    [SerializeField] protected Vector3  direction = new Vector3(0,1,0); //Direction of movement
    [SerializeField] protected Vector3 initalPosition;
    [SerializeField] protected PickUp pickUp;

    protected override void Start()
    {
        base.Start();
        this.pickUp = GetComponent<PickUp>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        this.initalPosition = transform.position;
    }

    protected override void Update()
    {
        if(this.pickUp && !this.pickUp.hasBeenCollected)
        {
            base.Update();
            transform.position = this.initalPosition + this.direction * Mathf.Sin(Time.time * frequency) * this.magnitude;
        }
    }
}
