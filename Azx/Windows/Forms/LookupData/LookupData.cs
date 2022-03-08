using System.Configuration;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace Azx.Windows.Forms
{
	public partial class LookupData : UserControl
    {

        #region Enum
        public enum ProviderType
        {
            Sql,
            Access
        }
        #endregion

        #region Properties
        
        private ProviderType _providerType;
        [System.ComponentModel.DefaultValue(LookupData.ProviderType.Sql)]
        [System.ComponentModel.Category("Lookup Properties")]
        public ProviderType ProviderName
        {
            get
            {
                return (_providerType);
            }
            set
            {
                _providerType = value;
            }
        }

        private string _tableName;
		/// <summary>
		/// نام جدول یا فایل مورد نظر 
		/// </summary>
		/// <remarks>
		/// Ali Azimzadeh - Date: 1385/11/01 - Version 1.0.1
		/// </remarks>
        [System.ComponentModel.Description("نام جدول یا فایل مورد نظر")]
        [System.ComponentModel.Category("Lookup Properties")]
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
        /// <summary>
        /// نام فیلد مورد نظر از جدول که می خواهید قابل جستجو باشد
        /// </summary>
        /// <remarks>
        /// Ali Azimzadeh - Date: 1385/11/01 - Version 1.0.0
        /// </remarks>
        private string _dataValueField;
        [System.ComponentModel.Description("نام فیلد مورد نظر از جدول که می خواهید قابل جستجو باشد")]
        [System.ComponentModel.Category("Lookup Properties")]
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

        /// <summary>
        /// نام فیلد مورد نظر از جدول که می خواهید نمایش داده شود
        /// </summary>

        /// <remarks>
        /// Ali Azimzadeh - Date: 1385/11/01 - Version 1.0.0
        /// </remarks>
        private string _dataTextField;
        [System.ComponentModel.Description("نام فیلد مورد نظر از جدول که می خواهید نمایش داده شود")]
        [System.ComponentModel.Category("Lookup Properties")]
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

        /// <summary>
        ///  نام فیلد مورد نظر از جدول که می خواهید به همراه فیلداول که قابل جستجو می باشد نمایش داده شود مانند نام بهمراه نام خانوادگی نمایش داده شود
        /// </summary>

        /// <remarks>
        /// Ali Azimzadeh - Date: 1386/01/28 - Version 1.0.0
        /// </remarks>
        private string _dataMatchField;
        [System.ComponentModel.Description("نام فیلد مورد نظر از جدول که می خواهید به همراه فیلد قابل جستجو نمایش داده شود")]
        [System.ComponentModel.Category("Lookup Properties")]
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

        /// <remarks>
        /// Ali Azimzadeh - Date: 1385/11/01 - Version 1.0.0
        /// </remarks>

        /// <summary>
        /// شرط مورد نظر برای فیلتر کردن اطلاعات بانک 
        /// </summary>
        private string _whereClause;
        [System.ComponentModel.Description("شرط مورد نظر برای فیلتر کردن اطلاعات بانک")]
        [System.ComponentModel.Category("Lookup Properties")]
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

        /// <summary>
        /// نام فیلد مورد نظر که بر اساس آن اطلاعات مرتب شود
        /// </summary>
		private string _orderBy;

		/// <remarks>
		/// Ali Azimzadeh - Date: 1385/11/01 - Version 1.0.0
		/// </remarks>
        
        [System.ComponentModel.Description("نام فیلد مورد نظر که بر اساس آن اطلاعات مرتب شود")]
        [System.ComponentModel.Category("Lookup Properties")]
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

        /// <summary>
        /// دستور اتصال به بانک اطلاعاتی
        /// </summary>
		private string _connectionString;
		/// <remarks>
		/// Ali Azimzadeh - Date: 1385/11/01 - Version 1.0.0
		/// </remarks>
		private string ConnectionString
		{
			get
			{
				_connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
				if (_connectionString == "")
					throw (new System.Exception("You did not specify [ConnectionString] in [App.Config]!"));

				return (_connectionString);
			}
		}

        /// <summary>
        /// عنوانی که می خواهید بالای فیلد اصلی / سمت راست لیست نمایان شود
        /// </summary>
        private string _rightHeader;
        /// <remarks>
        /// Ali Azimzadeh - Date: 1385/11/01 - Version 1.0.0
        /// </remarks>
        [System.ComponentModel.Description("عنوانی که می خواهید بالای فیلد اصلی / سمت راست لیست نمایان شود")]
        [System.ComponentModel.Category("Lookup Properties")]
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

        /// <summary>
        /// عنوانی که می خواهید در سمت چپ لیست نمایان شود
        /// </summary>
        private string _leftHeader;
        /// <remarks>
        /// Ali Azimzadeh - Date: 1385/11/01 - Version 1.0.0
        /// </remarks>
        [System.ComponentModel.Description("عنوانی که می خواهید در سمت چپ لیست نمایان شود")]
        [System.ComponentModel.Category("Lookup Properties")]
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

        private int _firstFieldLength;
        //public int FirstFieldLength
        //{
        //    get
        //    {
        //        return(_firstFieldLength);
        //    }
        //}

        private int _secondFieldLength;
        //public int SecondFieldLength
        //{
        //    get
        //    {
        //        return (_secondFieldLength);
        //    }
        //}

        /// <summary>
        /// اندازه طول فیلد اصلی  سمت راست در لیست باز شونده
        /// در صورت عدم تعیین مقدار مناسب لیست بصورت کامل قابل مشاهده نخواهد بود
        /// </summary>
        private int _rightWidth;
        /// <remarks>
        /// Ali Azimzadeh - Date: 1385/11/01 - Version 1.0.0
        /// </remarks>
        [System.ComponentModel.Description("تعیین اندازه طول فیلد اصلی  سمت راست در لیست باز شونده")]
        [System.ComponentModel.Category("Lookup Properties")]
        public int RightWidth
        {
            get
            {
                return (_rightWidth);
            }
            set
            {
                _rightWidth = value;
            }
        }

        /// <summary>
        /// اندازه طول فیلد چپ در لیست باز شونده
        /// در صورت عدم تعیین مقدار مناسب لیست بصورت کامل قابل مشاهده نخواهد بود
        /// </summary>
        private int _leftWidth;
        /// <remarks>
        /// Ali Azimzadeh - Date: 1385/11/01 - Version 1.0.0
        /// </remarks>
        [System.ComponentModel.Description("تعیین اندازه طول فیلد سمت چپ در لیست باز شونده")]
        [System.ComponentModel.Category("Lookup Properties")]
        public int LeftWidth
        {
            get
            {
                return (_leftWidth);
            }
            set
            {
                _leftWidth = value;
            }
        }

        /// <summary>
        /// مقدار فیلد اصلی را بر می گرداند
        /// </summary>
        private string _dataText;
        /// <remarks>
        /// Ali Azimzadeh - Date: 1385/11/01 - Version 1.0.0
        /// </remarks>
        [System.ComponentModel.Description("مقدار فیلد اصلی را بر می گرداند")]
        [System.ComponentModel.Browsable(false)]
        [System.ComponentModel.Category("Lookup Properties")]
        public string DataText
        {
            get
            {
                return (_dataText);
            }
            set
            {
                _dataText = value;
            }
        }

        /// <summary>
        /// مقدار فیلد دوم یا سمت چپ را بر می گرداند
        /// </summary>
        private string _dataValue;
        /// <remarks>
        /// Ali Azimzadeh - Date: 1385/11/01 - Version 1.0.0
        /// </remarks>
        [System.ComponentModel.Description("مقدار فیلد دوم یا سمت چپ را بر می گرداند")]
        [System.ComponentModel.Browsable(false)]
        [System.ComponentModel.Category("Lookup Properties")]
        public string DataValue
        {
            get
            {
                return (_dataValue);
            }
            set
            {
                _dataValue = value;
            }
        }

        private bool _refreshData;
        [System.ComponentModel.DefaultValue(false)]
        [System.ComponentModel.Category("Lookup Properties")]
        public bool RefreshData
        {
            get
            {
                return (_refreshData);
            }
            set
            {
                _refreshData = value;
            }
        }

        public System.Windows.Forms.TextBox DataTextBox
        {
            get
            {
                return (txtText);
            }
            set
            {
                txtText = value;
            }
        }

        public System.Windows.Forms.TextBox ValueTextBox
        {
            get
            {
                return(txtValue);
            }
        }

        internal bool _hasRows;
        [System.ComponentModel.Description("وجود رکورد در لیست را مشخص می کند")]
        [System.ComponentModel.Browsable(false)]
        [System.ComponentModel.Category("Lookup Properties")]
        public bool HasRows
        {
            get
            {
                return (_hasRows);
            }
        }

        #endregion

        #region Constructor
        /// <remarks>
		/// Ali Azimzadeh - Date: 1385/11/01 - Version 1.0.0
		/// </remarks>
		public LookupData()
		{
			InitializeComponent();
            this.RefreshData = false;
            this.ProviderName = ProviderType.Sql;
        }
        #endregion

        #region Events
        /// <remarks>
		/// Ali Azimzadeh - Date: 1385/11/01 - Version 1.0.0
		/// </remarks>
		protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
		{
			base.OnPaint(e);

			Width = 180;
			Height = 20;
		}

		/// <remarks>
		/// Ali Azimzadeh - Date: 1385/11/01 - Version 1.0.0
		/// </remarks>
		private void btnLookup_Click(object sender, System.EventArgs e)
		{
            ShowLookup();
        }


		/// <remarks>
		/// Ali Azimzadeh - Date: 1385/11/01 - Version 1.0.0
		/// </remarks>
		private void txtValue_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case System.Windows.Forms.Keys.Escape:
					{
						if ((!(e.Alt)) && (!(e.Shift)) && (!(e.Control)))
						{
							txtText.Text = "";
							txtValue.Text = "";
						}
						break;
					}
                case System.Windows.Forms.Keys.Enter:
                    {
                        ShowLookup();
                        if (txtText.Text != "" && txtValue.Text != "")
                            OnFillData(new System.EventArgs());
                        break;
                    }
            }
            OnLookupKeyDown(e);
        }

		/// <remarks>
		/// Ali Azimzadeh - Date: 1385/11/01 - Version 1.0.0
		/// </remarks>
		private void txtValue_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
            txtValue.Text = txtValue.Text.Trim();

            if (txtValue.Text != "")
            {
                string strSql = "SELECT " + DataValueField + ", " + DataTextField + " FROM " + TableName;

                if ((WhereClause != null) && (WhereClause != ""))
                    strSql += " WHERE " + WhereClause;

                if (strSql.IndexOf("WHERE") >= 0)
                    strSql += " AND " + DataValueField + " = '" + txtValue.Text + "'";
                else
                    strSql += " WHERE( " + DataValueField + " = '" + txtValue.Text + "')";

                switch (ProviderName)
                {
                    case ProviderType.Sql:
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

                                if (!(oDataReader.HasRows))
                                {
                                    txtText.Text = "";
                                    txtValue.Text = "";

                                    e.Cancel = true;
                                }
                                else
                                {
                                    oDataReader.Read();

                                    int intDataTextFieldIndex = oDataReader.GetOrdinal(DataTextField);

                                    string strDataTextField = oDataReader[intDataTextFieldIndex].ToString();

                                    txtText.Text = strDataTextField;
                                }
                                if (!(oDataReader.IsClosed))
                                    oDataReader.Close();
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
                    case ProviderType.Access:
                        {
                            //System.Data.OleDb.OleDbCommand oCommand = null;
                            //System.Data.OleDb.OleDbDataReader oDataReader = null;
                            //System.Data.OleDb.OleDbConnection oConnection = null;

                            //oConnection = new System.Data.OleDb.OleDbConnection();
                            //oConnection.ConnectionString = ConnectionString;

                            //oCommand = new System.Data.OleDb.OleDbCommand();

                            //oCommand.CommandTimeout = 60;
                            //oCommand.Connection = oConnection;

                            //oCommand.CommandText = strSql;
                            //oCommand.CommandType = System.Data.CommandType.Text;
                            //try
                            //{
                            //    if (oConnection.State != System.Data.ConnectionState.Open)
                            //        oConnection.Open();

                            //    oDataReader = oCommand.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

                            //    if (!(oDataReader.HasRows))
                            //    {
                            //        txtText.Text = "";
                            //        txtValue.Text = "";

                            //        e.Cancel = true;
                            //    }
                            //    else
                            //    {
                            //        oDataReader.Read();

                            //        int intDataTextFieldIndex = oDataReader.GetOrdinal(DataTextField);

                            //        string strDataTextField = oDataReader[intDataTextFieldIndex].ToString();

                            //        //txtText.Text = strDataTextField;
                            //    }
                            //    if (!(oDataReader.IsClosed))
                            //        oDataReader.Close();
                            //}
                            //catch (System.Exception ex)
                            //{
                            //    System.Windows.Forms.MessageBox.Show(ex.Message);
                            //}
                            //finally
                            //{
                            //    if (oDataReader != null)
                            //    {
                            //        if (!(oDataReader.IsClosed))
                            //            oDataReader.Close();

                            //        oDataReader.Dispose();
                            //        oDataReader = null;
                            //    }

                            //    if (oCommand != null)
                            //    {
                            //        oCommand.Dispose();
                            //        oCommand = null;
                            //    }

                            //    if (oConnection != null)
                            //    {
                            //        if (oConnection.State != System.Data.ConnectionState.Closed)
                            //            oConnection.Close();

                            //        oConnection.Dispose();
                            //        oConnection = null;
                            //    }
                            //}
                            break;
                        }
                }

                //try
                //{
                //    if (oConnection.State != System.Data.ConnectionState.Open)
                //        oConnection.Open();

                //    oDataReader = oCommand.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

                //    if (!(oDataReader.HasRows))
                //    {
                //        txtText.Text = "";
                //        txtValue.Text = "";

                //        e.Cancel = true;
                //    }
                //    else
                //    {
                //        oDataReader.Read();

                //        int intDataTextFieldIndex = oDataReader.GetOrdinal(DataTextField);

                //        string strDataTextField = oDataReader[intDataTextFieldIndex].ToString();

                //        txtText.Text = strDataTextField;
                //    }
                //    if (!(oDataReader.IsClosed))
                //        oDataReader.Close();
                //}
                //catch (System.Exception ex)
                //{
                //    System.Windows.Forms.MessageBox.Show(ex.Message);
                //}
                //finally
                //{
                //    if (oDataReader != null)
                //    {
                //        if (!(oDataReader.IsClosed))
                //            oDataReader.Close();

                //        oDataReader.Dispose();
                //        oDataReader = null;
                //    }

                //    if (oCommand != null)
                //    {
                //        oCommand.Dispose();
                //        oCommand = null;
                //    }

                //    if (oConnection != null)
                //    {
                //        if (oConnection.State != System.Data.ConnectionState.Closed)
                //            oConnection.Close();

                //        oConnection.Dispose();
                //        oConnection = null;
                //    }
                //}
            }
		}

		/// <remarks>
		/// Ali Azimzadeh - Date: 1385/11/01 - Version 1.0.0
		/// </remarks>
		private void txtValue_Enter(object sender, System.EventArgs e)
		{
			txtValue.SelectAll();
        }

        public delegate void FillDataEventHandler(object sender,System.EventArgs e);
        public event FillDataEventHandler FillData;
        protected virtual void OnFillData(System.EventArgs e)
        {
            if (FillData != null)
                FillData(this, e);
        }

        public delegate void HasRowEvenetHandler(object sender, System.EventArgs e);
        public event HasRowEvenetHandler HasRow;
        internal virtual void OnHasRow(System.EventArgs e)
        {
            if (HasRow != null)
                HasRow(this, e);
        }

        private void txtText_TextChanged(object sender, System.EventArgs e)
        {
            if(txtText.Text != null && txtText.Text != "")
                OnFillData(new System.EventArgs ());
//            if (txtText.Text != "" && txtValue.Text == "")
            if (txtText.Text != "" && txtText.Text != null)
                if (txtValue.Text == "" || txtValue.Text == null)
                {
                    switch (ProviderName)
                    {
                        case ProviderType.Sql:
                            {
                                string strSql = "";
                                System.Data.SqlClient.SqlCommand oCommand = null;
                                System.Data.SqlClient.SqlDataReader oDataReader = null;
                                System.Data.SqlClient.SqlConnection oConnection = null;
                                if (this.WhereClause == null || this.WhereClause == "")
                                    strSql = "SELECT " + DataValueField + ", " + DataTextField + " FROM " + TableName + " WHERE(" + DataTextField + "='" + DataTextBox.Text + "')";
                                else
                                    strSql = "SELECT " + DataValueField + ", " + DataTextField + " FROM " + TableName + " WHERE(" + DataTextField + "='" + DataTextBox.Text + "' and " + WhereClause + ")";

                                oConnection = new SqlConnection();
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

                                    if (!(oDataReader.HasRows))
                                    {
                                     //   _hasRows = false;
                                        txtText.Text = "";
                                        txtValue.Text = "";
                                    }
                                    else
                                    {
                                  //      _hasRows = true;
                                        oDataReader.Read();

                                        int intDataTextFieldIndex = oDataReader.GetOrdinal(DataValueField);

                                        txtValue.Text = oDataReader[intDataTextFieldIndex].ToString();

                                        //         txtText.Text = strDataTextField;
                                    }
                                    if (!(oDataReader.IsClosed))
                                        oDataReader.Close();
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
                        case ProviderType.Access:
                            {
                               // string strSql = "";
                               // System.Data.OleDb.OleDbCommand oCommand = null;
                               // System.Data.OleDb.OleDbDataReader oDataReader = null;
                               // System.Data.OleDb.OleDbConnection oConnection = null;
                               // if (this.WhereClause == null || this.WhereClause == "")
                               //     strSql = "SELECT " + DataValueField + ", " + DataTextField + " FROM " + TableName + " WHERE(" + DataTextField + "='" + DataTextBox.Text + "')";
                               // else
                               //     strSql = "SELECT " + DataValueField + ", " + DataTextField + " FROM " + TableName + " WHERE(" + DataTextField + "='" + DataTextBox.Text + "' and " + WhereClause + ")";


                               // oConnection = new System.Data.OleDb.OleDbConnection();
                               // oConnection.ConnectionString = ConnectionString;

                               // oCommand = new System.Data.OleDb.OleDbCommand();

                               // oCommand.CommandTimeout = 60;
                               // oCommand.Connection = oConnection;

                               // oCommand.CommandText = strSql;
                               // oCommand.CommandType = System.Data.CommandType.Text;
                               // try
                               // {
                               //     if (oConnection.State != System.Data.ConnectionState.Open)
                               //         oConnection.Open();

                               //     oDataReader = oCommand.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

                               //     if (!(oDataReader.HasRows))
                               //     {
                               //         txtText.Text = "";
                               //         txtValue.Text = "";
                               ////         _hasRows = false;
                               //     }
                               //     else
                               //     {
                               //  //       _hasRows = true;
                               //         oDataReader.Read();

                               //         int intDataTextFieldIndex = oDataReader.GetOrdinal(DataValueField);

                               //         txtValue.Text = oDataReader[intDataTextFieldIndex].ToString();

                               //         //txtText.Text = strDataTextField;
                               //     }
                               //     if (!(oDataReader.IsClosed))
                               //         oDataReader.Close();
                               // }
                               // catch (System.Data.OleDb.OleDbException olex)
                               // {
                               //     System.Windows.Forms.MessageBox.Show(olex.Message);
                               // }
                               // catch (System.Exception ex)
                               // {
                               //     System.Windows.Forms.MessageBox.Show(ex.Message);
                               // }
                               // finally
                               // {
                               //     if (oDataReader != null)
                               //     {
                               //         if (!(oDataReader.IsClosed))
                               //             oDataReader.Close();

                               //         oDataReader.Dispose();
                               //         oDataReader = null;
                               //     }

                               //     if (oCommand != null)
                               //     {
                               //         oCommand.Dispose();
                               //         oCommand = null;
                               //     }

                               //     if (oConnection != null)
                               //     {
                               //         if (oConnection.State != System.Data.ConnectionState.Closed)
                               //             oConnection.Close();

                               //         oConnection.Dispose();
                               //         oConnection = null;
                               //     }
                               // }
                                break;
                            }
                    }
                }
        }
        public delegate void LookupKeyDownEventHandler(object sender, System.Windows.Forms.KeyEventArgs e);
        public event LookupKeyDownEventHandler LookupKeyDown;
        protected virtual void OnLookupKeyDown(System.Windows.Forms.KeyEventArgs e)
        {
            if (LookupKeyDown != null)
                LookupKeyDown(this, e);
        }

#endregion

        #region Menthods
        /// <summary>
        /// لیست باز شونده را ساخته و نشان می دهد
        /// </summary>
        private void ShowLookup()
        {
            LookupForm frmLookup = new LookupForm();
            CalculateWidth();
            frmLookup.Width = RightWidth + LeftWidth + 20;
            if (this.Parent != null && 
                (this.Parent.GetType().BaseType == typeof(System.Windows.Forms.Form)
                || this.Parent.GetType().BaseType == typeof(Azx.Windows.Forms.Form))
                || this.Parent.GetType().BaseType == typeof(Azx.Windows.Forms.GradientForm))
                
            {
                if (this.ParentForm.IsMdiChild)
                {
                    frmLookup.Left = this.Left + this.ParentForm.ParentForm.Left + this.ParentForm.Left + ((this.Width - frmLookup.Width) / 2);
                    frmLookup.Top = this.ParentForm.Location.Y + this.ParentForm.ParentForm.Location.Y + this.Location.Y + 70;
                }
                else
                {
                    frmLookup.Left = this.Left + this.ParentForm.Left + ((this.Width - frmLookup.Width) / 2);
                    frmLookup.Top = this.ParentForm.Location.Y + this.Location.Y + 45;
                }
            }
            else
            {
                if (this.Parent.Parent != null &&
                    (this.Parent.Parent.GetType().BaseType == typeof(System.Windows.Forms.Form) ||
                    this.Parent.Parent.GetType().BaseType == typeof(Azx.Windows.Forms.Form)) ||
                    this.Parent.Parent.GetType().BaseType == typeof(Azx.Windows.Forms.GradientForm))


                {
                    if (this.ParentForm.IsMdiChild)
                    {
                        frmLookup.Left = this.Left + this.ParentForm.ParentForm.Left + this.ParentForm.Left + this.Parent.Left + ((this.Width - frmLookup.Width) / 2);
                        frmLookup.Top = this.ParentForm.ParentForm.Location.Y + this.ParentForm.Location.Y + this.Location.Y + this.Parent.Location.Y + 70;
                    }
                    else
                    {
                        frmLookup.Left = this.Left + this.ParentForm.Left + this.Parent.Left + ((this.Width - frmLookup.Width) / 2);
                        frmLookup.Top = this.ParentForm.Location.Y +  this.Location.Y + this.Parent.Location.Y + 45;
                    }
                }
                else
                {
                    if (this.Parent.Parent.Parent != null &&
                         (this.Parent.Parent.Parent.GetType().BaseType == typeof(System.Windows.Forms.Form) ||
                            this.Parent.Parent.Parent.GetType().BaseType == typeof(Azx.Windows.Forms.Form)) ||
                            this.Parent.Parent.Parent.GetType().BaseType == typeof(Azx.Windows.Forms.GradientForm))
                        {
                            if (this.ParentForm.IsMdiChild)
                            {
                                frmLookup.Left = this.Left + this.ParentForm.ParentForm.Left + this.ParentForm.Left + this.Parent.Parent.Left + this.Parent.Left + ((this.Width - frmLookup.Width) / 2);
                                frmLookup.Top = this.ParentForm.ParentForm.Location.Y + this.ParentForm.Location.Y + this.Location.Y + this.Parent.Location.Y + this.Parent.Parent.Location.Y + 70;
                            }
                            else
                            {
                                frmLookup.Left = this.Left + this.ParentForm.Left + this.Parent.Parent.Left + this.Parent.Left + ((this.Width - frmLookup.Width) / 2);
                                frmLookup.Top = this.ParentForm.Location.Y + this.Location.Y + this.Parent.Location.Y + this.Parent.Parent.Location.Y + 45;
                            }
                        }
                    else
                    {
                        if (this.Parent.Parent.Parent.Parent != null &&
                            (this.Parent.Parent.Parent.Parent.GetType().BaseType == typeof(System.Windows.Forms.Form) ||
                                this.Parent.Parent.Parent.Parent.GetType().BaseType == typeof(Azx.Windows.Forms.Form)) ||
                                this.Parent.Parent.Parent.Parent.GetType().BaseType == typeof(Azx.Windows.Forms.GradientForm))
                            {
                                if (this.ParentForm.IsMdiChild)
                                {
                                    frmLookup.Left = this.Left + this.ParentForm.Left + this.ParentForm.ParentForm.Left + this.Parent.Parent.Left + this.Parent.Parent.Parent.Left + this.Parent.Left + ((this.Width - frmLookup.Width) / 2);
                                    frmLookup.Top = this.ParentForm.ParentForm.Location.Y + this.ParentForm.Location.Y + this.Location.Y + this.Parent.Location.Y + this.Parent.Parent.Location.Y + this.Parent.Parent.Parent.Location.Y + 70;
                                }
                                else
                                {
                                    frmLookup.Left = this.Left + this.ParentForm.Left + this.Parent.Parent.Left + this.Parent.Parent.Parent.Left + this.Parent.Left + ((this.Width - frmLookup.Width) / 2);
                                    frmLookup.Top = this.ParentForm.Location.Y + this.Location.Y + this.Parent.Location.Y + this.Parent.Parent.Location.Y + this.Parent.Parent.Parent.Location.Y + 45;
                                }
                            }
                    }
                }
            }
            //            System.Windows.Forms.MessageBox.Show(this.Parent.Parent .Controls.Count.ToString());
            frmLookup.SenderUserControl = this;
            frmLookup.OrderBy = OrderBy;
            frmLookup.TableName = TableName;
            frmLookup.WhereClause = WhereClause;
            frmLookup.DataTextField = DataTextField;
            frmLookup.DataValueField = DataValueField;
            frmLookup.DataMatchField = DataMatchField;
            frmLookup.ConnectionString = ConnectionString;
            frmLookup.LeftHeader = LeftHeader;
            frmLookup.RightHeader = RightHeader;
        //    frmLookup.MdiParent = this.ParentForm.ParentForm;
            frmLookup.ShowDialog();
            string strDataValue = DataValue;
            string strDataText = DataText;
            this.txtValue.Text = strDataValue;
            this.txtText.Text = strDataText;
            //         System.Windows.Forms.MessageBox.Show(DataText + DataValue);
   //         System.Windows.Forms.MessageBox.Show(this.txtText.Text + this.txtValue.Text);
            if(RefreshData)
                this.LookUpReset();
            frmLookup.Dispose();
            frmLookup = null;

           // txtValue.Focus();
        }

        private void CalculateWidth()
        {
            //object FirstFieldLength1 = null;
            //object SecondFieldLength1 = null;

            switch (ProviderName)
            {
                case ProviderType.Sql:
                    {
                        string strSql_FirstField = "SELECT col_length('" + TableName + "', '" + DataTextField + "')";
                        string strSql_SecondField = "SELECT col_length('" + TableName + "', '" + DataValueField + "')";
                        System.Data.SqlClient.SqlCommand oCommand = null;
                        System.Data.SqlClient.SqlConnection oConnection = null;
                        try
                        {
                            oConnection = new System.Data.SqlClient.SqlConnection();
                            oConnection.ConnectionString = ConnectionString;

                            oCommand = new System.Data.SqlClient.SqlCommand();


                            oCommand.CommandTimeout = 60;
                            oCommand.Connection = oConnection;

                            oCommand.CommandText = strSql_FirstField;
                            oCommand.CommandType = System.Data.CommandType.Text;
                            if (oConnection.State != System.Data.ConnectionState.Open)
                                oConnection.Open();
                            _firstFieldLength = System.Convert.ToInt32(oCommand.ExecuteScalar().ToString());
                            oCommand.CommandText = strSql_SecondField;
                            _secondFieldLength = System.Convert.ToInt32(oCommand.ExecuteScalar().ToString());
                        }
                        catch (System.Exception ex)
                        {
                            System.Windows.Forms.MessageBox.Show(ex.Message);
                        }
                        finally
                        {
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
                case ProviderType.Access:
                    {
                        //string strSql_FirstField = "SELECT '" + TableName + "', '" + DataTextField + "'";
                        //string strSql_SecondField = "SELECT '" + TableName + "', '" + DataValueField + "'";
                        //System.Data.OleDb.OleDbCommand oCommand = null;
                        //System.Data.OleDb.OleDbConnection oConnection = null;

                        //oConnection = new System.Data.OleDb.OleDbConnection();
                        //oConnection.ConnectionString = ConnectionString;

                        //oCommand = new System.Data.OleDb.OleDbCommand();

                        //oCommand.CommandTimeout = 60;
                        //oCommand.Connection = oConnection;

                        //oCommand.CommandText = strSql_FirstField;
                        //oCommand.CommandType = System.Data.CommandType.Text;
                        //try
                        //{
                        //    if (oConnection.State != System.Data.ConnectionState.Open)
                        //        oConnection.Open();
                        //    _firstFieldLength = System.Convert.ToInt32(oCommand.ExecuteScalar().ToString());
                        //    oCommand.CommandText = strSql_SecondField;
                        //    _secondFieldLength = System.Convert.ToInt32(oCommand.ExecuteScalar().ToString());
                        //}
                        //catch (System.Exception ex)
                        //{
                        //    System.Windows.Forms.MessageBox.Show(ex.Message);
                        //}
                        //finally
                        //{
                        //    if (oCommand != null)
                        //    {
                        //        oCommand.Dispose();
                        //        oCommand = null;
                        //    }

                        //    if (oConnection != null)
                        //    {
                        //        if (oConnection.State != System.Data.ConnectionState.Closed)
                        //            oConnection.Close();

                        //        oConnection.Dispose();
                        //        oConnection = null;
                        //    }
                        //}
                        break;
                    }
            }


            //return (System.Convert.ToInt32(FirstFieldLength.ToString()) + System.Convert.ToInt32(SecondFieldLength1.ToString()));
        }

        public void LookUpReset()
        {
            this.txtText.Clear();
            this.txtValue.Clear();
            this.DataText = "";
            this.DataValue = "";
        }
        #endregion

    }
}
