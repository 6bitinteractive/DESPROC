using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchFeedback : MonoBehaviour {

    [SerializeField] private GameObject psObject;
    private Vector3 prevPosition = Vector3.zero;
    private int fingerId = -1;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
#if UNITY_EDITOR || UNITY_STANDALONE
        if (Input.GetMouseButtonDown(0))
            psObject.GetComponent<ParticleSystem>().Play();

        if (Input.GetMouseButton(0))
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0;

            emitOnPosition(pos);
        }

        if(Input.GetMouseButtonUp(0))
        {
            stopEmission();
        }
#else
        if (Input.touchCount > 0 && fingerId < 0)
        {
            psObject.GetComponent<ParticleSystem>().Play();
            fingerId = Input.touches[0].fingerId;
        }

        if(Input.touchCount > 0 && Input.touches[0].fingerId == fingerId)
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.touches[0].position);
            pos.z = 0;

            emitOnPosition(pos);
        }

        if((Input.touchCount > 0 && Input.touches[0].fingerId != fingerId) || Input.touchCount == 0)
        {
            stopEmission();

            fingerId = -1;
        }
#endif
    }

    private void stopEmission()
    {
        psObject.GetComponent<ParticleSystem>().Stop();
        ParticleSystem.EmissionModule mod = psObject.GetComponent<ParticleSystem>().emission;
        mod.rateOverTime = 10;
    }

    private void emitOnPosition(Vector3 pos)
    {
        psObject.transform.position = pos;

        if (prevPosition != Vector3.zero)
        {
            ParticleSystem.EmissionModule mod = psObject.GetComponent<ParticleSystem>().emission;
            mod.rateOverTime = Mathf.Max((pos - prevPosition).magnitude * 50, 10);
        }
        prevPosition = pos;
    }
}
