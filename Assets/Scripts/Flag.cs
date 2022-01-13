using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour
{
    [SerializeField] private CubeManager cubeManager;

    private void flagCell(){
        if (Input.GetMouseButtonDown(1)){
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray,out hit,100) && hit.transform.tag == "Cube"){ // check if cube is checked
                    Cube currentCube = hit.transform.GetComponent<Cube>();
                    Debug.Log(currentCube);
                    currentCube.Status = (short)((currentCube.Status+1)%3);
                    hit.transform.gameObject.GetComponent<Renderer>().material = cubeManager.Materials[currentCube.Status==0?"unclicked":currentCube.Status == 1 ? "sure_flagged" : "flagged"];
                }
        }
    }

    void Update()
    {
        flagCell();
    }
}
