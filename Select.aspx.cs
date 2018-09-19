using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;

public partial class Select : System.Web.UI.Page
{
    //無限大
    const int inf = 1 << 20;
    //節點最大數量
    const int N = 5000;
    //測試輸出
    String test = "";
    String ans = "";
    static string connString = WebConfigurationManager.ConnectionStrings["bustest001ConnectionString"].ConnectionString;
    
    
    /// <summary>
    /// 包含車站和路線以及車次的節點
    /// </summary>
    public struct node
    {
        public void setnode(int b, int s, int r,TimeSpan t)
        {
            bus = b;
            stop = s;
            route = r;
            arrive = t;
        }
        public int bus;
        public int stop;
        public int route;
        public TimeSpan arrive;
        public int routeName;
        public String stopName;
    };
    node[] n = new node[N];
    int total = 2;

    //二維陣列紀錄兩節點的最初距離
    int[,]  D = new int[N,N];
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Label1.Text = "";
        ans = "";
        int startName = findStop(TextBox1.Text);
        int endName = findStop(TextBox2.Text);
        int hour = Int32.Parse(DropDownList1.SelectedItem.Value);
        int minute = Int32.Parse(DropDownList2.SelectedItem.Value);
        int retrace = Int32.Parse(RadioButtonList1.SelectedItem.Value);
        if (startName != -1 && endName != -1&&startName!=endName&&hour!=-1&&minute!=-1)
        {
            //給使用者資訊
            ans += "起點:" + startName.ToString() + TextBox1.Text + " 終點:" + endName.ToString() + TextBox2.Text + "<br>";
            ans += "時間" + hour.ToString() + ":" + minute.ToString()+"<br>";
            n[0].stopName = TextBox1.Text;
            n[1].stopName = TextBox2.Text;
            //節點設定
            set(new TimeSpan(hour, minute, 0), retrace);
            //算出所有點的最初距離
            cal(startName, endName, new TimeSpan(hour, minute, 0));
            //最短路徑演算法
            Dijkstra();
            find(1, 0);
            Label1.Text += ans;
            //要看節點資訊可以看這行
            //Label1.Text += test;

        }
        else
        {
            if (startName == -1)
                Label1.Text += "起點找不到站牌"+TextBox1.Text+"<br>";
            if (endName == -1)
                Label1.Text += "終點找不到站牌" + TextBox2.Text+"<br>";
            if (hour == -1 || minute==-1)
                Label1.Text += "請選擇時間<br>";
            if(startName==endName)
                Label1.Text += "站名不可以相同<br>";
        }
       
    }
    protected int findStop(String x)
    {
        int ans=-1;
        SqlConnection connC = new SqlConnection(connString);
        connC.Open();
        SqlCommand cmdC = new SqlCommand("Select StopID from Stop Where Stop.StopName = @StopName", connC);
        cmdC.Parameters.Add("@StopName", SqlDbType.Char).Value = x;
        SqlDataReader drC;
        drC = cmdC.ExecuteReader();
        try
        {
            if (drC.Read())
            {
                ans = (int)drC[0];
            }

        }
        finally
        {
            cmdC.Dispose();
            connC.Close();
            connC.Dispose();
        }
        return ans;
    }
    //計算所有節點的距離
    protected void cal(int s,int t,TimeSpan time)
    {
        //起點為0站牌為 s，終點為1 站牌為 t 時間time歸到起點
        n[0].setnode(0, s, 0,time);
        n[1].setnode(-1, t, -1,new TimeSpan(12,0,0));
        for(int i=0;i<total;i++)
            for (int j = 0; j < total; j++)
            {
                //當站牌相同時
                if (n[i].stop == n[j].stop)
                {
                    TimeSpan ts = n[j].arrive - n[i].arrive;
                    //站牌為終點時節點到終點的時間為0
                    if (n[1].stop == n[i].stop || n[1].stop == n[j].stop)
                    {
                        test += "D:" + ts.Hours.ToString() + ":" + D[i, j].ToString() + " " + "i:" + i.ToString() + " j" + j.ToString() + "<br>";
                        D[i, j] = 0;
                    }
                    //當ts為正數表示等待ts分鐘便可以等車從i到j
                    else if (ts.Hours*60+ts.Minutes >= 0)
                    {
                        D[i, j] = ts.Hours * 60 + ts.Minutes;
                        test += "D:" + ts.Hours.ToString() + ":" + D[i, j].ToString() + " " + "i:" + i.ToString() + " j" + j.ToString() + "<br>";
                    }
                    else
                        D[i, j] = inf;
                    //不是則為無限大
                }
                else if (n[i].bus == n[j].bus && n[i].route == n[j].route)
                {
                    TimeSpan ts = n[j].arrive - n[i].arrive;
                    //當ts為正數表示等待ts分鐘便可以坐車從i的站到j的站
                    if (ts.Hours*60+ts.Minutes >= 0)
                    {
                        D[i, j] = ts.Hours * 60 + ts.Minutes;
                        test += "D:" + ts.Hours.ToString()+":"+D[i, j].ToString() + " " + "i:" + i.ToString() + " j" + j.ToString() + "<br>";
                    }
                    else
                        D[i, j] = inf;
                }
                else
                    D[i, j] = inf;
            }
        
    }
    //算出最短路徑
    int[] touch = new int[N];
    protected void Dijkstra()
    {
        int[] length = new int[N];
        int tt = total;
        int unear=0;
        length[0] = 0;
        for (int i = 1; i < total; i++)
        {
            //當i和起點有連接時
            if (D[0, i] < inf)
                touch[i] = 0;
            else
                touch[i] = -1;
            //沒有時為-1
            length[i] = D[0, i];
            
        }
        while (tt-- > 0)
        {
            int min = inf;
            for (int i = 1; i < total; i++)
            {
                if (0 <= length[i] && length[i] < min)
                {
                    min = length[i];
                    unear = i;
                }
            }
            if (unear == 0)
                break;
            for (int i = 1; i < total; i++)
            {
                if (length[unear] + D[unear, i] < length[i]&&length[unear]!=-1)
                {
                    length[i] = length[unear] + D[unear, i];
                    touch[i] = unear;
                }
            }
            length[unear] = -1;
        }
    }
    protected void find(int x,int findtime)
    {
        //touch [x] 0 到 touch[x] 到 x
        //假如find超過10次直接退回或是x找不到連接點
        if (findtime > 10 || touch[x]==-1)
        {
            ans += x.ToString() + "找不到合適的路徑<br>";
            return;
        }
        else
            findtime++;
        if (touch[x] == 0)
        {
                ans += "由" + n[x].stopName.ToString() + "等待" + D[0, x].ToString() + "分鐘後搭乘" + n[x].routeName.ToString() + "號<br>";
                
        }
        else
        {
            find(touch[x],findtime);
            if (n[x].stop == n[touch[x]].stop)
            {
                if (n[x].stop == n[1].stop)
                {
                    ans += "到達地點"+n[touch[x]].stopName.ToString()+"<br>";
                }
                else
                    ans += "在" + n[x].stopName.ToString() + "等待" + D[touch[x], x].ToString() + "分鐘後搭乘" + n[x].routeName.ToString() + "號<br>";
            }
            else
            {
                ans += "從公車" + n[touch[x]].routeName.ToString() + "號由" + n[touch[x]].stopName.ToString() + "到" + n[x].stopName.ToString() + "約花費" + D[touch[x], x] + "分鐘<br>";
            }
        }
    }
    protected void set(TimeSpan startTime,int retrace)
    {
        
            SqlConnection connB = new SqlConnection(connString);
            connB.Open();
            SqlCommand cmdB = new SqlCommand("Select ConstantFrequencyID,WaitBusID,ConstantFrequency.RouteID,Frequency,Stop.StopID,delayMinutes,StartTime,StopName,RouteNumber,direction from WaitBus,ConstantFrequency,Stop,Route Where WaitBus.RouteID = ConstantFrequency.RouteID and WaitBus.StopID = Stop.StopID and ConstantFrequency.RouteID = Route.RouteID", connB);
            SqlDataReader drB = cmdB.ExecuteReader();
            try
            {
                while(drB.Read())
                {
                    TimeSpan updateTime = ((DateTime)drB[6]).TimeOfDay.Add(TimeSpan.FromMinutes((int)drB[5]));
                    if (retrace != 0)
                    {
                        test += updateTime.ToString() + ">" + startTime.ToString() + ">" + updateTime.Add(TimeSpan.FromMinutes(-retrace)) + "<br>";
                        if (TimeSpan.Compare(startTime, updateTime) > 0 || TimeSpan.Compare(startTime, updateTime.Add(TimeSpan.FromMinutes(-retrace))) < 0)
                            continue;
                    }
                    else
                    {
                        test += updateTime.ToString() + ">" + startTime.ToString()+"<br>";
                        if (TimeSpan.Compare(startTime, updateTime) > 0)
                            continue;
                    }
                    //取得正確的節點n
                    n[total].stop = (int)drB[4];
                    n[total].route = (int)drB[2];
                    n[total].bus = (int)drB[3];
                    n[total].routeName = (int)drB[8];
                    n[total].arrive = ((DateTime)drB[6]).TimeOfDay.Add(TimeSpan.FromMinutes((int)drB[5]));
                    n[total].stopName = drB[7].ToString();
                    test = test + "id:"+total.ToString() + " stop:" + n[total].stop.ToString()+" "+n[total].stopName + " route:" + n[total].route.ToString() + " bus:"+n[total].bus + " dis:"+drB[5].ToString()+"Time:" +n[total].arrive.ToString()+ "<br>";
                    total++;
                }
            }
            finally
            {
                cmdB.Dispose();
                connB.Dispose();
                connB.Close();
            }
            ///Select BusID,WaitBusID,Bus.RouteID,BusNumber,Stop.StopID,delayMinutes,starttime,StopName,RouteName,direction
            ///from WaitBus,ConstantFrequency,Stop,Route
            ///Where WaitBus.RouteID = Bus.RouteID 
            ///and WaitBus.StopID = Stop.StopID 
            ///and Bus.RouteID = Route.RouteID
        ///}
        
    }
}