using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles user controls
/// </summary>
public class GameManager : MonoBehaviour
{
    [Tooltip("Material of a selected person")]
    public Material SelectedMaterial;

    // Update is called once per frame
    void Update()
    {
        LeftClickListener();
    }

    /// <summary>
    /// Listens to the left mouse click being lifted
    /// </summary>
    private void LeftClickListener()
    {
        // left click up
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider != null)
            {
                LeftClickCollision(hit);
            }
        }
    }

    /// <summary>
    /// Decides what to do when an object is clicked
    /// </summary>
    /// <param name="hit"></param>
    private void LeftClickCollision(RaycastHit2D hit)
    {

        switch (hit.collider.gameObject.name)
        {
            case "Customer":
            case "Customer(Clone)":
                hit.collider.GetComponent<Customer>().OnClick(SelectedMaterial);
                break;

            case "Employee":
            case "Employee(Clone)":

                // change to the color of selection
                hit.collider.GetComponent<Employee>().OnClick(SelectedMaterial);

                break;

            case "Tables":
                break;
        }
    }
}
