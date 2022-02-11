using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum FlagStatus{
    UNFLAGGED = 0,
    FLAGGED = 1,
    UNSURE = 2
}
public class Flag : MonoBehaviour
{
    [SerializeField] private CubeManager cubeManager;
    [SerializeField] private Camera mainCamera;

    private void Start(){
        mainCamera = FindObjectOfType<Camera>();
    }

    private void flagCell(){
        if (Input.GetMouseButtonDown(1)){
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray,out hit,100) && hit.transform.tag == "Cube"){ // check if cube is checked
                    Cube currentCube = hit.transform.GetComponent<Cube>();
                    if (!currentCube.HasBeenChecked){
                        currentCube.Status = (currentCube.Status == FlagStatus.FLAGGED) ? FlagStatus.UNSURE : (currentCube.Status == FlagStatus.UNFLAGGED) ? FlagStatus.FLAGGED : FlagStatus.UNFLAGGED;
                        hit.transform.gameObject.GetComponent<Renderer>().material = cubeManager.Materials[currentCube.Status==FlagStatus.UNFLAGGED?"unclicked":currentCube.Status == FlagStatus.FLAGGED ? "sure_flagged" : "flagged"];
                    }
                }
        }
    }



    void Update()
    {
        flagCell();
    }
}
