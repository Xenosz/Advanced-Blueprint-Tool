using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json; // Added the json libs, unity has a very simple version build in, but it doesnt have dynamic json objects (JObject). this is the main json stuff.
using Newtonsoft.Json.Linq; // JObject is in this class.

public class MenuContentBehaviourScript : MonoBehaviour {
    public GameObject ItemHolder;
    public Sprite sprite;

	// Use this for initialization
	void Start ()
    {

        TestFunction();
        GameObject GO = Instantiate(ItemHolder);
        GO.transform.SetParent(transform);
        GO.transform.GetChild(0).GetComponent<Image>().sprite = sprite;
        GO.transform.GetChild(1).GetComponent<Text>().text = "shinevision <3";

        GameObject GO1 = Instantiate(ItemHolder);
        GO1.transform.SetParent(transform);
        GO1.transform.GetChild(1).GetComponent<Text>().text = "maar wat nu met een langere text";

        List<GameObject> buttons = new List<GameObject>();
        for (int i = 0; i < 30; i++)
        {

        }
    }

    void TestFunction()
    {
        TestClass testClass = new TestClass(); // create random data for testing.
        testClass.testData = "TestDataStuff";
        testClass.nestedobject.Add(new  nestedObject {nameTest = "nameTestStuff" });
        JObject json = new JObject(); // Creating a json object.
        json = JsonConvert.DeserializeObject<JObject>(JsonConvert.SerializeObject(testClass)); // Converting the temp class to random json data. then deserializing it. (Simulating reading a file and getting data :P)
        Debug.Log(json["testData"]); // Trying to read random data. if all good, you should see the data inside testData in the console in unity. (Data should be what we set above ("TestDataStuff")).
        Debug.Log(json["nestedobject"][0]["nameTest"]); // Testing nesting objects.
    }

    // Update is called once per frame
    void Update ()
    {
		
	}
}

public class TestClass // Temp class for testing the json stuff
{
    public string testData;
    public static string testName;

    public List<nestedObject> nestedobject = new List<nestedObject>();
}

public class nestedObject
{
    public Texture2D imageTest;
    public string nameTest;
}
