using Kuhpik;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersNames : Singleton<PlayersNames>
{
	[SerializeField]
	List<string> namesList = new List<string>() { "Valery L.", "ColonelKotya","Dolphin1990", "disperion", "Vusar", "Vic_1", "Dimarik-NV", "ScArFaCe"
		,"Lost_mind","-Lapin-","xS > DRIFT","Doctor Alibi","indieCurator","Tuna Mav","baty131","dhueso_EC"
		,"AriElf","Hade_Vlot","User_1999120","User_1782838","Idler","mr.Jewbocabra","NastyTwitch","Neo3D"
		,"Epsonchik","MILFer","Sheldon Cooper","Le0nard0","SaInT","Sanitizer","=Chieftan=","Meune"
		,"Anti-Ded","Komatoz!","Dorfer Games","Durilka_14","Gambit_1990","Feuner_2004","Temuch_5000","Nick"
		,"Zur","Shooter","_Dark_","Desertir","ArHimoND","-=-Jojo-=-","+Fur Idioten+","Jeleznodorojnik"
		,"Sergey Lebedev","FluffY","Dionis","KryuK","Mr.Fact","DIMARIK-NV","Cerberus","MariaIvanna"
		,"URBAN","Andrei","Bad_Angel","Rauf","Puffff","kajirn","AnalniyKinjal","Limiter","Fungineerka","Inferno315"};

	public string GetName(bool Remove = true)
	{
		int randomIndex = Random.Range(0, namesList.Count);
		string Result = namesList[randomIndex];
		if (Remove)
			namesList.RemoveAt(randomIndex);
		return Result;
	}
}
