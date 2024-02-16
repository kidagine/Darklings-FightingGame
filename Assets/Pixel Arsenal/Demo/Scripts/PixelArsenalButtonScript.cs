using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace PixelArsenal
{
    public class PixelArsenalButtonScript : MonoBehaviour
    {
        public GameObject Button;
        Text MyButtonText;
        string projectileParticleName;      // The variable to update the text component of the button

        PixelArsenalFireProjectile effectScript;        // A variable used to access the list of projectiles
        PixelArsenalProjectileScript projectileScript;

        public float buttonsX;
        public float buttonsY;
        public float buttonsSizeX;
        public float buttonsSizeY;
        public float buttonsDistance;

        void Start()
        {
            effectScript = GameObject.Find("PixelArsenalFireProjectile").GetComponent<PixelArsenalFireProjectile>(); // The FireProjectile script needs to be on a gameobject called FireProjectile, or else it won't be found
            getProjectileNames();
            MyButtonText = Button.transform.Find("Text").GetComponent<Text>();
            MyButtonText.text = projectileParticleName;
        }

        void Update()
        {
            MyButtonText.text = projectileParticleName;
            //		print(projectileParticleName);
        }

        public void getProjectileNames()            // Find and diplay the name of the currently selected projectile
        {

            projectileScript = effectScript.projectiles[effectScript.currentProjectile].GetComponent<PixelArsenalProjectileScript>();// Access the currently selected projectile's 'ProjectileScript'
            projectileParticleName = projectileScript.projectileParticle.name;  // Assign the name of the currently selected projectile to projectileParticleName
        }

        public bool overButton()        // This function will return either true or false
        {
            Rect button1 = new Rect(buttonsX, buttonsY, buttonsSizeX, buttonsSizeY);
            Rect button2 = new Rect(buttonsX + buttonsDistance, buttonsY, buttonsSizeX, buttonsSizeY);

            if (button1.Contains(new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y)) ||
               button2.Contains(new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y)))
            {
                return true;
            }
            else
                return false;
        }
    }
}