using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class LaserAttack : State
{

    public State NextStateToThis;
    public State PreviousStateToThis;

    public bool stateTimeFinished = false;

    public float stateDurationTime = 4f;
    public float currentStateTime;

    public MeshRenderer bossRenderer;
    NavMeshAgent bossAgent;
    public Transform playerTransform;
    public Transform shootPivot;
    public float attackRange = 50f;
    public float attackDuration = 3f;

    public GameObject beam;

    



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

        Debug.Log("Laser Attack");
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
        bossRenderer.material.color = Color.green;
        ShootAttack();
        
    }

    void ShootAttack()
    {
        bossAgent.updateRotation = true;

        Quaternion targetRot = Quaternion.LookRotation(playerTransform.position - bossAgent.transform.position);
        Quaternion fromRot = bossAgent.transform.localRotation;



        bossAgent.transform.localRotation = Quaternion.Lerp(fromRot, targetRot, .025f);

        LimitRotation();
        BloodLaserAttack();
    }

    void LimitRotation()
    {
        Vector3 BossEulerAngles = bossAgent.transform.rotation.eulerAngles;

        BossEulerAngles.x = 0f;
        BossEulerAngles.z = 0f;

        bossAgent.transform.rotation = Quaternion.Euler(BossEulerAngles);
    }

    void BloodLaserAttack()
    {


        RaycastHit hit;
        if (Physics.Raycast(shootPivot.position, shootPivot.forward, out hit, attackRange))
        {
            

            float distanceToHit = Vector3.Distance(shootPivot.position, hit.point);
            
            

            beam.transform.localScale = new Vector3(1, 1, distanceToHit);

            
        }
        else
        {
            beam.transform.localScale = new Vector3(1, 1, attackRange);
        }

        if (currentStateTime <= 1f)
        {
            beam.transform.localScale = new Vector3(1, 1, 0);
        }

        //StartCoroutine(ShootLaser());
    }

    /*IEnumerator ShootLaser()
    {
        
        yield return new WaitForSeconds(attackDuration);
        
    }*/
}
