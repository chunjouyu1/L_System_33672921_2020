using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using System;
using UnityEngine.UI;


//public class TransformInfo
//{
    //public Vector3 position;
    //public Quaternion rotation;
//}


public class LSystem3 : MonoBehaviour
{
    private float angle;
    private float length;
    private float width = 1f;
    [Range (0,7)]
    private int iterations;   
    public float fieldOfView;
    private Vector3 initialPosition;
    private Vector3 initialRotation;

    //public int title = 1;

    private string axiom;
    private string currentString = string.Empty;

    [SerializeField] private GameObject treeParent;
    [SerializeField] private GameObject Branch;
    private GameObject Tree = null;
    private GameObject treeSegment;

    [SerializeField] private Dictionary<char, string> rules;
    private Stack<TransformInfo> transformStack;

    [SerializeField] private InputField inputFieldAngle;
    [SerializeField] private InputField inputFieldLength;
    [SerializeField] private InputField inputFieldIterations;
    [SerializeField] private InputField inputFieldAxiom;
    [SerializeField] private InputField inputFieldRules;
    [SerializeField] private Button generateButton;
    [SerializeField] private Button generatebystepButton;
    [SerializeField] private Button resetButton;
    [SerializeField] private Slider FOVslider;
    [SerializeField] private Button Amodel;
    [SerializeField] private Button Bmodel;
    [SerializeField] private Button Cmodel;
    [SerializeField] private Button Dmodel;
    [SerializeField] private Button Emodel;
    [SerializeField] private Button Fmodel;
    [SerializeField] private Button Gmodel;
    [SerializeField] private Button Hmodel;


    /*private void Awake()
    {
        inputFieldAngle.text = " ";
        inputFieldLength.text = " ";
        //inputFieldWidth.text = "";
        inputFieldIterations.text = " ";

        currentString = axiom;
    }*/

    void Start()
    {
        initialPosition = transform.position;
        initialRotation = transform.eulerAngles;
        fieldOfView = 150.0f;
        transformStack = new Stack<TransformInfo>();

        rules = new Dictionary<char, string>
        {
            //{'F', "F[+F]F[-F]F" }
        };
        
        
        //transform.Rotate(Vector3.right * -90.0f);
        currentString = axiom;
        //Generate();
    }

    public void OnEnable()
    {
        generateButton.onClick.AddListener(Generate);
        generatebystepButton.onClick.AddListener(Generatebystep);
        resetButton.onClick.AddListener(ResetValues);
        Amodel.onClick.AddListener(TreeA);
        Bmodel.onClick.AddListener(TreeB);
        Cmodel.onClick.AddListener(TreeC);
        Dmodel.onClick.AddListener(TreeD);
        Emodel.onClick.AddListener(TreeE);
        Fmodel.onClick.AddListener(TreeF);
        Gmodel.onClick.AddListener(TreeG);
        Hmodel.onClick.AddListener(TreeH);
    }

    private void Update()
    {
        Camera.main.fieldOfView = fieldOfView;
        OnGUI();

    }


    public void Generate()
    {
        
        transform.position = initialPosition;
        transform.eulerAngles = initialRotation;
        AddValues();
        Destroy(Tree);
        //WaitForSeconds();

        Tree = Instantiate(treeParent);

        StringBuilder sb = new StringBuilder();

        LineRenderer linerenderer = GetComponent<LineRenderer>();
        Debug.Log("drawline");


        for (int i = 0; i < iterations; i++)
        {
            foreach (char c in currentString)
            {
                sb.Append(rules.ContainsKey(c) ? rules[c] : c.ToString());
            }

            currentString = sb.ToString();
            sb = new StringBuilder();
        }


        foreach (char c in currentString)
        {
            switch (c)
            {
                case 'F':
                    Vector3 initialPosition = transform.position;
                    transform.Translate(Vector3.up * length);

                    treeSegment = Instantiate(Branch);
                    treeSegment.GetComponent<LineRenderer>().SetPosition(0, initialPosition);
                    treeSegment.GetComponent<LineRenderer>().SetPosition(1, transform.position);
                    treeSegment.GetComponent<LineRenderer>().startWidth = width;
                    treeSegment.GetComponent<LineRenderer>().endWidth = width;
                    treeSegment.transform.SetParent(Tree.transform);

                    break;
                case 'X':
                    break;
                case '+':
                    transform.Rotate(Vector3.forward * angle);
                    break;

                case '-':
                    transform.Rotate(Vector3.back * angle);
                    break;

                case '[':
                    transformStack.Push(new TransformInfo()
                    {
                        position = transform.position,
                        rotation = transform.rotation
                    });
                    break;

                case ']':
                    TransformInfo ti = transformStack.Pop();
                    transform.position = ti.position;
                    transform.rotation = ti.rotation;
                    break;

                default:
                    throw new InvalidOperationException("Invalid L-Tree operation");

            }
        }

        //length /= 2;
        //width /= 2;
        Debug.Log(currentString);
        Debug.Log("Generate");

       /*isLock = true;
        Button btn = GetComponent<Button>();
        if (_isLock)
        {

            btn.interactable = false;

        }
        else
        {
            btn.interactable = true;
        }
        StartCoroutine(countDown());
    */    
            }

    public void Generatebystep()
    {
        transform.position = initialPosition;
        transform.eulerAngles = initialRotation;
        AddValuesbystep();
        /*currentString = axiom;
        angle = float.Parse(inputFieldAngle.text);
        length = float.Parse(inputFieldLength.text);
        //width = 2f;
        iterations = 1;
        inputFieldIterations.text = iterations.ToString();
        Debug.Log("Add Values Iterations: " + iterations);
        */
        inputFieldIterations.text = iterations.ToString();

        Destroy(Tree);
        Tree = Instantiate(treeParent);

        StringBuilder sb = new StringBuilder();

        LineRenderer linerenderer = GetComponent<LineRenderer>();
        Debug.Log("drawlinebystep");


        for (int i = 0; i < iterations; i++)
        {
            foreach (char c in currentString)
            {
                sb.Append(rules.ContainsKey(c) ? rules[c] : c.ToString());
            }

            currentString = sb.ToString();
            sb = new StringBuilder();
        }


        foreach (char c in currentString)
        {
            switch (c)
            {
                case 'F':
                    Vector3 initialPosition = transform.position;
                    transform.Translate(Vector3.up * length);

                    treeSegment = Instantiate(Branch);
                    treeSegment.GetComponent<LineRenderer>().SetPosition(0, initialPosition);
                    treeSegment.GetComponent<LineRenderer>().SetPosition(1, transform.position);
                    treeSegment.GetComponent<LineRenderer>().startWidth = width;
                    treeSegment.GetComponent<LineRenderer>().endWidth = width;
                    treeSegment.transform.SetParent(Tree.transform);

                    break;
                case 'X':
                    break;
                case '+':
                    transform.Rotate(Vector3.forward * angle);
                    break;

                case '-':
                    transform.Rotate(Vector3.back * angle);
                    break;

                case '[':
                    transformStack.Push(new TransformInfo()
                    {
                        position = transform.position,
                        rotation = transform.rotation
                    });
                    break;

                case ']':
                    TransformInfo ti = transformStack.Pop();
                    transform.position = ti.position;
                    transform.rotation = ti.rotation;
                    break;

                default:
                    throw new InvalidOperationException("Invalid L-Tree operation");

            }
        }


        //length /= 2;
        //width /= 2;
        Debug.Log(currentString);
        Debug.Log("Generatebystep");
    }

    private void AddValues()
    {
        angle = float.Parse(inputFieldAngle.text);
        length = float.Parse(inputFieldLength.text);
        iterations = int.Parse(inputFieldIterations.text);
        Debug.Log("Add Values Iterations: " + iterations);
    }

    void AddValuesbystep()
    {
        angle = float.Parse(inputFieldAngle.text);
        length = float.Parse(inputFieldLength.text);
        iterations = 1;
        Debug.Log("Add Values Iterations: " + iterations);
    }

    public void ResetValues()
    {
        transform.position = initialPosition;
        transform.eulerAngles = initialRotation;
        Destroy(Tree);
        inputFieldAngle.text = "25";
        inputFieldLength.text = "5";
        //inputFieldWidth.text = "";
        inputFieldIterations.text = "1";

        currentString = axiom;

        angle = 25f;
        length = 5f;
        width = 2f;
        iterations = 1;

    }

    private void OnGUI()
    {
        float min = 20.0f;
        float max = 179.0f;
        FOVslider.minValue = min;
        FOVslider.maxValue = max;
        Camera.main.fieldOfView = FOVslider.value;

    }


    public void TreeA()
    {
        transform.position = initialPosition;
        transform.eulerAngles = initialRotation;
        angle = 25.7f;
        length = 1f;
        width = 0.5f;
        iterations = 5;
        axiom = "F";


        inputFieldAngle.text = angle.ToString();
        inputFieldLength.text = length.ToString();
        inputFieldIterations.text = iterations.ToString();
        inputFieldAxiom.text = "F";
        inputFieldRules.text = "F[+F]F[-F]F";

        rules = new Dictionary<char, string>
        {
            { 'F', "F[+F]F[-F]F" }
        };

        currentString = axiom;

        //Generate();

    }

    public void TreeB()
    {
        transform.position = initialPosition;
        transform.eulerAngles = initialRotation;
        angle = 20f;
        length = 3f;
        width = 0.5f;
        iterations = 5;
        axiom = "F";


        inputFieldAngle.text = angle.ToString();
        inputFieldLength.text = length.ToString();
        inputFieldIterations.text = iterations.ToString();
        inputFieldAxiom.text = "F";
        inputFieldRules.text = "F[+F]F[-F][F]";

        rules = new Dictionary<char, string>
        {
            {'F', "F[+F]F[-F][F]" }
        };

        currentString = axiom;

        //Generate();


    }

    public void TreeC()
    {
        transform.position = initialPosition;
        transform.eulerAngles = initialRotation;
        angle = 22.5f;
        length = 4f;
        //width = 0.5f;
        iterations = 4;
        axiom = "F";

        inputFieldAngle.text = angle.ToString();
        inputFieldLength.text = length.ToString();
        inputFieldIterations.text = iterations.ToString();
        inputFieldAxiom.text = "F";
        inputFieldRules.text = "FF-[-F+F+F]+[+F-F-F]";

        rules = new Dictionary<char, string>
        {
            {'F', "FF-[-F+F+F]+[+F-F-F]" }
        };

        currentString = axiom;

        //Generate();


    }

    public void TreeD()
    {
        transform.position = initialPosition;
        transform.eulerAngles = initialRotation;
        angle = 20f;
        length = 1f;
        //width = 0.5f;
        iterations = 7;
        axiom = "X";

        inputFieldAngle.text = angle.ToString();
        inputFieldLength.text = length.ToString();
        inputFieldIterations.text = iterations.ToString();
        inputFieldAxiom.text = "X";
        inputFieldRules.text = "F[+X]F[-X]+X";

        rules = new Dictionary<char, string>
        {
            {'X', "F[+X]F[-X]+X" },
            {'F', "FF" }
        };

        currentString = axiom;

        //Generate();


    }

    public void TreeE()
    {
        transform.position = initialPosition;
        transform.eulerAngles = initialRotation;
        angle = 25.7f;
        length = 1f;
        width = 0.5f;
        iterations = 7;
        axiom = "X";

        inputFieldAngle.text = angle.ToString();
        inputFieldLength.text = length.ToString();
        inputFieldIterations.text = iterations.ToString();
        inputFieldAxiom.text = "X";
        inputFieldRules.text = "F[+X][-X]FX";

        rules = new Dictionary<char, string>
        {
            {'X', "F[+X][-X]FX" },
            {'F', "FF" }
        };

        currentString = axiom;

        //Generate();


    }

    public void TreeF()
    {
        transform.position = initialPosition;
        transform.eulerAngles = initialRotation;
        angle = 22.5f;
        length = 3f;
        width = 0.5f;
        iterations = 5;
        axiom = "X";

        inputFieldAngle.text = angle.ToString();
        inputFieldLength.text = length.ToString();
        inputFieldIterations.text = iterations.ToString();
        inputFieldAxiom.text = "X";
        inputFieldRules.text = "F-[[X]+X]+F[+FX]-X";

        rules = new Dictionary<char, string>
        {
            {'X', "F-[[X]+X]+F[+FX]-X" },
            {'F', "FF" }
        };

        currentString = axiom;

        //Generate();


    }

    public void TreeG()
    {
        transform.position = initialPosition;
        transform.eulerAngles = initialRotation;
        angle = 90f;
        length = 3f;
        width = 1f;
        iterations = 3;
        axiom = "F+F+F+F";

        inputFieldAngle.text = angle.ToString();
        inputFieldLength.text = length.ToString();
        inputFieldIterations.text = iterations.ToString();
        inputFieldAxiom.text = "F+F+F+F";
        inputFieldRules.text = "F+F-F-FF+F+F-F";

        rules = new Dictionary<char, string>
        {
            {'F', "F+F-F-FF+F+F-F" }
        };

        currentString = axiom;

        //Generate();


    }

    public void TreeH()
    {
        transform.position = initialPosition;
        transform.eulerAngles = initialRotation;
        angle = 60f;
        length = 6f;
        width = 1f;
        iterations = 3;
        axiom = "F++F++F";

        inputFieldAngle.text = angle.ToString();
        inputFieldLength.text = length.ToString();
        inputFieldIterations.text = iterations.ToString();
        inputFieldAxiom.text = "F++F++F";
        inputFieldRules.text = "F-F++F-F";

        rules = new Dictionary<char, string>
        {
            {'F', "F-F++F-F" }
        };

        currentString = axiom;

        //Generate();


    }
}

