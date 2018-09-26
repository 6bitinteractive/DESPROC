using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupScore : Pickup
{
    protected override IEnumerator BuffTimer(Collider2D collision)
    {
        Score score = collision.gameObject.GetComponent<Score>();
        score.EarnScore(BuffValue);
        return base.BuffTimer(collision);
    }

    protected override void EndBuffTimer()
    {
        base.EndBuffTimer();
    }
}
