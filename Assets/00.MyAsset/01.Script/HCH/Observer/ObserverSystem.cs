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
    /// 리스너 추가 <br/>
    /// 리스너는 해당 이벤트가 일어났을 때 지정된 리액션을 실행 <br/><br/>
    /// EventManager.Instance.AddListener(string _eventType, Reaction _reaction);
    /// </summary>
    /// <param name="_eventType"> 리액션을 일으킬 이벤트 타입 </param>
    /// <param name="_reaction"> 이벤트 발생 시 실행시킬 리액션 </param>
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
    /// 이벤트가 발생했을 경우 이벤트에 부합하는 리스너들에게 콜백 메세지를 보냄 <br/><br/>
    /// 
    /// EventManager.Instance.PostNofication(string _eventType);
    /// </summary>
    /// <param name="_eventType"> 전달할 이벤트 </param>
    public void NotifyObservers(string _eventType)
    {
        if(!_listeners.ContainsKey(_eventType))
            return;

        foreach (Reaction _reaction in _listeners[_eventType]) { _reaction(); }
    }

    /// <summary> 리스너 목록 초기화 </summary>
    public void ClearListeners() => _listeners.Clear();

    #endregion
}
