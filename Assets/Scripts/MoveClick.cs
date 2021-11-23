using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveClick : MonoBehaviour
{
    private Vector3 position;
    [SerializeField] private float speed;

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            GetPosition();
        }
        Move();
    }

    void GetPosition()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            position = hit.point;

        }
    }

    private void Move()
    {
        if (Vector3.Distance(transform.position, position) > 0f)
        {
            Quaternion Rotation = Quaternion.LookRotation(position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, Rotation, 0.3f);
            transform.position = Vector3.MoveTowards(transform.position, position, speed * Time.deltaTime);

        }
    }
}
