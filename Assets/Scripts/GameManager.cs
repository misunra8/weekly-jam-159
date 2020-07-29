using System;
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

    [Tooltip("Material of a deselected person")]
    public Material DeselectedMaterial;

    /// <summary>
    /// The selected person
    /// </summary>
    private Person selectedUnit;

    // Update is called once per frame
    void Update()
    {
        LeftClickListener();
        SpaceBarListener();
    }

    /// <summary>
    /// Listens to the space bar being lifted
    /// </summary>
    private void SpaceBarListener()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            Deselect();
        }
    }

    /// <summary>
    /// Listens to the left mouse click being lifted
    /// </summary>
    private void LeftClickListener()
    {
        // left click up 
        if (Input.GetMouseButtonUp(0))
        {
            // orders the selected unit to move
            if (selectedUnit != null)
            {
                MovePerson();
                Deselect();
                return;
            }

            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            // collision detection
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
        if (selectedUnit == null)
            switch (hit.collider.gameObject.name)
            {
                case "Customer":
                case "Customer(Clone)":
                case "Employee":
                case "Employee(Clone)":
                    // select the person
                    Select(hit.collider.GetComponent<Person>());

                    break;

                case "Tables":
                case "Wall":
                case "Machines":
                    break;
            }
        
        // selected unit is told to go to a collidable
        else
            switch (hit.collider.gameObject.name)
            {
                case "Wall":
                    break;

                default:
                    MovePerson();
                    Deselect();
                    break;
            }
    }

    /// <summary>
    /// Deselects the current unit
    /// </summary>
    private void Deselect()
    {
        if (selectedUnit != null)
        {
            selectedUnit.Deselect(DeselectedMaterial);
            selectedUnit = null;
        }
    }

    /// <summary>
    /// Selects a person
    /// </summary>
    /// <param name="person"></param>
    private void Select(Person person)
    {
        selectedUnit = person.Select(SelectedMaterial);
    }

    /// <summary>
    /// Orders the person to go to cursor location
    /// </summary>
    /// <param name="point">2d position on the screen</param>
    private void MovePerson()
    {
        if (selectedUnit == null) throw new Exception("No unit selected to move");

        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPosition.z = 0;
        selectedUnit.MoveTo(worldPosition);
    }
}
