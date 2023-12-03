using UnityEngine;
using UnityEngine.Tilemaps;

public enum ENUM_EffectModel
{
	ModelAttack
}

public enum ENUM_Model
{
	ModelElf,
	ModelOgre
}

public enum ENUM_MapGround
{
	ModelRockGround
}

public enum ENUM_MapBlock
{
	ModelRockBlock
}

public enum ENUM_UI
{
	BarHP
}

public abstract class IAssetFactory
{
    // ����Soldier
    public abstract GameObject LoadCreatureModel(ENUM_Model _Model);

	// ����Effect
	public abstract GameObject LoadEffectModel(ENUM_EffectModel _Model);

	// ���ͦa��
	public abstract Tile LoadMapGroundModel(ENUM_MapGround _Model);

	public abstract GameObject LoadMapBlockModel(ENUM_MapBlock _Model);

	// ����UI
	public abstract GameObject LoadUIModel(ENUM_UI _Model);
}

public class AssetFactory : IAssetFactory
{
	AssetBundle ab;
	AssetBundle mapBundle;

	// �غc��
	public AssetFactory()
    {
		// ���J��b���a�˸m��AssetBundles
		string FilePath = "AssetBundles/test2.abc";
		ab = AssetBundle.LoadFromFile(FilePath); // ���JAssetBundles


		string MapFilePath = "AssetBundles/map.abc";
		mapBundle = AssetBundle.LoadFromFile(MapFilePath); // ���JAssetBundles
	}

	// ���J�ͪ�����
	public override GameObject LoadCreatureModel(ENUM_Model _Model)
	{
		// ����W��
		string AssetName = "";

		// ���o��������W��
		switch (_Model)
        {
			case ENUM_Model.ModelElf:
				AssetName = "elf";
				break;
			case ENUM_Model.ModelOgre:
				AssetName = "ogre";
				break;
			default:
				break;
		}


		GameObject obj = ab.LoadAsset<GameObject>(AssetName); // �ھ�AssetName, ���JGameObject
		
		// ������J
		return obj;
	}

	// ���J�ĪG����
	public override GameObject LoadEffectModel(ENUM_EffectModel _Model)
	{
		// ����W��
		string AssetName = "";

		// ���o��������W��
		switch (_Model)
		{
			case ENUM_EffectModel.ModelAttack:
				AssetName = "attack";
				break;
			default:
				break;
		}


		GameObject obj = ab.LoadAsset<GameObject>(AssetName); // �ھ�AssetName, ���JGameObject

		// ������J
		return obj;
	}

	/// <summary>
	/// �a�Ϧa�O
	/// </summary>
	public override Tile LoadMapGroundModel(ENUM_MapGround _Model)
	{
		// ����W��
		string AssetName = "";

		// ���o��������W��
		switch (_Model)
		{
			case ENUM_MapGround.ModelRockGround:
				AssetName = "backyard_04";
				break;
			default:
				break;
		}


		Tile tile = mapBundle.LoadAsset<Tile>(AssetName); // �ھ�AssetName, ���JGameObject

		// ������J
		return tile;
	}


	/// <summary>
	/// �a�����
	/// </summary>
	public override GameObject LoadMapBlockModel(ENUM_MapBlock _Model)
	{
		// ����W��
		string AssetName = "";

		// ���o��������W��
		switch (_Model)
		{
			case ENUM_MapBlock.ModelRockBlock:
				AssetName = "DirtBlock";
				break;
			default:
				break;
		}


		GameObject obj = mapBundle.LoadAsset<GameObject>(AssetName); // �ھ�AssetName, ���JGameObject

		// ������J
		return obj;
	}

    public override GameObject LoadUIModel(ENUM_UI eNUM_UI)
    {
		// ����W��
		string AssetName = "";

		// ���o��������W��
		switch (eNUM_UI)
		{
			case ENUM_UI.BarHP:
				AssetName = "BarHP";
				break;
			default:
				break;
		}


		GameObject obj = ab.LoadAsset<GameObject>(AssetName); // �ھ�AssetName, ���JGameObject

		// ������J
		return obj;
	}
}