using UnityEngine;
using System.Collections;
using System.Net;
using System.Collections.Generic;

[AddComponentMenu("2D Toolkit/Demo/tk2dUIDemoController")]
public class MenuInicio : tk2dUIBaseDemoController
{
    /// <summary>
    /// Button that will change to next page
    /// </summary>
//	public tk2dUIItem nextPage;
    
//	/// <summary>
//	/// GameObject containing everything in page 1
//	/// </summary>
    public GameObject window1;

//	
//	/// <summary>
//	/// Button that will change to prev page
//	/// </summary>
    public tk2dUIItem prevPage;
    public tk2dUITextInput txt;
//	public tk2dUIItem prevPage3;
//	
//	/// <summary>
//	/// GameObject containing everything in page 2
//	/// </summary>
//	public GameObject window2;
//	public GameObject window3;
    
    /// <summary>
    /// Used to demo progress bar movement
    /// </summary>
//	public tk2dUIProgressBar progressBar;
//	private float timeSincePageStart = 0;
//	private const float TIME_TO_COMPLETE_PROGRESS_BAR = 2f;
//	private float progressBarChaseVelocity = 0.0f;
//	public tk2dUIScrollbar slider;
    
    private GameObject currWindow;
    static public MenuInicio Instance;
    //WebAsync webAsync = new WebAsync();
    void Update()
    {
        //StartCoroutine(AreWeConnectedToInternet());
    }
    void Awake()
    {

        Instance = GetComponent<MenuInicio>();
//		ShowWindow(window1.transform);
//		HideWindow(window2.transform);
//		HideWindow(window3.transform);
//		Debug.Log ("Entre");
    }
    
    void OnEnable()
    {	
        prevPage.OnClick += GoToPage1;
    }
    
    void OnDisable()
    {
        prevPage.OnClick -= GoToPage1;
    }


    private void GoToPage1()
    {
#if UNITY_EDITOR_WIN
        Debug.Log("Unity Editor");
#endif

#if UNITY_IPHONE
      Debug.Log("Iphone");
#endif

#if UNITY_STANDALONE_OSX
    Debug.Log("Stand Alone OSX");
#endif

#if UNITY_STANDALONE_WIN
      Debug.Log("Stand Alone Windows");
#endif
        Application.LoadLevel("perfil");
    }
}