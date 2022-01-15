using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Passage : MonoBehaviour
{
    public Transform connection;
    private void OnTriggerEnter2D(Collider2D collider)
    {
        Vector3 position = collider.transform.position;
        position.x = this.connection.position.x;
        position.y = this.connection.position.y;

        collider.transform.position = position;
    }
}
