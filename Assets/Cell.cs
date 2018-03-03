using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour {
    public int neighborsAlive;
    public bool m_alive = true;

    // Upon mouse input change the state
    private void OnMouseDown()
    {
        m_alive = !m_alive;
        ChangeColor();
    }

    public void ChangeColor()
    {
        if(m_alive)
            gameObject.GetComponent<Renderer>().material.color = Color.white;
        else
            gameObject.GetComponent<Renderer>().material.color = Color.black;
    }
}
