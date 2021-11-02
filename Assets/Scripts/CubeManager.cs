using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CubeManager : MonoBehaviour
{
    
    [SerializeField] private GameObject cubePrefab;
    [SerializeField] private Transform parent;
    [SerializeField] private int rows;
    
    [SerializeField] private int columns;

    [SerializeField] private int numberOfMines;

    private GameObject[,] _grid;

    [SerializeField]
    private string[] keys;
    [SerializeField]
    private Material[] values;
    private Dictionary<string,Material> _materials = new Dictionary<string, Material>();

    void Start()
    {
        //values = (Material[])Resources.LoadAll("Materials",typeof(Material));
        _grid = new GameObject[rows,columns];
        for (int i = 0; i != Mathf.Min(keys.Length, values.Length); i++)
            _materials.Add(keys[i], values[i]);
        initGrid();
    }

    void initGrid(){
        for(int i=0;i<rows;++i){
            for(int j=0;j<columns;++j){
                _grid[i,j] = Instantiate(cubePrefab,new Vector3(1.1f*i,1.1f*j,0),Quaternion.identity);
                _grid[i,j].gameObject.GetComponent<Cube>().X = i;
                _grid[i,j].gameObject.GetComponent<Cube>().Y = j;
                _grid[i,j].transform.parent = parent;
                _grid[i,j].gameObject.GetComponent<Renderer>().material = _materials["unclicked"];
            }
        }
        generateMines();
    }

    void generateMines(){
        List<int> listNumbers = new List<int>();
        int number;
        for (int i = 0; i < numberOfMines; ++i){
            do {
                number = Random.Range(0,rows*columns);
            } while (listNumbers.Contains(number));
            listNumbers.Add(number);
        }
        int x;
        int y;
        for(int i=0;i<numberOfMines;++i){
            x = listNumbers[i]/rows;
            y = listNumbers[i]%rows;
            Debug.Log("mine : "+listNumbers[i]+" / x: "+x+" / y: "+y);
            _grid[x,y].gameObject.GetComponent<Cube>().CubeValue = -1;
        }
        for(int i=0;i<rows;++i){
            for(int j=0;j<columns;++j){
                if (_grid[i,j].GetComponent<Cube>().CubeValue != -1){
                    for(int k = -1; k<= 1; ++k){
                        for(int l = -1; l<= 1; ++l){
                            if (i+k >= 0 && i+k < rows && j+l >= 0 && j+l < columns)
                                _grid[i,j].GetComponent<Cube>().CubeValue += (_grid[i+k,j+l].GetComponent<Cube>().CubeValue == -1)? 1 : 0;
                        }
                    }
                }
                _grid[i,j].gameObject.GetComponent<Renderer>().material = _materials[keys[_grid[i,j].gameObject.GetComponent<Cube>().CubeValue+1]];
            }
        }
    }

    void OnDrawGizmos()
    {
        GUIStyle style = new GUIStyle();
        style.normal.textColor = Color.red;
        for(int i=0;i<rows;++i){
            for(int j=0;j<columns;++j){
                if (_grid[i,j] != null)
                    Handles.Label(new Vector3(1.1f*i,1.1f*j,0),""+_grid[i,j].GetComponent<Cube>().CubeValue, style);
            }
        }
        
    }
}
