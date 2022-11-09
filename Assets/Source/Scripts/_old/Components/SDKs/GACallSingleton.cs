//using GameAnalyticsSDK; //раскоментить, если используется GA
using Kuhpik;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//                        Этот скрипт надо повесить на объект на сцене в корне
//                        у AppMetrika тоже есть свой префаб, который надо вешать на сцену - не забудьте

//                        в PlayerData надо добавить следующие строчки
/*
public DateTime LevelTime; // тут будет отмечаться время начала уровня
public int level //Это будет счётчик уровней сколько игрок прошёл (не путать с индексом уровней, если у вас есть префабы уровней или ресурсы)
*/

//                        LEVEL WIN эти строчки добавляем в код успешного прохода уровня
/*
int Seconds = (int)((DateTime.Now - player.LevelTime).TotalSeconds);
Amplitude.Instance.logEvent("level_win", new Dictionary<string, object> { { "level", player.Level } , { "time", Seconds } });
GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, player.Level.ToString());
Amplitude amplitude = Amplitude.getInstance();
amplitude.setUserProperty("current_soft", player.Money); // если у вас нет внутриигровой валюты, то эту строчку удалить

//APPMETRICA
var @params = new Dictionary<string, object>()
{
    { "level_count", player.level},
    { "time", Seconds},
    { "result", "win" },
};
AppMetrica.Instance.ReportEvent("level_finish", @params);
AppMetrica.Instance.SendEventsBuffer();
*/

//                        LEVEL FAIL эти строчки надо добавить, если уровень провален
/*
int Seconds = (int)((DateTime.Now - player.LevelTime).TotalSeconds);
GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, player.Level.ToString());
Amplitude.Instance.logEvent("level_fail", new Dictionary<string, object> { { "level", player.Level }  , { "time", Seconds } });
Amplitude amplitude = Amplitude.getInstance();
amplitude.setUserProperty("current_soft", player.Money); // если у вас нет внутриигровой валюты, то эту строчку удалить

//APPMETRICA
var @params = new Dictionary<string, object>()
{
    { "level_count", player.level},
    { "time", Seconds},
    { "result", "lose" },
};
AppMetrica.Instance.ReportEvent("level_finish", @params);
AppMetrica.Instance.SendEventsBuffer();
*/

//                        START LEVEL эти строчки, когда уровень стартовал
/*
player.LevelTime = DateTime.Now;
GameAnalytics.NewDesignEvent("level start");
GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, player.Level.ToString());
Amplitude.Instance.logEvent("level start", new Dictionary<string, object> { { "level", player.Level } });
//Аппметрика
var @params = new Dictionary<string, object>() {     { "level_count", player.Level }    };
AppMetrica.Instance.ReportEvent("level_start", @params);
AppMetrica.Instance.SendEventsBuffer();
*/



//                        CUSTOM EVENT для амплитуды, если в каком-то месте надо послать несколько параметров
/*
Amplitude.Instance.logEvent("Event_Name", new Dictionary<string, object> { {"Param_1", game.CrashCounter.ToString() }, {"Param_2",player.Levels } });
 */

//                        CUSTOM EVENT для AppMetrica большая конструкция, лишнее из словаря можно удалить. Последняя строчка SendEventsBuffer ОБЯЗАТЕЛЬНА
/*
    var @params = new Dictionary<string, object>()
        {
            { "level_number", player.level + 1 },
            { "level_name", levelName },
            { "level_count", player.levelsPlayed },
            { "level_diff", game.characters.Length - 1 },
            { "level_loop", game.levelLoop },
            { "level_random", 0},
            { "level_type", game.levelType },
            { "result", game.isVictory ? "win" : "lose" },
            { "time", DateTime.Now.Subtract(game.gameStartTime).TotalSeconds},
            { "progress", progress }
        };
    AppMetrica.Instance.ReportEvent("level_finish", @params);
    AppMetrica.Instance.SendEventsBuffer();
*/
public class GACallSingleton : DontDestroySingleton<GACallSingleton>
{
    [SerializeField] string AmplitudeApiKey; //Сюда в инспекторе вписать ключ, который получаем от Саши Нечаева для амплитуды
    
    void Start()
    {
        //                        Раскоментировать для GA
        /* 
        GameAnalytics.Initialize();
        GameAnalytics.NewDesignEvent("game start");
        */

        //                        Раскоментировать для АМПЛИТУДЫ
        /*
        Amplitude amplitude = Amplitude.getInstance();
        amplitude.logging = true;
        amplitude.trackSessionEvents(true);
        amplitude.init(AmplitudeApiKey);

        amplitude.logEvent("game start");

        string Date = PlayerPrefs.GetString("RegDate", "");
        int SessionCounter = PlayerPrefs.GetInt("SessionCounter", 0);

        if (string.IsNullOrEmpty(Date))
        {
            Date = System.DateTime.Now.ToString("yyyy-MM-dd");
            string RealDate = System.DateTime.Now.ToString("yyyy.MMdd");
            amplitude.setUserProperty("reg_day", RealDate);
        }
        SessionCounter++;
        amplitude.setUserProperty("session_id", SessionCounter);
        amplitude.setUserProperty("register_days", ((int)System.DateTime.Now.Subtract(DateTime.Parse(Date)).TotalDays));

        PlayerPrefs.SetString("RegDate", Date);
        PlayerPrefs.SetInt("SessionCounter", SessionCounter);
        */
    }
}
