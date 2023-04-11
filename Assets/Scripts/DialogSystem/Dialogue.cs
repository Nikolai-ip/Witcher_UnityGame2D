using System.Collections;
using UnityEngine;

[System.Serializable]
public class Dialogue : IEnumerable
{
    [TextArea(5, 10)]
    [SerializeField] private string[] _conversations;

    [SerializeField] private string _speakerName;
    public string SpeakerName => _speakerName;

    public IEnumerator GetEnumerator()
    {
        return _conversations.GetEnumerator();
    }
}