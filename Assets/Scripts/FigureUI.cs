using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class FigureUIEvent : UnityEvent<FigureUI> {};

public class FigureUI : MonoBehaviour
{
    private MouseLook playerCursor;
    private SpriteRenderer spriteRenderer;

    
    [Header("Sprite States")]
    [SerializeField] Sprite unclickedSprite;
    [SerializeField] Sprite hoverSprite;
    [SerializeField] Sprite clickedSprite;
    

    
    //[HideInInspector] public FigureUIEvent onCursorEnter;
    [HideInInspector] public FigureUIEvent onClicked;
    //[HideInInspector] public FigureUIEvent onCursorExit;
    

    [Header("Selection Events")]
    public FigureUIEvent onSelect;
    public FigureUIEvent onDeselect;

    [HideInInspector] public bool isHoveredOver = false;
    private bool isHoveredOverOnPrevFrame = false;


    // field which is not managed internally
    [HideInInspector] public bool isSelected = false;

    //specify which number in the sequence the figure is
    public int figureNumber;

    private void Awake()
    {

        playerCursor = FindObjectOfType<MouseLook>();
        spriteRenderer = this.GetComponentInParent<SpriteRenderer>();

        if(!playerCursor)
            Debug.LogError("ERROR (" + name + "): Scene is missing MouseLook object!");

        if (!spriteRenderer)
            Debug.LogError("ERROR (" + name + "): Missing reference to SpriteRenderer!");

    }

    private void Start()
    {
        playerCursor.onHit.AddListener(FigureHit);
    }
    
    void Update()
    {
        // handles UnityEvent when FigureUI is clicked
        /*
        if (Input.GetMouseButtonDown(0) && isHoveredOver == true)
        {
            
            ChangeSprite("clickedSprite");
            onClicked.Invoke(this);
            //Increment figureOrder counter by 1
            figureOrder++;
        }
        */
        

        /*
        // handles UnityEvent when mouse leaves this FigureUI
        if (isHoveredOver == false && isHoveredOverOnPrevFrame == true)
        {
            if(!isSelected)
                ChangeSprite("unclickedSprite");

            onCursorExit.Invoke(this);
        }
        

        isHoveredOverOnPrevFrame = isHoveredOver;

        // reset state for next frame
        isHoveredOver = false;
        */
    }


    private void FigureHit(RaycastHit hit)
    {
        
        if (hit.transform.name == name)
        {
            // handles UnityEvent when mouse enters this FigureUI
            /*
            if (isHoveredOver == false && isHoveredOverOnPrevFrame == false)
            {
                if(!isSelected)
                    ChangeSprite("hoverSprite");
                onCursorEnter.Invoke(this);
            }
            */

            isHoveredOver = true;
        }
        
    }

    public void Select()
    {
        if(isSelected == false)
        {
            this.isSelected = true;
            //ChangeSprite("unclickedSprite");
            onSelect.Invoke(this);
        }
        
    }

    public void Deselect()
    {
        if(isSelected == true)
        {
            this.isSelected = false;
            //ChangeSprite("unclickedSprite");
            onDeselect.Invoke(this);
        }
    }

    #region UI Functions
    
    public void ChangeSprite(string state)
    {
        /*
        switch (state)
        {
            case "previousHighlight":
                spriteRenderer.sprite = clickedSprite;
                break;
            case "currentHighlight":
                spriteRenderer.sprite = hoverSprite;
                break;
            case "nextHighlight":
                spriteRenderer.sprite = unclickedSprite;
                break;
            default:
                //empty sprite
                //spriteRenderer.sprite = unclickedSprite;
                break;
        }
        */
        
        switch (state)
        {
            case "unclickedSprite":
                spriteRenderer.sprite = unclickedSprite;
                break;
            case "hoverSprite":
                spriteRenderer.sprite = hoverSprite;
                break;
            case "clickedSprite":
                spriteRenderer.sprite = clickedSprite;
                break;
            default:
                spriteRenderer.sprite = unclickedSprite;
                break;
        }
    }
    
    #endregion





}
