using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Death : MonoBehaviour
{
    public Animator anim;
    Rigidbody[] rigidbodies;
    Collider[] colliders;
    public GameObject enemy;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rigidbodies = GetComponentsInChildren<Rigidbody>();
        colliders = GetComponentsInChildren<Collider>();
        SetCollidersEnabled(false);
        SetRigidbodiesKinematic(true);
    }

    private void LateUpdate()
    {
        if(GetComponent<Health>().health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        anim.enabled = false;
        GetComponent<Health>().regenHealth = 0;
        gameObject.layer = 9;
        ChangeLayer();
        SetCollidersEnabled(true);
        SetRigidbodiesKinematic(false);
        GetComponent<Combat>().enabled = false;
        if(tag != "Tourist")
        GetComponent<Throw>().enabled = false;

        enemy.GetComponent<Combat>().enabled = false;
        GetComponent<Flinch>().isFlinching = false;
        if (tag == "Player")
        {
            GetComponent<CharacterController>().enabled = false;
            GetComponent<Movement>().enabled = false;
            enemy.GetComponent<AiBehavior>().enabled = false;
            enemy.GetComponent<AiBehavior>().agent.isStopped = true;
        }
        else if (tag == "Enemy")
        {
            GetComponent<AiBehavior>().agent.isStopped = true;
            GetComponent<AiBehavior>().enabled = false;
            GetComponent<CapsuleCollider>().enabled = false;
            GetComponent<NavMeshAgent>().enabled = false;
        } else if(tag == "Tourist")
        {
            GetComponent<Tourist>().agent.isStopped = true;
            GetComponent<Tourist>().enabled = false;
            GetComponent<CapsuleCollider>().enabled = false;
            GetComponent<NavMeshAgent>().enabled = false;
        }
    }

    void ChangeLayer()
    {
        foreach (Transform t in transform)
        {
            t.gameObject.layer = 9;
        }
    }

    void SetCollidersEnabled(bool enabled)
    {
        foreach (Collider col in colliders)
        {
            col.enabled = enabled;
        }
    }

    void SetRigidbodiesKinematic(bool enabled)
    {
        foreach (Rigidbody rb in rigidbodies)
        {
            rb.isKinematic = enabled;
        }
    }

    public IEnumerator Revive()
    {
        anim.enabled = true;
        SetCollidersEnabled(false);
        SetRigidbodiesKinematic(true);
        //anim.SetBool("canTransition", true);
        anim.SetInteger("State", 150);
        yield return new WaitForSeconds(.5f);
        anim.SetInteger("State", 0);
    }
}
