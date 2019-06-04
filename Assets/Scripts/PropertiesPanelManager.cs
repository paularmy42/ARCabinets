using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using VRTK;
using UnityEngine.UI;

public class PropertiesPanelManager : MonoBehaviour
{
    public GameObject leftController;
    public GameObject rightController;

    public Text lengthSliderValue;
    public Slider lengthSlider;

    public Text depthSliderValue;
    public Slider depthSlider;

    public Text heightSliderValue;
    public Slider heightSlider;

    public Dropdown cabinetSelector;
    public Dropdown faceMaterialSelector;

    public int sliderIncrement = 5;

    private PlacementManager manager;
    private List<ICabinet> library = CabinetLibrary.Cabinets;
    private void Awake()
    {
        //Build cabinet list for Cabinet selector droplist
        List<string> cabinetList = new List<string>();
        foreach (ICabinet cabinet in library)
        {
            cabinetList.Add(cabinet.Name.ToString());
        }
        cabinetSelector.ClearOptions();
        cabinetSelector.AddOptions(cabinetList);

    }
    // Start is called before the first frame update
    void Start()
    {
        manager = Object.FindObjectOfType<PlacementManager>();
        //leftController.GetComponent<VRTK_ControllerEvents>().ButtonTwoPressed += new ControllerInteractionEventHandler(OnLeftButtonTwoPressed);
        OnCabinetTypeChanged();
        PlacementManager.length = (int)lengthSlider.value * sliderIncrement;
        PlacementManager.depth = (int)depthSlider.value * sliderIncrement;
        PlacementManager.height = (int)heightSlider.value * sliderIncrement;
        PlacementManager.faceMat = manager.faceMaterials[faceMaterialSelector.value];
        PlacementManager.cabinetType = cabinetSelector.options[cabinetSelector.value].text;
    }

    //void OnLeftButtonTwoPressed(object sender, ControllerInteractionEventArgs e)
    //{
    //    GameObject canvas = leftController.transform.GetChild(0).gameObject;
    //    canvas.SetActive(true);
    //}

    public void OnLengthChanged()
    {
        lengthSliderValue.text = ((int) lengthSlider.value * sliderIncrement).ToString("0");
        PlacementManager.length = (int) lengthSlider.value * sliderIncrement;
    }

    public void OnDepthChanged()
    {
        depthSliderValue.text = ((int) depthSlider.value * sliderIncrement).ToString("0");
        PlacementManager.depth = (int) depthSlider.value * sliderIncrement;
    }

    public void OnHeightChanged()
    {
        heightSliderValue.text = ((int) heightSlider.value * sliderIncrement).ToString("0");
        PlacementManager.height = (int) heightSlider.value * sliderIncrement;
    }

    public void OnCabinetTypeChanged()
    {
        Debug.Log(string.Format("Cabinet selection changed to {0}", cabinetSelector.options[cabinetSelector.value].text));
        PlacementManager.cabinetType = cabinetSelector.options[cabinetSelector.value].text;
        ICabinet cabinet = library.Find(x => x.Name.Contains(PlacementManager.cabinetType));
        Debug.Log(string.Format("Default Length = {0}", PlacementManager.length));
        lengthSlider.minValue = cabinet.MinLength / sliderIncrement;
        lengthSlider.maxValue = cabinet.MaxLength / sliderIncrement;
        heightSlider.minValue = cabinet.MinHeight / sliderIncrement;
        heightSlider.maxValue = cabinet.MaxHeight / sliderIncrement;
        depthSlider.minValue = cabinet.MinDepth / sliderIncrement;
        depthSlider.maxValue = cabinet.MaxDepth / sliderIncrement;
        lengthSlider.value = cabinet.MaxLength / sliderIncrement;
        heightSlider.value = cabinet.MaxHeight / sliderIncrement;
        depthSlider.value = cabinet.MaxDepth / sliderIncrement;
    }

    public void OnFaceMaterialChanged()
    {
        PlacementManager.faceMat = manager.faceMaterials[faceMaterialSelector.value];
    }

}
