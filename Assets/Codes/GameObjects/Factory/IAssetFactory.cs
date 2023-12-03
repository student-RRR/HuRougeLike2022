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
    // 產生Soldier
    public abstract GameObject LoadCreatureModel(ENUM_Model _Model);

	// 產生Effect
	public abstract GameObject LoadEffectModel(ENUM_EffectModel _Model);

	// 產生地圖
	public abstract Tile LoadMapGroundModel(ENUM_MapGround _Model);

	public abstract GameObject LoadMapBlockModel(ENUM_MapBlock _Model);

	// 產生UI
	public abstract GameObject LoadUIModel(ENUM_UI _Model);
}

public class AssetFactory : IAssetFactory
{
	AssetBundle ab;
	AssetBundle mapBundle;

	// 建構者
	public AssetFactory()
    {
		// 載入放在本地裝置的AssetBundles
		string FilePath = "AssetBundles/test2.abc";
		ab = AssetBundle.LoadFromFile(FilePath); // 載入AssetBundles


		string MapFilePath = "AssetBundles/map.abc";
		mapBundle = AssetBundle.LoadFromFile(MapFilePath); // 載入AssetBundles
	}

	// 載入生物物件
	public override GameObject LoadCreatureModel(ENUM_Model _Model)
	{
		// 物件名稱
		string AssetName = "";

		// 取得對應物件名稱
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


		GameObject obj = ab.LoadAsset<GameObject>(AssetName); // 根據AssetName, 載入GameObject
		
		// 執行載入
		return obj;
	}

	// 載入效果物件
	public override GameObject LoadEffectModel(ENUM_EffectModel _Model)
	{
		// 物件名稱
		string AssetName = "";

		// 取得對應物件名稱
		switch (_Model)
		{
			case ENUM_EffectModel.ModelAttack:
				AssetName = "attack";
				break;
			default:
				break;
		}


		GameObject obj = ab.LoadAsset<GameObject>(AssetName); // 根據AssetName, 載入GameObject

		// 執行載入
		return obj;
	}

	/// <summary>
	/// 地圖地板
	/// </summary>
	public override Tile LoadMapGroundModel(ENUM_MapGround _Model)
	{
		// 物件名稱
		string AssetName = "";

		// 取得對應物件名稱
		switch (_Model)
		{
			case ENUM_MapGround.ModelRockGround:
				AssetName = "backyard_04";
				break;
			default:
				break;
		}


		Tile tile = mapBundle.LoadAsset<Tile>(AssetName); // 根據AssetName, 載入GameObject

		// 執行載入
		return tile;
	}


	/// <summary>
	/// 地圖牆塊
	/// </summary>
	public override GameObject LoadMapBlockModel(ENUM_MapBlock _Model)
	{
		// 物件名稱
		string AssetName = "";

		// 取得對應物件名稱
		switch (_Model)
		{
			case ENUM_MapBlock.ModelRockBlock:
				AssetName = "DirtBlock";
				break;
			default:
				break;
		}


		GameObject obj = mapBundle.LoadAsset<GameObject>(AssetName); // 根據AssetName, 載入GameObject

		// 執行載入
		return obj;
	}

    public override GameObject LoadUIModel(ENUM_UI eNUM_UI)
    {
		// 物件名稱
		string AssetName = "";

		// 取得對應物件名稱
		switch (eNUM_UI)
		{
			case ENUM_UI.BarHP:
				AssetName = "BarHP";
				break;
			default:
				break;
		}


		GameObject obj = ab.LoadAsset<GameObject>(AssetName); // 根據AssetName, 載入GameObject

		// 執行載入
		return obj;
	}
}