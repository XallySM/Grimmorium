using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovingAttack : State
{
    public State NextStateToThis;
    public State PreviousStateToThis;

    public bool stateTimeFinished = false;

    public float stateDurationTime = 25f;
    public float currentStateTime;

    

    NavMeshAgent bossAgent;
    public Transform[] waypoints;
    int waypointIndex;
    Vector3 waypointTarget;
    public Transform playerTransform;

    public Transform ShootPivot;

    [SerializeField] float fireRate = 1f;
    private float fireTimer = 0.0f;


    private void Awake()
    {
        
        bossAgent = GetComponentInParent<NavMeshAgent>();
    }

    private void Start()
    {
        currentStateTime = stateDurationTime;
        

    }
    public override State RunCurrentState()
    {
        stateTimeFinished = false;

        // here starts the individual state logic

        IndividualStateLogic();

        //here ends the individual state logic
        if( stateTimeFinished == false)
        {
            currentStateTime -= Time.deltaTime;
        }


        Debug.Log(currentStateTime);
        Debug.Log("Moving Attack");
        
        

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
        UpdateDestination();

        if (Vector3.Distance(bossAgent.transform.position, waypointTarget) < 1.2f)
        {
            IterateWaypointIndex();
            UpdateDestination();
        }

        if(currentStateTime <= .5)
        {
            bossAgent.SetDestination(bossAgent.transform.position);
        }

        ShootAttack();
        
    }

    void UpdateDestination()
    {
        waypointTarget = waypoints[waypointIndex].position;
        bossAgent.SetDestination(waypointTarget);
    }

    void IterateWaypointIndex()
    {
        waypointIndex++;
        if (waypointIndex == waypoints.Length)
        {
            waypointIndex = 0;
        }
    }

    void ShootAttack()
    {

        bossAgent.updateRotation = true;

        Quaternion targetRot = Quaternion.LookRotation(playerTransform.position - bossAgent.transform.position);
        Quaternion fromRot = bossAgent.transform.localRotation;
        


        bossAgent.transform.localRotation = Quaternion.Lerp(fromRot,targetRot,2f);

        LimitRotation();

        if (fireTimer < fireRate + 1.0f)
        {
            fireTimer += Time.deltaTime;
        }

        if (fireTimer > fireRate)
        {
            StartCoroutine(ShootBloodyTears());
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
    IEnumerator ShootBloodyTears()
    {


        GameObject bullet = ObjectPool.instance.GetPooledObject();

        if (bullet != null)
        {
            Projectile projectile = bullet.GetComponent<Projectile>();
            projectile.fireOrigin = ShootPivot;
            projectile.fireDirection = projectile.fireOrigin.forward;
            bullet.transform.position = ShootPivot.position;
            bullet.SetActive(true);


        }

        //yield return new WaitForSeconds(fireRate);
        yield return null;


    }

}
