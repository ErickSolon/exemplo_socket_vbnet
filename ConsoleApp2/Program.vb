Imports System
Imports System.Net
Imports System.Net.Sockets
Imports System.Text

Module Program
    Sub Main(args As String())
        Dim ip As New IPEndPoint(IPAddress.Parse("127.0.0.1"), 4443)
        Dim sc As New Socket(ip.AddressFamily, SocketType.Stream, ProtocolType.Tcp)

        sc.Bind(ip)
        sc.Listen(10)

        While True
            Dim socketCliente As Socket = sc.Accept()

            While True
                Try
                    Dim buffer(1024) As Byte

                    Dim resposta As Integer = socketCliente.Receive(buffer, SocketFlags.None)
                    If resposta > 0 Then
                        Dim resposta_decript As String = Encoding.UTF8.GetString(buffer, 0, resposta)

                        Console.WriteLine(resposta_decript)
                    Else
                        Exit While
                    End If
                Catch ex As SocketException
                    Exit While
                End Try
            End While

            socketCliente.Close()
        End While
    End Sub
End Module
