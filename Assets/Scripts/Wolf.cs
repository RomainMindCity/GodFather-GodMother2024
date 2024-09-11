using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Wolf is a monster that will rush to the player at high speed and stop.
/// </summary>
public class Wolf : MonsterBehavior
{

    protected override void Init()
    {
        
    }

    protected override void OnPlayerEnter(Collider2D other)
    {
        // Jumps to the player
    }

    protected override void OnPlayerExit(Collider2D other)
    {

    }


    public override void FlashMonster(Vector3? playerPosition = null)
    {
        throw new System.NotImplementedException();
    }

    public override void UnflashMonster()
    {
        throw new System.NotImplementedException();
    }
    protected override void Player()
    {
        throw new System.NotImplementedException();
    }

    //------------------ AI STATES ------------------

    protected override void Flashed()
    {
        throw new System.NotImplementedException();
    }

    protected override void Walking()
    {
        throw new System.NotImplementedException();
    }

    protected override void None()
    {
        throw new System.NotImplementedException();
    }
    protected override void Chase()
    {
        throw new System.NotImplementedException();
    }


}
