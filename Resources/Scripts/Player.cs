using System;
using SplashKitSDK;
public class Player
{
    private Bitmap _PlayerBitmap;
    private Window _gameWindow;

    public double X { get; private set; }
    public double Y { get; private set; }
    public Boolean Quit { get; private set; }


    public int Width
    {
        get { return _PlayerBitmap.Width; }
    }

    public int Height
    {
        get { return _PlayerBitmap.Height; }
    }

    public Player(Window gameWindow)
    {
        _gameWindow = gameWindow;

        Bitmap playerBitmap = new Bitmap("Player", "Player.png");
        _PlayerBitmap = playerBitmap;


        X = (gameWindow.Width - Width) / 2;
        Y = (gameWindow.Height - Height) / 2;

    }
    public void Draw()
    {
        _PlayerBitmap.Draw(X, Y);
    }


    public void HandleInput()
    {
        int speed = 5;
        Quit = false;

        if (SplashKit.KeyDown(KeyCode.UpKey))
        {

            Y += -speed;
            _PlayerBitmap.Draw(X, Y);

            StayOnWindow();

        }

        if (SplashKit.KeyDown(KeyCode.DownKey))
        {
            Y += speed;
            _PlayerBitmap.Draw(X, Y);
            StayOnWindow();
        }

        if (SplashKit.KeyDown(KeyCode.LeftKey))
        {
            X += -speed;
            _PlayerBitmap.Draw(X, Y);
            StayOnWindow();
        }

        if (SplashKit.KeyDown(KeyCode.RightKey))
        {
            X += speed;
            _PlayerBitmap.Draw(X, Y);
            StayOnWindow();
        }

        if (SplashKit.KeyTyped(KeyCode.EscapeKey))
        {
            Quit = true;
            _gameWindow.Close();
           
        }

        SplashKit.ProcessEvents();
    }

    public void StayOnWindow()
    {
        const int gap = 10;

        if (X < gap)
        {
            X = gap;
        }

        if (Y < gap)
        {
            Y = gap;
        }



        if (X > _gameWindow.Width - Width - gap)
        {
            X = _gameWindow.Width - Width - gap;
        }

        if (Y > _gameWindow.Height - Height - gap)
        {
            Y = _gameWindow.Height - Height - gap;
        }
    }


    public bool CollidedWith(Robot other)
    {
        return _PlayerBitmap.CircleCollision(X, Y, other.CollissionCircle);

    }

}


