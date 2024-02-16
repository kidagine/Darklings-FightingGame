using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace PixelArsenal
{

public class PixelArsenalBeamScript : MonoBehaviour {

    [Header("Prefabs")]
    public GameObject[] beamLineRendererPrefab;
    public GameObject[] beamStartPrefab;
    public GameObject[] beamEndPrefab;

    private int currentBeam = 0;

    private GameObject beamStart;
    private GameObject beamEnd;
    private GameObject beam;
    private LineRenderer line;

    [Header("Adjustable Variables")]
    public float beamEndOffset = 1f; //How far from the raycast hit point the end effect is positioned
    public float textureScrollSpeed = 8f; //How fast the texture scrolls along the beam
	public float textureLengthScale = 3; //Length of the beam texture

    [Header("Put Sliders here (Optional)")]
    public Slider endOffSetSlider; //Use UpdateEndOffset function on slider
    public Slider scrollSpeedSlider; //Use UpdateScrollSpeed function on slider

    [Header("Put UI Text object here to show beam name")]
    public Text textBeamName;

    // Use this for initialization
    void Start()
    {
        if (textBeamName)
            textBeamName.text = beamLineRendererPrefab[currentBeam].name;
        if (endOffSetSlider)
            endOffSetSlider.value = beamEndOffset;
        if (scrollSpeedSlider)
            scrollSpeedSlider.value = textureScrollSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();

        if (Input.GetMouseButtonDown(0))
        {
            beamStart = Instantiate(beamStartPrefab[currentBeam], new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            beamEnd = Instantiate(beamEndPrefab[currentBeam], new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            beam = Instantiate(beamLineRendererPrefab[currentBeam], new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            line = beam.GetComponent<LineRenderer>();
        }
        if (Input.GetMouseButtonUp(0))
        {
            Destroy(beamStart);
            Destroy(beamEnd);
            Destroy(beam);
        }

        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray.origin, ray.direction, out hit))
            {
                Vector3 tdir = hit.point - transform.position;
                ShootBeamInDir(transform.position, tdir);
            }
        }
		
		if (Input.GetKeyDown(KeyCode.RightArrow)) //4 next if commands are just hotkeys for cycling beams
        {
            nextBeam();
        }

		if (Input.GetKeyDown(KeyCode.D))
		{
			nextBeam();
		}

		if (Input.GetKeyDown(KeyCode.A))
		{
			previousBeam();
		}
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            previousBeam();
        }
		
    }

    public void nextBeam() // Next beam
    {
        if (currentBeam < beamLineRendererPrefab.Length - 1)
            currentBeam++;
        else
            currentBeam = 0;

        if (textBeamName)
            textBeamName.text = beamLineRendererPrefab[currentBeam].name;
    }
	
	    public void previousBeam() // Previous beam
    {
        if (currentBeam > - 0)
            currentBeam--;
        else
            currentBeam = beamLineRendererPrefab.Length - 1;

        if (textBeamName)
            textBeamName.text = beamLineRendererPrefab[currentBeam].name;
    }
	

    public void UpdateEndOffset()
    {
        beamEndOffset = endOffSetSlider.value;
    }

    public void UpdateScrollSpeed()
    {
        textureScrollSpeed = scrollSpeedSlider.value;
    }

    void ShootBeamInDir(Vector3 start, Vector3 dir)
    {
        line.SetVertexCount(2);
        line.SetPosition(0, start);
        beamStart.transform.position = start;

        Vector3 end = Vector3.zero;
        RaycastHit hit;
        if (Physics.Raycast(start, dir, out hit))
            end = hit.point - (dir.normalized * beamEndOffset);
        else
            end = transform.position + (dir * 100);

        beamEnd.transform.position = end;
        line.SetPosition(1, end);

        beamStart.transform.LookAt(beamEnd.transform.position);
        beamEnd.transform.LookAt(beamStart.transform.position);

        float distance = Vector3.Distance(start, end);
        line.sharedMaterial.mainTextureScale = new Vector2(distance / textureLengthScale, 1);
        line.sharedMaterial.mainTextureOffset -= new Vector2(Time.deltaTime * textureScrollSpeed, 0);
    }
}
}