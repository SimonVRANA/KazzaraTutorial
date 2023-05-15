using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is used to select the currently display text in the explanation menu.
/// I know Unity added a class for elements that doesn't need the Unity life cycle (Awake, Start, Update,...) but I'm not familiar with it so I used a MonoBehaviour.
/// </summary>
public class ExplanationTextManager : MonoBehaviour
{
    /// <summary>
    /// List of all the explanations.
    /// Each explanation represents a step in the tutorial.
    /// </summary>
    public List<GameObject> explanationsList;

    /// <summary>
    /// Current explanation index. Can be seen as the current step in the tutorial.
    /// </summary>
    private int currentExplanationInbdex= 0;

    /// <summary>
    /// Link to the ExplanationTextVisibility component.
    /// </summary>
    private ExplanationTextVisibility menuVisibilityManager;

    /// <summary>
    /// Sets the ExplanationTextVisibility.
    /// </summary>
    /// <param name="pMenuVisibilityManager">The new ExplanationTextVisibility.</param>
    public void SetMenuVisibilityManager(ExplanationTextVisibility pMenuVisibilityManager)
    {
        menuVisibilityManager = pMenuVisibilityManager;
    }

    /// <summary>
    /// Resets to the default step in the tutorial.
    /// </summary>
    public void Reset()
    {
        currentExplanationInbdex= 0;
        UpdateExplanationsVisibility();
    }

    public void NextExplanation()
    {
        currentExplanationInbdex++;
        if(currentExplanationInbdex >= explanationsList.Count)
        {
            menuVisibilityManager.Close();
        }
        else
        {
            UpdateExplanationsVisibility();
        }
    }

    /// <summary>
    /// Updates the visibility accordint to the current index.
    /// </summary>
    private void UpdateExplanationsVisibility()
    {
        DeactivateAllExplanations();
        ActivateExplanation(currentExplanationInbdex);
    }

    /// <summary>
    /// Deactivates all explanations.
    /// </summary>
    private void DeactivateAllExplanations()
    {
        for(int i=0; i<explanationsList.Count; i++)
        {
            explanationsList[i].gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Activates an explanation.
    /// </summary>
    /// <param name="lExplanationInbdex">Index of the explanation to activate.</param>
    private void ActivateExplanation(int lExplanationInbdex)
    {
        if(lExplanationInbdex >=0
           && lExplanationInbdex < explanationsList.Count)
        {
            explanationsList[lExplanationInbdex].gameObject.SetActive(true);
        }
    }
}
