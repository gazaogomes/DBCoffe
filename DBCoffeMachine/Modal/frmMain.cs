using System;
using System.Windows.Forms;

namespace DBCoffeMachine.Modal
{
    public partial class frmMain : MaterialSkin.Controls.MaterialForm
    {
        double totalCoins = 0;
        double totalCommands = 0;
        public frmMain()
        {
            InitializeComponent();

        }

        #region Methods
        private void detalheTroco()
        {
            int real1 = 0, cent50 = 0, cent25 = 0, cent1 = 0;
            while (totalCoins > 0)
            {
                switch (totalCoins)
                {
                    case > 1.00: real1++; totalCoins -= 1.00; break;
                    case > 0.50: cent50++; totalCoins -= 0.50; break;
                    case > 0.25: cent25++; totalCoins -= 0.25; break;
                    default: cent1++; totalCoins -= 0.01; break;
                }
            }
            txtTrocoDetalhado.Text = "Moedas de 1 real: " + real1 + " moedas\n" +
                "Moedas de 50 centavos: " + cent50 + " moedas\n" +
                "Moedas de 25 centavos: " + cent25 + " moedas\n" +
                "Moedas de 1 centavo: " + cent1 + " moedas";
        }
        private void detalhePedido()
        {
            int qtd1 = 0, qtd2 = 0, qtd3 = 0;
            if (cbItem1.Checked == true)
            {
                qtd1 += Convert.ToInt32(txtQtdItem1.Text);
            }
            if (cbItem2.Checked == true)
            {
                qtd2 += Convert.ToInt32(txtQtdItem2.Text);
            }
            if (cbItem3.Checked == true)
            {
                qtd3 += Convert.ToInt32(txtQtdItem3.Text);
            }
            txtResume.Text = "Cappuccino x" + qtd1 + "\n" + "Mocha x" + qtd2 + "\n" + "Com leite x" + qtd3;


        }
        private void resetAll()
        {
            pnlMenu.Visible = false;
            gbCoins.Visible = false;
            gbFinish.Visible = false;

            totalCoins = 0;
            totalCommands = 0;

            txtQtdItem1.Text = "";
            txtQtdItem2.Text = "";
            txtQtdItem3.Text = "";

            cbItem1.Checked = false;
            cbItem2.Checked = false;
            cbItem3.Checked = false;

            txtResume.Text = "";
            txtTroco.Text = "";
            txtTrocoDetalhado.Text = "";
            txtValorTotal.Text = "";
            return;
        }
        private void AtivaQtd()
        {
            if (cbItem1.Checked == true)
            {
                lblQtd.Visible = true;
                txtQtdItem1.Visible = true;
            }
            else
            {
                lblQtd.Visible = false;
                txtQtdItem1.Visible = false;
                txtQtdItem1.Text = "";
            }
            if (cbItem2.Checked == true)
            {
                lblQtd.Visible = true;
                txtQtdItem2.Visible = true;
            }
            else
            {
                lblQtd.Visible = false;
                txtQtdItem2.Visible = false;
                txtQtdItem2.Text = "";
            }
            if (cbItem3.Checked == true)
            {
                lblQtd.Visible = true;
                txtQtdItem3.Visible = true;
            }
            else
            {
                lblQtd.Visible = false;
                txtQtdItem3.Visible = false;
                txtQtdItem3.Text = "";
            }
        }

        private void tratandoCheckBox()
        {
            //tratando os checkBoxs
            if (cbItem1.Checked)
            {
                if (txtQtdItem1.Text != String.Empty)
                    totalCommands += 3.50 * Convert.ToInt32(txtQtdItem1.Text);
                else
                {
                    MessageBox.Show("Insira a quantidade do Capuccino.");
                    return;
                }
            }
            if (cbItem2.Checked)
            {
                if (txtQtdItem2.Text != String.Empty)
                    totalCommands += 4.00 * Convert.ToInt32(txtQtdItem2.Text);
                else
                {
                    MessageBox.Show("Insira a quantidade do Mocha.");
                    return;
                }
            }
            if (cbItem3.Checked)
            {
                if (txtQtdItem3.Text != String.Empty)
                    totalCommands += 3.00 * Convert.ToInt32(txtQtdItem3.Text);
                else
                {
                    MessageBox.Show("Insira a quantidade do Café com Leite.");
                    return;
                }
            }
        }

        private void calcGeneral() 
        {
            if (totalCoins < totalCommands)
            {
                MessageBox.Show("O valor inserido é menor que o valor total do pedido.");
            }
            else
            {
                gbFinish.Visible = true;
                totalCoins = totalCoins - totalCommands;
                txtTroco.Text = Convert.ToString(totalCoins);
                detalheTroco();
                detalhePedido();
            }
        }

        private void ErrorInsertCoins()
        {
            MessageBox.Show("Oops! Problema na máquina, não estamos recebendo esse tipo de moeda.");
            return;
        }

        private void validacao()
        {
            if(gbCoins.Visible == false)
            {
                MessageBox.Show("Por favor, insira as moedas e selecione o pedido no 'Cardapio'.");
            }
        }

        #endregion

        #region Events

        private void btnInsertCoins_Click(object sender, EventArgs e)
        {
            gbCoins.Visible = true;
        }

        private void btn1cent_Click(object sender, EventArgs e)
        {
            totalCoins += 0.01;
            txtValorTotal.Text = Convert.ToString(totalCoins);
            return;
        }

        private void btn5cent_Click(object sender, EventArgs e)
        {
            ErrorInsertCoins();
            return;
        }

        private void btn10cent_Click(object sender, EventArgs e)
        {
            ErrorInsertCoins();
            return;
        }

        private void btn25cent_Click(object sender, EventArgs e)
        {
            totalCoins += 0.25;
            txtValorTotal.Text = Convert.ToString(totalCoins);
            return;
        }

        private void btn50cent_Click(object sender, EventArgs e)
        {
            totalCoins += 0.50;
            txtValorTotal.Text = Convert.ToString(totalCoins);
            return;
        }

        private void btn1real_Click(object sender, EventArgs e)
        {
            totalCoins += 1.00;
            txtValorTotal.Text = Convert.ToString(totalCoins);
            return;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            pnlMenu.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            validacao();

            tratandoCheckBox();

            calcGeneral();
        }
            
        private void btnReset_Click(object sender, EventArgs e)
        {
            resetAll();
        }

        private void cbItem1_CheckedChanged(object sender, EventArgs e)
        {
            AtivaQtd();
        }

        private void cbItem2_CheckedChanged(object sender, EventArgs e)
        {
            AtivaQtd();
        }

        private void cbItem3_CheckedChanged(object sender, EventArgs e)
        {
            AtivaQtd();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            pnlFinal.Visible = false;
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            pnlFinal.Visible = true; 
        }

        #endregion
    }
}
