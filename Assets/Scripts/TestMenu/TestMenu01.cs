using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon;

public class TestMenu01 : Photon.PunBehaviour
{
    public Button backButton;               //返回按钮
    public GameObject roomMessagePanel;     //房间信息面板
    public GameObject previousButton;       //"上一页"按钮
    public GameObject nextButton;           //"下一页"按钮
    public Text pageMessage;                //房间页数文本控件
    public GameObject JoinOrCreateButton;      //创建房间面板
    public Text RoomName;

    private RoomInfo[] roomInfo;            //游戏大厅房间列表信息
    private int currentPageNumber;          //当前房间页
    private int maxPageNumber;              //最大房间页
    private int roomPerPage = 4;            //每页显示房间个数
    private GameObject[] roomMessage;       //游戏房间信息

    public GameObject Menu00;
    public GameObject Menu02;
    


    //当游戏大厅面板启用时调用，初始化信息
    void OnEnable()
    {
        currentPageNumber = 1;              //初始化当前房间页
        maxPageNumber = 1;                  //初始化最大房间页	

        //获取房间信息面板
        RectTransform rectTransform = roomMessagePanel.GetComponent<RectTransform>();
        roomPerPage = rectTransform.childCount;     //获取房间信息面板的条目数

        //初始化每条房间信息条目
        roomMessage = new GameObject[roomPerPage];
        for (int i = 0; i < roomPerPage; i++)
        {
            roomMessage[i] = rectTransform.GetChild(i).gameObject;
            roomMessage[i].SetActive(false);            //禁用房间信息条目
        }
    }
    
    /**覆写IPunCallback回调函数，当房间列表更新时调用
	 * 更新游戏大厅中房间列表的显示
	 */
    public override void OnReceivedRoomListUpdate()
    {
        roomInfo = PhotonNetwork.GetRoomList();                 //获取游戏大厅中的房间列表
        maxPageNumber = (roomInfo.Length - 1) / roomPerPage + 1;    //计算房间总页数
        if (currentPageNumber > maxPageNumber)      //如果当前页大于房间总页数时
            currentPageNumber = maxPageNumber;      //将当前房间页设为房间总页数
        pageMessage.text = currentPageNumber.ToString() + "/" + maxPageNumber.ToString();   //更新房间页数信息的显示
        ButtonControl();        //翻页按钮控制
        ShowRoomMessage();      //显示房间信息
    }

    //显示房间信息
    void ShowRoomMessage()
    {
        int start, end, i, j;
        start = (currentPageNumber - 1) * roomPerPage;          //计算需要显示房间信息的起始序号
        if (currentPageNumber * roomPerPage < roomInfo.Length)  //计算需要显示房间信息的末尾序号
            end = currentPageNumber * roomPerPage;
        else
            end = roomInfo.Length;

        //依次显示每条房间信息
        for (i = start, j = 0; i < end; i++, j++)
        {
            RectTransform rectTransform = roomMessage[j].GetComponent<RectTransform>();
            string roomName = roomInfo[i].Name; //获取房间名称
            rectTransform.GetChild(0).GetComponent<Text>().text = (i + 1).ToString();   //显示房间序号
            rectTransform.GetChild(1).GetComponent<Text>().text = roomName;             //显示房间名称
            rectTransform.GetChild(2).GetComponent<Text>().text
                = roomInfo[i].PlayerCount + "";
            Button button = rectTransform.GetChild(3).GetComponent<Button>();               //获取"进入房间"按钮组件
                                                                                            //如果游戏房间的Open属性为false（房间内游戏已开始），表示房间无法加入，禁用"进入房间"按钮
            if (roomInfo[i].IsOpen == false)
                button.gameObject.SetActive(false);
            //如果房间可以加入，启用"进入房间"按钮，给按钮绑定新的监听事件，加入此房间
            else
            {
                button.gameObject.SetActive(true);
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(delegate () {
                    ClickJoinRoomButton(roomName);
                });
            }
            roomMessage[j].SetActive(true); //启用房间信息条目
        }
        //禁用不显示的房间信息条目
        while (j < 4)
        {
            roomMessage[j++].SetActive(false);
        }
    }

    //翻页按钮控制函数
    void ButtonControl()
    {
        JoinOrCreateButton.SetActive(true);
        //如果当前页为1，禁用"上一页"按钮；否则，启用"上一页"按钮
        if (currentPageNumber == 1)
            previousButton.SetActive(false);
        else
            previousButton.SetActive(true);
        //如果当前页等于房间总页数，禁用"下一页"按钮；否则，启用"下一页"按钮
        if (currentPageNumber == maxPageNumber)
            nextButton.SetActive(false);
        else
            nextButton.SetActive(true);
    }

    //"创建房间"按钮事件处理函数，启用创建房间面板
    public void ClickJoinOrCreateButton()
    {
        if (RoomName.text == "")
            PhotonNetwork.JoinOrCreateRoom("Room" + Random.Range(1, 9999), null, null);
        else
            PhotonNetwork.JoinOrCreateRoom(RoomName.text, null, null);
        SwtichToMenu02();
    }

    //"上一页"按钮事件处理函数
    public void ClickPreviousButton()
    {
        currentPageNumber--;        //当前房间页减一
        pageMessage.text = currentPageNumber.ToString() + "/" + maxPageNumber.ToString();   //更新房间页数显示
        ButtonControl();            //当前房间页更新，调动翻页控制函数
        ShowRoomMessage();          //当前房间页更新，重新显示房间信息
    }

    //"下一页"按钮事件处理函数
    public void ClickNextButton()
    {
        currentPageNumber++;        //当前房间页加一
        pageMessage.text = currentPageNumber.ToString() + "/" + maxPageNumber.ToString();   //更新房间页数显示
        ButtonControl();            //当前房间页更新，调动翻页控制函数
        ShowRoomMessage();          //当前房间页更新，重新显示房间信息
    }

    //"进入房间"按钮事件处理函数
    public void ClickJoinRoomButton(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName);   //根据房间名加入游戏房间
        SwtichToMenu02();
    }

    public void ClickBackButton()
    {
        PhotonNetwork.Disconnect();
        SwtichToMenu00();
    }

    public void SwtichToMenu00()
    {
        gameObject.SetActive(false);
        Menu02.SetActive(false);
        Menu00.SetActive(true);
    }

    public void SwtichToMenu02()
    {
        Menu00.SetActive(false);
        gameObject.SetActive(false);
        Menu02.SetActive(true);
    }
}
