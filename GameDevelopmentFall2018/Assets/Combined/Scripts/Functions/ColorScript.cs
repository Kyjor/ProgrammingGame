using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorScript {

    private static ColorScript _instance;
    public static ColorScript instance
    {
        get
        {
            if (_instance == null)
                _instance = new ColorScript();
            return _instance;
        }
        set { }
    }

    public IEnumerator ChangeColor(float r, float g, float b, float a, ProgrammableObject PO)
    {
        if (a < 0)
            a = 0;
        if (r < 0)
            r = 0;
        if (g < 0)
            g = 0;
        if (b < 0)
            b = 0;
        if (a > 1)
            a = 1;
        if (r > 1)
            r = 1;
        if (g > 1)
            g = 1;
        if (b > 1)
            b = 1;
        PO.gameObject.GetComponent<Renderer>().material.color = new Color(r, g, b, a);

        if (PO.scriptQueue.Count > 0)
        {
			PO.fc.startCoroutine(PO.scriptQueue.Dequeue());
        }
        yield return null;
    }

    /*
     Color Cheat Sheet
     black = 0,0,0,1
     blue = 0,0,1,1
     clear = 0,0,0,0
     cyan = 0,1,1,1
     gray = 0.5,0.5,0.5,1
     green = 0,1,0,1
     magenta = 1,0,1,1
     red = 1,0,0,1
     white = 1,1,1,1
     yellow = 1,0.92,0.016
     */
}
