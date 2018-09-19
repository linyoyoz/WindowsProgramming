using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Data;
using System.Data.SqlClient;

public partial class WebSite7_List1 : System.Web.UI.Page
{
    int a = 0,car, routeId,i=0;
    string ddd,starttime;
    Boolean direction;
    protected void Page_Load(object sender, EventArgs e)
    {
         
    }
    protected void set()
    {
        car = Convert.ToInt32(carname.Text);
        string connString = WebConfigurationManager.ConnectionStrings["bustest001ConnectionString"].ConnectionString;
        SqlConnection connB = new SqlConnection(connString);
        connB.Open();
        SqlCommand cmd,cmd1;
        cmd = new SqlCommand("SELECT RouteID from Route where RouteNumber=@carname and Direction=@direction; ", connB);
        cmd.Parameters.AddWithValue("@carname", car);
        cmd.Parameters.AddWithValue("@direction", direction);
        SqlDataReader dr = cmd.ExecuteReader();
        while(dr.Read())
        {
            routeId = Convert.ToInt32(dr["RouteID"]);
            //direction = (Boolean)dr["Direction"];
        }
        connB.Close();
        //test.Text = routeId.ToString();
        connB.Open();
        cmd1 = new SqlCommand("SELECT StartTime from ConstantFrequency where RouteID=@routeId; ", connB);
        cmd1.Parameters.AddWithValue("@routeId", routeId);
        SqlDataReader dr1 = cmd1.ExecuteReader();
        while(dr1.Read())
        {
            starttime = dr1["StartTime"].ToString();
            ListBox1.Items.Add(new ListItem( i+1 +"."+"  "+starttime + "\n","i"));
            i++;
        }
        connB.Close();
    }
    protected bool CheckInput()
    {
        bool status = true;
        if (string.IsNullOrEmpty(carname.Text))
        {
            carname.Text = "";
            status = false;
            Label1.Text = "*請輸入名字";
        }
        if(RadioButtonList1.SelectedItem.Value=="go")
        {
            direction = true;
        }
        else if(RadioButtonList1.SelectedItem.Value == "back")
        {
            direction = false;
        }
        return status;
    }
        protected void Button1_Click(object sender, EventArgs e)
    { 
        if (CheckInput() == false)
        {

        }
        else
        {
            set();
        }
    }

}