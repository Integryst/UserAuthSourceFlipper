// Copyright (c) 2012 Integryst, LLC, http://www.integryst.com/
// See LICENSE.txt for licensing information

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.OracleClient;
using System.Collections;

namespace UserAuthSourceFlipper
{
    public partial class Form1 : Form
    {
        // when swapping users, this ID will be used as an intermediary
        // there must be no existing account with this ID
        private static int RESERVED_USERID = 101;
        private static int RESERVED_GROUPID = 101;

        public Form1()
        {
            InitializeComponent();
        }

        private void rdoSQLServer_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoSQLServer.Checked)
                txtDBPort.Text = "1433";
        }

        private void rdoOracle_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoOracle.Checked)
                txtDBPort.Text = "1521";
        }

        private DbConnection getDBConnection()
        {
            if (rdoOracle.Checked)
                return getOracleDBConnection();
            else
                return getSQLDBConnection();
        }

        private SqlConnection getSQLDBConnection()
        {
            SqlConnection oSQLConn = null;
            string connectionString = "";

            try
            {
                oSQLConn = new SqlConnection();
                connectionString = "Server=" + txtDBServer.Text + "," + txtDBPort.Text
                    + ";Database=" + txtDBName.Text
                    + ";User ID=" + txtDBUser.Text
                    + ";Password=" + txtDBPass.Text
                    + ";Trusted_Connection=False;";
                oSQLConn.ConnectionString = connectionString;
            }
            // catch exception when error in connecting to database occurs
            catch (Exception ex)
            {
                MessageBox.Show("An exception occurred connecting to SQL Server [" + connectionString + "]: " + ex.Message + Environment.NewLine + ex.StackTrace);
            }

            return oSQLConn;
        }

        private OracleConnection getOracleDBConnection()
        {
            OracleConnection conn = null;
            string connectionString = "";

            try
            {
                conn = new OracleConnection();
                connectionString = "Data Source=" + txtDBName.Text + ";User Id=" + txtDBUser.Text + ";Password=" + txtDBPass.Text;

                /*
                connectionString = "Data Source=(DESCRIPTION="
                     + "(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=" + txtDBServer.Text + ")(PORT=" + txtDBPort.Text + ")))"
                     + "(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=" + txtDBName.Text + ")));"
                     + "User Id=" + txtDBUser.Text + ";Password=" + txtDBPass.Text + ";";
                */

                //Connection to datasource, using connection parameters given above
                conn.ConnectionString = connectionString;
            }
            // catch exception when error in connecting to database occurs
            catch (Exception ex)
            {
                MessageBox.Show("An exception occurred connecting to Oracle [" + connectionString + "]: " + ex.Message + Environment.NewLine + ex.StackTrace);
            }
            return conn;
        }

        private void btnLoadAuthSources_Click(object sender, EventArgs e)
        {
            DbConnection conn=null;
            DbDataReader reader=null;
            try
            {
                // open a DB connection
                conn = getDBConnection();
                conn.Open();

                // run the query
                DbCommand command = conn.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "select objectid, name from ptauthsources";
                reader = command.ExecuteReader();

                int objID = 0;
                String name = "";
                while (reader.Read())
                {
                    objID = (int)reader["objectid"];
                    name = (String)reader["name"];
                    cmbLeftAuthSource.Items.Add("" + objID + "|" + name);
                    cmbRightAuthSource.Items.Add("" + objID + "|" + name);
                    addStatus("Found Auth Source: " + reader["objectid"] + ": " + reader["name"]);
                }
                
            }
            catch (Exception ex)
            {
                addStatus("Exception getting auth source: " + ex.Message);
            }
            finally
            {
                // Close data reader object and database connection
                if (reader != null)
                    reader.Close();

                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }

        private void addStatus(String status)
        {
            txtStatus.Text += status + Environment.NewLine;
            Application.DoEvents();
        }

        private void btnLoadUsers_Click(object sender, EventArgs e)
        {
            // clear the list
            lstUsers.Clear();
            lstUsers.Columns.Add("left-id", 50);
            lstUsers.Columns.Add("left-login", 155);
            lstUsers.Columns.Add("left-mappingauth", 155);
            lstUsers.Columns.Add("right-id", 50);
            lstUsers.Columns.Add("right-login", 155);
            lstUsers.Columns.Add("right-mappingauth", 155);

            // read auth source values
            int leftAuthSourceID = 1;
            int rightAuthSourceID = 1;
            try {
                String leftAuth = cmbLeftAuthSource.SelectedItem.ToString();
                leftAuthSourceID = Int32.Parse(leftAuth.Split('|')[0]);
                addStatus("Left Auth Source ID: " + leftAuthSourceID);
            }
            catch (Exception ex) {
                addStatus("Exception loading left auth source: " + ex.Message);
            }
            try {
                String rightAuth = cmbRightAuthSource.SelectedItem.ToString();
                rightAuthSourceID = Int32.Parse(rightAuth.Split('|')[0]);
                addStatus("Right Auth Source ID: " + rightAuthSourceID);
            }
            catch (Exception ex) {
                addStatus("Exception loading right auth source: " + ex.Message);
            }

            // get Array Lists for both Auth Sources
            ArrayList leftUsers = getUsersForAuthSource(leftAuthSourceID);
            ArrayList rightUsers = getUsersForAuthSource(rightAuthSourceID);

            prgStatus.Value = 0;
            addStatus("Sorting user lists...");
            leftUsers.Sort(new userComparer());
            rightUsers.Sort(new userComparer());

            // populate the list with the left auth source users
            prgStatus.Maximum = leftUsers.Count;
            addStatus("Users found on left but not right:");
            Application.DoEvents();

            for (int x = 0; x < leftUsers.Count; x++)
            {
                if (x % 100 == 0)
                {
                    prgStatus.Value = x;
                    Application.DoEvents();
                }

                ListViewItem lvi = lstUsers.Items.Add("" + ((User)leftUsers[x]).getID());
                String mappingAuthName = ((User)leftUsers[x]).getMappingAuthName();
                lvi.SubItems.Add(((User)leftUsers[x]).getLoginName());
                lvi.SubItems.Add(mappingAuthName);
                User rightUser = getUser(rightUsers, mappingAuthName);
                if (rightUser != null)
                {
                    lvi.SubItems.Add(""+rightUser.getID());
                    lvi.SubItems.Add(rightUser.getLoginName());
                    lvi.SubItems.Add(rightUser.getMappingAuthName());
                }
                else
                {
                    lvi.SubItems.Add("");
                    lvi.SubItems.Add("");
                    lvi.SubItems.Add("");
                    addStatus("   " + ((User)leftUsers[x]).getLoginName() + " [" + ((User)leftUsers[x]).getMappingAuthName() + "]");
                    lvi.BackColor = Color.Pink;
                }
            }
            prgStatus.Value = 0;

            // populate the list with the right auth source users
            addStatus("Users found on right but not left:");
            ArrayList usersOnRightButNotLeft = getUsersMissingFromLeft(leftUsers, rightUsers);
            prgStatus.Maximum = usersOnRightButNotLeft.Count;
            Application.DoEvents();

            for (int x = 0; x < usersOnRightButNotLeft.Count; x++)
            {
                if (x % 100 == 0)
                {
                    prgStatus.Value = x;
                    Application.DoEvents();
                }

                User rightUser = (User)usersOnRightButNotLeft[x];
                ListViewItem lvi = lstUsers.Items.Add("");
                lvi.SubItems.Add("");
                lvi.SubItems.Add("");
                lvi.SubItems.Add(""+rightUser.getID());
                lvi.SubItems.Add(rightUser.getLoginName());
                lvi.SubItems.Add(rightUser.getMappingAuthName());
                lvi.BackColor = Color.Orchid;
                addStatus("   " + rightUser.getLoginName() + " [" + rightUser.getMappingAuthName() + "]");

            }
            prgStatus.Value = 0;

        }

        private ArrayList getUsersMissingFromLeft(ArrayList leftUsers, ArrayList rightUsers)
        {
            ArrayList retList = new ArrayList();
            String rightMappingAuthName, leftMappingAuthName;
            bool userFound;
            prgStatus.Maximum = rightUsers.Count;

            // iterate through all right users
            for (int x = 0; x < rightUsers.Count; x++)
            {
                if (x % 100 == 0)
                {
                    prgStatus.Value = x;
                    Application.DoEvents();
                }

                rightMappingAuthName = ((User)rightUsers[x]).getMappingAuthName();

                // iterate through all left users
                userFound = false;
                for (int y = 0; y < leftUsers.Count; y++)
                {
                    leftMappingAuthName = ((User)leftUsers[y]).getMappingAuthName();
                    if (leftMappingAuthName.Equals(rightMappingAuthName))
                    {
                        // user is on the left; no need to add it
                        userFound = true;
                        break;
                    }
                }

                if (!userFound)
                {
                    // user is missing from list
                    retList.Add((User)rightUsers[x]);
                }
            }
            return retList;
        }

        private ArrayList getGroupsMissingFromLeft(ArrayList leftGroups, ArrayList rightGroups)
        {
            ArrayList retList = new ArrayList();
            String rightMappingAuthName, leftMappingAuthName;
            bool groupFound;
            prgStatus.Maximum = rightGroups.Count;

            // iterate through all right groups
            for (int x = 0; x < rightGroups.Count; x++)
            {
                if (x % 100 == 0)
                {
                    prgStatus.Value = x;
                    Application.DoEvents();
                }

                rightMappingAuthName = ((Group)rightGroups[x]).getMappingAuthName();

                // iterate through all left groups
                groupFound = false;
                for (int y = 0; y < leftGroups.Count; y++)
                {
                    leftMappingAuthName = ((Group)leftGroups[y]).getMappingAuthName();
                    if (leftMappingAuthName.Equals(rightMappingAuthName))
                    {
                        // group is on the left; no need to add it
                        groupFound = true;
                        break;
                    }
                }

                if (!groupFound)
                {
                    // group is missing from list
                    retList.Add((Group)rightGroups[x]);
                }
            }
            return retList;
        }

        private User getUser(ArrayList userList, String mappingAuthName)
        {
            for (int x = 0; x < userList.Count; x++)
            {
                if (mappingAuthName.Equals(((User)userList[x]).getMappingAuthName()))
                    return (User)userList[x];
            }
            return null;
        }

        private Group getGroup(ArrayList groupList, String mappingAuthName)
        {
            for (int x = 0; x < groupList.Count; x++)
            {
                if (mappingAuthName.Equals(((Group)groupList[x]).getMappingAuthName()))
                    return (Group)groupList[x];
            }
            return null;
        }

        public ArrayList getUsersForAuthSource(int sourceAuthID)
        {
            txtStatus.Text = "";
            addStatus("Loading Users for auth source " + sourceAuthID);

            // the arrayList to populate
            ArrayList userArray = new ArrayList();

            // query for users
            DbConnection conn = null;
            DbDataReader reader = null;
            int count = getUserCountForAuthSource(sourceAuthID);
            prgStatus.Maximum = count;
            int curr = 0;
            try
            {
                // open a DB connection
                conn = getDBConnection();
                conn.Open();

                // run the query for the left
                DbCommand command = conn.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "select objectid, name, authsourceid, loginname, mappingauthname from ptusers where authsourceid = " + sourceAuthID;
                reader = command.ExecuteReader();

                int objID = 0;
                String name = "";
                int authSourceID = 0;
                String loginName = "";
                String mappingAuthName = "";
                while (reader.Read())
                {
                    curr++;
                    if (curr % 100 == 0)
                    {
                        prgStatus.Value = curr;
                        Application.DoEvents();
                    }
                    User tempUser = new User();
                    objID = (int)reader["objectid"];
                    name = (String)reader["name"];
                    authSourceID = (int)reader["authsourceid"];
                    loginName = (String)reader["loginname"];
                    mappingAuthName = (reader["mappingAuthName"] == DBNull.Value) ? "" : (String)reader["mappingAuthName"];
                    tempUser.setID(objID);
                    tempUser.setName(name);
                    tempUser.setAuthSourceID(authSourceID);
                    tempUser.setLoginName(loginName);
                    tempUser.setMappingAuthName(mappingAuthName);

                    //addStatus("user read: " + authSourceID + ": " + objID + ": " + name + ": " + loginName + ": " + mappingAuthName);
                    userArray.Add(tempUser);
                }

            }
            catch (Exception ex)
            {
                addStatus("Exception getting users for auth source: " + ex.Message);
            }
            finally
            {
                // Close data reader object and database connection
                if (reader != null)
                    reader.Close();

                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }

            return userArray;
        }

        public ArrayList getGroupsForAuthSource(int sourceAuthID)
        {
            txtStatus.Text = "";
            addStatus("Loading Groups for auth source " + sourceAuthID);

            // the arrayList to populate
            ArrayList groupArray = new ArrayList();

            // query for users
            DbConnection conn = null;
            DbDataReader reader = null;
            int count = getGroupCountForAuthSource(sourceAuthID);
            prgStatus.Maximum = count;
            int curr = 0;
            try
            {
                // open a DB connection
                conn = getDBConnection();
                conn.Open();

                // run the query for the left
                DbCommand command = conn.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "select objectid, name, authsourceid, mappingauthname from ptusergroups where authsourceid = " + sourceAuthID;
                reader = command.ExecuteReader();

                int objID = 0;
                String name = "";
                int authSourceID = 0;
                String mappingAuthName = "";
                while (reader.Read())
                {
                    curr++;
                    if (curr % 100 == 0)
                    {
                        prgStatus.Value = curr;
                        Application.DoEvents();
                    }
                    Group tempGroup = new Group();
                    objID = (int)reader["objectid"];
                    name = (String)reader["name"];
                    authSourceID = (int)reader["authsourceid"];
                    mappingAuthName = (reader["mappingAuthName"] == DBNull.Value) ? "" : (String)reader["mappingAuthName"];
                    tempGroup.setID(objID);
                    tempGroup.setName(name);
                    tempGroup.setAuthSourceID(authSourceID);
                    tempGroup.setMappingAuthName(mappingAuthName);

                    //addStatus("user read: " + authSourceID + ": " + objID + ": " + name + ": " + loginName + ": " + mappingAuthName);
                    groupArray.Add(tempGroup);
                }

            }
            catch (Exception ex)
            {
                addStatus("Exception getting groups for auth source: " + ex.Message);
            }
            finally
            {
                // Close data reader object and database connection
                if (reader != null)
                    reader.Close();

                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }

            return groupArray;
        }

        public int getUserCountForAuthSource(int sourceAuthID)
        {
            // query for user count
            DbConnection conn = null;
            DbDataReader reader = null;
            int count = 0;
            try
            {
                // open a DB connection
                conn = getDBConnection();
                conn.Open();

                // run the query for the left
                DbCommand command = conn.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "select count(1) cnt from ptusers where authsourceid = " + sourceAuthID;
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    count = (int)reader["cnt"];
                }

            }
            catch (Exception ex)
            {
                addStatus("Exception getting user count for auth source: " + ex.Message);
            }
            finally
            {
                // Close data reader object and database connection
                if (reader != null)
                    reader.Close();

                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }

            return count;
        }

        public int getGroupCountForAuthSource(int sourceAuthID)
        {
            // query for user count
            DbConnection conn = null;
            DbDataReader reader = null;
            int count = 0;
            try
            {
                // open a DB connection
                conn = getDBConnection();
                conn.Open();

                // run the query for the left
                DbCommand command = conn.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "select count(1) cnt from ptusergroups where authsourceid = " + sourceAuthID;
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    count = (int)reader["cnt"];
                }

            }
            catch (Exception ex)
            {
                addStatus("Exception getting group count for auth source: " + ex.Message);
            }
            finally
            {
                // Close data reader object and database connection
                if (reader != null)
                    reader.Close();

                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }

            return count;
        }

        private void lstUsers_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            txtStatus.Text = "";
            addStatus("left-id\tleft-login\tleft-mapping-auth\tright-id\tright-login\tright-mapping-auth");
            prgStatus.Maximum = lstUsers.Items.Count;
            String status = "";
            for (int x = 0; x < lstUsers.Items.Count; x++)
            {
                if (x % 100 == 0)
                {
                    prgStatus.Value = x;
                    Application.DoEvents();
                }
                ListViewItem lvi = lstUsers.Items[x];
                status += lvi.Text + "\t" + lvi.SubItems[1].Text + "\t" + lvi.SubItems[2].Text + "\t" + lvi.SubItems[3].Text + "\t" + lvi.SubItems[4].Text + "\t" + lvi.SubItems[5].Text + Environment.NewLine;
            }
            prgStatus.Value = 0;
            addStatus(status);
        }

        private void btnSwapSelected_Click(object sender, EventArgs e)
        {
            // check all selected users
            ListView.SelectedListViewItemCollection selectedItems = lstUsers.SelectedItems;
            int count = 0;
            int ignored = 0;

            prgStatus.Maximum = selectedItems.Count;
            for (int x = 0; x < selectedItems.Count; x++)
            {
                if (x % 100 == 0)
                {
                    prgStatus.Value = x;
                    Application.DoEvents();
                }

                //addStatus("|" + selectedItems[x].SubItems[1].Text + "|" + selectedItems[x].SubItems[4].Text + "|");
                if ((selectedItems[x].SubItems[1].Text.Equals("")) || (selectedItems[x].SubItems[4].Text.Equals("")))
                    ignored++;
                else
                    count++;
            }
            prgStatus.Value = 0;
            Application.DoEvents();

            // confirm desire to swap
            String warn = "";
            if (ignored > 0)
                warn = Environment.NewLine + " (" + ignored + " users ignored because they didn't have a matching name in the other auth source)";
            if (MessageBox.Show("Are you sure you want to swap these " + count + " users?" + warn, "Confirm", MessageBoxButtons.YesNo) == DialogResult.No)
                return;

            txtStatus.Text = "";

            DbConnection conn = null;
            DbDataReader reader = null;
            try
            {
                // open a DB connection
                conn = getDBConnection();
                conn.Open();

                // make sure there is no user with the reserved ID before starting...
                DbCommand command = conn.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "select count(1) cnt from ptusers where objectid = " + RESERVED_USERID;
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    count = (int)reader["cnt"];
                }
                reader.Close();

                if (count > 0)
                {
                    MessageBox.Show("There is already a user in the DB with an ID of " + RESERVED_USERID + "." + Environment.NewLine +
                        "You must use a unique userID for this hard-coded value to allow swapping of users." + Environment.NewLine +
                        "NO RECORDS WILL BE UPDATED.");
                    return;
                }

                // iterate through all selected users
                int leftid, rightid;
                string mappingname, leftlogin, rightlogin;

                prgStatus.Maximum = selectedItems.Count;
                for (int x = 0; x < selectedItems.Count; x++)
                {
                    // update the status bar
                    if (x % 100 == 0)
                    {
                        prgStatus.Value = x;
                        Application.DoEvents();
                    }

                    // get the values and only swap if both login names are present
                    mappingname = selectedItems[x].SubItems[2].Text;
                    leftlogin = selectedItems[x].SubItems[1].Text;
                    rightlogin = selectedItems[x].SubItems[4].Text;
                    if (!(leftlogin.Equals("")) && !(rightlogin.Equals("")))
                    {
                        leftid = Int32.Parse(selectedItems[x].Text);
                        rightid = Int32.Parse(selectedItems[x].SubItems[3].Text);
                        addStatus("swapping " + mappingname + ": " + leftlogin + " (" + leftid + ") -> " + rightlogin + " (" + rightid + ")");

                        command.CommandText = "update ptusers set objectid = " + RESERVED_USERID + " where objectid = " + leftid;
                        if (command.ExecuteNonQuery() == 0)
                        {
                            addStatus("FAILED UPDATING (step1): " + mappingname + " with SQL: " + command.CommandText);
                            addStatus("ABORTING");
                            break;
                        }

                        command.CommandText = "update ptusers set objectid = " + leftid + " where objectid = " + rightid;
                        if (command.ExecuteNonQuery() == 0)
                        {
                            addStatus("FAILED UPDATING (step2): " + mappingname + " with SQL: " + command.CommandText);
                            addStatus("ABORTING");
                            break;
                        }

                        command.CommandText = "update ptusers set objectid = " + rightid + " where objectid = " + RESERVED_USERID;
                        if (command.ExecuteNonQuery() == 0)
                        {
                            addStatus("FAILED UPDATING (step3): " + mappingname + " with SQL: " + command.CommandText);
                            addStatus("ABORTING");
                            break;
                        }

                    }
                }
                prgStatus.Value = 0;

            }
            catch (Exception ex)
            {
                addStatus("Exception swapping users: " + ex.Message);
            }
            finally
            {
                // Close data reader object and database connection
                if (reader != null)
                    reader.Close();

                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }        
        }

        private void btnLoadGroups_Click(object sender, EventArgs e)
        {

            // clear the list
            lstUsers.Clear();
            lstUsers.Columns.Add("left-id", 50);
            lstUsers.Columns.Add("left-name", 155);
            lstUsers.Columns.Add("left-mappingauth", 155);
            lstUsers.Columns.Add("right-id", 50);
            lstUsers.Columns.Add("right-name", 155);
            lstUsers.Columns.Add("right-mappingauth", 155);

            // read auth source values
            int leftAuthSourceID = 1;
            int rightAuthSourceID = 1;
            try
            {
                String leftAuth = cmbLeftAuthSource.SelectedItem.ToString();
                leftAuthSourceID = Int32.Parse(leftAuth.Split('|')[0]);
                addStatus("Left Auth Source ID: " + leftAuthSourceID);
            }
            catch (Exception ex)
            {
                addStatus("Exception loading left auth source: " + ex.Message);
            }
            try
            {
                String rightAuth = cmbRightAuthSource.SelectedItem.ToString();
                rightAuthSourceID = Int32.Parse(rightAuth.Split('|')[0]);
                addStatus("Right Auth Source ID: " + rightAuthSourceID);
            }
            catch (Exception ex)
            {
                addStatus("Exception loading right auth source: " + ex.Message);
            }

            // get Array Lists for both Auth Sources
            ArrayList leftGroups = getGroupsForAuthSource(leftAuthSourceID);
            ArrayList rightGroups = getGroupsForAuthSource(rightAuthSourceID);

            prgStatus.Value = 0;
            addStatus("Sorting group lists...");
            leftGroups.Sort(new groupComparer());
            rightGroups.Sort(new groupComparer());

            // populate the list with the left auth source groups
            prgStatus.Maximum = leftGroups.Count;
            addStatus("Groups found on left but not right:");
            Application.DoEvents();

            for (int x = 0; x < leftGroups.Count; x++)
            {
                if (x % 100 == 0)
                {
                    prgStatus.Value = x;
                    Application.DoEvents();
                }

                ListViewItem lvi = lstUsers.Items.Add("" + ((Group)leftGroups[x]).getID());
                String mappingAuthName = ((Group)leftGroups[x]).getMappingAuthName();
                lvi.SubItems.Add(mappingAuthName);
                lvi.SubItems.Add(((Group)leftGroups[x]).getName());
                Group rightGroup = getGroup(rightGroups, mappingAuthName);
                if (rightGroup != null)
                {
                    lvi.SubItems.Add("" + rightGroup.getID());
                    lvi.SubItems.Add(rightGroup.getName());
                    lvi.SubItems.Add(rightGroup.getMappingAuthName());
                }
                else
                {
                    lvi.SubItems.Add("");
                    lvi.SubItems.Add("");
                    lvi.SubItems.Add("");
                    addStatus("    [" + ((Group)leftGroups[x]).getMappingAuthName() + "]");
                    lvi.BackColor = Color.Pink;
                }
            }
            prgStatus.Value = 0;

            // populate the list with the right auth source groups
            addStatus("Groups found on right but not left:");
            ArrayList groupsOnRightButNotLeft = getGroupsMissingFromLeft(leftGroups, rightGroups);
            prgStatus.Maximum = groupsOnRightButNotLeft.Count;
            Application.DoEvents();

            for (int x = 0; x < groupsOnRightButNotLeft.Count; x++)
            {
                if (x % 100 == 0)
                {
                    prgStatus.Value = x;
                    Application.DoEvents();
                }

                Group rightGroup = (Group)groupsOnRightButNotLeft[x];
                ListViewItem lvi = lstUsers.Items.Add("");
                lvi.SubItems.Add("");
                lvi.SubItems.Add("");
                lvi.SubItems.Add("" + rightGroup.getID());
                lvi.SubItems.Add("" + rightGroup.getName());
                lvi.SubItems.Add(rightGroup.getMappingAuthName());
                lvi.BackColor = Color.Orchid;
                addStatus("    [" + rightGroup.getMappingAuthName() + "]");

            }
            prgStatus.Value = 0;
        }

        private void btnSwapSelectedGroups_Click(object sender, EventArgs e)
        {
            // check all selected groups
            ListView.SelectedListViewItemCollection selectedItems = lstUsers.SelectedItems;
            int count = 0;
            int ignored = 0;

            prgStatus.Maximum = selectedItems.Count;
            for (int x = 0; x < selectedItems.Count; x++)
            {
                if (x % 100 == 0)
                {
                    prgStatus.Value = x;
                    Application.DoEvents();
                }

                //addStatus("|" + selectedItems[x].SubItems[1].Text + "|" + selectedItems[x].SubItems[4].Text + "|");
                if ((selectedItems[x].SubItems[1].Text.Equals("")) || (selectedItems[x].SubItems[4].Text.Equals("")))
                    ignored++;
                else
                    count++;
            }
            prgStatus.Value = 0;
            Application.DoEvents();

            // confirm desire to swap
            String warn = "";
            if (ignored > 0)
                warn = Environment.NewLine + " (" + ignored + " groups ignored because they didn't have a matching name in the other auth source)";
            if (MessageBox.Show("Are you sure you want to swap these " + count + " groups?" + warn, "Confirm", MessageBoxButtons.YesNo) == DialogResult.No)
                return;

            txtStatus.Text = "";

            DbConnection conn = null;
            DbDataReader reader = null;
            try
            {
                // open a DB connection
                conn = getDBConnection();
                conn.Open();

                // make sure there is no group with the reserved ID before starting...
                DbCommand command = conn.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "select count(1) cnt from ptusergroups where objectid = " + RESERVED_GROUPID;
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    count = (int)reader["cnt"];
                }
                reader.Close();

                if (count > 0)
                {
                    MessageBox.Show("There is already a group in the DB with an ID of " + RESERVED_GROUPID + "." + Environment.NewLine +
                        "You must use a unique groupID for this hard-coded value to allow swapping of groups." + Environment.NewLine +
                        "NO RECORDS WILL BE UPDATED.");
                    return;
                }

                // iterate through all selected groups
                int leftid, rightid;
                string mappingname, leftlogin, rightlogin;

                prgStatus.Maximum = selectedItems.Count;
                for (int x = 0; x < selectedItems.Count; x++)
                {
                    // update the status bar
                    if (x % 100 == 0)
                    {
                        prgStatus.Value = x;
                        Application.DoEvents();
                    }

                    // get the values and only swap if both login names are present
                    mappingname = selectedItems[x].SubItems[2].Text;
                    leftlogin = selectedItems[x].SubItems[1].Text;
                    rightlogin = selectedItems[x].SubItems[4].Text;
                    if (!(leftlogin.Equals("")) && !(rightlogin.Equals("")))
                    {
                        leftid = Int32.Parse(selectedItems[x].Text);
                        rightid = Int32.Parse(selectedItems[x].SubItems[3].Text);
                        addStatus("swapping " + mappingname + ": " + leftlogin + " (" + leftid + ") -> " + rightlogin + " (" + rightid + ")");

                        command.CommandText = "update ptusergroups set objectid = " + RESERVED_GROUPID + " where objectid = " + leftid;
                        if (command.ExecuteNonQuery() == 0)
                        {
                            addStatus("FAILED UPDATING (step1): " + mappingname + " with SQL: " + command.CommandText);
                            addStatus("ABORTING");
                            break;
                        }

                        command.CommandText = "update ptusergroups set objectid = " + leftid + " where objectid = " + rightid;
                        if (command.ExecuteNonQuery() == 0)
                        {
                            addStatus("FAILED UPDATING (step2): " + mappingname + " with SQL: " + command.CommandText);
                            addStatus("ABORTING");
                            break;
                        }

                        command.CommandText = "update ptusergroups set objectid = " + rightid + " where objectid = " + RESERVED_GROUPID;
                        if (command.ExecuteNonQuery() == 0)
                        {
                            addStatus("FAILED UPDATING (step3): " + mappingname + " with SQL: " + command.CommandText);
                            addStatus("ABORTING");
                            break;
                        }

                    }
                }
                prgStatus.Value = 0;

            }
            catch (Exception ex)
            {
                addStatus("Exception swapping groups: " + ex.Message);
            }
            finally
            {
                // Close data reader object and database connection
                if (reader != null)
                    reader.Close();

                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }        
        }

    }
}
