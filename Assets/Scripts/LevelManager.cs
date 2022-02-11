using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private List<Level> levels = new List<Level>();
    [SerializeField] private Transform levelParent;
    [SerializeField] private GameObject inGameUIParent;
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private Vector3 cameraOrigin;
    private Level currentLevel;

    private Level GetLevelByName(string levelName){
        Level result = levels.Find(level => level.name == levelName);
        if (result) return result;
        throw new System.ArgumentException("Couldn't find any level named "+levelName);
    }

    public void DestroyLevels(){
        int childCount = levelParent.childCount;
        mainCamera.transform.position = cameraOrigin;
        for(int i=0;i<childCount;++i){
            Destroy(levelParent.GetChild(i).gameObject);
        }
    }

    public void SetLevel(Level level){
        DestroyLevels();
        Instantiate(level.LevelPrefab, levelParent);
        mainCamera.transform.position = level.CameraPos;
        currentLevel = level;
    }

    public void ResetLevel(){
        DestroyLevels();
        inGameUIParent.SetActive(true);
        SetLevel(currentLevel);
    }
}
