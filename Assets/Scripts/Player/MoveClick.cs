using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveClick : MonoBehaviour
{
    private Vector3 position;
    [SerializeField] public float speed;
    Rigidbody rb;
    Vector3 Movement = new Vector3(0, 0, 0);
    public bool is_immobilized;

    private void Start()
    {
        rb = gameObject.GetComponentInChildren<Rigidbody>();
        is_immobilized=false;
    }

    void Update()
    {
        LookAt();
        if(is_immobilized==false)
        {
            Move();
        }
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
            rb.velocity = new Vector3(-1 * speed * Time.deltaTime, 0,0);
        }
        if (Input.GetKey("d"))
        {
            rb.velocity = new Vector3(1 * speed * Time.deltaTime, 0, 0);
        }if (Input.GetKey("z"))
        {
            rb.velocity = new Vector3(0, 0, 1 * speed * Time.deltaTime);
        }if (Input.GetKey("s"))
        {
            rb.velocity = new Vector3(0, 0, -1 * speed * Time.deltaTime);
        }
    }

    public void ChangeState()
    {
        is_immobilized=!is_immobilized;
    }
}
