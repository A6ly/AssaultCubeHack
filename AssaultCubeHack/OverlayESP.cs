using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace AssaultCubeHack
{

    public struct POINTS
    {
        public int Left, Top, Right, Bottom;
    }

    public partial class OverlayESP : Form
    {

        public const int WS_EX_LAYERED = 0x80000;
        public const int WS_EX_TRANSPARENT = 0x20;

        Font font = new Font(new FontFamily("Arial"), 12, FontStyle.Regular, GraphicsUnit.Pixel);
        Brush brush = new SolidBrush(Color.Magenta);
        Pen pen = new Pen(new SolidBrush(Color.Magenta));

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowRect(IntPtr hwnd, out POINTS lpPoints);

        public string WindowName;
        POINTS gameWindow;

        bool started = false;

        public Memory pm;
        public GameManager gm;

        public OverlayESP(Memory pm, GameManager gm, string windowName)
        {
            this.pm = pm;
            this.gm = gm;
            this.WindowName = windowName;
            InitializeComponent();
            this.TransparencyKey = this.BackColor = Color.White;
            WindowSetup();
        }

        public void setStarted(bool started)
        {
            this.started = started;
            timer1.Start();
        }

        private void OverlayESP_Paint(object sender, PaintEventArgs e)
        {
            if (started)
            {
                Graphics g = e.Graphics;
                if (gm.espEntities != null)
                {
                    Point from = new Point((gameWindow.Right - (gameWindow.Left / 2)), gameWindow.Bottom);
                    foreach (KeyValuePair<LocatableEntity, LocatableEntity[]> entity in gm.espEntities)
                    {
                        for (int i = 0; i < entity.Value.Length; i++)
                        {
                            byte[] matrix = gm.viewMatrix;
                            if (matrix != null && entity.Value[i] != null)
                            {
                                float[] xyFoot = Calculations.WorldToScreen(matrix, entity.Value[i], gameWindow, true);
                                float[] xyHead = Calculations.WorldToScreen(matrix, entity.Value[i], gameWindow, false);
                                if (xyFoot != null && xyHead != null && entity.Value[i].valid())
                                {
                                        Point to = new Point(((int)(gameWindow.Left + xyFoot[0])), (int)(gameWindow.Top + xyFoot[1]));
                                        g.DrawLine(pen, from, to);

                                        float height = Math.Abs(xyHead[1] - xyFoot[1]); 
                                        float width = height / 2;
                                        g.DrawRectangle(pen, (gameWindow.Left + (xyHead[0] - width / 2)), (gameWindow.Top + (xyHead[1])), width, height);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void WindowSetup()
        {
            IntPtr handle = FindWindow(null, this.WindowName);
            if (handle != IntPtr.Zero)
            {
                GetWindowRect(handle, out gameWindow);
            }
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= WS_EX_LAYERED | WS_EX_TRANSPARENT;
                return cp;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Invalidate();
        }

    }
}
