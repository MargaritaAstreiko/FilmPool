
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO

Namespace FileStorage
  Public Class Picture
    Public Function UploadImage(imgData As Byte(), Id As Integer) As Integer
      Dim connection As New SqlConnection("server=.; database=FilmPool; Trusted_Connection=True; TrustServerCertificate=Yes;")
      Dim command As New SqlCommand("UPDATE Films SET Picture = @Image WHERE Id = @Id", connection)

      'Create an Image object.'
      'Dim fi As New FileInfo(Path)'

      ' If fi.Exists = True Then'
      ' Open the Image file for Read
      'Dim imgStream As Stream ='
      'fi.OpenRead'

      'Dim imgData(imgStream.Length) As Byte'
      'imgStream.Read(imgData, 0, fi.Length)'


      'Get an array of Bytes from the stream and assign to the parameter.'
      command.Parameters.Add("@Id", SqlDbType.Int).Value = Id
      command.Parameters.Add("@Image", SqlDbType.Image).Value = imgData
      connection.Open()
      command.ExecuteNonQuery()
      connection.Close()
      'End If'
    End Function
  End Class
End Namespace
