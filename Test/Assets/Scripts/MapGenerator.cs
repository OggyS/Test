using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test
{
    public class MapGenerator : MonoBehaviour
    {
        int width = 15;
        int length = 15;
        [SerializeField]
        GameObject blackCube;
        [SerializeField]
        GameObject whiteCube;
        GameObject temp;
        int counter=0;
        int rnd, entry1,entry2, exit1,exit2;
        Test.Vector3[][] grid;
        GameObject[][] cells;
        float time = 0f;
        Test.Vector3 v3;
        [SerializeField]
        bool animate = false;
        void Start()
        {
            v3 = new Test.Vector3();
            grid = Test.Vector3.CreateGrid(width, length, 0, 1, 1);
            Grid();
        }

        // Update is called once per frame
       
        private void FixedUpdate()
        {
            if (time != 0&&animate)
            {
                StartCoroutine(AnimateGrid());
            }
            time += Time.deltaTime;
        }
        void Grid()
        {
            
            entry1 = Random.Range(1, length-1);
            do
            {
                entry2 = Random.Range(1, length-1);
            } while (!(entry2 != entry1 && entry2 != entry1 + 1 && entry2 != entry1 - 1));
            exit1 = Random.Range(1, length-1);
            do
            {
                exit2 = Random.Range(1, length-1);
            } while (!(exit2 != exit1 && exit2 != exit1 + 1 && exit2 != exit1 - 1));
            rnd = Random.Range(2, 4);//2 or 3 white in row
            cells = new GameObject[grid.Length][];
            //Debug.Log("Random=" + rnd);
            for (int i = 0; i < length; i++)
            {
                cells[i] = new GameObject[grid[i].Length];
                for (int j = 0; j <width; j++)
                {
                    
                    if(i==0 || i == length - 1)//top and bottom row
                    {
                        temp = Instantiate(blackCube, new UnityEngine.Vector3(j, 0f, i), Quaternion.identity, transform);
                        temp.name = "Black";
                        cells[i][j] = temp;
                    }
                    if (j == 0 &&i>0&&i<length-1)//first column
                    {
                        if(i == entry1 || i == entry2)
                        {
                            temp = Instantiate(whiteCube, new UnityEngine.Vector3(j, 0f, i), Quaternion.identity, transform);
                            temp.name = "White";
                            cells[i][j] = temp;
                        }
                        else
                        {
                            temp = Instantiate(blackCube, new UnityEngine.Vector3(j, 0f, i), Quaternion.identity, transform);
                            temp.name = "Black";
                            cells[i][j] = temp;
                        }
                    }
                    if (j == width-1&& i > 0 && i < grid.Length-1)//last column
                    {
                        if (i == exit1 || i == exit2)
                        {
                            temp = Instantiate(whiteCube, new UnityEngine.Vector3(j, 0f, i), Quaternion.identity, transform);
                            temp.name = "White";
                            cells[i][j] = temp;
                        }
                        else
                        {
                            temp = Instantiate(blackCube, new UnityEngine.Vector3(j, 0f, i), Quaternion.identity, transform);
                            temp.name = "Black";
                            cells[i][j] = temp;
                        }
                    }
                    if (i > 0 && i < length - 1&& j>0 && j<width-1)
                    {
                        if (entry1 == i|| entry2==i)
                        {
                            counter++;
                        }
                        if(exit1==i|| exit2 == i)
                        {
                            if (j == width -4)
                            {
                                counter = rnd;
                            }
                        }
                        if (j % 2 == 1)
                        {
                            temp = Instantiate(whiteCube, new UnityEngine.Vector3(j, 0f, i), Quaternion.identity, transform);
                            temp.name = "White";
                            cells[i][j] = temp;
                            counter++;
                        }
                        else
                        {
                            if (counter < rnd)
                            {
                                temp = Instantiate(whiteCube, new UnityEngine.Vector3(j, 0f, i), Quaternion.identity, transform);
                                temp.name = "White";
                                cells[i][j] = temp;
                                counter++;
                            }
                            else
                            {
                                temp = Instantiate(blackCube, new UnityEngine.Vector3(j, 0f, i), Quaternion.identity, transform);
                                temp.name = "Black";
                                cells[i][j] = temp;
                                rnd = Random.Range(1, 4);//1-3 white fields 
                                counter = 0;
                            }
                        }
                        
                    }
                }
            }
        }
        IEnumerator AnimateGrid()
        {
            grid = v3.StirUpGrid(time, 1);
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    cells[i][j].transform.position = new Vector3(grid[i][j].X, grid[i][j].Y, grid[i][j].Z);
                }
            }
            yield return new WaitForFixedUpdate();
        }
       
    }
}
