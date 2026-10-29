// Place this file in: Assets/Editor/MiniGolfBuilder.cs
// Then run: Tools > Mini Golf > Build Level
#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Rendering;

public static class MiniGolfBuilder
{
    const string MatFolder = "Assets/Materials";
    const string SceneFolder = "Assets/Scenes";
    static Dictionary<string, Material> mats = new Dictionary<string, Material>();

    [MenuItem("Tools/Mini Golf/Build Level")]
    public static void Build()
    {
        mats.Clear();
        EnsureFolder("Assets", "Materials");
        EnsureFolder("Assets", "Scenes");

        var scene = EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects, NewSceneMode.Single);
        Random.InitState(7);

        // ---------- Hierarchy ----------
        Transform root = Group("GolfCourse", null, Vector3.zero);
        Transform course = Group("Course", root, Vector3.zero);
        Transform walls = Group("BoundaryWalls", course, Vector3.zero);
        Transform obstacles = Group("Obstacles", root, Vector3.zero);
        Transform finish = Group("FinishArea", root, Vector3.zero);
        Transform deco = Group("Decorations", root, Vector3.zero);
        Transform trees = Group("Trees", deco, Vector3.zero);
        Transform rocks = Group("Rocks", deco, Vector3.zero);
        Transform bushes = Group("Bushes", deco, Vector3.zero);
        Transform flowers = Group("Flowers", deco, Vector3.zero);
        Transform signs = Group("Signs", deco, Vector3.zero);

        // ---------- Materials ----------
        Material grass = Mat("M_Grass", new Color(0.18f, 0.62f, 0.25f), 0.1f);
        Material grassLight = Mat("M_GrassLight", new Color(0.30f, 0.74f, 0.33f), 0.1f);
        Material ground = Mat("M_Ground", new Color(0.36f, 0.55f, 0.27f), 0f);
        Material wallMat = Mat("M_Walls", new Color(0.55f, 0.32f, 0.17f), 0.2f);
        Material obsPurple = Mat("M_ObstaclePurple", new Color(0.55f, 0.28f, 0.85f), 0.3f);
        Material obsBlue = Mat("M_ObstacleBlue", new Color(0.20f, 0.50f, 0.92f), 0.3f);
        Material ballMat = Mat("M_Ball", Color.white, 0.8f);
        Material flagMat = Mat("M_Flag", new Color(1f, 0.85f, 0.1f), 0.2f);
        Material holeMat = Mat("M_Hole", new Color(0.04f, 0.04f, 0.04f), 0f);
        Material ringMat = Mat("M_HoleRing", new Color(0.95f, 0.95f, 0.95f), 0.3f);
        Material teeMat = Mat("M_Tee", new Color(0.85f, 0.85f, 0.85f), 0.2f);
        Material poleMat = Mat("M_Pole", new Color(0.9f, 0.9f, 0.9f), 0.5f);
        Material trunk = Mat("M_Trunk", new Color(0.40f, 0.25f, 0.12f), 0.1f);
        Material leaves = Mat("M_Leaves", new Color(0.10f, 0.45f, 0.15f), 0.1f);
        Material leavesDark = Mat("M_LeavesDark", new Color(0.06f, 0.33f, 0.12f), 0.1f);
        Material rockMat = Mat("M_Rock", new Color(0.50f, 0.50f, 0.53f), 0.2f);
        Material water = Mat("M_Water", new Color(0.15f, 0.55f, 0.90f), 0.95f);
        Material lily = Mat("M_LilyPad", new Color(0.25f, 0.70f, 0.30f), 0.2f);
        Material signMat = Mat("M_SignBoard", new Color(0.75f, 0.12f, 0.12f), 0.2f);
        Material mill = Mat("M_WindmillWall", new Color(0.95f, 0.90f, 0.78f), 0.1f);
        Material roof = Mat("M_WindmillRoof", new Color(0.75f, 0.15f, 0.12f), 0.2f);
        Material sail = Mat("M_Sail", new Color(0.98f, 0.98f, 0.95f), 0.1f);
        Material fRed = Mat("M_FlowerRed", new Color(0.95f, 0.15f, 0.2f), 0.2f);
        Material fYellow = Mat("M_FlowerYellow", new Color(1f, 0.88f, 0.15f), 0.2f);
        Material fPink = Mat("M_FlowerPink", new Color(1f, 0.45f, 0.75f), 0.2f);
        Material stem = Mat("M_Stem", new Color(0.15f, 0.5f, 0.15f), 0.1f);
        Material[] petals = { fRed, fYellow, fPink };

        // ---------- Ground + Course ----------
        Make(PrimitiveType.Cube, "Ground", root, new Vector3(0, -0.1f, 0), Vector3.zero, new Vector3(70, 0.2f, 70), ground);
        Make(PrimitiveType.Cube, "CourseFloor", course, new Vector3(0, 0, 0), Vector3.zero, new Vector3(10, 0.2f, 20), grass);
        Make(PrimitiveType.Cube, "Stripe_1", course, new Vector3(-3.75f, 0.1f, 0), Vector3.zero, new Vector3(2.5f, 0.02f, 20), grassLight);
        Make(PrimitiveType.Cube, "Stripe_2", course, new Vector3(1.25f, 0.1f, 0), Vector3.zero, new Vector3(2.5f, 0.02f, 20), grassLight);
        Make(PrimitiveType.Cube, "TeePad", course, new Vector3(0, 0.1f, -8), Vector3.zero, new Vector3(1.4f, 0.04f, 1.4f), teeMat);

        // Walls
        Make(PrimitiveType.Cube, "Wall_Left", walls, new Vector3(-5.25f, 0.6f, 0), Vector3.zero, new Vector3(0.5f, 1, 20), wallMat);
        Make(PrimitiveType.Cube, "Wall_Right", walls, new Vector3(5.25f, 0.6f, 0), Vector3.zero, new Vector3(0.5f, 1, 20), wallMat);
        Make(PrimitiveType.Cube, "Wall_Start", walls, new Vector3(0, 0.6f, -10.25f), Vector3.zero, new Vector3(11, 1, 0.5f), wallMat);
        Make(PrimitiveType.Cube, "Wall_End", walls, new Vector3(0, 0.6f, 10.25f), Vector3.zero, new Vector3(11, 1, 0.5f), wallMat);

        // ---------- Ball ----------
        Make(PrimitiveType.Sphere, "GolfBall", root, new Vector3(0, 0.32f, -8), Vector3.zero, Vector3.one * 0.4f, ballMat);

        // ---------- Obstacles ----------
        Make(PrimitiveType.Cube, "Obstacle_Block1", obstacles, new Vector3(-1.5f, 0.6f, -4), Vector3.zero, new Vector3(3, 1, 0.5f), obsPurple);
        Make(PrimitiveType.Cylinder, "Obstacle_Pillar", obstacles, new Vector3(2.5f, 0.6f, -1), Vector3.zero, new Vector3(1.2f, 0.5f, 1.2f), obsBlue);
        Make(PrimitiveType.Cube, "Obstacle_Block2", obstacles, new Vector3(1.5f, 0.6f, 2.5f), new Vector3(0, 30, 0), new Vector3(3, 1, 0.5f), obsPurple);
        Make(PrimitiveType.Cube, "Obstacle_Block3", obstacles, new Vector3(-2.5f, 0.6f, 5.5f), new Vector3(0, -45, 0), new Vector3(2.5f, 1, 0.5f), obsBlue);

        // ---------- Finish area ----------
        Make(PrimitiveType.Cylinder, "FinishRing", finish, new Vector3(0, 0.105f, 8), Vector3.zero, new Vector3(2.1f, 0.01f, 2.1f), ringMat);
        Make(PrimitiveType.Cylinder, "FinishHole", finish, new Vector3(0, 0.11f, 8), Vector3.zero, new Vector3(1.5f, 0.02f, 1.5f), holeMat);
        Make(PrimitiveType.Cylinder, "FlagPole", finish, new Vector3(0.8f, 0.85f, 8), Vector3.zero, new Vector3(0.05f, 0.75f, 0.05f), poleMat);
        Make(PrimitiveType.Cube, "Flag", finish, new Vector3(1.05f, 1.4f, 8), Vector3.zero, new Vector3(0.5f, 0.3f, 0.02f), flagMat);

        // ---------- Signs ----------
        BuildSign("Sign_START", "START", signs, new Vector3(-3.6f, 0.1f, -9f), signMat, poleMat);
        BuildSign("Sign_FINISH", "FINISH", signs, new Vector3(-3.6f, 0.1f, 8.8f), signMat, poleMat);

        // ---------- Trees ----------
        Vector3[] treePos = {
            new Vector3(-9,0,-8), new Vector3(9,0,-8), new Vector3(-10,0,10), new Vector3(13,0,0),
            new Vector3(-13,0,-2), new Vector3(12,0,14), new Vector3(-14,0,8), new Vector3(-8,0,15),
            new Vector3(8,0,-14), new Vector3(-7,0,-14), new Vector3(14,0,-9), new Vector3(0,0,17)
        };
        for (int i = 0; i < treePos.Length; i++)
        {
            float s = Random.Range(0.85f, 1.3f);
            Transform t = Group("Tree_" + (i + 1).ToString("00"), trees, treePos[i]);
            Make(PrimitiveType.Cylinder, "Trunk", t, new Vector3(0, 1f * s, 0), Vector3.zero, new Vector3(0.3f, 1f, 0.3f) * s, trunk);
            Make(PrimitiveType.Sphere, "Leaves_Lower", t, new Vector3(0, 2.6f * s, 0), Vector3.zero, Vector3.one * 2.6f * s, i % 2 == 0 ? leaves : leavesDark);
            Make(PrimitiveType.Sphere, "Leaves_Upper", t, new Vector3(0, 3.7f * s, 0), Vector3.zero, Vector3.one * 1.7f * s, i % 2 == 0 ? leavesDark : leaves);
        }

        // ---------- Pond ----------
        Transform pond = Group("Pond", deco, new Vector3(-9, 0, 2));
        Make(PrimitiveType.Cylinder, "Water", pond, new Vector3(0, 0.04f, 0), Vector3.zero, new Vector3(5, 0.04f, 4), water);
        Make(PrimitiveType.Cylinder, "LilyPad_1", pond, new Vector3(-0.8f, 0.085f, 0.4f), Vector3.zero, new Vector3(0.5f, 0.005f, 0.5f), lily);
        Make(PrimitiveType.Cylinder, "LilyPad_2", pond, new Vector3(0.9f, 0.085f, -0.5f), Vector3.zero, new Vector3(0.4f, 0.005f, 0.4f), lily);
        Make(PrimitiveType.Cylinder, "LilyPad_3", pond, new Vector3(0.2f, 0.085f, 0.8f), Vector3.zero, new Vector3(0.35f, 0.005f, 0.35f), lily);
        for (int i = 0; i < 12; i++)
        {
            float a = i * Mathf.PI * 2f / 12f;
            float rs = Random.Range(0.4f, 0.75f);
            Make(PrimitiveType.Sphere, "PondRock_" + (i + 1), rocks,
                new Vector3(-9 + 2.7f * Mathf.Cos(a), 0.15f, 2 + 2.2f * Mathf.Sin(a)),
                new Vector3(0, Random.Range(0, 180), 0), new Vector3(rs, rs * 0.6f, rs * 0.85f), rockMat);
        }

        // ---------- Extra rocks and bushes ----------
        Vector3[] rockPos = { new Vector3(7, 0, -5), new Vector3(7.5f, 0, -4.4f), new Vector3(-7, 0, 9), new Vector3(7, 0, 12), new Vector3(-6.8f, 0, -9) };
        for (int i = 0; i < rockPos.Length; i++)
        {
            float rs = Random.Range(0.6f, 1.1f);
            Make(PrimitiveType.Sphere, "Rock_" + (i + 1), rocks, rockPos[i] + Vector3.up * 0.25f,
                new Vector3(0, Random.Range(0, 180), 0), new Vector3(rs, rs * 0.6f, rs * 0.85f), rockMat);
        }
        Vector3[] bushPos = { new Vector3(-7.5f, 0, -8), new Vector3(7.5f, 0, -9), new Vector3(-7.2f, 0, 12), new Vector3(7.5f, 0, 5), new Vector3(8f, 0, -12), new Vector3(-8, 0, -12) };
        for (int i = 0; i < bushPos.Length; i++)
        {
            float bs = Random.Range(0.9f, 1.4f);
            Make(PrimitiveType.Sphere, "Bush_" + (i + 1), bushes, bushPos[i] + Vector3.up * 0.4f, Vector3.zero,
                new Vector3(1.3f * bs, 0.9f * bs, 1.3f * bs), i % 2 == 0 ? leaves : leavesDark);
        }

        // ---------- Flowers ----------
        int fCount = 0;
        for (int i = 0; i < 40; i++)
        {
            float side = i % 2 == 0 ? -1 : 1;
            float x = side * Random.Range(6.2f, 8.2f);
            float z = Random.Range(-10f, 10f);
            if (x < 0 && z > -1 && z < 5.5f) continue; // keep clear of pond
            fCount++;
            Transform f = Group("Flower_" + fCount.ToString("00"), flowers, new Vector3(x, 0, z));
            Make(PrimitiveType.Cylinder, "Stem", f, new Vector3(0, 0.15f, 0), Vector3.zero, new Vector3(0.03f, 0.15f, 0.03f), stem);
            Make(PrimitiveType.Sphere, "Head", f, new Vector3(0, 0.34f, 0), Vector3.zero, Vector3.one * 0.2f, petals[i % 3]);
        }

        // ---------- Windmill ----------
        Transform wm = Group("Windmill", deco, new Vector3(9, 0, 7));
        Make(PrimitiveType.Cylinder, "Tower", wm, new Vector3(0, 2f, 0), Vector3.zero, new Vector3(1.4f, 2f, 1.4f), mill);
        Make(PrimitiveType.Sphere, "Roof", wm, new Vector3(0, 4.1f, 0), Vector3.zero, new Vector3(1.8f, 1.2f, 1.8f), roof);
        Make(PrimitiveType.Cube, "Door", wm, new Vector3(0, 0.55f, -0.7f), Vector3.zero, new Vector3(0.5f, 1.0f, 0.1f), trunk);
        Make(PrimitiveType.Sphere, "Hub", wm, new Vector3(0, 3.6f, -0.85f), Vector3.zero, Vector3.one * 0.4f, wallMat);
        Transform blades = Group("Blades", wm, new Vector3(0, 3.6f, -0.95f));
        blades.localRotation = Quaternion.Euler(0, 0, 20);
        Make(PrimitiveType.Cube, "Spar_V", blades, Vector3.zero, Vector3.zero, new Vector3(0.12f, 3.4f, 0.06f), wallMat);
        Make(PrimitiveType.Cube, "Spar_H", blades, Vector3.zero, Vector3.zero, new Vector3(3.4f, 0.12f, 0.06f), wallMat);
        for (int k = 0; k < 4; k++)
        {
            Transform arm = Group("Sail_" + (k + 1), blades, Vector3.zero);
            arm.localRotation = Quaternion.Euler(0, 0, k * 90);
            Make(PrimitiveType.Cube, "SailPanel", arm, new Vector3(0.4f, 1.15f, -0.04f), Vector3.zero, new Vector3(0.65f, 1.3f, 0.04f), sail);
        }

        // ---------- Camera ----------
        GameObject camGO = GameObject.Find("Main Camera");
        camGO.transform.SetParent(root, true);
        camGO.transform.position = new Vector3(0, 24, -12);
        camGO.transform.rotation = Quaternion.Euler(65, 0, 0);
        Camera cam = camGO.GetComponent<Camera>();
        cam.fieldOfView = 60;
        cam.clearFlags = CameraClearFlags.SolidColor;
        cam.backgroundColor = new Color(0.55f, 0.78f, 0.95f);

        // ---------- Light ----------
        GameObject lightGO = GameObject.Find("Directional Light");
        lightGO.transform.SetParent(root, true);
        lightGO.transform.rotation = Quaternion.Euler(50, -30, 0);
        Light l = lightGO.GetComponent<Light>();
        l.intensity = 1.3f;
        l.color = new Color(1f, 0.96f, 0.88f);
        l.shadows = LightShadows.Soft;
        RenderSettings.ambientMode = AmbientMode.Flat;
        RenderSettings.ambientLight = new Color(0.62f, 0.67f, 0.78f);

        // ---------- Save ----------
        AssetDatabase.SaveAssets();
        EditorSceneManager.SaveScene(scene, SceneFolder + "/MiniGolf_Level.unity");
        AssetDatabase.Refresh();
        Selection.activeGameObject = root.gameObject;
        EditorUtility.DisplayDialog("Mini Golf", "Level built and saved to Assets/Scenes/MiniGolf_Level.unity.\nSet the Game View aspect to 16:9 and take your screenshots.", "OK");
    }

    // ---------- Helpers ----------
    static void EnsureFolder(string parent, string name)
    {
        if (!AssetDatabase.IsValidFolder(parent + "/" + name))
            AssetDatabase.CreateFolder(parent, name);
    }

    static Transform Group(string name, Transform parent, Vector3 pos)
    {
        var g = new GameObject(name);
        if (parent != null) g.transform.SetParent(parent, false);
        g.transform.localPosition = pos;
        return g.transform;
    }

    static GameObject Make(PrimitiveType type, string name, Transform parent, Vector3 pos, Vector3 rot, Vector3 scale, Material m)
    {
        var g = GameObject.CreatePrimitive(type);
        g.name = name;
        g.transform.SetParent(parent, false);
        g.transform.localPosition = pos;
        g.transform.localRotation = Quaternion.Euler(rot);
        g.transform.localScale = scale;
        g.GetComponent<Renderer>().sharedMaterial = m;
        return g;
    }

    static Material Mat(string name, Color c, float smooth)
    {
        if (mats.TryGetValue(name, out var cached)) return cached;
        string path = MatFolder + "/" + name + ".mat";
        var existing = AssetDatabase.LoadAssetAtPath<Material>(path);
        if (existing != null) { mats[name] = existing; return existing; }

        Shader sh = Shader.Find("Universal Render Pipeline/Lit");
        if (sh == null) sh = Shader.Find("Standard");
        var m = new Material(sh);
        if (m.HasProperty("_BaseColor")) m.SetColor("_BaseColor", c);
        if (m.HasProperty("_Color")) m.SetColor("_Color", c);
        if (m.HasProperty("_Smoothness")) m.SetFloat("_Smoothness", smooth);
        if (m.HasProperty("_Glossiness")) m.SetFloat("_Glossiness", smooth);
        AssetDatabase.CreateAsset(m, path);
        mats[name] = m;
        return m;
    }

    static void BuildSign(string name, string text, Transform parent, Vector3 pos, Material board, Material post)
    {
        Transform s = Group(name, parent, pos);
        Make(PrimitiveType.Cylinder, "Post", s, new Vector3(0, 0.75f, 0), Vector3.zero, new Vector3(0.1f, 0.75f, 0.1f), post);
        Make(PrimitiveType.Cube, "Board", s, new Vector3(0, 1.45f, 0), Vector3.zero, new Vector3(1.6f, 0.5f, 0.08f), board);
        try
        {
            var go = new GameObject("Text_" + text);
            go.transform.SetParent(s, false);
            go.transform.localPosition = new Vector3(0, 1.45f, -0.06f);
            var tm = go.AddComponent<TextMesh>();
            tm.text = text;
            tm.anchor = TextAnchor.MiddleCenter;
            tm.alignment = TextAlignment.Center;
            tm.fontStyle = FontStyle.Bold;
            tm.fontSize = 100;
            tm.characterSize = 0.03f;
            tm.color = Color.white;
            Font f = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            tm.font = f;
            go.GetComponent<MeshRenderer>().sharedMaterial = f.material;
        }
        catch { Debug.LogWarning("Could not add text to " + name + " (board still created)."); }
    }
}
#endif
