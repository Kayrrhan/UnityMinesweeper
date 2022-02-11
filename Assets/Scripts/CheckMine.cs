using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CheckMine : MonoBehaviour
{

    [SerializeField] private CubeManager cubeManager;
    private Camera mainCamera;

    private void Start(){
        mainCamera = FindObjectOfType<Camera>();
    }

    private void CheckMineMethod(){
        if (Input.GetMouseButtonDown(0)){
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray,out hit,100) && hit.transform.tag == "Cube"){
                Cube currentCube = hit.transform.GetComponent<Cube>();
                // bool containsUnflaggedBomb;
                if (!currentCube.HasBeenChecked){
                    if (currentCube.CubeValue == -1 && currentCube.Status == 0){
                        cubeManager.RevealBombs(currentCube.X,currentCube.Y);
                    }else{
                        checkCell(currentCube);
                    }
                }else if (IsAllFlagged(currentCube))
                {
                    RevealSafeCells(currentCube);
                }
            }
        }
    }

    private bool IsSafe(Cube cube){
        if (cube.CubeValue > 0){
            for(int i = -1; i<= 1; i++){
                for(int j = -1; j<=1; j++){
                    if (cube.X+i >= 0 && cube.X+i < cubeManager.Rows && cube.Y+j >= 0 && cube.Y+j < cubeManager.Columns){
                        if (cubeManager.Grid[cube.X+i,cube.Y+j].CubeValue == -1 && cubeManager.Grid[cube.X+i,cube.Y+j].Status == FlagStatus.UNFLAGGED){
                            Debug.Log(false);
                            return false;
                        }
                    }
                }
            }
            Debug.Log(true);
            return true;
        }
        Debug.Log(false);
        return false;
    }

    private void RevealSafeCells(Cube cube){
        for(int i = -1; i<= 1; i++){
                for(int j = -1; j<=1; j++){
                    if (cube.X+i >= 0 && cube.X+i < cubeManager.Rows && cube.Y+j >= 0 && cube.Y+j < cubeManager.Columns){
                        if (cubeManager.Grid[cube.X+i,cube.Y+j].CubeValue == -1 && cubeManager.Grid[cube.X+i,cube.Y+j].Status == FlagStatus.UNFLAGGED){
                            cubeManager.RevealBombs(cube.X+i,cube.Y+j);
                        }
                        checkCell(cubeManager.Grid[cube.X+i,cube.Y+j]);
                    }
                }
            }
    }

    private bool IsAllFlagged(Cube cube/*, out bool containsUnflaggedBomb*/){
        int nbFlagged = 0;
        // containsUnflaggedBomb = false;
        for(int i = -1; i<= 1; i++){
            for(int j = -1; j<= 1; j++){
                if (cube.X+i >= 0 && cube.X+i < cubeManager.Rows && cube.Y+j >= 0 && cube.Y+j < cubeManager.Columns){
                    if (cubeManager.Grid[cube.X+i,cube.Y+j].Status == FlagStatus.FLAGGED){
                        nbFlagged++;
                        // if (cubeManager.Grid[cube.X+i,cube.Y+j].CubeValue != -1){
                        //     containsUnflaggedBomb = true;
                        // }
                    }
                }
            }
        }
        return (cube.CubeValue == nbFlagged);
    }

    private void checkCell(Cube cube){
        if (cube.CubeValue == 0){
            cube.HasBeenChecked = true;
            for(int i = -1; i<=1; i++){
                for(int j = -1; j<=1; j++){
                    if (cube.X+i >= 0 && cube.X+i < cubeManager.Rows && cube.Y+j >= 0 && cube.Y+j < cubeManager.Columns){
                        cubeManager.Renderers[cube.X,cube.Y].material = cubeManager.Materials[cube.CubeValue+"bomb"];
                        if (!cubeManager.Grid[(cube.X+i),(cube.Y+j)].HasBeenChecked && (i!= 0 || j != 0))
                            checkCell(cubeManager.Grid[(cube.X+i),(cube.Y+j)]);
                    }
                }
            }
        }else{
            if (!cube.HasBeenChecked){
                if (cube.Status != FlagStatus.FLAGGED && cube.CubeValue != -1){
                    cube.HasBeenChecked = true;
                    cubeManager.Renderers[cube.X,cube.Y].material = cubeManager.Materials[cube.CubeValue+"bomb"];
                }else if (cube.Status != FlagStatus.FLAGGED && cube.CubeValue == -1){
                    Debug.Log("Here");
                    cubeManager.RevealBombs(cube.X,cube.Y);
                }
            }
        }
    }

    void Update()
    {
        CheckMineMethod();
    }

}
 