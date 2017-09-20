using System;
using System.Drawing;
using System.Windows.Forms;

namespace OrderWiseCalculator
{

    public enum Operators
    {
        Clear = 0,
        Plus = 10,
        Minus = 20,
        Multiply = 30,
        Divide = 40,
        Equals = 50
    }
    public partial class frmMain : Form
    {
        bool booClearScreen = false;
        MathsGenius genius = new MathsGenius();
        public frmMain()
        {
            InitializeComponent();
            
        }

        void genius_GeniusHasResults(object source, MathGeniusEventArgs args)
        {
            //MessageBox.Show(args.Value.ToString());
            genius.Number = args.Value;
            txtLCD.Text = genius.Number.ToString();
            
        }

        
        /// <summary>
        /// Setting up the screen
        /// </summary>
        private void PopulateScreen()
        {
            
            byte bytGapSize = 5;    //Gap between the buttons and the edges of the controls
            txtLCD.Width = frmMain.ActiveForm.Width - (2 * bytGapSize);
            txtLCD.Top = bytGapSize;
            txtLCD.Left = bytGapSize;
            this.Controls.Add(txtLCD);

            Int32 bytTopGap = 53 + (2 * bytGapSize);

            Int32 intButtonHeight = 53;// ((int)(frmMain.ActiveForm.Height / 4) - bytGapSize) - bytTopGap;
            Int32 intButtonWidth = (int)(frmMain.ActiveForm.Width / 4)-bytGapSize;

            for (byte rows = 0; rows < 4; rows++)
            {
                Int32 intY = ((rows * intButtonHeight) + (rows * bytGapSize)) + bytTopGap;
                for (byte cols = 0; cols < 4; cols++)
                {
                    Int32 intX = ((cols * intButtonWidth) + (cols * bytGapSize)) + bytGapSize;
                    Button button = new Button();
                    txtLCD.Font = new Font("Arial", 24, FontStyle.Bold);
                    button.Font = new Font("Arial", 24, FontStyle.Regular);
                    button.Click += button_Click;
                    button.Width = intButtonWidth;
                    button.Height = intButtonHeight;
                    if (rows == 0) 
                    {
                        switch (cols)
                        {
                            case 0:
                                {
                                    button.Text = "7";
                                    button.Tag = "Number";
                                    
                                }
                                break;
                            case 1:
                                {
                                    button.Text = "8";
                                    button.Tag = "Number";

                                }
                                break;
                            case 2:
                                {
                                    button.Text = "9";
                                    button.Tag = "Number";
                                }
                                break;
                            case 3:
                                {
                                    button.Text = "÷";
                                    button.Tag = Operators.Divide;
                                }
                                break;
                        
                            default:
                                break;
                        }
                        
                    }
                    if (rows == 1)
                    {
                        switch (cols)
                        {
                            case 0:
                                {
                                    button.Text = "4";
                                    button.Tag = "Number";
                                }
                                break;
                            case 1:
                                {
                                    button.Text = "5";
                                    button.Tag = "Number";
                                }
                                break;
                            case 2:
                                {
                                    button.Text = "6";
                                    button.Tag = "Number";
                                }
                                break;
                            case 3:
                                {
                                    button.Text = "×";
                                    button.Tag = Operators.Multiply;
                                }
                                break;

                            default:
                                break;
                        }

                    }
                    if (rows == 2)
                    {
                        switch (cols)
                        {
                            case 0:
                                {
                                    button.Text = "1";
                                    button.Tag = "Number";
                                }
                                break;
                            case 1:
                                {
                                    button.Text = "2";
                                    button.Tag = "Number";
                                }
                                break;
                            case 2:
                                {
                                    button.Text = "3";
                                    button.Tag = "Number";
                                }
                                break;
                            case 3:
                                {
                                    button.Text = "-";
                                    button.Tag = Operators.Minus;
                                }
                                break;

                            default:
                                break;
                        }

                    }
                    if (rows == 3)
                    {
                        switch (cols)
                        {
                            case 0:
                                {
                                    button.Text = "0";
                                    button.Tag = "Number";
                                }
                                break;
                            case 1:
                                {
                                    button.Text = "C";
                                    button.Tag = Operators.Clear;
                                }
                                break;
                            case 2:
                                {
                                    button.Text = "=";
                                    button.Tag = Operators.Equals;
                                }
                                break;
                            case 3:
                                {
                                    button.Text = "+";
                                    button.Tag = Operators.Plus;
                                }
                                break;

                            default:
                                break;
                        }

                    }
                    this.Controls.Add(button);
                    button.Location = new Point(intX,intY);

                }
            }
        }
        /// <summary>
        /// Some button was clicked.
        /// Now do something with what you've been given
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void button_Click(object sender, EventArgs e)
        {
            if(booClearScreen)
                txtLCD.Text = "";
            booClearScreen = false;
            Button btn = (Button)sender;
            string strText = btn.Text;
            string strTag = btn.Tag.ToString();
            if (strTag == "Number")
            {
                if (genius.Number == 0)
                    txtLCD.Text = "";
                txtLCD.Text += strText;
                genius.Number = Convert.ToDecimal(txtLCD.Text);
            }
            else
            {
                Operators operatorChosen = (Operators)btn.Tag;
                if (operatorChosen == Operators.Equals)
                    booClearScreen = true;
                genius.Operator = operatorChosen;
            }
            
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            genius.GeniusHasResults += genius_GeniusHasResults;
            genius.GeniusEncounteredErrors += genius_GeniusEncounteredErrors;
            PopulateScreen();
        }

        void genius_GeniusEncounteredErrors(object source, ErrorArgs args)
        {
            txtLCD.Text = "";
            MessageBox.Show(this,args.ErrorMessage,"Ooops!",MessageBoxButtons.OK,MessageBoxIcon.Error);
        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            MessageBox.Show(this, "This little calculator app was written as a demonstration only for Orderwise by Neil Vermeulen." + 
                Environment.NewLine + 
                Environment.NewLine + 
                "You can email me at NeilOCV@GMail.com" + 
                Environment.NewLine + 
                "or call me directly on 072 519 4799.", "For when you decide you want to hire me", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
