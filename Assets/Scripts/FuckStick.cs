﻿using UnityEngine;

public class FuckStick : MonoBehaviour
{
    public bool taken;
    public Sprite newSprite;

    private void FixedUpdate()
    {
        if (taken)
        {
            if (transform.parent.GetComponent<SpriteRenderer>().flipX)
            {
                GetComponent<SpriteRenderer>().flipX = true;
                transform.localPosition = new Vector2(-1.5f, 0);
            }
            else
            {
                GetComponent<SpriteRenderer>().flipX = false;
                transform.localPosition = new Vector2(1.5f, 0);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            taken = true;
            GetComponent<SpriteRenderer>().sprite = newSprite;
            transform.parent = collision.gameObject.transform;
            transform.localPosition = Vector2.zero;
        }
    }
}