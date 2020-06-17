using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;

public class utama : MonoBehaviour
{
    
    public GameObject Model_A;
    public GameObject Model_B;
    public GameObject Model_C;
    public GameObject Model_D;

    ArrayList binatang = new ArrayList();

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("folderList", 0f, 10f);
    }

    void folderList()
    {
        scanFolder("Model_A");
        scanFolder("Model_B");
        scanFolder("Model_C");
        scanFolder("Model_D");
    }

    void scanFolder(string folder)
    {
        string path = Application.streamingAssetsPath+"/";
        DirectoryInfo dir = new DirectoryInfo(path+folder);
        FileInfo[] info = dir.GetFiles("*.jpg").OrderBy(p => p.CreationTime).ToArray();
        if (info.Length > 0)
        {
            foreach (FileInfo f in info)
            {
                if (binatang.Contains(f.ToString()) != true)
                {
                    binatang.Add(f.ToString());
                    if (folder == "Model_A")
                    {
                        munculkan(Model_A, f.ToString());
                    }
                    if (folder == "Model_B")
                    {
                        munculkan(Model_B, f.ToString());
                    }
                    if (folder == "Model_C")
                    {
                        munculkan(Model_C, f.ToString());
                    }
                    if (folder == "Model_D")
                    {
                        munculkan(Model_D, f.ToString());
                    }
                    break;
                    //StartCoroutine(setImage(hewan, f.ToString()));
                }
               // Debug.Log(binatang.Contains(f.ToString()));
            }
            //StartCoroutine(ulangLagi());
        }
        else
        {
            //StartCoroutine(ulangLagi());
        }
    }

    void Update()
    {
		if(Input.GetKeyDown("space")){
			Debug.Log("aaaaa");
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
    }
    void munculkan(GameObject hewan, string nama)
    {
        Renderer rend = hewan.GetComponentInChildren<SkinnedMeshRenderer>();
        Material mat = new Material(Shader.Find("Standard"));
        mat.SetFloat("_Metallic", 0f);
        mat.SetFloat("_Glossiness", 0f);
        rend.sharedMaterial = mat;

        byte[] bytes = System.IO.File.ReadAllBytes(nama);
        Texture2D tex = new Texture2D(2, 2);
        tex.LoadImage(bytes);
        rend.sharedMaterial.mainTexture = tex;

        GameObject karakter = Instantiate(hewan, new Vector3( Random.Range(0f,0f), 0.2f, Random.Range(0f, 6f)), Quaternion.identity);
        karakter.name = nama;
    }
}
