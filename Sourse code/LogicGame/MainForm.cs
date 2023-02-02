using System;
using System.Windows.Forms;
using System.Drawing;

namespace LogicGame
{

    internal class MainForm : Form
    {
        static void Main(string[] args) => Application.Run(new MainForm());

        private const int BUTTON_COUNT = 16;

        private readonly int SQUARE_ROOT = Convert.ToInt32(Math.Sqrt(BUTTON_COUNT));
        private readonly int PART_COUNT = (Convert.ToInt32(Math.Sqrt(BUTTON_COUNT)) * 2) + 1;

        private Button[] btn = new Button[BUTTON_COUNT];
        private Label lbl;

        public MainForm()
        {
            // styling form
            this.Left = 0;
            this.Top = 0;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            
            this.Text = "Logic Game";
            
            for (int i = 0; i < BUTTON_COUNT; i++)
            {
                // styling btn
                btn[i] = new Button();
                btn[i].Width = this.ClientSize.Width / PART_COUNT;
                btn[i].Height = this.ClientSize.Height / PART_COUNT;
                btn[i].Left = this.ClientSize.Width / PART_COUNT + 2 * (i % SQUARE_ROOT) * this.ClientSize.Width / PART_COUNT;
                btn[i].Top = this.ClientSize.Height / PART_COUNT + 2 * (i / SQUARE_ROOT) * this.ClientSize.Height / PART_COUNT;
                btn[i].BackColor = Color.Red;
                btn[i].Text = Convert.ToString(1 + i);
                
                // click event
                btn[i].Click += new EventHandler(btnClick);
                btn[i].Tag = 0;
                
                // adding into form
                this.Controls.Add(btn[i]);
            }

            // Creating label
            lbl = new Label();
            lbl.Left = (this.ClientSize.Width / PART_COUNT + 2 * (0 % SQUARE_ROOT) * this.ClientSize.Width / PART_COUNT);
            lbl.Top = (this.ClientSize.Height / PART_COUNT + 2 * (0 / SQUARE_ROOT) * this.ClientSize.Height / PART_COUNT) / 3;
            lbl.Width = this.ClientSize.Width;
            lbl.Text = "All buttons must be green!";
            // Adding into form
            this.Controls.Add(lbl);

        }

        private void btnClick(object sender, EventArgs e)
        {
            int i = Convert.ToInt32(((Button)sender).Text) - 1;

            // pressed
            ChangeButtonState(i);
            // right
            ChangeButtonState((i / SQUARE_ROOT) * SQUARE_ROOT + (i + 1) % SQUARE_ROOT);
            // left
            ChangeButtonState((i / SQUARE_ROOT) * SQUARE_ROOT + (i + SQUARE_ROOT - 1) % SQUARE_ROOT);
            // down
            ChangeButtonState((i + SQUARE_ROOT) % BUTTON_COUNT);
            // up
            ChangeButtonState((i - SQUARE_ROOT + BUTTON_COUNT) % BUTTON_COUNT);

            if (CheckForWin()) WonTheGame();
        }
        private void WonTheGame()
        {
            lbl.Text = "Congratulations!";
            for (int i = 0; i < BUTTON_COUNT; i++) btn[i].Click -= new EventHandler(btnClick);
        }
        private bool CheckForWin()
        {
            for (int i = 0; i < BUTTON_COUNT; i++) if (Convert.ToInt32(btn[i].Tag) == 0) return false;
            return true;
        }

        private void ChangeButtonState(int i)
        {
            if (Convert.ToInt32(btn[i].Tag) == 0)
            {
                btn[i].Tag = 1;
                btn[i].BackColor = Color.SeaGreen;
                return;
            }
            btn[i].Tag = 0;
            btn[i].BackColor = Color.Red;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Name = "MainForm";
            this.ResumeLayout(false);

        }
    }
 

}
