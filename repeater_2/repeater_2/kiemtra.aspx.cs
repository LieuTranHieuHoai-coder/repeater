using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
namespace repeater_2
{
    public partial class kiemtra : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-B1PJQ57\HIEUHOAI;Initial Catalog=Repeater;Integrated Security=True");
            conn.Open();
            string query = "select * from sp";
            
            if (Request["key"]!=null)
            {
                query = "select * from sp where name like '%"+ Request["key"].ToString() +"%'";
            }
            SqlDataAdapter da = new SqlDataAdapter(query,conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            int so_item_1_trang = 3;
            int so_trang = (dt.Rows.Count / so_item_1_trang) + (dt.Rows.Count % so_item_1_trang == 0 ? 0 : 1);
            int page = Request["page"] == null ? 1 : Convert.ToInt32(Request["page"].ToString());
            int from = (page - 1) * 3;
            int to = page * 3 - 1;
            for (int i = dt.Rows.Count - 1; i >=0; i--)
            {
                if(i < from || i > to)
                {
                    dt.Rows.RemoveAt(i);
                }
            }
            Repeater1.DataSource = dt;
            Repeater1.DataBind();
            conn.Close();
            if (Request["id"]!=null)
            {
                query = "select * from sp where id = '" + Request["id"] + "'";
                SqlDataAdapter list = new SqlDataAdapter(query, conn);
                DataTable dtlist = new DataTable();
                list.Fill(dtlist);
                TextBox1.Text = dtlist.Rows[0]["name"].ToString();
                Image1.Visible = true;
                Image1.ImageUrl = "~/img/" + dtlist.Rows[0]["img"].ToString();
                DropDownList1.SelectedValue = dtlist.Rows[0]["stt"].ToString();
            }
            DataTable dtPage = new DataTable();
            dtPage.Columns.Add("index");
            dtPage.Columns.Add("active");
            for (int i = 1; i <= so_trang; i++)
            {
                DataRow dr = dtPage.NewRow();
                dr["index"] = i;

                dtPage.Rows.Add(dr);
            }
            Repeater2.DataSource = dtPage;
            Repeater2.DataBind();
        }

        protected void btn_tim_Click(object sender, EventArgs e)
        {
            Response.Redirect("kiemtra.aspx?key="+txt_key.Text);
        }
        //nút thêm
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile)
            {
                SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-B1PJQ57\HIEUHOAI;Initial Catalog=Repeater;Integrated Security=True");
                conn.Open();
                
                string unit = Path.GetExtension(FileUpload1.FileName);
                if (unit==".jpg"||unit==".png")
                {
                    string path = Server.MapPath("img\\");
                    string name = FileUpload1.FileName;
                    FileUpload1.SaveAs(path + name);

                    string insert = "insert into sp values('"+TextBox1.Text+"','" + name + "','"+DropDownList1.SelectedValue+"')";
                    SqlCommand cmd = new SqlCommand(insert, conn);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        protected void btn_xoa_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-B1PJQ57\HIEUHOAI;Initial Catalog=Repeater;Integrated Security=True");
            conn.Open();

            string xoa = "update sp set stt = 0 where id = '" + Request["id"] + "'";
            SqlCommand cmd = new SqlCommand(xoa, conn);
            cmd.ExecuteNonQuery();
            Response.Write("xoa thành công");
        }

        protected void btn_update_Click(object sender, EventArgs e)
        {

        }
    }
}