using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeController : CustomBehaviour
{
    public bool IsReadyToSlice;

    public KnifeMovementBehaviour _knifeMovementBehaviour;
    public KnifeHandleCollisionBehaviour _knifeCollisionBehaviour;
    public KnifeTipCollisionBehaviour _knifeTipCollisionBehaviour;


    public override void Initialize(GameManager gameManager)
    {
        base.Initialize(gameManager);
        _knifeMovementBehaviour.Initialize(this);
        _knifeCollisionBehaviour.Initialize(this);
        _knifeTipCollisionBehaviour.Initialize(this);
    }


}
