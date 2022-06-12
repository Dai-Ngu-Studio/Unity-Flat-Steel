using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 10;
    public float maxDistance = 10;

    private Vector2 startPosition;
    private float conquaredDistance = 0;
    private Rigidbody2D rb2d;
}
