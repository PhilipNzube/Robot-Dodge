using System;
using SplashKitSDK;
public class Program
{
    public static void Main()
    {
        Window gameWindow = new Window("Robot Dodge", 800, 600);
        // Player playerCode = new Player(gameWindow);
        RoboDodge roboDodge = new RoboDodge(gameWindow);
        while (!gameWindow.CloseRequested)
        {
            //Previous Functional Control Code(Might be useful to you sir...)

            // if (SplashKit.KeyDown(KeyCode.UpKey))
            // {
            //     playerCode.MoveUp();
            // }
            // else if (SplashKit.KeyDown(KeyCode.DownKey))
            // {
            //     playerCode.MoveDown();
            // }
            // else if (SplashKit.KeyDown(KeyCode.LeftKey))
            // {
            //     playerCode.MoveLeft();
            // }
            // else if (SplashKit.KeyDown(KeyCode.RightKey))
            // {
            //     playerCode.MoveRight();
            // }

            gameWindow.Clear(Color.White);
            roboDodge.HandleInput();
            roboDodge.Draw();
            roboDodge.Update();
            gameWindow.Refresh(60);
            SplashKit.ProcessEvents();
        }
    }
}



public abstract class Hud
{
    protected Window _gameWindow;
    private int _life = 10;

    public Font font
    {
        get; set;
    }

    public int Life
    {
        get { return _life; }
        set { _life = value; }
    }

    protected int X { get; set; }
    protected int Y { get; set; }

    public virtual void Draw()
    {
        int windowWidth = _gameWindow.Width;
        _gameWindow.FillRectangle(Color.RGBAColor(10, 29, 49, 100), 0, 555, windowWidth, 50);
    }
}

public class DisplayLife : Hud
{
    int _lives = 3;
    public DisplayLife(Window gameWindow)
    {
        X = 670;
        Y = 570;
        _gameWindow = gameWindow;
    }
    public int Lives
    {
        get { return _lives; }
        set { _lives = value; }
    }

    public override void Draw()
    {
        String Lives1 = Convert.ToString(Lives);
        _gameWindow.DrawText($"{Lives1}", Color.White, "StencilStd.otf", 20, 700, 570);
        _gameWindow.FillCircle(Color.Red, 765, 575, 10);
        _gameWindow.FillRectangle(Color.RGBAColor(10, 29, 49, 100), 650, 565, 210, 50);

    }
}

public class DisplayScore : Hud
{
    public DisplayScore(Window gameWindow)
    {
        _gameWindow = gameWindow;
    }


    public override void Draw()
    {

        _gameWindow.DrawText("Score Here", Color.White, "StencilStd.otf", 20, 10, 570);

    }
}


public abstract class Robot
{
    protected Vector2D Velocity { get; set; }
    public double X { get; set; }
    public double Y { get; set; }
    public Color MainColor { get; set; }
    public bool OffScreen
    {
        get; set;
    }

    public int Width
    {
        get { return 50; }
    }
    public int Height
    {
        get { return 50; }
    }

    public Circle CollissionCircle
    {
        get { return SplashKit.CircleAt(X + Width / 2, Y + Height / 2, 20); }
    }


    public abstract void Draw();
    public void Update()
    {

        X += Velocity.X;
        Y += Velocity.Y;

    }

    public void isOffScreen(Window screen)
    {
        if (X < -Width || X > screen.Width || Y < -Height || Y > screen.Height)
        {

            OffScreen = true;
        }
    }

}

public class Boxy : Robot
{
    public Boxy(Window gameWindow, Player player)
    {
        SplashKit.Rnd();
        if (SplashKit.Rnd() < 0.5)
        {
            X = SplashKit.Rnd(gameWindow.Width);


            if (SplashKit.Rnd() < 0.5)

                Y = -Height;

            else

                Y = gameWindow.Height;
        }
        else
        {
            Y = SplashKit.Rnd(gameWindow.Height);

            if (SplashKit.Rnd() < 0.5)

                X = -Width;

            else

                Y = gameWindow.Width;

        }
        MainColor = Color.RandomRGB(200);

        int speed = 4;
        if (SplashKit.KeyDown(KeyCode.SpaceKey))
        {
            speed += 7;
        }


        Point2D fromPt = new Point2D()
        {
            X = X,
            Y = Y
        };

        Point2D toPt = new Point2D()
        {
            X = player.X,
            Y = player.Y
        };

        Vector2D dir;
        dir = SplashKit.UnitVector(SplashKit.VectorPointToPoint(fromPt, toPt));

        Velocity = SplashKit.VectorMultiply(dir, speed);


    }
    public override void Draw()
    {
        double leftX;
        double rightX;

        double eyeY;
        double mouthY;

        leftX = X + 12;
        rightX = X + 27;

        eyeY = Y + 10;
        mouthY = Y + 30;

        SplashKit.FillRectangle(Color.Random(), X, Y, Width, Height);

        SplashKit.FillRectangle(MainColor, leftX, eyeY, 10, 10);
        SplashKit.FillRectangle(MainColor, rightX, eyeY, 10, 10);
        SplashKit.FillRectangle(MainColor, leftX, mouthY, 25, 10);
        SplashKit.FillRectangle(MainColor, leftX + 2, mouthY + 2, 21, 6);

    }

}

public class Roundy : Robot
{
    public Roundy(Window gameWindow, Player player)
    {
        SplashKit.Rnd();
        if (SplashKit.Rnd() < 0.5)
        {
            X = SplashKit.Rnd(gameWindow.Width);


            if (SplashKit.Rnd() < 0.5)

                Y = -Height;

            else

                Y = gameWindow.Height;
        }
        else
        {
            Y = SplashKit.Rnd(gameWindow.Height);

            if (SplashKit.Rnd() < 0.5)

                X = -Width;

            else

                Y = gameWindow.Width;

        }
        MainColor = Color.RandomRGB(200);

        int speed = 4;
        if (SplashKit.KeyDown(KeyCode.SpaceKey))
        {
            speed += 7;
        }


        Point2D fromPt = new Point2D()
        {
            X = X,
            Y = Y
        };

        Point2D toPt = new Point2D()
        {
            X = player.X,
            Y = player.Y
        };

        Vector2D dir;
        dir = SplashKit.UnitVector(SplashKit.VectorPointToPoint(fromPt, toPt));

        Velocity = SplashKit.VectorMultiply(dir, speed);


    }

    public override void Draw()
    {
        double leftX, midX, rightX;
        double midY, eyeY, mouthY;
        leftX = X + 17;
        midX = X + 25;
        rightX = X + 33;
        midY = Y + 25;
        eyeY = Y + 20;
        mouthY = Y + 35;
        SplashKit.FillCircle(Color.White, midX, midY, 25);
        SplashKit.DrawCircle(Color.Gray, midX, midY, 25);
        SplashKit.FillCircle(MainColor, leftX, eyeY, 5);
        SplashKit.FillCircle(MainColor, rightX, eyeY, 5);
        SplashKit.FillEllipse(Color.Gray, X, eyeY, 50, 30);
        SplashKit.DrawLine(Color.Black, X, mouthY, X + 50, Y + 35);
    }

}

public class BlackBox : Robot
{
    public BlackBox(Window gameWindow, Player player)
    {
        SplashKit.Rnd();
        if (SplashKit.Rnd() < 0.5)
        {
            X = SplashKit.Rnd(gameWindow.Width);


            if (SplashKit.Rnd() < 0.5)

                Y = -Height;

            else

                Y = gameWindow.Height;
        }
        else
        {
            Y = SplashKit.Rnd(gameWindow.Height);

            if (SplashKit.Rnd() < 0.5)

                X = -Width;

            else

                Y = gameWindow.Width;

        }

        MainColor = Color.RandomRGB(200);

        //all caps for constant? Convention?
        int speed = 4;
        if (SplashKit.KeyDown(KeyCode.SpaceKey))
        {
            speed += 7;
        }


        Point2D fromPt = new Point2D()
        {
            X = X,
            Y = Y
        };

        Point2D toPt = new Point2D()
        {
            X = player.X,
            Y = player.Y
        };

        Vector2D dir;
        dir = SplashKit.UnitVector(SplashKit.VectorPointToPoint(fromPt, toPt));

        Velocity = SplashKit.VectorMultiply(dir, speed);
    }
    public override void Draw()
    {
        double leftX;
        double rightX;

        double eyeY;
        double mouthY;

        leftX = X + 12;
        rightX = X + 27;

        eyeY = Y + 10;
        mouthY = Y + 30;

        SplashKit.FillRectangle(Color.Black, X, Y, Width, Height);

        SplashKit.FillRectangle(MainColor, leftX, eyeY, 10, 10);
        SplashKit.FillRectangle(MainColor, rightX, eyeY, 10, 10);
        SplashKit.FillRectangle(MainColor, leftX, mouthY, 25, 10);
        SplashKit.FillRectangle(MainColor, leftX + 2, mouthY + 2, 21, 6);
    }
}



public class RoboDodge
{
    private Player _player;
    private Window _gameWindow;
    private bool _success = false;
    public bool Success
    {
        get { return _success; }
        set { _success = value; }
    }
    private List<Robot> _robots = new List<Robot>();
    private List<Robot> _checkCollisions = new List<Robot>();
    private List<DisplayLife> _livesRemaining = new List<DisplayLife>();
    public bool Quit
    {
        get { return _player.Quit; }
    }

    public RoboDodge(Window gameWindow)
    {
        _gameWindow = gameWindow;

        RandomBot(gameWindow);

        
        Player Player = new Player(gameWindow);
        _player = Player;

    }
    public void HandleInput()
    {
        _player.HandleInput();
        _player.StayOnWindow();
    }
    public void Draw()
    {

        // DrawBackground();
        DrawLife();

        DisplayScore timing = new DisplayScore(_gameWindow);
        timing.Draw();


        foreach (Robot a in _robots)
        {
            a.Draw();
        }
        _player.Draw();
        DisplayLife life = new DisplayLife(_gameWindow);
        life.Draw();
        DrawLife();
        _gameWindow.Refresh(60);
    }

    public void RandomBot(Window gameWindow)
    {
        _gameWindow = gameWindow;
        Player player1 = new Player(gameWindow);
    }
    public void Update()
    {
        int count = _robots.Count;

        for (int i = 10; i > _robots.Count; i--)
        {
            Boxy testBoxy = new Boxy(_gameWindow, _player);
            _robots.Add(testBoxy);

            for (int j = 0; j == _robots.Count % 2; j += 2)
            {
                Roundy testRoundy = new Roundy(_gameWindow, _player);
                _robots.Add(testRoundy);
            }

            BlackBox testBlacky = new BlackBox(_gameWindow, _player);
            _robots.Add(testBlacky);
        }

        foreach (Robot a in _robots)
        {
            a.Update();
            a.isOffScreen(_gameWindow);
        }
        CheckCollisions();
    }

    public void CheckCollisions()
    {
        foreach (Robot a in _robots)
        {
            if (_player.CollidedWith(a) || (a.OffScreen == true))
            {
                _checkCollisions.Add(a);
            }

            if (_player.CollidedWith(a))
            {
                Bitmap ExplosionBitmap = new Bitmap("Bang", "explosion.png");
                ExplosionBitmap.Draw(_player.X, _player.Y);
                _gameWindow.DrawText("Ouch!", Color.Red, "StencilStd.otf", 100, _player.X, _player.Y);
                SoundEffect Bang = new SoundEffect("Bang", "explosion-1.wav");
                Bang.Play(1, 0.5F);
                SplashKit.Delay(150);
                _gameWindow.Refresh(60);

                if (_livesRemaining.Count > 1)
                {
                    _livesRemaining.RemoveAt(0);
                }

                else if (_livesRemaining.Count == 1)
                {
                    _livesRemaining.RemoveAt(0);
                    _gameWindow.Clear(Color.Black);
                    _gameWindow.DrawText("GAME OVER", Color.Red, "StencilStd.otf", 200, 350, _gameWindow.Height / 2);
                    _gameWindow.Refresh(60);
                    SplashKit.Delay(4000);
                    _gameWindow.Close();
                }

            }
        }

        foreach (Robot a in _checkCollisions)
        {
            _robots.Remove(a);
        }



    }


    public void DrawLife()
    {
        for (int k = 0; k < 3 && Success == false; k++)
        {
            DisplayLife remaining = new DisplayLife(_gameWindow);

            _livesRemaining.Add(remaining);




            if (_livesRemaining.Count > 3)
            {
                _livesRemaining.Remove(remaining);
                Success = true;
            }
        }

        string livesCount = Convert.ToString(_livesRemaining.Count);
        _gameWindow.DrawText(livesCount, Color.MistyRose, "StencilStd.otf", 25, 700, 570);


    }

}