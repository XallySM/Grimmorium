using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IdleAttack : State
{

    public State NextStateToThis;
    public State PreviousStateToThis;

    public bool stateTimeFinished = false;

    public float stateDurationTime = 10f;
    public float currentStateTime;

    public MeshRenderer bossRenderer;
    NavMeshAgent bossAgent;
    public Transform playerTransform;

    public Transform[] ShootPivots;
    public GameObject[] burst;

    [SerializeField] float fireRate = 1f;
    private float fireTimer = 0.0f;

    int burstNumber = 5;

    
    

    private void Awake()
    {
        bossRenderer = GetComponentInParent<MeshRenderer>();
        bossAgent = GetComponentInParent<NavMeshAgent>();
    }
    private void Start()
    {
        currentStateTime = stateDurationTime;
       


    }
    public override State RunCurrentState()
    {
        stateTimeFinished = false;

        IndividualStateLogic();

        if (stateTimeFinished == false)
        {
            currentStateTime -= Time.deltaTime;
        }

        Debug.Log("Idle Attack");
        Debug.Log(currentStateTime);

        if (currentStateTime <= 0)
        {
            
            stateTimeFinished = true;
        }


        if (stateTimeFinished == true)
        {
            currentStateTime = stateDurationTime;
            return NextStateToThis;
        }

        else
        {
            return this;
        }
    }

    public override void IndividualStateLogic()
    {
        bossRenderer.material.color = Color.red;
        ShootAttack();
        
    }

    void ShootAttack()
    {

        bossAgent.updateRotation = true;

        Quaternion targetRot = Quaternion.LookRotation(playerTransform.position - bossAgent.transform.position);

        bossAgent.transform.localRotation = targetRot;

        LimitRotation();

        if (fireTimer < fireRate + 1.0f)
        {
            fireTimer += Time.deltaTime;
        }

        if (fireTimer > fireRate)
        {
            StartCoroutine(RadialShootBloodyTears());
            fireTimer = 0.0f;
        }

    }

    

    void LimitRotation()
    {
        Vector3 BossEulerAngles = bossAgent.transform.rotation.eulerAngles;

        BossEulerAngles.x = 0f;
        BossEulerAngles.z = 0f;

        bossAgent.transform.rotation = Quaternion.Euler(BossEulerAngles);
    }

   

    IEnumerator RadialShootBloodyTears()
    {
        
        
        if(burst != null)
        {
            for (int i = 0; i < burstNumber; i++)
            {
                burst[i] = ObjectPool.instance.GetPooledObject();

                Projectile projectile = burst[i].GetComponent<Projectile>();
                    projectile.fireOrigin = ShootPivots[i];
                    projectile.fireDirection = projectile.fireOrigin.forward;
                    burst[i].transform.position = ShootPivots[i].position;
                    burst[i].SetActive(true);
                
                
            }
        }
        

        /*if (bullet != null)
        {
            Projectile projectile = bullet.GetComponent<Projectile>();
            projectile.fireOrigin = ShootPivot;
            projectile.fireDirection = projectile.fireOrigin.forward;
            bullet.transform.position = ShootPivot.position;
            bullet.SetActive(true);


        }*/

        //yield return new WaitForSeconds(fireRate);
        yield return null;


    }
}
