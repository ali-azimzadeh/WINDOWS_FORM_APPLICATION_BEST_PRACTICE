using System.Data.SqlClient;
using System.Data.SqlTypes;
namespace Azx.Windows.Forms
{
	internal partial class LookupForm : Form
    {

        #region LookupForm Properties
        private LookupData _senderUserControl;

		/// <remarks>
		/// Ali Azimzadeh - Date: 1385/06/30 - Version 1.0.0
		/// </remarks>
		public LookupData SenderUserControl
		{
			get
			{
				return (_senderUserControl);
			}
			set
			{
				_senderUserControl = value;
			}
		}

		private string _tableName;

		/// <remarks>
		/// Ali Azimzadeh - Date: 1385/06/30 - Version 1.0.0
		/// </remarks>
		public string TableName
		{
			get
			{
				return (_tableName);
			}
			set
			{
				_tableName = value;
			}
		}

		private string _dataValueField;

		/// <remarks>
		/// Ali Azimzadeh - Date: 1385/06/30 - Version 1.0.0
		/// </remarks>
		public string DataValueField
		{
			get
			{
				return (_dataValueField);
			}
			set
			{
				_dataValueField = value;
			}
		}

		private string _dataTextField;

		/// <remarks>
		/// Ali Azimzadeh - Date: 1385/06/30 - Version 1.0.0
		/// </remarks>
		public string DataTextField
		{
			get
			{
				return (_dataTextField);
			}
			set
			{
				_dataTextField = value;
			}
		}


        /// <remarks>
        /// Ali Azimzadeh - Date: 1386/01/28 - Version 1.0.0
        /// </remarks>
        private string _dataMatchField;
        public string DataMatchField
        {
            get
            {
                return (_dataMatchField);
            }
            set
            {
                _dataMatchField = value;
            }
        }

		private string _whereClause;

		/// <remarks>
		/// Ali Azimzadeh - Date: 1385/06/30 - Version 1.0.0
		/// </remarks>
		public string WhereClause
		{
			get
			{
				return (_whereClause);
			}
			set
			{
				_whereClause = value;
			}
		}

		private string _orderBy;

		/// <remarks>
		/// Ali Azimzadeh - Date: 1385/06/30 - Version 1.0.0
		/// </remarks>
		public string OrderBy
		{
			get
			{
				return (_orderBy);
			}
			set
			{
				_orderBy = value;
			}
		}

		private string _connectionString;

		/// <remarks>
		/// Ali Azimzadeh - Date: 1385/06/30 - Version 1.0.0
		/// </remarks>
		public string ConnectionString
		{
			get
			{
				return (_connectionString);
			}
			set
			{
				_connectionString = value;
			}
		}
        private string _rightHeader;
        public string RightHeader
        {
            get
            {
                return (_rightHeader);
            }
            set
            {
                _rightHeader = value;
            }
        }

        private string _leftHeader;
        public string LeftHeader
        {
            get
            {
                return (_leftHeader);
            }
            set
            {
                _leftHeader = value;
            }
        }

        #endregion

        /// <remarks>
		/// Ali Azimzadeh - Date: 1385/06/30 - Version 1.0.0
		/// </remarks>
		public LookupForm()
		{
			InitializeComponent();
        }

        #region LookupForm Events


        /// <remarks>
		/// Ali Azimzadeh - Date: 1385/06/30 - Version 1.0.0
		/// </remarks>
		private void LookupForm_Load(object sender, System.EventArgs e)
		{
			// **************************************************
			// **************************************************
			// **************************************************
			lvwData.Tag = "";
			lvwData.Enabled = true;
			lvwData.TabStop = true;
			lvwData.Visible = true;
			lvwData.CausesValidation = true;

			lvwData.GridLines = true;
			lvwData.AllowDrop = false;
			lvwData.LabelEdit = false;
			lvwData.LabelWrap = false;
			lvwData.Scrollable = true;
			lvwData.CheckBoxes = false;
            lvwData.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			lvwData.MultiSelect = false;
			lvwData.AutoArrange = false;
			lvwData.FullRowSelect = true;
			lvwData.HideSelection = false;
			lvwData.HoverSelection = false;
			lvwData.AllowColumnReorder = true;
			lvwData.View = System.Windows.Forms.View.Details;

			lvwData.RightToLeftLayout = true;
			lvwData.RightToLeft = System.Windows.Forms.RightToLeft.Inherit;

			// lvwData.ListViewItemSorter
			lvwData.Sorting = System.Windows.Forms.SortOrder.None;
			lvwData.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;

			lvwData.ForeColor = System.Drawing.Color.Blue;
			lvwData.BackColor = System.Drawing.Color.Yellow;
			lvwData.Cursor = System.Windows.Forms.Cursors.Hand;
			lvwData.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			lvwData.Alignment = System.Windows.Forms.ListViewAlignment.Default;
			lvwData.Font = new System.Drawing.Font("Tahoma", 8, System.Drawing.FontStyle.Bold);
			// lvwData.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Bottom;

			// lvwData.SmallImageList
			// lvwData.LargeImageList
			// **************************************************
			// **************************************************
			// **************************************************

			// **************************************************
			// **************************************************
			// **************************************************

			lvwData.Columns.Clear();
            lvwData.Columns.Add(RightHeader, SenderUserControl.RightWidth, System.Windows.Forms.HorizontalAlignment.Left);
			lvwData.Columns.Add(LeftHeader, SenderUserControl.LeftWidth, System.Windows.Forms.HorizontalAlignment.Left);
			// **************************************************
			// **************************************************
			// **************************************************

			// **************************************************
			// **************************************************
			// **************************************************
            string strSql;
            if((DataMatchField == null) ||(DataMatchField == "")) 
                strSql = "SELECT " + DataValueField + ", " + DataTextField + " FROM " + TableName;
            else
                strSql = "SELECT " + DataValueField + ", " + DataTextField + ", " + DataMatchField + " FROM " + TableName;

			if ((WhereClause != null) && (WhereClause != ""))
				strSql += " WHERE " + WhereClause;

			if ((OrderBy != null) && (OrderBy != ""))
				strSql += " ORDER BY " + OrderBy;
			// **************************************************
			// **************************************************
			// **************************************************

			// **************************************************
			// **************************************************
			// **************************************************
			lvwData.Items.Clear();

            switch (SenderUserControl.ProviderName)
            {
                case Azx.Windows.Forms.LookupData.ProviderType.Sql:
                    {
                        System.Data.SqlClient.SqlCommand oCommand = null;
                        System.Data.SqlClient.SqlDataReader oDataReader = null;
                        System.Data.SqlClient.SqlConnection oConnection = null;

                        oConnection = new System.Data.SqlClient.SqlConnection();
                        oConnection.ConnectionString = ConnectionString;

                        oCommand = new System.Data.SqlClient.SqlCommand();

                        oCommand.CommandTimeout = 60;
                        oCommand.Connection = oConnection;

                        oCommand.CommandText = strSql;
                        oCommand.CommandType = System.Data.CommandType.Text;
                        try
                        {
                            if (oConnection.State != System.Data.ConnectionState.Open)
                                oConnection.Open();

                            oDataReader = oCommand.ExecuteReader(System.Data.CommandBehavior.CloseConnection);


                            if (oDataReader.HasRows)
                            {
                                SenderUserControl._hasRows = true;
                                //System.Windows.Forms.MessageBox.Show(SenderUserControl.HasRows.ToString());
                                int intDataTextFieldIndex = oDataReader.GetOrdinal(DataTextField);
                                int intDataValueFieldIndex = oDataReader.GetOrdinal(DataValueField);
                                int intDataMatchFieldIndex = -1;
                                if ((DataMatchField != null) && (DataMatchField != ""))
                                    intDataMatchFieldIndex = oDataReader.GetOrdinal(DataMatchField);
                                while (oDataReader.Read())
                                {
                                    //Microsoft.SqlServer.Management.Smo 
                                    //string str = oDataReader.GetDataTypeName(1).Length.ToString();
                                    string strDataTextField = oDataReader[intDataTextFieldIndex].ToString();
                                    string strDataValueField = oDataReader[intDataValueFieldIndex].ToString();
                                    string strDataMatchField = "";
                                    if (intDataMatchFieldIndex > -1)
                                        strDataMatchField = oDataReader[intDataMatchFieldIndex].ToString();

                                    System.Windows.Forms.ListViewItem itmCurrent =
                                         new System.Windows.Forms.ListViewItem(strDataValueField + "   " + strDataMatchField);
                                    itmCurrent.SubItems.Add(strDataTextField);

                                    lvwData.Items.Add(itmCurrent);
                                }
                                if (SenderUserControl.DataTextBox.Text != "")
                                {
                                    System.Windows.Forms.ListViewItem oListViewItem = lvwData.FindItemWithText(SenderUserControl.DataTextBox.Text);

                                    lvwData.Items[oListViewItem.Index].Selected = true;
                                    lvwData.Items[oListViewItem.Index].Focused = true;
                                    lvwData.TopItem = lvwData.Items[oListViewItem.Index];
                                    lvwData.Items[oListViewItem.Index].EnsureVisible();
                                }
                            }
                            if (!(oDataReader.IsClosed))
                                oDataReader.Close();
                            SenderUserControl.OnHasRow(new System.EventArgs());   
                        }
                        catch (System.Exception ex)
                        {
                            System.Windows.Forms.MessageBox.Show(ex.Message);
                        }
                        finally
                        {
                            if (oDataReader != null)
                            {
                                if (!(oDataReader.IsClosed))
                                    oDataReader.Close();

                                oDataReader.Dispose();
                                oDataReader = null;
                            }

                            if (oCommand != null)
                            {
                                oCommand.Dispose();
                                oCommand = null;
                            }

                            if (oConnection != null)
                            {
                                if (oConnection.State != System.Data.ConnectionState.Closed)
                                    oConnection.Close();

                                oConnection.Dispose();
                                oConnection = null;
                            }
                        }
                        break;
                    }
                case Azx.Windows.Forms.LookupData.ProviderType.Access:
                    {
//                        System.Data.OleDb.OleDbCommand oCommand = null;
//                        System.Data.OleDb.OleDbDataReader oDataReader = null;
//                        System.Data.OleDb.OleDbConnection oConnection = null;

//                        oConnection = new System.Data.OleDb.OleDbConnection();
//                        oConnection.ConnectionString = ConnectionString;

//                        oCommand = new System.Data.OleDb.OleDbCommand();

//                        oCommand.CommandTimeout = 60;
//                        oCommand.Connection = oConnection;

//                        oCommand.CommandText = strSql;
//                        oCommand.CommandType = System.Data.CommandType.Text;
//                        try
//                        {
//                            if (oConnection.State != System.Data.ConnectionState.Open)
//                                oConnection.Open();

//                            oDataReader = oCommand.ExecuteReader(System.Data.CommandBehavior.CloseConnection);


//                            if (oDataReader.HasRows)
//                            {
//                                int intDataTextFieldIndex = oDataReader.GetOrdinal(DataTextField);
//                                int intDataValueFieldIndex = oDataReader.GetOrdinal(DataValueField);
//                                int intDataMatchFieldIndex = -1;
//                                if ((DataMatchField != null) && (DataMatchField != ""))
//                                    intDataMatchFieldIndex = oDataReader.GetOrdinal(DataMatchField);
//                                while (oDataReader.Read())
//                                {
//                                    //Microsoft.SqlServer.Management.Smo 
//                                    //string str = oDataReader.GetDataTypeName(1).Length.ToString();
//                                    string strDataTextField = oDataReader[intDataTextFieldIndex].ToString();
//                                    string strDataValueField = oDataReader[intDataValueFieldIndex].ToString();
//                                    string strDataMatchField = "";
//                                    if (intDataMatchFieldIndex > -1)
//                                        strDataMatchField = oDataReader[intDataMatchFieldIndex].ToString();

//                                    System.Windows.Forms.ListViewItem itmCurrent =
//                                         new System.Windows.Forms.ListViewItem(strDataValueField + "   " + strDataMatchField);

//                                    itmCurrent.SubItems.Add(strDataTextField);

//                                    lvwData.Items.Add(itmCurrent);
//                                }
//                                if (SenderUserControl.DataTextBox.Text != "")
//                                {
//                                    System.Windows.Forms.ListViewItem oListViewItem = lvwData.FindItemWithText(SenderUserControl.DataTextBox.Text);

//                                    lvwData.TopItem = lvwData.Items[oListViewItem.Index];
//                                    lvwData.Items[oListViewItem.Index].Focused = true;
//                                    lvwData.Items[oListViewItem.Index].Selected = true;
//                                    lvwData.Items[oListViewItem.Index].EnsureVisible();
//                                    //System.Windows.Forms.MessageBox.Show(lvwData.Items[oListViewItem.Index].Bounds.Y.ToString());
//                                    //                                    System.Windows.Forms.ListViewItem oListViewItem = lvwData.SelectedItems[0];
//                                   // System.Drawing.Rectangle rec = new System.Drawing.Rectangle(3, 17, 200, 14);
//                                   //lvwData.Items[oListViewItem.Index].Position.Y  = rec.Y ;
//                                 //   SenderUserControl.txtValue.Text = oListViewItem.SubItems[1].Text;
//                                  // lvwData.SetBounds(0, 17, 200, 14);
//                                }
//                            }
//                            if (!(oDataReader.IsClosed))
//                                oDataReader.Close();
////                            SenderUserControl.OnHasRow(new System.EventArgs());   
//                        }
//                        catch (System.Exception ex)
//                        {
//                            System.Windows.Forms.MessageBox.Show(ex.Message);
//                        }
//                        finally
//                        {
//                            if (oDataReader != null)
//                            {
//                                if (!(oDataReader.IsClosed))
//                                    oDataReader.Close();

//                                oDataReader.Dispose();
//                                oDataReader = null;
//                            }

//                            if (oCommand != null)
//                            {
//                                oCommand.Dispose();
//                                oCommand = null;
//                            }

//                            if (oConnection != null)
//                            {
//                                if (oConnection.State != System.Data.ConnectionState.Closed)
//                                    oConnection.Close();

//                                oConnection.Dispose();
//                                oConnection = null;
//                            }
//                        }
                        break;
                    }
            }

			// **************************************************
			// **************************************************
			// **************************************************
		}

		/// <remarks>
		/// Ali Azimzadeh - Date: 1385/06/30 - Version 1.0.0
		/// </remarks>
		private void lvwData_DoubleClick(object sender, System.EventArgs e)
		{
            this.Close();
//			FillParentTextBoxes();
		}

		/// <remarks>
		/// Ali Azimzadeh - Date: 1385/06/30 - Version 1.0.0
		/// </remarks>
		private void LookupForm_Activated(object sender, System.EventArgs e)
		{
            if (lvwData.Items.Count >= 1)
            {
                if (SenderUserControl.DataTextBox.Text == "")
                {
                    lvwData.Items[0].Selected = true;
                    lvwData.Items[0].Focused = true;
                    //         System.Windows.Forms.ListViewItem oListViewItem = lvwData.FindItemWithText(SenderUserControl.DataTextBox);
           ////         System.Windows.Forms.MessageBox.Show(oListViewItem.Index.ToString());
           //         lvwData.Items[oListViewItem.Index].Selected = true;
           //         lvwData.Items[oListViewItem.Index].Focused = true;
                }
                //else
            }

            //lvwData.Focus();
		}

        /// <remarks>
		/// Ali Azimzadeh - Date: 1385/06/30 - Version 1.0.0
		/// </remarks>
		private void lvwData_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case System.Windows.Forms.Keys.Enter:
					{
                        if ((!(e.Alt)) && (!(e.Shift)) && (!(e.Control)))
                           // FillParentTextBoxes();
                            this.Close();
						break;
					}
                case System.Windows.Forms.Keys.Escape:
                    {
                        this.Close();
                        break;
                    }
			}
		}

        private void LookupForm_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            FillParentTextBoxes();
            //         System.Windows.Forms.MessageBox.Show(SenderUserControl.txtText.Text.ToString());

        }
        #endregion

        #region LookupForm Method
        /// <remarks>
		/// Ali Azimzadeh - Date: 1385/06/30 - Version 1.0.0
		/// </remarks>
		private void FillParentTextBoxes()
		{
            if (lvwData.SelectedItems.Count >= 1)
            {
                System.Windows.Forms.ListViewItem oListViewItem = lvwData.SelectedItems[0];

                //SenderUserControl.txtText.Text = oListViewItem.SubItems[1].Text;
                //	SenderUserControl.txtValue.Text = oListViewItem.SubItems[0].Text;
                SenderUserControl.DataText = oListViewItem.SubItems[1].Text;
                SenderUserControl.DataValue = oListViewItem.SubItems[0].Text;
                //	Close();
                //                System.Windows.Forms.MessageBox.Show(SenderUserControl.DataValue + SenderUserControl.DataText);
            }
            else
            {
                SenderUserControl.DataText = " ";
                SenderUserControl.DataValue = " ";
            }
            SenderUserControl.OnHasRow(new System.EventArgs());

        }
        #endregion

        private void LookupForm_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Escape)
                this.Close();
        }

        private void lvwData_Click(object sender, System.EventArgs e)
        {
//            System.Windows.Forms.MessageBox.Show(lvwData.SelectedItems[0].Position.Y.ToString());
        }
    }
}