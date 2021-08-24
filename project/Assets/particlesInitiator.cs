using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particlesInitiator : MonoBehaviour
{
    public GameObject particles;

    public void initParticles() {
        particles.SetActive(true);
    }
}
