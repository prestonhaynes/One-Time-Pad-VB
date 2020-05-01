'
' OneTimePad.vb 
'
' This program allows you to encrypt a message using a one-time pad for the
' letters [A...Z] and spaces being perserved.
'
' @Author: Preston Haynes
'

Imports System

Module OneTimePad
    Private alphabetArray As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray()
    Private key As Integer()


    Sub Main(args As String())
        Dim choice As Integer = 0
        Randomize()


        While choice < 4
            Console.WriteLine("Enter: (1) Generate random key (2) Encrypt (3) Decrypt (4) Exit")
            Console.Write("Choice: ")
            Try

                choice = Console.ReadLine()
                Console.WriteLine(choice)

                Select Case choice
                    Case 1
                        Console.Write("Enter the length of the key: ")
                        Dim length As Integer = Console.ReadLine()
                        ReDim key(length)
                        key = getRandomNumber(length)
                        Console.WriteLine("Random key of length {0} generated", length)

                    Case 2
                        Console.Write("Enter the plaintext: ")
                        Dim plainText As String = Console.ReadLine().ToUpper
                        If plainText.Length <= key.Length Then
                            Dim encryptedText As String = encryptString(plainText)
                            Console.WriteLine("{0} encrypted using the one-time pad is {1}", plainText, encryptedText)
                        Else
                            Console.WriteLine("Key is too short to encrypt message, please use a key of {0} or longer to encypt message", key.Length)
                        End If

                    Case 3
                        Console.Write("Enter the ciphertext: ")
                        Dim encryptedText As String = Console.ReadLine().ToUpper
                        Dim plainText As String = decryptString(encryptedText)
                        Console.WriteLine("{0} decrypted using one-time pad is {1}", encryptedText, plainText)

                    Case 4
                        Console.WriteLine("Goodbye!")
                        Exit While

                    Case Else
                        choice = 0
                        Console.WriteLine("Please choose 1, 2, 3, or 4")
                End Select

            Catch ex As System.IndexOutOfRangeException
                Console.WriteLine()
                Console.WriteLine("Failed to create key. Please try again.")


            Catch ex As System.InvalidCastException
                Console.WriteLine("Please choose 1, 2, 3, or 4")

            Catch ex As Exception
                Console.WriteLine(ex)

            End Try
        End While

        Console.WriteLine("Press Any Key to Exit")

        Console.ReadLine()
    End Sub

    Function getRandomNumber(args As Integer) As Integer()
        Dim letters(args - 1) As Integer
        For index As Integer = 0 To (args - 1)
            letters(index) = CInt(Int(26 * Rnd()) - 1)
        Next
        Return letters
    End Function

    Function encryptString(plainText As String) As String
        Dim encryptedText(plainText.Length) As Char
        For index As Integer = 0 To plainText.Length - 1
            If plainText(index).Equals(" "c) Then
                encryptedText(index) = " "c
            Else
                Try
                    encryptedText(index) = alphabetArray((alphabetArray.IndexOf(plainText(index)) + key(index)) Mod 26)
                Catch ex As Exception

                End Try
            End If

        Next

        Return encryptedText
    End Function


    Function decryptString(encryptedText As String) As String
        Dim plainText(encryptedText.Length) As Char
        For index As Integer = 0 To encryptedText.Length - 1
            If encryptedText(index).Equals(" "c) Then
                plainText(index) = " "c
            Else
                Try
                    Dim keyValue As Integer = key(index)
                    Dim messageValue As Integer = alphabetArray.IndexOf(encryptedText(index))
                    Dim keyMessageDifference As Integer = (messageValue - keyValue)
                    While keyMessageDifference < 0
                        keyMessageDifference = keyMessageDifference + 26
                    End While
                    plainText(index) = alphabetArray(keyMessageDifference Mod 26)
                Catch ex As Exception
                    Console.WriteLine(ex)
                End Try
            End If

        Next

        Return plainText
    End Function
End Module
