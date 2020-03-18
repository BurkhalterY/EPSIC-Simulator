﻿using UnityEngine;

public class NPC : Entity
{
    private float timeToChangeDirection;
    private float horizontal;

    private void FixedUpdate()
    {
        timeToChangeDirection -= Time.fixedDeltaTime;
        if (timeToChangeDirection <= 0)
        {
            timeToChangeDirection = Random.Range(0.5f, 2);
            horizontal = Random.Range(-1, 2);
        }

        if (false)
        {
            Move(0, false, false);
        }
        else
        {
            Move(horizontal, false, false);
        }
    }
}
