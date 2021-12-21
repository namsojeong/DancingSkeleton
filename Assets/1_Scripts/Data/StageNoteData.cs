using UnityEngine;

[System.Serializable]
public class StageNoteData : MonoBehaviour
{
    public AudioClip stageMusicClip = null;
    public string danceName = "Skeleton";
    public int stageNum = 0;
    public Vector2[] notePos = null;
    public float[] noteTime = null;
    public string[] noteType = null;
}
