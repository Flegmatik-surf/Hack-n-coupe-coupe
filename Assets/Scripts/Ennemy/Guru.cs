using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Guru : Ennemy
{
    private float rangeBuff;
    bool m_Started;
    public LayerMask m_LayerMask;

    //buffSpeed
    //MeshFilter spellCircle;
    MeshRenderer spellCircle;
    private float buffSpeed = 3f; //de combiens augmente la vitesse
    private float buffSpeedTime = 5f; //combiens de temps dure le buff
    private float SpeedSpellTimer = 10f; //combiens de temps le spell est actif
    private float SpeedSpellPause = 5f; // combiens de temps entre chaque spell
    private float timer = 0f;
    [SerializeField] private bool SpeedSpell = false;

    //magicspell ball
    [SerializeField] GameObject magicSpell;
    private MagicSpellFactory factory;



    public override void Attack()
    {
        print("attack");
        //animation d'attaque
        if (Time.time > timeStamp + cooldown)
        {
            var inst = factory.GetNewInstance();
            inst.transform.position = transform.forward + transform.position;
            //Vector3 vector = new Vector3(transform.forward.z, 0, transform.forward.x).normalized;
            //inst.transform.localEulerAngles = 90 * vector;
            timeStamp = Time.time;
        }
        
    }


    void Awake()
    {
        factory = magicSpell.GetComponent<MagicSpellFactory>();
        m_Started = true;
        timer = 0f; 
        //spellCircle = GetComponent(typeof(MeshFilter)) as MeshFilter;
        spellCircle = GameObject.Find("Plane").GetComponentInChildren<MeshRenderer>();
        spellCircle.enabled = false;
    }


    void FixedUpdate()
    {
        spellCircle.enabled = SpeedSpell;
        timer += Time.deltaTime;
        if (SpeedSpell)
        {
            if (timer >= SpeedSpellTimer)
            {
                SpeedSpell = false;
                timer = 0f;
            }
            else
            {
                MyCollisions();
            }
        }

        else
        {
            if (timer >= SpeedSpellPause)
            {
                SpeedSpell = true;
                timer = 0f;
            }
        }
    }

    void MyCollisions()
    {
        
        Collider[] hitColliders = Physics.OverlapSphere(gameObject.transform.position, 10f);
        int i = 0;
        while (i < hitColliders.Length)
        {
            
            if (hitColliders[i].tag == "Ennemy")
            {
                Ennemy ennemy = hitColliders[i].GetComponent<Ennemy>();
                ennemy.GetBuffSpeed(buffSpeed, buffSpeedTime);
            }
            
            
            i++;
        }


    }
    /*
   IEnumerator SpeedTimer(int time, bool boolean)
    {
        int counter = time;
        while (counter > 0)
        {
            print(counter);
            yield return new WaitForSeconds(1);
            counter--;
        }
        SpeedSpell = boolean;
        print(SpeedSpell);
    }
    
    void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(gameObject.transform.position, 5);
    }
    */
}
