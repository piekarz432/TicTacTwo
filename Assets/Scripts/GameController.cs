using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private Sprite[] colorField;

    [SerializeField]
    private Sprite[] colorClickField;

    [SerializeField]
    private Button[] grid;

    [SerializeField]
    private int turn;  //0 - Player 1    1 - Player 2 

    private int turnCount;

    [SerializeField]
    private GameObject[] turnImage;  //Turn player

    [SerializeField]
    private Sprite[] symbol; // x  o 

    [SerializeField]
    private Sprite[] whoseWin;

    [SerializeField]
    private Image win;

    [SerializeField]
    private int[] markedSpace;

    [SerializeField]
    private Image[] winsLine;

    private int indexFildColor;

    // Start is called before the first frame update
    void Start()
    {
        markedSpace = new int[9];
        markedSpace = Enumerable.Repeat(100, markedSpace.Length).ToArray();
        win.gameObject.SetActive(false);
        turn = 0;
        turnCount = 0;
        turnImage[0].SetActive(true);
        var colorField = setFildColor();
        for (int i = 0; i < grid.Length; i++)
        {
            grid[i].GetComponent<Image>().sprite = colorField;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private Sprite setFildColor()
    {
        indexFildColor = Random.Range(0, colorField.Length);
        return colorField[indexFildColor];
    }

    public void gridButton(int whichNumber)
    {
        grid[whichNumber].transform.GetChild(0).GetComponent<Image>().enabled= true;
        grid[whichNumber].transform.GetChild(0).GetComponent<Image>().sprite = symbol[turn];
        grid[whichNumber].GetComponent<Image>().sprite = colorClickField[indexFildColor];
        grid[whichNumber].interactable = false;
        markedSpace[whichNumber] = turn;
        turnCount++;

        if (turnCount > 3)
        {
            checkWin();
        }

        if(turnCount > 8)
        {
            win.gameObject.SetActive(true);
            win.transform.GetChild(0).GetComponent<Image>().sprite = whoseWin[2];
        }

        if (turn == 0)
        {
            turn = 1;
            turnImage[0].SetActive(false); ;
            turnImage[1].SetActive(true);
        }
        else
        {
            turn = 0;
            turnImage[1].SetActive(false);
            turnImage[0].SetActive(true);
        }


    }

    private void checkWin()
    {
        int p1 = markedSpace[0] + markedSpace[1] + markedSpace[2];
        int p2 = markedSpace[0] + markedSpace[3] + markedSpace[6];
        int p3 = markedSpace[0] + markedSpace[4] + markedSpace[8];
        int p4 = markedSpace[1] + markedSpace[4] + markedSpace[7];
        int p5 = markedSpace[2] + markedSpace[5] + markedSpace[8];
        int p6 = markedSpace[2] + markedSpace[4] + markedSpace[6];
        int p7 = markedSpace[3] + markedSpace[4] + markedSpace[5];
        int p8 = markedSpace[6] + markedSpace[7] + markedSpace[8];

        var possibilityTable = new int[] { p1, p2, p3, p4, p5, p6, p7, p8 };

        for(int i = 0; i < possibilityTable.Length; i++)
        {
            if(possibilityTable[i] == 3 * turn)
            {
                displayWinner(i);
            }
        }

    }

    private void displayWinner(int index)
    {
        winsLine[index].gameObject.SetActive(true);

        if(turn == 0)
        {
            win.gameObject.SetActive(true);
            win.transform.GetChild(0).GetComponent<Image>().sprite = whoseWin[0];
        }
        else if (turn == 1)
        {
            win.gameObject.SetActive(true);
            win.transform.GetChild(0).GetComponent<Image>().sprite = whoseWin[1];
        }
    }
}
