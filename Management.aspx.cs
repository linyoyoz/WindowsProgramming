using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;

public partial class Management : System.Web.UI.Page
{
    static string connString = WebConfigurationManager.ConnectionStrings["bustest001ConnectionString"].ConnectionString;
    //busID儲存值
    int[] busID = new int[30];
    bool[] use = new bool[30];
    int number;
    int RouteID;
    String test = ""; 

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        SqlConnection connA = new SqlConnection(connString);
        connA.Open();
        SqlCommand cmdA = new SqlCommand("select RouteID from Route where RouteNumber = @RouteNumber and Direction = @Direction", connA);
            
            cmdA.Parameters.Add("@RouteNumber", SqlDbType.Char).Value = TextBox1.Text;
        if(RadioButtonList1.Items[0].Selected)
            cmdA.Parameters.Add("@Direction", SqlDbType.Int).Value = 1;
        else
            cmdA.Parameters.Add("@Direction", SqlDbType.Int).Value = 0;

        ///
        ///select RouteID
        ///from Route
        ///where RouteNumber = @RouteNumber
        ///and Direction = @Direction
        ///

        SqlDataReader drA;
        drA = cmdA.ExecuteReader();
        
        try
        {
            if (drA.Read())
            {
                RouteID = (int)drA[0];
            }

        }
        finally
        {
            cmdA.Dispose();
            connA.Close();
            connA.Dispose();
        }
        ///將日期值存到Date內
        Session["Date"] = Calendar1.SelectedDate.ToShortDateString();
        ///將路線存到RouteID內
        Session["RouteID"] = RouteID;
        searchFrequency();
        add();
        Label1.Text = test;
        ///
        Response.Redirect("~/DetailManageMent.aspx");
    }
    void searchFrequency()
    {
        number = 0;
        SqlConnection connB = new SqlConnection(connString);
        connB.Open();
        SqlCommand cmdB = new SqlCommand(" select ConstantFrequencyID from ConstantFrequency where RouteID = @RouteID", connB);
        cmdB.Parameters.Add("@RouteID", SqlDbType.Int).Value = RouteID;
        ///
        ///select ConstantFrequencyID,StartTime
        ///from ConstantFrequency
        ///where RouteID = @RouteID
        ///
        SqlDataReader drB;
        drB = cmdB.ExecuteReader();
        try
        {
            while(drB.Read())
            {
                busID[number] = (int)drB[0];
                use[number] = false;
                number++;
            }

        }
        finally
        {
            cmdB.Dispose();
            connB.Close();
            connB.Dispose();
        }
    }
    void add()
    {
        for (int i = 0; i < number; i++)
        {
            SqlConnection connC = new SqlConnection(connString);
            connC.Open();
            ///找到沒有的任務
            SqlCommand cmdC = new SqlCommand("select CaseID from Cases where ConstantFrequencyID = @BusID and UseDate=@UseDate", connC);
            cmdC.Parameters.Add("@BusID", SqlDbType.Int).Value = busID[i];
            cmdC.Parameters.Add("@UseDate", SqlDbType.DateTime).Value = Calendar1.SelectedDate.ToShortDateString();
            ///
            ///select CaseID
            ///from Cases
            ///where BusID = 2 and UseDate='2017/05/24'
            ///
            SqlDataReader drC;
            drC = cmdC.ExecuteReader();
            test += busID[i]+" ";
            try
            {
                if (drC.Read())
                {
                    test += "CaseID:"+drC[0].ToString();
                    use[i] = true;
                }
                else
                {
                    test += "無任務";
                }
            }
            finally
            {
                cmdC.Dispose();
                connC.Close();
                connC.Dispose();
            }
            test += use[i]+"<br>";
        }
        
        for (int i = 0; i < number; i++)
        {
            int size = 0;
            SqlConnection connC = new SqlConnection(connString);
            connC.Open();
            ///取得任務總數量
            SqlCommand cmdC = new SqlCommand("select count(*) from Cases", connC);
            SqlDataReader drC;
            drC = cmdC.ExecuteReader();
            try
            {
                if (drC.Read())
                {
                    size = (int)drC[0];
                    //test += size+"<br>";
                }
            }
            finally
            {
                cmdC.Dispose();
                connC.Close();
                connC.Dispose();
            }

            if (!use[i])
            {
                SqlConnection connD = new SqlConnection(connString);
                connD.Open();
                ///插入沒有的任務
                SqlCommand cmdD = new SqlCommand("insert into Cases(CaseID,ConstantFrequencyID,UseDate,DriverID,BusID) values(@size,@BusID,@UseDate,0,0)", connD);
                cmdD.Parameters.Add("@size", SqlDbType.Int).Value = size+1;
                cmdD.Parameters.Add("@BusID", SqlDbType.Int).Value = busID[i];
                cmdD.Parameters.Add("@UseDate", SqlDbType.DateTime).Value = Calendar1.SelectedDate.ToShortDateString();
                SqlDataReader drD;
                drD = cmdD.ExecuteReader();
                try
                {
                    test += "新增車次" + (size + 1) + "時刻ID:" + busID[i] + "日期:"+Calendar1.SelectedDate.ToShortDateString()+"<br>";
                }
                finally
                {
                    cmdD.Dispose();
                    connD.Close();
                    connD.Dispose();
                }
            }     
        }
    }
}