Public Class Form1
    Inherits formulario

    Sub New()
        Me.InitializeComponent()
        Me.RegistrarAtalhoGlobal(HKey.Mods.Ctrl Or HKey.Mods.Shift, Keys.I, AddressOf Me.iniciar)
        Me.RegistrarAtalhoGlobal(HKey.Mods.Ctrl Or HKey.Mods.Shift, Keys.P, AddressOf Me.finalizar)
    End Sub

    Sub iniciar()
        MsgBox("iniciar")
    End Sub

    Sub finalizar()
        MsgBox("finalizar")
    End Sub

End Class
