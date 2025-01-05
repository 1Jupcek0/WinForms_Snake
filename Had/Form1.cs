using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Had {
	public partial class Form1 : Form {
		Direction Smer = new Direction();
		Piece Hlava = new Piece(200, 200, Color.Black);
		List<Piece> telo = new List<Piece>();
		Food Jidlo = new Food(160, 160, Color.Brown);
		Random rnd = new Random();
		int score = 0;
		public Form1() {
			InitializeComponent();
			
			ScoreLbl.Text += score;
			ScoreLbl.Location = new Point(Width / 2-ScoreLbl.Size.Width,5);
			
			Controls.Add(Hlava);
			Controls.Add(Jidlo);
			Smer = Direction.nowhere;
			timer1.Start();
			this.Hide();

		}
		private void Form1_KeyDown(object sender, KeyEventArgs e) {
			switch (e.KeyCode) {
				case Keys.Right:
					if(Smer != Direction.left) { 
						Smer = Direction.right;
					}
					break;
				case Keys.Left:
					if(Smer != Direction.right) { 
						Smer = Direction.left;
					}
					break;
				case Keys.Up:
					if(Smer != Direction.down) {
						Smer = Direction.up;
					}
					break;
				case Keys.Down:
					if(Smer != Direction.up) {
						Smer = Direction.down;
					}
					break;
			}
		}
		void BodyVice(Point konec) {
			if(telo.Count != 0) {
				telo.Add(new Piece(konec.X,konec.Y, Color.Red));
				Controls.Add(telo[telo.Count-1]);
			}
            else {
				switch(Smer) {
					case Direction.up:
						telo.Add(new Piece(Hlava.X, Hlava.Y + 20, Color.Red));
						break;
					case Direction.down:
						telo.Add(new Piece(Hlava.X, Hlava.Y - 20, Color.Red));
						break;
					case Direction.left:
						telo.Add(new Piece(Hlava.X + 20, Hlava.Y, Color.Red));
						break;
					case Direction.right:
						telo.Add(new Piece(Hlava.X - 20, Hlava.Y, Color.Red));
						break;
					default:
						MessageBox.Show("Error BodyVice");
						break;
				}
				Controls.Add(telo[0]);
			}
        }

        private void timer1_Tick(object sender, EventArgs e) {
			ScoreLbl.Location = new Point(Width / 2 - ScoreLbl.Size.Width, 5);
			Point predchoziHlava = new Point(Hlava.X, Hlava.Y);
			switch(Smer) {
				case Direction.up:
					Hlava.Y -= 20;
					break;
				case Direction.down:
					Hlava.Y += 20;
					break;
				case Direction.right:
					Hlava.X += 20;
					break;
				case Direction.left:
					Hlava.X -= 20;
					break;
				case Direction.nowhere:
					break;
				default:
					MessageBox.Show("Direction Error");
					break;
			}
			if(Hlava.X < 0 || Hlava.X > this.Width || Hlava.Y < 0 || Hlava.Y > this.Height) {
				timer1.Stop();
				MessageBox.Show("Narazil jsi do zdi");
				this.Close();
			}
			foreach(var telo in telo) {
				if(Hlava.Location == telo.Location) {
					timer1.Stop();
					MessageBox.Show("Narazil jsi do těla");
					this.Close();
				}
            }
			Point predchoziKonec = new Point();
			if(Smer != Direction.nowhere) {
				if(telo.Count == 0) {
					Hlava.Location = new Point(Hlava.X, Hlava.Y);
				}
                else {
					for(int i = telo.Count-1; i >= 0; i--) {
						if(i == 0) {
							telo[i].Location = predchoziHlava;
							Hlava.Location = new Point(Hlava.X, Hlava.Y);
							break;
						}
						telo[i].Location = telo[i - 1].Location;
					}
					predchoziKonec = telo[telo.Count - 1].Location;
				}
            }
			if(Hlava.Location == Jidlo.Location) {
				Jidlo.Location = new Point(rnd.Next(0, this.Width) / 20 * 20, rnd.Next(0, this.Height) / 20 * 20);

				score++;
				ScoreLbl.Text = "Score: " + score;
				ScoreLbl.Location = new Point(Width / 2 - ScoreLbl.Size.Width, 5);
                if((score % 10) == 0) {
					timer1.Interval -= 50;
                }
				BodyVice(predchoziKonec);
			}

		}
    }
}
