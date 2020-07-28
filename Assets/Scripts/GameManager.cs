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
                switch (hit.collider.gameObject.name)
                {
                    case "Customer":
                    case "Employee":
                        
                        hit.collider.GetComponentInChildren<SpriteRenderer>().material = SelectedMaterial;
                        
                        break;

                    case "Tables":
                        break;
                }
            }
        }
    }
}
