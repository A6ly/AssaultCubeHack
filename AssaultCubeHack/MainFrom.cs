using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
using System.Windows.Forms;
using Memory;

namespace AssaultCubeHack
{
    public partial class MainFrom : Form
    {

        public int VK_MOUSERIGHT = 0x02;
        GameManager gm;
        OverlayESP overlayesp;
        Memory pm;
        Process ac;
        int gameBase;

        [DllImport("user32.dll")]
        public static extern short GetKeyState(int nVirtKey);

        public MainFrom()
        {
            InitializeComponent();
        }

        [DllImport("user32.dll")]
        public static extern short GetAsyncKeyState(uint vk);
        private const uint VK_LBUTTON = 0x00000002;

        public struct Player
        {
            public float x, y, z;
            public int teamnum;
        }

        public struct Entity
        {
            public float distance, x, y, z;
            public int hp, teamnum;
        }

        int PID, total, ProximateEnemy;
        bool isopened = false;
        bool aimbot = false;
        float[] Distance = new float[31];
        float[] angle = { 0, 0 };

        Mem mem = new Mem();
        Player player;
        Entity[] entities = new Entity[31];

        static public float GetDistance(Entity enemy, Player player)
        {
            return Convert.ToSingle(Math.Sqrt(Math.Pow(enemy.x - player.x, 2) + Math.Pow(enemy.y - player.y, 2) + Math.Pow(enemy.z - player.z, 2)));
        }

        static public int GetProximateEnemy(float[] List, int total)
        {
            int ProximateEnemyNum = 0;
            float ProximateDistance = List[0];

            for (int i = 1; i < total; i++)
            {
                if (List[i] < ProximateDistance)
                {
                    ProximateDistance = List[i];
                    ProximateEnemyNum = i;
                }
            }
            return ProximateEnemyNum;
        }

        static public float[] GetAngle(Entity enemy, Player player)
        {
            float[] degree = { 0, 0 };

            if (player.y > enemy.y && player.x < enemy.x)
            {
                degree[0] = (float)(Math.Atan((player.y - enemy.y) / (enemy.x - player.x)) * 180 / Math.PI); //degree:= atan((enemyy % closest % -myy) / (enemyx % closest % -myx)) * 57.3
                degree[0] = 90 - degree[0];
            }
            if (player.y > enemy.y && player.x > enemy.x)
            {
                degree[0] = (float)(Math.Atan((player.y - enemy.y) / (player.x - enemy.x)) * 180 / Math.PI);
                degree[0] += 270;
            }
            if (player.y < enemy.y && player.x < enemy.x)
            {
                degree[0] = (float)(Math.Atan((enemy.y - player.y) / (enemy.x - player.x)) * 180 / Math.PI);
                degree[0] += 90;
            }
            if (player.y < enemy.y && player.x > enemy.x)
            {
                degree[0] = (float)(Math.Atan((enemy.y - player.y) / (player.x - enemy.x)) * 180 / Math.PI);
                degree[0] = 270 - degree[0];
            }
            if (player.z > enemy.z)
            {
                degree[1] = (float)(-1 * Math.Asin((player.z - enemy.z) / enemy.distance) * 180 / Math.PI);
            }
            else if (player.z < enemy.z)
            {
                degree[1] = (float)(1 * Math.Asin((enemy.z - player.z) / enemy.distance) * 180 / Math.PI);
            }
            return degree;
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {

            while (true)
            {
                PID = mem.getProcIDFromName("ac_client");

                if (PID != 0)
                    break;
            }

            isopened = mem.OpenProcess(PID);

            if (isopened)
            {
                try
                {

                    if (aimbot)
                    {
                        total = mem.readInt("ac_client.exe+0x110D98");

                        player.x = mem.readFloat("ac_client.exe+0011E20C,0x34");
                        player.y = mem.readFloat("ac_client.exe+0x0011E20C,0x38");
                        player.z = mem.readFloat("ac_client.exe+0x0011E20C,0x3C");
                        player.teamnum = mem.readInt("ac_client.exe+0x0011E20C,0x32C");

                        for (int i = 0; i < total; i++)
                        {
                            entities[i].x = mem.readFloat("ac_client.exe+0x110D90," + (i * 4).ToString("x2") + ",0x34");
                            entities[i].y = mem.readFloat("ac_client.exe+0x110D90," + (i * 4).ToString("x2") + ",0x38");
                            entities[i].z = mem.readFloat("ac_client.exe+0x110D90," + (i * 4).ToString("x2") + ",0x3C");
                            entities[i].hp = mem.readInt("ac_client.exe+0x110D90," + (i * 4).ToString("x2") + ",0xF8");
                            entities[i].teamnum = mem.readInt("ac_client.exe+0x110D90," + (i * 4).ToString("x2") + ",0x32C");

                            if (entities[i].hp > 0 && entities[i].teamnum != player.teamnum)
                                Distance[i] = GetDistance(entities[i], player);

                            else
                                Distance[i] = float.MaxValue;

                            entities[i].distance = Distance[i];
                        }

                        ProximateEnemy = GetProximateEnemy(Distance, total);
                        angle = GetAngle(entities[ProximateEnemy], player);

                        if (GetAsyncKeyState(VK_LBUTTON) != 0 && Distance[ProximateEnemy] != float.MaxValue)
                        {
                            mem.writeMemory("ac_client.exe+0x109B74,0x40", "float", angle[0].ToString());
                            mem.writeMemory("ac_client.exe+0x109B74,0x44", "float", angle[1].ToString());
                        }
                    }
                }

                catch { }
            }
        }

        private void AimbotCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if(AimbotCheckBox.Checked == true)
            {
                aimbot = true;
            }

            else
            {
                aimbot = false;
            }
        }

        private void ESPCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (ESPCheckBox.Checked == true)
            {
                ac = Process.GetProcessesByName("ac_client")[0];
                gameBase = ac.MainModule.BaseAddress.ToInt32();

                pm = new Memory();
                pm.ReadProcess = ac;
                pm.OpenProcess();

                gm = new GameManager(gameBase, pm);

                overlayesp = new OverlayESP(pm, gm, "AssaultCube");
                overlayesp.Show();
                gm.startPlayerThread();
                overlayesp.setStarted(true);
            }

            else
            {
                overlayesp.Hide();
            }
        }
    }
}
