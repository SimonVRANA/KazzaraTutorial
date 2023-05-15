using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class is used to manage the visibility of the explanation texts.
/// It also stops time whan the texts are visible
/// </summary>
public class ExplanationTextVisibility : MonoBehaviour
{
    /// <summary>
    /// Link to the background image.
    /// </summary>
    [Tooltip("Link to the Background.")]
    public GameObject background;

    /// <summary>
    /// Link to the explanation text manager.
    /// </summary>
    [Tooltip("Link to the explanation text manager.")]
    public GameObject explanationTextManagerGameObject;

    /// <summary>
    /// Link to the "Next text" button.
    /// </summary>
    [Tooltip("Link to the \"Next text\" button.")]
    public GameObject nextTextButton;

    /// <summary>
    /// Link to the "Show explanations" button.
    /// </summary>
    [Tooltip("Link to the \"Show explanations\" button.")]
    public GameObject showExplanationsButton;

    /// <summary>
    /// Link to the game manager.
    /// </summary>
    public GameManager gameManager;

    /// <summary>
    /// Link to the ExplanationTextManager of explanationTextManagerGameObject.
    /// </summary>
    private ExplanationTextManager explanationTextManager;


    // Start is called before the first frame update
    void Start()
    {
        explanationTextManager = explanationTextManagerGameObject.GetComponent<ExplanationTextManager>();
        explanationTextManager.SetMenuVisibilityManager(this);
        Open();
    }

    /// <summary>
    /// Oppens the menu: display the elements and reset them if needed.
    /// </summary>
    public void Open()
    {
        SetMenuVisibility(true);
        Time.timeScale= 0;
    }

    /// <summary>
    /// Oppens the menu: Hides the elements and display the "Show explanations" button.
    /// </summary>
    public void Close()
    {
        SetMenuVisibility(false);
        Time.timeScale = 1;
        gameManager.Initialize();
    }


    /// <summary>
    /// Sets the menu visibility and resets the menu elements if needed.
    /// </summary>
    /// <param name="pDisplay">New menu visibility</param>
    private void SetMenuVisibility(bool pDisplay)
    {
        background.SetActive(pDisplay);
        explanationTextManagerGameObject.SetActive(pDisplay);
        if(pDisplay)
        {
            explanationTextManager.Reset();
        }
        nextTextButton.SetActive(pDisplay);
        showExplanationsButton.SetActive(!pDisplay);
    }
}
