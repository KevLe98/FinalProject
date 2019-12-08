using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    public ParticleSystem starfield1;
    public ParticleSystem starfield2;
    public ParticleSystem lightspeed1;
    public ParticleSystem lightspeed2;

    public GameObject starfield;

    // Start is called before the first frame update
    void Start()
    {
        starfield1.Play();
        starfield2.Play();
        lightspeed1.Stop();
        lightspeed2.Stop();
    }

    public void WinGame()
    {
        Destroy(starfield);
        lightspeed1.Play();
        lightspeed2.Play();
    }
}
