using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDrowStartScene : MonoBehaviour
{
    private Vector3 _size = new Vector3(70/3f, 100/3f, 1f);

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawCube(transform.position, _size);
    }
}
