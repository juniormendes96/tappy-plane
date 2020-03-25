using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public static class ExtensionMethods {

    public static void InstantiateObjects(this List<GameObject> gameObjectsList, int quantity, GameObject objectType) {
        for (var i = 0; i < quantity; i++) {
            var tempObject = GameObject.Instantiate(objectType) as GameObject;
            tempObject.SetActive(false);
            gameObjectsList.Add(tempObject); 
        }
    }

    public static GameObject ReturnActive(this List<GameObject> gameObjectsList) {
        return gameObjectsList.Where(w => w.activeSelf == false).First();
    }

    public static void FindAndSetActive(string name, System.Type type, bool status) {
        var objects = Resources.FindObjectsOfTypeAll(type);

        foreach (GameObject obj in objects) {
            if (obj.name == name) {
                obj.SetActive(status);
            }
        }
    } 

    public static T FindChildObject<T> (this GameObject gameObject, string objectName = "") {
        var type = typeof(T);
        object ret;

        if (objectName != string.Empty) {
            ret = gameObject.GetComponentsInChildren(type).Where(w => w.name == objectName).First();
        } else {
            ret = gameObject.GetComponentsInChildren(type).First();
        }

        return (T) Convert.ChangeType(ret, type);
    }
}
