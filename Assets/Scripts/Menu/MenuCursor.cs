using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCursor : MonoBehaviour
{
    private void Awake()
    {
        Cursor.visible= false;
    }
    private void Update()
    {
        gameObject.transform.position = Input.mousePosition;
    }

}
