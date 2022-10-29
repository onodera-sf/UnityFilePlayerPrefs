using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonEvent : MonoBehaviour
{
	/// <summary>���O���̓t�B�[���h�B</summary>
	[SerializeField] InputField InputFieldName;

	/// <summary>HP ���̓t�B�[���h�B</summary>
	[SerializeField] InputField InputFieldHP;

	/// <summary>�U���͓��̓t�B�[���h�B</summary>
	[SerializeField] InputField InputFieldAttack;

	/// <summary>�������̓t�B�[���h�B</summary>
	[SerializeField] InputField InputFieldMoney;

	/// <summary>
	/// �ۑ��{�^�����N���b�N�����Ƃ��̏����B
	/// </summary>
	public void OnClickSave()
	{
		// �e�l���w�肵���L�[�ɕۑ����܂�
		PlayerPrefs.SetString("Name", InputFieldName.text);
		PlayerPrefs.SetString("HP", InputFieldHP.text);
		PlayerPrefs.SetString("Attack", InputFieldAttack.text);
		PlayerPrefs.SetString("Money", InputFieldMoney.text);

		// �ݒ肵���f�[�^���m�肵�ĕۑ����܂�
		PlayerPrefs.Save();

		Debug.Log("�ۑ����܂����B");
	}

	/// <summary>
	/// �ǂݍ��݃{�^�����N���b�N�����Ƃ��̏����B
	/// </summary>
	public void OnClickLoad()
	{
		// �w�肵���L�[����l��ǂݍ��݂܂�
		InputFieldName.text = PlayerPrefs.GetString("Name");
		InputFieldHP.text = PlayerPrefs.GetString("HP");
		InputFieldAttack.text = PlayerPrefs.GetString("Attack");
		InputFieldMoney.text = PlayerPrefs.GetString("Money");

		Debug.Log("�ǂݍ��݂܂����B");
	}


	/// <summary>�Z�[�u�f�[�^�N���X�B</summary>
	public class SaveData
	{
		public List<Character> Characters;
		public long Money;
	}

	/// <summary>�P�L�����N�^�[�̏��B</summary>
	[Serializable]
	public class Character
	{
		public string Name;
		public int HP;
		public int Attack;

		public override string ToString() => $"{Name} HP:{HP} �U����:{Attack}";
	}

	/// <summary>
	/// JSON �{�^�����N���b�N�����Ƃ��̏����B
	/// </summary>
	public void OnClickJson()
	{
		// �ۑ�����f�[�^�쐬
		var saveData = new SaveData
		{
			Money = 10000,
			Characters = new List<Character>
			{
				new Character { Name= "���C�V�A", HP = 50, Attack = 40, },
				new Character { Name= "�A�C��", HP = 56, Attack = 27, },
				new Character { Name= "�j�[��", HP = 72, Attack = 36, },
				new Character { Name= "�g���[", HP = 61, Attack = 30, },
			},
		};

		// �I�u�W�F�N�g�� JSON ������ɃV���A���C�Y���܂�
		var saveJson = JsonUtility.ToJson(saveData);

		// �f�[�^��ۑ����܂�
		PlayerPrefs.SetString("SaveData", saveJson);
		PlayerPrefs.Save();

		// �f�[�^��ǂݍ��݂܂�
		var loadJson = PlayerPrefs.GetString("SaveData");

		// JSON �����񂩂�I�u�W�F�N�g�Ƀf�V���A���C�Y���܂�
		var newData = JsonUtility.FromJson<SaveData>(loadJson);

		// ���O�ɕ\�����܂�
		Debug.Log($"Money:{newData.Money}");
		foreach (var chara in newData.Characters)
		{
			Debug.Log($"{chara.Name} HP:{chara.HP} �U����:{chara.Attack}");
		}
	}
}
