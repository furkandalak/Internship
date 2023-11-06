using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Random = UnityEngine.Random;

public class GameObj : MonoBehaviour
{
    public GameObject puzzleCube;
    public GameObject gridCube;
    public GameObject clickArea;
    
    private List<GameObject> puzzleCubes = new List<GameObject>();
    private List<GameObject> gridCubes = new List<GameObject>();
    private List<GameObject> clickAreas = new List<GameObject>();
    
    public static int row = 10;

    public static int column = 10;

    public float separate = 1.0f;

    private float boxSize = 0.4f;
    
    public int[,] slots = new int[row, column];
    private int[,] tempArr = new int[row, column];

    public int score = 0;
    
    public int puzzleNumber = 3;
    public float puzzleDistance = 5.0f;
    public int[] curPuzzles;
    
    private List<int[,]> puzzleList = new List<int[,]>();
    
    private int[,] puzzle1 = new int[3,3] {{ 1 , 1 , 1 }, { 1 , 0 , 0 }, { 1 , 0 , 0}};
    private int[,] puzzle2 = new int[1,4] {{ 1 , 1 , 1 , 1 }};
    private int[,] puzzle3 = new int[4,1] {{ 1 }, { 1 }, { 1 } , { 1 }};
    private int[,] puzzle4 = new int[4,4] {{ 1 , 1 , 1 , 1 }, { 1 , 1 , 1 , 1 }, { 1 , 1 , 1 , 1 } , { 1 , 1 , 1 , 1 }};
    private int[,] puzzle5 = new int[1,3] {{ 1 , 1 , 1 }};
    private int[,] puzzle6 = new int[1,1] {{ 1 }};
    private int[,] puzzle7 = new int[2,2] {{ 1 , 1 }, { 1 , 0 }};
    private int[,] puzzle8 = new int[2,2] {{ 0 , 1 }, { 1 , 1 }};
    private int[,] puzzle9 = new int[2,2] {{ 1 , 1 }, { 1 , 1 }};
    private int[,] puzzle10 = new int[3,3] {{ 1 , 1 , 1 }, { 1 , 0 , 0 }, { 1 , 0 , 0 }};
    private int[,] puzzle11 = new int[3,3] {{ 0 , 0 , 1 }, { 0 , 0 , 1 }, { 1 , 1 , 1 }};
    private int[,] puzzle12 = new int[3,3] {{ 1 , 1 , 1 }, { 1 , 1 , 1 }, { 1 , 1 , 1 }};
    private int[,] puzzle13 = new int[3,3] {{ 0 , 0 , 1 }, { 0 , 0 , 1 }, { 1 , 1 , 1 }};
    private int[,] puzzle14 = new int[3,3] {{ 1 , 0 , 0 }, { 1 , 0 , 0 }, { 1 , 1 , 1 }};
    private int[,] puzzle15 = new int[2,2] {{ 1 , 1 }, { 0 , 1 }};
    private int[,] puzzle16 = new int[2,2] {{ 1 , 0 }, { 1 , 1 }};
    private int[,] puzzle17 = new int[1,2] {{ 1 , 1 }};
    private int[,] puzzle18 = new int[2,1] {{ 1 }, { 1 }};

    public int choosenPuzzle;
    public int choosenSlot;
    
    // Start is called before the first frame update
    void Start()
    {
        puzzleList.Add(puzzle1);
        puzzleList.Add(puzzle2);
        puzzleList.Add(puzzle3);
        puzzleList.Add(puzzle4);
        puzzleList.Add(puzzle5);
        puzzleList.Add(puzzle6);
        puzzleList.Add(puzzle7);
        puzzleList.Add(puzzle8);
        puzzleList.Add(puzzle9);
        puzzleList.Add(puzzle10);
        puzzleList.Add(puzzle11);
        puzzleList.Add(puzzle12);
        puzzleList.Add(puzzle13);
        puzzleList.Add(puzzle14);
        puzzleList.Add(puzzle15);
        puzzleList.Add(puzzle16);
        puzzleList.Add(puzzle17);
        puzzleList.Add(puzzle18);
        
        curPuzzles = new int[puzzleNumber];

        choosenPuzzle = -1;
        choosenSlot = -1;
        
        for (int i = 0; i < column; i++)
        {
            for (int j = 0; j < row; j++)
            {
                slots[i, j] = 0;
                tempArr[i, j] = 0;
            }
        }
        float baseY = ((column - 1) * separate)/2;
        float baseX = ((row - 1) * separate)/2;
        Vector3 topLeft = new Vector3(-baseX, baseY, 0);
    
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < column; j++)
            {
                Vector3 position = topLeft + new Vector3(j * separate, -i * separate, 0);
                GameObject newObject = Instantiate(gridCube, position, Quaternion.identity);
                newObject.GetComponent<CubeGrid>().SetIndex(i, j);
                gridCubes.Add(newObject); 
            }
        }
        puzzleRandomise();
    }

    // Update is called once per frame
    void Update()
    {
        boxSize = separate - 0.1f;
        
        Vector3 mousePos = Input.mousePosition;
        
        Vector3 target = new Vector3(0, 0, 0);
        mousePos.z = -Camera.main.transform.position.z - target.z;
        
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);

        float baseY = ((column - 1) * separate) / 2;
        float baseX = ((row - 1) * separate) / 2;

        int columnIndex = Mathf.FloorToInt((worldPos.x + baseX + separate / 2) / separate);
        int rowIndex = Mathf.FloorToInt((baseY - worldPos.y + separate / 2) / separate);

        
        for (int i = 0; i < column; i++)
        {
            for (int j = 0; j < row; j++)
            {
                if (slots[i, j] == 2)
                {
                    slots[i, j] = 0;
                    SetHighlight(i, j, 0);
                }
            }
        }
        
        if (choosenSlot != -1)
        {
            foreach (GameObject obj in puzzleCubes) 
            {
                if (obj != null)
                {
                    CubeBehaviour cubeScript = puzzleCube.GetComponent<CubeBehaviour>();

                    int inn = obj.GetComponent<CubeBehaviour>().getIndex();
                        
                    if (inn == choosenSlot)
                    {
                        obj.GetComponent<CubeBehaviour>().setState(1);
                        int hoverRow, hoverColumn;
                        obj.GetComponent<CubeBehaviour>().ReturnMyLocation(out hoverColumn, out hoverRow);
                        if (hoverColumn >= 0 && hoverColumn < column && hoverRow >= 0 && hoverRow < row)
                        {
                            if (slots[hoverColumn, hoverRow] != 1)
                            {
                                slots[hoverColumn, hoverRow] = 2;
                                Debug.Log(hoverColumn + "/" + hoverColumn);
                                SetHighlight(hoverColumn, hoverRow, 2);
                            }
                        }
                    }
                }
            }
        }


        if (Input.GetMouseButtonUp(0) && choosenPuzzle != -1)
        {
            if (columnIndex >= 0 && columnIndex < column && rowIndex >= 0 && rowIndex < row)
            {
                bool test = CheckCubePlacement(choosenSlot);
                if (test == false)
                {
                    putBack();
                }
                else
                {
                    curPuzzles[choosenSlot] = -2;
                    choosenPuzzle = -1;
                    choosenSlot = -1;
                }
                //putPiece(rowIndex, columnIndex, puzzleList[choosenPuzzle]);
            }
            else
            {
                putBack();
            }
            checkDestroy();
            checkLose();
        }

        int checkPuzzleEmpty = 0;
        for (int c = 0; c < puzzleNumber; c++)
        {
            if (curPuzzles[c] == -2)
            {
                checkPuzzleEmpty += 1;
            }
        }
        if (checkPuzzleEmpty == puzzleNumber)
        {
            puzzleRandomise();
            checkLose();
        }
        
        if (columnIndex >= 0 && columnIndex < column && rowIndex >= 0 && rowIndex < row &&
        slots[rowIndex, columnIndex] != 1)
        {
            //slots[rowIndex, columnIndex] = 2;
            
        }
}
    
    void OnDrawGizmos()
    {
        float baseY = ((column - 1) * separate)/2;
        float baseX = ((row - 1) * separate)/2;
        Vector3 topLeft = new Vector3(-baseX, baseY, 0);
        
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < column; j++)
            {
                Vector3 position = topLeft + new Vector3(j * separate, -i * separate, 0);
                if (slots[i, j] == 0)
                {
                    Gizmos.color = Color.gray;
                }
                else if (slots[i, j] == 1)
                {
                    Gizmos.color = Color.green;
                }
                else
                {
                    Gizmos.color = Color.white;
                }
            }
        }
        
    }

    void checkDestroy()
    {

        // Satırları test et
        for (int i = 0; i < column; i++)
        {
            for (int j = 0; j < row; j++)
            {
                tempArr[i, j] = slots[i, j];
            }
        }
        
        // Satırları test et
        for (int i = 0; i < column; i++)
        {
            int points = 0;
            for (int j = 0; j < row; j++)
            {
                if (tempArr[i, j] == 1)
                {
                    points++;
                }
            }
            if (points >= row)
            {
                deleteRow(i);
            }
        }
        
        // Sütunları test et
        for (int i = 0; i < row; i++)
        {
            int points = 0;
            for (int j = 0; j < column; j++)
            {
                if (tempArr[j, i] == 1)
                {
                    points++;
                }
            }
            if (points >= column)
            {
                deleteColumn(i);
            }
        }
        
    }

    void deleteColumn(int no)
    {
        increaseScore(column);
        for(int dc = 0; dc < column; dc++)
        {
            foreach (GameObject obj in puzzleCubes) 
            {
                if (obj != null)
                {
                    CubeBehaviour cubeScript = puzzleCube.GetComponent<CubeBehaviour>();

                    int cubeC = -1;
                    int cubeR = -1;
                
                    obj.GetComponent<CubeBehaviour>().ReturnMyLocation(out cubeC, out cubeR);
                    int objState = obj.GetComponent<CubeBehaviour>().GetState();
                    if (cubeR == no && cubeC == dc)
                    {
                        obj.GetComponent<CubeBehaviour>().setState(3);
                        obj.GetComponent<CubeBehaviour>().scaleChangeStart = dc * 6;
                        //Destroy(obj);
                    }
                }
            }
            SetHighlight(dc, no, 0);
            slots[dc, no] = 0;
        }
    }
    
    void deleteRow(int no)
    {
        increaseScore(row);
        for(int dr = 0; dr < row; dr++)
        {
            foreach (GameObject obj in puzzleCubes) 
            {
                if (obj != null)
                {
                    CubeBehaviour cubeScript = puzzleCube.GetComponent<CubeBehaviour>();

                    int cubeC = -1;
                    int cubeR = -1;
                
                    obj.GetComponent<CubeBehaviour>().ReturnMyLocation(out cubeC, out cubeR);
                    if (cubeR == dr && cubeC == no)
                    {
                        obj.GetComponent<CubeBehaviour>().setState(3);
                        obj.GetComponent<CubeBehaviour>().scaleChangeStart = dr * 6;
                        //Destroy(obj);
                    }
                }
            }
            SetHighlight(no, dr, 0);
            slots[no, dr] = 0;
        }
    }

    void increaseScore(int no)
    {
        score += no;
        Debug.Log("Score:" + score);
    }

    void puzzleRandomise()
    {
        clickAreas.Clear();
        for (int ri = 0; ri < puzzleNumber; ri++)
        {
            curPuzzles[ri] = Random.Range(0, puzzleList.Count);
            cubesCreateAtPuzzle(ri);
        }
    }

    void putBack()
    {
        foreach (GameObject obj in puzzleCubes) {
            if (obj != null)
            {
                CubeBehaviour cubeScript = puzzleCube.GetComponent<CubeBehaviour>();

                int inn = obj.GetComponent<CubeBehaviour>().getIndex();
                if (inn == choosenSlot)
                {
                    obj.GetComponent<CubeBehaviour>().setState(0);
                }
            }
        }
        
        curPuzzles[choosenSlot] = choosenPuzzle;
        choosenPuzzle = -1;
        choosenSlot = -1;
    }

    public bool CheckCubePlacement(int curPuzzleIndex)
    {
        bool cubesPlaceable = true;
        
        foreach (GameObject obj in puzzleCubes) 
        {
            if (obj != null)
            {
                CubeBehaviour cubeScript = puzzleCube.GetComponent<CubeBehaviour>();


                int cubeIn =  obj.GetComponent<CubeBehaviour>().getIndex();
                if (cubeIn == curPuzzleIndex)
                {                
                    int cubeC = -1;
                    int cubeR = -1;
                
                    obj.GetComponent<CubeBehaviour>().ReturnMyLocation(out cubeC, out cubeR);
                    if (cubeC < column && cubeR < row && cubeC >= 0 && cubeR >= 0)
                    {
                        if (slots[cubeC, cubeR] == 1)
                        {
                            //Debug.Log("Index:" + cubeIn + "/" + curPuzzleIndex + "[" + cubeC + "/" + cubeR);
                            cubesPlaceable = false;
                            break;
                        }
                    }
                    else
                    {
                        cubesPlaceable = false;
                        break;
                    }
                }
            }
        }

        if (cubesPlaceable == true)
        {
            foreach (GameObject clickObj in clickAreas)
            {
                if (clickObj != null)
                {
                    int cubeIn =  clickObj.GetComponent<SlotChoose>().GetIndex();
                    if (cubeIn == curPuzzleIndex)
                    {
                        Destroy(clickObj);
                    }
                }
            }
            foreach (GameObject obj in puzzleCubes) 
            {
                if (obj != null)
                {
                    CubeBehaviour cubeScript = puzzleCube.GetComponent<CubeBehaviour>();

                    int cubeC = -1;
                    int cubeR = -1;
                
                    obj.GetComponent<CubeBehaviour>().ReturnMyLocation(out cubeC, out cubeR);
                    int cubeIn =  obj.GetComponent<CubeBehaviour>().getIndex();
                    if (cubeIn == curPuzzleIndex)
                    {
                        slots[cubeC, cubeR] = 1;
                        SetHighlight(cubeC, cubeR, 1);
                        score += 1;

                        obj.GetComponent<CubeBehaviour>().PlaceMyself();
                        obj.GetComponent<CubeBehaviour>().setState(2);
                    }
                }
            }
        }
        Debug.Log("Score: " + score);
        return cubesPlaceable;
    }
    
    public bool checkPlaceable(int c, int r, int [,] myPuzzle)
    {
        bool placeable = true;
        
        int puzzleLength = myPuzzle.Length;
        int puzzleRow = myPuzzle.GetLength(1);
        int puzzleColumn = puzzleLength / puzzleRow;
        for (int i = 0; i < puzzleColumn; i++)
        {
            for (int j = 0; j < puzzleRow; j++)
            {
                if (myPuzzle[i, j] == 1)
                {
                    if ((j + r >= row || i + c >= column))
                    {
                        placeable = false;
                        break;
                    }
                    else if (slots[c + i, r + j] == 1)
                    {
                        placeable = false;
                        break;
                    }
                }
            }
        }
        
        return placeable;
    }

    void checkLose()
    {
        bool lose = true;

        int puzzleEmpty = 0;
        
        for (int l = 0; l < puzzleNumber; l++)
        {
            if (curPuzzles[l] >= 0)
            {
                for (int ci = 0; ci < column; ci++)
                {
                    for (int ri = 0; ri < row; ri++)
                    {
                        if (slots[ci, ri] != 1)
                        {
                            if (checkPlaceable(ci, ri, puzzleList[curPuzzles[l]]))
                            {
                                lose = false;
                            }
                            if (lose == false)
                            {
                                break;
                            }
                        }
                    }
                }
            }
            else
            {
                puzzleEmpty++;
            }
        }

        if (puzzleEmpty == puzzleNumber)
        {
            lose = false;
        }
        
        if (lose == true)
        {
            Debug.Log("Game Over! Score:" + score);
        }
    }

    void cubesCreateAtPuzzle(int cubeIndex)
    {
        Vector3 mousePos = Input.mousePosition;

        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        worldPos.z = 0;
        
        float puzzleY = ((-9 + puzzleDistance))/2;
        float puzzleX = ((puzzleNumber - 1) * puzzleDistance)/2;
        
        Vector3 puzzleLeft = new Vector3(-puzzleX, puzzleY, 0);
        Vector3 puzzlePos = puzzleLeft + new Vector3(cubeIndex * puzzleDistance, 0, 0);

        int myCurPuzzle = curPuzzles[cubeIndex];
        int[,] curPuzzle = puzzleList[myCurPuzzle];
        
        int puzzleLength = curPuzzle.Length;
        int puzzleRow = curPuzzle.GetLength(1);
        int puzzleColumn = puzzleLength / puzzleRow;
        
        float baseY = ((puzzleColumn - 1) * separate)/2;
        float baseX = ((puzzleRow - 1) * separate)/2;
        Vector3 topLeft = new Vector3(-baseX, baseY, 0);
        
        Vector3 cubesCenter = new Vector3(- ((puzzleRow - 1) * separate)/2, ((puzzleColumn - 1) * separate)/2, 0);
        
        GameObject clickAreaObj = Instantiate(clickArea, puzzlePos, Quaternion.identity);
        clickAreaObj.transform.localScale = new Vector3(separate * puzzleRow, separate * puzzleColumn, 0.2f);
        clickAreaObj.GetComponent<SlotChoose>().SetIndex(cubeIndex);
        clickAreas.Add(clickAreaObj); 
        
        for (int i = 0; i < puzzleColumn; i++)
        {
            for (int j = 0; j < puzzleRow; j++)
            {
                Vector3 position = puzzlePos + topLeft + new Vector3(j * separate, -i * separate, 0);
                if (curPuzzle[i, j] == 0)
                {
                    
                }
                else if (curPuzzle[i, j] == 1)
                {
                    GameObject newObject = Instantiate(puzzleCube, position + new Vector3(0, -3, 0), Quaternion.identity);
                    newObject.GetComponent<CubeBehaviour>().setOffset(new Vector3(j * separate, -i * separate, 0));
                    newObject.GetComponent<CubeBehaviour>().setCenter(cubesCenter);
                    newObject.GetComponent<CubeBehaviour>().setStart(position);
                    newObject.GetComponent<CubeBehaviour>().setIndex(cubeIndex);
                    puzzleCubes.Add(newObject); 
                }
                
            }
        }
    }

    void SetHighlight(int hColumn, int hRow, int hState)
    {
        foreach (GameObject objGrid in gridCubes) 
        {
            if (objGrid != null)
            {
                CubeBehaviour cubeScript = puzzleCube.GetComponent<CubeBehaviour>();

                int innColumn = objGrid.GetComponent<CubeGrid>().cubeColumn;
                int innRow = objGrid.GetComponent<CubeGrid>().cubeRow;
                        
                if (innColumn == hColumn && innRow == hRow)
                {
                    objGrid.GetComponent<CubeGrid>().state = hState;
                }
            }
        }
    }
    
}



