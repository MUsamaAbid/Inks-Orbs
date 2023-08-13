using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using OcularInk.Utils;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    public List<CanvasController> canvases = new List<CanvasController>();

    public void RegisterCanvas(CanvasController canvas)
    {
        canvases.Add(canvas);
    }

    public void UnregisterCanvas(CanvasController canvas)
    {
        if (canvas == null)
        {
            Debug.Log("Is null, skipping");
            return;
        }

        if (!canvases.Contains(canvas))
        {
            Debug.Log("Does not include, skipping");
            return;
        }
        
        try
        {
            canvases.Remove(canvas);
        }
        catch (Exception e)
        {
            Debug.LogWarning(e.Message);
        }
    }

    public T GetCanvas<T>() where T : CanvasController
    {
        return canvases.OfType<T>().First();
    }
}