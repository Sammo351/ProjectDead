using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class World : MonoBehaviour {
    public static Enemies enemies = (Enemies) ~0;
    public Enemies _enemies;
    public List<EnemyGameobject> _gameobjects;
    public static List<EnemyGameobject> gameobjects;
    [SerializeField]
    public static Dictionary<Enemies, GameObject> meobjects;
    void OnValidate () {
        enemies = _enemies;
        gameobjects = _gameobjects;
    }
    void Start () { }

    // Update is called once per frame
    void Update () {

    }
    void Reset () {

    }

    [System.Serializable]
    public struct EnemyGameobject {
        public Enemies enemy;
        public GameObject gameobject;
    }
    public static GameObject GetEnemyObject (Enemies enemy) {
        GameObject[] array = gameobjects.Where ((x) => x.enemy == enemy).Select ((x) => x.gameobject).ToArray ();
        if (array != null && array.Length > 0) {
            return array[0];
        }
        return null;
    }
}

[System.Flags]
public enum Enemies { Basic = 0x1, Boomer = 0x2, Splitter = 0x4 }
//public static Dictionary<string, Entity> nemies = new Dictionary<string, Entity>();