using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CubeManager : MonoBehaviour
{
    
    [SerializeField] private GameObject cubePrefab;
    [SerializeField] private Transform parent;
    [SerializeField] private int rows;
    public int Rows => rows;
    
    [SerializeField] private int columns;
    public int Columns => columns;

    [SerializeField] private int numberOfMines;
    public int NumberOfMines => numberOfMines;

    private Cube[,] _grid;
    public Cube[,] Grid => _grid;

    private Renderer[,] _renderers;
    public Renderer[,] Renderers => _renderers;


    [SerializeField]
    private string[] keys;
    [SerializeField]
    private Material[] values;
    private Dictionary<string,Material> _materials = new Dictionary<string, Material>();
    public Dictionary<string,Material> Materials => _materials;
    void Start()
    {
        //values = (Material[])Resources.LoadAll("Materials",typeof(Material));
        _grid = new Cube[rows,columns];
        _renderers = new Renderer[rows,columns];
        for (int i = 0; i != Mathf.Min(keys.Length, values.Length); i++)
            _materials.Add(keys[i], values[i]);
        InitGrid();
    }

    void InitGrid(){
        for(int i=0;i<rows;++i){
            for(int j=0;j<columns;++j){
                GameObject o = Instantiate(cubePrefab,new Vector3(1.1f*i,1.1f*j,0),Quaternion.identity);
                o.AddComponent<Cube>();
                _grid[i,j] = o.GetComponent<Cube>();
                _grid[i,j].X = i;
                _grid[i,j].Y = j;
                _grid[i,j].transform.parent = parent;
                _renderers[i,j] = o.GetComponent<Renderer>();
                _renderers[i,j].material = _materials["unclicked"];
            }
        }
        GenerateMines();
    }

    void GenerateMines(){
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
            x = (listNumbers[i]%rows);
            y = (listNumbers[i]%columns);
            _grid[x,y].CubeValue = -1;
        }
        for(int i=0;i<rows;++i){
            for(int j=0;j<columns;++j){
                if (_grid[i,j].CubeValue != -1){
                    for(int k = -1; k<= 1; ++k){
                        for(int l = -1; l<= 1; ++l){
                            if (i+k >= 0 && i+k < rows && j+l >= 0 && j+l < columns)
                                _grid[i,j].CubeValue += (_grid[i+k,j+l].CubeValue == -1)? 1 : 0;
                        }
                    }
                }
            }
        }
    }

    public void RevealBombs(int x, int y){
        for(int i = 0; i<rows; i++){
            for (int j = 0; j<columns; j++){
                if (_grid[i,j].CubeValue == -1 && _grid[i,j].Status != FlagStatus.FLAGGED){
                    _renderers[i,j].material = _materials["unclicked_bomb"];
                }
            }
        }
        _renderers[x,y].material = _materials["clicked_bomb"];
    } 

    void OnDrawGizmos()
    {
        GUIStyle style = new GUIStyle();
        style.normal.textColor = Color.red;
        for(int i=0;i<rows;++i){
            for(int j=0;j<columns;++j){
                if (_grid[i,j] != null)
                    Handles.Label(new Vector3(1.1f*i,1.15f*j,0),""+_grid[i,j].CubeValue+'\n'+_grid[i,j].HasBeenChecked, style);
            }
        }
        
    }
}
