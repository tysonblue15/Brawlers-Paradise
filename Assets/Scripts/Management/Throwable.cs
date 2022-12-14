using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : MonoBehaviour
{
    GameObject combatManagear;
    public string previousTag;
    public GameObject holder;

    private void Awake()
    {
        combatManagear = GameObject.Find("Combat Manager");
    }

    private void Update()
    {
        if (transform.parent != null)
        {
            if (transform.parent.name == "Throwables")
            {
                GetComponent<MeshRenderer>().enabled = true;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if((collision.gameObject.tag == "Enemy" && holder != collision.gameObject) || (collision.gameObject.tag == "Player" && previousTag == "Enemy"))
        {
            if (name == "Briefcase")
            {
                collision.gameObject.GetComponent<Flinch>().ReactionInitiation(100, combatManagear.GetComponent<CombatStats>().throwableDamage * 2);
            }
            else
            {
                
                collision.gameObject.GetComponent<Flinch>().ReactionInitiation(100, combatManagear.GetComponent<CombatStats>().throwableDamage);
            }
            StartCoroutine(ResetThrowable(collision.gameObject));
        }
    }
    IEnumerator ResetThrowable(GameObject throwable)
    {
        previousTag = "";
        yield return new WaitForSeconds(.25f);
        if (name == "Briefcase")
        {
            previousTag = "";
            yield return new WaitForSeconds(1);
            Destroy(gameObject);
            yield return null;
        }
        else
        {
            yield return new WaitForSeconds(2);
            tag = "Throwable";
            if (throwable.tag != previousTag)
            {
                previousTag = "";
            }
        }

    }
}
