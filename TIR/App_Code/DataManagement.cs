using System;
using System.Data;
using System.Configuration;
////////using System.Linq;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Odbc;

/// <summary>
/// Summary description for UserManagement`````````````````````````````````````````````````````````````````````````````````````````````````
/// 
/// </summary>
    public class DataManagement : DatabaseHelper
    {
        private SqlCommand objCmd;
        //private SqlDataAdapter objSqlDA;
        public DataManagement()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #region Functions
        public int ExecuteNonQuery(string query)
        {
            objCmd = base.GetSqlCommand();
            objCmd.CommandType = CommandType.Text;
            objCmd.CommandText = query;
            return objCmd.ExecuteNonQuery();
        }
        private string[] CtrlToString(Control[] ctrls)
        {
            int i = 0;
            string[] datas = new string[ctrls.Length];
            for (i = 0; i < ctrls.Length; i++)
            {
                if (ctrls[i].GetType().ToString() == "System.Web.UI.WebControls.TextBox")
                {
                    datas[i] = ((TextBox)ctrls[i]).Text.Trim().Replace("'", "''");
                }
                else if (ctrls[i].GetType().ToString() == "System.Web.UI.WebControls.DropDownList")
                {
                    try
                    {
                        datas[i] = ((System.Web.UI.WebControls.DropDownList)ctrls[i]).SelectedValue;
                    }
                    catch { }
                }
                else if (ctrls[i].GetType().ToString() == "System.Web.UI.WebControls.CheckBox")
                {
                    if (((System.Web.UI.WebControls.CheckBox)ctrls[i]).Checked)
                        datas[i] = "1";
                    else
                        datas[i] = "0";
                }
                else if (ctrls[i].GetType().ToString() == "System.Web.UI.WebControls.RadioButtonList")
                {
                    if (((System.Web.UI.WebControls.RadioButtonList)ctrls[i]).SelectedIndex > -1)
                        datas[i] = ((System.Web.UI.WebControls.RadioButtonList)ctrls[i]).SelectedValue;
                    else datas[i] = "-1";
                }
                else if (ctrls[i].GetType().ToString() == "System.Web.UI.WebControls.HiddenField")
                {

                    datas[i] = ((System.Web.UI.WebControls.HiddenField)ctrls[i]).Value;

                }
                else
                {
                    //MessageBox.
                }
            }
            return datas;
        }
        #endregion

        #region Form entry Update
        public int InsertData(string tableName, string fields, string[] datas)
        {
            objCmd = base.GetSqlCommand();
            objCmd.CommandType = CommandType.Text;
            System.Text.StringBuilder sql = new System.Text.StringBuilder();
            sql.AppendFormat("Insert into {0} ({1}) values(", tableName, fields);
            int i = 0;
            for (i = 0; i < datas.Length - 1; i++)
            {
                if (datas[i].Trim() == "")
                    sql.Append("NULL,");
                else
                    sql.Append("'" + datas[i].Replace("'", "''").Trim() + "',");
            }
            if (datas[i].Trim() == "")
                sql.Append("NULL);");
            else
                sql.Append("'" + datas[i].Replace("'", "''").Trim() + "');");
            objCmd.CommandText = sql.ToString();
            return objCmd.ExecuteNonQuery();
            // return Convert.ToInt64(op.Value.ToString());
        }
        public int InsertData(string tableName, string[] fields, string[] datas)
        {
            objCmd = base.GetSqlCommand();
            objCmd.CommandType = CommandType.Text;
            System.Text.StringBuilder sql = new System.Text.StringBuilder();
            System.Text.StringBuilder val = new System.Text.StringBuilder();
            sql.AppendFormat("Insert into {0} (", tableName);
            val.Append(" values(");
            int i = 0;
            for (i = 0; i < datas.Length - 1; i++)
            {
                if (datas[i] != string.Empty)
                {
                    sql.Append(fields[i] + ",");
                    val.Append("'" + datas[i].Replace("'", "''").Trim() + "',");
                }
            }
            if (fields[i] != string.Empty)
            {
                sql.Append(fields[i] + ") ");
                val.Append("'" + datas[i].Replace("'", "''").Trim() + "');");
            }
            else
            {
                sql.Append( ") ");
                val.Append(");");
            }

            sql.Append(val);
            objCmd.CommandText = sql.ToString();
            try
            {
                return objCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
        public int InsertData(string tableName, string[] fields, object[] datas,SqlDbType[] types)
        {
           
            System.Text.StringBuilder sql = new System.Text.StringBuilder();
            System.Text.StringBuilder val = new System.Text.StringBuilder();
            sql.AppendFormat("Insert into {0} (", tableName);
            val.Append(" values(");
            int i = 0;
            for (i = 0; i < datas.Length - 1; i++)
            {
                sql.Append(fields[i] + ",");
                val.Append("@" + fields[i] + ",");
            }
            
                sql.Append(fields[i] + ") ");
                val.Append("@" + fields[i] + ");");

            sql.Append(val);
            objCmd = base.GetSqlCommand(sql.ToString());
            objCmd.CommandType = CommandType.Text;
            for (i = 0; i < datas.Length; i++)
            {
                objCmd.Parameters.Add("@" + fields[i], types[i]).Value=datas[i];
            }
            return objCmd.ExecuteNonQuery();
        }
        public int InsertData(string tableName, string[] fields, Control[] ctrls)
        {
            return InsertData(tableName, fields, CtrlToString(ctrls));
        }
        public int UpdateData(string tableName, string[] fields, string[] datas, string whereCondn)
        {
            objCmd = base.GetSqlCommand();
            objCmd.CommandType = CommandType.Text;
            System.Text.StringBuilder sql = new System.Text.StringBuilder();
            sql.AppendFormat("Update {0} set ", tableName);
            int i = 0;
            for (i = 0; i < datas.Length - 1; i++)
            {
                if (datas[i].Trim() == "")
                    sql.AppendFormat("{0} = NULL,", fields[i]);
                else
                    sql.AppendFormat("{0}='{1}',", fields[i], datas[i].Replace("'", "''").Trim());
            }
            if (datas[i].Trim() == "")
                sql.AppendFormat("{0} = NULL", fields[i]);
            else
                sql.AppendFormat("{0}='{1}'", fields[i], datas[i].Replace("'", "''").Trim());
            sql.AppendFormat(" where {0}", whereCondn);
            objCmd.CommandText = sql.ToString();
            return objCmd.ExecuteNonQuery();
        }
        public int UpdateData(string tableName, string[] fields, Control[] ctrls, string whereCondn)
        {
            return UpdateData(tableName, fields, CtrlToString(ctrls), whereCondn);
        }
        public int UpdateData(string tableName, string field, string data, string whereCondn)
        {
            objCmd = base.GetSqlCommand();
            objCmd.CommandType = CommandType.Text;
            System.Text.StringBuilder sql = new System.Text.StringBuilder();
            sql.AppendFormat("Update {0} set ", tableName);
            if (data.Trim() == "")
                sql.AppendFormat("{0} = NULL", field);
            else
                sql.AppendFormat(" {0}='{1}'", field, data.Replace("'", "''").Trim());
            sql.AppendFormat(" where {0}", whereCondn);
            objCmd.CommandText = sql.ToString();
            return objCmd.ExecuteNonQuery();
            // return Convert.ToInt64(op.Value.ToString());
        }
        public int DeleteData(string tableName,  string whereCondn)
        {
            objCmd = base.GetSqlCommand();
            objCmd.CommandType = CommandType.Text;
            System.Text.StringBuilder sql = new System.Text.StringBuilder();
            sql.AppendFormat("Delete from {0} ", tableName);
            sql.AppendFormat(" where {0}", whereCondn);
            objCmd.CommandText = sql.ToString();
            try
            {
                return objCmd.ExecuteNonQuery();
            }
            catch 
            {
                return -1;
            }
        }

        #endregion

        #region Fetch data
        public string GetData(string tableName, string field, string whereCondn)
        {

            objCmd = base.GetSqlCommand();
            objCmd.CommandType = CommandType.Text;
            System.Text.StringBuilder sql = new System.Text.StringBuilder();
            sql.AppendFormat("Select {1} from {0} where {2} ", tableName, field, whereCondn);
            objCmd.CommandText = sql.ToString();
            return base.ExecuteScaler(objCmd);
        }
        public string GetValue(string query)
        {
            objCmd = base.GetSqlCommand();
            objCmd.CommandType = CommandType.Text;
            objCmd.CommandText = query;
            return base.ExecuteScaler(objCmd);
        }
        public DataTable FetchData(string tableName, string fields, string whereCondn)
        {

            objCmd = base.GetSqlCommand();
            objCmd.CommandType = CommandType.Text;
            System.Text.StringBuilder sql = new System.Text.StringBuilder();
            sql.AppendFormat("Select {1} from {0} where {2} ", tableName, fields, whereCondn);
            objCmd.CommandText = sql.ToString();
            return base.GetDataResult(objCmd);
        }
        public DataTable FetchData(string tableName, string fields, string whereCondn,string orderBy)
        {
            objCmd = base.GetSqlCommand();
            objCmd.CommandType = CommandType.Text;
            System.Text.StringBuilder sql = new System.Text.StringBuilder();
            sql.AppendFormat("Select {1} from {0} where {2} order by {3}", tableName, fields, whereCondn , orderBy);
            objCmd.CommandText = sql.ToString();
            return base.GetDataResult(objCmd);
        }
        public DataTable GetData(string query)
        {
            objCmd = base.GetSqlCommand();
            objCmd.CommandType = CommandType.Text;
            objCmd.CommandText = query;
            return base.GetDataResult(objCmd);
        }
        #endregion

        #region Common Validations
        public bool IsValidEmail(string email)
        {
            string pattern = @"^[a-z][a-z|0-9|]*([_][a-z|0-9]+)*([.][a-z|0-9]+([_][a-z|0-9]+)*)?@[a-z][a-z|0-9|]*\.([a-z][a-z|0-9]*(\.[a-z][a-z|0-9]*)?)$";
            Match match = Regex.Match(email.Trim(), pattern, RegexOptions.IgnoreCase);

            if (match.Success)
                return true;
            else
                return false;
        }
        #endregion

        #region Load List
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ddl">DropDownList where data is to be filled</param> 
        /// <param name="tableName">Source Table's Name</param>
        /// <param name="keyCol">Key Value in DDL</param>
        /// <param name="valueCol">Display Value in DDL</param>
        /// <param name="addEmpty">TO Add Empty value in DropDownList.</param>
        /// <param name="emptyString">Empty String in DropDownList.</param>
        public void LoadDDL(DropDownList ddl, string tableName, string keyCol, string valueCol)
        {
            LoadDDL(ddl, tableName, keyCol, valueCol, " 1=1 ", false, "");
        }
        public void LoadDDL(DropDownList ddl, string tableName, string keyCol, string valueCol, string whereCond)
        {
            LoadDDL(ddl, tableName, keyCol, valueCol, whereCond, false, "");
        }

        public void LoadDDL(DropDownList ddl, string tableName, string keyCol, string valueCol, bool addEmpty)
        {
            LoadDDL(ddl, tableName, keyCol, valueCol, " 1=1 ", addEmpty, "");

        }
        public void LoadDDL(DropDownList ddl, string tableName, string keyCol, string valueCol, bool addEmpty,string emptyString)
        {
            LoadDDL(ddl, tableName, keyCol, valueCol, " 1=1 ", addEmpty, emptyString);

        }
        public void LoadDDL(DropDownList ddl, string tableName, string keyCol, string valueCol, string whereCond, bool addEmpty)
        {
            LoadDDL(ddl, tableName, keyCol, valueCol, whereCond, addEmpty, "");
        }
        public void LoadDDL(DropDownList ddl, string tableName, string keyCol, string valueCol, string whereCond, bool addEmpty,string emptyString)
        {
            ddl.DataTextField = valueCol;
            ddl.DataValueField = keyCol;
            DataTable tblData = FetchData(tableName, keyCol + ',' + valueCol, whereCond,valueCol);
           // tblData.DefaultView.Sort = valueCol;
           
            if (addEmpty)
            {
                DataRow row = tblData.NewRow();
                row[1] = emptyString;
                tblData.Rows.InsertAt(row, 0);
                ddl.DataSource = tblData;
            }
            else
                ddl.DataSource = tblData;

            ddl.DataBind();
        }
        public void LoadDDL(DropDownList ddl, string tableName, string keyCol, string valueCol, string whereCond, string orderCol, bool addEmpty, string emptyString)
        {
            ddl.DataTextField = valueCol;
            ddl.DataValueField = keyCol;
            DataTable tblData = FetchData(tableName, keyCol + ',' + valueCol, whereCond, orderCol);
            // tblData.DefaultView.Sort = valueCol;

            if (addEmpty)
            {
                DataRow row = tblData.NewRow();
                row[1] = emptyString;
                tblData.Rows.InsertAt(row, 0);
                ddl.DataSource = tblData;
            }
            else
                ddl.DataSource = tblData;

            ddl.DataBind();
        }
        public void LoadDDL(DropDownList ddl, string[] values)
        {
            ddl.Items.Clear();
            for (int i = 0; i < values.Length; i++)
            {
                ddl.Items.Add(new ListItem(values[i], (i+1).ToString()));
            }
        }
        public void LoadDDL(DropDownList ddl, string[] values,string[] keys)
        {
            ddl.Items.Clear();
            for (int i = 0; i < values.Length;i++ )
            {
                ddl.Items.Add(new ListItem(values[i],keys[i]));
            }
        }
        public void LoadrdbList(RadioButtonList rdoList, string tableName, string keyCol, string valueCol, string whereCond)
        {
            
            rdoList.DataTextField = valueCol;
            rdoList.DataValueField = keyCol;
            DataTable dt = FetchData(tableName, keyCol + ',' + valueCol, whereCond);
            dt.DefaultView.Sort = valueCol;
            rdoList.DataSource = dt;
            rdoList.DataBind();
        }
        public void LoadChkList(CheckBoxList chkList, string tableName, string keyCol, string valueCol, string whereCond)
        {

            chkList.DataTextField = valueCol;
            chkList.DataValueField = keyCol;
            DataTable dt = FetchData(tableName, keyCol + ',' + valueCol, whereCond);
            dt.DefaultView.Sort = valueCol;
            chkList.DataSource = dt;
            chkList.DataBind();
        }
        public void LoadChkList(CheckBoxList chkList, string tableName, string keyCol, string valueCol, string whereCond,string orderBy)
        {

            chkList.DataTextField = valueCol;
            chkList.DataValueField = keyCol;
            DataTable dt = FetchData(tableName, keyCol + ',' + valueCol, whereCond,orderBy);
            dt.DefaultView.Sort = valueCol;
            chkList.DataSource = dt;
            chkList.DataBind();
        }
        public void LoadLstBox(ListBox lstBox, string tableName, string keyCol, string valueCol, string orderby)
        {
            DataTable dt = FetchData(tableName, keyCol + ',' + valueCol," 1=1 ", orderby);
            string[] valcolName = valueCol.Trim().Split(' ');
            lstBox.DataTextField = valcolName[valcolName.Length-1];
            lstBox.DataValueField = keyCol;
            dt.DefaultView.Sort = orderby;
            lstBox.DataSource = dt;
            lstBox.DataBind();
        }
        public bool LoadLstBox(ListBox lstBox, string tableName, string keyCol, string valueCol, string whereCond, string orderby)
        {
            DataTable dt = FetchData(tableName, keyCol + ',' + valueCol, whereCond, orderby);
            if (dt.Rows.Count < 1) return false;
            string[] valcolName = valueCol.Trim().Split(' ');
            lstBox.DataTextField = valcolName[valcolName.Length - 1];
            lstBox.DataValueField = keyCol;

            dt.DefaultView.Sort = orderby;
            lstBox.DataSource = dt;
            lstBox.DataBind();
            return true;
        }
        #endregion

        #region Form Controls

        public void ClearFields(Control[] ctrl)
        {
            for (int i = 0; i < ctrl.Length; i++)
            {
                if (ctrl[i].GetType().ToString() == "System.Web.UI.WebControls.TextBox")
                {
                    ((TextBox)ctrl[i]).Text = "";
                }
                else if (ctrl[i].GetType().ToString() == "System.Web.UI.WebControls.DropDownList")
                {
                    ((System.Web.UI.WebControls.DropDownList)ctrl[i]).SelectedIndex = -1;
                }
                else if (ctrl[i].GetType().ToString() == "System.Web.UI.WebControls.CheckBox")
                {
                    ((System.Web.UI.WebControls.CheckBox)ctrl[i]).Checked = false;
                }
                else if (ctrl[i].GetType().ToString() == "System.Web.UI.WebControls.RadioButtonList")
                {
                    ((System.Web.UI.WebControls.RadioButtonList)ctrl[i]).SelectedIndex = -1;
                }
                else if (ctrl[i].GetType().ToString() == "System.Web.UI.WebControls.HiddenField")
                {
                    ((System.Web.UI.WebControls.HiddenField)ctrl[i]).Value = "";
                }
                else if (ctrl[i].GetType().ToString() == "System.Web.UI.WebControls.Label")
                {
                    ((System.Web.UI.WebControls.Label)ctrl[i]).Text = "";
                }
                else if (ctrl[i].GetType().ToString() == "System.Web.UI.WebControls.CheckBoxList")
                {
                   CheckBoxList chklist= ((System.Web.UI.WebControls.CheckBoxList)ctrl[i]);
                   foreach (ListItem chk in chklist.Items)
                   {
                       chk.Selected = false;
                   }
                }
               
            }
        }

        public void FillControls(Control[] ctrl, DataTable dt)
        {
            if (dt.Rows.Count < 1) return;
            for (int i = 0; i < ctrl.Length; i++)
            {
               
                //string aaa = ctrl[i].GetType().ToString();
                string Value = dt.Rows[0][i].ToString().Trim();
                if (dt.Columns[i].DataType.FullName == "System.DateTime" && Value!="")
                {
                    Value = Convert.ToDateTime(Value).ToString("dd-MM-yyyy");
                }
                if (ctrl[i].GetType().ToString() == "System.Web.UI.WebControls.TextBox")
                {
                    ((TextBox)ctrl[i]).Text = Value;
                }
                else if (ctrl[i].GetType().ToString() == "System.Web.UI.WebControls.DropDownList")
                {
                    try
                    {
                        ((System.Web.UI.WebControls.DropDownList)ctrl[i]).SelectedValue = Value;
                    }
                    catch { }
                }
                else if (ctrl[i].GetType().ToString() == "System.Web.UI.WebControls.CheckBox")
                {
                    if (Value == "1")
                        ((System.Web.UI.WebControls.CheckBox)ctrl[i]).Checked = true;
                    else
                        ((System.Web.UI.WebControls.CheckBox)ctrl[i]).Checked = false;

                }
                else if (ctrl[i].GetType().ToString() == "System.Web.UI.WebControls.RadioButtonList")
                {
                    try
                    {
                        ((System.Web.UI.WebControls.RadioButtonList)ctrl[i]).SelectedValue = Value;
                    }
                    catch
                    {
                    }
                }
                else if (ctrl[i].GetType().ToString() == "System.Web.UI.WebControls.HiddenField")
                {
                    ((System.Web.UI.WebControls.HiddenField)ctrl[i]).Value = Value;

                }
            }
        }

        public void FillControl(Control ctrl, string Value)
        {
                if (ctrl.GetType().ToString() == "System.Web.UI.WebControls.TextBox")
                {
                    ((TextBox)ctrl).Text = Value;
                }
                else if (ctrl.GetType().ToString() == "System.Web.UI.WebControls.DropDownList")
                {
                    try
                    {
                        ((System.Web.UI.WebControls.DropDownList)ctrl).SelectedValue = Value;
                    }
                    catch { }
                }
                else if (ctrl.GetType().ToString() == "System.Web.UI.WebControls.CheckBox")
                {
                    if (Value == "1")
                        ((System.Web.UI.WebControls.CheckBox)ctrl).Checked = true;
                    else
                        ((System.Web.UI.WebControls.CheckBox)ctrl).Checked = false;

                }
                else if (ctrl.GetType().ToString() == "System.Web.UI.WebControls.RadioButtonList")
                {
                    ((System.Web.UI.WebControls.RadioButtonList)ctrl).SelectedValue = Value;

                }
                else if (ctrl.GetType().ToString() == "System.Web.UI.WebControls.HiddenField")
                {
                    ((System.Web.UI.WebControls.HiddenField)ctrl).Value = Value;
                }
        }

        public void FillControls(Control[] ctrl, string[] Value)
        {
            for (int i = 0; i < ctrl.Length; i++)
            {

                //string aaa = ctrl[i].GetType().ToString();
                if (ctrl[i].GetType().ToString() == "System.Web.UI.WebControls.TextBox")
                {
                    ((TextBox)ctrl[i]).Text = Value[i];
                }
                else if (ctrl[i].GetType().ToString() == "System.Web.UI.WebControls.DropDownList")
                {
                    try
                    {
                        ((System.Web.UI.WebControls.DropDownList)ctrl[i]).SelectedValue = Value[i];
                    }
                    catch { }
                }
                else if (ctrl[i].GetType().ToString() == "System.Web.UI.WebControls.CheckBox")
                {
                    if (Value[i] == "1")
                        ((System.Web.UI.WebControls.CheckBox)ctrl[i]).Checked = true;
                    else
                        ((System.Web.UI.WebControls.CheckBox)ctrl[i]).Checked = false;

                }
                else if (ctrl[i].GetType().ToString() == "System.Web.UI.WebControls.RadioButtonList")
                {
                    ((System.Web.UI.WebControls.RadioButtonList)ctrl[i]).SelectedValue = Value[i];

                }
                else if (ctrl[i].GetType().ToString() == "System.Web.UI.WebControls.HiddenField")
                {
                    ((System.Web.UI.WebControls.HiddenField)ctrl[i]).Value = Value[i];
                }

            }
        }

        public void FillControls(TextBox[] txt, string[] values)
        {
            for (int i = 0; i < txt.Length; i++)
            {
                txt[i].Text = values[i];
            }
        }
        #endregion

        public int InsertLog(string tableName, string[] fields, string[] datas)
        {
            SqlCommand objCmd = GetSqlCommand();
            objCmd.CommandType = CommandType.Text;
            System.Text.StringBuilder sql = new System.Text.StringBuilder();
            System.Text.StringBuilder val = new System.Text.StringBuilder();
            sql.AppendFormat("Insert into {0} (", tableName);
            val.Append(" values(");
            int i = 0;
            for (i = 0; i < datas.Length - 1; i++)
            {
                sql.Append(fields[i] + ",");
                if (datas[i] == null) datas[i] = "";
                val.Append("'" + datas[i].Replace("'", "''") + "',");
            }
            sql.Append("logtimestamp,");
            sql.Append("userid,");
            val.Append("GetDate(),");
            val.Append("'"+SessionValues.UserIdSession + "',");
            sql.Append(fields[i] + ") ");
            val.Append("'" + datas[i].Replace("'", "''") + "');");
            sql.Append(val);
            objCmd.CommandText = "SET ANSI_WARNINGS OFF; " + sql.ToString() + " SET ANSI_WARNINGS ON;";
            return objCmd.ExecuteNonQuery();
            // return Convert.ToInt64(op.Value.ToString());
        }
    }
