Imports MySql.Data.MySqlClient

Public Class Cadastrar

    Private Sub Cadastrar_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try
            AbrirConexao()
        Catch ex As Exception
            MessageBox.Show($"Erro ao conectar: {ex.Message}")
        End Try

    End Sub

    Private Sub btn_confirmar_Click(sender As Object, e As EventArgs) Handles btn_confirmar.Click

        Dim sql As String
        Dim cmd As New MySqlCommand

        Dim usuario As String = txt_usuario.Text.Trim()
        Dim nome As String = txt_nome.Text.Trim()
        Dim email As String = txt_email.Text.Trim()
        Dim senha As String = txt_senha.Text

        'Validação da confirmação da senha
        If txt_csenha.Text <> senha Then
            MessageBox.Show("As senhas não coincidem.")
            Exit Sub
        End If

        'Validação da senha
        If senha.Length < 8 Then
            MessageBox.Show("A senha deve possuir pelo menos 8 caracteres.")
            Exit Sub
        End If

        Try
            AbrirConexao()

            'Verifica se o usuário já existe
            sql = "SELECT COUNT(*) FROM usuarios WHERE usuario = @usuario OR email = @email"

            cmd = New MySqlCommand(sql, conexao)
            cmd.Parameters.AddWithValue("@usuario", usuario)

            Dim qtd As Integer = Convert.ToInt32(cmd.ExecuteScalar())

            If qtd > 0 Then
                MessageBox.Show("Este usuário já está cadastrado.")
                Exit Sub
            End If

            'Insere o usuário
            sql = "INSERT INTO usuarios (nome, usuario, senha, email) VALUES (@nome, @usuario, @senha, @email)"

            cmd = New MySqlCommand(sql, conexao)

            cmd.Parameters.AddWithValue("@nome", nome)
            cmd.Parameters.AddWithValue("@usuario", usuario)
            cmd.Parameters.AddWithValue("@senha", senha)
            cmd.Parameters.AddWithValue("@email", email)

            cmd.ExecuteNonQuery()

            MessageBox.Show("Usuário cadastrado com sucesso!")
            Limpar_cadastro()

        Catch ex As Exception
            MessageBox.Show($"Erro ao cadastrar: {ex.Message}")

        Finally
            conexao.Close()

        End Try

    End Sub

    Private Sub txt_email_TextChanged(sender As Object, e As EventArgs) Handles txt_email.TextChanged

    End Sub

    Private Sub Guna2GradientPanel1_Paint(sender As Object, e As PaintEventArgs) Handles Guna2GradientPanel1.Paint

    End Sub
End Class