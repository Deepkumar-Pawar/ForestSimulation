Module Module1

    Structure Tree

        Public type As String
        Public age As Integer
        Public isDead As Boolean
        Public isHarvested As Boolean

    End Structure

    Sub Main()

        Dim day As Integer = 0
        Dim month As Integer = 0
        Dim year As Integer = 0
        Dim simYears As Integer = 25

        Dim forest() As Tree = GenerateNewForest(10000)
        Dim time As Integer

        For year = 1 To 25
            For day = 1 To 365

                If day > 30 And day <= 50 Then

                    time = 60 * 8

                    Dim i As Integer
                    Dim oakCutPerDay As Integer = 0
                    Dim pineCutPerDay As Integer = 0
                    Dim tree As Tree

                    While time > 0
                        If True Then
                            tree = forest(i)

                            If tree.type = "pine" And tree.age >= 25 And tree.age <= 75 & pineCutPerDay <= 10 Then
                                tree.isDead = True
                                time -= 40
                                pineCutPerDay += 1
                            ElseIf tree.type = "oak" And tree.age >= 90 And tree.age <= 150 & oakCutPerDay <= 4 Then
                                tree.isDead = True
                                time -= 40
                                oakCutPerDay += 1
                            End If

                            time -= 15
                        End If
                    End While

                End If

            Next

            DisplayYearlyFigures(forest, year)

        Next

        Console.ReadLine()
    End Sub

    Function GenerateNewForest(ByVal size As Integer)

        Dim forest(size - 1) As Tree

        For Each tree As Tree In forest
            Dim r As Double = Rnd() * 5
            If r > 1 Then
                tree.type = "pine"
                tree.age = 30
            Else
                tree.type = "oak"
                tree.age = 80
            End If

            tree.isDead = False
            tree.isHarvested = False
        Next

        Return forest
    End Function

    Sub DisplayYearlyFigures(ByVal forest() As Tree, ByVal year As Integer)

        Dim numberOfPines As Integer = 0
        Dim numberOfOaks As Integer = 0
        Dim averageAgeOfPines As Double = 0
        Dim averageAgeofOaks As Double = 0
        Dim averageAgeOfAllTrees As Double = 0

        For Each tree As Tree In forest
            If tree.type = "oak" Then
                numberOfOaks += 1
                averageAgeofOaks += tree.age
            ElseIf tree.type = "pine" Then
                numberOfPines += 1
                averageAgeOfPines += tree.age
            End If

            averageAgeOfAllTrees += tree.age
        Next

        averageAgeofOaks /= numberOfOaks
        averageAgeOfPines /= numberOfPines
        averageAgeOfAllTrees /= forest.Length

        Console.WriteLine()
        Console.WriteLine("YEARLY FIGURES for YEAR " & year)
        Console.WriteLine()
        Console.WriteLine("Number of Oaks: " & numberOfOaks)
        Console.WriteLine("Number of Pines: " & numberOfPines)
        Console.WriteLine("Average age of Oaks: " & averageAgeofOaks)
        Console.WriteLine("Average age of Pines: " & averageAgeOfPines)
        Console.WriteLine("Average age of all trees: " & averageAgeOfAllTrees)

    End Sub

    Function HarvestTree(ByVal tree As Tree)

        If tree.type = "oak" & tree.age > 90 & tree.age < 150 Then
            tree.isHarvested = True
        End If

        Return tree
    End Function

    Function CutTree(ByVal tree As Tree)

        If tree.type = "pine" Then
            If tree.age > 25 & tree.age < 70 Then
                tree.isDead = True
            End If
        ElseIf tree.type = "oak" Then
            If tree.age > 90 & tree.age < 150 Then
                tree.isDead = True
            End If
        End If

        Return tree
    End Function

    Function PlantNewTree(ByVal tree As Tree)

        Dim r As Double = Rnd() * 5

        If r > 1 Then
            tree.type = "pine"
            tree.age = 30
        Else
            tree.type = "oak"
            tree.age = 80
        End If

        tree.isDead = False

        Return tree
    End Function

End Module
