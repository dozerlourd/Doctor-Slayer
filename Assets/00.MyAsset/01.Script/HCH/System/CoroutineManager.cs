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
    /// 코루틴 리액션 추가 <br/>
    /// 추가된 코루틴은 해당 이벤트가 일어났을 때 CoroutineManager 객체에서 실행됨 <br/><br/>
    /// EventManager.Instance.AddListener(string _eventType, Reaction _reaction);
    /// </summary>
    /// <param name="_eventType"> 리액션을 일으킬 이벤트 타입 </param>
    /// <param name="_reaction"> 이벤트 발생 시 실행시킬 코루틴 리액션 </param>
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
    /// 이벤트가 발생했을 경우 이벤트에 부합하는 코루틴을 CoroutineManager 객체에서 실행 <br/><br/>
    /// 
    /// EventManager.Instance.PostNofication(string _eventType);
    /// </summary>
    /// <param name="_eventType"> 전달할 이벤트 </param>
    public void NotifyObservers(string _eventType)
    {
        if (!_listeners.ContainsKey(_eventType))
            return;

        foreach (IEnumerator _reaction in _listeners[_eventType]) { StartCoroutine(_reaction); }
    }

    /// <summary> 리스너 목록 초기화 </summary>
    public void ClearListeners() => _listeners.Clear();

    #endregion

    #endregion
}
