Module Module1

    Structure Tree

        Public type As String
        Public age As Integer
        Public isDead As Boolean
        Public isHarvested As Boolean
        Public isDiseased As Boolean

    End Structure

    Sub Main()

        Dim day As Integer = 0
        Dim month As Integer = 0
        Dim year As Integer = 0
        Dim simYears As Integer = 25

        Dim forest() As Tree = GenerateNewForest(10000)

        Dim newTreesPlanted As Integer = 0

        Dim pineTreesCutPerYear As Integer = 0
        Dim oakTreesCutPerYear As Integer = 0

        Dim oakHarvest As Integer = 0
        Dim pineHarvest As Integer = 0
        Dim trucksNeededForHarvest As Integer = 0

        For year = 1 To 25

            pineTreesCutPerYear = 0
            oakTreesCutPerYear = 0

            oakHarvest = 0
            pineHarvest = 0
            trucksNeededForHarvest = 0

            For day = 1 To 365

                If day > 30 And day <= 50 Then

                    forest = CutTrees(forest, pineTreesCutPerYear, oakTreesCutPerYear, newTreesPlanted, oakHarvest, pineHarvest)

                End If

                If day = 50 Then
                    trucksNeededForHarvest = HarvestTrees(oakHarvest, pineHarvest)
                End If

            Next

            Dim i As Integer
            For i = 0 To forest.Length - 1
                forest(i) = AgeTree(forest(i))
            Next

            'ADD LOGIC FOR DISEASING TREES AND ETC LOL

            DisplayYearlyFigures(forest, year, pineTreesCutPerYear, oakTreesCutPerYear, newTreesPlanted, trucksNeededForHarvest)

        Next

        Console.ReadLine()
    End Sub

    Function CutTrees(ByVal forest() As Tree, ByRef pineTreesCutPerYear As Integer, ByRef oakTreesCutPerYear As Integer, ByRef newTreesPlanted As Integer,
                      ByRef oakHarvest As Integer, ByRef pineHarvest As Integer)

        Dim time As Integer = 480

        Dim i As Integer = (Rnd() * 10000)
        Dim oakCutPerDay As Integer = 0
        Dim pineCutPerDay As Integer = 0

        While time > 0 And i < forest.Length

            If forest(i).isDiseased = False Then

                If forest(i).type = "pine" And forest(i).age >= 25 And forest(i).age <= 75 And pineCutPerDay <= 10 Then
                    forest(i) = KillTree(forest(i))
                    time -= 35
                    pineCutPerDay += 1
                    pineTreesCutPerYear += 1

                    pineHarvest += 1

                    forest(i) = PlantNewTree(forest(i), newTreesPlanted)
                    time -= 10
                ElseIf forest(i).type = "oak" And forest(i).age >= 90 And forest(i).age <= 150 And oakCutPerDay <= 4 Then
                    forest(i) = KillTree(forest(i))
                    time -= 35
                    oakCutPerDay += 1
                    oakTreesCutPerYear += 1

                    oakHarvest += 1

                    forest(i) = PlantNewTree(forest(i), newTreesPlanted)
                    time -= 10
                End If

            Else
                forest(i) = KillTree(forest(i))
                time -= 35

                If forest(i).type = "pine" Then
                    pineCutPerDay += 1
                    pineTreesCutPerYear += 1
                ElseIf forest(i).type = "oak" Then
                    oakCutPerDay += 1
                    oakTreesCutPerYear += 1
                End If

                forest(i) = PlantNewTree(forest(i), newTreesPlanted)
                time -= 10
            End If

            'Add logic to record when maple trees are getting cut and how many

            If i >= forest.Length Then
                i -= forest.Length
            End If

            i += 1
            time -= 5
        End While

        Return forest
    End Function
    Function HarvestTrees(ByVal oakHarvest As Integer, ByVal pineHarvest As Integer)

        Dim trucks As Integer = 0

        trucks = Math.Ceiling(oakHarvest / 15 + pineHarvest / 35)

        Return trucks
    End Function
    Function GenerateNewForest(ByVal size As Integer)

        Dim forest(size - 1) As Tree

        Dim i As Integer
        For i = 1 To size
            Dim r As Double = Rnd() * 5
            If r > 1 Then
                forest(i - 1).type = "pine"
                forest(i - 1).age = 30
            Else
                forest(i - 1).type = "oak"
                forest(i - 1).age = 100
            End If

            Dim d As Double = Rnd() * 40
            If d < 2 Then
                forest(i - 1).isDiseased = True
            Else
                forest(i - 1).isDiseased = False
            End If

            forest(i - 1).isDead = False
            forest(i - 1).isHarvested = False
        Next

        Return forest
    End Function
    Sub DisplayYearlyFigures(ByVal forest() As Tree, ByVal year As Integer, ByVal pineCut As Integer, ByVal oakCut As Integer, ByVal newTreesPlanted As Integer, ByVal trucksNeededForHarvest As Integer)

        Dim numberOfPines As Integer = 0
        Dim numberOfOaks As Integer = 0
        Dim numberOfMaples As Integer = 0
        Dim averageAgeOfPines As Double = 0
        Dim averageAgeofOaks As Double = 0
        Dim averageAgeOfMaples As Double = 0
        Dim averageAgeOfAllTrees As Double = 0

        For Each tree As Tree In forest
            If tree.isDead = False Then
                If tree.type = "oak" Then
                    numberOfOaks += 1
                    averageAgeofOaks += tree.age
                ElseIf tree.type = "pine" Then
                    numberOfPines += 1
                    averageAgeOfPines += tree.age
                ElseIf tree.type = "maple" Then
                    numberOfMaples += 1
                    averageAgeOfMaples += tree.age
                End If

                averageAgeOfAllTrees += tree.age
            End If
        Next

        averageAgeofOaks /= numberOfOaks
        averageAgeOfPines /= numberOfPines
        averageAgeOfMaples /= numberOfMaples
        averageAgeOfAllTrees /= forest.Length

        Console.WriteLine()
        Console.WriteLine("YEARLY FIGURES for YEAR " & year)
        Console.WriteLine()
        Console.WriteLine("Number of Oaks: " & numberOfOaks)
        Console.WriteLine("Number of Pines: " & numberOfPines)
        Console.WriteLine("Number of Maples: " & numberOfMaples)
        Console.WriteLine("Number of Pines Cut: " & pineCut)
        Console.WriteLine("Number of Oaks Cut: " & oakCut)
        Console.WriteLine("Number of Trucks needed for Harvest: " & trucksNeededForHarvest)
        Console.WriteLine("Number of New Trees Planted: " & newTreesPlanted)
        Console.WriteLine("Average age of Oaks: " & averageAgeofOaks)
        Console.WriteLine("Average age of Pines: " & averageAgeOfPines)
        Console.WriteLine("Average age of Maples: " & averageAgeOfMaples)
        Console.WriteLine("Average age of all trees: " & averageAgeOfAllTrees)

    End Sub
    Function PlantNewTree(ByVal tree As Tree, ByRef NumberOfNewTreesPlanted As Integer)

        If (((NumberOfNewTreesPlanted - 1) Mod 3) = 0) Then
            tree.type = "maple"
        End If

        tree.isDead = False
        tree.isHarvested = False
        tree.age = 0

        NumberOfNewTreesPlanted += 1
        Return tree
    End Function
    Function AgeTree(ByVal tree As Tree)

        tree.age += 1

        Return tree
    End Function
    Function KillTree(ByVal tree As Tree)
        tree.isDead = True

        Return tree
    End Function

End Module
