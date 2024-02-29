namespace lab4
{
    public partial class Form1 : Form
    {
        bool[,] map;
        const int size = 10;

        private Color aliveColor = Color.Green;
        private Color deadColor = Color.White;

        Random random = new Random();

        List<Double> probs = new List<Double>(); 
        public Form1()
        {
            InitializeComponent();
            map = new bool[size, size];
            dataGridView1.RowCount = size;
            dataGridView1.ColumnCount = size;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.Transparent;

            dataGridView1.ColumnHeadersVisible = false;
            dataGridView1.RowHeadersVisible = false;
            probs.Add(0);
            probs.Add(0.1);
            probs.Add(0.6);
            probs.Add(0.8);
            probs.Add(0.6);
            probs.Add(0.4);
            probs.Add(0.2);
            probs.Add(0.01);
            probs.Add(0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled)
            {
                timer1.Stop();
            }
            else
            {
                timer1.Start();
            }
        }

        private void updateGridView()
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    dataGridView1.Rows[i].Cells[j].Style.BackColor = map[i, j] ? aliveColor : deadColor;
                }
            }
        }

        private int countNeighbors(int row, int column)
        {
            int count = 0;

            for (int i = row - 1; i <= row + 1; i++)
            {
                for (int j = column - 1; j <= column + 1; j++)
                {
                    if (i == row && j == column)
                        continue;

                    if (i >= 0 && i < size && j >= 0 && j < size)
                    {
                        if (map[i, j])
                            count++;
                    }
                }
            }

            return count;
        }

        private bool isCellAlive(int row, int column) 
        {
            int Neighbors = countNeighbors(row, column);

            return random.NextDouble() > probs[Neighbors];
        }

        private void UpdateCellStates()
        {
            for(int i = 0;i < size;i++) 
            {
                for(int j = 0; j < size;j++) 
                {
                    map[i, j] = isCellAlive(i, j);
                }
            }
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            UpdateCellStates();
            updateGridView();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            map[e.RowIndex, e.ColumnIndex] = !map[e.RowIndex, e.ColumnIndex];
        }
    }
}