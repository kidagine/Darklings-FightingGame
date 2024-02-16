using UnityEngine;
using System.Collections;

public class PixelArsenalProjectileScript : MonoBehaviour
{
  public GameObject impactParticle;
  public GameObject projectileParticle;
  public GameObject muzzleParticle;


  public GameObject[] trailParticles;
  [HideInInspector]
  public Vector3 impactNormal; //Used to rotate impactparticle.

  private bool hasCollided = false;

  //changed from start because awake is called before OnCollisionEnter, start isn't.
  void Awake()
  {
    projectileParticle = Instantiate(projectileParticle, transform.position, transform.rotation) as GameObject;
    projectileParticle.transform.parent = transform;
    if (muzzleParticle)
    {
      muzzleParticle = Instantiate(muzzleParticle, transform.position, transform.rotation) as GameObject;
      Destroy(muzzleParticle, 1.5f); // Lifetime of muzzle effect.
    }
  }

  void OnCollisionEnter(Collision hit)
  {
    if (!hasCollided)
    {
      hasCollided = true;
      impactParticle = Instantiate(impactParticle, transform.position, Quaternion.FromToRotation(Vector3.up, impactNormal)) as GameObject;

      if (hit.gameObject.tag == "Destructible") // Projectile will destroy objects tagged as Destructible
      {
        Destroy(hit.gameObject);
      }

      foreach (GameObject trail in trailParticles)
      {
        GameObject curTrail = transform.Find(projectileParticle.name + "/" + trail.name).gameObject;
        curTrail.transform.parent = null;
        Destroy(curTrail, 3f);
      }
      Destroy(projectileParticle, 3f);
      Destroy(impactParticle, 5f);
      Destroy(gameObject);

      ParticleSystem[] trails = GetComponentsInChildren<ParticleSystem>();
      //Component at [0] is that of the parent i.e. this object (if there is any)
      for (int i = 1; i < trails.Length; i++)
      {

        ParticleSystem trail = trails[i];

        if (trail.gameObject.name.Contains("Trail"))
        {
          trail.transform.SetParent(null);
          Destroy(trail.gameObject, 2f);
        }
      }
    }
  }
}