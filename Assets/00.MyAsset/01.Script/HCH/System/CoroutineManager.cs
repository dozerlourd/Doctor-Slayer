using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineManager : SingletonObject<CoroutineManager>
{
    #region Variable

    Dictionary<string, List<IEnumerator>> _listeners = new Dictionary<string, List<IEnumerator>>();

    #endregion

    #region Implementation Place

    #region Observer

    /// <summary>
    /// �ڷ�ƾ ���׼� �߰� <br/>
    /// �߰��� �ڷ�ƾ�� �ش� �̺�Ʈ�� �Ͼ�� �� CoroutineManager ��ü���� ����� <br/><br/>
    /// EventManager.Instance.AddListener(string _eventType, Reaction _reaction);
    /// </summary>
    /// <param name="_eventType"> ���׼��� ����ų �̺�Ʈ Ÿ�� </param>
    /// <param name="_reaction"> �̺�Ʈ �߻� �� �����ų �ڷ�ƾ ���׼� </param>
    public void AddListener(string _eventType, IEnumerator _reaction)
    {
        if (!_listeners.ContainsKey(_eventType))
        {
            List<IEnumerator> reactions = new List<IEnumerator>();
            _listeners.Add(_eventType, reactions);
        }

        _listeners[_eventType].Add(_reaction);
    }

    /// <summary>
    /// �̺�Ʈ�� �߻����� ��� �̺�Ʈ�� �����ϴ� �ڷ�ƾ�� CoroutineManager ��ü���� ���� <br/><br/>
    /// 
    /// EventManager.Instance.PostNofication(string _eventType);
    /// </summary>
    /// <param name="_eventType"> ������ �̺�Ʈ </param>
    public void NotifyObservers(string _eventType)
    {
        if (!_listeners.ContainsKey(_eventType))
            return;

        foreach (IEnumerator _reaction in _listeners[_eventType]) { StartCoroutine(_reaction); }
    }

    /// <summary> ������ ��� �ʱ�ȭ </summary>
    public void ClearListeners() => _listeners.Clear();

    #endregion

    #endregion
}
