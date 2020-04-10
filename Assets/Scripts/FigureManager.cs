using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FigureManager : MonoBehaviour
{

    public List<FigureUI> figures;

    
    //private FigureUI currentFigure;

    private FigureUI previousFigure;
    private FigureUI activeFigure;
    private FigureUI nextFigure;

    //public GameObject currentFigure;


    private MouseLook playerCursor;

    private RaycastHit mouseHit;

    private int figureOrder;

    private void Awake()
    {
        figureOrder = 0;
        //stepCounter = 0;

        playerCursor = FindObjectOfType<MouseLook>();
        if (!playerCursor)
            Debug.LogError("ERROR (" + name + "): Scene is missing MouseLook object!");

        playerCursor.onHit.AddListener(UpdateRaycastInfo);
        mouseHit = new RaycastHit();

        var figureList = FindObjectsOfType<FigureUI>();
        foreach(var figure in figureList)
        {
            //figure.onClicked.AddListener(SelectFigure);
            figures.Add(figure);
        }

        figures.Sort(SortByNumber);
        nextFigure = figures[figureOrder];
        
    }

    private void Start()
    {
        nextFigure.ChangeSprite("unclickedSprite");
    }

    private void Update()
    {

        if (Input.GetMouseButtonDown(0) && mouseHit.transform.name == nextFigure.name)
        {
          
            if (figureOrder >= figures.Count)
            {
                return;
            }

            if (activeFigure)
            {
                previousFigure = activeFigure;
                previousFigure.ChangeSprite("clickedSprite");
                previousFigure.Deselect();
            }
            activeFigure = nextFigure;
            activeFigure.ChangeSprite("hoverSprite");
            activeFigure.Select();

            if (previousFigure)
            {
                var go = new GameObject();
                var line = go.AddComponent<LineRenderer>();
                line.startWidth = 0.05f;
                line.endWidth = 0.05f;

                line.SetPosition(0, previousFigure.GetComponent<Renderer>().bounds.center);
                line.SetPosition(1, activeFigure.GetComponent<Renderer>().bounds.center);
            }

            nextFigure = figures[figureOrder + 1];
            nextFigure.ChangeSprite("unclickedSprite");
          
            figureOrder++;

            /*
            if (activeFigure)
            {
                if (mouseHit.transform.name != activeFigure.name)
                {
                    activeFigure.Deselect();
                    activeFigure = null;
                }
            }
            */
        }
    }

    private void UpdateRaycastInfo(RaycastHit hit)
    {
        mouseHit = hit;
    }
    
    private int SortByNumber(FigureUI f1, FigureUI f2)
    {
        return f1.figureNumber.CompareTo(f2.figureNumber);
    }
    


    /*
    private void SelectFigure(FigureUI ui)
    {
        // make sure a reference to a figure exists
        if (!activeFigure)
        {
            activeFigure = ui;
            ui.Select();
        }

        // if there has been a new selected figure...
        else if (activeFigure != ui)
        {
            activeFigure.Deselect();

            ui.Select(); // tell the UI it has been selected
            activeFigure = ui; // set new selected figure
        }

    }
    */
}
