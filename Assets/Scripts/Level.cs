using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/Level",order =1)]
public class Level : ScriptableObject
{
    [SerializeField] private GameObject levelPrefab;
    public GameObject LevelPrefab => levelPrefab;

    [SerializeField] private Vector3 cameraPos;
    public Vector3 CameraPos => cameraPos;

}
