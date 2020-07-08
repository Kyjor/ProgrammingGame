using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public static class IDManager {

    private static Dictionary<int , ProgrammableObject> container;

    public static int maxPOInGame = 10000;

    /// <summary>
    /// Sets an ID to the Programmable object.
    /// </summary>
    public static void setID( ProgrammableObject PO)
    {
        if (container == null)
            container = new Dictionary<int, ProgrammableObject>();

        if (PO.id != 0 && !container.ContainsKey(PO.id))
        {
            container.Add(PO.id, PO);
            return;
        }

        int id;
        do
        {
            if (container.Count == maxPOInGame)
            {
                Debug.Log("Object Exceeds max Object Count.");
                return;
            }
            id = Random.Range(1, maxPOInGame + 1);
        } while (container.ContainsKey(id));

        PO.id = id;
        container.Add(PO.id, PO);
    }

    /// <summary>
    /// Gets ID from the object container,
    /// returns null if ID doesnt match.
    /// </summary>
    public static ProgrammableObject getProgrammableObjectFromID(int id)
    {
        if (container == null || !container.ContainsKey(id))
            return null;

        return container[id];
        
    }
    public static void removeObjectWithID(int id)
    {
        container.Remove(id);
    }
}
