using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace Runner_Game
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer gameTimer = new DispatcherTimer();
        private Rect groundHitBox;
        private Rect obstacleHitBox;
        private Rect playerHitBox;

        private bool isJumping;
        private int force = 20;
        private int speed = 5;
        private Random random = new Random();
        private bool gameOver;
        private double spriteIndex = 0;

        private ImageBrush playerSprite = new ImageBrush();
        private ImageBrush backgroundSprite = new ImageBrush();
        private ImageBrush obstacleSprite = new ImageBrush();

        private int[] obstaclePosition = { 320, 310, 300, 305, 315 };
        private int score = 0;

        public MainWindow()
        {
            InitializeComponent();
            MyCanvas.Focus();

            gameTimer.Tick += GameEngine;
            gameTimer.Interval = TimeSpan.FromMilliseconds(20);

            backgroundSprite.ImageSource = new BitmapImage(new Uri(@"C:\Users\bajer\Documents\Portfolio\.NET\Runner Game\Assets\background.gif"));

            background.Fill = backgroundSprite;
            background2.Fill = backgroundSprite;

            StartGame();
        }

        private void GameEngine(object sender, EventArgs e)
        {
            Canvas.SetLeft(background, Canvas.GetLeft(background) - 8);
            Canvas.SetLeft(background2, Canvas.GetLeft(background2) - 8);

            if (Canvas.GetLeft(background) < -1262)
            {
                Canvas.SetLeft(background, Canvas.GetLeft(background2) + background2.Width);
            }
            if (Canvas.GetLeft(background2) < -1262)
            {
                Canvas.SetLeft(background2, Canvas.GetLeft(background) + background.Width);
            }

            Canvas.SetTop(player, Canvas.GetTop(player) + speed);
            Canvas.SetLeft(obstacle, Canvas.GetLeft(obstacle) - 12);
            scoreText.Content = "Score : " + score;

            playerHitBox = new Rect(Canvas.GetLeft(player), Canvas.GetTop(player), player.Width - 15, player.Height);
            obstacleHitBox = new Rect(Canvas.GetLeft(obstacle), Canvas.GetTop(obstacle), obstacle.Width, obstacle.Height);
            groundHitBox = new Rect(Canvas.GetLeft(ground), Canvas.GetTop(ground), ground.Width, ground.Height);

            if (playerHitBox.IntersectsWith(groundHitBox))
            {
                speed = 0;
                Canvas.SetTop(player, Canvas.GetTop(ground) - player.Height);
                isJumping = false;
                spriteIndex += .5;

                if (spriteIndex > 8)
                {
                    spriteIndex = 1;
                }
                RunSprite(spriteIndex);
            }
            if (isJumping == true)
            {
                speed = -9;
                force -= 1;
            }
            else
            {
                speed = 12;
            }
            if (force < 0)
            {
                isJumping = false;
            }
            if (Canvas.GetLeft(obstacle) < -50)
            {
                Canvas.SetLeft(obstacle, 950);
                Canvas.SetTop(obstacle, obstaclePosition[random.Next(1, obstaclePosition.Length)]);
                score += 1;
            }
            if (playerHitBox.IntersectsWith(obstacleHitBox))
            {
                gameOver = true;
                gameTimer.Stop();
            }
            if (gameOver == true)
            {
                obstacle.Stroke = Brushes.Black;
                obstacle.StrokeThickness = 1;

                player.Stroke = Brushes.Red;
                player.StrokeThickness = 1;

                scoreText.Content = "Score : " + score + "Press Enter to restart the game";
            }
            else
            {
                player.StrokeThickness = 0;
                obstacle.StrokeThickness = 0;
            }
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && gameOver == true)
            {
                StartGame();
            }
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space && isJumping == false && Canvas.GetTop(player) > 260)
            {
                isJumping = true;
                force = 15;
                speed = -12;

                playerSprite.ImageSource = new BitmapImage(new Uri(@"C:\Users\bajer\Documents\Portfolio\.NET\Runner Game\Assets\newRunner_02.gif"));
            }
        }

        private void StartGame()
        {
            Canvas.SetLeft(background, 0);
            Canvas.SetLeft(background2, 1262);

            Canvas.SetLeft(player, 110);
            Canvas.SetTop(player, 140);

            Canvas.SetLeft(obstacle, 950);
            Canvas.SetTop(obstacle, 310);

            RunSprite(1);

            obstacleSprite.ImageSource = new BitmapImage(new Uri(@"C:\Users\bajer\Documents\Portfolio\.NET\Runner Game\Assets\obstacle.png"));
            obstacle.Fill = obstacleSprite;

            isJumping = false;
            gameOver = false;
            score = 0;
            scoreText.Content = "Score : " + score;

            gameTimer.Start();
        }

        private void RunSprite(double i)
        {
            switch (i)
            {
                case 1:
                    playerSprite.ImageSource = new BitmapImage(new Uri(@"C:\Users\bajer\Documents\Portfolio\.NET\Runner Game\Assets\newRunner_01.gif"));
                    break;

                case 2:
                    playerSprite.ImageSource = new BitmapImage(new Uri(@"C:\Users\bajer\Documents\Portfolio\.NET\Runner Game\Assets\newRunner_02.gif"));
                    break;

                case 3:
                    playerSprite.ImageSource = new BitmapImage(new Uri(@"C:\Users\bajer\Documents\Portfolio\.NET\Runner Game\Assets\newRunner_03.gif"));
                    break;

                case 4:
                    playerSprite.ImageSource = new BitmapImage(new Uri(@"C:\Users\bajer\Documents\Portfolio\.NET\Runner Game\Assets\newRunner_04.gif"));
                    break;

                case 5:
                    playerSprite.ImageSource = new BitmapImage(new Uri(@"C:\Users\bajer\Documents\Portfolio\.NET\Runner Game\Assets\newRunner_05.gif"));
                    break;

                case 6:
                    playerSprite.ImageSource = new BitmapImage(new Uri(@"C:\Users\bajer\Documents\Portfolio\.NET\Runner Game\Assets\newRunner_06.gif"));
                    break;

                case 7:
                    playerSprite.ImageSource = new BitmapImage(new Uri(@"C:\Users\bajer\Documents\Portfolio\.NET\Runner Game\Assets\newRunner_07.gif"));
                    break;

                case 8:
                    playerSprite.ImageSource = new BitmapImage(new Uri(@"C:\Users\bajer\Documents\Portfolio\.NET\Runner Game\Assets\newRunner_08.gif"));
                    break;
            }
            player.Fill = playerSprite;
        }
    }
}