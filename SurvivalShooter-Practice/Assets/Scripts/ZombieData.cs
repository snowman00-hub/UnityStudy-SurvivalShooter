using UnityEngine;

[CreateAssetMenu(fileName = "ZombieData", menuName = "Scriptable Objects/ZombieData")]
public class ZombieData : ScriptableObject
{
    public float maxHp;
    public float damage;
    public float speed;
    public float score;
    public float respawnInterval;
}
