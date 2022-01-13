using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CheckMine : MonoBehaviour
{

    [SerializeField] private CubeManager cubeManager;
    private void CheckMineMethod(){
        if (Input.GetMouseButtonDown(0)){
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray,out hit,100) && hit.transform.tag == "Cube"){
                Cube currentCube = hit.transform.GetComponent<Cube>();
                if (!currentCube.HasBeenChecked)
                    if (currentCube.CubeValue == -1 && currentCube.Status == 0){
                        cubeManager.revealBombs(currentCube.X,currentCube.Y);
                    }else{
                        checkCell(currentCube);
                    }
            }
        }
    }

  /*  private bool IsSafe(Cube cube){
        if (cube.CubeValue > 0 && !cube.HasBeenChecked){
            for(int i = -1; i<= 1; i++){
                for(int j = -1; j<=1; j++){
                    if (cube.X+i >= 0 && cube.X+i < cubeManager.Rows && cube.Y+j >= 0 && cube.Y+j < cubeManager.Columns){
                        if (cubeManager.Grid[cube.X+i,cube.Y+j].CubeValue == -1 && cube.Status == 0)
                            return false;
                    }
                }
            }
            return true;
        }
        return false;
    }*/

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
                cube.HasBeenChecked = true; 
                cubeManager.Renderers[cube.X,cube.Y].material = cubeManager.Materials[cube.CubeValue+"bomb"];
            }
        }
    }

    void Update()
    {
        CheckMineMethod();
    }

}
 