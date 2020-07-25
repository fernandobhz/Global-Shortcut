Public Class formulario
    Inherits Form

    Dim hkr As New HKey(Me.Handle)
    Private Acoes As New List(Of Acao)

    Protected Sub RegistrarAtalhoGlobal(ByVal Modifier As HKey.Mods, ByVal Key As System.Windows.Forms.Keys, Acao As Acao)
        Me.hkr.Register(Modifier, Key)
        Me.Acoes.Add(Acao)
    End Sub

    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
        If m.Msg = HKey.Mensagens.HotKey Then Me.Acoes(m.WParam.ToInt32).Invoke()
        MyBase.WndProc(m)
    End Sub

End Class

Public Delegate Sub Acao()

Public NotInheritable Class HKey

    Private Declare Function RegisterHotKey Lib "user32.dll" (ByVal handle As IntPtr, ByVal id As Int32, ByVal fsModifier As Int32, ByVal vk As Int32) As Int32
    Private Declare Function UnregisterHotKey Lib "user32.dll" (ByVal handle As IntPtr, ByVal id As Int32) As Int32

    Private Handle As IntPtr = IntPtr.Zero
    Private Registry As New System.Collections.Generic.List(Of Int32)

    Public Enum Mensagens
        HotKey = &H312
    End Enum

    Public Enum Mods
        Alt = &H1
        Ctrl = &H2
        Shift = &H4
    End Enum

    Sub New(ByVal Handle As IntPtr)
        Me.Handle = Handle
    End Sub

    Public Function Register(ByVal Modifier As Int32, ByVal Key As System.Windows.Forms.Keys) As Int32
        Dim ret As Int32

        ret = NextAvailableIndex()
        Call RegisterHotKey(Me.Handle, ret, Modifier, Key)
        Registry.Insert(ret, ret)
        Return ret
    End Function

    Public Sub Unregister(ByVal ID As Int32)
        Call UnregisterHotKey(Me.Handle, ID)
        Registry.Remove(ID)
    End Sub

    Private Function NextAvailableIndex() As Int32
        Dim ret As Int32 = 0
        Dim n As Int32 = 0

        For i As Int32 = 0 To Registry.Count - 1
            If Registry(i) = n Then
                n = n + 1
            ElseIf n < Registry(i) Then
                Return n
            End If
        Next
        If n = Registry.Count Then
            Return Registry.Count
        End If
        Return ret
    End Function

End Class
