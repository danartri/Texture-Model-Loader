using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;

public class loader : MonoBehaviour
{
 
    public GameObject Model_A;
    public GameObject Model_B;


  ArrayList binatang = new ArrayList();


    void Start()
    {
        InvokeRepeating("folderList", 0f, 12f);
    }

    void folderList()
    {
        scanFolder("Model_A");
        scanFolder("Model_B");
       
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

                    break;
                   //StartCoroutine(setImage(hewan, f.ToString()));
                }
               //Debug.Log(binatang.Contains(f.ToString()));
            }
          // StartCoroutine(ulangLagi());
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
