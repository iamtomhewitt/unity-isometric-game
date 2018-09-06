using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class LevelBuilder : MonoBehaviour 
{
    public Texture2D blueprint;
    public ColourMap[] colourMapping;

	void Start () 
    {
        GenerateLevel();
	}

    public void DestroyLevel(Transform parent)
    {
		while (transform.childCount != 0)
		{
			DestroyImmediate(transform.GetChild(0).gameObject);
		}
	}

    public void GenerateLevel()
    {
        for (int x = 0; x < blueprint.width; x++)
        {
            for (int z = 0; z < blueprint.height; z++)
            {
                GenerateTile(x, z);
            }
        }
    }

    void GenerateTile(int x, int z)
    {
        Color pixelColour = blueprint.GetPixel(x, z);

		if (pixelColour.a == 0 || pixelColour == Color.white)
		{
			return;
		}
		else
		{
			foreach (ColourMap colourMap in colourMapping)
			{
				if (colourMap.colour.Equals(pixelColour))
				{
					Vector3 position = new Vector3(x, 0, z);
					GameObject tile = Instantiate(colourMap.prefab, position, Quaternion.identity, this.transform) as GameObject;
					tile.transform.name += " (" + x + "," + z + ")";
				}
			}
		}
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(LevelBuilder))]
public class LevelBuilderEditor : Editor
{
	LevelBuilder lb = null;

	void OnEnable()
	{
		lb = (LevelBuilder)target;
	}

	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		GUILayout.Space(15f);

		if (GUILayout.Button("BUILD LEVEL"))
		{
			lb.GenerateLevel();
		}

		GUILayout.Space(5f);

		if (GUILayout.Button("DESTROY LEVEL"))
		{
			if (EditorUtility.DisplayDialog("Delete Level", "Are you sure you want to delete this level?", "Yes", "No") == true)
				lb.DestroyLevel(lb.transform);
		}
	}
}
#endif

[System.Serializable]
public class ColourMap
{
    public string name;
    public Color colour;
    public GameObject prefab;
    public Transform parent;
}
