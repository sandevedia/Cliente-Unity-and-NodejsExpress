using UnityEngine;
using System.Collections.Generic;

    /*
        La clase tiene un método 'FromJson' genérico que retorna una lista de cualquier clase que vayamos a hacer.       
        La parte clave es la clase Wrapper que tiene una lista de resultados que almacena los valores.
     */
public static class JsonHelper
{    
    public static List<T> FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.result;
    }

    [System.Serializable]
    private class Wrapper<T>
    {
        public List<T> result;
    }
}