//******************************************************************************
// Author Information:
// Name: Victor V. Vu
// Email: vuvictor@csu.fullerton.edu
// Section: 223N-01
//
// Program Information:
// Program Name: Double Pinball Rebound
// This File: DoublePinballui.cs
// Description: UI file containing animations for the double rebounding balls
//******************************************************************************
// Copyright (C) 2022 Victor V. Vu
// This program is free software: you can redistribute it and/or modify it under
// the terms of the GNU General Public License version 3 as published by the
// Free Software Foundation. This program is distributed in the hope that it
// will be useful, but WITHOUT ANY WARRANTY without even the implied Warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General
// Public License for more details. A copy of the GNU General Public License v3
// is available here: <https://www.gnu.org/licenses/>.
//******************************************************************************
// Programmed in Ubuntu-based Linux Platform.
// To run program, type in terminal: "sh r.sh"
//******************************************************************************
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Timers;

// Call functions in form library & delcare variables
public class DoublePinballui : Form {
  private Label author = new Label();
  private Label speed_label = new Label();
  private TextBox speed_input = new TextBox();
  private Button initialize_button = new Button();
  private Button start_button = new Button();
  private Label coords = new Label();
  private Label x_label = new Label();
  private Label y_label = new Label();
  private TextBox x_coord = new TextBox();
  private TextBox y_coord = new TextBox();
  private Button quit_button = new Button();
  private Panel header_panel = new Panel();
  private Graphicpanel display_panel = new Graphicpanel();
  private Panel control_panel = new Panel();
  private Size max_exit_ui_size = new Size(1024, 900);
  private Size min_exit_ui_size = new Size(1024, 900);
  // set up constants and static variables
  private const double refresh_rate = 60.00; // speed in Hz
  private const double motion_clock_rate = 42.60; // speed in tics/seconds
  private static double speed; // input speed in pixel/seconds
  private static double direction; // input direction in degrees
  private static double X;
  private static double Y;
  private static double X2;
  private static double Y2;
  private static double ball_center_x;
  private static double ball_center_y;
  private static double ball_center_x2;
  private static double ball_center_y2;
  private static double Δx;
  private static double Δy;
  private static double Δx2;
  private static double Δy2;
  private static double ball_speed_pixel_per_tic;
  private static bool button_pressed = false; // control if statement
  private static bool ball_visible = false;
  // Declare ball timer and interval
  private double ball_clock_interval = 1000.00/motion_clock_rate;
  private static System.Timers.Timer ball_clock = new System.Timers.Timer();
  // Declare the refresh clock.
  private double refresh_clock_interval = 1000.00/refresh_rate;
  private static System.Timers.Timer ui_refresh_clock = new System.Timers.Timer();

  // Initialize Variables
  public DoublePinballui() {
    // Assign size to the ui
    MaximumSize = max_exit_ui_size;
    MinimumSize = min_exit_ui_size;
    // Initialize string variables
    Text = "Ricochet Pinball";
    author.Text = "Ricochet Ball by Victor V. Vu";
    speed_label.Text = "Enter Speed (pixel/seconds)";
    initialize_button.Text = "Initialize";
    start_button.Text = "Start";
    coords.Text = "Coordinates";
    x_label.Text = "X =";
    y_label.Text = "Y =";
    quit_button.Text = "Quit";
    // Set size values (width, length)
    author.Size = new Size(440, 40);
    speed_label.Size = new Size(230, 30);
    speed_input.Size = new Size(70, 60);
    initialize_button.Size = new Size(120, 60);
    start_button.Size = new Size(120, 60);
    coords.Size = new Size(100, 30);
    x_label.Size = new Size(30, 30);
    y_label.Size = new Size(30, 30);
    x_coord.Size = new Size(50, 60);
    y_coord.Size = new Size(50, 60);
    quit_button.Size = new Size(120, 60);
    header_panel.Size = new Size(1024, 50);
    display_panel.Size = new Size(1024, 625);
    control_panel.Size = new Size(1024, 200);
    // Set colors for panel and buttons
    header_panel.BackColor = Color.Cornsilk;
    display_panel.BackColor = Color.BurlyWood;
    control_panel.BackColor = Color.CornflowerBlue;
    initialize_button.BackColor = Color.MediumAquamarine;
    start_button.BackColor = Color.MediumAquamarine;
    quit_button.BackColor = Color.MediumAquamarine;
    // Set text fonts and font size
    author.Font = new Font("Times New Roman", 26, FontStyle.Regular);
    speed_label.Font = new Font("Times New Roman", 15, FontStyle.Regular);
    speed_input.Font = new Font("Times New Roman", 15, FontStyle.Regular);
    initialize_button.Font = new Font("Times New Roman", 15, FontStyle.Regular);
    start_button.Font = new Font("Times New Roman", 15, FontStyle.Regular);
    coords.Font = new Font("Times New Roman", 15, FontStyle.Underline);
    x_label.Font = new Font("Times New Roman", 15, FontStyle.Regular);
    y_label.Font = new Font("Times New Roman", 15, FontStyle.Regular);
    x_coord.Font = new Font("Times New Roman", 15, FontStyle.Regular);
    y_coord.Font = new Font("Times New Roman", 15, FontStyle.Regular);
    quit_button.Font = new Font("Times New Roman", 15, FontStyle.Regular);
    // Set text alignment and property
    author.TextAlign = ContentAlignment.MiddleCenter;
    speed_input.TextAlign = HorizontalAlignment.Center;
    x_coord.TextAlign = HorizontalAlignment.Center;
    y_coord.TextAlign = HorizontalAlignment.Center;
    x_coord.ReadOnly = true;
    y_coord.ReadOnly = true;
    // Set locations (width, length)
    author.Location = new Point(300, 5);
    speed_label.Location = new Point(300, 25);
    speed_input.Location = new Point(340, 60);
    initialize_button.Location = new Point(110, 35);
    start_button.Location = new Point(110, 110);
    coords.Location = new Point(610, 50);
    x_label.Location = new Point(550, 100);
    y_label.Location = new Point(650, 100);
    x_coord.Location = new Point(590, 100);
    y_coord.Location = new Point(690, 100);
    quit_button.Location = new Point(790, 75);
    header_panel.Location = new Point(0, 0);
    display_panel.Location = new Point(0, 50);
    control_panel.Location = new Point(0, 675);
    // Control elements to display
    Controls.Add(header_panel);
    header_panel.Controls.Add(author);
    Controls.Add(display_panel);
    Controls.Add(control_panel);
    control_panel.Controls.Add(speed_label);
    control_panel.Controls.Add(speed_input);
    control_panel.Controls.Add(initialize_button);
    control_panel.Controls.Add(start_button);
    control_panel.Controls.Add(coords);
    control_panel.Controls.Add(x_label);
    control_panel.Controls.Add(y_label);
    control_panel.Controls.Add(x_coord);
    control_panel.Controls.Add(y_coord);
    control_panel.Controls.Add(quit_button);
    // Control buttons when clicked
    initialize_button.Click += new EventHandler(initialize);
    start_button.Enabled = false; // start is disabled at launch
    start_button.Click += new EventHandler(start);
    quit_button.Click += new EventHandler(terminate);
    // Set properties of the refresh clock
    ui_refresh_clock.Enabled = false;
    ui_refresh_clock.Interval = refresh_clock_interval;
    ui_refresh_clock.Elapsed += new ElapsedEventHandler(refresh_ui);
    // Prepare the ball clock properties
    ball_clock.Enabled = false;
    ball_clock.Interval = ball_clock_interval;
    ball_clock.Elapsed += new ElapsedEventHandler(update_ball_coords);
    // set location to start at 1/3 of width
    X = display_panel.Width/3;
    Y = display_panel.Height/2;
    X2 = display_panel.Width - display_panel.Width/3;
    Y2 = display_panel.Height/2;
    CenterToScreen(); // Center the screen when program is opened
  } // End of ui constructor

  // Function draw balls at center and clears inputs
  protected void initialize(Object sender, EventArgs h) {
    try { // check if user inputted coords
      if (speed_input != null) {
        // convert input to double then display starting coords
        speed = double.Parse(speed_input.Text);
        x_coord.Text = "" + (int)Math.Round(X) + "";
        y_coord.Text = "" + (int)Math.Round(Y) + "";
        // restore control to start
        ball_visible = true;
        start_button.Enabled = true;
        speed_input.Clear();
        // set the coordinates to ball center
        ball_center_x = X;
        ball_center_y = Y;
        ball_center_x2 = X2;
        ball_center_y2 = Y2;
        ball_speed_pixel_per_tic = speed/motion_clock_rate; // control the speed
        // convert degrees to radians
        direction = 0; // place holder code for now until randomizer is written
        Δx = (ball_speed_pixel_per_tic)*Math.Cos(((Math.PI / 180) * -direction));
        Δy = (ball_speed_pixel_per_tic)*Math.Sin(((Math.PI / 180) * -direction));
        Δx2 = (ball_speed_pixel_per_tic)*Math.Cos(((Math.PI / 180) * -direction));
        Δy2 = (ball_speed_pixel_per_tic)*Math.Sin(((Math.PI / 180) * -direction));
        display_panel.Invalidate(); // calls OnPaint
      } // end of if statement
    } // end of try
    catch (Exception) { // prevents program from crashing in case of error
      Console.WriteLine("No input detected"); // program does nothing
    } // end of catch
  } // End of method initialize

  // Function to update coords & animate the ball
  protected void update_ball_coords(System.Object sender, ElapsedEventArgs even) {
    // check if the balls have collided with walls
    ball_center_x += Δx;
    ball_center_x2 += Δx2;
    if (ball_center_x + 12.5 >= 1015 || ball_center_x - 12.5 <= 0) {
      Δx = -1 * Δx;
    }
    if (ball_center_x2 + 12.5 >= 1015 || ball_center_x2 - 12.5 <= 0) {
      Δx2 = -1 * Δx2;
    }
    ball_center_y += Δy;
    ball_center_y2 += Δy2;
    if (ball_center_y + 12.5 >= display_panel.Height || ball_center_y - 12.5 <= 0) {
      Δy = -1 * Δy;
    }
    if (ball_center_y2 + 12.5 >= display_panel.Height || ball_center_y2 - 12.5 <= 0) {
      Δy2 = -1 * Δy2;
    }
    // checks if the two balls collided with each other
    if (ball_center_x + 12.5 >= ball_center_x2 - 12.5) {
      Δx = -1 * Δx;
      Δx2 = -1 * Δx2;
    }
    if (ball_center_y + 12.5 >= ball_center_y - 12.5 ) {
      Δy = -1 * Δy;
      Δy2 = -1 * Δy2;
    }
  } // End of method update_ball_coords

  // Function called to start animation
  protected void start(Object sender, EventArgs h) {
    if (button_pressed == false) { // begin timer
      start_button.Text = "Pause";
      button_pressed = true;
      ui_refresh_clock.Enabled = true;
      ball_clock.Enabled = true;
    } else { // pause timers
      start_button.Text = "Resume";
      button_pressed = false;
      ui_refresh_clock.Enabled = false;
      ball_clock.Enabled = false;
    }
  } // End of go

  // tracks the current location of the ball
  protected void refresh_ui(Object sender, EventArgs h) {
    x_coord.Text = "" + (int)Math.Round(ball_center_x) + "";
    y_coord.Text = "" + (int)Math.Round(ball_center_y) + "";
    display_panel.Invalidate(); // calls OnPaint
  }

  // Function called by quit button to terminate
  protected void terminate(Object sender, EventArgs h) {
    System.Console.WriteLine("This program will now quit.");
    Close();
  }

  // Graphic class to output a panel
  public class Graphicpanel : Panel {
    public Graphicpanel() { Console.WriteLine("A graphic panel was created."); }
    // Calls OnPaint to draw ball
    protected override void OnPaint(PaintEventArgs ii) {
      Graphics graph = ii.Graphics;
      if (ball_visible) {
        // (x, y, width, length)
        graph.FillEllipse(Brushes.Crimson, (float)Math.Round(ball_center_x - 12.5), (float)Math.Round(ball_center_y - 12.5), 25, 25);
        graph.FillEllipse(Brushes.White, (float)Math.Round(ball_center_x2 - 12.5), (float)Math.Round(ball_center_y2 - 12.5), 25, 25);
      }
      base.OnPaint(ii);
    } // OnPaint end
  } // End of graphics constructor
} // End of main class
