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

    
    NavMeshAgent bossAgent;
    public Transform playerTransform;
    public Transform shootPivot;
    public float attackRange = 50f;
    public float attackDuration = 3f;
    public float beamOffset = 1f;
    public int laserDamage = 100;

    public Animator bossAnim;

    public GameObject beam;
    public GameObject impactEffect;
    public GameObject emmisionEffect;

    public bool canShootLaser = false;

    PlayerStats playerStats;



    private void Awake()
    {
        
        bossAgent = GetComponentInParent<NavMeshAgent>();
        bossAnim = GetComponent<Animator>();
        playerStats = GameObject.FindGameObjectWithTag("Jugador").GetComponent<PlayerStats>();
    }

    private void Start()
    {
        currentStateTime = stateDurationTime;
        emmisionEffect.SetActive(false);
        
    }
    public override State RunCurrentState()
    {
        stateTimeFinished = false;

        IndividualStateLogic();

        if (stateTimeFinished == false)
        {
            bossAnim.SetBool("IsIdle", false);
            bossAnim.SetBool("IsLaser", true);
            bossAnim.SetBool("IsMoving", false);
            
            currentStateTime -= Time.deltaTime;
        }

        Debug.Log("Laser Attack");
        Debug.Log(currentStateTime);


        if (currentStateTime <= 0)
        {

            emmisionEffect.SetActive(false);

            bossAnim.SetBool("IsIdle", true);
            bossAnim.SetBool("IsLaser", false);
            bossAnim.SetBool("IsMoving", false);
            bossAnim.SetBool("IsLaserPerformed", false);

            canShootLaser = false;
            

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
        if (canShootLaser)
        {
            ShootAttack();
            emmisionEffect.SetActive(true);
        }
        
        
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

            //PlayerStats playerStats = hit.collider.GetComponent<PlayerStats>();
            if (playerStats != null)
            {
                playerStats.TakeDamage(laserDamage);

                if (hit.collider.gameObject.name == "BlockCollider")
                {
                    Debug.Log("hitttt");
                    playerStats.TakeDamage(laserDamage);
                }
            }

            


            beam.transform.localScale = new Vector3(.5f, .5f, distanceToHit - beamOffset);

            if (beam.transform.localScale == new Vector3(.5f, .5f, distanceToHit - beamOffset))
            {
                impactEffect.SetActive(true);
                impactEffect.transform.position = hit.point;
                impactEffect.transform.LookAt(shootPivot.position);
                
            }

            
        }
        else
        {
            
            beam.transform.localScale = new Vector3(1, 1, attackRange);
        }

        if (currentStateTime <= .25f)
        {
            
            beam.transform.localScale = new Vector3(1, 1, 0);
            impactEffect.SetActive(false);
            //emmisionEffect.SetActive(false);
        }

        //StartCoroutine(ShootLaser());
    }


    void ChangeToLaserPerformed()
    {
        canShootLaser = true;
        bossAnim.SetBool("IsLaserPerformed", true);
        
    }

    /*IEnumerator ShootLaser()
    {
        
        yield return new WaitForSeconds(attackDuration);
        
    }*/
}
