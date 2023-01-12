using System;
using System.Reflection.Emit;
using System.Threading;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

class StressTestClient
{
    static int _concurrentUsers = 0;
    static int _maxConcurrentUsers = 1000;
    static string _serverIp = "127.0.0.1";

    [STAThread]
    static void Main()
    {
        var form = new Form();
        var serverIpLabel = new System.Windows.Forms.Label();
        var serverIpText = new TextBox();
        var clientAmountLabel = new System.Windows.Forms.Label();
        var clientAmountText = new TextBox();
        var startButton = new Button();

        form.Text = "Stress Test";
        serverIpLabel.Text = "Server IP:";
        clientAmountLabel.Text = "Client Amount:";
        startButton.Text = "Start";
        startButton.Click += (sender, e) => StartStressTest(serverIpText, clientAmountText);

        serverIpText.Text = _serverIp;
        clientAmountText.Text = _maxConcurrentUsers.ToString();

        serverIpLabel.SetBounds(10, 10, 70, 20);
        serverIpText.SetBounds(90, 10, 150, 20);
        clientAmountLabel.SetBounds(10, 40, 70, 20);
        clientAmountText.SetBounds(90, 40, 150, 20);
        startButton.SetBounds(250, 10, 70, 50);

        form.Controls.Add(serverIpLabel);
        form.Controls.Add(serverIpText);
        form.Controls.Add(clientAmountLabel);
        form.Controls.Add(clientAmountText);
        form.Controls.Add(startButton);

        form.FormBorderStyle = FormBorderStyle.FixedSingle;
        form.StartPosition = FormStartPosition.CenterScreen;
        form.Width = 350;
        form.Height = 120;

        System.Windows.Forms.Application.Run(form);
    }

    static void StartStressTest(TextBox serverIpText, TextBox clientAmountText)
    {
        _serverIp = serverIpText.Text;
        _maxConcurrentUsers = int.Parse(clientAmountText.Text);

        for (int i = 0; i < _maxConcurrentUsers; i++)
        {
            ThreadPool.QueueUserWorkItem(RunUser);
        }
    }

    static void RunUser(Object stateInfo)
    {
        Interlocked.Increment(ref _concurrentUsers);

        try
        {
            // simulate user actions here
            Console.WriteLine("User connected to " + _serverIp + ". Total: " + _concurrentUsers);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
        finally
        {
            Interlocked.Decrement(ref _concurrentUsers);
        }
    }
}