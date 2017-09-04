using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vendespil
{
    public partial class Form1 : Form
    {
        Label førsteTryk = null;
        Label andetTryk = null;
        Random tilfældig = new Random(); // Opretter en ny instance for en tilfældighedsgenerator. 
        List<string> ikoner = new List<string>() /* Opretter en ny instance for en liste, som skal indeholde ikonerne.
        Ikonerne er placeret som par i listen.
             */
        {

            "!", "!", "M", "M", "h", "h", "a", "a",
            "c", "c", "l", "l", "d", "d", "k", "k"

        };
        public Form1()
        {
            InitializeComponent();
            TilføjIkonerTilFelter();
        }

        /// <summary>
        /// Den private metode forneden bliver brugt til at tilføje ikonerne fra ikoner listen til felterne. Ikonerne bliver
        /// tilfældigt valgt via. tilfældighedsgeneratoren. 
        /// </summary>
        private void TilføjIkonerTilFelter()
        {
            foreach (Control kontrol in tableLayoutPanel1.Controls)
            {
                Label ikonFelt = kontrol as Label;
                if (ikonFelt != null)
                {
                    int tilfældigtNummer = tilfældig.Next(ikoner.Count);
                    ikonFelt.Text = ikoner[tilfældigtNummer];

                    ikonFelt.ForeColor = ikonFelt.BackColor;
                    ikoner.RemoveAt(tilfældigtNummer);
                }
            }
        }

        /// <summary>
        /// Den private metode forneden bliver brugt til at ændre farven på det felt, som bliver trykket på til hvid. 
        /// Den kalder derudover metoden tjekForEnVinder, for at afgøre om spilleren har vundet. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void felt_klik(object sender, EventArgs e)
        {
            if (timer1.Enabled == true)
                return;

            Label klikketFelt = sender as Label;
            if (klikketFelt != null)

            {
                if (klikketFelt.ForeColor == Color.White)
                    return;

               
                if (førsteTryk==null)
                {
                    førsteTryk = klikketFelt;
                    førsteTryk.ForeColor = Color.White;
                    return;
                   
                }
                andetTryk = klikketFelt;
                andetTryk.ForeColor = Color.White;

                tjekForEnVinder();

                if (førsteTryk.Text == andetTryk.Text)

                {
                    førsteTryk = null;
                    andetTryk = null;
                    return;
                    
                  
                }
           

                timer1.Start();
            }

        }
        /// <summary>
        /// Den private metode forneden bliver brugt til stoppe timeren, for derefter at ændre farverne på det felt, som man har trykket
        /// på. Derefter nulstillerne vi variablerne, så metoden er klar til næste tryk. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tik(object sender, EventArgs e)
        {
            timer1.Stop();
            førsteTryk.ForeColor = førsteTryk.BackColor;
            andetTryk.ForeColor = andetTryk.BackColor;
            førsteTryk = null;
            andetTryk = null;
        }

        /// <summary>
        /// Den private metode forneden bliver brugt til at tjekke, om spilleren har vundet spillet. 
        /// </summary>
        private void tjekForEnVinder()
        {
            foreach (Control kontrol in tableLayoutPanel1.Controls)
            {
                Label ikonLabel = kontrol as Label;

                if (ikonLabel != null)
                {
                    if (ikonLabel.ForeColor == ikonLabel.BackColor)
                        return;
                }
            }
            MessageBox.Show("Du har vundet spillet, ved at finde alle de rigtige felter.", "Stort tillykke med det!");
            Close();
        }
    }
}
