Imports System.ComponentModel
Imports System.Text
Imports Bunifu
Imports System.Net
Imports System.IO
Imports Newtonsoft.Json
Imports System.Linq

Public Class Home

    Private Sub printJSON(jsonStr As String, ByVal currency As String, ByVal icon As Image)

        ' Json.NET deserializer gives you a list of .NET objects
        Dim Eassel = JsonConvert.DeserializeObject(Of List(Of JSON_data))(jsonStr)



        ' Set up a LINQ statement to filter the monarchs list
        Dim EasselList = From monarch In Eassel Where monarch.n.Contains(currency) Select monarch

        ' Print the results of our LINQ query
        For Each EasselX In EasselList
            mygridview.Rows.Add(icon, EasselX.n, EasselX.v, EasselX.k, EasselX.cp, EasselX.d.Substring(0, EasselX.d.Length - 14))
        Next

        'Loop Through Gridview to change the background color of the cell in case currency value were down 
        For i As Integer = 0 To mygridview.Rows.Count - 1
            If mygridview.Rows(i).Cells(4).Value.ToString.Contains("-") Then
                mygridview.Rows(i).Cells(4).Style.BackColor = Color.Red
            Else
                mygridview.Rows(i).Cells(4).Style.BackColor = Color.White
            End If
        Next
    End Sub
    Public Class JSON_data
        'Defining the variables that holds data from Json File
        Public n As String
        Public k As String
        Public v As String
        Public cp As String
        Public d As String
    End Class

    Private Sub FinalFunction()
        Dim errorMsg As String = Nothing
        Try
            'Define the json url
            Dim jsonURL As String = "http://essale.herokuapp.com/api/tick"
            Dim reader As StreamReader
            ' make an HTTP request to our json url
            Dim request As HttpWebRequest = CType(WebRequest.Create(jsonURL), HttpWebRequest)
            Dim response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)
            'Read Data from Json File
            reader = New StreamReader(response.GetResponseStream())
            Dim jsonStr As String = reader.ReadToEnd()
            'Sending the currency name to brings back the data from json file
            printJSON(jsonStr, "USD", My.Resources.USA_32px)
            printJSON(jsonStr, "EUR", My.Resources.Flag_Of_Europe_32px)
            printJSON(jsonStr, "GBP", My.Resources.Great_Britain_32px)
            printJSON(jsonStr, "TUN", My.Resources.Tunisia_32px)
            printJSON(jsonStr, "TUR", My.Resources.Turkey_32px)
            printJSON(jsonStr, "EGP", My.Resources.Egypt_32px)
            printJSON(jsonStr, "CJM", My.Resources.Transaction_32px)
        Catch ex As WebException
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Home_Load(sender As Object, e As EventArgs) Handles Me.Load
        FinalFunction()
    End Sub
End Class
