using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float speed = 3f;
    [SerializeField] float spinSpeed = -5f;

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime, Space.World);
        transform.Rotate(0, 0, spinSpeed); ;
    }

    private void OnTriggerEnter2D(Collider2D othercollider)
    {
        Destroy(gameObject);
    }

}
