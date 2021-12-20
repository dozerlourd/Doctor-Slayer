using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObserverSystem : SingletonObject<ObserverSystem>
{
    #region Variable

    public delegate void Reaction();
    Dictionary<string, List<Reaction>> _listeners = new Dictionary<string, List<Reaction>>();

    #endregion

    #region Implementation Place

    /// <summary>
    /// ������ �߰� <br/>
    /// �����ʴ� �ش� �̺�Ʈ�� �Ͼ�� �� ������ ���׼��� ���� <br/><br/>
    /// EventManager.Instance.AddListener(string _eventType, Reaction _reaction);
    /// </summary>
    /// <param name="_eventType"> ���׼��� ����ų �̺�Ʈ Ÿ�� </param>
    /// <param name="_reaction"> �̺�Ʈ �߻� �� �����ų ���׼� </param>
    public void AddListener(string _eventType, Reaction _reaction)
    {
        if(!_listeners.ContainsKey(_eventType))
        {
            List<Reaction> reactions = new List<Reaction>();
            _listeners.Add(_eventType, reactions);
        }

        _listeners[_eventType].Add(_reaction);
    }

    /// <summary>
    /// �̺�Ʈ�� �߻����� ��� �̺�Ʈ�� �����ϴ� �����ʵ鿡�� �ݹ� �޼����� ���� <br/><br/>
    /// 
    /// EventManager.Instance.PostNofication(string _eventType);
    /// </summary>
    /// <param name="_eventType"> ������ �̺�Ʈ </param>
    public void NotifyObservers(string _eventType)
    {
        if(!_listeners.ContainsKey(_eventType))
            return;

        foreach (Reaction _reaction in _listeners[_eventType]) { _reaction(); }
    }

    /// <summary> ������ ��� �ʱ�ȭ </summary>
    public void ClearListeners() => _listeners.Clear();

    #endregion
}
