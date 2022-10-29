using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonEvent : MonoBehaviour
{
	/// <summary>名前入力フィールド。</summary>
	[SerializeField] InputField InputFieldName;

	/// <summary>HP 入力フィールド。</summary>
	[SerializeField] InputField InputFieldHP;

	/// <summary>攻撃力入力フィールド。</summary>
	[SerializeField] InputField InputFieldAttack;

	/// <summary>お金入力フィールド。</summary>
	[SerializeField] InputField InputFieldMoney;

	/// <summary>
	/// 保存ボタンをクリックしたときの処理。
	/// </summary>
	public void OnClickSave()
	{
		// 各値を指定したキーに保存します
		PlayerPrefs.SetString("Name", InputFieldName.text);
		PlayerPrefs.SetString("HP", InputFieldHP.text);
		PlayerPrefs.SetString("Attack", InputFieldAttack.text);
		PlayerPrefs.SetString("Money", InputFieldMoney.text);

		// 設定したデータを確定して保存します
		PlayerPrefs.Save();

		Debug.Log("保存しました。");
	}

	/// <summary>
	/// 読み込みボタンをクリックしたときの処理。
	/// </summary>
	public void OnClickLoad()
	{
		// 指定したキーから値を読み込みます
		InputFieldName.text = PlayerPrefs.GetString("Name");
		InputFieldHP.text = PlayerPrefs.GetString("HP");
		InputFieldAttack.text = PlayerPrefs.GetString("Attack");
		InputFieldMoney.text = PlayerPrefs.GetString("Money");

		Debug.Log("読み込みました。");
	}


	/// <summary>セーブデータクラス。</summary>
	public class SaveData
	{
		public List<Character> Characters;
		public long Money;
	}

	/// <summary>１キャラクターの情報。</summary>
	[Serializable]
	public class Character
	{
		public string Name;
		public int HP;
		public int Attack;

		public override string ToString() => $"{Name} HP:{HP} 攻撃力:{Attack}";
	}

	/// <summary>
	/// JSON ボタンをクリックしたときの処理。
	/// </summary>
	public void OnClickJson()
	{
		// 保存するデータ作成
		var saveData = new SaveData
		{
			Money = 10000,
			Characters = new List<Character>
			{
				new Character { Name= "レイシア", HP = 50, Attack = 40, },
				new Character { Name= "アイリ", HP = 56, Attack = 27, },
				new Character { Name= "ニール", HP = 72, Attack = 36, },
				new Character { Name= "トリー", HP = 61, Attack = 30, },
			},
		};

		// オブジェクトを JSON 文字列にシリアライズします
		var saveJson = JsonUtility.ToJson(saveData);

		// データを保存します
		PlayerPrefs.SetString("SaveData", saveJson);
		PlayerPrefs.Save();

		// データを読み込みます
		var loadJson = PlayerPrefs.GetString("SaveData");

		// JSON 文字列からオブジェクトにデシリアライズします
		var newData = JsonUtility.FromJson<SaveData>(loadJson);

		// ログに表示します
		Debug.Log($"Money:{newData.Money}");
		foreach (var chara in newData.Characters)
		{
			Debug.Log($"{chara.Name} HP:{chara.HP} 攻撃力:{chara.Attack}");
		}
	}
}
