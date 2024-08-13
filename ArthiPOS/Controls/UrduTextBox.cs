/*
 * Author: Muhammad Mobeen Movania
 * Last modified: 9 June 2007
 * 
 * This product is provided as is without any implied warranty
 * Use it at your own risk.
 * You may use this in a product provided that you keep this notice
 * and the Author tag above and also provide the author name in your
 * application somewhere ideally in the about box. 
 * If you feel you have improved it, please let me know at mobeen211@yahoo.com
 * so that others may benefit from it. 
 * */
using System;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ArthiPOS;

namespace ArthiPOS.Controls
{
	
    public class UrduTextBox : System.Windows.Forms.TextBox
	{
		//used to keep track of keystrokes handled by us
		private bool handled = false;
		
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public UrduTextBox()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			this.RightToLeft = RightToLeft.Yes;
            
            JoinEvents(true);

        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
		}
		#endregion

		#region Urdutextbox custom code
	/*	protected override void OnKeyPress(KeyPressEventArgs e)
		{		
			//Move the caret to the end of text				
			this.SelectionStart = this.Text.Length;

			e.Handled=handled;

            //We handle only the required keys checked in the key down event
            //the rest are passed to the parent
            if (!handled)
                if (IsNumeric == false)
                {
                    base.OnKeyPress(e);
                }
                else if (!handled && IsNumeric)
                {
                    if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                    {
                        this.Text = "";
                        this.SelectAll();

                    }
                    else
                    {
                        base.OnKeyPress(e);
                    }
                }
                else
                {
                    base.OnKeyPress(e);
                }
            


        }
   */
        
        /*
                protected override void OnKeyDown(KeyEventArgs e)
                {




                    if (!this._langenglish )
                    { 
                        //Set the handled flag only if we are handlign a keystroke
                        handled = (e.KeyCode== Keys.Space || e.KeyCode == Keys.Oemcomma || e.KeyCode == Keys.Decimal 
                            || e.KeyCode == Keys.OemQuestion || e.KeyCode == Keys.OemPipe 
                            || e.KeyCode == Keys.OemBackslash ||e.KeyCode == Keys.OemSemicolon 
                            ||e.KeyCode ==Keys.OemQuotes 	|| e.KeyCode ==	Keys.OemOpenBrackets 
                            || e.KeyCode == Keys.OemCloseBrackets ) ||
                        (e.KeyCode >= Keys.D0 && e.KeyCode<=Keys.D9) || (e.KeyCode>= Keys.A && e.KeyCode<= Keys.Z);
                        //if (this.SelectionLength == this.Text.Length && handled)
                        //{
                        //    this.Clear();
                        //}
                        //Get the text from our textbox and store it in a string builder
                        StringBuilder sb = new StringBuilder(this.Text);

                        //Append appropriate letter to our textbox based on the key pressed
                        switch (e.KeyCode)
                        {
                            case Keys.Control | Keys.X:
                                MessageBox.Show(this.SelectedText);
                                break;
                            case Keys.D0:
                                sb.Append("\u0660");
                                break;

                            case Keys.D1:
                                sb.Append("\u0661");
                                break;

                            case Keys.D2:
                                sb.Append("\u0662");
                                break;

                            case Keys.D3:
                                sb.Append("\u0663");
                                break;

                            case Keys.D4:
                                sb.Append("\u0664");
                                break;

                            case Keys.D5:
                                sb.Append("\u0665");
                                break;

                            case Keys.D6:
                                sb.Append("\u0666");
                                break;

                            case Keys.D7:
                                sb.Append("\u0667");
                                break;

                            case Keys.D8:
                                sb.Append("\u0668");
                                break;

                            case Keys.D9:
                                sb.Append("\u0669");
                                break;		

                            case Keys.Space:
                                sb.Append(" \u200c");							
                                break;

                            case Keys.A:
                                sb.Append(((e.Shift)?"\u0622":"\u0627"));					
                                break;

                            case Keys.B:
                                sb.Append(((e.Shift)?"\u0613":"\u0628"));
                                break;

                            case Keys.C:
                                sb.Append(((e.Shift)?"\u062b":"\u0686"));					
                                break;

                            case Keys.D:
                                sb.Append(((e.Shift)?"\u0688":"\u062f"));					
                                break;

                            case Keys.E:
                                sb.Append(((e.Shift)?"\u0610":"\u0639"));					
                                break;

                            case Keys.F:
                                sb.Append("\u0641");
                                break;

                            case Keys.G:
                                sb.Append(((e.Shift)?"\u063a":"\u06af"));
                                break;

                            case Keys.H:
                                sb.Append(((e.Shift)?"\u062d":"\u06be"));//0647 also
                                break;

                            case Keys.I:
                                sb.Append("\u06cc");//0649 also
                                break;

                            case Keys.J:
                                sb.Append(((e.Shift)?"\u0636":"\u062c"));					
                                break;

                            case Keys.K:
                                sb.Append(((e.Shift)?"\u062e":"\u0643"));
                                break;

                            case Keys.L:
                                sb.Append(((e.Shift)?"\u0612":"\u0644"));					
                                break;

                            case Keys.M:
                                sb.Append("\u0645");					
                                break;

                            case Keys.N:
                                sb.Append(((e.Shift)?"\u06ba":"\u0646"));					
                                break;

                            case Keys.O:					
                                sb.Append(((e.Shift)?"\u0629":"\u06c1"));
                                break;

                            case Keys.P:
                                sb.Append(((e.Shift)?"\u0645":"\u067e"));//paish
                                break;

                            case Keys.Q:
                                sb.Append("\u0642");
                                break;

                            case Keys.R:
                                sb.Append(((e.Shift)?"\u0691":"\u0631"));					
                                break;

                            case Keys.S:
                                sb.Append(((e.Shift)?"\u0635":"\u0633"));					
                                break;

                            case Keys.T:
                                sb.Append(((e.Shift)?"\u0679":"\u062a"));
                                break;

                            case Keys.U:
                                sb.Append("\u0621");
                                break;

                            case Keys.V:
                                sb.Append(((e.Shift)?"\u0638":"\u0637"));					
                                break;

                            case Keys.W:
                                sb.Append(((e.Shift)?"\u0635\u0644\u0649\u0020\u0627\u0644\u0644\u0647\u0020\u0639\u0644\u064a\u0647\u0020\u0648\u0633\u0644\u0645":"\u0648"));
                                break;

                            case Keys.X:
                                sb.Append(((e.Shift)?"\u0698":"\u0634"));					
                                break;

                            case Keys.Y:
                                sb.Append("\u06d2");
                                break;

                            case Keys.Z:
                                sb.Append(((e.Shift)?"\u0630":"\u0632"));
                                break;

                            case Keys.Decimal:
                                sb.Append("\u06d4");	
                                break;

                            case Keys.Oemcomma:
                                sb.Append("\u060c");
                                break;

                            case Keys.OemQuestion:
                                sb.Append("\u061f");					
                                break;

                            case Keys.OemPipe:
                                sb.Append("\u06d4");					
                                break;

                            case Keys.OemBackslash:
                                sb.Append("\u0602");					
                                break;

                            case Keys.OemSemicolon:
                                sb.Append("\u061b");					
                                break;

                            case Keys.OemQuotes:
                                sb.Append("\u0022");
                                break;

                            case Keys.OemOpenBrackets:					
                                sb.Append("\u007b");
                                break;

                            case Keys.OemCloseBrackets:					
                                sb.Append("\u007d");
                                break;

                        }		

                        //Set the text to our textbox from the string builder


                        this.Text = sb.ToString();
                    }
                }*/




        #region Control Keys,Events

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (!this._langenglish)
            {
                handled = (keyData == Keys.Space || keyData == Keys.Oemcomma || keyData == Keys.Decimal
                           || keyData == Keys.OemQuestion || keyData == Keys.OemPipe
                           || keyData == Keys.OemBackslash || keyData == Keys.OemSemicolon
                           || keyData == Keys.OemQuotes || keyData == Keys.OemOpenBrackets
                           || keyData == Keys.OemCloseBrackets) ||
                       (keyData >= Keys.D0 && keyData <= Keys.D9) || (keyData >= Keys.A && keyData <= Keys.Z);
                //if (this.SelectionLength == this.Text.Length && handled)
                //{
                //    this.Clear();
                //}
                //Get the text from our textbox and store it in a string builder
                StringBuilder sb = new StringBuilder(this.Text);

                //Append appropriate letter to our textbox based on the key pressed

                switch (keyData)
                {

                    case Keys.D0:
                        sb.Append("\u0660");
                        this.Text = sb.ToString(); this.SelectionStart = this.Text.Length; return true;

                    case Keys.D1:
                        sb.Append("\u0661");
                        this.Text = sb.ToString(); this.SelectionStart = this.Text.Length; return true;

                    case Keys.D2:
                        sb.Append("\u0662");
                        this.Text = sb.ToString(); this.SelectionStart = this.Text.Length; return true;

                    case Keys.D3:
                        sb.Append("\u0663");
                        this.Text = sb.ToString(); this.SelectionStart = this.Text.Length; return true;

                    case Keys.D4:
                        sb.Append("\u0664");
                        this.Text = sb.ToString(); this.SelectionStart = this.Text.Length; return true;

                    case Keys.D5:
                        sb.Append("\u0665");
                        this.Text = sb.ToString(); this.SelectionStart = this.Text.Length; return true;

                    case Keys.D6:
                        sb.Append("\u0666");
                        this.Text = sb.ToString(); this.SelectionStart = this.Text.Length; return true;

                    case Keys.D7:
                        sb.Append("\u0667");
                        this.Text = sb.ToString(); this.SelectionStart = this.Text.Length; return true;

                    case Keys.D8:
                        sb.Append("\u0668");
                        this.Text = sb.ToString(); this.SelectionStart = this.Text.Length; return true;

                    case Keys.D9:
                        sb.Append("\u0669");
                        this.Text = sb.ToString(); this.SelectionStart = this.Text.Length; return true;

                    case Keys.Space:
                        sb.Append(" \u200c");
                        this.Text = sb.ToString(); this.SelectionStart = this.Text.Length; return true;

                    case Keys.A:
                        sb.Append(((keyData == Keys.Shift) ? "\u0622" : "\u0627"));
                        this.Text = sb.ToString(); this.SelectionStart = this.Text.Length; return true;

                    case Keys.B:
                        sb.Append(((keyData == Keys.Shift) ? "\u0613" : "\u0628"));
                        this.Text = sb.ToString(); this.SelectionStart = this.Text.Length; return true;

                    case Keys.C:
                        sb.Append(((keyData == Keys.Shift) ? "\u062b" : "\u0686"));
                        this.Text = sb.ToString(); this.SelectionStart = this.Text.Length; return true;

                    case Keys.D:
                        sb.Append(((keyData == Keys.Shift) ? "\u0688" : "\u062f"));
                        this.Text = sb.ToString(); this.SelectionStart = this.Text.Length; return true;

                    case Keys.E:
                        sb.Append(((keyData == Keys.Shift) ? "\u0610" : "\u0639"));
                        this.Text = sb.ToString(); this.SelectionStart = this.Text.Length; return true;

                    case Keys.F:
                        sb.Append("\u0641");
                        this.Text = sb.ToString(); this.SelectionStart = this.Text.Length; return true;

                    case Keys.G:
                        sb.Append(((keyData == Keys.Shift) ? "\u063a" : "\u06af"));
                        this.Text = sb.ToString(); this.SelectionStart = this.Text.Length; return true;

                    case Keys.H:
                        sb.Append(((keyData == Keys.Shift) ? "\u062d" : "\u06be"));//0647 also
                        this.Text = sb.ToString(); this.SelectionStart = this.Text.Length; return true;

                    case Keys.I:
                        sb.Append("\u06cc");//0649 also
                        this.Text = sb.ToString(); this.SelectionStart = this.Text.Length; return true;

                    case Keys.J:
                        sb.Append(((keyData == Keys.Shift) ? "\u0636" : "\u062c"));
                        this.Text = sb.ToString(); this.SelectionStart = this.Text.Length; return true;

                    case Keys.K:
                        sb.Append(((keyData == Keys.Shift) ? "\u062e" : "\u0643"));
                        this.Text = sb.ToString(); this.SelectionStart = this.Text.Length; return true;

                    case Keys.L:
                        sb.Append(((keyData == Keys.Shift) ? "\u0612" : "\u0644"));
                        this.Text = sb.ToString(); this.SelectionStart = this.Text.Length; return true;

                    case Keys.M:
                        sb.Append("\u0645");
                        this.Text = sb.ToString(); this.SelectionStart = this.Text.Length; return true;

                    case Keys.N:
                        sb.Append(((keyData == Keys.Shift) ? "\u06ba" : "\u0646"));
                        this.Text = sb.ToString(); this.SelectionStart = this.Text.Length; return true;

                    case Keys.O:
                        sb.Append(((keyData == Keys.Shift) ? "\u0629" : "\u06c1"));
                        this.Text = sb.ToString(); this.SelectionStart = this.Text.Length; return true;

                    case Keys.P:
                        sb.Append(((keyData == Keys.Shift) ? "\u0645" : "\u067e"));//paish
                        this.Text = sb.ToString(); this.SelectionStart = this.Text.Length; return true;

                    case Keys.Q:
                        sb.Append("\u0642");
                        this.Text = sb.ToString(); this.SelectionStart = this.Text.Length; return true;

                    case Keys.R:
                        sb.Append(((keyData == Keys.Shift) ? "\u0691" : "\u0631"));
                        this.Text = sb.ToString(); this.SelectionStart = this.Text.Length; return true;

                    case Keys.S:
                        sb.Append(((keyData == Keys.Shift) ? "\u0635" : "\u0633"));
                        this.Text = sb.ToString(); this.SelectionStart = this.Text.Length; return true;

                    case Keys.T:
                        sb.Append(((keyData == Keys.Shift) ? "\u0679" : "\u062a"));
                        this.Text = sb.ToString(); this.SelectionStart = this.Text.Length; return true;

                    case Keys.U:
                        sb.Append("\u0621");
                        this.Text = sb.ToString(); this.SelectionStart = this.Text.Length; return true;

                    case Keys.V:
                        sb.Append(((keyData == Keys.Shift) ? "\u0638" : "\u0637"));
                        this.Text = sb.ToString(); this.SelectionStart = this.Text.Length; return true;

                    case Keys.W:
                        sb.Append(((keyData == Keys.Shift) ? "\u0635\u0644\u0649\u0020\u0627\u0644\u0644\u0647\u0020\u0639\u0644\u064a\u0647\u0020\u0648\u0633\u0644\u0645" : "\u0648"));
                        this.Text = sb.ToString(); this.SelectionStart = this.Text.Length; return true;

                    case Keys.X:
                        sb.Append(((keyData == Keys.Shift) ? "\u0698" : "\u0634"));
                        this.Text = sb.ToString(); this.SelectionStart = this.Text.Length; return true;

                    case Keys.Y:
                        sb.Append("\u06d2");
                        this.Text = sb.ToString(); this.SelectionStart = this.Text.Length; return true;

                    case Keys.Z:
                        sb.Append(((keyData == Keys.Shift) ? "\u0630" : "\u0632"));
                        this.Text = sb.ToString(); this.SelectionStart = this.Text.Length; return true;

                        #region Shift Alphabets

                    case Keys.Shift | Keys.A:
                        sb.Append(((true) ? "\u0622" : "\u0627"));
                        this.Text = sb.ToString(); this.SelectionStart = this.Text.Length; return true;

                    case Keys.Shift | Keys.B:
                        sb.Append(((true) ? "\u0613" : "\u0628"));
                        this.Text = sb.ToString(); this.SelectionStart = this.Text.Length; return true;

                    case Keys.Shift | Keys.C:
                        sb.Append(((true) ? "\u062b" : "\u0686"));
                        this.Text = sb.ToString(); this.SelectionStart = this.Text.Length; return true;

                    case Keys.Shift | Keys.D:
                        sb.Append(((true) ? "\u0688" : "\u062f"));
                        this.Text = sb.ToString(); this.SelectionStart = this.Text.Length; return true;

                    case Keys.Shift | Keys.E:
                        sb.Append(((true) ? "\u0610" : "\u0639"));
                        this.Text = sb.ToString(); this.SelectionStart = this.Text.Length; return true;

                    case Keys.Shift | Keys.F:
                        sb.Append("\u0641");
                        this.Text = sb.ToString(); this.SelectionStart = this.Text.Length; return true;

                    case Keys.Shift | Keys.G:
                        sb.Append(((true) ? "\u063a" : "\u06af"));
                        this.Text = sb.ToString(); this.SelectionStart = this.Text.Length; return true;

                    case Keys.Shift | Keys.H:
                        sb.Append(((true) ? "\u062d" : "\u06be"));//0647 also
                        this.Text = sb.ToString(); this.SelectionStart = this.Text.Length; return true;

                    case Keys.Shift | Keys.I:
                        sb.Append("\u06cc");//0649 also
                        this.Text = sb.ToString(); this.SelectionStart = this.Text.Length; return true;

                    case Keys.Shift | Keys.J:
                        sb.Append(((true) ? "\u0636" : "\u062c"));
                        this.Text = sb.ToString(); this.SelectionStart = this.Text.Length; return true;

                    case Keys.Shift | Keys.K:
                        sb.Append(((true) ? "\u062e" : "\u0643"));
                        this.Text = sb.ToString(); this.SelectionStart = this.Text.Length; return true;

                    case Keys.Shift | Keys.L:
                        sb.Append(((true) ? "\u0612" : "\u0644"));
                        this.Text = sb.ToString(); this.SelectionStart = this.Text.Length; return true;

                    case Keys.Shift | Keys.M:
                        sb.Append("\u0645");
                        this.Text = sb.ToString(); this.SelectionStart = this.Text.Length; return true;

                    case Keys.Shift | Keys.N:
                        sb.Append(((true) ? "\u06ba" : "\u0646"));
                        this.Text = sb.ToString(); this.SelectionStart = this.Text.Length; return true;

                    case Keys.Shift | Keys.O:
                        sb.Append(((true) ? "\u0629" : "\u06c1"));
                        this.Text = sb.ToString(); this.SelectionStart = this.Text.Length; return true;

                    case Keys.Shift | Keys.P:
                        sb.Append(((true) ? "\u0645" : "\u067e"));//paish
                        this.Text = sb.ToString(); this.SelectionStart = this.Text.Length; return true;

                    case Keys.Shift | Keys.Q:
                        sb.Append("\u0642");
                        this.Text = sb.ToString(); this.SelectionStart = this.Text.Length; return true;

                    case Keys.Shift | Keys.R:
                        sb.Append(((true) ? "\u0691" : "\u0631"));
                        this.Text = sb.ToString(); this.SelectionStart = this.Text.Length; return true;

                    case Keys.Shift | Keys.S:
                        sb.Append(((true) ? "\u0635" : "\u0633"));
                        this.Text = sb.ToString(); this.SelectionStart = this.Text.Length; return true;

                    case Keys.Shift | Keys.T:
                        sb.Append(((true) ? "\u0679" : "\u062a"));
                        this.Text = sb.ToString(); this.SelectionStart = this.Text.Length; return true;

                    case Keys.Shift | Keys.U:
                        sb.Append("\u0621");
                        this.Text = sb.ToString(); this.SelectionStart = this.Text.Length; return true;

                    case Keys.Shift | Keys.V:
                        sb.Append(((true) ? "\u0638" : "\u0637"));
                        this.Text = sb.ToString(); this.SelectionStart = this.Text.Length; return true;

                    case Keys.Shift | Keys.W:
                        sb.Append(((true) ? "\u0635\u0644\u0649\u0020\u0627\u0644\u0644\u0647\u0020\u0639\u0644\u064a\u0647\u0020\u0648\u0633\u0644\u0645" : "\u0648"));
                        this.Text = sb.ToString(); this.SelectionStart = this.Text.Length; return true;

                    case Keys.Shift | Keys.X:
                        sb.Append(((true) ? "\u0698" : "\u0634"));
                        this.Text = sb.ToString(); this.SelectionStart = this.Text.Length; return true;

                    case Keys.Shift | Keys.Y:
                        sb.Append("\u06d2");
                        this.Text = sb.ToString(); this.SelectionStart = this.Text.Length; return true;

                    case Keys.Shift | Keys.Z:
                        sb.Append(((true) ? "\u0630" : "\u0632"));
                        this.Text = sb.ToString(); this.SelectionStart = this.Text.Length; return true;

                    #endregion



                    case Keys.Decimal:
                        sb.Append("\u06d4");
                        this.Text = sb.ToString(); this.SelectionStart = this.Text.Length; return true;

                    case Keys.Oemcomma:
                        sb.Append("\u060c");
                        this.Text = sb.ToString(); this.SelectionStart = this.Text.Length; return true;

                    case Keys.OemQuestion:
                        sb.Append("\u061f");
                        this.Text = sb.ToString(); this.SelectionStart = this.Text.Length; return true;

                    case Keys.OemPipe:
                        sb.Append("\u06d4");
                        this.Text = sb.ToString(); this.SelectionStart = this.Text.Length; return true;

                    case Keys.OemBackslash:
                        sb.Append("\u0602");
                        this.Text = sb.ToString(); this.SelectionStart = this.Text.Length; return true;

                    case Keys.OemSemicolon:
                        sb.Append("\u061b");
                        this.Text = sb.ToString(); this.SelectionStart = this.Text.Length; return true;

                    case Keys.OemQuotes:
                        sb.Append("\u0022");
                        this.Text = sb.ToString(); this.SelectionStart = this.Text.Length; return true;

                    case Keys.OemOpenBrackets:
                        sb.Append("\u007b");
                        this.Text = sb.ToString();
                        this.SelectionStart = this.Text.Length; return true;

                    case Keys.OemCloseBrackets:
                        sb.Append("\u007d");
                        this.Text = sb.ToString();
                        this.SelectionStart = this.Text.Length; return true;
                }

            }
            return base.ProcessCmdKey(ref msg, keyData);
        }


        #endregion
        #endregion


        #region Watermark
        private Font oldFont = null;
        private Boolean waterMarkTextEnabled = false;

        #region Attributes 
        private Color _waterMarkColor = Color.Gray;
        public Color WaterMarkColor
        {
            get { return _waterMarkColor; }
            set
            {
                _waterMarkColor = value; Invalidate();/*thanks to Bernhard Elbl
                                                              for Invalidate()*/
            }
        }

        private string _waterMarkText = "Water Mark";
        public string WaterMarkText
        {
            get { return _waterMarkText; }
            set { _waterMarkText = value; Invalidate(); }
        }
        private bool _langenglish = false;

        public bool LangEnglish
        {
            get { return _langenglish; }
            set { _langenglish = value; Invalidate(); }
        }
        private bool _isnumeric = false;

        public bool IsNumeric
        {
            get { return _isnumeric; }
            set { _isnumeric = value; Invalidate(); }
        }
        #endregion

        //Default constructor


        //Override OnCreateControl ... thanks to  "lpgray .. codeproject guy"
        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            WaterMark_Toggel(null, null);
        }

        //Override OnPaint
        protected override void OnPaint(PaintEventArgs args)
        {
            // Use the same font that was defined in base class
            System.Drawing.Font drawFont = new System.Drawing.Font(Font.FontFamily,
                Font.Size, Font.Style, Font.Unit);
            //Create new brush with gray color or 
            SolidBrush drawBrush = new SolidBrush(WaterMarkColor);//use Water mark color
            //Draw Text or WaterMark
            args.Graphics.DrawString((waterMarkTextEnabled ? WaterMarkText : Text),
                drawFont, drawBrush, new PointF(0.0F, 0.0F));
            base.OnPaint(args);
        }

        private void JoinEvents(Boolean join)
        {
            if (join)
            {
                this.TextChanged += new System.EventHandler(this.WaterMark_Toggel);
                this.LostFocus += new System.EventHandler(this.WaterMark_Toggel);
                this.FontChanged += new System.EventHandler(this.WaterMark_FontChanged);
                //No one of the above events will start immeddiatlly 
                //TextBox control still in constructing, so,
                //Font object (for example) couldn't be catched from within
                //WaterMark_Toggle
                //So, call WaterMark_Toggel through OnCreateControl after TextBox
                //is totally created
                //No doupt, it will be only one time call

                //Old solution uses Timer.Tick event to check Create property
            }
        }

        private void WaterMark_Toggel(object sender, EventArgs args)
        {
            if (this.Text.Length <= 0)
                EnableWaterMark();
            else
                DisbaleWaterMark();
        }

        private void EnableWaterMark()
        {
            //Save current font until returning the UserPaint style to false (NOTE:
            //It is a try and error advice)
            oldFont = new System.Drawing.Font(Font.FontFamily, Font.Size, Font.Style,
               Font.Unit);
            //Enable OnPaint event handler
            this.SetStyle(ControlStyles.UserPaint, true);
            this.waterMarkTextEnabled = true;
            //Triger OnPaint immediatly
            Refresh();
        }

        private void DisbaleWaterMark()
        {
            //Disbale OnPaint event handler
            this.waterMarkTextEnabled = false;
            this.SetStyle(ControlStyles.UserPaint, false);
            //Return back oldFont if existed
            if (oldFont != null)
                this.Font = new System.Drawing.Font(oldFont.FontFamily, oldFont.Size,
                    oldFont.Style, oldFont.Unit);
        }

        private void WaterMark_FontChanged(object sender, EventArgs args)
        {
            if (waterMarkTextEnabled)
            {
                oldFont = new System.Drawing.Font(Font.FontFamily, Font.Size, Font.Style,
                    Font.Unit);
                Refresh();
            }
        }
        #endregion


    }
}
