
namespace Watch_Face_Editor
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage_Edit = new System.Windows.Forms.TabPage();
            this.button_SaveJson = new System.Windows.Forms.Button();
            this.button_OpenDir = new System.Windows.Forms.Button();
            this.tabControl_Edit_SetShow = new System.Windows.Forms.TabControl();
            this.tabPage_Edit_Elements = new System.Windows.Forms.TabPage();
            this.groupBox_AddElemets = new System.Windows.Forms.GroupBox();
            this.pictureBox_IconBackground = new System.Windows.Forms.PictureBox();
            this.comboBox_AddBackground = new System.Windows.Forms.ComboBox();
            this.pictureBox_IconDate = new System.Windows.Forms.PictureBox();
            this.pictureBox_IconAir = new System.Windows.Forms.PictureBox();
            this.pictureBox_IconSystem = new System.Windows.Forms.PictureBox();
            this.pictureBox_IconTime = new System.Windows.Forms.PictureBox();
            this.pictureBox_IconActivity = new System.Windows.Forms.PictureBox();
            this.comboBox_AddSystem = new System.Windows.Forms.ComboBox();
            this.comboBox_AddAir = new System.Windows.Forms.ComboBox();
            this.comboBox_AddActivity = new System.Windows.Forms.ComboBox();
            this.comboBox_AddDate = new System.Windows.Forms.ComboBox();
            this.comboBox_AddTime = new System.Windows.Forms.ComboBox();
            this.panel_WatchfaceElements = new System.Windows.Forms.Panel();
            this.tableLayoutPanel_ElemetsWatchFace = new System.Windows.Forms.TableLayoutPanel();
            this.panel_UC_DigitalTime = new System.Windows.Forms.Panel();
            this.uCtrl_DigitalTime_Elm = new ControlLibrary.UCtrl_DigitalTime_Elm();
            this.panel_UC_AnalogTime = new System.Windows.Forms.Panel();
            this.uCtrl_AnalogTime_Elm = new ControlLibrary.UCtrl_AnalogTime_Elm();
            this.panel_UC_DateDay = new System.Windows.Forms.Panel();
            this.uCtrl_DateDay_Elm = new ControlLibrary.UCtrl_DateDay_Elm();
            this.panel_UC_DateMonth = new System.Windows.Forms.Panel();
            this.uCtrl_DateMonth_Elm = new ControlLibrary.UCtrl_DateMonth_Elm();
            this.panel_UC_DateYear = new System.Windows.Forms.Panel();
            this.uCtrl_DateYear_Elm = new ControlLibrary.UCtrl_DateYear_Elm();
            this.panel_UC_Background = new System.Windows.Forms.Panel();
            this.uCtrl_Background_Elm = new ControlLibrary.UCtrl_Background_Elm();
            this.panel_UC_DateWeek = new System.Windows.Forms.Panel();
            this.uCtrl_DateWeek_Elm = new ControlLibrary.UCtrl_DateWeek_Elm();
            this.panel_UC_Steps = new System.Windows.Forms.Panel();
            this.uCtrl_Steps_Elm = new ControlLibrary.UCtrl_Steps_Elm();
            this.panel_UC_Statuses = new System.Windows.Forms.Panel();
            this.uCtrl_Statuses_Elm = new ControlLibrary.UCtrl_Statuses_Elm();
            this.panel_UC_Shortcuts = new System.Windows.Forms.Panel();
            this.uCtrl_Shortcuts_Elm = new ControlLibrary.UCtrl_Shortcuts_Elm();
            this.panel_UC_Battery = new System.Windows.Forms.Panel();
            this.uCtrl_Battery_Elm = new ControlLibrary.UCtrl_Battery_Elm();
            this.panel_UC_Heart = new System.Windows.Forms.Panel();
            this.uCtrl_Heart_Elm = new ControlLibrary.UCtrl_Heart_Elm();
            this.panel_UC_Calories = new System.Windows.Forms.Panel();
            this.uCtrl_Calories_Elm = new ControlLibrary.UCtrl_Calories_Elm();
            this.panel_UC_PAI = new System.Windows.Forms.Panel();
            this.uCtrl_PAI_Elm = new ControlLibrary.UCtrl_PAI_Elm();
            this.panel_UC_Distance = new System.Windows.Forms.Panel();
            this.uCtrl_Distance_Elm = new ControlLibrary.UCtrl_Distance_Elm();
            this.panel_UC_Weather = new System.Windows.Forms.Panel();
            this.uCtrl_Weather_Elm = new ControlLibrary.UCtrl_Weather_Elm();
            this.panel_ElementsOpt = new System.Windows.Forms.Panel();
            this.uCtrl_Text_Weather_Opt = new ControlLibrary.UCtrl_Text_Weather_Opt();
            this.uCtrl_Shortcut_Opt = new ControlLibrary.UCtrl_Shortcut_Opt();
            this.uCtrl_Segments_Opt = new ControlLibrary.UCtrl_Segments_Opt();
            this.uCtrl_Icon_Opt = new ControlLibrary.UCtrl_Icon_Opt();
            this.uCtrl_Linear_Scale_Opt = new ControlLibrary.UCtrl_Linear_Scale_Opt();
            this.uCtrl_Circle_Scale_Opt = new ControlLibrary.UCtrl_Circle_Scale_Opt();
            this.uCtrl_Images_Opt = new ControlLibrary.UCtrl_Images_Opt();
            this.uCtrl_Pointer_Opt = new ControlLibrary.UCtrl_Pointer_Opt();
            this.uCtrl_AmPm_Opt = new ControlLibrary.UCtrl_AmPm_Opt();
            this.uCtrl_Text_Opt = new ControlLibrary.UCtrl_Text_Opt();
            this.userCtrl_Background_Options = new ControlLibrary.UCtrl_Background_Opt();
            this.panel_MainScreen_AOD = new System.Windows.Forms.Panel();
            this.button_CopyAOD = new System.Windows.Forms.Button();
            this.button_RandomPreview = new System.Windows.Forms.Button();
            this.radioButton_ScreenIdle = new System.Windows.Forms.RadioButton();
            this.radioButton_ScreenNormal = new System.Windows.Forms.RadioButton();
            this.tabPage_Show_Set = new System.Windows.Forms.TabPage();
            this.panel_set = new System.Windows.Forms.Panel();
            this.userCtrl_Set12 = new ControlLibrary.UCtrl_Set();
            this.userCtrl_Set11 = new ControlLibrary.UCtrl_Set();
            this.userCtrl_Set10 = new ControlLibrary.UCtrl_Set();
            this.userCtrl_Set9 = new ControlLibrary.UCtrl_Set();
            this.userCtrl_Set8 = new ControlLibrary.UCtrl_Set();
            this.userCtrl_Set7 = new ControlLibrary.UCtrl_Set();
            this.userCtrl_Set6 = new ControlLibrary.UCtrl_Set();
            this.userCtrl_Set5 = new ControlLibrary.UCtrl_Set();
            this.userCtrl_Set4 = new ControlLibrary.UCtrl_Set();
            this.userCtrl_Set3 = new ControlLibrary.UCtrl_Set();
            this.userCtrl_Set2 = new ControlLibrary.UCtrl_Set();
            this.userCtrl_Set1 = new ControlLibrary.UCtrl_Set();
            this.panel_PreviewStates = new System.Windows.Forms.Panel();
            this.button_JsonPreview_Random = new System.Windows.Forms.Button();
            this.button_JsonPreview_Read = new System.Windows.Forms.Button();
            this.button_JsonPreview_Write = new System.Windows.Forms.Button();
            this.button_Add_Images = new System.Windows.Forms.Button();
            this.button_New_Project = new System.Windows.Forms.Button();
            this.dataGridView_ImagesList = new System.Windows.Forms.DataGridView();
            this.Number = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NameFile = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnImage = new System.Windows.Forms.DataGridViewImageColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.button_JSON = new System.Windows.Forms.Button();
            this.tabPageConverting = new System.Windows.Forms.TabPage();
            this.label_ConvertingHelp03 = new System.Windows.Forms.Label();
            this.label_ConvertingHelp02 = new System.Windows.Forms.Label();
            this.label_ConvertingHelp01 = new System.Windows.Forms.Label();
            this.label_ConvertingHelp = new System.Windows.Forms.Label();
            this.button_Converting = new System.Windows.Forms.Button();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.numericUpDown_ConvertingOutput_Custom = new System.Windows.Forms.NumericUpDown();
            this.radioButton_ConvertingOutput_Custom = new System.Windows.Forms.RadioButton();
            this.radioButton_ConvertingOutput_TRexPro = new System.Windows.Forms.RadioButton();
            this.radioButton_ConvertingOutput_GTR3Pro = new System.Windows.Forms.RadioButton();
            this.radioButton_ConvertingOutput_GTR3 = new System.Windows.Forms.RadioButton();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.numericUpDown_ConvertingInput_Custom = new System.Windows.Forms.NumericUpDown();
            this.radioButton_ConvertingInput_Custom = new System.Windows.Forms.RadioButton();
            this.radioButton_ConvertingInput_TRexPro = new System.Windows.Forms.RadioButton();
            this.radioButton_ConvertingInput_GTR3Pro = new System.Windows.Forms.RadioButton();
            this.radioButton_ConvertingInput_GTR3 = new System.Windows.Forms.RadioButton();
            this.tabPage_Settings = new System.Windows.Forms.TabPage();
            this.button_Reset = new System.Windows.Forms.Button();
            this.numericUpDown_Gif_Speed = new System.Windows.Forms.NumericUpDown();
            this.checkBox_AllWidgetsInGif = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button_WatchSkin_PathGet = new System.Windows.Forms.Button();
            this.textBox_WatchSkin_Path = new System.Windows.Forms.TextBox();
            this.checkBox_WatchSkin_Use = new System.Windows.Forms.CheckBox();
            this.label355 = new System.Windows.Forms.Label();
            this.checkBox_ShowIn12hourFormat = new System.Windows.Forms.CheckBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.checkBox_Shortcuts_Image = new System.Windows.Forms.CheckBox();
            this.checkBox_Shortcuts_Border = new System.Windows.Forms.CheckBox();
            this.checkBox_Shortcuts_Area = new System.Windows.Forms.CheckBox();
            this.checkBox_JsonWarnings = new System.Windows.Forms.CheckBox();
            this.comboBox_Language = new System.Windows.Forms.ComboBox();
            this.label356 = new System.Windows.Forms.Label();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.radioButton_Settings_Pack_DoNotning = new System.Windows.Forms.RadioButton();
            this.radioButton_Settings_Pack_GoToFile = new System.Windows.Forms.RadioButton();
            this.radioButton_Settings_Pack_Dialog = new System.Windows.Forms.RadioButton();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.radioButton_Settings_Open_DoNotning = new System.Windows.Forms.RadioButton();
            this.radioButton_Settings_Open_Download = new System.Windows.Forms.RadioButton();
            this.radioButton_Settings_Open_Dialog = new System.Windows.Forms.RadioButton();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.radioButton_Settings_AfterUnpack_DoNothing = new System.Windows.Forms.RadioButton();
            this.radioButton_Settings_AfterUnpack_Download = new System.Windows.Forms.RadioButton();
            this.radioButton_Settings_AfterUnpack_Dialog = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButton_Settings_Unpack_Replace = new System.Windows.Forms.RadioButton();
            this.radioButton_Settings_Unpack_Save = new System.Windows.Forms.RadioButton();
            this.radioButton_Settings_Unpack_Dialog = new System.Windows.Forms.RadioButton();
            this.tabPage_About = new System.Windows.Forms.TabPage();
            this.label_TranslateHelp = new System.Windows.Forms.Label();
            this.label415 = new System.Windows.Forms.Label();
            this.label414 = new System.Windows.Forms.Label();
            this.label412 = new System.Windows.Forms.Label();
            this.label413 = new System.Windows.Forms.Label();
            this.linkLabel_py_amazfit_tools = new System.Windows.Forms.LinkLabel();
            this.label409 = new System.Windows.Forms.Label();
            this.label408 = new System.Windows.Forms.Label();
            this.label407 = new System.Windows.Forms.Label();
            this.label_version_help = new System.Windows.Forms.Label();
            this.label406 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.radioButton_GTR3_Pro = new System.Windows.Forms.RadioButton();
            this.checkBox_WidgetsArea = new System.Windows.Forms.CheckBox();
            this.checkBox_center_marker = new System.Windows.Forms.CheckBox();
            this.button_CreatePreview = new System.Windows.Forms.Button();
            this.button_RefreshPreview = new System.Windows.Forms.Button();
            this.checkBox_CircleScaleImage = new System.Windows.Forms.CheckBox();
            this.checkBox_Show_Shortcuts = new System.Windows.Forms.CheckBox();
            this.checkBox_crop = new System.Windows.Forms.CheckBox();
            this.radioButton_GTS3 = new System.Windows.Forms.RadioButton();
            this.checkBox_border = new System.Windows.Forms.CheckBox();
            this.label_preview_Y = new System.Windows.Forms.Label();
            this.label_preview_X = new System.Windows.Forms.Label();
            this.radioButton_GTR3 = new System.Windows.Forms.RadioButton();
            this.button_SaveGIF = new System.Windows.Forms.Button();
            this.button_SavePNG = new System.Windows.Forms.Button();
            this.checkBox_WebB = new System.Windows.Forms.CheckBox();
            this.checkBox_WebW = new System.Windows.Forms.CheckBox();
            this.button_PreviewBig = new System.Windows.Forms.Button();
            this.label_version = new System.Windows.Forms.Label();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.button_pack_zip = new System.Windows.Forms.Button();
            this.button_unpack_zip = new System.Windows.Forms.Button();
            this.pictureBox_Preview = new System.Windows.Forms.PictureBox();
            this.tabControl1.SuspendLayout();
            this.tabPage_Edit.SuspendLayout();
            this.tabControl_Edit_SetShow.SuspendLayout();
            this.tabPage_Edit_Elements.SuspendLayout();
            this.groupBox_AddElemets.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_IconBackground)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_IconDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_IconAir)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_IconSystem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_IconTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_IconActivity)).BeginInit();
            this.panel_WatchfaceElements.SuspendLayout();
            this.tableLayoutPanel_ElemetsWatchFace.SuspendLayout();
            this.panel_UC_DigitalTime.SuspendLayout();
            this.panel_UC_AnalogTime.SuspendLayout();
            this.panel_UC_DateDay.SuspendLayout();
            this.panel_UC_DateMonth.SuspendLayout();
            this.panel_UC_DateYear.SuspendLayout();
            this.panel_UC_Background.SuspendLayout();
            this.panel_UC_DateWeek.SuspendLayout();
            this.panel_UC_Steps.SuspendLayout();
            this.panel_UC_Statuses.SuspendLayout();
            this.panel_UC_Shortcuts.SuspendLayout();
            this.panel_UC_Battery.SuspendLayout();
            this.panel_UC_Heart.SuspendLayout();
            this.panel_UC_Calories.SuspendLayout();
            this.panel_UC_PAI.SuspendLayout();
            this.panel_UC_Distance.SuspendLayout();
            this.panel_UC_Weather.SuspendLayout();
            this.panel_ElementsOpt.SuspendLayout();
            this.panel_MainScreen_AOD.SuspendLayout();
            this.tabPage_Show_Set.SuspendLayout();
            this.panel_set.SuspendLayout();
            this.panel_PreviewStates.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_ImagesList)).BeginInit();
            this.tabPageConverting.SuspendLayout();
            this.groupBox10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_ConvertingOutput_Custom)).BeginInit();
            this.groupBox9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_ConvertingInput_Custom)).BeginInit();
            this.tabPage_Settings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Gif_Speed)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage_About.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Preview)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage_Edit);
            this.tabControl1.Controls.Add(this.tabPageConverting);
            this.tabControl1.Controls.Add(this.tabPage_Settings);
            this.tabControl1.Controls.Add(this.tabPage_About);
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tabControl1_Selecting);
            // 
            // tabPage_Edit
            // 
            this.tabPage_Edit.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage_Edit.Controls.Add(this.button_SaveJson);
            this.tabPage_Edit.Controls.Add(this.button_OpenDir);
            this.tabPage_Edit.Controls.Add(this.tabControl_Edit_SetShow);
            this.tabPage_Edit.Controls.Add(this.button_Add_Images);
            this.tabPage_Edit.Controls.Add(this.button_New_Project);
            this.tabPage_Edit.Controls.Add(this.dataGridView_ImagesList);
            this.tabPage_Edit.Controls.Add(this.button_JSON);
            resources.ApplyResources(this.tabPage_Edit, "tabPage_Edit");
            this.tabPage_Edit.Name = "tabPage_Edit";
            // 
            // button_SaveJson
            // 
            resources.ApplyResources(this.button_SaveJson, "button_SaveJson");
            this.button_SaveJson.Name = "button_SaveJson";
            this.button_SaveJson.UseVisualStyleBackColor = true;
            this.button_SaveJson.Click += new System.EventHandler(this.button_SaveJson_Click);
            // 
            // button_OpenDir
            // 
            resources.ApplyResources(this.button_OpenDir, "button_OpenDir");
            this.button_OpenDir.Name = "button_OpenDir";
            this.button_OpenDir.UseVisualStyleBackColor = true;
            this.button_OpenDir.Click += new System.EventHandler(this.button_OpenDir_Click);
            // 
            // tabControl_Edit_SetShow
            // 
            this.tabControl_Edit_SetShow.Controls.Add(this.tabPage_Edit_Elements);
            this.tabControl_Edit_SetShow.Controls.Add(this.tabPage_Show_Set);
            resources.ApplyResources(this.tabControl_Edit_SetShow, "tabControl_Edit_SetShow");
            this.tabControl_Edit_SetShow.Name = "tabControl_Edit_SetShow";
            this.tabControl_Edit_SetShow.SelectedIndex = 0;
            // 
            // tabPage_Edit_Elements
            // 
            this.tabPage_Edit_Elements.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage_Edit_Elements.Controls.Add(this.groupBox_AddElemets);
            this.tabPage_Edit_Elements.Controls.Add(this.panel_WatchfaceElements);
            this.tabPage_Edit_Elements.Controls.Add(this.panel_ElementsOpt);
            this.tabPage_Edit_Elements.Controls.Add(this.panel_MainScreen_AOD);
            resources.ApplyResources(this.tabPage_Edit_Elements, "tabPage_Edit_Elements");
            this.tabPage_Edit_Elements.Name = "tabPage_Edit_Elements";
            // 
            // groupBox_AddElemets
            // 
            this.groupBox_AddElemets.Controls.Add(this.pictureBox_IconBackground);
            this.groupBox_AddElemets.Controls.Add(this.comboBox_AddBackground);
            this.groupBox_AddElemets.Controls.Add(this.pictureBox_IconDate);
            this.groupBox_AddElemets.Controls.Add(this.pictureBox_IconAir);
            this.groupBox_AddElemets.Controls.Add(this.pictureBox_IconSystem);
            this.groupBox_AddElemets.Controls.Add(this.pictureBox_IconTime);
            this.groupBox_AddElemets.Controls.Add(this.pictureBox_IconActivity);
            this.groupBox_AddElemets.Controls.Add(this.comboBox_AddSystem);
            this.groupBox_AddElemets.Controls.Add(this.comboBox_AddAir);
            this.groupBox_AddElemets.Controls.Add(this.comboBox_AddActivity);
            this.groupBox_AddElemets.Controls.Add(this.comboBox_AddDate);
            this.groupBox_AddElemets.Controls.Add(this.comboBox_AddTime);
            resources.ApplyResources(this.groupBox_AddElemets, "groupBox_AddElemets");
            this.groupBox_AddElemets.Name = "groupBox_AddElemets";
            this.groupBox_AddElemets.TabStop = false;
            this.groupBox_AddElemets.Paint += new System.Windows.Forms.PaintEventHandler(this.groupBox_Paint);
            // 
            // pictureBox_IconBackground
            // 
            this.pictureBox_IconBackground.BackgroundImage = global::Watch_Face_Editor.Properties.Resources.Background_icon;
            resources.ApplyResources(this.pictureBox_IconBackground, "pictureBox_IconBackground");
            this.pictureBox_IconBackground.Name = "pictureBox_IconBackground";
            this.pictureBox_IconBackground.TabStop = false;
            // 
            // comboBox_AddBackground
            // 
            this.comboBox_AddBackground.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_AddBackground.FormattingEnabled = true;
            this.comboBox_AddBackground.Items.AddRange(new object[] {
            resources.GetString("comboBox_AddBackground.Items")});
            resources.ApplyResources(this.comboBox_AddBackground, "comboBox_AddBackground");
            this.comboBox_AddBackground.Name = "comboBox_AddBackground";
            this.comboBox_AddBackground.SelectedIndexChanged += new System.EventHandler(this.comboBox_AddBackground_SelectedIndexChanged);
            // 
            // pictureBox_IconDate
            // 
            this.pictureBox_IconDate.BackgroundImage = global::Watch_Face_Editor.Properties.Resources.Very_Basic_Calendar_16;
            resources.ApplyResources(this.pictureBox_IconDate, "pictureBox_IconDate");
            this.pictureBox_IconDate.Name = "pictureBox_IconDate";
            this.pictureBox_IconDate.TabStop = false;
            // 
            // pictureBox_IconAir
            // 
            this.pictureBox_IconAir.BackgroundImage = global::Watch_Face_Editor.Properties.Resources.Weather_Partly_Cloudy_Rain_16;
            resources.ApplyResources(this.pictureBox_IconAir, "pictureBox_IconAir");
            this.pictureBox_IconAir.Name = "pictureBox_IconAir";
            this.pictureBox_IconAir.TabStop = false;
            // 
            // pictureBox_IconSystem
            // 
            this.pictureBox_IconSystem.BackgroundImage = global::Watch_Face_Editor.Properties.Resources.Logos_Administrative_Tools_16;
            resources.ApplyResources(this.pictureBox_IconSystem, "pictureBox_IconSystem");
            this.pictureBox_IconSystem.Name = "pictureBox_IconSystem";
            this.pictureBox_IconSystem.TabStop = false;
            // 
            // pictureBox_IconTime
            // 
            this.pictureBox_IconTime.BackgroundImage = global::Watch_Face_Editor.Properties.Resources.Sidebar_Search_16;
            resources.ApplyResources(this.pictureBox_IconTime, "pictureBox_IconTime");
            this.pictureBox_IconTime.Name = "pictureBox_IconTime";
            this.pictureBox_IconTime.TabStop = false;
            // 
            // pictureBox_IconActivity
            // 
            this.pictureBox_IconActivity.BackgroundImage = global::Watch_Face_Editor.Properties.Resources.Sports_Walking_16;
            resources.ApplyResources(this.pictureBox_IconActivity, "pictureBox_IconActivity");
            this.pictureBox_IconActivity.Name = "pictureBox_IconActivity";
            this.pictureBox_IconActivity.TabStop = false;
            // 
            // comboBox_AddSystem
            // 
            this.comboBox_AddSystem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_AddSystem.FormattingEnabled = true;
            this.comboBox_AddSystem.Items.AddRange(new object[] {
            resources.GetString("comboBox_AddSystem.Items"),
            resources.GetString("comboBox_AddSystem.Items1"),
            resources.GetString("comboBox_AddSystem.Items2"),
            resources.GetString("comboBox_AddSystem.Items3")});
            resources.ApplyResources(this.comboBox_AddSystem, "comboBox_AddSystem");
            this.comboBox_AddSystem.Name = "comboBox_AddSystem";
            this.comboBox_AddSystem.DropDownClosed += new System.EventHandler(this.comboBox_AddSystem_DropDownClosed);
            this.comboBox_AddSystem.Click += new System.EventHandler(this.comboBox_AddElements_Click);
            // 
            // comboBox_AddAir
            // 
            this.comboBox_AddAir.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.comboBox_AddAir, "comboBox_AddAir");
            this.comboBox_AddAir.FormattingEnabled = true;
            this.comboBox_AddAir.Items.AddRange(new object[] {
            resources.GetString("comboBox_AddAir.Items"),
            resources.GetString("comboBox_AddAir.Items1"),
            resources.GetString("comboBox_AddAir.Items2"),
            resources.GetString("comboBox_AddAir.Items3"),
            resources.GetString("comboBox_AddAir.Items4"),
            resources.GetString("comboBox_AddAir.Items5"),
            resources.GetString("comboBox_AddAir.Items6")});
            this.comboBox_AddAir.Name = "comboBox_AddAir";
            this.comboBox_AddAir.DropDownClosed += new System.EventHandler(this.comboBox_AddAir_DropDownClosed);
            this.comboBox_AddAir.Click += new System.EventHandler(this.comboBox_AddElements_Click);
            // 
            // comboBox_AddActivity
            // 
            this.comboBox_AddActivity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_AddActivity.FormattingEnabled = true;
            this.comboBox_AddActivity.Items.AddRange(new object[] {
            resources.GetString("comboBox_AddActivity.Items"),
            resources.GetString("comboBox_AddActivity.Items1"),
            resources.GetString("comboBox_AddActivity.Items2"),
            resources.GetString("comboBox_AddActivity.Items3"),
            resources.GetString("comboBox_AddActivity.Items4"),
            resources.GetString("comboBox_AddActivity.Items5"),
            resources.GetString("comboBox_AddActivity.Items6"),
            resources.GetString("comboBox_AddActivity.Items7")});
            resources.ApplyResources(this.comboBox_AddActivity, "comboBox_AddActivity");
            this.comboBox_AddActivity.Name = "comboBox_AddActivity";
            this.comboBox_AddActivity.DropDownClosed += new System.EventHandler(this.comboBox_AddActivity_DropDownClosed);
            this.comboBox_AddActivity.Click += new System.EventHandler(this.comboBox_AddElements_Click);
            // 
            // comboBox_AddDate
            // 
            this.comboBox_AddDate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_AddDate.FormattingEnabled = true;
            this.comboBox_AddDate.Items.AddRange(new object[] {
            resources.GetString("comboBox_AddDate.Items"),
            resources.GetString("comboBox_AddDate.Items1"),
            resources.GetString("comboBox_AddDate.Items2"),
            resources.GetString("comboBox_AddDate.Items3"),
            resources.GetString("comboBox_AddDate.Items4")});
            resources.ApplyResources(this.comboBox_AddDate, "comboBox_AddDate");
            this.comboBox_AddDate.Name = "comboBox_AddDate";
            this.comboBox_AddDate.DropDownClosed += new System.EventHandler(this.comboBox_AddDate_DropDownClosed);
            this.comboBox_AddDate.Click += new System.EventHandler(this.comboBox_AddElements_Click);
            // 
            // comboBox_AddTime
            // 
            this.comboBox_AddTime.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_AddTime.FormattingEnabled = true;
            this.comboBox_AddTime.Items.AddRange(new object[] {
            resources.GetString("comboBox_AddTime.Items"),
            resources.GetString("comboBox_AddTime.Items1"),
            resources.GetString("comboBox_AddTime.Items2")});
            resources.ApplyResources(this.comboBox_AddTime, "comboBox_AddTime");
            this.comboBox_AddTime.Name = "comboBox_AddTime";
            this.comboBox_AddTime.DropDownClosed += new System.EventHandler(this.comboBox_AddTime_DropDownClosed);
            this.comboBox_AddTime.Click += new System.EventHandler(this.comboBox_AddElements_Click);
            // 
            // panel_WatchfaceElements
            // 
            resources.ApplyResources(this.panel_WatchfaceElements, "panel_WatchfaceElements");
            this.panel_WatchfaceElements.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_WatchfaceElements.Controls.Add(this.tableLayoutPanel_ElemetsWatchFace);
            this.panel_WatchfaceElements.Name = "panel_WatchfaceElements";
            // 
            // tableLayoutPanel_ElemetsWatchFace
            // 
            this.tableLayoutPanel_ElemetsWatchFace.AllowDrop = true;
            resources.ApplyResources(this.tableLayoutPanel_ElemetsWatchFace, "tableLayoutPanel_ElemetsWatchFace");
            this.tableLayoutPanel_ElemetsWatchFace.Controls.Add(this.panel_UC_DigitalTime, 0, 0);
            this.tableLayoutPanel_ElemetsWatchFace.Controls.Add(this.panel_UC_AnalogTime, 0, 1);
            this.tableLayoutPanel_ElemetsWatchFace.Controls.Add(this.panel_UC_DateDay, 0, 2);
            this.tableLayoutPanel_ElemetsWatchFace.Controls.Add(this.panel_UC_DateMonth, 0, 3);
            this.tableLayoutPanel_ElemetsWatchFace.Controls.Add(this.panel_UC_DateYear, 0, 4);
            this.tableLayoutPanel_ElemetsWatchFace.Controls.Add(this.panel_UC_Background, 0, 15);
            this.tableLayoutPanel_ElemetsWatchFace.Controls.Add(this.panel_UC_DateWeek, 0, 5);
            this.tableLayoutPanel_ElemetsWatchFace.Controls.Add(this.panel_UC_Steps, 0, 8);
            this.tableLayoutPanel_ElemetsWatchFace.Controls.Add(this.panel_UC_Statuses, 0, 7);
            this.tableLayoutPanel_ElemetsWatchFace.Controls.Add(this.panel_UC_Shortcuts, 0, 6);
            this.tableLayoutPanel_ElemetsWatchFace.Controls.Add(this.panel_UC_Battery, 0, 9);
            this.tableLayoutPanel_ElemetsWatchFace.Controls.Add(this.panel_UC_Heart, 0, 10);
            this.tableLayoutPanel_ElemetsWatchFace.Controls.Add(this.panel_UC_Calories, 0, 11);
            this.tableLayoutPanel_ElemetsWatchFace.Controls.Add(this.panel_UC_PAI, 0, 12);
            this.tableLayoutPanel_ElemetsWatchFace.Controls.Add(this.panel_UC_Distance, 0, 13);
            this.tableLayoutPanel_ElemetsWatchFace.Controls.Add(this.panel_UC_Weather, 0, 14);
            this.tableLayoutPanel_ElemetsWatchFace.Name = "tableLayoutPanel_ElemetsWatchFace";
            this.tableLayoutPanel_ElemetsWatchFace.DragOver += new System.Windows.Forms.DragEventHandler(this.tableLayoutPanel1_DragOver);
            // 
            // panel_UC_DigitalTime
            // 
            resources.ApplyResources(this.panel_UC_DigitalTime, "panel_UC_DigitalTime");
            this.panel_UC_DigitalTime.Controls.Add(this.uCtrl_DigitalTime_Elm);
            this.panel_UC_DigitalTime.Name = "panel_UC_DigitalTime";
            // 
            // uCtrl_DigitalTime_Elm
            // 
            resources.ApplyResources(this.uCtrl_DigitalTime_Elm, "uCtrl_DigitalTime_Elm");
            this.uCtrl_DigitalTime_Elm.Name = "uCtrl_DigitalTime_Elm";
            this.uCtrl_DigitalTime_Elm.VisibleElementChanged += new ControlLibrary.UCtrl_DigitalTime_Elm.VisibleElementChangedHandler(this.uCtrl_DigitalTime_Elm_VisibleElementChanged);
            this.uCtrl_DigitalTime_Elm.VisibleOptionsChanged += new ControlLibrary.UCtrl_DigitalTime_Elm.VisibleOptionsChangedHandler(this.uCtrl_DigitalTime_Elm_VisibleOptionsChanged);
            this.uCtrl_DigitalTime_Elm.OptionsMoved += new ControlLibrary.UCtrl_DigitalTime_Elm.OptionsMovedHandler(this.uCtrl_DigitalTime_Elm_OptionsMoved);
            this.uCtrl_DigitalTime_Elm.SelectChanged += new ControlLibrary.UCtrl_DigitalTime_Elm.SelectChangedHandler(this.uCtrl_DigitalTime_Elm_SelectChanged);
            this.uCtrl_DigitalTime_Elm.DelElement += new ControlLibrary.UCtrl_DigitalTime_Elm.DelElementHandler(this.uCtrl_Elm_DelElement);
            this.uCtrl_DigitalTime_Elm.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Control_MouseDown);
            this.uCtrl_DigitalTime_Elm.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Control_MouseMove);
            this.uCtrl_DigitalTime_Elm.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Control_MouseUp);
            // 
            // panel_UC_AnalogTime
            // 
            resources.ApplyResources(this.panel_UC_AnalogTime, "panel_UC_AnalogTime");
            this.panel_UC_AnalogTime.Controls.Add(this.uCtrl_AnalogTime_Elm);
            this.panel_UC_AnalogTime.Name = "panel_UC_AnalogTime";
            // 
            // uCtrl_AnalogTime_Elm
            // 
            resources.ApplyResources(this.uCtrl_AnalogTime_Elm, "uCtrl_AnalogTime_Elm");
            this.uCtrl_AnalogTime_Elm.Name = "uCtrl_AnalogTime_Elm";
            this.uCtrl_AnalogTime_Elm.VisibleElementChanged += new ControlLibrary.UCtrl_AnalogTime_Elm.VisibleElementChangedHandler(this.uCtrl_AnalogTime_Elm_VisibleElementChanged);
            this.uCtrl_AnalogTime_Elm.VisibleOptionsChanged += new ControlLibrary.UCtrl_AnalogTime_Elm.VisibleOptionsChangedHandler(this.uCtrl_AnalogTime_Elm_VisibleOptionsChanged);
            this.uCtrl_AnalogTime_Elm.OptionsMoved += new ControlLibrary.UCtrl_AnalogTime_Elm.OptionsMovedHandler(this.uCtrl_AnalogTime_Elm_OptionsMoved);
            this.uCtrl_AnalogTime_Elm.SelectChanged += new ControlLibrary.UCtrl_AnalogTime_Elm.SelectChangedHandler(this.uCtrl_AnalogTime_Elm_SelectChanged);
            this.uCtrl_AnalogTime_Elm.DelElement += new ControlLibrary.UCtrl_AnalogTime_Elm.DelElementHandler(this.uCtrl_Elm_DelElement);
            this.uCtrl_AnalogTime_Elm.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Control_MouseDown);
            this.uCtrl_AnalogTime_Elm.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Control_MouseMove);
            this.uCtrl_AnalogTime_Elm.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Control_MouseUp);
            // 
            // panel_UC_DateDay
            // 
            resources.ApplyResources(this.panel_UC_DateDay, "panel_UC_DateDay");
            this.panel_UC_DateDay.Controls.Add(this.uCtrl_DateDay_Elm);
            this.panel_UC_DateDay.Name = "panel_UC_DateDay";
            // 
            // uCtrl_DateDay_Elm
            // 
            resources.ApplyResources(this.uCtrl_DateDay_Elm, "uCtrl_DateDay_Elm");
            this.uCtrl_DateDay_Elm.Name = "uCtrl_DateDay_Elm";
            this.uCtrl_DateDay_Elm.VisibleElementChanged += new ControlLibrary.UCtrl_DateDay_Elm.VisibleElementChangedHandler(this.uCtrl_DateDay_Elm_VisibleElementChanged);
            this.uCtrl_DateDay_Elm.VisibleOptionsChanged += new ControlLibrary.UCtrl_DateDay_Elm.VisibleOptionsChangedHandler(this.uCtrl_DateDay_Elm_VisibleOptionsChanged);
            this.uCtrl_DateDay_Elm.OptionsMoved += new ControlLibrary.UCtrl_DateDay_Elm.OptionsMovedHandler(this.uCtrl_DateDay_Elm_OptionsMoved);
            this.uCtrl_DateDay_Elm.SelectChanged += new ControlLibrary.UCtrl_DateDay_Elm.SelectChangedHandler(this.uCtrl_DateDay_Elm_SelectChanged);
            this.uCtrl_DateDay_Elm.DelElement += new ControlLibrary.UCtrl_DateDay_Elm.DelElementHandler(this.uCtrl_Elm_DelElement);
            this.uCtrl_DateDay_Elm.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Control_MouseDown);
            this.uCtrl_DateDay_Elm.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Control_MouseMove);
            this.uCtrl_DateDay_Elm.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Control_MouseUp);
            // 
            // panel_UC_DateMonth
            // 
            resources.ApplyResources(this.panel_UC_DateMonth, "panel_UC_DateMonth");
            this.panel_UC_DateMonth.Controls.Add(this.uCtrl_DateMonth_Elm);
            this.panel_UC_DateMonth.Name = "panel_UC_DateMonth";
            // 
            // uCtrl_DateMonth_Elm
            // 
            resources.ApplyResources(this.uCtrl_DateMonth_Elm, "uCtrl_DateMonth_Elm");
            this.uCtrl_DateMonth_Elm.Name = "uCtrl_DateMonth_Elm";
            this.uCtrl_DateMonth_Elm.VisibleElementChanged += new ControlLibrary.UCtrl_DateMonth_Elm.VisibleElementChangedHandler(this.uCtrl_DateMonth_Elm_VisibleElementChanged);
            this.uCtrl_DateMonth_Elm.VisibleOptionsChanged += new ControlLibrary.UCtrl_DateMonth_Elm.VisibleOptionsChangedHandler(this.uCtrl_DateMonth_Elm_VisibleOptionsChanged);
            this.uCtrl_DateMonth_Elm.OptionsMoved += new ControlLibrary.UCtrl_DateMonth_Elm.OptionsMovedHandler(this.uCtrl_DateMonth_Elm_OptionsMoved);
            this.uCtrl_DateMonth_Elm.SelectChanged += new ControlLibrary.UCtrl_DateMonth_Elm.SelectChangedHandler(this.uCtrl_DateMonth_Elm_SelectChanged);
            this.uCtrl_DateMonth_Elm.DelElement += new ControlLibrary.UCtrl_DateMonth_Elm.DelElementHandler(this.uCtrl_Elm_DelElement);
            this.uCtrl_DateMonth_Elm.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Control_MouseDown);
            this.uCtrl_DateMonth_Elm.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Control_MouseMove);
            this.uCtrl_DateMonth_Elm.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Control_MouseUp);
            // 
            // panel_UC_DateYear
            // 
            resources.ApplyResources(this.panel_UC_DateYear, "panel_UC_DateYear");
            this.panel_UC_DateYear.Controls.Add(this.uCtrl_DateYear_Elm);
            this.panel_UC_DateYear.Name = "panel_UC_DateYear";
            // 
            // uCtrl_DateYear_Elm
            // 
            resources.ApplyResources(this.uCtrl_DateYear_Elm, "uCtrl_DateYear_Elm");
            this.uCtrl_DateYear_Elm.Name = "uCtrl_DateYear_Elm";
            this.uCtrl_DateYear_Elm.SelectChanged += new ControlLibrary.UCtrl_DateYear_Elm.SelectChangedHandler(this.uCtrl_DateYear_Elm_SelectChanged);
            this.uCtrl_DateYear_Elm.VisibleElementChanged += new ControlLibrary.UCtrl_DateYear_Elm.VisibleElementChangedHandler(this.uCtrl_DateYear_Elm_VisibleElementChanged);
            this.uCtrl_DateYear_Elm.DelElement += new ControlLibrary.UCtrl_DateYear_Elm.DelElementHandler(this.uCtrl_Elm_DelElement);
            this.uCtrl_DateYear_Elm.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Control_MouseDown);
            this.uCtrl_DateYear_Elm.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Control_MouseMove);
            this.uCtrl_DateYear_Elm.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Control_MouseUp);
            // 
            // panel_UC_Background
            // 
            resources.ApplyResources(this.panel_UC_Background, "panel_UC_Background");
            this.panel_UC_Background.BackColor = System.Drawing.Color.Red;
            this.panel_UC_Background.Controls.Add(this.uCtrl_Background_Elm);
            this.panel_UC_Background.Name = "panel_UC_Background";
            // 
            // uCtrl_Background_Elm
            // 
            resources.ApplyResources(this.uCtrl_Background_Elm, "uCtrl_Background_Elm");
            this.uCtrl_Background_Elm.BackColor = System.Drawing.SystemColors.Control;
            this.uCtrl_Background_Elm.Name = "uCtrl_Background_Elm";
            this.uCtrl_Background_Elm.SelectChanged += new ControlLibrary.UCtrl_Background_Elm.SelectChangedHandler(this.uCtrl_Background_Elm_SelectChanged);
            this.uCtrl_Background_Elm.VisibleElementChanged += new ControlLibrary.UCtrl_Background_Elm.VisibleElementChangedHandler(this.uCtrl_Background_Elm_VisibleElemenChanged);
            this.uCtrl_Background_Elm.DelElement += new ControlLibrary.UCtrl_Background_Elm.DelElementHandler(this.uCtrl_Background_Elm_DelElement);
            // 
            // panel_UC_DateWeek
            // 
            resources.ApplyResources(this.panel_UC_DateWeek, "panel_UC_DateWeek");
            this.panel_UC_DateWeek.Controls.Add(this.uCtrl_DateWeek_Elm);
            this.panel_UC_DateWeek.Name = "panel_UC_DateWeek";
            // 
            // uCtrl_DateWeek_Elm
            // 
            resources.ApplyResources(this.uCtrl_DateWeek_Elm, "uCtrl_DateWeek_Elm");
            this.uCtrl_DateWeek_Elm.Name = "uCtrl_DateWeek_Elm";
            this.uCtrl_DateWeek_Elm.VisibleElementChanged += new ControlLibrary.UCtrl_DateWeek_Elm.VisibleElementChangedHandler(this.uCtrl_DateWeek_Elm_VisibleElementChanged);
            this.uCtrl_DateWeek_Elm.VisibleOptionsChanged += new ControlLibrary.UCtrl_DateWeek_Elm.VisibleOptionsChangedHandler(this.uCtrl_DateWeek_Elm_VisibleOptionsChanged);
            this.uCtrl_DateWeek_Elm.OptionsMoved += new ControlLibrary.UCtrl_DateWeek_Elm.OptionsMovedHandler(this.uCtrl_DateWeek_Elm_OptionsMoved);
            this.uCtrl_DateWeek_Elm.SelectChanged += new ControlLibrary.UCtrl_DateWeek_Elm.SelectChangedHandler(this.uCtrl_DateWeek_Elm_SelectChanged);
            this.uCtrl_DateWeek_Elm.DelElement += new ControlLibrary.UCtrl_DateWeek_Elm.DelElementHandler(this.uCtrl_Elm_DelElement);
            this.uCtrl_DateWeek_Elm.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Control_MouseDown);
            this.uCtrl_DateWeek_Elm.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Control_MouseMove);
            this.uCtrl_DateWeek_Elm.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Control_MouseUp);
            // 
            // panel_UC_Steps
            // 
            resources.ApplyResources(this.panel_UC_Steps, "panel_UC_Steps");
            this.panel_UC_Steps.Controls.Add(this.uCtrl_Steps_Elm);
            this.panel_UC_Steps.Name = "panel_UC_Steps";
            // 
            // uCtrl_Steps_Elm
            // 
            resources.ApplyResources(this.uCtrl_Steps_Elm, "uCtrl_Steps_Elm");
            this.uCtrl_Steps_Elm.Name = "uCtrl_Steps_Elm";
            this.uCtrl_Steps_Elm.VisibleElementChanged += new ControlLibrary.UCtrl_Steps_Elm.VisibleElementChangedHandler(this.uCtrl_Steps_Elm_VisibleElementChanged);
            this.uCtrl_Steps_Elm.VisibleOptionsChanged += new ControlLibrary.UCtrl_Steps_Elm.VisibleOptionsChangedHandler(this.uCtrl_Steps_Elm_VisibleOptionsChanged);
            this.uCtrl_Steps_Elm.OptionsMoved += new ControlLibrary.UCtrl_Steps_Elm.OptionsMovedHandler(this.uCtrl_Steps_Elm_OptionsMoved);
            this.uCtrl_Steps_Elm.SelectChanged += new ControlLibrary.UCtrl_Steps_Elm.SelectChangedHandler(this.uCtrl_Steps_Elm_SelectChanged);
            this.uCtrl_Steps_Elm.DelElement += new ControlLibrary.UCtrl_Steps_Elm.DelElementHandler(this.uCtrl_Elm_DelElement);
            this.uCtrl_Steps_Elm.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Control_MouseDown);
            this.uCtrl_Steps_Elm.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Control_MouseMove);
            this.uCtrl_Steps_Elm.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Control_MouseUp);
            // 
            // panel_UC_Statuses
            // 
            resources.ApplyResources(this.panel_UC_Statuses, "panel_UC_Statuses");
            this.panel_UC_Statuses.Controls.Add(this.uCtrl_Statuses_Elm);
            this.panel_UC_Statuses.Name = "panel_UC_Statuses";
            // 
            // uCtrl_Statuses_Elm
            // 
            resources.ApplyResources(this.uCtrl_Statuses_Elm, "uCtrl_Statuses_Elm");
            this.uCtrl_Statuses_Elm.Name = "uCtrl_Statuses_Elm";
            this.uCtrl_Statuses_Elm.VisibleElementChanged += new ControlLibrary.UCtrl_Statuses_Elm.VisibleElementChangedHandler(this.uCtrl_Statuses_Elm_VisibleElementChanged);
            this.uCtrl_Statuses_Elm.VisibleOptionsChanged += new ControlLibrary.UCtrl_Statuses_Elm.VisibleOptionsChangedHandler(this.uCtrl_Statuses_Elm_VisibleOptionsChanged);
            this.uCtrl_Statuses_Elm.OptionsMoved += new ControlLibrary.UCtrl_Statuses_Elm.OptionsMovedHandler(this.uCtrl_Statuses_Elm_OptionsMoved);
            this.uCtrl_Statuses_Elm.SelectChanged += new ControlLibrary.UCtrl_Statuses_Elm.SelectChangedHandler(this.uCtrl_Statuses_Elm_SelectChanged);
            this.uCtrl_Statuses_Elm.DelElement += new ControlLibrary.UCtrl_Statuses_Elm.DelElementHandler(this.uCtrl_Elm_DelElement);
            this.uCtrl_Statuses_Elm.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Control_MouseDown);
            this.uCtrl_Statuses_Elm.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Control_MouseMove);
            this.uCtrl_Statuses_Elm.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Control_MouseUp);
            // 
            // panel_UC_Shortcuts
            // 
            resources.ApplyResources(this.panel_UC_Shortcuts, "panel_UC_Shortcuts");
            this.panel_UC_Shortcuts.Controls.Add(this.uCtrl_Shortcuts_Elm);
            this.panel_UC_Shortcuts.Name = "panel_UC_Shortcuts";
            // 
            // uCtrl_Shortcuts_Elm
            // 
            resources.ApplyResources(this.uCtrl_Shortcuts_Elm, "uCtrl_Shortcuts_Elm");
            this.uCtrl_Shortcuts_Elm.Name = "uCtrl_Shortcuts_Elm";
            this.uCtrl_Shortcuts_Elm.VisibleElementChanged += new ControlLibrary.UCtrl_Shortcuts_Elm.VisibleElementChangedHandler(this.uCtrl_Shortcuts_Elm_VisibleElementChanged);
            this.uCtrl_Shortcuts_Elm.VisibleOptionsChanged += new ControlLibrary.UCtrl_Shortcuts_Elm.VisibleOptionsChangedHandler(this.uCtrl_Shortcuts_Elm_VisibleOptionsChanged);
            this.uCtrl_Shortcuts_Elm.OptionsMoved += new ControlLibrary.UCtrl_Shortcuts_Elm.OptionsMovedHandler(this.uCtrl_Shortcuts_Elm_OptionsMoved);
            this.uCtrl_Shortcuts_Elm.SelectChanged += new ControlLibrary.UCtrl_Shortcuts_Elm.SelectChangedHandler(this.uCtrl_Shortcuts_Elm_SelectChanged);
            this.uCtrl_Shortcuts_Elm.DelElement += new ControlLibrary.UCtrl_Shortcuts_Elm.DelElementHandler(this.uCtrl_Elm_DelElement);
            this.uCtrl_Shortcuts_Elm.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Control_MouseDown);
            this.uCtrl_Shortcuts_Elm.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Control_MouseMove);
            this.uCtrl_Shortcuts_Elm.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Control_MouseUp);
            // 
            // panel_UC_Battery
            // 
            resources.ApplyResources(this.panel_UC_Battery, "panel_UC_Battery");
            this.panel_UC_Battery.Controls.Add(this.uCtrl_Battery_Elm);
            this.panel_UC_Battery.Name = "panel_UC_Battery";
            // 
            // uCtrl_Battery_Elm
            // 
            resources.ApplyResources(this.uCtrl_Battery_Elm, "uCtrl_Battery_Elm");
            this.uCtrl_Battery_Elm.Name = "uCtrl_Battery_Elm";
            this.uCtrl_Battery_Elm.VisibleElementChanged += new ControlLibrary.UCtrl_Battery_Elm.VisibleElementChangedHandler(this.uCtrl_Battery_Elm_VisibleElementChanged);
            this.uCtrl_Battery_Elm.VisibleOptionsChanged += new ControlLibrary.UCtrl_Battery_Elm.VisibleOptionsChangedHandler(this.uCtrl_Battery_Elm_VisibleOptionsChanged);
            this.uCtrl_Battery_Elm.OptionsMoved += new ControlLibrary.UCtrl_Battery_Elm.OptionsMovedHandler(this.uCtrl_Battery_Elm_OptionsMoved);
            this.uCtrl_Battery_Elm.SelectChanged += new ControlLibrary.UCtrl_Battery_Elm.SelectChangedHandler(this.uCtrl_Battery_Elm_SelectChanged);
            this.uCtrl_Battery_Elm.DelElement += new ControlLibrary.UCtrl_Battery_Elm.DelElementHandler(this.uCtrl_Elm_DelElement);
            this.uCtrl_Battery_Elm.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Control_MouseDown);
            this.uCtrl_Battery_Elm.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Control_MouseMove);
            this.uCtrl_Battery_Elm.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Control_MouseUp);
            // 
            // panel_UC_Heart
            // 
            resources.ApplyResources(this.panel_UC_Heart, "panel_UC_Heart");
            this.panel_UC_Heart.Controls.Add(this.uCtrl_Heart_Elm);
            this.panel_UC_Heart.Name = "panel_UC_Heart";
            // 
            // uCtrl_Heart_Elm
            // 
            resources.ApplyResources(this.uCtrl_Heart_Elm, "uCtrl_Heart_Elm");
            this.uCtrl_Heart_Elm.Name = "uCtrl_Heart_Elm";
            this.uCtrl_Heart_Elm.VisibleElementChanged += new ControlLibrary.UCtrl_Heart_Elm.VisibleElementChangedHandler(this.uCtrl_Heart_Elm_VisibleElementChanged);
            this.uCtrl_Heart_Elm.VisibleOptionsChanged += new ControlLibrary.UCtrl_Heart_Elm.VisibleOptionsChangedHandler(this.uCtrl_Heart_Elm_VisibleOptionsChanged);
            this.uCtrl_Heart_Elm.OptionsMoved += new ControlLibrary.UCtrl_Heart_Elm.OptionsMovedHandler(this.uCtrl_Heart_Elm_OptionsMoved);
            this.uCtrl_Heart_Elm.SelectChanged += new ControlLibrary.UCtrl_Heart_Elm.SelectChangedHandler(this.uCtrl_Heart_Elm_SelectChanged);
            this.uCtrl_Heart_Elm.DelElement += new ControlLibrary.UCtrl_Heart_Elm.DelElementHandler(this.uCtrl_Elm_DelElement);
            this.uCtrl_Heart_Elm.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Control_MouseDown);
            this.uCtrl_Heart_Elm.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Control_MouseMove);
            this.uCtrl_Heart_Elm.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Control_MouseUp);
            // 
            // panel_UC_Calories
            // 
            resources.ApplyResources(this.panel_UC_Calories, "panel_UC_Calories");
            this.panel_UC_Calories.Controls.Add(this.uCtrl_Calories_Elm);
            this.panel_UC_Calories.Name = "panel_UC_Calories";
            // 
            // uCtrl_Calories_Elm
            // 
            resources.ApplyResources(this.uCtrl_Calories_Elm, "uCtrl_Calories_Elm");
            this.uCtrl_Calories_Elm.Name = "uCtrl_Calories_Elm";
            this.uCtrl_Calories_Elm.VisibleElementChanged += new ControlLibrary.UCtrl_Calories_Elm.VisibleElementChangedHandler(this.uCtrl_Calories_Elm_VisibleElementChanged);
            this.uCtrl_Calories_Elm.VisibleOptionsChanged += new ControlLibrary.UCtrl_Calories_Elm.VisibleOptionsChangedHandler(this.uCtrl_Calories_Elm_VisibleOptionsChanged);
            this.uCtrl_Calories_Elm.OptionsMoved += new ControlLibrary.UCtrl_Calories_Elm.OptionsMovedHandler(this.uCtrl_Calories_Elm_OptionsMoved);
            this.uCtrl_Calories_Elm.SelectChanged += new ControlLibrary.UCtrl_Calories_Elm.SelectChangedHandler(this.uCtrl_Calories_Elm_SelectChanged);
            this.uCtrl_Calories_Elm.DelElement += new ControlLibrary.UCtrl_Calories_Elm.DelElementHandler(this.uCtrl_Elm_DelElement);
            this.uCtrl_Calories_Elm.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Control_MouseDown);
            this.uCtrl_Calories_Elm.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Control_MouseMove);
            this.uCtrl_Calories_Elm.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Control_MouseUp);
            // 
            // panel_UC_PAI
            // 
            resources.ApplyResources(this.panel_UC_PAI, "panel_UC_PAI");
            this.panel_UC_PAI.Controls.Add(this.uCtrl_PAI_Elm);
            this.panel_UC_PAI.Name = "panel_UC_PAI";
            // 
            // uCtrl_PAI_Elm
            // 
            resources.ApplyResources(this.uCtrl_PAI_Elm, "uCtrl_PAI_Elm");
            this.uCtrl_PAI_Elm.Name = "uCtrl_PAI_Elm";
            this.uCtrl_PAI_Elm.VisibleElementChanged += new ControlLibrary.UCtrl_PAI_Elm.VisibleElementChangedHandler(this.uCtrl_PAI_Elm_VisibleElementChanged);
            this.uCtrl_PAI_Elm.VisibleOptionsChanged += new ControlLibrary.UCtrl_PAI_Elm.VisibleOptionsChangedHandler(this.uCtrl_PAI_Elm_VisibleOptionsChanged);
            this.uCtrl_PAI_Elm.OptionsMoved += new ControlLibrary.UCtrl_PAI_Elm.OptionsMovedHandler(this.uCtrl_PAI_Elm_OptionsMoved);
            this.uCtrl_PAI_Elm.SelectChanged += new ControlLibrary.UCtrl_PAI_Elm.SelectChangedHandler(this.uCtrl_PAI_Elm_SelectChanged);
            this.uCtrl_PAI_Elm.DelElement += new ControlLibrary.UCtrl_PAI_Elm.DelElementHandler(this.uCtrl_Elm_DelElement);
            this.uCtrl_PAI_Elm.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Control_MouseDown);
            this.uCtrl_PAI_Elm.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Control_MouseMove);
            this.uCtrl_PAI_Elm.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Control_MouseUp);
            // 
            // panel_UC_Distance
            // 
            resources.ApplyResources(this.panel_UC_Distance, "panel_UC_Distance");
            this.panel_UC_Distance.Controls.Add(this.uCtrl_Distance_Elm);
            this.panel_UC_Distance.Name = "panel_UC_Distance";
            // 
            // uCtrl_Distance_Elm
            // 
            resources.ApplyResources(this.uCtrl_Distance_Elm, "uCtrl_Distance_Elm");
            this.uCtrl_Distance_Elm.Name = "uCtrl_Distance_Elm";
            this.uCtrl_Distance_Elm.SelectChanged += new ControlLibrary.UCtrl_Distance_Elm.SelectChangedHandler(this.uCtrl_Distance_Elm_SelectChanged);
            this.uCtrl_Distance_Elm.VisibleElementChanged += new ControlLibrary.UCtrl_Distance_Elm.VisibleElementChangedHandler(this.uCtrl_Distance_Elm_VisibleElementChanged);
            this.uCtrl_Distance_Elm.DelElement += new ControlLibrary.UCtrl_Distance_Elm.DelElementHandler(this.uCtrl_Elm_DelElement);
            this.uCtrl_Distance_Elm.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Control_MouseDown);
            this.uCtrl_Distance_Elm.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Control_MouseMove);
            this.uCtrl_Distance_Elm.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Control_MouseUp);
            // 
            // panel_UC_Weather
            // 
            resources.ApplyResources(this.panel_UC_Weather, "panel_UC_Weather");
            this.panel_UC_Weather.Controls.Add(this.uCtrl_Weather_Elm);
            this.panel_UC_Weather.Name = "panel_UC_Weather";
            // 
            // uCtrl_Weather_Elm
            // 
            resources.ApplyResources(this.uCtrl_Weather_Elm, "uCtrl_Weather_Elm");
            this.uCtrl_Weather_Elm.Name = "uCtrl_Weather_Elm";
            this.uCtrl_Weather_Elm.VisibleElementChanged += new ControlLibrary.UCtrl_Weather_Elm.VisibleElementChangedHandler(this.uCtrl_Weather_Elm_VisibleElementChanged);
            this.uCtrl_Weather_Elm.VisibleOptionsChanged += new ControlLibrary.UCtrl_Weather_Elm.VisibleOptionsChangedHandler(this.uCtrl_Weather_Elm_VisibleOptionsChanged);
            this.uCtrl_Weather_Elm.OptionsMoved += new ControlLibrary.UCtrl_Weather_Elm.OptionsMovedHandler(this.uCtrl_Weather_Elm_OptionsMoved);
            this.uCtrl_Weather_Elm.SelectChanged += new ControlLibrary.UCtrl_Weather_Elm.SelectChangedHandler(this.uCtrl_Weather_Elm_SelectChanged);
            this.uCtrl_Weather_Elm.DelElement += new ControlLibrary.UCtrl_Weather_Elm.DelElementHandler(this.uCtrl_Elm_DelElement);
            this.uCtrl_Weather_Elm.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Control_MouseDown);
            this.uCtrl_Weather_Elm.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Control_MouseMove);
            this.uCtrl_Weather_Elm.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Control_MouseUp);
            // 
            // panel_ElementsOpt
            // 
            resources.ApplyResources(this.panel_ElementsOpt, "panel_ElementsOpt");
            this.panel_ElementsOpt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_ElementsOpt.Controls.Add(this.uCtrl_Text_Weather_Opt);
            this.panel_ElementsOpt.Controls.Add(this.uCtrl_Shortcut_Opt);
            this.panel_ElementsOpt.Controls.Add(this.uCtrl_Segments_Opt);
            this.panel_ElementsOpt.Controls.Add(this.uCtrl_Icon_Opt);
            this.panel_ElementsOpt.Controls.Add(this.uCtrl_Linear_Scale_Opt);
            this.panel_ElementsOpt.Controls.Add(this.uCtrl_Circle_Scale_Opt);
            this.panel_ElementsOpt.Controls.Add(this.uCtrl_Images_Opt);
            this.panel_ElementsOpt.Controls.Add(this.uCtrl_Pointer_Opt);
            this.panel_ElementsOpt.Controls.Add(this.uCtrl_AmPm_Opt);
            this.panel_ElementsOpt.Controls.Add(this.uCtrl_Text_Opt);
            this.panel_ElementsOpt.Controls.Add(this.userCtrl_Background_Options);
            this.panel_ElementsOpt.Name = "panel_ElementsOpt";
            // 
            // uCtrl_Text_Weather_Opt
            // 
            resources.ApplyResources(this.uCtrl_Text_Weather_Opt, "uCtrl_Text_Weather_Opt");
            this.uCtrl_Text_Weather_Opt.Follow = false;
            this.uCtrl_Text_Weather_Opt.ImageError = true;
            this.uCtrl_Text_Weather_Opt.Name = "uCtrl_Text_Weather_Opt";
            this.uCtrl_Text_Weather_Opt.PaddingZero = true;
            // 
            // uCtrl_Shortcut_Opt
            // 
            resources.ApplyResources(this.uCtrl_Shortcut_Opt, "uCtrl_Shortcut_Opt");
            this.uCtrl_Shortcut_Opt.Name = "uCtrl_Shortcut_Opt";
            this.uCtrl_Shortcut_Opt.ValueChanged += new ControlLibrary.UCtrl_Shortcut_Opt.ValueChangedHandler(this.uCtrl_Shortcut_Opt_ValueChanged);
            // 
            // uCtrl_Segments_Opt
            // 
            resources.ApplyResources(this.uCtrl_Segments_Opt, "uCtrl_Segments_Opt");
            this.uCtrl_Segments_Opt.FixedRowsCount = false;
            this.uCtrl_Segments_Opt.ImagesCount = -1;
            this.uCtrl_Segments_Opt.Name = "uCtrl_Segments_Opt";
            this.uCtrl_Segments_Opt.ValueChanged += new ControlLibrary.UCtrl_Segments_Opt.ValueChangedHandler(this.uCtrl_Segments_Opt_ValueChanged);
            // 
            // uCtrl_Icon_Opt
            // 
            resources.ApplyResources(this.uCtrl_Icon_Opt, "uCtrl_Icon_Opt");
            this.uCtrl_Icon_Opt.Name = "uCtrl_Icon_Opt";
            this.uCtrl_Icon_Opt.ValueChanged += new ControlLibrary.UCtrl_Icon_Opt.ValueChangedHandler(this.uCtrl_Icon_Opt_ValueChanged);
            // 
            // uCtrl_Linear_Scale_Opt
            // 
            resources.ApplyResources(this.uCtrl_Linear_Scale_Opt, "uCtrl_Linear_Scale_Opt");
            this.uCtrl_Linear_Scale_Opt.Name = "uCtrl_Linear_Scale_Opt";
            this.uCtrl_Linear_Scale_Opt.ValueChanged += new ControlLibrary.UCtrl_Linear_Scale_Opt.ValueChangedHandler(this.uCtrl_Linear_Scale_Opt_ValueChanged);
            // 
            // uCtrl_Circle_Scale_Opt
            // 
            resources.ApplyResources(this.uCtrl_Circle_Scale_Opt, "uCtrl_Circle_Scale_Opt");
            this.uCtrl_Circle_Scale_Opt.Name = "uCtrl_Circle_Scale_Opt";
            this.uCtrl_Circle_Scale_Opt.ValueChanged += new ControlLibrary.UCtrl_Circle_Scale_Opt.ValueChangedHandler(this.uCtrl_Circle_Scale_Opt_ValueChanged);
            // 
            // uCtrl_Images_Opt
            // 
            resources.ApplyResources(this.uCtrl_Images_Opt, "uCtrl_Images_Opt");
            this.uCtrl_Images_Opt.ImagesCount = 10;
            this.uCtrl_Images_Opt.ImagesCountEnable = true;
            this.uCtrl_Images_Opt.Name = "uCtrl_Images_Opt";
            this.uCtrl_Images_Opt.ValueChanged += new ControlLibrary.UCtrl_Images_Opt.ValueChangedHandler(this.uCtrl_Images_Opt_ValueChanged);
            // 
            // uCtrl_Pointer_Opt
            // 
            resources.ApplyResources(this.uCtrl_Pointer_Opt, "uCtrl_Pointer_Opt");
            this.uCtrl_Pointer_Opt.Name = "uCtrl_Pointer_Opt";
            this.uCtrl_Pointer_Opt.ShowBackground = false;
            this.uCtrl_Pointer_Opt.TimeMode = false;
            this.uCtrl_Pointer_Opt.ValueChanged += new ControlLibrary.UCtrl_Pointer_Opt.ValueChangedHandler(this.uCtrl_Pointer_Opt_ValueChanged);
            // 
            // uCtrl_AmPm_Opt
            // 
            resources.ApplyResources(this.uCtrl_AmPm_Opt, "uCtrl_AmPm_Opt");
            this.uCtrl_AmPm_Opt.Name = "uCtrl_AmPm_Opt";
            this.uCtrl_AmPm_Opt.ValueChanged += new ControlLibrary.UCtrl_AmPm_Opt.ValueChangedHandler(this.uCtrl_AmPm_Opt_ValueChanged);
            // 
            // uCtrl_Text_Opt
            // 
            this.uCtrl_Text_Opt.Distance = false;
            resources.ApplyResources(this.uCtrl_Text_Opt, "uCtrl_Text_Opt");
            this.uCtrl_Text_Opt.Follow = true;
            this.uCtrl_Text_Opt.ImageError = true;
            this.uCtrl_Text_Opt.Name = "uCtrl_Text_Opt";
            this.uCtrl_Text_Opt.OptionalSymbol = true;
            this.uCtrl_Text_Opt.PaddingZero = false;
            this.uCtrl_Text_Opt.Year = false;
            this.uCtrl_Text_Opt.ValueChanged += new ControlLibrary.UCtrl_Text_Opt.ValueChangedHandler(this.uCtrl_Text_Opt_ValueChanged);
            // 
            // userCtrl_Background_Options
            // 
            this.userCtrl_Background_Options.AOD = false;
            resources.ApplyResources(this.userCtrl_Background_Options, "userCtrl_Background_Options");
            this.userCtrl_Background_Options.Name = "userCtrl_Background_Options";
            this.userCtrl_Background_Options.ValueChanged += new ControlLibrary.UCtrl_Background_Opt.ValueChangedHandler(this.userCtrl_Background_Options_ValueChanged);
            // 
            // panel_MainScreen_AOD
            // 
            this.panel_MainScreen_AOD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_MainScreen_AOD.Controls.Add(this.button_CopyAOD);
            this.panel_MainScreen_AOD.Controls.Add(this.button_RandomPreview);
            this.panel_MainScreen_AOD.Controls.Add(this.radioButton_ScreenIdle);
            this.panel_MainScreen_AOD.Controls.Add(this.radioButton_ScreenNormal);
            resources.ApplyResources(this.panel_MainScreen_AOD, "panel_MainScreen_AOD");
            this.panel_MainScreen_AOD.Name = "panel_MainScreen_AOD";
            // 
            // button_CopyAOD
            // 
            resources.ApplyResources(this.button_CopyAOD, "button_CopyAOD");
            this.button_CopyAOD.Name = "button_CopyAOD";
            this.button_CopyAOD.UseVisualStyleBackColor = true;
            this.button_CopyAOD.Click += new System.EventHandler(this.button_CopyAOD_Click);
            // 
            // button_RandomPreview
            // 
            resources.ApplyResources(this.button_RandomPreview, "button_RandomPreview");
            this.button_RandomPreview.Name = "button_RandomPreview";
            this.button_RandomPreview.UseVisualStyleBackColor = true;
            this.button_RandomPreview.Click += new System.EventHandler(this.button_RandomPreview_Click);
            // 
            // radioButton_ScreenIdle
            // 
            resources.ApplyResources(this.radioButton_ScreenIdle, "radioButton_ScreenIdle");
            this.radioButton_ScreenIdle.Name = "radioButton_ScreenIdle";
            this.radioButton_ScreenIdle.UseVisualStyleBackColor = true;
            // 
            // radioButton_ScreenNormal
            // 
            resources.ApplyResources(this.radioButton_ScreenNormal, "radioButton_ScreenNormal");
            this.radioButton_ScreenNormal.Checked = true;
            this.radioButton_ScreenNormal.Name = "radioButton_ScreenNormal";
            this.radioButton_ScreenNormal.TabStop = true;
            this.radioButton_ScreenNormal.UseVisualStyleBackColor = true;
            this.radioButton_ScreenNormal.CheckedChanged += new System.EventHandler(this.radioButton_ScreenNormal_CheckedChanged);
            // 
            // tabPage_Show_Set
            // 
            this.tabPage_Show_Set.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage_Show_Set.Controls.Add(this.panel_set);
            this.tabPage_Show_Set.Controls.Add(this.panel_PreviewStates);
            resources.ApplyResources(this.tabPage_Show_Set, "tabPage_Show_Set");
            this.tabPage_Show_Set.Name = "tabPage_Show_Set";
            // 
            // panel_set
            // 
            resources.ApplyResources(this.panel_set, "panel_set");
            this.panel_set.Controls.Add(this.userCtrl_Set12);
            this.panel_set.Controls.Add(this.userCtrl_Set11);
            this.panel_set.Controls.Add(this.userCtrl_Set10);
            this.panel_set.Controls.Add(this.userCtrl_Set9);
            this.panel_set.Controls.Add(this.userCtrl_Set8);
            this.panel_set.Controls.Add(this.userCtrl_Set7);
            this.panel_set.Controls.Add(this.userCtrl_Set6);
            this.panel_set.Controls.Add(this.userCtrl_Set5);
            this.panel_set.Controls.Add(this.userCtrl_Set4);
            this.panel_set.Controls.Add(this.userCtrl_Set3);
            this.panel_set.Controls.Add(this.userCtrl_Set2);
            this.panel_set.Controls.Add(this.userCtrl_Set1);
            this.panel_set.Name = "panel_set";
            // 
            // userCtrl_Set12
            // 
            resources.ApplyResources(this.userCtrl_Set12, "userCtrl_Set12");
            this.userCtrl_Set12.Collapsed = true;
            this.userCtrl_Set12.Name = "userCtrl_Set12";
            this.userCtrl_Set12.SetNumber = 12;
            this.userCtrl_Set12.Collapse += new ControlLibrary.UCtrl_Set.CollapseHandler(this.userCtrl_Set12_Collapse);
            this.userCtrl_Set12.ValueChanged += new ControlLibrary.UCtrl_Set.ValueChangedHandler(this.userCtrl_Set_ValueChanged);
            // 
            // userCtrl_Set11
            // 
            resources.ApplyResources(this.userCtrl_Set11, "userCtrl_Set11");
            this.userCtrl_Set11.Collapsed = true;
            this.userCtrl_Set11.Name = "userCtrl_Set11";
            this.userCtrl_Set11.SetNumber = 11;
            this.userCtrl_Set11.Collapse += new ControlLibrary.UCtrl_Set.CollapseHandler(this.userCtrl_Set11_Collapse);
            this.userCtrl_Set11.ValueChanged += new ControlLibrary.UCtrl_Set.ValueChangedHandler(this.userCtrl_Set_ValueChanged);
            // 
            // userCtrl_Set10
            // 
            resources.ApplyResources(this.userCtrl_Set10, "userCtrl_Set10");
            this.userCtrl_Set10.Collapsed = true;
            this.userCtrl_Set10.Name = "userCtrl_Set10";
            this.userCtrl_Set10.SetNumber = 10;
            this.userCtrl_Set10.Collapse += new ControlLibrary.UCtrl_Set.CollapseHandler(this.userCtrl_Set10_Collapse);
            this.userCtrl_Set10.ValueChanged += new ControlLibrary.UCtrl_Set.ValueChangedHandler(this.userCtrl_Set_ValueChanged);
            // 
            // userCtrl_Set9
            // 
            resources.ApplyResources(this.userCtrl_Set9, "userCtrl_Set9");
            this.userCtrl_Set9.Collapsed = true;
            this.userCtrl_Set9.Name = "userCtrl_Set9";
            this.userCtrl_Set9.SetNumber = 9;
            this.userCtrl_Set9.Collapse += new ControlLibrary.UCtrl_Set.CollapseHandler(this.userCtrl_Set9_Collapse);
            this.userCtrl_Set9.ValueChanged += new ControlLibrary.UCtrl_Set.ValueChangedHandler(this.userCtrl_Set_ValueChanged);
            // 
            // userCtrl_Set8
            // 
            resources.ApplyResources(this.userCtrl_Set8, "userCtrl_Set8");
            this.userCtrl_Set8.Collapsed = true;
            this.userCtrl_Set8.Name = "userCtrl_Set8";
            this.userCtrl_Set8.SetNumber = 8;
            this.userCtrl_Set8.Collapse += new ControlLibrary.UCtrl_Set.CollapseHandler(this.userCtrl_Set8_Collapse);
            this.userCtrl_Set8.ValueChanged += new ControlLibrary.UCtrl_Set.ValueChangedHandler(this.userCtrl_Set_ValueChanged);
            // 
            // userCtrl_Set7
            // 
            resources.ApplyResources(this.userCtrl_Set7, "userCtrl_Set7");
            this.userCtrl_Set7.Collapsed = true;
            this.userCtrl_Set7.Name = "userCtrl_Set7";
            this.userCtrl_Set7.SetNumber = 7;
            this.userCtrl_Set7.Collapse += new ControlLibrary.UCtrl_Set.CollapseHandler(this.userCtrl_Set7_Collapse);
            this.userCtrl_Set7.ValueChanged += new ControlLibrary.UCtrl_Set.ValueChangedHandler(this.userCtrl_Set_ValueChanged);
            // 
            // userCtrl_Set6
            // 
            resources.ApplyResources(this.userCtrl_Set6, "userCtrl_Set6");
            this.userCtrl_Set6.Collapsed = true;
            this.userCtrl_Set6.Name = "userCtrl_Set6";
            this.userCtrl_Set6.SetNumber = 6;
            this.userCtrl_Set6.Collapse += new ControlLibrary.UCtrl_Set.CollapseHandler(this.userCtrl_Set6_Collapse);
            this.userCtrl_Set6.ValueChanged += new ControlLibrary.UCtrl_Set.ValueChangedHandler(this.userCtrl_Set_ValueChanged);
            // 
            // userCtrl_Set5
            // 
            resources.ApplyResources(this.userCtrl_Set5, "userCtrl_Set5");
            this.userCtrl_Set5.Collapsed = true;
            this.userCtrl_Set5.Name = "userCtrl_Set5";
            this.userCtrl_Set5.SetNumber = 5;
            this.userCtrl_Set5.Collapse += new ControlLibrary.UCtrl_Set.CollapseHandler(this.userCtrl_Set5_Collapse);
            this.userCtrl_Set5.ValueChanged += new ControlLibrary.UCtrl_Set.ValueChangedHandler(this.userCtrl_Set_ValueChanged);
            // 
            // userCtrl_Set4
            // 
            resources.ApplyResources(this.userCtrl_Set4, "userCtrl_Set4");
            this.userCtrl_Set4.Collapsed = true;
            this.userCtrl_Set4.Name = "userCtrl_Set4";
            this.userCtrl_Set4.SetNumber = 4;
            this.userCtrl_Set4.Collapse += new ControlLibrary.UCtrl_Set.CollapseHandler(this.userCtrl_Set4_Collapse);
            this.userCtrl_Set4.ValueChanged += new ControlLibrary.UCtrl_Set.ValueChangedHandler(this.userCtrl_Set_ValueChanged);
            // 
            // userCtrl_Set3
            // 
            resources.ApplyResources(this.userCtrl_Set3, "userCtrl_Set3");
            this.userCtrl_Set3.Collapsed = true;
            this.userCtrl_Set3.Name = "userCtrl_Set3";
            this.userCtrl_Set3.SetNumber = 3;
            this.userCtrl_Set3.Collapse += new ControlLibrary.UCtrl_Set.CollapseHandler(this.userCtrl_Set3_Collapse);
            this.userCtrl_Set3.ValueChanged += new ControlLibrary.UCtrl_Set.ValueChangedHandler(this.userCtrl_Set_ValueChanged);
            // 
            // userCtrl_Set2
            // 
            resources.ApplyResources(this.userCtrl_Set2, "userCtrl_Set2");
            this.userCtrl_Set2.Collapsed = true;
            this.userCtrl_Set2.Name = "userCtrl_Set2";
            this.userCtrl_Set2.SetNumber = 2;
            this.userCtrl_Set2.Collapse += new ControlLibrary.UCtrl_Set.CollapseHandler(this.userCtrl_Set2_Collapse);
            this.userCtrl_Set2.ValueChanged += new ControlLibrary.UCtrl_Set.ValueChangedHandler(this.userCtrl_Set_ValueChanged);
            // 
            // userCtrl_Set1
            // 
            resources.ApplyResources(this.userCtrl_Set1, "userCtrl_Set1");
            this.userCtrl_Set1.Collapsed = true;
            this.userCtrl_Set1.Name = "userCtrl_Set1";
            this.userCtrl_Set1.SetNumber = 1;
            this.userCtrl_Set1.Collapse += new ControlLibrary.UCtrl_Set.CollapseHandler(this.userCtrl_Set1_Collapse);
            this.userCtrl_Set1.ValueChanged += new ControlLibrary.UCtrl_Set.ValueChangedHandler(this.userCtrl_Set_ValueChanged);
            // 
            // panel_PreviewStates
            // 
            this.panel_PreviewStates.Controls.Add(this.button_JsonPreview_Random);
            this.panel_PreviewStates.Controls.Add(this.button_JsonPreview_Read);
            this.panel_PreviewStates.Controls.Add(this.button_JsonPreview_Write);
            resources.ApplyResources(this.panel_PreviewStates, "panel_PreviewStates");
            this.panel_PreviewStates.Name = "panel_PreviewStates";
            // 
            // button_JsonPreview_Random
            // 
            resources.ApplyResources(this.button_JsonPreview_Random, "button_JsonPreview_Random");
            this.button_JsonPreview_Random.Name = "button_JsonPreview_Random";
            this.button_JsonPreview_Random.UseVisualStyleBackColor = true;
            this.button_JsonPreview_Random.Click += new System.EventHandler(this.button_JsonPreview_Random_Click);
            // 
            // button_JsonPreview_Read
            // 
            resources.ApplyResources(this.button_JsonPreview_Read, "button_JsonPreview_Read");
            this.button_JsonPreview_Read.Name = "button_JsonPreview_Read";
            this.button_JsonPreview_Read.UseVisualStyleBackColor = true;
            this.button_JsonPreview_Read.Click += new System.EventHandler(this.button_JsonPreview_Read_Click);
            // 
            // button_JsonPreview_Write
            // 
            resources.ApplyResources(this.button_JsonPreview_Write, "button_JsonPreview_Write");
            this.button_JsonPreview_Write.Name = "button_JsonPreview_Write";
            this.button_JsonPreview_Write.UseVisualStyleBackColor = true;
            this.button_JsonPreview_Write.Click += new System.EventHandler(this.button_JsonPreview_Write_Click_1);
            // 
            // button_Add_Images
            // 
            resources.ApplyResources(this.button_Add_Images, "button_Add_Images");
            this.button_Add_Images.Name = "button_Add_Images";
            this.button_Add_Images.UseVisualStyleBackColor = true;
            this.button_Add_Images.Click += new System.EventHandler(this.button_Add_Images_Click);
            // 
            // button_New_Project
            // 
            resources.ApplyResources(this.button_New_Project, "button_New_Project");
            this.button_New_Project.Name = "button_New_Project";
            this.button_New_Project.UseVisualStyleBackColor = true;
            this.button_New_Project.Click += new System.EventHandler(this.button_New_Project_Click);
            // 
            // dataGridView_ImagesList
            // 
            this.dataGridView_ImagesList.AllowUserToAddRows = false;
            this.dataGridView_ImagesList.AllowUserToDeleteRows = false;
            this.dataGridView_ImagesList.AllowUserToResizeRows = false;
            this.dataGridView_ImagesList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_ImagesList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Number,
            this.NameFile,
            this.ColumnImage,
            this.Column1});
            resources.ApplyResources(this.dataGridView_ImagesList, "dataGridView_ImagesList");
            this.dataGridView_ImagesList.Name = "dataGridView_ImagesList";
            this.dataGridView_ImagesList.RowHeadersVisible = false;
            // 
            // Number
            // 
            this.Number.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Number.DefaultCellStyle = dataGridViewCellStyle3;
            resources.ApplyResources(this.Number, "Number");
            this.Number.Name = "Number";
            this.Number.ReadOnly = true;
            // 
            // NameFile
            // 
            this.NameFile.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            resources.ApplyResources(this.NameFile, "NameFile");
            this.NameFile.Name = "NameFile";
            this.NameFile.ReadOnly = true;
            // 
            // ColumnImage
            // 
            this.ColumnImage.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ColumnImage.FillWeight = 95F;
            resources.ApplyResources(this.ColumnImage, "ColumnImage");
            this.ColumnImage.Name = "ColumnImage";
            this.ColumnImage.ReadOnly = true;
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.NullValue = null;
            this.Column1.DefaultCellStyle = dataGridViewCellStyle4;
            this.Column1.FillWeight = 95F;
            resources.ApplyResources(this.Column1, "Column1");
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // button_JSON
            // 
            resources.ApplyResources(this.button_JSON, "button_JSON");
            this.button_JSON.Name = "button_JSON";
            this.button_JSON.UseVisualStyleBackColor = true;
            this.button_JSON.Click += new System.EventHandler(this.button_JSON_Click);
            // 
            // tabPageConverting
            // 
            this.tabPageConverting.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageConverting.Controls.Add(this.label_ConvertingHelp03);
            this.tabPageConverting.Controls.Add(this.label_ConvertingHelp02);
            this.tabPageConverting.Controls.Add(this.label_ConvertingHelp01);
            this.tabPageConverting.Controls.Add(this.label_ConvertingHelp);
            this.tabPageConverting.Controls.Add(this.button_Converting);
            this.tabPageConverting.Controls.Add(this.groupBox10);
            this.tabPageConverting.Controls.Add(this.groupBox9);
            resources.ApplyResources(this.tabPageConverting, "tabPageConverting");
            this.tabPageConverting.Name = "tabPageConverting";
            // 
            // label_ConvertingHelp03
            // 
            resources.ApplyResources(this.label_ConvertingHelp03, "label_ConvertingHelp03");
            this.label_ConvertingHelp03.Name = "label_ConvertingHelp03";
            // 
            // label_ConvertingHelp02
            // 
            resources.ApplyResources(this.label_ConvertingHelp02, "label_ConvertingHelp02");
            this.label_ConvertingHelp02.Name = "label_ConvertingHelp02";
            // 
            // label_ConvertingHelp01
            // 
            resources.ApplyResources(this.label_ConvertingHelp01, "label_ConvertingHelp01");
            this.label_ConvertingHelp01.Name = "label_ConvertingHelp01";
            // 
            // label_ConvertingHelp
            // 
            resources.ApplyResources(this.label_ConvertingHelp, "label_ConvertingHelp");
            this.label_ConvertingHelp.Name = "label_ConvertingHelp";
            // 
            // button_Converting
            // 
            resources.ApplyResources(this.button_Converting, "button_Converting");
            this.button_Converting.Name = "button_Converting";
            this.button_Converting.UseVisualStyleBackColor = true;
            this.button_Converting.Click += new System.EventHandler(this.button_Converting_Click);
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.numericUpDown_ConvertingOutput_Custom);
            this.groupBox10.Controls.Add(this.radioButton_ConvertingOutput_Custom);
            this.groupBox10.Controls.Add(this.radioButton_ConvertingOutput_TRexPro);
            this.groupBox10.Controls.Add(this.radioButton_ConvertingOutput_GTR3Pro);
            this.groupBox10.Controls.Add(this.radioButton_ConvertingOutput_GTR3);
            resources.ApplyResources(this.groupBox10, "groupBox10");
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.TabStop = false;
            this.groupBox10.Paint += new System.Windows.Forms.PaintEventHandler(this.groupBox_Paint);
            // 
            // numericUpDown_ConvertingOutput_Custom
            // 
            resources.ApplyResources(this.numericUpDown_ConvertingOutput_Custom, "numericUpDown_ConvertingOutput_Custom");
            this.numericUpDown_ConvertingOutput_Custom.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numericUpDown_ConvertingOutput_Custom.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDown_ConvertingOutput_Custom.Name = "numericUpDown_ConvertingOutput_Custom";
            this.numericUpDown_ConvertingOutput_Custom.Value = new decimal(new int[] {
            480,
            0,
            0,
            0});
            // 
            // radioButton_ConvertingOutput_Custom
            // 
            resources.ApplyResources(this.radioButton_ConvertingOutput_Custom, "radioButton_ConvertingOutput_Custom");
            this.radioButton_ConvertingOutput_Custom.Name = "radioButton_ConvertingOutput_Custom";
            this.radioButton_ConvertingOutput_Custom.UseVisualStyleBackColor = true;
            this.radioButton_ConvertingOutput_Custom.CheckedChanged += new System.EventHandler(this.radioButton_ConvertingOutput_Custom_CheckedChanged);
            // 
            // radioButton_ConvertingOutput_TRexPro
            // 
            resources.ApplyResources(this.radioButton_ConvertingOutput_TRexPro, "radioButton_ConvertingOutput_TRexPro");
            this.radioButton_ConvertingOutput_TRexPro.Name = "radioButton_ConvertingOutput_TRexPro";
            this.radioButton_ConvertingOutput_TRexPro.UseVisualStyleBackColor = true;
            this.radioButton_ConvertingOutput_TRexPro.CheckedChanged += new System.EventHandler(this.radioButton_ConvertingOutput_GTR3_CheckedChanged);
            // 
            // radioButton_ConvertingOutput_GTR3Pro
            // 
            resources.ApplyResources(this.radioButton_ConvertingOutput_GTR3Pro, "radioButton_ConvertingOutput_GTR3Pro");
            this.radioButton_ConvertingOutput_GTR3Pro.Checked = true;
            this.radioButton_ConvertingOutput_GTR3Pro.Name = "radioButton_ConvertingOutput_GTR3Pro";
            this.radioButton_ConvertingOutput_GTR3Pro.TabStop = true;
            this.radioButton_ConvertingOutput_GTR3Pro.UseVisualStyleBackColor = true;
            this.radioButton_ConvertingOutput_GTR3Pro.CheckedChanged += new System.EventHandler(this.radioButton_ConvertingOutput_GTR3_CheckedChanged);
            // 
            // radioButton_ConvertingOutput_GTR3
            // 
            resources.ApplyResources(this.radioButton_ConvertingOutput_GTR3, "radioButton_ConvertingOutput_GTR3");
            this.radioButton_ConvertingOutput_GTR3.Name = "radioButton_ConvertingOutput_GTR3";
            this.radioButton_ConvertingOutput_GTR3.UseVisualStyleBackColor = true;
            this.radioButton_ConvertingOutput_GTR3.CheckedChanged += new System.EventHandler(this.radioButton_ConvertingOutput_GTR3_CheckedChanged);
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.numericUpDown_ConvertingInput_Custom);
            this.groupBox9.Controls.Add(this.radioButton_ConvertingInput_Custom);
            this.groupBox9.Controls.Add(this.radioButton_ConvertingInput_TRexPro);
            this.groupBox9.Controls.Add(this.radioButton_ConvertingInput_GTR3Pro);
            this.groupBox9.Controls.Add(this.radioButton_ConvertingInput_GTR3);
            resources.ApplyResources(this.groupBox9, "groupBox9");
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.TabStop = false;
            this.groupBox9.Paint += new System.Windows.Forms.PaintEventHandler(this.groupBox_Paint);
            // 
            // numericUpDown_ConvertingInput_Custom
            // 
            resources.ApplyResources(this.numericUpDown_ConvertingInput_Custom, "numericUpDown_ConvertingInput_Custom");
            this.numericUpDown_ConvertingInput_Custom.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numericUpDown_ConvertingInput_Custom.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDown_ConvertingInput_Custom.Name = "numericUpDown_ConvertingInput_Custom";
            this.numericUpDown_ConvertingInput_Custom.Value = new decimal(new int[] {
            454,
            0,
            0,
            0});
            // 
            // radioButton_ConvertingInput_Custom
            // 
            resources.ApplyResources(this.radioButton_ConvertingInput_Custom, "radioButton_ConvertingInput_Custom");
            this.radioButton_ConvertingInput_Custom.Name = "radioButton_ConvertingInput_Custom";
            this.radioButton_ConvertingInput_Custom.UseVisualStyleBackColor = true;
            this.radioButton_ConvertingInput_Custom.CheckedChanged += new System.EventHandler(this.radioButton_ConvertingInput_Custom_CheckedChanged);
            // 
            // radioButton_ConvertingInput_TRexPro
            // 
            resources.ApplyResources(this.radioButton_ConvertingInput_TRexPro, "radioButton_ConvertingInput_TRexPro");
            this.radioButton_ConvertingInput_TRexPro.Name = "radioButton_ConvertingInput_TRexPro";
            this.radioButton_ConvertingInput_TRexPro.UseVisualStyleBackColor = true;
            this.radioButton_ConvertingInput_TRexPro.CheckedChanged += new System.EventHandler(this.radioButton_ConvertingInput_GTR3_CheckedChanged);
            // 
            // radioButton_ConvertingInput_GTR3Pro
            // 
            resources.ApplyResources(this.radioButton_ConvertingInput_GTR3Pro, "radioButton_ConvertingInput_GTR3Pro");
            this.radioButton_ConvertingInput_GTR3Pro.Name = "radioButton_ConvertingInput_GTR3Pro";
            this.radioButton_ConvertingInput_GTR3Pro.UseVisualStyleBackColor = true;
            this.radioButton_ConvertingInput_GTR3Pro.CheckedChanged += new System.EventHandler(this.radioButton_ConvertingInput_GTR3_CheckedChanged);
            // 
            // radioButton_ConvertingInput_GTR3
            // 
            resources.ApplyResources(this.radioButton_ConvertingInput_GTR3, "radioButton_ConvertingInput_GTR3");
            this.radioButton_ConvertingInput_GTR3.Checked = true;
            this.radioButton_ConvertingInput_GTR3.Name = "radioButton_ConvertingInput_GTR3";
            this.radioButton_ConvertingInput_GTR3.TabStop = true;
            this.radioButton_ConvertingInput_GTR3.UseVisualStyleBackColor = true;
            this.radioButton_ConvertingInput_GTR3.CheckedChanged += new System.EventHandler(this.radioButton_ConvertingInput_GTR3_CheckedChanged);
            // 
            // tabPage_Settings
            // 
            this.tabPage_Settings.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage_Settings.Controls.Add(this.button_Reset);
            this.tabPage_Settings.Controls.Add(this.numericUpDown_Gif_Speed);
            this.tabPage_Settings.Controls.Add(this.checkBox_AllWidgetsInGif);
            this.tabPage_Settings.Controls.Add(this.groupBox2);
            this.tabPage_Settings.Controls.Add(this.label355);
            this.tabPage_Settings.Controls.Add(this.checkBox_ShowIn12hourFormat);
            this.tabPage_Settings.Controls.Add(this.groupBox8);
            this.tabPage_Settings.Controls.Add(this.checkBox_JsonWarnings);
            this.tabPage_Settings.Controls.Add(this.comboBox_Language);
            this.tabPage_Settings.Controls.Add(this.label356);
            this.tabPage_Settings.Controls.Add(this.groupBox7);
            this.tabPage_Settings.Controls.Add(this.groupBox6);
            this.tabPage_Settings.Controls.Add(this.groupBox5);
            this.tabPage_Settings.Controls.Add(this.groupBox1);
            resources.ApplyResources(this.tabPage_Settings, "tabPage_Settings");
            this.tabPage_Settings.Name = "tabPage_Settings";
            // 
            // button_Reset
            // 
            resources.ApplyResources(this.button_Reset, "button_Reset");
            this.button_Reset.Name = "button_Reset";
            this.button_Reset.UseVisualStyleBackColor = true;
            this.button_Reset.Click += new System.EventHandler(this.button_Reset_Click);
            // 
            // numericUpDown_Gif_Speed
            // 
            this.numericUpDown_Gif_Speed.DecimalPlaces = 1;
            this.numericUpDown_Gif_Speed.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            resources.ApplyResources(this.numericUpDown_Gif_Speed, "numericUpDown_Gif_Speed");
            this.numericUpDown_Gif_Speed.Maximum = new decimal(new int[] {
            25,
            0,
            0,
            65536});
            this.numericUpDown_Gif_Speed.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDown_Gif_Speed.Name = "numericUpDown_Gif_Speed";
            this.numericUpDown_Gif_Speed.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // checkBox_AllWidgetsInGif
            // 
            resources.ApplyResources(this.checkBox_AllWidgetsInGif, "checkBox_AllWidgetsInGif");
            this.checkBox_AllWidgetsInGif.Name = "checkBox_AllWidgetsInGif";
            this.checkBox_AllWidgetsInGif.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button_WatchSkin_PathGet);
            this.groupBox2.Controls.Add(this.textBox_WatchSkin_Path);
            this.groupBox2.Controls.Add(this.checkBox_WatchSkin_Use);
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            this.groupBox2.Paint += new System.Windows.Forms.PaintEventHandler(this.groupBox_Paint);
            // 
            // button_WatchSkin_PathGet
            // 
            resources.ApplyResources(this.button_WatchSkin_PathGet, "button_WatchSkin_PathGet");
            this.button_WatchSkin_PathGet.Name = "button_WatchSkin_PathGet";
            this.button_WatchSkin_PathGet.UseVisualStyleBackColor = true;
            this.button_WatchSkin_PathGet.Click += new System.EventHandler(this.button_WatchSkin_PathGet_Click);
            // 
            // textBox_WatchSkin_Path
            // 
            resources.ApplyResources(this.textBox_WatchSkin_Path, "textBox_WatchSkin_Path");
            this.textBox_WatchSkin_Path.Name = "textBox_WatchSkin_Path";
            // 
            // checkBox_WatchSkin_Use
            // 
            resources.ApplyResources(this.checkBox_WatchSkin_Use, "checkBox_WatchSkin_Use");
            this.checkBox_WatchSkin_Use.Checked = true;
            this.checkBox_WatchSkin_Use.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_WatchSkin_Use.Name = "checkBox_WatchSkin_Use";
            this.checkBox_WatchSkin_Use.UseVisualStyleBackColor = true;
            this.checkBox_WatchSkin_Use.CheckedChanged += new System.EventHandler(this.checkBox_UnvisibleSettings_CheckedChanged);
            this.checkBox_WatchSkin_Use.Click += new System.EventHandler(this.checkBox_WatchSkin_Use_Click);
            // 
            // label355
            // 
            resources.ApplyResources(this.label355, "label355");
            this.label355.Name = "label355";
            // 
            // checkBox_ShowIn12hourFormat
            // 
            resources.ApplyResources(this.checkBox_ShowIn12hourFormat, "checkBox_ShowIn12hourFormat");
            this.checkBox_ShowIn12hourFormat.Checked = true;
            this.checkBox_ShowIn12hourFormat.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_ShowIn12hourFormat.Name = "checkBox_ShowIn12hourFormat";
            this.checkBox_ShowIn12hourFormat.UseVisualStyleBackColor = true;
            this.checkBox_ShowIn12hourFormat.CheckedChanged += new System.EventHandler(this.checkBox_VisibleSettings_CheckedChanged);
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.checkBox_Shortcuts_Image);
            this.groupBox8.Controls.Add(this.checkBox_Shortcuts_Border);
            this.groupBox8.Controls.Add(this.checkBox_Shortcuts_Area);
            resources.ApplyResources(this.groupBox8, "groupBox8");
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.TabStop = false;
            this.groupBox8.Paint += new System.Windows.Forms.PaintEventHandler(this.groupBox_Paint);
            // 
            // checkBox_Shortcuts_Image
            // 
            resources.ApplyResources(this.checkBox_Shortcuts_Image, "checkBox_Shortcuts_Image");
            this.checkBox_Shortcuts_Image.Name = "checkBox_Shortcuts_Image";
            this.checkBox_Shortcuts_Image.UseVisualStyleBackColor = true;
            this.checkBox_Shortcuts_Image.CheckedChanged += new System.EventHandler(this.checkBox_VisibleSettings_CheckedChanged);
            // 
            // checkBox_Shortcuts_Border
            // 
            resources.ApplyResources(this.checkBox_Shortcuts_Border, "checkBox_Shortcuts_Border");
            this.checkBox_Shortcuts_Border.Checked = true;
            this.checkBox_Shortcuts_Border.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_Shortcuts_Border.Name = "checkBox_Shortcuts_Border";
            this.checkBox_Shortcuts_Border.UseVisualStyleBackColor = true;
            this.checkBox_Shortcuts_Border.CheckedChanged += new System.EventHandler(this.checkBox_VisibleSettings_CheckedChanged);
            // 
            // checkBox_Shortcuts_Area
            // 
            resources.ApplyResources(this.checkBox_Shortcuts_Area, "checkBox_Shortcuts_Area");
            this.checkBox_Shortcuts_Area.Checked = true;
            this.checkBox_Shortcuts_Area.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_Shortcuts_Area.Name = "checkBox_Shortcuts_Area";
            this.checkBox_Shortcuts_Area.UseVisualStyleBackColor = true;
            this.checkBox_Shortcuts_Area.CheckedChanged += new System.EventHandler(this.checkBox_VisibleSettings_CheckedChanged);
            // 
            // checkBox_JsonWarnings
            // 
            resources.ApplyResources(this.checkBox_JsonWarnings, "checkBox_JsonWarnings");
            this.checkBox_JsonWarnings.Checked = true;
            this.checkBox_JsonWarnings.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_JsonWarnings.Name = "checkBox_JsonWarnings";
            this.checkBox_JsonWarnings.UseVisualStyleBackColor = true;
            // 
            // comboBox_Language
            // 
            this.comboBox_Language.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Language.FormattingEnabled = true;
            this.comboBox_Language.Items.AddRange(new object[] {
            resources.GetString("comboBox_Language.Items"),
            resources.GetString("comboBox_Language.Items1"),
            resources.GetString("comboBox_Language.Items2")});
            resources.ApplyResources(this.comboBox_Language, "comboBox_Language");
            this.comboBox_Language.Name = "comboBox_Language";
            this.comboBox_Language.SelectedIndexChanged += new System.EventHandler(this.comboBox_Language_SelectedIndexChanged);
            // 
            // label356
            // 
            resources.ApplyResources(this.label356, "label356");
            this.label356.Name = "label356";
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.radioButton_Settings_Pack_DoNotning);
            this.groupBox7.Controls.Add(this.radioButton_Settings_Pack_GoToFile);
            this.groupBox7.Controls.Add(this.radioButton_Settings_Pack_Dialog);
            resources.ApplyResources(this.groupBox7, "groupBox7");
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.TabStop = false;
            this.groupBox7.Paint += new System.Windows.Forms.PaintEventHandler(this.groupBox_Paint);
            // 
            // radioButton_Settings_Pack_DoNotning
            // 
            resources.ApplyResources(this.radioButton_Settings_Pack_DoNotning, "radioButton_Settings_Pack_DoNotning");
            this.radioButton_Settings_Pack_DoNotning.Name = "radioButton_Settings_Pack_DoNotning";
            this.radioButton_Settings_Pack_DoNotning.UseVisualStyleBackColor = true;
            this.radioButton_Settings_Pack_DoNotning.CheckedChanged += new System.EventHandler(this.radioButton_Settings_CheckedChanged);
            // 
            // radioButton_Settings_Pack_GoToFile
            // 
            resources.ApplyResources(this.radioButton_Settings_Pack_GoToFile, "radioButton_Settings_Pack_GoToFile");
            this.radioButton_Settings_Pack_GoToFile.Checked = true;
            this.radioButton_Settings_Pack_GoToFile.Name = "radioButton_Settings_Pack_GoToFile";
            this.radioButton_Settings_Pack_GoToFile.TabStop = true;
            this.radioButton_Settings_Pack_GoToFile.UseVisualStyleBackColor = true;
            this.radioButton_Settings_Pack_GoToFile.CheckedChanged += new System.EventHandler(this.radioButton_Settings_CheckedChanged);
            // 
            // radioButton_Settings_Pack_Dialog
            // 
            resources.ApplyResources(this.radioButton_Settings_Pack_Dialog, "radioButton_Settings_Pack_Dialog");
            this.radioButton_Settings_Pack_Dialog.Name = "radioButton_Settings_Pack_Dialog";
            this.radioButton_Settings_Pack_Dialog.UseVisualStyleBackColor = true;
            this.radioButton_Settings_Pack_Dialog.CheckedChanged += new System.EventHandler(this.radioButton_Settings_CheckedChanged);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.radioButton_Settings_Open_DoNotning);
            this.groupBox6.Controls.Add(this.radioButton_Settings_Open_Download);
            this.groupBox6.Controls.Add(this.radioButton_Settings_Open_Dialog);
            resources.ApplyResources(this.groupBox6, "groupBox6");
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.TabStop = false;
            this.groupBox6.Paint += new System.Windows.Forms.PaintEventHandler(this.groupBox_Paint);
            // 
            // radioButton_Settings_Open_DoNotning
            // 
            resources.ApplyResources(this.radioButton_Settings_Open_DoNotning, "radioButton_Settings_Open_DoNotning");
            this.radioButton_Settings_Open_DoNotning.Name = "radioButton_Settings_Open_DoNotning";
            this.radioButton_Settings_Open_DoNotning.UseVisualStyleBackColor = true;
            this.radioButton_Settings_Open_DoNotning.CheckedChanged += new System.EventHandler(this.radioButton_Settings_CheckedChanged);
            // 
            // radioButton_Settings_Open_Download
            // 
            resources.ApplyResources(this.radioButton_Settings_Open_Download, "radioButton_Settings_Open_Download");
            this.radioButton_Settings_Open_Download.Checked = true;
            this.radioButton_Settings_Open_Download.Name = "radioButton_Settings_Open_Download";
            this.radioButton_Settings_Open_Download.TabStop = true;
            this.radioButton_Settings_Open_Download.UseVisualStyleBackColor = true;
            this.radioButton_Settings_Open_Download.CheckedChanged += new System.EventHandler(this.radioButton_Settings_CheckedChanged);
            // 
            // radioButton_Settings_Open_Dialog
            // 
            resources.ApplyResources(this.radioButton_Settings_Open_Dialog, "radioButton_Settings_Open_Dialog");
            this.radioButton_Settings_Open_Dialog.Name = "radioButton_Settings_Open_Dialog";
            this.radioButton_Settings_Open_Dialog.UseVisualStyleBackColor = true;
            this.radioButton_Settings_Open_Dialog.CheckedChanged += new System.EventHandler(this.radioButton_Settings_CheckedChanged);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.radioButton_Settings_AfterUnpack_DoNothing);
            this.groupBox5.Controls.Add(this.radioButton_Settings_AfterUnpack_Download);
            this.groupBox5.Controls.Add(this.radioButton_Settings_AfterUnpack_Dialog);
            resources.ApplyResources(this.groupBox5, "groupBox5");
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.TabStop = false;
            this.groupBox5.Paint += new System.Windows.Forms.PaintEventHandler(this.groupBox_Paint);
            // 
            // radioButton_Settings_AfterUnpack_DoNothing
            // 
            resources.ApplyResources(this.radioButton_Settings_AfterUnpack_DoNothing, "radioButton_Settings_AfterUnpack_DoNothing");
            this.radioButton_Settings_AfterUnpack_DoNothing.Name = "radioButton_Settings_AfterUnpack_DoNothing";
            this.radioButton_Settings_AfterUnpack_DoNothing.UseVisualStyleBackColor = true;
            this.radioButton_Settings_AfterUnpack_DoNothing.CheckedChanged += new System.EventHandler(this.radioButton_Settings_CheckedChanged);
            // 
            // radioButton_Settings_AfterUnpack_Download
            // 
            resources.ApplyResources(this.radioButton_Settings_AfterUnpack_Download, "radioButton_Settings_AfterUnpack_Download");
            this.radioButton_Settings_AfterUnpack_Download.Checked = true;
            this.radioButton_Settings_AfterUnpack_Download.Name = "radioButton_Settings_AfterUnpack_Download";
            this.radioButton_Settings_AfterUnpack_Download.TabStop = true;
            this.radioButton_Settings_AfterUnpack_Download.UseVisualStyleBackColor = true;
            this.radioButton_Settings_AfterUnpack_Download.CheckedChanged += new System.EventHandler(this.radioButton_Settings_CheckedChanged);
            // 
            // radioButton_Settings_AfterUnpack_Dialog
            // 
            resources.ApplyResources(this.radioButton_Settings_AfterUnpack_Dialog, "radioButton_Settings_AfterUnpack_Dialog");
            this.radioButton_Settings_AfterUnpack_Dialog.Name = "radioButton_Settings_AfterUnpack_Dialog";
            this.radioButton_Settings_AfterUnpack_Dialog.UseVisualStyleBackColor = true;
            this.radioButton_Settings_AfterUnpack_Dialog.CheckedChanged += new System.EventHandler(this.radioButton_Settings_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButton_Settings_Unpack_Replace);
            this.groupBox1.Controls.Add(this.radioButton_Settings_Unpack_Save);
            this.groupBox1.Controls.Add(this.radioButton_Settings_Unpack_Dialog);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            this.groupBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.groupBox_Paint);
            // 
            // radioButton_Settings_Unpack_Replace
            // 
            resources.ApplyResources(this.radioButton_Settings_Unpack_Replace, "radioButton_Settings_Unpack_Replace");
            this.radioButton_Settings_Unpack_Replace.Name = "radioButton_Settings_Unpack_Replace";
            this.radioButton_Settings_Unpack_Replace.TabStop = true;
            this.radioButton_Settings_Unpack_Replace.UseVisualStyleBackColor = true;
            this.radioButton_Settings_Unpack_Replace.CheckedChanged += new System.EventHandler(this.radioButton_Settings_CheckedChanged);
            // 
            // radioButton_Settings_Unpack_Save
            // 
            resources.ApplyResources(this.radioButton_Settings_Unpack_Save, "radioButton_Settings_Unpack_Save");
            this.radioButton_Settings_Unpack_Save.Name = "radioButton_Settings_Unpack_Save";
            this.radioButton_Settings_Unpack_Save.TabStop = true;
            this.radioButton_Settings_Unpack_Save.UseVisualStyleBackColor = true;
            this.radioButton_Settings_Unpack_Save.CheckedChanged += new System.EventHandler(this.radioButton_Settings_CheckedChanged);
            // 
            // radioButton_Settings_Unpack_Dialog
            // 
            resources.ApplyResources(this.radioButton_Settings_Unpack_Dialog, "radioButton_Settings_Unpack_Dialog");
            this.radioButton_Settings_Unpack_Dialog.Checked = true;
            this.radioButton_Settings_Unpack_Dialog.Name = "radioButton_Settings_Unpack_Dialog";
            this.radioButton_Settings_Unpack_Dialog.TabStop = true;
            this.radioButton_Settings_Unpack_Dialog.UseVisualStyleBackColor = true;
            this.radioButton_Settings_Unpack_Dialog.CheckedChanged += new System.EventHandler(this.radioButton_Settings_CheckedChanged);
            // 
            // tabPage_About
            // 
            this.tabPage_About.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage_About.Controls.Add(this.label_TranslateHelp);
            this.tabPage_About.Controls.Add(this.label415);
            this.tabPage_About.Controls.Add(this.label414);
            this.tabPage_About.Controls.Add(this.label412);
            this.tabPage_About.Controls.Add(this.label413);
            this.tabPage_About.Controls.Add(this.linkLabel_py_amazfit_tools);
            this.tabPage_About.Controls.Add(this.label409);
            this.tabPage_About.Controls.Add(this.label408);
            this.tabPage_About.Controls.Add(this.label407);
            this.tabPage_About.Controls.Add(this.label_version_help);
            this.tabPage_About.Controls.Add(this.label406);
            this.tabPage_About.Controls.Add(this.pictureBox2);
            resources.ApplyResources(this.tabPage_About, "tabPage_About");
            this.tabPage_About.Name = "tabPage_About";
            // 
            // label_TranslateHelp
            // 
            resources.ApplyResources(this.label_TranslateHelp, "label_TranslateHelp");
            this.label_TranslateHelp.Name = "label_TranslateHelp";
            // 
            // label415
            // 
            resources.ApplyResources(this.label415, "label415");
            this.label415.Name = "label415";
            // 
            // label414
            // 
            resources.ApplyResources(this.label414, "label414");
            this.label414.Name = "label414";
            // 
            // label412
            // 
            resources.ApplyResources(this.label412, "label412");
            this.label412.Name = "label412";
            // 
            // label413
            // 
            resources.ApplyResources(this.label413, "label413");
            this.label413.Name = "label413";
            // 
            // linkLabel_py_amazfit_tools
            // 
            resources.ApplyResources(this.linkLabel_py_amazfit_tools, "linkLabel_py_amazfit_tools");
            this.linkLabel_py_amazfit_tools.Name = "linkLabel_py_amazfit_tools";
            this.linkLabel_py_amazfit_tools.TabStop = true;
            this.linkLabel_py_amazfit_tools.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_py_amazfit_tools_LinkClicked);
            // 
            // label409
            // 
            resources.ApplyResources(this.label409, "label409");
            this.label409.Name = "label409";
            // 
            // label408
            // 
            resources.ApplyResources(this.label408, "label408");
            this.label408.Name = "label408";
            // 
            // label407
            // 
            resources.ApplyResources(this.label407, "label407");
            this.label407.Name = "label407";
            // 
            // label_version_help
            // 
            resources.ApplyResources(this.label_version_help, "label_version_help");
            this.label_version_help.Name = "label_version_help";
            // 
            // label406
            // 
            resources.ApplyResources(this.label406, "label406");
            this.label406.Name = "label406";
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackgroundImage = global::Watch_Face_Editor.Properties.Resources.gtr_3;
            resources.ApplyResources(this.pictureBox2, "pictureBox2");
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.TabStop = false;
            // 
            // progressBar1
            // 
            resources.ApplyResources(this.progressBar1, "progressBar1");
            this.progressBar1.Name = "progressBar1";
            // 
            // radioButton_GTR3_Pro
            // 
            resources.ApplyResources(this.radioButton_GTR3_Pro, "radioButton_GTR3_Pro");
            this.radioButton_GTR3_Pro.Name = "radioButton_GTR3_Pro";
            this.radioButton_GTR3_Pro.UseVisualStyleBackColor = true;
            this.radioButton_GTR3_Pro.CheckedChanged += new System.EventHandler(this.radioButton_Model_CheckedChanged);
            // 
            // checkBox_WidgetsArea
            // 
            this.checkBox_WidgetsArea.Checked = true;
            this.checkBox_WidgetsArea.CheckState = System.Windows.Forms.CheckState.Checked;
            resources.ApplyResources(this.checkBox_WidgetsArea, "checkBox_WidgetsArea");
            this.checkBox_WidgetsArea.Name = "checkBox_WidgetsArea";
            this.checkBox_WidgetsArea.UseVisualStyleBackColor = true;
            this.checkBox_WidgetsArea.CheckedChanged += new System.EventHandler(this.checkBox_VisibleSettings_CheckedChanged);
            // 
            // checkBox_center_marker
            // 
            this.checkBox_center_marker.Checked = true;
            this.checkBox_center_marker.CheckState = System.Windows.Forms.CheckState.Checked;
            resources.ApplyResources(this.checkBox_center_marker, "checkBox_center_marker");
            this.checkBox_center_marker.Name = "checkBox_center_marker";
            this.checkBox_center_marker.UseVisualStyleBackColor = true;
            this.checkBox_center_marker.CheckedChanged += new System.EventHandler(this.checkBox_VisibleSettings_CheckedChanged);
            // 
            // button_CreatePreview
            // 
            resources.ApplyResources(this.button_CreatePreview, "button_CreatePreview");
            this.button_CreatePreview.Name = "button_CreatePreview";
            this.button_CreatePreview.UseVisualStyleBackColor = true;
            this.button_CreatePreview.Click += new System.EventHandler(this.button_CreatePreview_Click);
            // 
            // button_RefreshPreview
            // 
            resources.ApplyResources(this.button_RefreshPreview, "button_RefreshPreview");
            this.button_RefreshPreview.Name = "button_RefreshPreview";
            this.button_RefreshPreview.UseVisualStyleBackColor = true;
            this.button_RefreshPreview.Click += new System.EventHandler(this.button_RefreshPreview_Click);
            // 
            // checkBox_CircleScaleImage
            // 
            resources.ApplyResources(this.checkBox_CircleScaleImage, "checkBox_CircleScaleImage");
            this.checkBox_CircleScaleImage.Name = "checkBox_CircleScaleImage";
            this.checkBox_CircleScaleImage.UseVisualStyleBackColor = true;
            this.checkBox_CircleScaleImage.CheckedChanged += new System.EventHandler(this.checkBox_VisibleSettings_CheckedChanged);
            // 
            // checkBox_Show_Shortcuts
            // 
            this.checkBox_Show_Shortcuts.Checked = true;
            this.checkBox_Show_Shortcuts.CheckState = System.Windows.Forms.CheckState.Checked;
            resources.ApplyResources(this.checkBox_Show_Shortcuts, "checkBox_Show_Shortcuts");
            this.checkBox_Show_Shortcuts.Name = "checkBox_Show_Shortcuts";
            this.checkBox_Show_Shortcuts.UseVisualStyleBackColor = true;
            this.checkBox_Show_Shortcuts.CheckedChanged += new System.EventHandler(this.checkBox_VisibleSettings_CheckedChanged);
            // 
            // checkBox_crop
            // 
            this.checkBox_crop.Checked = true;
            this.checkBox_crop.CheckState = System.Windows.Forms.CheckState.Checked;
            resources.ApplyResources(this.checkBox_crop, "checkBox_crop");
            this.checkBox_crop.Name = "checkBox_crop";
            this.checkBox_crop.UseVisualStyleBackColor = true;
            this.checkBox_crop.CheckedChanged += new System.EventHandler(this.checkBox_VisibleSettings_CheckedChanged);
            // 
            // radioButton_GTS3
            // 
            resources.ApplyResources(this.radioButton_GTS3, "radioButton_GTS3");
            this.radioButton_GTS3.Name = "radioButton_GTS3";
            this.radioButton_GTS3.UseVisualStyleBackColor = true;
            this.radioButton_GTS3.CheckedChanged += new System.EventHandler(this.radioButton_Model_CheckedChanged);
            // 
            // checkBox_border
            // 
            resources.ApplyResources(this.checkBox_border, "checkBox_border");
            this.checkBox_border.Name = "checkBox_border";
            this.checkBox_border.UseVisualStyleBackColor = true;
            this.checkBox_border.CheckedChanged += new System.EventHandler(this.checkBox_VisibleSettings_CheckedChanged);
            // 
            // label_preview_Y
            // 
            resources.ApplyResources(this.label_preview_Y, "label_preview_Y");
            this.label_preview_Y.Name = "label_preview_Y";
            // 
            // label_preview_X
            // 
            resources.ApplyResources(this.label_preview_X, "label_preview_X");
            this.label_preview_X.Name = "label_preview_X";
            // 
            // radioButton_GTR3
            // 
            resources.ApplyResources(this.radioButton_GTR3, "radioButton_GTR3");
            this.radioButton_GTR3.Checked = true;
            this.radioButton_GTR3.Name = "radioButton_GTR3";
            this.radioButton_GTR3.TabStop = true;
            this.radioButton_GTR3.UseVisualStyleBackColor = true;
            this.radioButton_GTR3.CheckedChanged += new System.EventHandler(this.radioButton_Model_CheckedChanged);
            // 
            // button_SaveGIF
            // 
            resources.ApplyResources(this.button_SaveGIF, "button_SaveGIF");
            this.button_SaveGIF.Name = "button_SaveGIF";
            this.button_SaveGIF.UseVisualStyleBackColor = true;
            this.button_SaveGIF.Click += new System.EventHandler(this.button_SaveGIF_Click);
            // 
            // button_SavePNG
            // 
            resources.ApplyResources(this.button_SavePNG, "button_SavePNG");
            this.button_SavePNG.Name = "button_SavePNG";
            this.button_SavePNG.UseVisualStyleBackColor = true;
            this.button_SavePNG.Click += new System.EventHandler(this.button_SavePNG_Click);
            // 
            // checkBox_WebB
            // 
            resources.ApplyResources(this.checkBox_WebB, "checkBox_WebB");
            this.checkBox_WebB.Name = "checkBox_WebB";
            this.checkBox_WebB.UseVisualStyleBackColor = true;
            this.checkBox_WebB.CheckedChanged += new System.EventHandler(this.checkBox_WebW_CheckedChanged);
            // 
            // checkBox_WebW
            // 
            resources.ApplyResources(this.checkBox_WebW, "checkBox_WebW");
            this.checkBox_WebW.Name = "checkBox_WebW";
            this.checkBox_WebW.UseVisualStyleBackColor = true;
            this.checkBox_WebW.CheckedChanged += new System.EventHandler(this.checkBox_WebW_CheckedChanged);
            // 
            // button_PreviewBig
            // 
            resources.ApplyResources(this.button_PreviewBig, "button_PreviewBig");
            this.button_PreviewBig.Name = "button_PreviewBig";
            this.button_PreviewBig.UseVisualStyleBackColor = true;
            this.button_PreviewBig.Click += new System.EventHandler(this.pictureBox_Preview_Click);
            // 
            // label_version
            // 
            resources.ApplyResources(this.label_version, "label_version");
            this.label_version.Name = "label_version";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Background-Icon.png");
            this.imageList1.Images.SetKeyName(1, "Sidebar-Search-Icon.png");
            this.imageList1.Images.SetKeyName(2, "Calendar.ico");
            this.imageList1.Images.SetKeyName(3, "Sports-Walking-Icon.png");
            this.imageList1.Images.SetKeyName(4, "Weather-Partly-Cloudy-Rain-Icon.png");
            this.imageList1.Images.SetKeyName(5, "Tools-Icon.png");
            // 
            // button_pack_zip
            // 
            this.button_pack_zip.Image = global::Watch_Face_Editor.Properties.Resources.packaging;
            resources.ApplyResources(this.button_pack_zip, "button_pack_zip");
            this.button_pack_zip.Name = "button_pack_zip";
            this.button_pack_zip.UseVisualStyleBackColor = true;
            this.button_pack_zip.Click += new System.EventHandler(this.button_pack_zip_Click);
            // 
            // button_unpack_zip
            // 
            this.button_unpack_zip.Image = global::Watch_Face_Editor.Properties.Resources.unpacking;
            resources.ApplyResources(this.button_unpack_zip, "button_unpack_zip");
            this.button_unpack_zip.Name = "button_unpack_zip";
            this.button_unpack_zip.UseVisualStyleBackColor = true;
            this.button_unpack_zip.Click += new System.EventHandler(this.button_unpack_zip_Click);
            // 
            // pictureBox_Preview
            // 
            resources.ApplyResources(this.pictureBox_Preview, "pictureBox_Preview");
            this.pictureBox_Preview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox_Preview.Cursor = System.Windows.Forms.Cursors.Cross;
            this.pictureBox_Preview.Name = "pictureBox_Preview";
            this.pictureBox_Preview.TabStop = false;
            this.pictureBox_Preview.Click += new System.EventHandler(this.pictureBox_Preview_Click);
            // 
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.label_version);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.radioButton_GTR3_Pro);
            this.Controls.Add(this.checkBox_WidgetsArea);
            this.Controls.Add(this.checkBox_center_marker);
            this.Controls.Add(this.button_unpack_zip);
            this.Controls.Add(this.button_pack_zip);
            this.Controls.Add(this.button_CreatePreview);
            this.Controls.Add(this.button_RefreshPreview);
            this.Controls.Add(this.checkBox_CircleScaleImage);
            this.Controls.Add(this.pictureBox_Preview);
            this.Controls.Add(this.checkBox_Show_Shortcuts);
            this.Controls.Add(this.checkBox_crop);
            this.Controls.Add(this.radioButton_GTS3);
            this.Controls.Add(this.checkBox_border);
            this.Controls.Add(this.label_preview_Y);
            this.Controls.Add(this.label_preview_X);
            this.Controls.Add(this.radioButton_GTR3);
            this.Controls.Add(this.button_SaveGIF);
            this.Controls.Add(this.button_SavePNG);
            this.Controls.Add(this.checkBox_WebB);
            this.Controls.Add(this.checkBox_WebW);
            this.Controls.Add(this.button_PreviewBig);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.tabControl1.ResumeLayout(false);
            this.tabPage_Edit.ResumeLayout(false);
            this.tabControl_Edit_SetShow.ResumeLayout(false);
            this.tabPage_Edit_Elements.ResumeLayout(false);
            this.groupBox_AddElemets.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_IconBackground)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_IconDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_IconAir)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_IconSystem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_IconTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_IconActivity)).EndInit();
            this.panel_WatchfaceElements.ResumeLayout(false);
            this.panel_WatchfaceElements.PerformLayout();
            this.tableLayoutPanel_ElemetsWatchFace.ResumeLayout(false);
            this.tableLayoutPanel_ElemetsWatchFace.PerformLayout();
            this.panel_UC_DigitalTime.ResumeLayout(false);
            this.panel_UC_DigitalTime.PerformLayout();
            this.panel_UC_AnalogTime.ResumeLayout(false);
            this.panel_UC_AnalogTime.PerformLayout();
            this.panel_UC_DateDay.ResumeLayout(false);
            this.panel_UC_DateDay.PerformLayout();
            this.panel_UC_DateMonth.ResumeLayout(false);
            this.panel_UC_DateMonth.PerformLayout();
            this.panel_UC_DateYear.ResumeLayout(false);
            this.panel_UC_DateYear.PerformLayout();
            this.panel_UC_Background.ResumeLayout(false);
            this.panel_UC_Background.PerformLayout();
            this.panel_UC_DateWeek.ResumeLayout(false);
            this.panel_UC_DateWeek.PerformLayout();
            this.panel_UC_Steps.ResumeLayout(false);
            this.panel_UC_Steps.PerformLayout();
            this.panel_UC_Statuses.ResumeLayout(false);
            this.panel_UC_Statuses.PerformLayout();
            this.panel_UC_Shortcuts.ResumeLayout(false);
            this.panel_UC_Shortcuts.PerformLayout();
            this.panel_UC_Battery.ResumeLayout(false);
            this.panel_UC_Battery.PerformLayout();
            this.panel_UC_Heart.ResumeLayout(false);
            this.panel_UC_Heart.PerformLayout();
            this.panel_UC_Calories.ResumeLayout(false);
            this.panel_UC_Calories.PerformLayout();
            this.panel_UC_PAI.ResumeLayout(false);
            this.panel_UC_PAI.PerformLayout();
            this.panel_UC_Distance.ResumeLayout(false);
            this.panel_UC_Distance.PerformLayout();
            this.panel_UC_Weather.ResumeLayout(false);
            this.panel_UC_Weather.PerformLayout();
            this.panel_ElementsOpt.ResumeLayout(false);
            this.panel_MainScreen_AOD.ResumeLayout(false);
            this.panel_MainScreen_AOD.PerformLayout();
            this.tabPage_Show_Set.ResumeLayout(false);
            this.panel_set.ResumeLayout(false);
            this.panel_set.PerformLayout();
            this.panel_PreviewStates.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_ImagesList)).EndInit();
            this.tabPageConverting.ResumeLayout(false);
            this.tabPageConverting.PerformLayout();
            this.groupBox10.ResumeLayout(false);
            this.groupBox10.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_ConvertingOutput_Custom)).EndInit();
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_ConvertingInput_Custom)).EndInit();
            this.tabPage_Settings.ResumeLayout(false);
            this.tabPage_Settings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Gif_Speed)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage_About.ResumeLayout(false);
            this.tabPage_About.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Preview)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage_Edit;
        private System.Windows.Forms.Button button_Add_Images;
        private System.Windows.Forms.Button button_New_Project;
        private System.Windows.Forms.DataGridView dataGridView_ImagesList;
        private System.Windows.Forms.Button button_JSON;
        private System.Windows.Forms.Panel panel_set;
        private ControlLibrary.UCtrl_Set userCtrl_Set12;
        private ControlLibrary.UCtrl_Set userCtrl_Set11;
        private ControlLibrary.UCtrl_Set userCtrl_Set10;
        private ControlLibrary.UCtrl_Set userCtrl_Set9;
        private ControlLibrary.UCtrl_Set userCtrl_Set8;
        private ControlLibrary.UCtrl_Set userCtrl_Set7;
        private ControlLibrary.UCtrl_Set userCtrl_Set6;
        private ControlLibrary.UCtrl_Set userCtrl_Set5;
        private ControlLibrary.UCtrl_Set userCtrl_Set4;
        private ControlLibrary.UCtrl_Set userCtrl_Set3;
        private ControlLibrary.UCtrl_Set userCtrl_Set2;
        private ControlLibrary.UCtrl_Set userCtrl_Set1;
        private System.Windows.Forms.Panel panel_PreviewStates;
        private System.Windows.Forms.Button button_JsonPreview_Random;
        private System.Windows.Forms.Button button_JsonPreview_Read;
        private System.Windows.Forms.Button button_JsonPreview_Write;
        private System.Windows.Forms.TabPage tabPageConverting;
        private System.Windows.Forms.TabPage tabPage_Settings;
        private System.Windows.Forms.TabPage tabPage_About;
        private System.Windows.Forms.RadioButton radioButton_GTR3_Pro;
        private System.Windows.Forms.CheckBox checkBox_WidgetsArea;
        private System.Windows.Forms.CheckBox checkBox_center_marker;
        private System.Windows.Forms.Button button_unpack_zip;
        private System.Windows.Forms.Button button_pack_zip;
        private System.Windows.Forms.Button button_CreatePreview;
        private System.Windows.Forms.Button button_RefreshPreview;
        private System.Windows.Forms.CheckBox checkBox_CircleScaleImage;
        private System.Windows.Forms.PictureBox pictureBox_Preview;
        private System.Windows.Forms.CheckBox checkBox_Show_Shortcuts;
        public System.Windows.Forms.CheckBox checkBox_crop;
        private System.Windows.Forms.RadioButton radioButton_GTS3;
        private System.Windows.Forms.CheckBox checkBox_border;
        private System.Windows.Forms.Label label_preview_Y;
        private System.Windows.Forms.Label label_preview_X;
        private System.Windows.Forms.RadioButton radioButton_GTR3;
        private System.Windows.Forms.Button button_SaveGIF;
        private System.Windows.Forms.Button button_SavePNG;
        private System.Windows.Forms.CheckBox checkBox_WebB;
        private System.Windows.Forms.CheckBox checkBox_WebW;
        private System.Windows.Forms.Button button_PreviewBig;
        private System.Windows.Forms.Label label_version;
        private System.Windows.Forms.CheckBox checkBox_AllWidgetsInGif;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button_WatchSkin_PathGet;
        private System.Windows.Forms.TextBox textBox_WatchSkin_Path;
        private System.Windows.Forms.CheckBox checkBox_WatchSkin_Use;
        private System.Windows.Forms.Label label355;
        private System.Windows.Forms.CheckBox checkBox_ShowIn12hourFormat;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.CheckBox checkBox_Shortcuts_Border;
        private System.Windows.Forms.CheckBox checkBox_Shortcuts_Area;
        private System.Windows.Forms.CheckBox checkBox_JsonWarnings;
        private System.Windows.Forms.ComboBox comboBox_Language;
        private System.Windows.Forms.Label label356;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.RadioButton radioButton_Settings_Pack_DoNotning;
        private System.Windows.Forms.RadioButton radioButton_Settings_Pack_GoToFile;
        private System.Windows.Forms.RadioButton radioButton_Settings_Pack_Dialog;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.RadioButton radioButton_Settings_Open_DoNotning;
        private System.Windows.Forms.RadioButton radioButton_Settings_Open_Download;
        private System.Windows.Forms.RadioButton radioButton_Settings_Open_Dialog;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.RadioButton radioButton_Settings_AfterUnpack_DoNothing;
        private System.Windows.Forms.RadioButton radioButton_Settings_AfterUnpack_Download;
        private System.Windows.Forms.RadioButton radioButton_Settings_AfterUnpack_Dialog;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButton_Settings_Unpack_Replace;
        private System.Windows.Forms.RadioButton radioButton_Settings_Unpack_Save;
        private System.Windows.Forms.RadioButton radioButton_Settings_Unpack_Dialog;
        public System.Windows.Forms.NumericUpDown numericUpDown_Gif_Speed;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_ElemetsWatchFace;
        private System.Windows.Forms.Panel panel_ElementsOpt;
        private ControlLibrary.UCtrl_Background_Opt userCtrl_Background_Options;
        private System.Windows.Forms.TabControl tabControl_Edit_SetShow;
        private System.Windows.Forms.TabPage tabPage_Edit_Elements;
        private System.Windows.Forms.TabPage tabPage_Show_Set;
        private System.Windows.Forms.Panel panel_WatchfaceElements;
        private System.Windows.Forms.Panel panel_UC_Background;
        private ControlLibrary.UCtrl_Background_Elm uCtrl_Background_Elm;
        private System.Windows.Forms.Panel panel_UC_DigitalTime;
        private ControlLibrary.UCtrl_DigitalTime_Elm uCtrl_DigitalTime_Elm;
        private ControlLibrary.UCtrl_Text_Opt uCtrl_Text_Opt;
        private System.Windows.Forms.GroupBox groupBox_AddElemets;
        private System.Windows.Forms.ComboBox comboBox_AddTime;
        private System.Windows.Forms.PictureBox pictureBox_IconActivity;
        private System.Windows.Forms.ComboBox comboBox_AddSystem;
        private System.Windows.Forms.ComboBox comboBox_AddAir;
        private System.Windows.Forms.ComboBox comboBox_AddActivity;
        private System.Windows.Forms.ComboBox comboBox_AddDate;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.PictureBox pictureBox_IconAir;
        private System.Windows.Forms.PictureBox pictureBox_IconSystem;
        private System.Windows.Forms.PictureBox pictureBox_IconTime;
        private System.Windows.Forms.PictureBox pictureBox_IconDate;
        private System.Windows.Forms.Panel panel_MainScreen_AOD;
        private System.Windows.Forms.Button button_RandomPreview;
        private System.Windows.Forms.RadioButton radioButton_ScreenIdle;
        private System.Windows.Forms.RadioButton radioButton_ScreenNormal;
        private System.Windows.Forms.Button button_SaveJson;
        private System.Windows.Forms.Button button_OpenDir;
        private System.Windows.Forms.PictureBox pictureBox_IconBackground;
        private System.Windows.Forms.ComboBox comboBox_AddBackground;
        private System.Windows.Forms.DataGridViewTextBoxColumn Number;
        private System.Windows.Forms.DataGridViewTextBoxColumn NameFile;
        private System.Windows.Forms.DataGridViewImageColumn ColumnImage;
        private System.Windows.Forms.DataGridViewImageColumn Column1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private ControlLibrary.UCtrl_AmPm_Opt uCtrl_AmPm_Opt;
        private System.Windows.Forms.Panel panel_UC_AnalogTime;
        private ControlLibrary.UCtrl_AnalogTime_Elm uCtrl_AnalogTime_Elm;
        private ControlLibrary.UCtrl_Pointer_Opt uCtrl_Pointer_Opt;
        private System.Windows.Forms.Label label_TranslateHelp;
        private System.Windows.Forms.Label label415;
        private System.Windows.Forms.Label label414;
        private System.Windows.Forms.Label label412;
        private System.Windows.Forms.Label label413;
        private System.Windows.Forms.LinkLabel linkLabel_py_amazfit_tools;
        private System.Windows.Forms.Label label409;
        private System.Windows.Forms.Label label408;
        private System.Windows.Forms.Label label407;
        private System.Windows.Forms.Label label_version_help;
        private System.Windows.Forms.Label label406;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Panel panel_UC_DateYear;
        private System.Windows.Forms.Panel panel_UC_DateMonth;
        private ControlLibrary.UCtrl_DateMonth_Elm uCtrl_DateMonth_Elm;
        private System.Windows.Forms.Panel panel_UC_DateDay;
        private ControlLibrary.UCtrl_DateDay_Elm uCtrl_DateDay_Elm;
        private ControlLibrary.UCtrl_DateYear_Elm uCtrl_DateYear_Elm;
        private System.Windows.Forms.Panel panel_UC_DateWeek;
        private ControlLibrary.UCtrl_DateWeek_Elm uCtrl_DateWeek_Elm;
        private ControlLibrary.UCtrl_Images_Opt uCtrl_Images_Opt;
        private System.Windows.Forms.Button button_CopyAOD;
        private System.Windows.Forms.Panel panel_UC_Steps;
        private ControlLibrary.UCtrl_Steps_Elm uCtrl_Steps_Elm;
        private ControlLibrary.UCtrl_Icon_Opt uCtrl_Icon_Opt;
        private ControlLibrary.UCtrl_Linear_Scale_Opt uCtrl_Linear_Scale_Opt;
        private ControlLibrary.UCtrl_Circle_Scale_Opt uCtrl_Circle_Scale_Opt;
        private ControlLibrary.UCtrl_Segments_Opt uCtrl_Segments_Opt;
        private System.Windows.Forms.Button button_Reset;
        private System.Windows.Forms.Label label_ConvertingHelp03;
        private System.Windows.Forms.Label label_ConvertingHelp02;
        private System.Windows.Forms.Label label_ConvertingHelp01;
        private System.Windows.Forms.Label label_ConvertingHelp;
        private System.Windows.Forms.Button button_Converting;
        private System.Windows.Forms.GroupBox groupBox10;
        private System.Windows.Forms.NumericUpDown numericUpDown_ConvertingOutput_Custom;
        private System.Windows.Forms.RadioButton radioButton_ConvertingOutput_Custom;
        private System.Windows.Forms.RadioButton radioButton_ConvertingOutput_TRexPro;
        private System.Windows.Forms.RadioButton radioButton_ConvertingOutput_GTR3Pro;
        private System.Windows.Forms.RadioButton radioButton_ConvertingOutput_GTR3;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.NumericUpDown numericUpDown_ConvertingInput_Custom;
        private System.Windows.Forms.RadioButton radioButton_ConvertingInput_Custom;
        private System.Windows.Forms.RadioButton radioButton_ConvertingInput_TRexPro;
        private System.Windows.Forms.RadioButton radioButton_ConvertingInput_GTR3Pro;
        private System.Windows.Forms.RadioButton radioButton_ConvertingInput_GTR3;
        private System.Windows.Forms.Panel panel_UC_Statuses;
        private ControlLibrary.UCtrl_Statuses_Elm uCtrl_Statuses_Elm;
        private System.Windows.Forms.Panel panel_UC_Shortcuts;
        private ControlLibrary.UCtrl_Shortcuts_Elm uCtrl_Shortcuts_Elm;
        private ControlLibrary.UCtrl_Shortcut_Opt uCtrl_Shortcut_Opt;
        private System.Windows.Forms.CheckBox checkBox_Shortcuts_Image;
        private System.Windows.Forms.Panel panel_UC_Battery;
        private ControlLibrary.UCtrl_Battery_Elm uCtrl_Battery_Elm;
        private System.Windows.Forms.Panel panel_UC_Heart;
        private ControlLibrary.UCtrl_Heart_Elm uCtrl_Heart_Elm;
        private System.Windows.Forms.Panel panel_UC_Calories;
        private ControlLibrary.UCtrl_Calories_Elm uCtrl_Calories_Elm;
        private System.Windows.Forms.Panel panel_UC_PAI;
        private ControlLibrary.UCtrl_PAI_Elm uCtrl_PAI_Elm;
        private System.Windows.Forms.Panel panel_UC_Distance;
        private ControlLibrary.UCtrl_Distance_Elm uCtrl_Distance_Elm;
        private System.Windows.Forms.Panel panel_UC_Weather;
        private ControlLibrary.UCtrl_Weather_Elm uCtrl_Weather_Elm;
        private ControlLibrary.UCtrl_Text_Weather_Opt uCtrl_Text_Weather_Opt;
    }
}

