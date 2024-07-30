using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class DialogueManager : MonoBehaviour // 类名应以大写字母开头
{
    public TextAsset dialogueDataFile; // 对话数据文件
    public Image image; // 图像组件
    public TMP_Text nameText; // 名称文本组件
    public TMP_Text dialogueText; // 对话文本组件\
    public List<Sprite> sprites = new List<Sprite>(); // 精灵列表
    Dictionary<string, Sprite> imageDic = new Dictionary<string, Sprite>(); // 图像字典
    public int dialogIndex; // 当前对话索引
    public string[] dialogRows; // 应为 string 数组，以存储每一行的对话
    public Button next; //下一个按钮
    public GameObject option; //选项
    public Transform buttonGroup;//父节点
    public List<Person> people = new List<Person>();
    public Canvas canvas; // 用于引用 Canvas
                          // Start is called before the first frame update
    public void HideCanvas()
    {
        if (canvas != null)
        {
            canvas.gameObject.SetActive(false); // 隐藏 Canvas
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
        readText(dialogueDataFile); // 先读取对话文本
        ShowDialogRow(); // 然后显示对话
    }

    public void updateText(string _name, string _text)
    {
        nameText.text = _name; // 更新名称文本
        dialogueText.text = _text; // 更新对话文本
    }

    public void updateImage(string _name)
    {
        // 假设这里会依据名字更新图像
        if (imageDic.TryGetValue(_name, out Sprite sprite))
        {
            image.sprite = sprite; // 更新图像
        }
    }

    public void readText(TextAsset _textAsset)
    {
        dialogRows = _textAsset.text.Split(new[] { "\n" }, System.StringSplitOptions.None); // 按行分割文本
        Debug.Log("Total Rows: " + dialogRows.Length); // 输出行的数量
    }

    public void ShowDialogRow()
    {
        for (int i=1;i<dialogRows.Length;i++) // 现在为字符串数组，正确迭代
        {
            string[] cells = dialogRows[i].Split(','); // 按逗号分割
            Debug.Log($"Processing row: {i} - {dialogRows[i]}");
            if (cells[0] == "对话" && int.Parse(cells[1]) == dialogIndex)
            {
                updateText(cells[2], cells[3]); // 更新文本
                updateImage(cells[2]); // 更新图像
                dialogIndex = int.Parse(cells[4]); // 更新索引
                dialogIndex = int.Parse(cells[4]); // 更新索引
                next.gameObject.SetActive(true);
                break;
            }  
             else if (cells[0] == "选项" && int.Parse(cells[1]) == dialogIndex) 
            {
                next.gameObject.SetActive(false);
                GenerateOption(i);
            }
            else if (cells[0] =="END" && int.Parse(cells[1]) == dialogIndex) 
            {
                Debug.Log("剧情结束");
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
        if (cells[0] =="选项")
        {
            GameObject button = Instantiate(option, buttonGroup);
            //绑定按钮事件
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
        if(_effect =="好感度")
        {
            foreach(var person in people) 
            {
                if (person.name == _target) 
                {
                    person.likeValue += _param;
                }
            }
        }
        else if(_effect == "体力值")
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
