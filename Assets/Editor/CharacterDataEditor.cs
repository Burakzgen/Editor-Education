using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

[CustomEditor(typeof(CharacterData))]
public class CharacterDataEditor : Editor
{
    CharacterData characterData;
    GUILayoutOption half;
    GUILayoutOption oneThird;

    static GUISkin characterSkin;

    string[] names = new string[]
    {
        "Ersin",
        "Rossier",
        "Sassia",
        "Umut",
        "Necip",
        "Atiba",
        "Ghezzal",
        "Cenk"
        };
    string[] surnames = new string[]
    {
        "Destanoglu",
        "Valentine",
        "Unknown",
        "Nayer",
        "Uysal",
        "Hutchisson",
        "Rashied",
        "Tosun"
    };
    void RandomName()
    {
        string name = names[Random.Range(0, names.Length - 1)];
        string surname = surnames[Random.Range(0, surnames.Length - 1)];

        characterData.name = name;
        characterData.surName = surname;
    }
    public override void OnInspectorGUI()
    {
        GUI.skin = characterSkin;
        GUI.skin.font = characterSkin.font;
        GUILayout.BeginVertical();

        half = GUILayout.Width((EditorGUIUtility.currentViewWidth / 2) - 15);
        oneThird = GUILayout.Width((EditorGUIUtility.currentViewWidth / 3) - 5);

        NameArea();
        GUILayout.Space(10);
        GUILayout.Label("Features");
        GUILayout.BeginHorizontal();
        HealthArea();
        ManaArea();
        PowerArea();
        GUILayout.EndHorizontal();

        DraftsRegion();

        GUILayout.EndVertical();

        // Deðiþim olduðunda kayýt için eklendi.
        if (GUI.changed)
        {
            EditorUtility.SetDirty(characterData); // Verilerdeki kayýt
            EditorSceneManager.MarkSceneDirty(characterData.gameObject.scene); // Sahnelerdeki kayýt
        }
    }
    void DraftsRegion()
    {
        GUILayout.Label("Drafts");
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Character 1", oneThird))
            Character_1();
        if (GUILayout.Button("Character 2", oneThird))
            Character_2();
        if (GUILayout.Button("Character 3", oneThird))
            Character_3();
        GUILayout.EndHorizontal();
    }
    void NameArea()
    {
        GUILayout.Label("CHARACTER");
        GUILayout.BeginHorizontal();
        GUILayout.BeginVertical();
        GUILayout.Label("Name", half);
        characterData.name = GUILayout.TextField(characterData.name, half);
        //EditorGUILayout.HelpBox("Bu button obje ismini verir.", MessageType.Error);
        GUILayout.EndVertical();
        GUILayout.BeginVertical();
        GUILayout.Label("Surname", half);
        characterData.surName = GUILayout.TextField(characterData.surName, half);
        GUILayout.EndVertical();
        GUILayout.EndHorizontal();

        if (GUILayout.Button("Random Name"))
            RandomName();
    }
    void HealthArea()
    {
        GUILayout.BeginVertical();
        GUIStyle labelStyle = characterSkin.GetStyle("label");
        labelStyle.normal.background = ColorTex(Color.red);

        Texture image = EditorGUIUtility.IconContent("health").image;
        GUIContent labelContect = new GUIContent("Health", image);

        GUILayout.Label(labelContect, labelStyle, oneThird);
        GUILayout.Space(5);
        characterData.health = EditorGUILayout.FloatField(characterData.health, GUI.skin.textField, oneThird);
        GUILayout.EndVertical();

    }
    void ManaArea()
    {
        GUILayout.BeginVertical();
        GUIStyle labelStyle = characterSkin.GetStyle("label");
        labelStyle.normal.background = ColorTex(Color.blue);

        Texture image = EditorGUIUtility.IconContent("Mana").image;
        GUIContent labelContect = new GUIContent("Mana", image);

        GUILayout.Label(labelContect, labelStyle, oneThird);
        GUILayout.Space(5);
        characterData.mana = EditorGUILayout.FloatField(characterData.mana, GUI.skin.textField, oneThird);
        GUILayout.EndVertical();
    }
    void PowerArea()
    {
        GUILayout.BeginVertical();
        GUIStyle labelStyle = characterSkin.GetStyle("label");
        labelStyle.normal.background = ColorTex(Color.green);

        Texture image = EditorGUIUtility.IconContent("power").image;
        GUIContent labelContect = new GUIContent("Power", image);

        GUILayout.Label(labelContect, labelStyle, oneThird);
        GUILayout.Space(5);
        characterData.power = EditorGUILayout.FloatField(characterData.power, GUI.skin.textField, oneThird);
        GUILayout.EndVertical();
    }
    [MenuItem("CONTEXT/CharacterData/Default Skin")]
    static void DefaultSkin()
    {
        if (characterSkin != null)
        {
            characterSkin.textField = new GUIStyle(EditorStyles.textField);
            characterSkin.button = new GUIStyle(EditorStyles.miniButtonMid);
            characterSkin.label = new GUIStyle(EditorStyles.label);
        }
    }
    Texture2D ColorTex(Color color)
    {
        Texture2D texture = new Texture2D(1, 1);

        texture.SetPixel(1, 1, color);
        texture.Apply();

        return texture;
    }
    private void OnEnable()
    {

        characterData = (CharacterData)target;
        if (characterSkin == null)
        {
            characterSkin = EditorGUIUtility.Load("ChracterData.guiskin") as GUISkin;
        }

    }
    void Character_1()
    {
        characterData.health = 20;
        characterData.mana = 30;
        characterData.power = 40;
    }
    void Character_2()
    {
        characterData.health = 5;
        characterData.mana = 8;
        characterData.power = 7;
    }
    void Character_3()
    {
        characterData.health = 12;
        characterData.mana = 5;
        characterData.power = 16;
    }
}
