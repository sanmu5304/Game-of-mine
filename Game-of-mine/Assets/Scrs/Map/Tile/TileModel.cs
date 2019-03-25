using UnityEngine;
using System.Collections;

public class TileModel : MonoBehaviour
{
    private string _key = "default";
    public string key{
        get{
            return _key;
        }
        set{
            this._key = value;
        }
    }
}
