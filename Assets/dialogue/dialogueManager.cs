using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class DialogueManager : MonoBehaviour // ����Ӧ�Դ�д��ĸ��ͷ
{
    public TextAsset dialogueDataFile; // �Ի������ļ�
    public Image image; // ͼ�����
    public TMP_Text nameText; // �����ı����
    public TMP_Text dialogueText; // �Ի��ı����\
    public List<Sprite> sprites = new List<Sprite>(); // �����б�
    Dictionary<string, Sprite> imageDic = new Dictionary<string, Sprite>(); // ͼ���ֵ�
    public int dialogIndex; // ��ǰ�Ի�����
    public string[] dialogRows; // ӦΪ string ���飬�Դ洢ÿһ�еĶԻ�
    public Button next; //��һ����ť
    public GameObject option; //ѡ��
    public Transform buttonGroup;//���ڵ�
    public List<Person> people = new List<Person>();
    public Canvas canvas; // �������� Canvas
                          // Start is called before the first frame update
    public void HideCanvas()
    {
        if (canvas != null)
        {
            canvas.gameObject.SetActive(false); // ���� Canvas
        }
    }
    private void Awake() 
    {
        Person person = new Person();
        person.name = "Amet";
        people.Add(person);
        Person Amalur =new Person();
        Amalur.name = "Amalur";
        people.Add(Amalur);
    }
    void Start()
    {
        dialogIndex = 1;
        readText(dialogueDataFile); // �ȶ�ȡ�Ի��ı�
        ShowDialogRow(); // Ȼ����ʾ�Ի�
    }

    public void updateText(string _name, string _text)
    {
        nameText.text = _name; // ���������ı�
        dialogueText.text = _text; // ���¶Ի��ı�
    }

    public void updateImage(string _name)
    {
        // ����������������ָ���ͼ��
        if (imageDic.TryGetValue(_name, out Sprite sprite))
        {
            image.sprite = sprite; // ����ͼ��
        }
    }

    public void readText(TextAsset _textAsset)
    {
        dialogRows = _textAsset.text.Split(new[] { "\n" }, System.StringSplitOptions.None); // ���зָ��ı�
        Debug.Log("Total Rows: " + dialogRows.Length); // ����е�����
    }

    public void ShowDialogRow()
    {
        for (int i=1;i<dialogRows.Length;i++) // ����Ϊ�ַ������飬��ȷ����
        {
            string[] cells = dialogRows[i].Split(','); // �����ŷָ�
            Debug.Log($"Processing row: {i} - {dialogRows[i]}");
            if (cells[0] == "�Ի�" && int.Parse(cells[1]) == dialogIndex)
            {
                updateText(cells[2], cells[3]); // �����ı�
                updateImage(cells[2]); // ����ͼ��
                dialogIndex = int.Parse(cells[4]); // ��������
                dialogIndex = int.Parse(cells[4]); // ��������
                next.gameObject.SetActive(true);
                break;
            }  
             else if (cells[0] == "ѡ��" && int.Parse(cells[1]) == dialogIndex) 
            {
                next.gameObject.SetActive(false);
                GenerateOption(i);
            }
            else if (cells[0] =="END" && int.Parse(cells[1]) == dialogIndex) 
            {
                Debug.Log("�������");
                HideCanvas();
            }
        }
    }
    public void onClickNext()
    {
        ShowDialogRow();
    }
    public void GenerateOption(int _index)
    {
        string[] cells = dialogRows[_index].Split(",");
        if (cells[0] =="ѡ��")
        {
            GameObject button = Instantiate(option, buttonGroup);
            //�󶨰�ť�¼�
            button.GetComponentInChildren<TMP_Text>().text = cells[3];
            button.GetComponent<Button>().onClick.AddListener(
                delegate {
                OnOptionClick(int.Parse(cells[4]));
                    if (cells[5] != "") 
                    {
                        string[] effect = cells[5].Split('@');
                        Debug.Log(effect[1]);
                        OptionEffect(effect[0],int.Parse(effect[1]), "Amet");
                    }
                 }
        );
            GenerateOption(_index + 1);
        }
    }
    public void OnOptionClick(int _id)
    {
        dialogIndex = _id;
        ShowDialogRow();
        for(int i = 0;i<buttonGroup.childCount;i++)
        {
            Destroy(buttonGroup.GetChild(i).gameObject);
        }
    }
    public void OptionEffect(string _effect, int _param, string _target) 
    {
        if(_effect =="�øж�")
        {
            foreach(var person in people) 
            {
                if (person.name == _target) 
                {
                    person.likeValue += _param;
                }
            }
        }
        else if(_effect == "����ֵ")
        {
            foreach (var person in people)
            {
                if (person.name == _target)
                {
                    person.tolerence += _param;
                }
            }
        }
    }
}
