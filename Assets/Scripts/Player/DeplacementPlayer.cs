using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DeplacementPlayer : MonoBehaviour
{
    private Vector3 position;
    [SerializeField] public float speed;
    Rigidbody rb;
    Vector3 Movement = new Vector3(0, 0, 0);
    public bool is_immobilized;
    NavMeshAgent agent;
    Vector3 movement;

    //the animator :
    [SerializeField] private Animator animator;

    //this variable will contain the actual speed of the nav agent :
    private float velocity;

    Camera camera;
    [SerializeField] float distanceCamera;
    [SerializeField] float heightCamera;


    private void Start()
    {
        rb = gameObject.GetComponentInChildren<Rigidbody>();
        is_immobilized=false;
        agent = GetComponent<NavMeshAgent>();
        agent.enabled = true;
        camera = GameObject.FindObjectOfType<Camera>();
        print(camera);
    }

    void Update()
    {
        FollowPlayer();
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
        Quaternion Rotation = Quaternion.LookRotation(new Vector3(position.x, transform.position.y, position.z) - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, Rotation, 0.3f);
    }

    private void Move()
    {
        animator.SetFloat("speed",agent.velocity.magnitude/agent.speed);
        movement = new Vector3(Input.GetAxis("Horizontal"),0, Input.GetAxis("Vertical"));
        Vector3 moveDestination = transform.position + movement;
        agent.destination = moveDestination;
    }
    
    public void ChangeState()
    {
        is_immobilized=!is_immobilized;
    }

    void FollowPlayer()
    {
        camera.transform.position = new Vector3(transform.position.x, transform.position.y + heightCamera, transform.position.z - distanceCamera);
        camera.transform.LookAt(transform);
    }

}
