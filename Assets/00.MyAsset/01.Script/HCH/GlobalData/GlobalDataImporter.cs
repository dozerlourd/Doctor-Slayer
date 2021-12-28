using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class GlobalDataImporter : MonoBehaviour
{
    [SerializeField]
    GoogleSheetData googleSheetData;

    [System.Serializable]
    public class GoogleSheetData
    {
        [Header("패시브 아이템")]
        public TextAsset passiveItem;

        [Header("몬스터 정보")]
        public TextAsset monsterInfo;
    }

    void Awake()
    {
        GlobalState.passiveItemList = JsonConvert.DeserializeObject<List<Excel_PassiveItem>>(googleSheetData.passiveItem.ToString());
        GlobalState.monsterInfoList = JsonConvert.DeserializeObject<List<Excel_MonsterInfo>>(googleSheetData.monsterInfo.ToString());
    }
}
