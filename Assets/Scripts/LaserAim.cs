using UnityEngine;
using System.Collections;

public class LaserAim : MonoBehaviour {

    public GameObject laserPrefab;
    public Transform laserStartingPoint;
    public GameObject laserFlarePrefab;
    GameObject lFlare;
    LineRenderer lRender;

	void Start () 
    {
        GameObject go = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
        go.transform.parent = transform;
        lRender = go.GetComponent<LineRenderer>();

        lFlare = Instantiate(laserFlarePrefab, transform.position, Quaternion.identity) as GameObject;
	}
	
	void Update () 
    {
        lRender.SetPosition(0, laserStartingPoint.position);
        lRender.SetPosition(1, laserStartingPoint.position);

        Ray ray = new Ray(laserStartingPoint.position, laserStartingPoint.forward);

        Vector3 lastPos = ray.GetPoint(100);

        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, 100))
        {
            lastPos = hit.point;
        }

        lRender.SetPosition(1, lastPos);
        lFlare.transform.position = lastPos;
	}
}
