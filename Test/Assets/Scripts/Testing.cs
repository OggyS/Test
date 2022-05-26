using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Test;
//using Vector3 = Test.Vector3;
using Vector3 = UnityEngine.Vector3;
public class Testing : MonoBehaviour
{
    [SerializeField] GameObject prefabCell;
    Test.Vector3[][] testGrid;
    [SerializeField] int length;
    [SerializeField] int width;
    [SerializeField] int deltaX;
    [SerializeField] int deltaY;
    [SerializeField] int pointHeight;
    GameObject tempGameObject;
    Test.Vector3 v3;
    public float time = 0f;
    public float waveLength = 0f;
    GameObject[][] gameObjects;

    // Start is called before the first frame update
    void Start()
    {
        testGrid = Test.Vector3.CreateGrid(width, length, pointHeight, deltaX, deltaY);
        v3 = new Test.Vector3();
        SpawnGrid();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()
    {
        if (time != 0)
        {
            UpdateGrid();
        }
         
         time += Time.deltaTime;
    }
    void SpawnGrid()
    {
        Vector3 temp;
        gameObjects = new GameObject[length][];
        for (int i = 0; i < length; i++)
        {
            gameObjects[i] = new GameObject[width];
            for (int j = 0; j < width; j++)
            {
                temp = new Vector3(testGrid[i][j].X, testGrid[i][j].Y, testGrid[i][j].Z);
                tempGameObject = Instantiate(prefabCell, temp, Quaternion.identity, transform);
                tempGameObject.name = testGrid[i][j].X.ToString() + "," + testGrid[i][j].Y.ToString() + "," + testGrid[i][j].Z.ToString();
                gameObjects[i][j] = tempGameObject;
                tempGameObject = null;
            }
        }
    }
    void UpdateGrid()
    {
        testGrid = v3.StirUpGrid(time, waveLength);
        for (int i = 0; i < length; i++)
        {
            for (int j = 0; j < width; j++)
            {
                gameObjects[i][j].transform.position = new Vector3(testGrid[i][j].X, testGrid[i][j].Y, testGrid[i][j].Z);
            }
        }
    }

}
