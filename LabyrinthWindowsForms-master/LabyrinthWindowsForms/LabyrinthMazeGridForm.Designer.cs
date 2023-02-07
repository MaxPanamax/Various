namespace Ponomarenko_Labyrinth_WF
{
	partial class LabyrinthMazeGridForm
	{
        /// <краткое содержание>
        /// Требуемая переменная конструктора.
        /// </краткое содержание>
        private System.ComponentModel.IContainer components = null;
        /// <краткое содержание>
        /// Очистите все используемые ресурсы.
        /// </краткое содержание>
        ///<param name = "disposing" > значение true,
        ///если управляемые ресурсы должны быть утилизированы; в противном случае значение false.</param>

        protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.MazePanel = new System.Windows.Forms.Panel();
            this.SearchButton = new System.Windows.Forms.Button();
            this.NewButton = new System.Windows.Forms.Button();
            this.RowBox = new System.Windows.Forms.TextBox();
            this.ColumnBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // MazePanel
            // 
            this.MazePanel.BackColor = System.Drawing.Color.White;
            this.MazePanel.Location = new System.Drawing.Point(226, 3);
            this.MazePanel.Name = "MazePanel";
            this.MazePanel.Size = new System.Drawing.Size(401, 401);
            this.MazePanel.TabIndex = 0;
            // 
            // SearchButton
            // 
            this.SearchButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.SearchButton.Enabled = false;
            this.SearchButton.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SearchButton.ForeColor = System.Drawing.Color.Navy;
            this.SearchButton.Location = new System.Drawing.Point(50, 277);
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.Size = new System.Drawing.Size(105, 49);
            this.SearchButton.TabIndex = 1;
            this.SearchButton.Text = "Поиск";
            this.SearchButton.UseVisualStyleBackColor = false;
            this.SearchButton.Click += new System.EventHandler(this.SearchButton_Click);
            // 
            // NewButton
            // 
            this.NewButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.NewButton.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.NewButton.ForeColor = System.Drawing.Color.Navy;
            this.NewButton.Location = new System.Drawing.Point(50, 209);
            this.NewButton.Name = "NewButton";
            this.NewButton.Size = new System.Drawing.Size(105, 51);
            this.NewButton.TabIndex = 2;
            this.NewButton.Text = "Создать новый";
            this.NewButton.UseVisualStyleBackColor = false;
            this.NewButton.Click += new System.EventHandler(this.NewButton_Click);
            // 
            // RowBox
            // 
            this.RowBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.RowBox.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.RowBox.ForeColor = System.Drawing.Color.Navy;
            this.RowBox.Location = new System.Drawing.Point(129, 169);
            this.RowBox.MaxLength = 2;
            this.RowBox.Name = "RowBox";
            this.RowBox.Size = new System.Drawing.Size(26, 22);
            this.RowBox.TabIndex = 3;
            this.RowBox.Text = "20";
            this.RowBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.RowBox.TextChanged += new System.EventHandler(this.RowBox_TextChanged);
            this.RowBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.RowBox_KeyPress);
            // 
            // ColumnBox
            // 
            this.ColumnBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.ColumnBox.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ColumnBox.ForeColor = System.Drawing.Color.Navy;
            this.ColumnBox.Location = new System.Drawing.Point(129, 143);
            this.ColumnBox.MaxLength = 2;
            this.ColumnBox.Name = "ColumnBox";
            this.ColumnBox.Size = new System.Drawing.Size(26, 22);
            this.ColumnBox.TabIndex = 4;
            this.ColumnBox.Text = "20";
            this.ColumnBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ColumnBox.TextChanged += new System.EventHandler(this.ColumnBox_TextChanged);
            this.ColumnBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ColumnBox_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.Color.Navy;
            this.label1.Location = new System.Drawing.Point(43, 143);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 19);
            this.label1.TabIndex = 5;
            this.label1.Text = "Ширина:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.ForeColor = System.Drawing.Color.Navy;
            this.label2.Location = new System.Drawing.Point(46, 168);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 19);
            this.label2.TabIndex = 6;
            this.label2.Text = "Высота:";
            // 
            // LabyrinthMazeGridForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(784, 407);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ColumnBox);
            this.Controls.Add(this.RowBox);
            this.Controls.Add(this.NewButton);
            this.Controls.Add(this.SearchButton);
            this.Controls.Add(this.MazePanel);
            this.ForeColor = System.Drawing.Color.Navy;
            this.Name = "LabyrinthMazeGridForm";
            this.Text = "Лабиринт";
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Panel MazePanel;
		private System.Windows.Forms.Button SearchButton;
		private System.Windows.Forms.Button NewButton;
		private System.Windows.Forms.TextBox RowBox;
		private System.Windows.Forms.TextBox ColumnBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
	}
}

