using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotChoose : MonoBehaviour
{
    public int index;
    
    private GameObject gameObj;
    // Start is called before the first frame update
    void Start()
    {
        gameObj = GameObject.Find("GameObject");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
       
        
        Vector3 target = new Vector3(0, 0, 0);
        mousePos.z = -Camera.main.transform.position.z - target.z;
        
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);

        if (Input.GetMouseButtonDown(0))
        {
            if (worldPos.x > transform.position.x - transform.localScale.x/2 && worldPos.x < transform.position.x + transform.localScale.x/2 
                && worldPos.y > transform.position.y - transform.localScale.y/2 && worldPos.y < transform.position.y + transform.localScale.y/2)
            {
                GameObj GameObjScript = gameObj.GetComponent<GameObj>();
                GameObjScript.choosenSlot = index;
                GameObjScript.choosenPuzzle = GameObjScript.curPuzzles[index];
                GameObjScript.curPuzzles[index] = -1;
                //Debug.Log("My Index: " + index);
            }
        }

    }
    
    public void SetIndex(int index)
    {
        this.index = index;
    }
    public int GetIndex() {return this.index;}
    
}
