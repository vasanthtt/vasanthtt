using System;
using System.Windows.Forms;

namespace CalculatorApp
{
    public class CalculatorForm : Form
    {
        private TextBox display;
        private string? currentOperator;
        private double firstOperand;
        private bool clearOnNext;

        public CalculatorForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            Text = "Calculator";
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;

            display = new TextBox
            {
                ReadOnly = true,
                Text = "0",
                TextAlign = HorizontalAlignment.Right,
                Dock = DockStyle.Top,
                Height = 40
            };
            Controls.Add(display);

            var panel = new TableLayoutPanel
            {
                RowCount = 4,
                ColumnCount = 4,
                Dock = DockStyle.Fill
            };
            Controls.Add(panel);

            string[] buttons =
            {
                "7", "8", "9", "/",
                "4", "5", "6", "*",
                "1", "2", "3", "-",
                "0", "C", "=", "+"
            };

            int index = 0;
            for (int r = 0; r < 4; r++)
            {
                for (int c = 0; c < 4; c++)
                {
                    var text = buttons[index++];
                    var btn = new Button { Text = text, Dock = DockStyle.Fill };
                    btn.Click += OnButtonClick;
                    panel.Controls.Add(btn, c, r);
                }
            }
        }

        private void OnButtonClick(object? sender, EventArgs e)
        {
            if (sender is not Button btn) return;
            string text = btn.Text;

            if (text == "C")
            {
                display.Text = "0";
                currentOperator = null;
                firstOperand = 0;
                clearOnNext = false;
                return;
            }

            if (text is "+" or "-" or "*" or "/")
            {
                firstOperand = double.Parse(display.Text);
                currentOperator = text;
                clearOnNext = true;
                return;
            }

            if (text == "=")
            {
                double second = double.Parse(display.Text);
                double result = firstOperand;
                switch (currentOperator)
                {
                    case "+": result += second; break;
                    case "-": result -= second; break;
                    case "*": result *= second; break;
                    case "/": result /= second; break;
                }
                display.Text = result.ToString();
                clearOnNext = true;
                return;
            }

            if (display.Text == "0" || clearOnNext)
            {
                display.Text = text;
                clearOnNext = false;
            }
            else
            {
                display.Text += text;
            }
        }
    }
}
