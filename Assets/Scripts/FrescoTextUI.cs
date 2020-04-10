using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrescoTextUI : MonoBehaviour
{
    [SerializeField] private FigureUI connectedFigure;
    [SerializeField] private GameObject textPanel;

    private void Awake()
    {
        if (!connectedFigure)
            Debug.LogError("ERROR (" + name + "): Missing reference to connectedFigure (FigureUI)!");
        if (textPanel)
            textPanel.SetActive(false);
    }

    private void Start()
    {
        connectedFigure.onSelect.AddListener(Show);
        connectedFigure.onDeselect.AddListener(Hide);
    }

    private void Show(FigureUI ui)
    {
        if (ui == connectedFigure)
            textPanel.SetActive(true);
    }

    private void Hide(FigureUI ui)
    {
        if (ui == connectedFigure)
            textPanel.SetActive(false);
    }
}
