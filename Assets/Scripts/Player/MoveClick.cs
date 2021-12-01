using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveClick : MonoBehaviour
{
    private Vector3 position;
    [SerializeField] private float speed;
    Rigidbody rb;
    Vector3 Movement = new Vector3(0, 0, 0);

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        LookAt();
        
        Move();
    }

    void LookAt()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            position = hit.point;

        }

        Quaternion Rotation = Quaternion.LookRotation(position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, Rotation, 0.3f);
    }

    private void Move()
    {
        
        if (Input.GetKey("q"))
        {
            Movement = new Vector3(-1, 0, 0);
        }
        

        this.transform.position += Movement * speed * Time.deltaTime;

    }
}
