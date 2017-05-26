using UnityEngine;
using System.Collections;

public class ShootingHandler : MonoBehaviour {
 
    //Add a particle and it will emit when you click the left mouse button

    public int bullets = 30;
    public float fireRate = 0.3f;
    float frate;
    Animator anim;

    bool fire;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

	void Update () 
    {
        if (Input.GetMouseButton(0))
        {
            fire = true; 
        }

        if (fire)
        {
            frate += Time.deltaTime;

            if (frate > fireRate)
            {
                ParticleSystem[] parts = GetComponentsInChildren<ParticleSystem>();

                if (bullets > 0)
                {
                    foreach (ParticleSystem ps in parts)
                    {
                        ps.Emit(1);
                    }

                    bullets--;
                }
                else
                {
                    anim.SetBool("Reload", true);
                    StartCoroutine("CloseReload");
                }
                frate = 0;
                fire = false;
            }
        }
	}

    IEnumerator CloseReload()
    {
        yield return new WaitForSeconds(1);
        anim.SetBool("Reload", false);
        yield return new WaitForSeconds(0.5f);
        bullets = 30;
    }
}
